using LJCDBMessage;
using LJCDocLibDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenDocScript
{
  // Represents the DocMethodGroupHeading script.
  internal class MethodGroupHeadingScript
  {
    // Initializes an object instance.
    internal MethodGroupHeadingScript()
    {
      var managers = CommonGenDocScript.GetManagers();
      mHeadingManager = managers.DocMethodGroupHeadingManager;
    }

    // Generates the script.
    internal void Gen()
    {
      var propertyNames = PropertyNames();
      mHeadingManager.SetOrderBy(OrderByNames());
      var result = mHeadingManager.Manager.Load(null, propertyNames);

      Console.WriteLine();
      Console.WriteLine("*****************************");
      Console.WriteLine("*** DocMethodGroupHeading ***");
      Console.WriteLine("*****************************");
      Console.WriteLine();
      var fileName = "13DocMethodGroupHeading.sql";
      File.WriteAllText(fileName, ScriptHeader());
      foreach (var row in result.Rows)
      {
        mHeadingValues = GetValues(row);
        StringBuilder builder = new StringBuilder(256);
        var name = mHeadingValues.Name;
        Console.WriteLine($"Heading: {name}");

        builder.Append($"exec sp_DMGHAddUnique '{name}',");
        builder.Append($" '{mHeadingValues.Heading}'");
        builder.AppendLine($"  , {mHeadingValues.Sequence}");
        var text = builder.ToString();
        File.AppendAllText(fileName, text);
      }
    }

    #region Private Methods

    // Gets the method values.
    private MethodGroupHeadingValues GetValues(DbRow row)
    {
      var retValue = new MethodGroupHeadingValues();

      var values = row.Values;
      retValue.Heading = values.LJCGetValue("Heading");
      retValue.Name = values.LJCGetValue("Name");
      retValue.Sequence = values.LJCGetInt16("Sequence");
      return retValue;
    }

    // Gets the order by names.
    private List<string> OrderByNames()
    {
      var retValue = new List<string>()
      {
        DocMethodGroupHeading.ColumnSequence
      };
      return retValue;
    }

    // Gets the property names.
    private List<string> PropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocMethodGroupHeading.ColumnName },
        { DocMethodGroupHeading.ColumnHeading },
        { DocMethodGroupHeading.ColumnSequence }
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 13DocMethodGroupHeading.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.Append("select ID 'DocMethodGroupHeading', Name, Heading,");
      builder.AppendLine(" Sequence, ActiveFlag");
      builder.AppendLine("from DocMethodGroupHeading");
      builder.AppendLine("order by Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocMethodGroupHeadingManager mHeadingManager;
    private MethodGroupHeadingValues mHeadingValues;
    #endregion
  }
}
