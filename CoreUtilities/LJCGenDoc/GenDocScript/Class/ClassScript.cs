// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassScript.cs
using LJCDBMessage;
using LJCGenDocDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenDocScript
{
  // Represents the DocClass script.
  internal class ClassScript
  {
    // Initializes an object instance.
    internal ClassScript()
    {
      var managers = ValuesGenDoc.Instance.Managers;
      mClassManager = managers.DocClassManager;
    }

    // Generates the script.
    internal void Gen()
    {
      var propertyNames = PropertyNames();
      DbJoins classJoins = Joins();
      mClassManager.SetOrderBy(OrderByNames());
      var result = mClassManager.Manager.Load(null, propertyNames, null
        , classJoins);

      Console.WriteLine();
      Console.WriteLine("****************");
      Console.WriteLine("*** DocClass ***");
      Console.WriteLine("****************");
      var fileName = "11DocClass.sql";
      File.WriteAllText(fileName, ScriptHeader());
      mPrevAssemblyName = "Nothing";
      mPrevHeadingName = "Nothing";
      foreach (var row in result.Rows)
      {
        mClassValues = GetValues(row);

        StringBuilder builder = new StringBuilder(256);
        builder.Append(AssemblyHeader());
        builder.Append(GroupHeader());
        mPrevAssemblyName = mClassValues.AssemblyName;
        mPrevHeadingName = mClassValues.HeadingName;
        var className = mClassValues.Name;
        Console.WriteLine($"Class: {className}");

        builder.AppendLine($"exec sp_DCAddUnique @assemblyName, @headingName,");
        builder.AppendLine($"  '{className}',");
        builder.AppendLine($"  '{mClassValues.Description}',");
        builder.AppendLine($"  @seq;");
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
      var assemblyName = mClassValues.AssemblyName;
      if (assemblyName != mPrevAssemblyName)
      {
        Console.WriteLine();
        Console.WriteLine($"Assembly: {assemblyName}");
        mPrevHeadingName = "Nothing";
        StringBuilder builder = new StringBuilder(256);
        builder.AppendLine();
        builder.AppendLine($"/* {mClassValues.AssemblyName} */");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the script group header.
    private string GroupHeader()
    {
      string retValue;

      // Class Group has changed.
      StringBuilder builder = new StringBuilder(256);
      var assemblyName = mClassValues.AssemblyName;
      var headingName = mClassValues.HeadingName;
      if (headingName != mPrevHeadingName)
      {
        // Assembly has not changed.
        if (assemblyName == mPrevAssemblyName)
        {
          Console.WriteLine();
          builder.AppendLine();
        }
        Console.WriteLine($"Group: {headingName}");
        builder.AppendLine($"set @assemblyName = '{assemblyName}';");
        builder.AppendLine($"set @headingName = '{headingName}';");
        builder.AppendLine("set @seq = 1;");
      }
      else
      {
        // Class Group has not changed.
        builder.AppendLine("set @seq += 1;");
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Gets the values.
    private ClassValues GetValues(DbRow row)
    {
      var retValue = new ClassValues();

      var values = row.Values;
      retValue.AssemblyName = values.LJCGetString("AssemblyName");
      retValue.HeadingName = values.LJCGetString("HeadingName");
      var description = values.LJCGetString("Description");
      if (NetString.HasValue(description))
      {
        retValue.Description = description.Replace("\n", "");
      }
      retValue.Name = values.LJCGetString("Name");
      retValue.Sequence = values.LJCGetInt32("Sequence");
      return retValue;
    }

    // Creates and return the joins definition.
    private DbJoins Joins()
    {
      var retValue = new DbJoins();
      var join = new DbJoin()
      {
        TableAlias = "da",
        TableName = DocAssembly.TableName,
        JoinOns = new DbJoinOns()
        {
          { "DocAssemblyID", "ID" }
        },
        Columns = new DbColumns
        {
          // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
          { DocAssembly.ColumnName, "AssemblyName", "AssemblyName" }
        }
      };
      retValue.Add(join);

      join = new DbJoin()
      {
        TableAlias = "dcg",
        TableName = DocClassGroup.TableName,
        JoinOns = new DbJoinOns()
        {
          { DocClass.ColumnDocClassGroupID, "ID" }
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

    // Gets the order by names.
    private List<string> OrderByNames()
    {
      var retValue = new List<string>()
      {
        $"da.{DocAssembly.ColumnName}",
        //$"DocClass.{DocClass.ColumnName}",
        DocClassGroup.ColumnHeadingName,
        DocClass.ColumnSequence
      };
      return retValue;
    }

    // Gets the property names.
    private List<string> PropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocClass.ColumnName },
        { DocClass.ColumnDescription },
        { DocClass.ColumnSequence },
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 11DocClass.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.AppendLine("select dc.ID 'DocClass', da.Name 'Assembly Name', dcg.HeadingName, dc.Name,");
      builder.AppendLine("  dc.Description, dc.Sequence");
      builder.AppendLine("from DocClass as dc");
      builder.AppendLine("left join DocAssembly as da on DocAssemblyID = da.ID");
      builder.AppendLine("left join DocClassGroup as dcg on DocClassGroupID = dcg.ID");
      builder.AppendLine("order by da.Name, HeadingName, Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();

      builder.AppendLine("declare @assemblyName nvarchar(60);");
      builder.AppendLine("declare @headingName nvarchar(60);");
      builder.AppendLine("declare @seq int");
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocClassManager mClassManager;
    private ClassValues mClassValues;
    private string mPrevAssemblyName;
    private string mPrevHeadingName;
    #endregion
  }
}
