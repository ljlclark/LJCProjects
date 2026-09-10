// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MyProcBuilder.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using static LJCDataUtility5.ProcBuilder;

namespace LJCDataUtility5
{
  //Provides methods to create MySQL procedure SQL code.
  internal class MyProcBuilder
  {
    #region Properties

    // Gets or sets the Add data Procedure Name.
    internal string AddProcName { get; set; } = null!;

    // The beginning identifier delimiter.
    internal string BeginDelimiter { get; set; } = null!;

    // Gets or sets the Create Table Procedure Name.
    internal string CreateProcName { get; set; } = null!;

    // Gets or sets the Database Name.
    internal string DBName { get; set; } = null!;

    // The ending identifier delimiter.
    internal string EndDelimiter { get; set; } = null!;

    // Gets or sets the Primary Key Name.
    internal string PKName { get; set; } = null!;

    // Gets or sets the Table Name.
    internal string TableName { get; set; } = null!;

    // Gets or sets the Unique Key Name.
    internal string UQName { get; set; } = null!;

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
      set { Builder.AddIndent(value); }
    }

    // Gets or sets the first item indicator.
    internal bool IsFirst
    {
      get { return Builder.IsFirst; }
      set { Builder.IsFirst = value; }
    }

    // Gets or sets the TextBuilder object.
    private LJCTextBuilder Builder { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    internal MyProcBuilder(DataUtilityList parentObject, string? dbName = null
      , string? tableName = null)
    {
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
      Reset(dbName, tableName);
    }

    // Resets the text values.
    internal void Reset(string? dbName = null, string? tableName = null)
    {
      if (LJC.HasText(dbName))
      {
        DBName = dbName;
      }

      if (LJC.HasText(tableName))
      {
        TableName = tableName;
        AddProcName = $"mysp_{TableName}Add";
        PKName = $"mypk_{TableName}";
        UQName = $"myuq_{TableName}";
        CreateProcName = $"mysp_{TableName}";
      }

      BeginDelimiter = "`";
      EndDelimiter = "`";
      Builder = new LJCTextBuilder();
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

    // Adds text to the builder, no indent or wrap.
    internal void Add(string text)
    {
      Builder.AddText(text);
    }

    //Clears the Builder text.
    internal void ClearText()
    {
      Builder = new LJCTextBuilder();
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

    // Adds a delimiter if not the first list item
    internal void Item(string text)
    {
      Builder.Item(text);
    }

    // Adds a line to the builder with indent and wrap.
    internal void Line(string? text = null)
    {
      Builder.Line(text);
    }

    // Adds text to the builder with indent and wrap.
    internal void Text(string text)
    {
      Builder.Text(text);
    }
    #endregion

    #region Procedure Methods

    // Adds the Procedure begin code.
    internal string Begin(string procedureName)
    {
      var tb = new LJCTextBuilder();
      tb.Line("-- Copyright(c) Lester J. Clark and Contributors.");
      tb.Line("-- Licensed under the MIT License.");
      tb.Line($"-- {procedureName}.sql");
      var qualifiedName = QualifiedName(DBName, procedureName);
      tb.Line("DELIMITER $$");
      tb.Line($"DROP PROCEDURE IF EXISTS {qualifiedName};$$");
      tb.Line($"CREATE PROCEDURE {qualifiedName} (");
      string retString = tb.ToString();

      Add(retString);
      return retString;
    }

    // Creates the insert Columns list.
    internal string ColumnsList(DataColumns dataColumns
      , bool includeParens = true, bool useNewNames = false
      , bool includeID = false, int indentCount = 0)
    {
      var tb = new LJCTextBuilder();
      tb.AddIndent(indentCount);
      var value = "    ";
      if (includeParens)
      {
        value += "(";
      }
      tb.Text(value);

      if (LJC.HasListItems(dataColumns))
      {
        tb.IsFirst = true;
        foreach (DataUtilColumn dataColumn in dataColumns)
        {
          if (!includeID
            && "ID" == dataColumn.Name)
          {
            continue;
          }

          var nameValue = dataColumn.Name;
          if (useNewNames
            && LJC.HasText(dataColumn.NewName))
          {
            nameValue = dataColumn.NewName;
          }
          tb.Item(nameValue);
        }

        if (includeParens)
        {
          tb.Text(")");
        }
      }
      var retList = tb.ToString();
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

      var tb = new LJCTextBuilder();
      tb.Line($"(SET {varRefName} = (SELECT {parentIDColumnName}");
      tb.Line($" FROM {parentTableName}");
      tb.Line($" WHERE {parentFindColumnName} = {parmFindName});");
      var retIf = tb.ToString();
      return retIf;
    }

    // Creates the Parameters.
    internal string Parameters(DataColumns dataColumns, bool isFirst = true
      , int indentCount = 0)
    {
      var tb = new LJCTextBuilder();
      {
        IndentCount = indentCount;
      }
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (!dataColumn.Name.EndsWith("ID"))
        {
          if (!isFirst)
          {
            tb.Line(",");
          }
          isFirst = false;
          var declaration = SQLDeclaration(dataColumn);
          tb.Text($"  {declaration}");
        }
      }
      var retParams = tb.ToString();
      return retParams;
    }

    // Creates the qualified name.
    internal string QualifiedName(string dbName, string name)
    {
      string? retValue = null;

      if (LJC.HasText(dbName))
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
      //retName += columnName.Substring(1);
      retName += columnName[1..];
      return retName;
    }

    // Creates the Values list.
    internal string ValuesList(DataColumns dataColumns
      , List<string>? varRefNames = null, int indentCount = 0)
    {
      var tb = new LJCTextBuilder();
      tb.AddIndent(indentCount);
      tb.Text("    VALUES(");

      // Use the variable references instead of value.
      if (LJC.HasListItems(varRefNames))
      {
        tb.IsFirst = true;
        foreach (string varRefName in varRefNames)
        {
          tb.Item(varRefName);
        }
      }

      if (LJC.HasListItems(dataColumns))
      {
        tb.IsFirst = true;
        foreach (DataUtilColumn dataColumn in dataColumns)
        {
          if (dataColumn.Name.EndsWith("ID"))
          {
            continue;
          }

          var nameValue = SQLVarName(dataColumn.Name);
          nameValue = $"`{nameValue}`";
          tb.Item(nameValue);
        }
      }

      tb.Text(");");
      var retList = tb.ToString();
      return retList;
    }
    #endregion

    #region Create Table Methods

    // Adds a foreign key.
    private string AddForeignKey(string tableName
      , string objectName, string sourceColumnList
      , string targetTableName, string targetColumnList)
    {
      var sourceNames = LJCNetString.DelimitValues(sourceColumnList, "`", "`");
      var targetNames = LJCNetString.DelimitValues(targetColumnList, "`", "`");
      var tb = new LJCTextBuilder();
      tb.Line($"ALTER TABLE `{tableName}`");
      tb.Line($"  ADD CONSTRAINT `{objectName}`");
      tb.Line($"   FOREIGN KEY ({sourceNames})");
      tb.Text($"   REFERENCES `{targetTableName}`");
      tb.Text($" ({targetNames});");
      var retValue = tb.ToString();
      return retValue;
    }

    // Adds a primary key.
    private string AddPrimaryKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = LJCNetString.DelimitValues(columnList, "`", "`");
      var tb = new LJCTextBuilder();
      tb.Line($"ALTER TABLE `{tableName}`");
      tb.Line($"  ADD CONSTRAINT `{objectName}`");
      tb.Text($"  PRIMARY KEY ({columnNames});");
      var retValue = tb.ToString();
      return retValue;
    }

    // Adds a unique key.
    private string AddUniqueKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = LJCNetString.DelimitValues(columnList, "`", "`");
      var tb = new LJCTextBuilder();
      tb.Line($"ALTER TABLE `{tableName}`");
      tb.Line($"  ADD CONSTRAINT `{objectName}`");
      tb.Text($"  UNIQUE ({columnNames});");
      var retValue = tb.ToString();
      return retValue;
    }

    // Creates the Create Table SQL.
    private string CreateTable(DataColumns dataColumns)
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
    private string CreateTableProc(DataColumns dataColumns)
    {
      Begin(CreateProcName);
      //Line("  IN parmName varchar(60)");
      Line(")");
      Text("BEGIN");

      CreateTable(dataColumns);

      //var keyValues = ParentObject.UniqueKeyColumns();
      var KeyGridCode = ParentObject.KeyGridCode;
      var keyValues = KeyGridCode.UniqueKeyColumns();
      if (LJC.HasText(keyValues))
      {
        Line();
        var text = AddUniqueKey(TableName, UQName, keyValues);
        Text(text);
      }

      if (!EndsWith("\r\n"))
      {
        Line();
      }
      Line("END$$");
      Line("DELIMITER ;");
      var retProc = ToString();
      return retProc;
    }

    // Drops the constraint by provided name.
    private string DropConstraint(string tableName
      , string objectName)
    {
      var tb = new LJCTextBuilder();
      tb.Line($"ALTER TABLE `{tableName}`");
      tb.Text($"  DROP CONSTRAINT IF EXISTS `{objectName}`;");
      var retValue = tb.ToString();
      return retValue;
    }

    // Get column name and type.
    private string NameAndType(DataUtilColumn dataColumn)
    {
      var tb = new LJCTextBuilder();

      // Column Name
      tb.Text($"  {BeginDelimiter}");
      tb.Text($"{dataColumn.Name}");
      tb.Text($"{EndDelimiter}");

      // Type Name
      var typeName = dataColumn.TypeName.ToLower();
      if (typeName.StartsWith('n'))
      {
        //typeName = typeName.Substring(1);
        typeName = typeName[1..];
      }
      tb.Text($" {typeName}");

      var retString = tb.ToString();
      return retString;
    }

    // Renames a table. Removes old keys and creates new keys.
    private string RenameTableSQL(long tableID, short dbID)
    {
      var keyManager = Managers.DataKeyManager;
      var tb = new LJCTextBuilder();
      tb.Line("/*");
      tb.Text("/* Drop foreign keys and constraints. */");

      // Drop referencing foreign keys.
      var foreignKeys = keyManager.LoadWithForeign(TableName);
      if (LJC.HasListItems(foreignKeys))
      {
        foreach (DataKey dataKey in foreignKeys)
        {
          if (LJC.HasText(dataKey.DataTableName))
          {
            var text = DropConstraint(dataKey.DataTableName, dataKey.Name);
            tb.Line();
            tb.Line(text);
          }
        }
      }

      // Drop constraints and foreign keys.
      var otherKeys = keyManager.LoadWithParent(dbID, tableID);
      if (LJC.HasListItems(otherKeys))
      {
        foreach (DataKey dataKey in otherKeys)
        {
          if (dataKey.KeyType != (short)ObjectType.Primary)
          {
            var text = DropConstraint(TableName, dataKey.Name);
            tb.Line();
            tb.Line(text);
          }
        }
      }

      tb.Line();
      tb.Line($"RENAME TABLE `{TableName}` TO `{TableName}Backup`;");
      tb.Line($"RENAME TABLE `New{TableName}` TO `{TableName}`;");

      tb.Line();
      tb.Text("/* Add constraints and foreign keys. */");

      if (LJC.HasListItems(otherKeys))
      {
        foreach (DataKey dataKey in otherKeys)
        {
          string text;
          switch ((ObjectType)dataKey.KeyType)
          {
            case ObjectType.Unique:
              if (LJC.HasText(dataKey.SourceColumnName))
              {
                var columnList = dataKey.SourceColumnName;
                text = AddUniqueKey(TableName, dataKey.Name, columnList);
                tb.Line();
                tb.Line(text);
              }
              break;

            case ObjectType.Foreign:
              if (LJC.HasText(dataKey.SourceColumnName)
                && LJC.HasText(dataKey.TargetTableName)
                && LJC.HasText(dataKey.TargetColumnName))
              {
                text = AddForeignKey(TableName, dataKey.Name
                  , dataKey.SourceColumnName, dataKey.TargetTableName
                  , dataKey.TargetColumnName);
                tb.Line();
                tb.Line(text);
              }
              break;
          }
        }
      }

      // Add referencing foreign keys.
      if (LJC.HasListItems(foreignKeys))
      {
        foreach (DataKey dataKey in foreignKeys)
        {
          if (LJC.HasText(dataKey.DataTableName)
            && LJC.HasText(dataKey.SourceColumnName)
            && LJC.HasText(dataKey.TargetTableName)
            && LJC.HasText(dataKey.TargetColumnName))
          {
            var text = AddForeignKey(dataKey.DataTableName, dataKey.Name
            , dataKey.SourceColumnName, dataKey.TargetTableName
            , dataKey.TargetColumnName);
            tb.Line();
            tb.Line(text);
          }
          break;
        }
      }
      tb.Line("*/");
      var retValue = tb.ToString();
      return retValue;
    }

    // Adds the table begin code.
    private string TableBegin()
    {
      var tb = new LJCTextBuilder();
      tb.Line();
      tb.Text($"CREATE TABLE IF NOT EXISTS ");
      if (LJC.HasText(DBName))
      {
        tb.Text($"{BeginDelimiter}");
        tb.Text($"{DBName}");
        tb.Text($"{EndDelimiter}.");
      }
      tb.Text($"{BeginDelimiter}");
      tb.Text($"{TableName}");
      tb.Text($"{EndDelimiter} (");

      HasColumns = false;
      string retString = tb.ToString();
      return retString;
    }

    // Adds a table column definition.
    private string TableColumn(DataUtilColumn dataColumn)
    {
      var tb = new LJCTextBuilder();
      var itemEnd = ItemEnd(HasColumns);
      if (LJC.HasText(itemEnd))
      {
        tb.Text(itemEnd);
      }
      tb.Text(NameAndType(dataColumn));

      var typeName = dataColumn.TypeName.Trim().ToLower();
      if (IsCharType(typeName))
      {
        tb.Text($"({dataColumn.MaxLength})");

        if (!LJC.HasText(dataColumn.DefaultValue))
        {
          if (!dataColumn.AllowNull)
          {
            tb.Text(" NOT NULL");
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
          tb.Text(" NOT");
        }
        tb.Text(" NULL");
      }

      if (dataColumn.DefaultValue != null)
      {
        tb.Text($" DEFAULT {dataColumn.DefaultValue}");
      }

      HasColumns = true;
      var retString = tb.ToString();
      return retString;
    }

    // Creates the Identity column.
    private string TableIdentity(DataUtilColumn dataColumn)
    {
      var tb = new LJCTextBuilder();
      var itemEnd = ItemEnd(HasColumns);
      if (LJC.HasText(itemEnd))
      {
        tb.Text(itemEnd);
      }
      tb.Text(NameAndType(dataColumn));
      tb.Text($" NOT NULL AUTO_INCREMENT");

      HasColumns = true;
      var retString = tb.ToString();
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
    private string? ItemEnd(bool hasValue)
    {
      string? retValue = null;

      if (hasValue)
      {
        retValue = $",\r\n";
      }
      return retValue;
    }
    #endregion
  }
}
