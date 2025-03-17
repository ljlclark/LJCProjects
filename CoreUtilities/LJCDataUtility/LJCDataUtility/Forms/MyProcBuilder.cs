// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MyProcBuilder.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtility
{
  //Provides MySQL procedure SQL code.
  internal class MyProcBuilder
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal MyProcBuilder(DataUtilityList parentObject, string dbName = null
      , string tableName = null)
    {
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
      Reset(dbName, tableName);
    }

    // Resets the text values.
    internal void Reset(string dbName = null, string tableName = null)
    {
      if (NetString.HasValue(dbName))
      {
        DBName = dbName;
      }

      if (NetString.HasValue(tableName))
      {
        TableName = tableName;
        AddProcName = $"mysp_{TableName}Add";
        PKName = $"mypk_{TableName}";
        UQName = $"myuq_{TableName}";
        CreateProcName = $"mysp_{TableName}";
      }

      BeginDelimiter = "`";
      EndDelimiter = "`";
      Builder = new TextBuilder(512);
      HasColumns = false;
    }
    #endregion

    #region Data Class Methods

    //Returns the builder string.
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Builder Methods

    internal void Add(string text)
    {
      Builder.Add(text);
    }

    //Clears the Builder text.
    internal void ClearText()
    {
      Builder = new TextBuilder(512);
    }

    // Checks if the builder text ends with a supplied value.
    internal bool EndsWith(string value)
    {
      bool retValue = false;
      var text = Builder.ToString();
      if (text.EndsWith(value))
      {
        retValue = true;
      }
      return retValue;
    }

    // Adds a line to the builder.
    internal void Line(string text = null)
    {
      Builder.Line(text);
    }

    // Adds text to the builder.
    internal void Text(string text)
    {
      Builder.Text(text);
    }
    #endregion

    #region Procedure Methods

    // Adds the Procedure begin code.
    internal string Begin(string procedureName)
    {
      var b = new TextBuilder(512);
      b.Line("-- Copyright(c) Lester J. Clark and Contributors.");
      b.Line("-- Licensed under the MIT License.");
      b.Line($"-- {procedureName}.sql");
      var qualifiedName = QualifiedName(DBName, procedureName);
      b.Line("DELIMITER //");
      b.Line($"DROP PROCEDURE IF EXISTS {qualifiedName};");
      b.Line();
      b.Line($"CREATE PROCEDURE {qualifiedName} (");
      string retString = b.ToString();

      //Text(retString);
      Add(retString);
      return retString;
    }

    // Creates the insert Columns list.
    internal string ColumnsList(DataColumns dataColumns
      , bool includeParens = true, bool useNewNames = false
      , bool includeID = false, int indentCount = 0)
    {
      var b = new TextBuilder(256)
      {
        IndentCount = indentCount
      };
      var value = "    ";
      if (includeParens)
      {
        value += "(";
      }
      b.Text(value);

      if (NetCommon.HasItems(dataColumns))
      {
        b.IsFirst = true;
        foreach (DataUtilColumn dataColumn in dataColumns)
        {
          if (!includeID
            && "ID" == dataColumn.Name)
          {
            continue;
          }

          var nameValue = dataColumn.Name;
          if (useNewNames
            && NetString.HasValue(dataColumn.NewName))
          {
            nameValue = dataColumn.NewName;
          }
          b.Item(nameValue);
        }

        if (includeParens)
        {
          b.Text(")");
        }
      }
      var retList = b.ToString();
      return retList;
    }

    // Gets the Table row IF statement.
    internal string IFItem(string parentTableName
      , string parentIDColumnName, string parentFindColumnName
      , string parmFindName)
    {
      // Reference name "TableNameParentID".
      var varRefName
        = SQLVarName($"{parentTableName}{parentIDColumnName}");

      var b = new TextBuilder(128);
      b.Line($"(SET {varRefName} = (SELECT {parentIDColumnName}");
      b.Line($" FROM {parentTableName}");
      b.Line($" WHERE {parentFindColumnName} = {parmFindName});");
      var retIf = b.ToString();
      return retIf;
    }

    // Creates the Parameters.
    internal string Parameters(DataColumns dataColumns, bool isFirst = true
      , int indentCount = 0)
    {
      var b = new TextBuilder(128);
      {
        IndentCount = indentCount;
      }
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (!dataColumn.Name.EndsWith("ID"))
        {
          if (!isFirst)
          {
            b.Line(",");
          }
          isFirst = false;
          var declaration = SQLDeclaration(dataColumn);
          b.Text($"  {declaration}");
        }
      }
      var retParams = b.ToString();
      return retParams;
    }

    // Creates the qualified name.
    internal string QualifiedName(string dbName, string name)
    {
      string retValue = null;

      if (NetString.HasValue(dbName))
      {
        retValue += $"`{DBName}`.";
      }
      retValue += $"`{name}`";
      return retValue;
    }

    // Creates a SQL Declaration variable from a DataUtilityColumn.
    internal string SQLDeclaration(DataUtilColumn dataColumn)
    {
      var retValue = "";

      // @name nvarchar(60)
      // If value is a variable, it needs an Identifier Quote.
      retValue += "`";
      retValue += SQLVarName(dataColumn.Name);
      retValue += "`";
      retValue += $" {dataColumn.TypeName}";
      if (dataColumn.MaxLength > 0)
      {
        retValue += $"({dataColumn.MaxLength})";
      }
      return retValue;
    }

    // Creates a SQL variable name from a column name.
    internal string SQLVarName(string columnName)
    {
      var retName = "";

      // @name
      var startChar = columnName.ToLower()[0];
      retName += $"@{startChar}";
      retName += columnName.Substring(1);
      return retName;
    }

    // Creates the Values list.
    internal string ValuesList(DataColumns dataColumns
      , List<string> varRefNames = null, int indentCount = 0)
    {
      var b = new TextBuilder(256)
      {
        IndentCount = indentCount
      };
      b.Text("    VALUES(");

      // Use the variable references instead of value.
      if (NetCommon.HasItems(varRefNames))
      {
        b.IsFirst = true;
        foreach (string varRefName in varRefNames)
        {
          b.Item(varRefName);
        }
      }

      if (NetCommon.HasItems(dataColumns))
      {
        b.IsFirst = true;
        foreach (DataUtilColumn dataColumn in dataColumns)
        {
          if (dataColumn.Name.EndsWith("ID"))
          {
            continue;
          }

          var nameValue = SQLVarName(dataColumn.Name);
          nameValue = $"`{nameValue}`";
          b.Item(nameValue);
        }
      }

      b.Text(");");
      var retList = b.ToString();
      return retList;
    }
    #endregion

    #region Create Table Methods

    // Adds a foreign key.
    internal string AddForeignKey(string tableName
      , string objectName, string sourceColumnList
      , string targetTableName, string targetColumnList)
    {
      var sourceNames = NetString.DelimitValues(sourceColumnList, "`", "`");
      var targetNames = NetString.DelimitValues(targetColumnList, "`", "`");
      var b = new TextBuilder(128);
      b.Line($"ALTER TABLE `{tableName}`");
      b.Line($"  ADD CONSTRAINT `{objectName}`");
      b.Line($"   FOREIGN KEY ({sourceNames})");
      b.Text($"   REFERENCES `{targetTableName}`");
      b.Text($" ({targetNames});");
      var retValue = b.ToString();
      return retValue;
    }

    // Adds a primary key.
    internal string AddPrimaryKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = NetString.DelimitValues(columnList, "`", "`");
      var b = new TextBuilder(128);
      b.Line($"ALTER TABLE `{tableName}`");
      b.Line($"  ADD CONSTRAINT `{objectName}`");
      b.Text($"  PRIMARY KEY ({columnNames});");
      var retValue = b.ToString();
      return retValue;
    }

    // Adds a unique key.
    internal string AddUniqueKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = NetString.DelimitValues(columnList, "`", "`");
      var b = new TextBuilder(128);
      b.Line($"ALTER TABLE `{tableName}`");
      b.Line($"  ADD CONSTRAINT `{objectName}`");
      b.Text($"  UNIQUE ({columnNames});");
      var retValue = b.ToString();
      return retValue;
    }

    // Creates the Create Table SQL.
    internal string CreateTable(DataColumns dataColumns)
    {
      Line(TableBegin());
      bool isAutoIncrement = false;
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (dataColumn.IdentityIncrement > 0)
        {
          isAutoIncrement = true;
          Text(TableIdentity(dataColumn));
        }
        else
        {
          if (dataColumn.NewMaxLength > 0)
          {
            dataColumn.MaxLength = dataColumn.NewMaxLength;
          }
          Text(TableColumn(dataColumn));
        }
      }

      if (isAutoIncrement)
      {
        Line(", ");
        Text("  PRIMARY KEY (`ID`)");
      }

      Line();
      Text(")");
      if (isAutoIncrement)
      {
        Text(" AUTO_INCREMENT = 1");
      }
      Line(";");
      var retProc = ToString();
      return retProc;
    }

    // Complete Create Table procedure.
    internal string CreateTableProc(DataColumns dataColumns)
    {
      Begin(CreateProcName);
      //Line("  IN parm varchar(60)");
      Line(")");
      Text("BEGIN");

      CreateTable(dataColumns);

      var keyValues = ParentObject.UniqueKeyValues();
      if (NetString.HasValue(keyValues))
      {
        Line();
        var text = AddUniqueKey(TableName, UQName, keyValues);
        Text(text);
      }

      // *** Next Statement *** Add 3/14/25
      if (!EndsWith("\n\r"))
      {
        Line();
      }
      Line("END");
      Line("//");
      Line("DELIMITER ;");
      var retProc = ToString();
      return retProc;
    }

    // Drops the constraint by provided name.
    internal string DropConstraint(string tableName
      , string objectName)
    {
      var b = new TextBuilder(128);
      b.Line($"ALTER TABLE `{tableName}`");
      b.Text($"  DROP CONSTRAINT IF EXISTS `{objectName}`;");
      var retValue = b.ToString();
      return retValue;
    }

    // Get column name and type.
    internal string NameAndType(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(64);

      // Column Name
      b.Text($"  {BeginDelimiter}");
      b.Text($"{dataColumn.Name}");
      b.Text($"{EndDelimiter}");

      // Type Name
      var typeName = dataColumn.TypeName.ToLower();
      if (typeName.StartsWith("n"))
      {
        typeName = typeName.Substring(1);
      }
      b.Text($" {typeName}");

      var retString = b.ToString();
      return retString;
    }

    // Renames a table. Removes old keys and creates new keys.
    internal string RenameTableSQL(long tableID, long siteID, DataKeys dataKeys)
    {
      var b = new TextBuilder(512);
      b.Line("/*");
      b.Text("/* Drop foreign keys and constraints. */");

      // Drop referencing foreign keys.
      var foreignKeys = dataKeys.FindAll(x => x.TargetTableName == TableName
        && x.KeyType == (short)ObjectType.Foreign);
      foreach (DataKey dataKey in foreignKeys)
      {
        var text = DropConstraint(dataKey.DataTableName, dataKey.Name);
        b.Line();
        b.Line(text);
      }

      // Drop constraints and foreign keys.
      var otherKeys = dataKeys.FindAll(x => x.DataTableID == tableID
        && x.DataTableSiteID == siteID
        && x.KeyType != (short)ObjectType.Primary);
      foreach (DataKey dataKey in otherKeys)
      {
        var text = DropConstraint(TableName, dataKey.Name);
        b.Line();
        b.Line(text);
      }

      b.Line();
      b.Line($"RENAME TABLE `{TableName}` TO `{TableName}Backup`;");
      b.Line($"RENAME TABLE `New{TableName}` TO `{TableName}`;");

      b.Line();
      b.Text("/* Add constraints and foreign keys. */");
      foreach (DataKey dataKey in otherKeys)
      {
        string text;
        switch ((ObjectType)dataKey.KeyType)
        {
          case ObjectType.Unique:
            var columnList = dataKey.SourceColumnName;
            text = AddUniqueKey(TableName, dataKey.Name, columnList);
            b.Line();
            b.Line(text);
            break;

          case ObjectType.Foreign:
            text = AddForeignKey(TableName, dataKey.Name
              , dataKey.SourceColumnName, dataKey.TargetTableName
              , dataKey.TargetColumnName);
            b.Line();
            b.Line(text);
            break;
        }
      }

      // Add referencing foreign keys.
      foreach (DataKey dataKey in foreignKeys)
      {
        var text = AddForeignKey(dataKey.DataTableName, dataKey.Name
          , dataKey.SourceColumnName, dataKey.TargetTableName
          , dataKey.TargetColumnName);
        b.Line();
        b.Line(text);
        break;
      }
      b.Line("*/");
      var retValue = b.ToString();
      return retValue;
    }

    // Adds the table begin code.
    internal string TableBegin()
    {
      var b = new TextBuilder(128);
      b.Line();
      b.Text($"CREATE TABLE IF NOT EXISTS ");
      if (NetString.HasValue(DBName))
      {
        b.Text($"{BeginDelimiter}");
        b.Text($"{DBName}");
        b.Text($"{EndDelimiter}.");
      }
      b.Text($"{BeginDelimiter}");
      b.Text($"{TableName}");
      b.Text($"{EndDelimiter} (");

      HasColumns = false;
      string retString = b.ToString();
      return retString;
    }

    // Adds a table column definition.
    internal string TableColumn(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(512);
      b.Text(ItemEnd(HasColumns));
      b.Text(NameAndType(dataColumn));

      var typeName = dataColumn.TypeName.Trim().ToLower();
      if (IsCharType(typeName))
      {
        b.Text($"({dataColumn.MaxLength})");

        if (!NetString.HasValue(dataColumn.DefaultValue))
        {
          if (!dataColumn.AllowNull)
          {
            b.Text(" NOT NULL");
          }
          else
          {
            dataColumn.DefaultValue = "NULL";
          }
        }
      }

      if (!IsCharType(typeName))
      {
        if (!dataColumn.AllowNull)
        {
          b.Text(" NOT");
        }
        b.Text(" NULL");
      }

      if (dataColumn.DefaultValue != null)
      {
        b.Text($" DEFAULT {dataColumn.DefaultValue}");
      }

      HasColumns = true;
      var retString = b.ToString();
      return retString;
    }

    // Creates the Identity column.
    internal string TableIdentity(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(64);
      b.Text(ItemEnd(HasColumns));
      b.Text(NameAndType(dataColumn));
      b.Text($" NOT NULL AUTO_INCREMENT");

      HasColumns = true;
      var retString = b.ToString();
      return retString;
    }

    // Checks if the type name is a char type.
    private bool IsCharType(string typeName)
    {
      bool retValue = false;

      if ("char" == typeName
        || "varchar" == typeName
        || "nchar" == typeName
        || "nvarchar" == typeName)
      {
        retValue = true;
      }
      return retValue;
    }

    // Adds a comma and new line.
    private string ItemEnd(bool hasValue)
    {
      string retValue = null;

      if (hasValue)
      {
        retValue = $",\r\n";
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the Add data Procedure Name.
    internal string AddProcName { get; set; }

    // The beginning identifier delimiter.
    internal string BeginDelimiter { get; set; }

    // Gets or sets the Create Table Procedure Name.
    internal string CreateProcName { get; set; }

    // Gets or sets the Database Name.
    internal string DBName { get; set; }

    // The ending identifier delimiter.
    internal string EndDelimiter { get; set; }

    // Gets or sets the Primary Key Name.
    internal string PKName { get; set; }

    // Gets or sets the Table Name.
    internal string TableName { get; set; }

    // Gets or sets the Unique Key Name.
    internal string UQName { get; set; }

    // Gets or sets an indicator if Create Table already has defined columns.
    private bool HasColumns { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent object reference.
    private DataUtilityList ParentObject { get; set; }
    #endregion

    #region TextBuilder Properties

    // Gets or sets the delimiter.
    internal string Delimiter
    {
      get { return Builder.Delimiter; }
      set { Builder.Delimiter = value; }
    }

    // Gets or sets the indent character count.
    internal int IndentCharCount
    {
      get { return Builder.IndentCharCount; }
      set { Builder.IndentCharCount = value; }
    }

    // Gets or sets the indent count.
    internal int IndentCount
    {
      get { return Builder.IndentCount; }
      set { Builder.IndentCount = value; }
    }

    // Gets or sets the first item indicator.
    internal bool IsFirst
    {
      get { return Builder.IsFirst; }
      set { Builder.IsFirst = value; }
    }

    // Gets or sets the TextBuilder object.
    private TextBuilder Builder { get; set; }
    #endregion
  }
}
