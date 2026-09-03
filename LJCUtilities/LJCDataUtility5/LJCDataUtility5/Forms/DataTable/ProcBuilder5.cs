// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcBuilder5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  // Provides procedure SQL code.
  internal class ProcBuilder
  {
    #region Static Methods

    // Creates a SQL variable name from a column name.
    internal static string SQLVarName(string columnName)
    {
      var retName = "";

      // @name
      var startChar = columnName.ToLower()[0];
      retName += $"@{startChar}";
      //retName += columnName.Substring(1);
      retName += columnName[1..];
      return retName;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    internal ProcBuilder(DataUtilityList parentObject, string dbName
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
        AddProcName = $"sp_{TableName}Add";
        CreateProcName = $"sp_{TableName}";
        ForeignKeyProcName = $"sp_{TableName}FK";
        ForeignKeyDropProcName = $"sp_{TableName}DropFK";
        PKName = $"pk_{TableName}";
        UQName = $"uq_{TableName}";
      }

      BeginDelimiter = "[";
      EndDelimiter = "]";
      Builder = new LJCTextBuilder();
      HasColumns = false;
      IsFirst = true;
    }
    #endregion

    #region Data Class Methods

    // Returns the builder string.
    /// <include file='Doc/ProcBuilder.xml'
    ///  path='members/ToString/*'/>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region TextBuilder Methods

    // Appends text to the builder.
    internal void AddText(string text)
    {
      Builder.AddText(text);
    }

    // Clears the Builder text.
    internal void ClearText()
    {
      Builder = new LJCTextBuilder();
      IsFirst = true;
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

    // Returns the current indent string.
    internal string GetIndentString()
    {
      var retValue = Builder.GetIndentString();
      return retValue;
    }

    // Changes the IndentCount by the supplied value.
    internal int Indent(int count = 1)
    {
      var retCount = Builder.AddIndent(count);
      return retCount;
    }

    // Adds a line to the builder.
    internal string Line(string? text = null)
    {
      var retLine = Builder.Line(text);
      return retLine;
    }

    // Adds text to the builder.
    internal string Text(string text)
    {
      var retText = Builder.Text(text);
      return retText;
    }
    #endregion

    #region Procedure Methods

    // Adds the Procedure begin code.
    internal string Begin(string procedureName)
    {
      var tb = new LJCTextBuilder();
      tb.Line("/* Copyright(c) Lester J. Clark and Contributors. */");
      tb.Line("/* Licensed under the MIT License. */");
      tb.Line($"/* {procedureName}.sql */");
      tb.Line($"USE [{DBName}]");
      tb.Line("GO");
      tb.Line("SET ANSI_NULLS ON");
      tb.Line("GO");
      tb.Line("SET QUOTED_IDENTIFIER ON");
      tb.Line("GO");
      tb.Line("");
      tb.Line($"IF OBJECT_ID('[dbo].[{procedureName}]', N'p')");
      tb.Line(" IS NOT NULL");
      tb.Line($"  DROP PROCEDURE [dbo].[{procedureName}];");
      tb.Line("GO");
      tb.Line($"CREATE PROCEDURE [dbo].[{procedureName}]");
      string retString = tb.ToString();

      Text(retString);
      return retString;
    }

    // Creates the Proc body code.
    internal string BodyBegin()
    {
      var tb = new LJCTextBuilder();
      tb.Line("AS");
      tb.Line("BEGIN");
      var retValue = tb.ToString();

      Text(retValue);
      return retValue;
    }

    // Creates the insert Columns list.
    internal string ColumnsList(DataColumns dataColumns
      , bool includeParens = true, bool useNewNames = false
      , bool includeID = false)
    {
      var tb = new LJCTextBuilder()
      {
        WrapAtDelimiter = true,
        WrapEnabled = true,
      };
      tb.AddIndent(2);

      var value = "";
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
          tb.Item(nameValue, false, false);
        }
      }

      if (includeParens)
      {
        tb.AddText(")");
      }
      var retList = tb.ToString();
      return retList;
    }

    // Gets the Table row IF statement.
    internal string IFItem(string parentTableName
      , string parentIDColumnName, string parentFindColumnName
      , string parmFindName)
    {
      var varRefName = SQLVarName(parentIDColumnName);

      var tb = new LJCTextBuilder();
      tb.Text($"DECLARE {varRefName} bigint = ");
      tb.Line($"(SELECT {parentIDColumnName} FROM {parentTableName}");
      tb.Line($" WHERE {parentFindColumnName} = {parmFindName});");
      var retIf = tb.ToString();
      return retIf;
    }

    // Creates the Parameters.
    internal string Parameters(DataColumns dataColumns, bool isFirst = true)
    {
      var tb = new LJCTextBuilder();
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (!dataColumn.Name.EndsWith("ID"))
        {
          if (!isFirst)
          {
            tb.AddLine(",");
          }
          isFirst = false;
          var declaration = SQLDeclaration(dataColumn);
          tb.Text($"  {declaration}");
        }
      }
      var retParams = tb.ToString();
      return retParams;
    }

    // Creates a SQL Declaration variable from a DataUtilityColumn.
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

    // Creates the Values list.
    internal string ValuesList(DataColumns dataColumns
      , string? varRefName = null)
    {
      var tb = new LJCTextBuilder();
      tb.Text("    VALUES(");

      if (LJC.HasText(varRefName))
      {
        tb.Text($"{varRefName}, ");
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
          tb.Text(nameValue);
        }
      }

      tb.Text(");");
      var retList = tb.ToString();
      return retList;
    }
    #endregion

    #region Create Table Methods

    // Adds a foreign key.
    internal string AddForeignKey(string tableName
      , string objectName, string sourceColumnList
      , string targetTableName, string targetColumnList)
    {
      var sourceNames = LJCNetString.DelimitValues(sourceColumnList, "[", "]");
      var targetNames = LJCNetString.DelimitValues(targetColumnList, "[", "]");
      var tb = new LJCTextBuilder();
      tb.Line(Check(objectName, ObjectType.Foreign));
      tb.Line($" ALTER TABLE [dbo].[{tableName}]");
      tb.Line($"  ADD CONSTRAINT [{objectName}]");
      tb.Line($"  FOREIGN KEY ({sourceNames})");
      tb.Text($"  REFERENCES [dbo].[{targetTableName}]");
      tb.Line($" ({targetNames})");
      tb.Line("  ON DELETE NO ACTION ON UPDATE NO ACTION;");
      tb.Text("END");
      var retValue = tb.ToString();
      return retValue;
    }

    // Adds a primary key.
    internal string AddPrimaryKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = LJCNetString.DelimitValues(columnList, "[", "]");
      var tb = new LJCTextBuilder();
      tb.Line(Check(objectName, ObjectType.Primary));
      tb.Line($" ALTER TABLE [dbo].[{tableName}]");
      tb.Line($"  ADD CONSTRAINT [{objectName}]");
      tb.Line("  PRIMARY KEY CLUSTERED (");
      tb.Line($"    {columnNames} ASC");
      tb.Line("  )");
      tb.Text("END");
      var retValue = tb.ToString();
      return retValue;
    }

    // Adds a unique key.
    internal string AddUniqueKey(string tableName
      , string objectName, string columnList)
    {
      var columnNames = LJCNetString.DelimitValues(columnList, "[", "]");
      var tb = new LJCTextBuilder();
      tb.Line(Check(objectName, ObjectType.Unique));
      tb.Line($" ALTER TABLE [dbo].[{tableName}]");
      tb.Line($"  ADD CONSTRAINT [{objectName}]");
      tb.Line($"  UNIQUE ({columnNames});");
      tb.Text("END");
      var retValue = tb.ToString();
      return retValue;
    }

    // Returns Create Table SQL.
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
    internal string CreateTableProc(DataColumns dataColumns)
    {
      Begin(CreateProcName);
      Line("AS");
      Line("BEGIN");

      CreateTable(dataColumns);

      var keyGridCode = ParentObject.KeyGridCode;
      var keyValues = keyGridCode.PrimaryKeyColumns();
      if (LJC.HasText(keyValues))
      {
        Line();
        var text = AddPrimaryKey(TableName, PKName, keyValues);
        Text(text);
      }

      keyValues = keyGridCode.UniqueKeyColumns();
      if (LJC.HasText(keyValues))
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
    /// <include path='members/DropConstraint/*' file='Doc/ProcBuilder.xml'/>
    internal string DropConstraint(string tableName
      , string objectName, ObjectType objectType)
    {
      var tb = new LJCTextBuilder();
      tb.Line(Check(objectName, objectType, true));
      tb.Line($" ALTER TABLE[dbo].[{tableName}]");
      tb.Line($"  DROP CONSTRAINT[{objectName}]");
      tb.Text("END");
      var retValue = tb.ToString();
      return retValue;
    }

    // Get column name and type.
    /// <include path='members/NameAndType/*' file='Doc/ProcBuilder.xml'/>
    internal string NameAndType(DataUtilColumn dataColumn)
    {
      var tb = new LJCTextBuilder();

      // Column Name
      tb.Text($"  {BeginDelimiter}");
      tb.AddText($"{dataColumn.Name}");
      tb.AddText($"{EndDelimiter}");

      // Type Name
      tb.AddText($" {BeginDelimiter}");
      tb.AddText($"{dataColumn.TypeName}");
      tb.AddText($"{EndDelimiter}");

      var retString = tb.ToString();
      return retString;
    }

    // Renames a table. Removes old keys and creates new keys.
    internal string RenameTableSQL(long tableID, long siteID, DataKeys dataKeys)
    {
      var tb = new LJCTextBuilder();
      tb.Line($"USE [{DBName}]");
      tb.Line();
      tb.Line("/*");
      tb.Text("/* Drop foreign keys and other constraints. */");

      // Drop referencing foreign keys.
      var foreignKeys = dataKeys.FindAll(x => x.TargetTableName == TableName
        && x.KeyType == (short)ObjectType.Foreign);
      foreach (DataKey dataKey in foreignKeys)
      {
        if (LJC.HasText(dataKey.DataTableName))
        {
          var text = DropConstraint(dataKey.DataTableName
            , dataKey.Name, ObjectType.Foreign);
          tb.Line();
          tb.Line(text);
        }
      }

      // Drop constraints and foreign keys.
      var otherKeys = dataKeys.FindAll(x => x.DataTableId == tableID
        && x.DataTableDbId == siteID
        && x.KeyType != (short)ObjectType.Primary);
      foreach (DataKey dataKey in otherKeys)
      {
        var objectType = (ObjectType)dataKey.KeyType;
        var text = DropConstraint(TableName, dataKey.Name
          , objectType);
        tb.Line();
        tb.Line(text);
      }

      tb.Line();
      tb.Line($"EXEC sp_rename 'dbo.{TableName}', '{TableName}Backup'");
      tb.Line($"EXEC sp_rename 'dbo.New{TableName}', '{TableName}'");

      tb.Line();
      tb.Text("/* Add constraints and foreign keys. */");
      foreach (DataKey dataKey in otherKeys)
      {
        string text;
        switch ((ObjectType)dataKey.KeyType)
        {
          case ObjectType.Primary:
            if (LJC.HasText(dataKey.SourceColumnName))
            {
              text = AddPrimaryKey(TableName, dataKey.Name
                , dataKey.SourceColumnName);
              tb.Line();
              tb.Line(text);
            }
            break;

          case ObjectType.Unique:
            var columnList = dataKey.SourceColumnName;
            if (LJC.HasText(columnList))
            {
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

      // Add referencing foreign keys.
      foreach (DataKey dataKey in dataKeys)
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
      tb.Line("*/");
      var retValue = tb.ToString();
      return retValue;
    }

    // Adds the Table begin SQL.
    internal string TableBegin()
    {
      var tb = new LJCTextBuilder();
      tb.Line();
      tb.Text("/* Create Table */");
      tb.AddLine(Check(TableName, ObjectType.Table));
      tb.Line($" CREATE TABLE [dbo].[{TableName}] (");
      HasColumns = false;
      string retString = tb.ToString();
      Builder.Text(retString);
      return retString;
    }

    // Adds a table column definition.
    internal string TableColumn(DataUtilColumn dataColumn)
    {
      var tb = new LJCTextBuilder();
      tb.AddText(ItemEnd(HasColumns));
      tb.Text(NameAndType(dataColumn));

      var typeName = dataColumn.TypeName.Trim().ToLower();
      if ("nvarchar" == typeName
        || "varchar" == typeName)
      {
        tb.AddText($"({dataColumn.MaxLength})");
      }

      // AllowNull
      if (!dataColumn.AllowNull)
      {
        tb.AddText(" NOT");
      }
      tb.AddText(" NULL");

      if (dataColumn.DefaultValue != null)
      {
        tb.AddText($" DEFAULT {dataColumn.DefaultValue}");
      }

      // Add to Builder property and also return.
      HasColumns = true;
      var retString = tb.ToString();
      AddText(retString);
      return retString;
    }

    /// <summary>Creates the Table end code.</summary>
    internal string TableEnd()
    {
      var tb = new LJCTextBuilder();
      tb.Line(" )");
      tb.Line("END");
      string retString = tb.ToString();

      Text(retString);
      return retString;
    }

    // Creates the Identity column.
    internal string TableIdentity(DataUtilColumn dataColumn)
    {
      var tb = new LJCTextBuilder();
      tb.AddText(ItemEnd(HasColumns));
      tb.Text(NameAndType(dataColumn));
      tb.AddText($" IDENTITY ({dataColumn.IdentityStart}");
      tb.AddText($", {dataColumn.IdentityIncrement}) NOT NULL");

      // Add to Builder property and also return.
      HasColumns = true;
      var retString = tb.ToString();
      AddText(retString);
      return retString;
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

    #region Alter Methods

    // Checks for the database object.
    internal string Check(string objectName, ObjectType objectType
      , bool useNot = false)
    {
      string? not = null;
      if (useNot)
      {
        not = " NOT";
      }
      var tb = new LJCTextBuilder();
      if (!EndsWith("\r\n\r\n"))
      {
        tb.Line();
      }
      var typeValue = GetObjectTypeValue(objectType);
      tb.Line($"IF OBJECT_ID('{objectName}', N'{typeValue}')");
      tb.Line($" IS{not} NULL");
      tb.Text("BEGIN");
      string retString = tb.ToString();
      return retString;
    }

    // Gets the object type prefix value.
    internal string? GetObjectTypeValue(ObjectType objectType)
    {
      string? retValue = null;

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

    // Gets or sets the Create Foreign Key Drop Procedure Name.
    internal string ForeignKeyDropProcName { get; set; } = null!;

    // Gets or sets the Create Foreign Key Procedure Name.
    internal string ForeignKeyProcName { get; set; } = null!;

    // Gets or sets the Primary Key Name.
    internal string PKName { get; set; } = null!;

    /// <summary>Gets or sets the
    /// Table Name.</summary>
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
      get => Builder.Delimiter;
      set { Builder.Delimiter = value; }
    }

    // Gets or sets the indent character count.
    internal int IndentCharCount
    {
      get => Builder.IndentCharCount;
      set { Builder.IndentCharCount = value; }
    }

    // Gets or sets the indent count.
    internal int IndentCount
    {
      get => Builder.IndentCount;
      set { Builder.AddIndent(value); }
    }

    // Gets or sets the first item indicator.
    internal bool IsFirst
    {
      get => Builder.IsFirst;
      set { Builder.IsFirst = value; }
    }

    // Gets or sets the TextBuilder object.
    private LJCTextBuilder Builder { get; set; } = null!;
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
