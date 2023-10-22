using LJCDBMessage;
using LJCDocLibDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenDocScript
{
  // Represents the DocMethodGroup script.
  internal class MethodGroupScript
  {
    // Initializes an object instance.
    internal MethodGroupScript()
    {
      var managers = ValuesDocGen.Instance.Managers;
      mGroupManager = managers.DocMethodGroupManager;
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
      Console.WriteLine("**********************");
      Console.WriteLine("*** DocMethodGroup ***");
      Console.WriteLine("**********************");
      var fileName = "15DocMethodGroup.sql";
      File.WriteAllText(fileName, ScriptHeader());
      mPrevClassName = "Nothing";
      foreach (var row in result.Rows)
      {
        mGroupValues = GetValues(row);
        StringBuilder builder = new StringBuilder(256);
        builder.Append(ClassHeader());
        var name = mGroupValues.ClassName;
        mPrevClassName = name;
        var headingName = mGroupValues.HeadingName;
        Console.WriteLine($"Heading: {headingName}");

        //builder.Append($"exec sp_DMGAddUnique '{name}',");
        builder.Append($"exec sp_DMGAddUnique @className,");
        builder.AppendLine($" '{headingName},'");
        builder.Append($" '{mGroupValues.HeadingTextCustom}',");
        builder.AppendLine($"  {mGroupValues.Sequence}");
        var text = builder.ToString();
        File.AppendAllText(fileName, text);
      }
    }

    #region Private Methods

    // Creates and returns the script assembly header.
    private string ClassHeader()
    {
      string retValue = null;

      // Assembly has changed.
      var className = mGroupValues.ClassName;
      if (className != mPrevClassName)
      {
        Console.WriteLine();
        Console.WriteLine($"Class: {className}");
        StringBuilder builder = new StringBuilder(256);
        builder.AppendLine();
        builder.AppendLine($"set @className= '{className}';");
        builder.AppendLine("/* ------------------------------ */");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Gets the values.
    private MethodGroupValues GetValues(DbRow row)
    {
      var retValue = new MethodGroupValues();

      var values = row.Values;
      retValue.ActiveFlag = values.LJCGetBoolean("ActiveFlag");
      retValue.ClassName = values.LJCGetValue("ClassName");
      retValue.HeadingName = values.LJCGetValue("HeadingName");
      retValue.HeadingTextCustom = values.LJCGetValue("HeadingTextCustom");
      retValue.Sequence = values.LJCGetInt32("Sequence");
      return retValue;
    }

    // Creates and return the joins definition.
    private DbJoins Joins()
    {
      var retValue = new DbJoins();
      var join = new DbJoin()
      {
        TableName = DocClass.TableName,
        JoinOns = new DbJoinOns()
        {
          { DocMethodGroup.ColumnDocClassID, DocClass.ColumnID }
        },
        Columns = new DbColumns
        {
          // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
          { DocClass.ColumnName, "ClassName", "ClassName" }
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
        DocMethodGroup.ColumnDocClassID,
        DocMethodGroup.ColumnSequence
      };
      return retValue;
    }

    // Gets the property names.
    private List<string> PropertyNames()
    {
      var retValue = new List<string>()
      {
        { DocMethodGroup.ColumnHeadingName },
        { DocMethodGroup.ColumnHeadingTextCustom },
        { DocMethodGroup.ColumnSequence },
        { DocMethodGroup.ColumnActiveFlag }
      };
      return retValue;
    }

    // Creates and returns the script header.
    private string ScriptHeader()
    {
      StringBuilder builder = new StringBuilder(256);
      builder.AppendLine("/* Copyright(c) Lester J.Clark and Contributors. */");
      builder.AppendLine("/* Licensed under the MIT License. */");
      builder.AppendLine("/* 15DocMethodGroup.sql */");
      builder.AppendLine("USE[LJCData]");
      builder.AppendLine("GO");
      builder.AppendLine("SET ANSI_NULLS ON");
      builder.AppendLine("GO");
      builder.AppendLine("SET QUOTED_IDENTIFIER ON");
      builder.AppendLine("GO");
      builder.AppendLine();
      builder.AppendLine("/*");
      builder.AppendLine("select DocMethodGroup.ID 'DocMethodGroup', Name 'Class Name', HeadingName,");
      builder.AppendLine("  HeadingTextCustom, DocMethodGroup.Sequence, DocMethodGroup.ActiveFlag");
      builder.AppendLine("from DocClassGroup");
      builder.AppendLine("left join DocClass on DocClassID = DocClass.ID");
      builder.AppendLine("order by DocClassID, DocMethodGroup.Sequence;");
      builder.AppendLine("*/");
      builder.AppendLine();
      builder.AppendLine("\r\ndeclare @className nvarchar(60);");
      builder.AppendLine();
      var retValue = builder.ToString();
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly DocMethodGroupManager mGroupManager;
    private MethodGroupValues mGroupValues;
    private string mPrevClassName;
    #endregion
  }
}
