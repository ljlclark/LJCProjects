﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcBuilder.cs
using LJCDataUtilityDAL;
using LJCNetCommon;

namespace LJCDataUtility
{
  /// <summary>Provides procedure SQL code.</summary>
  internal class ProcBuilder
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CProcBuilder/*' file='Doc/ProcBuilder.xml'/>
    internal ProcBuilder(DataUtilityList parentObject, string dbName
      , string tableName = null)
    {
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
      Reset(dbName, tableName);
    }

    // Resets the text values.
    /// <include path='items/Reset/*' file='Doc/ProcBuilder.xml'/>
    internal void Reset(string dbName = null, string tableName = null)
    {
      if (NetString.HasValue(dbName))
      {
        DBName = dbName;
      }

      if (NetString.HasValue(tableName))
      {
        TableName = tableName;
        AddProcName = $"sp_{TableName}Add";
        CreateProcName = $"sp_{TableName}";
        ForeignKeyProcName = $"sp_{TableName}FK";
        ForeignKeyDropProcName = $"sp_{TableName}DropFK";
        PKName = $"pk_{TableName}";
        UQName = $"uq_{TableName}";
      }

      BeginDelimiter = "[";
      EndDelimiter = "]";
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
    /// <include path='items/EndsWith/*' file='Doc/ProcBuilder.xml'/>
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

    /// <summary>Adds a line to the builder.</summary>
    /// <param name="text">The optional text value.</param>
    internal void Line(string text = null)
    {
      Builder.Line(text);
    }

    /// <summary>Adds text to the builder.</summary>
    /// <param name="text">The text value.</param>
    internal void Text(string text)
    {
      Builder.Text(text);
    }
    #endregion

    #region Procedure Methods

    // Adds the Procedure begin code.
    /// <include path='items/Begin/*' file='Doc/ProcBuilder.xml'/>
    internal string Begin(string procedureName)
    {
      var b = new TextBuilder(512);
      b.Line("/* Copyright(c) Lester J. Clark and Contributors. */");
      b.Line("/* Licensed under the MIT License. */");
      b.Line($"/* {procedureName}.sql */");
      b.Line($"USE [{DBName}]");
      b.Line("GO");
      b.Line("SET ANSI_NULLS ON");
      b.Line("GO");
      b.Line("SET QUOTED_IDENTIFIER ON");
      b.Line("GO");
      b.Line("");
      b.Line($"IF OBJECT_ID('[dbo].[{procedureName}]', N'p')");
      b.Line(" IS NOT NULL");
      b.Line($"  DROP PROCEDURE [dbo].[{procedureName}];");
      b.Line("GO");
      b.Line($"CREATE PROCEDURE [dbo].[{procedureName}]");
      string retString = b.ToString();
      Text(retString);
      return retString;
    }

    /// <summary>Creates the Proc body code.</summary>
    internal string BodyBegin()
    {
      var b = new TextBuilder(64);
      b.Line("AS");
      b.Line("BEGIN");
      var retValue = b.ToString();
      Text(retValue);
      return retValue;
    }

    // Creates the insert Columns list.
    /// <include path='items/ColumnsList/*' file='Doc/ProcBuilder.xml'/>
    internal string ColumnsList(DataColumns dataColumns
      , bool includeParens = true, bool useNewNames = false
      , bool includeID = false)
    {
      var b = new TextBuilder(256);
      var value = "    ";
      if (includeParens)
      {
        value += "(";
      }
      b.Text(value);
      var lineLength = value.Length;

      var first = true;
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (!includeID
          //&& dataColumn.Name != "ID")
          && "ID" == dataColumn.Name)
        {
          continue;
        }

        // Calculate length before adding to insert newline.
        var nameValue = dataColumn.Name;
        if (useNewNames
          && NetString.HasValue(dataColumn.NewName))
        {
          nameValue = dataColumn.NewName;
        }
        lineLength += nameValue.Length;

        if (lineLength > 80)
        {
          var newLine = "\r\n     ";
          b.Text(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
          lineLength += nameValue.Length;
        }

        if (!first)
        {
          var firstValue = ", ";
          b.Text(firstValue);
          lineLength += firstValue.Length;
        }
        first = false;

        b.Text(nameValue);
      }

      if (includeParens)
      {
        b.Text(")");
      }
      var retList = b.ToString();
      return retList;
    }

    // Gets the Table row IF statement.
    /// <include path='items/IFItem/*' file='Doc/ProcBuilder.xml'/>
    internal string IFItem(string parentTableName
      , string parentIDColumnName, string parentFindColumnName
      , string parmFindName)
    {
      // Reference name "TableNameParentID".
      var varRefName
        = SQLVarName($"{parentTableName}{parentIDColumnName}");

      var b = new TextBuilder(128);
      b.Text($"DECLARE {varRefName} bigint = ");
      b.Line($"(SELECT {parentIDColumnName} FROM {parentTableName}");
      b.Line($" WHERE {parentFindColumnName} = {parmFindName});");
      var retIf = b.ToString();
      return retIf;
    }

    // Creates the Parameters.
    /// <include path='items/Parameters/*' file='Doc/ProcBuilder.xml'/>
    internal string Parameters(DataColumns dataColumns, bool isFirst = true)
    {
      var b = new TextBuilder(128);
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

    // Creates a SQL Declaration variable from a DataUtilityColumn.
    /// <include path='items/SQLDeclaration/*' file='Doc/ProcBuilder.xml'/>
    internal string SQLDeclaration(DataUtilColumn dataColumn)
    {
      var retValue = "";

      // @name nvarchar(60)
      retValue += SQLVarName(dataColumn.Name);
      retValue += $" {dataColumn.TypeName}";
      if (dataColumn.MaxLength > 0)
      {
        retValue += $"({dataColumn.MaxLength})";
      }
      return retValue;
    }

    // Creates a SQL variable name from a column name.
    /// <include path='items/SQLVarName/*' file='Doc/ProcBuilder.xml'/>
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
    /// <include path='items/ValuesList/*' file='Doc/ProcBuilder.xml'/>
    internal string ValuesList(DataColumns dataColumns
      , string varRefName = null)
    {
      var b = new TextBuilder(256);
      var value = "    VALUES(";
      b.Text(value);
      var lineLength = value.Length;

      if (NetString.HasValue(varRefName))
      {
        value = $"{varRefName}, ";
        b.Text(value);
        lineLength += varRefName.Length;
      }

      var first = true;
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (dataColumn.Name.EndsWith("ID"))
        {
          continue;
        }

        // Calculate length before adding value.
        var nameValue = SQLVarName(dataColumn.Name);
        lineLength += nameValue.Length;

        if (lineLength > 80)
        {
          var newLine = "\r\n     ";
          b.Text(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
          lineLength += nameValue.Length;
        }

        if (!first)
        {
          var firstValue = ", ";
          b.Text(firstValue);
          lineLength += firstValue.Length;
        }
        first = false;

        b.Text(nameValue);
      }

      b.Text(");");
      var retList = b.ToString();
      return retList;
    }
    #endregion

    #region Create Table Methods

    // Adds a foreign key.
    /// <include path='items/AddForeignKey/*' file='Doc/ProcBuilder.xml'/>
    internal string AddForeignKey(string tableName
      , string objectName, string sourceColumnList
      , string targetTableName, string targetColumnList)
    {
      var sourceNames = NetString.DelimitValues(sourceColumnList, "[", "]");
      var targetNames = NetString.DelimitValues(targetColumnList, "[", "]");
      var b = new TextBuilder(128);
      b.Line(Check(objectName, ObjectType.Foreign));
      b.Line($" ALTER TABLE [dbo].[{tableName}]");
      b.Line($"  ADD CONSTRAINT [{objectName}]");
      b.Line($"  FOREIGN KEY ({sourceNames})");
      b.Text($"  REFERENCES [dbo].[{targetTableName}]");
      b.Line($" ({targetNames})");
      b.Line("  ON DELETE NO ACTION ON UPDATE NO ACTION;");
      b.Text("END");
      var retValue = b.ToString();
      return retValue;
    }

    // Adds a primary key.
    /// <include path='items/AddPrimaryKey/*' file='Doc/ProcBuilder.xml'/>
    internal string AddPrimaryKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = NetString.DelimitValues(columnList, "[", "]");
      var b = new TextBuilder(128);
      b.Line(Check(objectName, ObjectType.Primary));
      b.Line($" ALTER TABLE [dbo].[{tableName}]");
      b.Line($"  ADD CONSTRAINT [{objectName}]");
      b.Line("  PRIMARY KEY CLUSTERED");
      b.Line("  (");
      b.Line($"    {columnNames} ASC");
      b.Line("  )");
      b.Text("END");
      var retValue = b.ToString();
      return retValue;
    }

    // Adds a unique key.
    /// <include path='items/AddUniqueKey/*' file='Doc/ProcBuilder.xml'/>
    internal string AddUniqueKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = NetString.DelimitValues(columnList, "[", "]");
      var b = new TextBuilder(128);
      b.Line(Check(objectName, ObjectType.Unique));
      b.Line($" ALTER TABLE [dbo].[{tableName}]");
      b.Line($"  ADD CONSTRAINT [{objectName}]");
      b.Line($"  UNIQUE ({columnNames});");
      b.Text("END");
      var retValue = b.ToString();
      return retValue;
    }

    // Returns Create Table SQL.
    /// <include path='items/CreateTable/*' file='Doc/ProcBuilder.xml'/>
    internal string CreateTable(DataColumns dataColumns)
    {
      TableBegin();
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (dataColumn.IdentityIncrement > 0)
        {
          TableIdentity(dataColumn);
        }
        else
        {
          if (dataColumn.NewMaxLength > 0)
          {
            dataColumn.MaxLength = dataColumn.NewMaxLength;
          }
          TableColumn(dataColumn);
        }
      }
      TableEnd();
      var retProc = ToString();
      return retProc;
    }

    // Complete Create Table procedure.
    /// <include path='items/CreateTableProc/*' file='Doc/ProcBuilder.xml'/>
    internal string CreateTableProc(DataColumns dataColumns)
    {
      Begin(CreateProcName);
      Line("AS");
      Line("BEGIN");

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

      Line();
      Line("END");
      var retProc = ToString();
      return retProc;
    }

    // Drops the constraint by provided name.
    /// <include path='items/DropConstraint/*' file='Doc/ProcBuilder.xml'/>
    internal string DropConstraint(string tableName
      , string objectName, ObjectType objectType)
    {
      var b = new TextBuilder(128);
      b.Line(Check(objectName, objectType, true));
      b.Line($" ALTER TABLE[dbo].[{tableName}]");
      b.Line($"  DROP CONSTRAINT[{objectName}]");
      b.Text("END");
      var retValue = b.ToString();
      return retValue;
    }

    // Get column name and type.
    /// <include path='items/NameAndType/*' file='Doc/ProcBuilder.xml'/>
    internal string NameAndType(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(64);

      // Column Name
      b.Text($"  {BeginDelimiter}");
      b.Text($"{dataColumn.Name}");
      b.Text($"{EndDelimiter}");

      // Type Name
      b.Text($" {BeginDelimiter}");
      b.Text($"{dataColumn.TypeName}");
      b.Text($"{EndDelimiter}");

      var retString = b.ToString();
      return retString;
    }

    // Renames a table. Removes old keys and creates new keys.
    /// <include path='items/RenameTableSQL/*' file='Doc/ProcBuilder.xml'/>
    internal string RenameTableSQL(long tableID, DataKeys dataKeys)
    {
      var b = new TextBuilder(512);
      b.Line($"USE [{DBName}]");
      b.Line();
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
          var text = DropConstraint(dataKey.DataTableName
            , dataKey.Name, objectType);
          b.Line();
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

        var objectType = (ObjectType)dataKey.KeyType;
        var text = DropConstraint(TableName, dataKey.Name
          , objectType);
        b.Line();
        b.Line(text);
      }

      b.Line();
      b.Line($"EXEC sp_rename 'dbo.{TableName}', '{TableName}Backup'");
      b.Line($"EXEC sp_rename 'dbo.New{TableName}', '{TableName}'");

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
            b.Line();
            b.Line(text);
            break;

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
            b.Line();
            b.Line(text);
            break;
        }
      }
      b.Line("*/");

      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Adds the Table begin SQL.</summary>
    /// <returns>The table begin SQL text.</returns>
    internal string TableBegin()
    {
      var b = new TextBuilder(128);
      b.Line();
      b.Text("/* Create Table */");
      b.Line(Check(TableName, ObjectType.Table));
      b.Line($"CREATE TABLE [dbo].[{TableName}] (");
      HasColumns = false;
      string retString = b.ToString();
      Text(retString);
      return retString;
    }

    // Adds a table column definition.
    /// <include path='items/TableColumn/*' file='Doc/ProcBuilder.xml'/>
    internal string TableColumn(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(512);
      b.Text(ItemEnd(HasColumns));
      b.Text(NameAndType(dataColumn));

      var typeName = dataColumn.TypeName.Trim().ToLower();
      if ("nvarchar" == typeName
        || "varchar" == typeName)
      {
        b.Text($"({dataColumn.MaxLength})");
      }

      // AllowNull
      if (!dataColumn.AllowNull)
      {
        b.Text(" NOT");
      }
      b.Text(" NULL");

      if (dataColumn.DefaultValue != null)
      {
        b.Text($" DEFAULT {dataColumn.DefaultValue}");
      }

      // Add to Builder property and also return.
      HasColumns = true;
      var retString = b.ToString();
      Text(retString);
      return retString;
    }

    /// <summary>Creates the Table end code.</summary>
    /// <returns>The table end SQL text.</returns>
    internal string TableEnd()
    {
      var b = new TextBuilder(64);
      b.Line();
      b.Line("  )");
      b.Line("END");
      string retString = b.ToString();

      Text(retString);
      return retString;
    }

    // Creates the Identity column.
    /// <include path='items/TableIdentity/*' file='Doc/ProcBuilder.xml'/>
    internal string TableIdentity(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(64);
      b.Text(ItemEnd(HasColumns));
      b.Text(NameAndType(dataColumn));
      b.Text($" IDENTITY ({dataColumn.IdentityStart}");
      b.Text($", {dataColumn.IdentityIncrement}) NOT NULL");

      // Add to Builder property and also return.
      HasColumns = true;
      var retString = b.ToString();
      Text(retString);
      return retString;
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

    #region Alter Methods

    // Checks for the database object.
    /// <include path='items/Check/*' file='Doc/ProcBuilder.xml'/>
    internal string Check(string objectName, ObjectType objectType
      , bool useNot = false)
    {
      string not = null;
      if (useNot)
      {
        not = " NOT";
      }
      var b = new TextBuilder(128);
      if (!EndsWith("\r\n\r\n"))
      {
        b.Line();
      }
      var typeValue = GetObjectTypeValue(objectType);
      b.Line($"IF OBJECT_ID('{objectName}', N'{typeValue}')");
      b.Line($" IS{not} NULL");
      b.Text("BEGIN");
      string retString = b.ToString();
      return retString;
    }

    // Gets the object type prefix value.
    internal string GetObjectTypeValue(ObjectType objectType)
    {
      string retValue = null;

      switch (objectType)
      {
        case ObjectType.Primary:
          retValue = "pk";
          break;

        case ObjectType.Unique:
          retValue = "uq";
          break;

        case ObjectType.Foreign:
          retValue = "f";
          break;

        case ObjectType.Table:
          retValue = "u";
          break;
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

    /// <summary>Gets or sets the Add data Procedure Name.</summary>
    internal string AddProcName { get; set; }

    /// <summary>The beginning identifier delimiter.</summary>
    internal string BeginDelimiter { get; set; }

    /// <summary>Gets or sets the
    /// Create Table Procedure Name.</summary>
    internal string CreateProcName { get; set; }

    /// <summary>Gets or sets the
    /// Database Name.</summary>
    internal string DBName { get; set; }

    /// <summary>The ending identifier delimiter.</summary>
    internal string EndDelimiter { get; set; }

    /// <summary>Gets or sets the
    /// Create Foreign Key Drop Procedure Name.</summary>
    internal string ForeignKeyDropProcName { get; set; }

    /// <summary>Gets or sets the
    /// Create Foreign Key Procedure Name.</summary>
    internal string ForeignKeyProcName { get; set; }

    /// <summary>Gets or sets the
    /// Primary Key Name.</summary>
    internal string PKName { get; set; }

    /// <summary>Gets or sets the
    /// Table Name.</summary>
    internal string TableName { get; set; }

    /// <summary>Gets or sets the
    /// Unique Key Name.</summary>
    internal string UQName { get; set; }

    // Gets or sets the
    // StringBuilder object.
    private TextBuilder Builder { get; set; }

    // Gets or sets an indicator if Create Table already has defined columns.
    private bool HasColumns { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent object reference.
    private DataUtilityList ParentObject { get; set; }
    #endregion
  }

  /// <summary></summary>
  internal enum ObjectType
  {
    /// <summary></summary>
    Primary = 1,
    /// <summary></summary>
    Unique,
    /// <summary></summary>
    Foreign,
    /// <summary></summary>
    Table
  }
}
