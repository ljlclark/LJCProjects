// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MySQLBuilder.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Data;
using System.Xml.Linq;

namespace LJCDataUtility
{
  /// <summary>Provides MySQL SQL code.</summary>
  internal class MySQLBuilder
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="parentList">The parent form.</param>
    /// <param name="dbName">The database name.</param>
    /// <param name="tableName">The table name.</param>
    internal MySQLBuilder(DataUtilityList parentList, string dbName = null
      , string tableName = null)
    {
      ParentList = parentList;
      Managers = ParentList.Managers;

      Reset(dbName, tableName);
    }

    // Resets the text values.
    /// <summary>
    /// Resets the text values.
    /// </summary>
    /// <param name="dbName">The database name.</param>
    /// <param name="tableName">The table name.</param>
    internal void Reset(string dbName = null, string tableName = null)
    {
      if (NetString.HasValue(dbName))
      {
        DBName = dbName;
      }

      if (NetString.HasValue(tableName))
      {
        TableName = tableName;
        PKName = $"pk_{TableName}";
        UQName = $"uq_{TableName}";
        CreateProcName = $"sp_{TableName}";
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
    /// <summary>
    /// Checks if the builder text ends with a supplied value.
    /// </summary>
    /// <param name="value">The ending value.</param>
    /// <returns>
    /// true if the internal builder ends with the value; otherwise false.
    /// </returns>
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
    /// <summary>
    /// Adds a line to the builder.
    /// </summary>
    /// <param name="text">The optional text value.</param>
    internal void Line(string text = null)
    {
      Builder.Line(text);
    }

    // Adds text to the builder.
    /// <summary>
    /// Adds text to the builder.
    /// </summary>
    /// <param name="text">The text value.</param>
    internal void Text(string text)
    {
      Builder.Text(text);
    }
    #endregion

    #region Procedure Methods

    /// <summary>Adds the Procedure begin code.</summary>
    public string Begin(string procName)
    {
      var b = new TextBuilder(512);
      b.Line("-- Copyright(c) Lester J. Clark and Contributors.");
      b.Line("-- Licensed under the MIT License.");
      b.Line($"-- {procName}.sql");
      b.Line("DELIMITER //");
      b.Line($"CREATE PROCEDURE `{procName}` (");
      string retString = b.ToString();
      return retString;
    }
    #endregion

    #region Create Table Methods

    /// <summary>Adds a foreign key.</summary>
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

    /// <summary>Adds a primary key.</summary>
    internal string AddPrimaryKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = NetString.DelimitValues(columnList, "`", "`");
      var b = new TextBuilder(128);
      b.Line($" ALTER TABLE `{tableName}`");
      b.Text($"  ADD CONSTRAINT `{objectName}`");
      b.Line($" PRIMARY KEY ({columnNames});");
      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Adds a unique key.</summary>
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
    /// <summary>Creates the Create Table SQL.</summary>
    /// <param name="dataColumns">The DataColumns collection.</param>
    /// <returns>The Create Table SQL.</returns>
    /// <remarks>
    /// Methods that start with "Create" use the internal builder.
    /// Other methods return a value.
    /// </remarks>
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

      //var keyValues = PrimaryKeyValues();
      //if (NetString.HasValue(keyValues))
      //{
      //  var keyNames = NetString.DelimitValues(keyValues, "`", "`");
      //  Text(ItemEnd(HasColumns));
      //  Text($"  PRIMARY KEY ({keyNames})");
      //}

      //keyValues = UniqueKeyValues();
      //if (NetString.HasValue(keyValues))
      //{
      //  var keyNames = NetString.DelimitValues(keyValues, "`", "`");
      //  Text(ItemEnd(HasColumns));
      //  Text($"  UNIQUE INDEX `{UQName}` ({keyNames})");
      //}

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

    /// <summary>Complete Create Table procedure.</summary>
    internal string CreateTableProc(DataColumns dataColumns)
    {
      Text(Begin(CreateProcName));
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

      Line("END");
      Line("//");
      Line("DELIMITER;");
      var retProc = ToString();
      return retProc;
    }

    /// <summary>Drops the constraint by provided name.</summary>
    public string DropConstraint(string tableName
      , string objectName)
    {
      var b = new TextBuilder(128);
      b.Line($" ALTER TABLE `{tableName}`");
      b.Line($"  DROP CONSTRAINT `{objectName}`;");
      var retValue = b.ToString();
      return retValue;
    }

    // Adds a comma and new line.
    /// <summary>
    /// Adds a comma and new line.
    /// </summary>
    /// <param name="hasValue">Indicates if the item already has a value.</param>
    /// <returns>The item end value.</returns>
    internal string ItemEnd(bool hasValue)
    {
      string retValue = null;

      if (hasValue)
      {
        retValue = $",\r\n";
      }
      return retValue;
    }

    // Get column name and type.
    /// <summary>
    /// Get column name and type.
    /// </summary>
    /// <param name="dataColumn">The DataUtilColumn object.</param>
    /// <returns>The name and type string.</returns>
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

    /// <summary>Adds the table begin code.</summary>
    internal string TableBegin()
    {
      var b = new TextBuilder(128);
      b.Line();
      b.Text($"CREATE TABLE IF NOT EXISTS ");
      b.Text($"{BeginDelimiter}");
      b.Text($"{TableName}");
      b.Text($"{EndDelimiter} (");

      HasColumns = false;
      string retString = b.ToString();
      return retString;
    }

    // Adds a table column definition.
    /// <summary>
    /// Adds a table column definition.
    /// </summary>
    /// <param name="dataColumn">The DataUtilColumn object.</param>
    /// <returns>The table column definition string.</returns>
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
    /// <summary>
    /// Creates the Identity column.
    /// </summary>
    /// <param name="dataColumn">The DataUtilColumn object.</param>
    /// <returns>The table identity column definition string.</returns>
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
    #endregion

    #region Data Methods

    /// <summary>Retrieve the Primary key list.</summary>
    /// <returns>The primary key values text.</returns>
    internal string PrimaryKeyValues()
    {
      string retList = null;

      var parentTableID = ParentList.DataTableID();
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

      var parentTableID = ParentList.DataTableID();
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

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentList { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    /// <summary>Gets or sets the Primary Key Name.</summary>
    public string PKName { get; set; }

    /// <summary>Gets or sets the Table Name.</summary>
    internal string TableName { get; set; }

    /// <summary>Gets or sets the Unique Key Name.</summary>
    internal string UQName { get; set; }

    // Gets or sets the StringBuilder object.
    private TextBuilder Builder { get; set; }

    // Gets or sets an indicator if Create Table already has defined columns.
    private bool HasColumns { get; set; }
    #endregion
  }
}
