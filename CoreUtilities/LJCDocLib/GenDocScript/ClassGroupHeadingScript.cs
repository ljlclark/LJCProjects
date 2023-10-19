using LJCDBMessage;
using LJCDocLibDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenDocScript
{
  // Represents the DocClassHeadingGroup script.
  internal class ClassGroupHeadingScript
  {
    // Initializes an object instance.
    internal ClassGroupHeadingScript()
    {
      var managers = ValuesDocGen.Instance.Managers;
      mGroupManager = managers.DocClassGroupHeadingManager;
    }

    // Generates the script.
    internal void Gen()
    {
      var propertyNames = PropertyNames();
      mGroupManager.SetOrderBy(OrderByNames());
      var result = mGroupManager.Manager.Load(null, propertyNames);

      Console.WriteLine();
      Console.WriteLine("*** DocClassGroupHeading ***");
      Console.WriteLine();
      var fileName = "07DocClassGroupHeading.sql";
      File.WriteAllText(fileName, ScriptHeader());
      foreach (var row in result.Rows)
      {
        mHeadingValues = GetHeadingValues(row);
        StringBuilder builder = new StringBuilder(256);
        var name = mHeadingValues.Name;
        Console.WriteLine($"Heading: {name}");

        builder.Append($"exec sp_DCGHAddUnique '{name}',");
        builder.Append($" '{mHeadingValues.Heading}'");
        builder.AppendLine($"  , {mHeadingValues.Sequence}");
        var text = builder.ToString();
        File.AppendAllText(fileName, text);
      }
    }

    #region Private Methods

    // Gets the method values.
    private ClassGroupHeadingValues GetHeadingValues(DbRow row)
    {
      var retValue = new ClassGroupHeadingValues();

      var values = row.Values;
      retValue.Heading = values.LJCGetValue("Heading");
      retValue.Name = values.LJCGetValue("Name");
      retValue.Sequence = values.LJCGetInt16("Sequence");
      return retValue;
    }

    // Gets the method order by names.
    private List<string> OrderByNames()
    {
      var retValue = new List<string>()
      {
        DocClassGroupHeading.ColumnSequence
      };
      return retValue;
    }

    // Gets the assembly property names.
    private List<string> PropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocClassGroupHeading.ColumnName },
        { DocClassGroupHeading.ColumnHeading },
        { DocClassGroupHeading.ColumnSequence }
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 07DocClassGroupHeading.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.Append("select ID 'DocClassGroupHeading', Name, Heading,");
      builder.AppendLine(" Sequence, ActiveFlag");
      builder.AppendLine("from DocClassGroupHeading");
      builder.AppendLine("order by Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocClassGroupHeadingManager mGroupManager;
    private ClassGroupHeadingValues mHeadingValues;
    #endregion
  }
}
