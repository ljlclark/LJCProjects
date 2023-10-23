// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// AssemblyGroupScript.cs
using LJCDBMessage;
using LJCDocLibDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenDocScript
{
  // Represents the DocAssemblyGroup script.
  internal class AssemblyGroupScript
  {
    // Initializes an object instance.
    internal AssemblyGroupScript()
    {
      var managers = CommonGenDocScript.GetManagers();
      mGroupManager = managers.DocAssemblyGroupManager;
    }

    // Generates the script.
    internal void Gen()
    {
      var propertyNames = PropertyNames();
      mGroupManager.SetOrderBy(OrderByNames());
      var result = mGroupManager.Manager.Load(null, propertyNames);

      Console.WriteLine();
      Console.WriteLine("************************");
      Console.WriteLine("*** DocAssemblyGroup ***");
      Console.WriteLine("************************");
      Console.WriteLine();
      var fileName = "03DocAssemblyGroup.sql";
      File.WriteAllText(fileName, ScriptHeader());
      foreach (var row in result.Rows)
      {
        mGroupValues = GetValues(row);
        StringBuilder builder = new StringBuilder(256);
        var name = mGroupValues.Name;
        Console.WriteLine($"Assembly: {name}");

        builder.Append($"exec sp_DAGAddUnique '{name}',");
        builder.Append($" '{mGroupValues.Heading}'");
        builder.AppendLine($"  , {mGroupValues.Sequence}");
        var text = builder.ToString();
        File.AppendAllText(fileName, text);
      }
    }

    #region Private Methods

    // Gets the values.
    private AssemblyGroupValues GetValues(DbRow row)
    {
      var retValue = new AssemblyGroupValues();

      var values = row.Values;
      retValue.ActiveFlag = values.LJCGetBoolean("ActiveFlag");
      retValue.Heading = values.LJCGetValue("Heading");
      retValue.Name = values.LJCGetValue("Name");
      retValue.Sequence = values.LJCGetInt32("Sequence");
      return retValue;
    }

    // Gets the order by names.
    private List<string> OrderByNames()
    {
      var retValue = new List<string>()
      {
        DocAssemblyGroup.ColumnSequence
      };
      return retValue;
    }

    // Gets the property names.
    private List<string> PropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocAssemblyGroup.ColumnName },
        { DocAssemblyGroup.ColumnHeading },
        { DocAssemblyGroup.ColumnSequence },
        { DocAssemblyGroup.ColumnActiveFlag }
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 03DocAssemblyGroup.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.AppendLine("select ID 'DocAssemblyGroup', Name, Heading, Sequence, ActiveFlag");
      builder.AppendLine("from DocAssemblyGroup");
      builder.AppendLine("order by Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocAssemblyGroupManager mGroupManager;
    private AssemblyGroupValues mGroupValues;
    #endregion
  }
}
