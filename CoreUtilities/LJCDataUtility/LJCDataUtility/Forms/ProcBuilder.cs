using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
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
    /// <summary>Initializes an object instance.</summary>
    // ********************
    public ProcBuilder(string dbName, string tableName)
    {
      DBName = dbName;
      TableName = tableName;
      ProcName = $"sp_{TableName}";
      PKName = $"pk_{TableName}";
      UQName = $"uq_{TableName}";
      Builder = new StringBuilder(512);
      HasColumns = false;
    }

    /// <summary>Returns the built procedure string.</summary>
    // ********************
    public override string ToString()
    {
      return Builder.ToString();
    }

    /// <summary>Creates a Create Table procedure.</summary>
    // ********************
    public string TableCreateProc(DataColumns columns
      , string primaryKeyList = null, string uniqueKeyList = null)
    {
      Begin();
      TableBegin();

      foreach (DataUtilityColumn column in columns)
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
        PrimaryKey(primaryKeyList);
      }
      if (uniqueKeyList != null)
      {
        Line();
        UniqueKey(uniqueKeyList);
      }
      Line("END");
      var retProc = ToString();
      return retProc;
    }

    /// <summary>Creates the Procedure begin code.</summary>
    // ********************
    public string Begin()
    {
      var b = new StringBuilder(512);
      b.AppendLine("/* Copyright(c) Lester J. Clark and Contributors. */");
      b.AppendLine("/* Licensed under the MIT License. */");
      b.AppendLine($"/* {ProcName} */");
      b.AppendLine($"USE[{DBName}]");
      b.AppendLine("GO");
      b.AppendLine("SET ANSI_NULLS ON");
      b.AppendLine("GO");
      b.AppendLine("SET QUOTED_IDENTIFIER ON");
      b.AppendLine("GO");
      b.AppendLine("");
      b.AppendLine($"IF OBJECT_ID('{ProcName}', N'p')");
      b.AppendLine(" IS NOT NULL");
      b.AppendLine($"  DROP PROCEDURE[dbo].[{ProcName}];");
      b.AppendLine("GO");
      b.AppendLine($"CREATE PROCEDURE[dbo].[{ProcName}]");
      b.AppendLine("AS");
      b.AppendLine("BEGIN");
      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Adds a line to the procedure.</summary>
    // ********************
    public void Line(string text = null)
    {
      Builder.AppendLine(text);
    }

    /// <summary>Creates the Primary Key code.</summary>
    // ********************
    public string PrimaryKey(string columnsList)
    {
      var b = new StringBuilder(128);
      b.AppendLine($"IF OBJECT_ID('{PKName}', N'pk'");
      b.AppendLine(" IS NULL");
      b.AppendLine("BEGIN");
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

    /// <summary>Creates the Table begin code.</summary>
    // ********************
    public string TableBegin()
    {
      var b = new StringBuilder(128);
      b.AppendLine("/* Create Table */");
      b.AppendLine($"IF OBJECT_ID('{TableName}', N'u')");
      b.AppendLine(" IS NULL");
      b.AppendLine("BEGIN");
      b.AppendLine($"CREATE TABLE[dbo].[{TableName}](");
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
        b.Append($" default {defaultValue}");
      }
      var retString = b.ToString();
      HasColumns = true;
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
      string retString = b.ToString();
      HasColumns = true;
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

    /// <summary>Adds text to the procedure.</summary>
    // ********************
    public void Text(string text)
    {
      Builder.Append(text);
    }

    /// <summary>Creates the Unique Key code.</summary>
    // ********************
    public string UniqueKey(string columnsList)
    {
      var b = new StringBuilder(512);
      b.AppendLine("IF OBJECT_ID('{UQName}', N'uq')");
      b.AppendLine(" IS NULL");
      b.AppendLine("BEGIN");
      b.AppendLine("ALTER TABLE[dbo].[{TableName}]");
      b.AppendLine("  ADD CONSTRAINT[{UQName}]");
      b.AppendLine($"  UNIQUE({columnsList});");
      b.AppendLine("END");
      string retString = b.ToString();
      Builder.Append(retString);
      return retString;
    }

    /// <summary>Gets the Database Name value.</summary>
    public string DBName { get; set; }

    /// <summary>Gets the Primary Key Name value.</summary>
    public string PKName { get; set; }

    /// <summary>Gets the Procedure Name value.</summary>
    public string ProcName { get; set; }

    /// <summary>Gets the Table Name value.</summary>
    public string TableName { get; set; }

    /// <summary>Gets the Unique Key Name value.</summary>
    public string UQName { get; set; }

    // The StringBuilder object.
    private StringBuilder Builder { get; set; }

    // Indicates if the Create Table already has defined columns.
    private bool HasColumns { get; set; }
  }
}
