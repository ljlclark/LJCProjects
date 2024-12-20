// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcBuilder.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LJCDataUtility
{
  /// <summary>Provides Procedure SQL code.</summary>
  public class ProcBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public ProcBuilder(string dbName, string tableName)
    {
      Reset(dbName, tableName);
    }

    /// <summary>Resets the text values.</summary>
    public void Reset(string dbName = null
      , string tableName = null)
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
      Builder = new TextBuilder(512);
      HasColumns = false;
    }
    #endregion

    #region Data Class Methods

    /// <summary>Returns the built procedure string.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Proc Methods

    /// <summary>Adds the Procedure begin code.</summary>
    public string Begin(string procName)
    {
      var b = new TextBuilder(512);
      b.Line("/* Copyright(c) Lester J. Clark and Contributors. */");
      b.Line("/* Licensed under the MIT License. */");
      b.Line($"/* {procName}.sql */");
      b.Line($"USE [{DBName}]");
      b.Line("GO");
      b.Line("SET ANSI_NULLS ON");
      b.Line("GO");
      b.Line("SET QUOTED_IDENTIFIER ON");
      b.Line("GO");
      b.Line("");
      b.Line($"IF OBJECT_ID('[dbo].[{procName}]', N'p')");
      b.Line(" IS NOT NULL");
      b.Line($"  DROP PROCEDURE [dbo].[{procName}];");
      b.Line("GO");
      b.Line($"CREATE PROCEDURE [dbo].[{procName}]");
      string retString = b.ToString();
      Text(retString);
      return retString;
    }

    /// <summary>Checks if the builder text ends with a supplied value.</summary>
    public bool EndsWith(string value)
    {
      bool retValue = false;
      var text = Builder.ToString();
      if (text.EndsWith(value))
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Gets the Table row IF statement.</summary>
    public string IFItem(string parentTableName
      , string parentIDColumnName, string parentFindColumnName
      , string parmFindName)
    {
      // Reference name "TableNameParentID".
      var varRefName
        = SQLVarName($"{parentTableName}{parentIDColumnName}");

      var b = new TextBuilder(128);
      b.Text($"DECLARE {varRefName} int = ");
      b.Line($"(SELECT {parentIDColumnName} FROM {parentTableName}");
      b.Line($" WHERE {parentFindColumnName} = {parmFindName});");
      var retIf = b.ToString();
      return retIf;
    }

    /// <summary>Creates the insert Columns list.</summary>
    public string ColumnsList(DataColumns dataColumns
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

    /// <summary>Adds a line to the procedure.</summary>
    public void Line(string text = null)
    {
      Builder.Line(text);
    }

    /// <summary>Creates the Parameters.</summary>
    public string Parameters(DataColumns dataColumns, bool isFirst = true)
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

    /// <summary>Creates the Proc body code.</summary>
    public string BodyBegin()
    {
      var b = new TextBuilder(64);
      b.Line("AS");
      b.Line("BEGIN");
      var retValue = b.ToString();
      Text(retValue);
      return retValue;
    }

    /// <summary>Creates a SQL Declaration variable from a DataUtilityColumn.</summary>
    public string SQLDeclaration(DataUtilColumn dataColumn)
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

    /// <summary>Creates a SQL variable name from a column name.</summary>
    public string SQLVarName(string columnName)
    {
      var retName = "";

      // @name
      var startChar = columnName.ToLower()[0];
      retName += $"@{startChar}";
      retName += columnName.Substring(1);
      return retName;
    }

    /// <summary>Adds text to the procedure.</summary>
    public void Text(string text)
    {
      Builder.Text(text);
    }

    /// <summary>Creates the Values list.</summary>
    public string ValuesList(DataColumns dataColumns
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

    /// <summary>Adds a foreign key.</summary>
    public string AddForeignKey(string tableName
      , string objectName, string sourceColumnName
      , string targetTableName, string targetColumnName)
    {
      var b = new TextBuilder(128);
      b.Line(Check(objectName, ObjectType.Foreign));
      b.Line($" ALTER TABLE[dbo].[{tableName}]");
      b.Line($"  ADD CONSTRAINT[{objectName}]");
      b.Line($"  FOREIGN KEY([{sourceColumnName}])");
      b.Text($"  REFERENCES[dbo].[{targetTableName}]");
      b.Line($"([{targetColumnName}])");
      b.Line("  ON DELETE NO ACTION ON UPDATE NO ACTION;");
      b.Text("END");
      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Adds a primary key.</summary>
    public string AddPrimaryKey(string tableName
      , string objectName, string columnList)
    {
      var b = new TextBuilder(128);
      b.Line(Check(objectName, ObjectType.Primary));
      b.Line($" ALTER TABLE[dbo].[{tableName}]");
      b.Line($"  ADD CONSTRAINT[{objectName}]");
      b.Line("  PRIMARY KEY CLUSTERED");
      b.Line("  (");
      b.Line($"   [{columnList}] ASC");
      b.Line("  )");
      b.Text("END");
      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Adds a unique key.</summary>
    public string AddUniqueKey(string tableName
      , string objectName, string columnList)
    {
      var b = new TextBuilder(128);
      b.Line(Check(objectName, ObjectType.Unique));
      b.Line($" ALTER TABLE[dbo].[{tableName}]");
      b.Line($"  ADD CONSTRAINT[{objectName}]");
      b.Line($"  UNIQUE({columnList});");
      b.Text("END");
      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Complete Create Table procedure.</summary>
    public string CreateTableProc(DataColumns dataColumns
      , string primaryKeyList = null, string uniqueKeyList = null)
    {
      Begin(CreateProcName);
      Line("AS");
      Line("BEGIN");

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

      if (primaryKeyList != null)
      {
        var text = AddPrimaryKey(TableName, PKName, primaryKeyList);
        Line(text);
      }
      if (uniqueKeyList != null)
      {
        var text = AddUniqueKey(TableName, UQName, uniqueKeyList);
        Line(text);
      }

      Line("END");
      var retProc = ToString();
      return retProc;
    }

    /// <summary>Drops the constraint by provided name.</summary>
    public string DropConstraint(string tableName
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

    /// <summary>Renames a table.
    /// Removes old keys and creates new keys.</summary>
    public string RenameTableSQL(int tableID, DataKeys dataKeys)
    {
      var b = new TextBuilder(512);
      b.Line($"USE [{DBName}]");
      b.Line();
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

      var retValue = b.ToString();
      return retValue;
    }

    /// <summary>Adds the Table begin code.</summary>
    public string TableBegin()
    {
      var b = new TextBuilder(128);
      b.Line();
      b.Text("/* Create Table */");
      b.Line(Check(TableName, ObjectType.Table));
      b.Line($"CREATE TABLE[dbo].[{TableName}](");
      HasColumns = false;
      string retString = b.ToString();
      Text(retString);
      return retString;
    }

    /// <summary>Adds a table column definition.</summary>
    public string TableColumn(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(512);
      if (HasColumns)
      {
        b.Line(",");
      }
      b.Text($"  [{dataColumn.Name}]");
      b.Text($" [{dataColumn.TypeName}]");
      if ("nvarchar" == dataColumn.TypeName.Trim().ToLower()
        || "varchar" == dataColumn.TypeName.Trim().ToLower())
      {
        b.Text($"({dataColumn.MaxLength})");
      }
      if (!dataColumn.AllowNull)
      {
        b.Text(" NOT");
      }
      b.Text(" NULL");
      if (dataColumn.DefaultValue != null)
      {
        b.Text($" DEFAULT {dataColumn.DefaultValue}");
      }
      HasColumns = true;
      var retString = b.ToString();
      Text(retString);
      return retString;
    }

    /// <summary>Creates the Table end code.</summary>
    public string TableEnd()
    {
      var b = new TextBuilder(64);
      b.Line();
      b.Line("  )");
      b.Line("END");
      string retString = b.ToString();
      Line(retString);
      return retString;
    }

    /// <summary>Creates the Identity column.</summary>
    public string TableIdentity(DataUtilColumn dataColumn)
    {
      var b = new TextBuilder(64);
      if (HasColumns)
      {
        b.Line(",");
      }
      b.Text($"  [{dataColumn.Name}]");
      b.Text($" [{dataColumn.TypeName}]");
      b.Text($" IDENTITY({dataColumn.IdentityStart}");
      b.Text($", {dataColumn.IdentityIncrement}) NOT NULL");
      HasColumns = true;
      string retString = b.ToString();
      Text(retString);
      return retString;
    }
    #endregion

    #region Alter Methods

    /// <summary>Checks for the database object.</summary>
    public string Check(string objectName, ObjectType objectType
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

    /// <summary>Gets the object type value.</summary>
    public string GetObjectTypeValue(ObjectType objectType)
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

    #region Properties

    /// <summary>Gets or sets the
    /// Add data Procedure Name.</summary>
    public string AddProcName { get; set; }

    /// <summary>Gets or sets the
    /// Create Table Procedure Name.</summary>
    public string CreateProcName { get; set; }

    /// <summary>Gets or sets the
    /// Database Name.</summary>
    public string DBName { get; set; }

    /// <summary>Gets or sets the
    /// Create Foreign Key Drop Procedure Name.</summary>
    public string ForeignKeyDropProcName { get; set; }

    /// <summary>Gets or sets the
    /// Create Foreign Key Procedure Name.</summary>
    public string ForeignKeyProcName { get; set; }

    /// <summary>Gets or sets the
    /// Primary Key Name.</summary>
    public string PKName { get; set; }

    /// <summary>Gets or sets the
    /// Table Name.</summary>
    public string TableName { get; set; }

    /// <summary>Gets or sets the
    /// Unique Key Name.</summary>
    public string UQName { get; set; }

    // Gets or sets the
    // StringBuilder object.
    private TextBuilder Builder { get; set; }

    // Gets or sets an
    // indicator if Create Table already
    // has defined columns.
    private bool HasColumns { get; set; }
    #endregion
  }

  /// <summary></summary>
  public enum ObjectType
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

  /// <summary>A StringBuilder helper class.</summary>
  public class TextBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public TextBuilder(int capacity, StringBuilder builder = null)
    {
      if (null == builder)
      {
        builder = new StringBuilder(capacity);
      }
      Builder = builder;
    }
    #endregion

    #region Methods

    /// <summary>Implents the ToString() method.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }

    /// <summary>Adds a line.</summary>
    public void Line(string text = null)
    {
      Builder.AppendLine(text);
    }

    /// <summary>Adds text.</summary>
    public void Text(string text)
    {
      Builder.Append(text);
    }
    #endregion

    #region Properties

    /// <summary>The internal StringBuilder class.</summary>
    public StringBuilder Builder { get; set; }
    #endregion
  }
}
