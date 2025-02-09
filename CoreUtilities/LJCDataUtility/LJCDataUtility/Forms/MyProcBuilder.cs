// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MyProcBuilder.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Xml.Linq;

namespace LJCDataUtility
{
  /// <summary>Provides MySQL procedure SQL code.</summary>
  internal class MyProcBuilder
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/CMyProcBuilder/*' file='Doc/MyProcBuilder.xml'/>
    internal MyProcBuilder(DataUtilityList parentObject, string dbName = null
      , string tableName = null)
    {
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
      Reset(dbName, tableName);
    }

    // Resets the text values.
    /// <include path='items/Reset/*' file='Doc/MyProcBuilder.xml'/>
    internal void Reset(string dbName = null, string tableName = null)
    {
      if (NetString.HasValue(dbName))
      {
        DBName = dbName;
      }

      if (NetString.HasValue(tableName))
      {
        TableName = tableName;
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

    /// <summary>Returns the builder string.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Builder Methods

    /// <summary>Clears the Builder text.</summary>
    internal void ClearText()
    {
      Builder = new TextBuilder(512);
    }

    // Checks if the builder text ends with a supplied value.
    /// <include path='items/EndsWith/*' file='Doc/MyProcBuilder.xml'/>
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
    /// <include path='items/Line/*' file='Doc/MyProcBuilder.xml'/>
    internal void Line(string text = null)
    {
      Builder.Line(text);
    }

    // Adds text to the builder.
    /// <include path='items/Text/*' file='Doc/MyProcBuilder.xml'/>
    internal void Text(string text)
    {
      Builder.Text(text);
    }
    #endregion

    #region Procedure Methods

    // Adds the Procedure begin code.
    /// <include path='items/Begin/*' file='Doc/MyProcBuilder.xml'/>
    public string Begin(string procedureName)
    {
      var b = new TextBuilder(512);
      b.Line("-- Copyright(c) Lester J. Clark and Contributors.");
      b.Line("-- Licensed under the MIT License.");
      b.Line($"-- {procedureName}.sql");
      b.Line("DELIMITER //");
      b.Line($"CREATE PROCEDURE `{procedureName}` (");
      string retString = b.ToString();
      Text(retString);
      return retString;
    }
    #endregion

    #region Create Table Methods

    // Adds a foreign key.
    /// <include path='items/AddForeignKey/*' file='Doc/MyProcBuilder.xml'/>
    internal string AddForeignKey(string tableName
      , string objectName, string sourceColumnList
      , string targetTableName, string targetColumnList)
    {
      var sourceNames = NetString.DelimitValues(sourceColumnList, "`", "`");
      var targetNames = NetString.DelimitValues(targetColumnList, "`", "`");
      var b = new TextBuilder(128);
      b.Line($" ALTER TABLE `{tableName}`");
      b.Line($"  ADD CONSTRAINT `{objectName}`");
      b.Line($"   FOREIGN KEY ({sourceNames})");
      b.Text($"   REFERENCES `{targetTableName}`");
      b.Line($" ({targetNames})");
      var retValue = b.ToString();
      return retValue;
    }

    // Adds a primary key.
    /// <include path='items/AddPrimaryKey/*' file='Doc/MyProcBuilder.xml'/>
    internal string AddPrimaryKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = NetString.DelimitValues(columnList, "`", "`");
      var b = new TextBuilder(128);
      b.Line($" ALTER TABLE `{tableName}`");
      b.Line($"  ADD CONSTRAINT `{objectName}`");
      b.Line($"  PRIMARY KEY ({columnNames});");
      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Adds a unique key.</summary>
    /// <include path='items/AddUniqueKey/*' file='Doc/MyProcBuilder.xml'/>
    internal string AddUniqueKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = NetString.DelimitValues(columnList, "`", "`");
      var b = new TextBuilder(128);
      b.Line($" ALTER TABLE `{tableName}`");
      b.Line($"  ADD CONSTRAINT `{objectName}`");
      b.Line($"  UNIQUE ({columnNames});");
      var retValue = b.ToString();
      return retValue;
    }

    // Creates the Create Table SQL.
    /// <include path='items/CreateTable/*' file='Doc/MyProcBuilder.xml'/>
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
    /// <include path='items/CreateTableProc/*' file='Doc/MyProcBuilder.xml'/>
    internal string CreateTableProc(DataColumns dataColumns)
    {
      Begin(CreateProcName);
      //Line("  IN parm varchar(60)");
      Line(")");
      Text("BEGIN");

      CreateTable(dataColumns);

      var keyValues = PrimaryKeyValues();
      if (NetString.HasValue(keyValues))
      {
        Line();
        var text = AddPrimaryKey(TableName, PKName, keyValues);
        Text(text);
      }

      keyValues = UniqueKeyValues();
      if (NetString.HasValue(keyValues))
      {
        Line();
        var text = AddUniqueKey(TableName, UQName, keyValues);
        Text(text);
      }

      Line("END//");
      Line("DELIMITER ;");
      var retProc = ToString();
      return retProc;
    }

    // Drops the constraint by provided name.
    /// <include path='items/DropConstraint/*' file='Doc/MyProcBuilder.xml'/>
    internal string DropConstraint(string tableName
      , string objectName)
    {
      var b = new TextBuilder(128);
      b.Line($" ALTER TABLE `{tableName}`");
      b.Line($"  DROP CONSTRAINT `{objectName}`;");
      var retValue = b.ToString();
      return retValue;
    }

    // Get column name and type.
    /// <include path='items/NameAndType/*' file='Doc/MyProcBuilder.xml'/>
    internal string NameAndType(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(64);

      // Column Name
      b.Text($"  {BeginDelimiter}");
      b.Text($"{dataColumn.Name}");
      b.Text($"{EndDelimiter}");

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
    /// <include path='items/RenameTableSQL/*' file='Doc/ProcBuilder.xml'/>
    internal string RenameTableSQL(long tableID, DataKeys dataKeys)
    {
      var b = new TextBuilder(512);
      //b.Line($"USE [{DBName}]");
      //b.Line();
      b.Line("/*");
      b.Text("/* Remove foreign keys and other constraints. */");

      // Remove referencing foreign keys.
      foreach (DataKey dataKey in dataKeys)
      {
        if (dataKey.TargetTableName != TableName)
        {
          continue;
        }

        var objectType = (ObjectType)dataKey.KeyType;
        if (ObjectType.Foreign == objectType)
        {
          var text = DropConstraint(dataKey.DataTableName, dataKey.Name);
          b.Line(text);
        }
      }

      // Remove reference foreign keys and other constraints.
      foreach (DataKey dataKey in dataKeys)
      {
        if (dataKey.DataTableID != tableID)
        {
          continue;
        }

        var text = DropConstraint(TableName, dataKey.Name);
        b.Line(text);
      }

      b.Line();
      b.Line($"RENAME TABLE {TableName} TO {TableName}Backup");
      b.Line($"RENAME TABLE New{TableName} TO {TableName}");
      b.Line();

      b.Text("/* Add constraints and foreign keys. */");
      foreach (DataKey dataKey in dataKeys)
      {
        if (dataKey.DataTableID != tableID)
        {
          continue;
        }
        string text;

        var objectType = (ObjectType)dataKey.KeyType;
        switch (objectType)
        {
          case ObjectType.Primary:
            text = AddPrimaryKey(TableName, dataKey.Name
              , dataKey.SourceColumnName);
            b.Line(text);
            break;

          case ObjectType.Unique:
            var columnList = dataKey.SourceColumnName;
            text = AddUniqueKey(TableName, dataKey.Name, columnList);
            b.Line(text);
            break;

          case ObjectType.Foreign:
            text = AddForeignKey(TableName, dataKey.Name
              , dataKey.SourceColumnName, dataKey.TargetTableName
              , dataKey.TargetColumnName);
            b.Line(text);
            break;
        }
      }

      // Create referencing foreign keys.
      foreach (DataKey dataKey in dataKeys)
      {
        if (dataKey.TargetTableName != TableName)
        {
          continue;
        }
        var objectType = (ObjectType)dataKey.KeyType;
        switch (objectType)
        {
          case ObjectType.Foreign:
            var text = AddForeignKey(dataKey.DataTableName, dataKey.Name
              , dataKey.SourceColumnName, dataKey.TargetTableName
              , dataKey.TargetColumnName);
            b.Line(text);
            break;
        }
      }
      b.Line("*/");

      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Adds the table begin code.</summary>
    /// <returns>The table begin SQL text.</returns>
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
    /// <include path='items/TableColumn/*' file='Doc/MyProcBuilder.xml'/>
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
    /// <include path='items/TableIdentity/*' file='Doc/MyProcBuilder.xml'/>
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

    #region Data Methods

    /// <summary>Retrieve the Primary key list.</summary>
    /// <returns>The primary key values text.</returns>
    internal string PrimaryKeyValues()
    {
      string retList = null;

      var parentTableID = ParentObject.DataTableID();
      var keyManager = Managers.DataKeyManager;
      var dataKey = keyManager.RetrieveWithType(parentTableID
        , (short)KeyType.Primary);
      if (dataKey != null)
      {
        retList = dataKey.SourceColumnName;
      }
      return retList;
    }

    /// <summary>Retrieve the Unique key list.</summary>
    /// <returns>The unique key values text.</returns>
    internal string UniqueKeyValues()
    {
      string retList = null;

      var parentTableID = ParentObject.DataTableID();
      var keyManager = Managers.DataKeyManager;
      var dataKey = keyManager.RetrieveWithType(parentTableID
        , (short)KeyType.Unique);
      if (dataKey != null)
      {
        retList = dataKey.SourceColumnName;
      }
      return retList;
    }
    #endregion

    #region Properties

    /// <summary>The beginning identifier delimiter.</summary>
    internal string BeginDelimiter { get; set; }

    /// <summary>Gets or sets the Create Table Procedure Name.</summary>
    internal string CreateProcName { get; set; }

    /// <summary>Gets or sets the Database Name.</summary>
    internal string DBName { get; set; }

    /// <summary>The ending identifier delimiter.</summary>
    internal string EndDelimiter { get; set; }

    /// <summary>Gets or sets the Primary Key Name.</summary>
    internal string PKName { get; set; }

    /// <summary>Gets or sets the Table Name.</summary>
    internal string TableName { get; set; }

    /// <summary>Gets or sets the Unique Key Name.</summary>
    internal string UQName { get; set; }

    // Gets or sets the StringBuilder object.
    private TextBuilder Builder { get; set; }

    // Gets or sets an indicator if Create Table already has defined columns.
    private bool HasColumns { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent object reference.
    private DataUtilityList ParentObject { get; set; }
    #endregion
  }
}
