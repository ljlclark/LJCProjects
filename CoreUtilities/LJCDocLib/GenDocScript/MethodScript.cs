﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodScript.cs
using LJCDBMessage;
using LJCDocLibDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GenDocScript
{
  // Represents the DocMethod script.
  internal class MethodScript
  {
    // Initializes an object instance.
    internal MethodScript()
    {
      var managers = ValuesDocGen.Instance.Managers;
      mMethodManager = managers.DocMethodManager;
    }

    // Generates the script.
    internal void Gen()
    {
      var propertyNames = MethodPropertyNames();
      DbJoins methodJoins = MethodJoins();
      mMethodManager.SetOrderBy(MethodOrderByNames());
      var result = mMethodManager.Manager.Load(null, propertyNames, null
        , methodJoins);

      var fileName = "17DocMethod.sql";
      File.WriteAllText(fileName, ScriptHeader());
      mPrevAssemblyName = "Nothing";
      mPrevClassName = "Nothing";
      mPrevHeadingName = "Nothing";
      foreach (var row in result.Rows)
      {
        mMethodValues = GetMethodValues(row);

        StringBuilder builder = new StringBuilder(256);
        builder.Append(AssemblyHeader());
        builder.Append(GroupHeader());
        mPrevAssemblyName = mMethodValues.AssemblyName;
        mPrevClassName = mMethodValues.ClassName;
        mPrevHeadingName = mMethodValues.HeadingName;
        var methodName = mMethodValues.MethodName;
        Console.WriteLine($"Method: {methodName}");

        builder.AppendLine($"exec sp_DMAddUnique @docClassName, @headingName");
        builder.AppendLine($"  , '{methodName}'");
        builder.AppendLine($"  , '{mMethodValues.Description}'");
        builder.Append($"  , @seq");
        if (NetString.HasValue(mMethodValues.OverloadName))
        {
          builder.Append($", 1, '{mMethodValues.OverloadName}'");
        }
        builder.AppendLine(";");
        var text = builder.ToString();
        File.AppendAllText(fileName, text);
      }
    }

    #region Private Methods

    // Creates and returns the script assembly header.
    private string AssemblyHeader()
    {
      string retValue = null;

      // Assembly has changed.
      var assemblyName = mMethodValues.AssemblyName;
      if (assemblyName != mPrevAssemblyName)
      {
        Console.WriteLine();
        Console.WriteLine($"Assembly: {assemblyName}");
        mPrevHeadingName = "Nothing";
        StringBuilder builder = new StringBuilder(256);
        builder.AppendLine();
        builder.AppendLine($"/* {assemblyName} */");
        builder.AppendLine("/* ------------------------------ */");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Gets the method values.
    private MethodValues GetMethodValues(DbRow row)
    {
      var retValue = new MethodValues();

      var values = row.Values;
      retValue.AssemblyName = values.LJCGetValue("AName");
      retValue.ClassName = values.LJCGetValue("CName");
      retValue.HeadingName = values.LJCGetValue("HeadingName");
      retValue.MethodName = values.LJCGetValue("Name");
      var description = values.LJCGetValue("Description");
      retValue.Description = description.Replace("\n", "");
      retValue.Sequence = values.LJCGetInt32("Sequence");
      retValue.OverloadName = values.LJCGetValue("OverloadName");
      return retValue;
    }

    // Creates and returns the script method header.
    private string GroupHeader()
    {
      string retValue;

      // Method Group has changed.
      StringBuilder builder = new StringBuilder(256);
      var assemblyName = mMethodValues.AssemblyName;
      var className = mMethodValues.ClassName;
      var headingName = mMethodValues.HeadingName;
      if (headingName != mPrevHeadingName)
      {
        // Assembly has not changed.
        if (assemblyName == mPrevAssemblyName)
        {
          builder.AppendLine();
        }
        Console.WriteLine();
        Console.WriteLine($"Class: {className}");
        Console.WriteLine($"Group: {headingName}");
        builder.AppendLine($"set @docClassName = '{className}';");
        builder.AppendLine($"set @headingName = '{headingName}';");
        builder.AppendLine("set @seq = 1;");
      }
      else
      {
        // Method Group has not changed.
        builder.AppendLine("set @seq += 1;");
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Creates and return the joins definition.
    private DbJoins MethodJoins()
    {
      var retValue = new DbJoins();
      DbJoin join = new DbJoin()
      {
        TableAlias = "dc",
        TableName = DocClass.TableName,
        JoinOns = new DbJoinOns()
        {
          { DocMethod.ColumnDocClassID, "ID" }
        },
        Columns = new DbColumns
        {
          // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
          { DocClass.ColumnName, "CName", "ClassName" }
        }
      };
      retValue.Add(join);

      join = new DbJoin()
      {
        TableAlias = "da",
        TableName = DocAssembly.TableName,
        JoinOns = new DbJoinOns()
        {
          { "dc.DocAssemblyID", "ID" }
        },
        Columns = new DbColumns
        {
          // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
          { DocAssembly.ColumnName, "AName", "AssemblyName" }
        }
      };
      retValue.Add(join);

      join = new DbJoin()
      {
        TableAlias = "dmg",
        TableName = DocMethodGroup.TableName,
        JoinOns = new DbJoinOns()
        {
          { DocMethod.ColumnDocMethodGroupID, "ID" }
        },
        Columns = new DbColumns
        {
          // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
          { "HeadingName" }
        }
      };
      retValue.Add(join);
      return retValue;
    }

    // Gets the method order by names.
    private List<string> MethodOrderByNames()
    {
      var retValue = new List<string>()
      {
        $"da.{DocAssembly.ColumnName}",
        $"dc.{DocClass.ColumnName}",
        $"dmg.{DocMethodGroup.ColumnHeadingName}",
        DocMethod.ColumnSequence
      };
      return retValue;
    }

    // Gets the method property names.
    private List<string> MethodPropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocMethod.ColumnName },
        { DocMethod.ColumnDescription },
        { DocMethod.ColumnSequence },
        { DocMethod.ColumnOverloadName }
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 17DocMethod.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.AppendLine("select dm.ID 'DocMethod', da.Name 'Assembly Name', dm.DocClassID, dc.Name,");
      builder.AppendLine("  DocMethodGroupID, dmg.HeadingName 'Group Heading Name', dm.Name,");
      builder.AppendLine("  dm.Description, dm.Sequence, OverloadName");
      builder.AppendLine("from DocMethod as dm");
      builder.AppendLine("left join DocClass as dc on DocClassID = dc.ID");
      builder.AppendLine("left join DocAssembly as da on dc.DocAssemblyID = da.ID");
      builder.AppendLine("left join DocMethodGroup as dmg on DocMethodGroupID = dmg.ID");
      builder.AppendLine("order by da.Name, dc.Name, dmg.HeadingName, Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();
      builder.AppendLine("declare @docClassName nvarchar(60);");
      builder.AppendLine("declare @headingName nvarchar(60);");
      builder.AppendLine("declare @seq int");
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocMethodManager mMethodManager;
    private MethodValues mMethodValues;
    private string mPrevAssemblyName;
    private string mPrevClassName;
    private string mPrevHeadingName;
    #endregion
  }
}
