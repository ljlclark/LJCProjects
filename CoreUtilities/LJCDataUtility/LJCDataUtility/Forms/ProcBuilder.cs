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

// Constructors
//   public ProcBuilder(string dbName, string tableName)
// Data Class Methods
//   public override string ToString()
// Proc Methods
//   public string Begin()
//   public string IFItem(string parentTableName
//     , string parentIDColumnName, string parentFindColumnName)
//   public string InsertList(DataColumns dataColumns)
//   public void Line(string text = null)
//   public string Parameters(DataColumns dataColumns)
//   public string SQLDeclaration(DataUtilityColumn dataColumn)
//   public string SQLVarName(string columnName)
//   public void Text(string text)
//   public string ValuesList(DataColumns dataColumns
//     , string varRefName = null)
// Create Table Methods
//   public string CreateTableProc(DataColumns columns
//     , string primaryKeyList = null, string uniqueKeyList = null)
//   public string TableBegin()
//   public string TableCheck()
//   public string TableColumn(string name, string typeName
//     , bool allowNull = true, short maxLength = 0
//     , string defaultValue = null)
//   public string TableEnd()
//   public string TableIdentity(string name, string typeName
//     , short idBegin = 1, short idIncrement = 1)
// Alter Methods
//   public string AlterPrimaryKey(string columnsList)
//   public string AlterUniqueKey(string columnsList)
//   public string PKCheck()
//   public string UQCheck()
// 21 Methods
// Properties
//   public string AddProcName { get; set; }
//   public string CreateProcName { get; set; }
//   public string DBName { get; set; }
//   public string PKName { get; set; }
//   public string TableName { get; set; }
//   public string UQName { get; set; }

namespace LJCDataUtility
{
  /// <summary>Provides Procedure SQL code.</summary>
  public class ProcBuilder
  {
    // ******************************
    #region Constructors
    // ******************************

    /// <summary>Initializes an object instance.</summary>
    // ********************
    public ProcBuilder(string dbName, string tableName)
    {
      DBName = dbName;
      TableName = tableName;
      AddProcName = $"sp_{TableName}Add";
      CreateProcName = $"sp_{TableName}";
      PKName = $"pk_{TableName}";
      UQName = $"uq_{TableName}";
      Builder = new StringBuilder(512);
      HasColumns = false;
    }
    #endregion

    // ******************************
    #region Data Class Methods
    // ******************************

    /// <summary>Returns the built procedure string.</summary>
    // ********************
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    // ******************************
    #region Proc Methods
    // ******************************

    /// <summary>Adds the Procedure begin code.</summary>
    // ********************
    public string Begin()
    {
      var addProcName = $"{CreateProcName}Add";
      var b = new StringBuilder(512);
      b.AppendLine("/* Copyright(c) Lester J. Clark and Contributors. */");
      b.AppendLine("/* Licensed under the MIT License. */");
      b.AppendLine($"/* {addProcName}.sql */");
      b.AppendLine($"USE[{DBName}]");
      b.AppendLine("GO");
      b.AppendLine("SET ANSI_NULLS ON");
      b.AppendLine("GO");
      b.AppendLine("SET QUOTED_IDENTIFIER ON");
      b.AppendLine("GO");
      b.AppendLine("");
      b.AppendLine($"IF OBJECT_ID(' [dbo].[{addProcName}]', N'p')");
      b.AppendLine(" IS NOT NULL");
      b.AppendLine($"  DROP PROCEDURE [dbo].[{addProcName}];");
      b.AppendLine("GO");
      b.AppendLine($"CREATE PROCEDURE [dbo].[{addProcName}]");

      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Gets the Table row IF statement.</summary>
    // ********************
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
    // ********************
    public string InsertList(DataColumns dataColumns)
    {
      var value = "    (";
      var retList = value;
      var lineLength = value.Length;

      var first = true;
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (dataColumn.Name != "ID")
        {
          var nameValue = $"{dataColumn.Name}";
          lineLength += nameValue.Length;

          if (lineLength > 80)
          {
            var newLine = "\r\n     ";
            retList += newLine;
            lineLength = newLine.Length - 2;
          }

          if (!first)
          {
            var firstValue = ", ";
            retList += firstValue;
            lineLength += firstValue.Length;
          }
          first = false;

          retList += nameValue;
        }
      }
      retList += ")";
      return retList;
    }

    /// <summary>Adds a line to the procedure.</summary>
    // ********************
    public void Line(string text = null)
    {
      Builder.AppendLine(text);
    }

    /// <summary>Creates the Parameters.</summary>
    // ********************
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

    /// <summary>Creates a SQL Declaration variable from a DataUtilityColumn.</summary>
    // ********************
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
    // ********************
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
    // ********************
    public void Text(string text)
    {
      Builder.Append(text);
    }

    /// <summary>Creates the Values list.</summary>
    // ********************
    public string ValuesList(DataColumns dataColumns
      , string varRefName = null)
    {
      var value = "    VALUES(";
      var retList = value;
      var lineLength = value.Length;

      if (NetString.HasValue(varRefName))
      {
        value = $"{varRefName}, ";
        retList += value;
        lineLength += varRefName.Length;
      }

      var first = true;
      foreach (DataUtilColumn dataColumn in dataColumns)
      {
        if (!dataColumn.Name.EndsWith("ID"))
        {
          var nameValue = SQLVarName(dataColumn.Name);
          lineLength += nameValue.Length;

          if (lineLength > 80)
          {
            var newLine = "\r\n     ";
            retList += newLine;
            lineLength = newLine.Length - 2;
          }

          if (!first)
          {
            var firstValue = ", ";
            retList += firstValue;
            lineLength += firstValue.Length;
          }
          first = false;

          retList += nameValue;
        }
      }
      retList += ");";
      return retList;
    }
    #endregion

    // ******************************
    #region Create Table Methods
    // ******************************

    /// <summary>Complete Create Table procedure.</summary>
    // ********************
    public string CreateTableProc(DataColumns columns
      , string primaryKeyList = null, string uniqueKeyList = null)
    {
      Begin();
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
          TableColumn(column.Name, column.TypeName, column.AllowNull
            , column.MaxLength, column.DefaultValue);
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
    // ********************
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
    // ********************
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
    // ********************
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
        b.Append($" default {defaultValue}");
      }

      HasColumns = true;
      var retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Creates the Table end code.</summary>
    // ********************
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
    // ********************
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

    // ******************************
    #region Alter Methods
    // ******************************

    /// <summary>Creates the Primary Key code.</summary>
    // ********************
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
    // ********************
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
    // ********************
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
    // ********************
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

    // ******************************
    #region Properties
    // ******************************

    /// <summary>Gets or sets the
    /// Procedure Name value.</summary>
    public string AddProcName { get; set; }

    /// <summary>Gets or sets the
    /// Create Table Procedure Name.</summary>
    public string CreateProcName { get; set; }

    /// <summary>Gets or sets the
    /// Database Name.</summary>
    public string DBName { get; set; }

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
