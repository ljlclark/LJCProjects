﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodScript.cs
using LJCDBMessage;
using LJCGenDocDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenDocScript
{
  // Represents the DocAssembly script.
  internal class AssemblyScript
  {
    // Initializes an object instance.
    internal AssemblyScript()
    {
      var managers = ValuesGenDoc.Instance.Managers;
      mAssemblyManager = managers.DocAssemblyManager;
    }

    // Generates the script.
    internal void Gen()
    {
      var propertyNames = PropertyNames();
      DbJoins assemblyJoins = Joins();
      mAssemblyManager.SetOrderBy(OrderByNames());
      var result = mAssemblyManager.Manager.Load(null, propertyNames, null
        , assemblyJoins);

      Console.WriteLine();
      Console.WriteLine("*******************");
      Console.WriteLine("*** DocAssembly ***");
      Console.WriteLine("*******************");
      var fileName = "05DocAssembly.sql";
      File.WriteAllText(fileName, ScriptHeader());
      mPrevGroupName = "Nothing";
      foreach (var row in result.Rows)
      {
        mAssemblyValues = GetValues(row);

        StringBuilder builder = new StringBuilder(256);
        builder.Append(GroupHeader());
        mPrevGroupName = mAssemblyValues.GroupName;
        var assemblyName = mAssemblyValues.Name;
        Console.WriteLine($"Assembly: {assemblyName}");

        builder.AppendLine($"exec sp_DAAddUnique @groupName, '{assemblyName}',");
        builder.AppendLine($"  '{mAssemblyValues.Description}',");
        builder.AppendLine($"  '{mAssemblyValues.FileSpec}',");
        builder.AppendLine($"  '{mAssemblyValues.MainImage}', @seq;");
        var text = builder.ToString();
        File.AppendAllText(fileName, text);
      }
    }

    #region Private Methods

    // Gets the values.
    private AssemblyValues GetValues(DbRow row)
    {
      var retValue = new AssemblyValues();

      var values = row.Values;
      var description = values.LJCGetString("Description");
      if (NetString.HasValue(description))
      {
        retValue.Description = description.Replace("\n", "");
      }
      retValue.FileSpec = values.LJCGetString("FileSpec");
      retValue.GroupName = values.LJCGetString("GroupName");
      retValue.MainImage = values.LJCGetString("MainImage");
      retValue.Name = values.LJCGetString("Name");
      retValue.Sequence = values.LJCGetInt32("Sequence");
      return retValue;
    }

    // Creates and returns the script class header.
    private string GroupHeader()
    {
      string retValue;

      // Assembly Group has changed.
      StringBuilder builder = new StringBuilder(256);
      var groupName = mAssemblyValues.GroupName;
      if (groupName != mPrevGroupName)
      {
        Console.WriteLine();
        Console.WriteLine($"Group: {groupName}");
        builder.AppendLine();
        builder.AppendLine($"set @groupName = '{groupName}';");
        builder.AppendLine("set @seq = 1;");
      }
      else
      {
        // Assembly Group has not changed.
        builder.AppendLine("set @seq += 1;");
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Creates and return the joins definition.
    private DbJoins Joins()
    {
      var retValue = new DbJoins();
      var join = new DbJoin()
      {
        TableAlias = "dag",
        TableName = DocAssemblyGroup.TableName,
        JoinOns = new DbJoinOns()
        {
          { DocAssembly.ColumnDocAssemblyGroupID, "ID" }
        },
        Columns = new DbColumns
        {
          // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
          { "Name", "GroupName", "GroupName" }
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
        $"dag.{DocAssemblyGroup.ColumnSequence}",
        $"DocAssembly.{DocAssembly.ColumnSequence}"
      };
      return retValue;
    }

    // Gets the property names.
    private List<string> PropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocAssembly.ColumnName },
        { DocAssembly.ColumnDescription },
        { DocAssembly.ColumnFileSpec },
        { DocAssembly.ColumnMainImage },
        { DocAssembly.ColumnSequence },
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 05DocAssembly.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.AppendLine("select DocAssembly.ID 'DocAssembly' , DocAssembly.Name 'Assembly Name',");
      builder.AppendLine("  dag.Name as GroupName, Description, FileSpec, MainImage");
      builder.AppendLine("from DocAssembly");
      builder.AppendLine("left join DocAssemblyGroup as dag on DocAssemblyGroupID = dag.ID");
      builder.AppendLine("order by dag.Sequence, DocAssembly.Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();
      builder.AppendLine("declare @groupName nvarchar(60);");
      builder.AppendLine("declare @seq int;");
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocAssemblyManager mAssemblyManager;
    private AssemblyValues mAssemblyValues;
    private string mPrevGroupName;
    #endregion
  }
}
