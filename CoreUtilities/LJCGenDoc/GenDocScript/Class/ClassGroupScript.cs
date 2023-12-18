// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassGroupScript.cs
using LJCDBMessage;
using LJCGenDocDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenDocScript
{
  // Represents the DocClassGroup script.
  internal class ClassGroupScript
  {
    // Initializes an object instance.
    internal ClassGroupScript()
    {
      var managers = ValuesGenDoc.Instance.Managers;
      mGroupManager = managers.DocClassGroupManager;
    }

    // Generates the script.
    internal void Gen()
    {
      var propertyNames = PropertyNames();
      DbJoins groupJoins = Joins();
      mGroupManager.SetOrderBy(OrderByNames());
      var result = mGroupManager.Manager.Load(null, propertyNames
        , joins: groupJoins);

      Console.WriteLine();
      Console.WriteLine("*********************");
      Console.WriteLine("*** DocClassGroup ***");
      Console.WriteLine("*********************");
      var fileName = "09DocClassGroup.sql";
      File.WriteAllText(fileName, ScriptHeader());
      mPrevAssemblyName = "Nothing";
      foreach (var row in result.Rows)
      {
        mGroupValues = GetValues(row);
        StringBuilder builder = new StringBuilder(256);
        builder.Append(AssemblyHeader());
        var name = mGroupValues.AssemblyName;
        mPrevAssemblyName = name;
        var headingName = mGroupValues.HeadingName;
        Console.WriteLine($"Heading: {headingName}");

        //builder.Append($"exec sp_DCGAddUnique '{name}',");
        builder.Append("exec sp_DCGAddUnique @assemblyName,");
        builder.AppendLine($"  '{headingName}',");
        builder.Append($" '{mGroupValues.HeadingTextCustom}',");
        builder.AppendLine($"  {mGroupValues.Sequence}");
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
      var assemblyName = mGroupValues.AssemblyName;
      if (assemblyName != mPrevAssemblyName)
      {
        Console.WriteLine();
        Console.WriteLine($"Assembly: {assemblyName}");
        StringBuilder builder = new StringBuilder(256);
        builder.AppendLine();
        builder.AppendLine($"set @assemblyName= '{mGroupValues.AssemblyName}';");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Gets the values.
    private ClassGroupValues GetValues(DbRow row)
    {
      var retValue = new ClassGroupValues();

      var values = row.Values;
      retValue.ActiveFlag = values.LJCGetBoolean("ActiveFlag");
      retValue.AssemblyName = values.LJCGetString("AssemblyName");
      retValue.HeadingName = values.LJCGetString("HeadingName");
      retValue.HeadingTextCustom = values.LJCGetString("HeadingTextCustom");
      retValue.Sequence = values.LJCGetInt32("Sequence");
      return retValue;
    }

    // Creates and return the joins definition.
    private DbJoins Joins()
    {
      var retValue = new DbJoins();
      var join = new DbJoin()
      {
        TableName = DocAssembly.TableName,
        JoinOns = new DbJoinOns()
        {
          { DocClassGroup.ColumnDocAssemblyID, DocAssembly.ColumnID }
        },
        Columns = new DbColumns
        {
          // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
          { DocAssembly.ColumnName, "AssemblyName", "AssemblyName" }
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
        DocClassGroup.ColumnDocAssemblyID,
        DocClassGroup.ColumnSequence
      };
      return retValue;
    }

    // Gets the property names.
    private List<string> PropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocClassGroup.ColumnHeadingName },
        { DocClassGroup.ColumnHeadingTextCustom },
        { DocClassGroup.ColumnSequence },
        { DocClassGroup.ColumnActiveFlag }
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 09DocClassGroup.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.AppendLine("select DocClassGroup.ID 'DocClassGroup', Name 'Assembly Name', HeadingName,");
      builder.AppendLine("  HeadingTextCustom, DocClassGroup.Sequence, DocClassGroup.ActiveFlag");
      builder.AppendLine("from DocClassGroup");
      builder.AppendLine("left join DocAssembly on DocAssemblyID = DocAssembly.ID");
      builder.AppendLine("order by DocAssemblyID, DocClassGroup.Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();
      builder.AppendLine("declare @assemblyName nvarchar(60);");
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocClassGroupManager mGroupManager;
    private ClassGroupValues mGroupValues;
    private string mPrevAssemblyName;
    #endregion
  }
}
