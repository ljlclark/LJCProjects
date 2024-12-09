﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcBuilder.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    public void Reset(string dbName = null, string tableName = null)
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
      Builder = new StringBuilder(512);
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
      var b = new StringBuilder(512);
      b.AppendLine("/* Copyright(c) Lester J. Clark and Contributors. */");
      b.AppendLine("/* Licensed under the MIT License. */");
      b.AppendLine($"/* {procName}.sql */");
      b.AppendLine($"USE [{DBName}]");
      b.AppendLine("GO");
      b.AppendLine("SET ANSI_NULLS ON");
      b.AppendLine("GO");
      b.AppendLine("SET QUOTED_IDENTIFIER ON");
      b.AppendLine("GO");
      b.AppendLine("");
      b.AppendLine($"IF OBJECT_ID('[dbo].[{procName}]', N'p')");
      b.AppendLine(" IS NOT NULL");
      b.AppendLine($"  DROP PROCEDURE [dbo].[{procName}];");
      b.AppendLine("GO");
      b.AppendLine($"CREATE PROCEDURE [dbo].[{procName}]");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Gets the Table row IF statement.</summary>
    public string IFItem(string parentTableName
      , string parentIDColumnName, string parentFindColumnName
      , string parmFindName)
    {
      // Reference name "TableNameParentID".
      var varRefName
        = SQLVarName($"{parentTableName}{parentIDColumnName}");

      var b = new StringBuilder(128);
      b.Append($"DECLARE {varRefName} int = ");
      b.AppendLine($"(SELECT {parentIDColumnName} FROM {parentTableName}");
      b.AppendLine($" WHERE {parentFindColumnName} = {parmFindName});");
      var retIf = b.ToString();
      return retIf;
    }

    /// <summary>Creates the insert Columns list.</summary>
    public string ColumnsList(DataColumns dataColumns
      , bool includeParens = true, bool useNewNames = false
      , bool includeID = false)
    {
      var b = new StringBuilder(256);
      var value = "    ";
      if (includeParens)
      {
        value += "(";
      }
      b.Append(value);
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
          b.Append(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
          lineLength += nameValue.Length;
        }

        if (!first)
        {
          var firstValue = ", ";
          b.Append(firstValue);
          lineLength += firstValue.Length;
        }
        first = false;

        b.Append(nameValue);
      }

      if (includeParens)
      {
        b.Append(")");
      }
      var retList = b.ToString();
      return retList;
    }

    /// <summary>Adds a line to the procedure.</summary>
    public void Line(string text = null)
    {
      Builder.AppendLine(text);
    }

    /// <summary>Creates the Parameters.</summary>
    public string Parameters(DataColumns dataColumns, bool isFirst = true)
    {
      var retParams = "";

      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (!dataColumn.Name.EndsWith("ID"))
        {
          if (!isFirst)
          {
            retParams += ",\r\n";
          }
          isFirst = false;
          var declaration = SQLDeclaration(dataColumn);
          retParams += $"  {declaration}";
        }
      }
      return retParams;
    }

    /// <summary>Creates the Proc body code.</summary>
    public string BodyBegin()
    {
      var b = new StringBuilder(64);
      b.AppendLine("AS");
      b.AppendLine("BEGIN");

      var retValue = b.ToString();
      Builder.Append(retValue);
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
      Builder.Append(text);
    }

    /// <summary>Creates the Values list.</summary>
    public string ValuesList(DataColumns dataColumns
      , string varRefName = null)
    {
      var b = new StringBuilder(256);
      var value = "    VALUES(";
      b.Append(value);
      var lineLength = value.Length;

      if (NetString.HasValue(varRefName))
      {
        value = $"{varRefName}, ";
        b.Append(value);
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
          b.Append(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
          lineLength += nameValue.Length;
        }

        if (!first)
        {
          var firstValue = ", ";
          b.Append(firstValue);
          lineLength += firstValue.Length;
        }
        first = false;

        b.Append(nameValue);
      }

      b.Append(");");
      var retList = b.ToString();
      return retList;
    }
    #endregion

    #region Create Table Methods

    /// <summary>Complete Create Table procedure.</summary>
    public string CreateTableProc(DataColumns columns
      , string primaryKeyList = null, string uniqueKeyList = null)
    {
      Begin(CreateProcName);
      Line("AS");
      Line("BEGIN");

      TableBegin();
      foreach (DataUtilColumn column in columns)
      {
        if (column.IdentityIncrement > 0)
        {
          TableIdentity(column.Name, column.TypeName
            , column.IdentityStart, column.IdentityIncrement);
        }
        else
        {
          var maxLength = column.MaxLength;
          if (column.NewMaxLength > 0)
          {
            maxLength = column.MaxLength;
          }
          TableColumn(column.Name, column.TypeName, column.AllowNull
            , maxLength, column.DefaultValue);
        }
      }

      Line();
      TableEnd();
      if (primaryKeyList != null)
      {
        Line();
        AlterPrimaryKey(primaryKeyList);
      }
      if (uniqueKeyList != null)
      {
        Line();
        AlterUniqueKey(uniqueKeyList);
      }

      Line("END");

      var retProc = ToString();
      return retProc;
    }

    /// <summary>Adds the Table begin code.</summary>
    public string TableBegin()
    {
      var b = new StringBuilder(128);
      b.AppendLine("/* Create Table */");
      b.AppendLine(TableCheck());
      b.AppendLine($"CREATE TABLE[dbo].[{TableName}](");

      HasColumns = false;
      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Checks if table exists.</summary>
    public string TableCheck()
    {
      var b = new StringBuilder(128);
      b.AppendLine($"IF OBJECT_ID('{TableName}', N'u')");
      b.AppendLine(" IS NULL");
      b.AppendLine("BEGIN");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Adds a table column definition.</summary>
    public string TableColumn(string name, string typeName
      , bool allowNull = true, short maxLength = 0
      , string defaultValue = null)
    {
      var b = new StringBuilder(512);
      if (HasColumns)
      {
        b.AppendLine(",");
      }
      b.Append($"  [{name}]");
      b.Append($" [{typeName}]");
      if ("nvarchar" == typeName.Trim().ToLower()
        || "varchar" == typeName.Trim().ToLower())
      {
        b.Append($"({maxLength})");
      }
      if (!allowNull)
      {
        b.Append(" NOT");
      }
      b.Append(" NULL");
      if (defaultValue != null)
      {
        b.Append($" DEFAULT {defaultValue}");
      }

      HasColumns = true;
      var retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Creates the Table end code.</summary>
    public string TableEnd()
    {
      var b = new StringBuilder(64);
      b.AppendLine("  )");
      b.AppendLine("END");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Creates the Identity column.</summary>
    public string TableIdentity(string name, string typeName
      , short idBegin = 1, short idIncrement = 1)
    {
      var b = new StringBuilder(64);
      if (HasColumns)
      {
        b.AppendLine(",");
      }
      b.Append($"  [{name}]");
      b.Append($" [{typeName}]");
      b.Append($" IDENTITY({idBegin}, {idIncrement}) NOT NULL");

      HasColumns = true;
      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }
    #endregion

    #region Alter Methods

    /// <summary>Creates the Primary Key code.</summary>
    public string AlterPrimaryKey(string columnsList)
    {
      var b = new StringBuilder(128);
      b.AppendLine(PKCheck());
      b.AppendLine($"ALTER TABLE[dbo].[{TableName}]");
      b.AppendLine($"  ADD CONSTRAINT[{PKName}]");
      b.AppendLine("  PRIMARY KEY CLUSTERED");
      b.AppendLine("  (");
      b.AppendLine($"    [{columnsList}] ASC");
      b.AppendLine("  )");
      b.AppendLine("END");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Creates the Unique Key code.</summary>
    public string AlterUniqueKey(string columnsList)
    {
      var b = new StringBuilder(512);
      b.AppendLine(UQCheck());
      b.AppendLine("ALTER TABLE[dbo].[{TableName}]");
      b.AppendLine($"  ADD CONSTRAINT[{UQName}]");
      b.AppendLine($"  UNIQUE({columnsList});");
      b.AppendLine("END");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Checks for the primary key.</summary>
    public string PKCheck()
    {
      var b = new StringBuilder(128);
      b.AppendLine($"IF OBJECT_ID('{PKName}', N'pk'");
      b.AppendLine(" IS NULL");
      b.AppendLine("BEGIN");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Checks for the unique key.</summary>
    public string UQCheck()
    {
      var b = new StringBuilder(128);
      b.AppendLine($"IF OBJECT_ID('{UQName}', N'uq')");
      b.AppendLine(" IS NULL");
      b.AppendLine("BEGIN");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
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
    private StringBuilder Builder { get; set; }

    // Gets or sets an
    // indicator if Create Table already
    // has defined columns.
    private bool HasColumns { get; set; }
    #endregion
  }
}
