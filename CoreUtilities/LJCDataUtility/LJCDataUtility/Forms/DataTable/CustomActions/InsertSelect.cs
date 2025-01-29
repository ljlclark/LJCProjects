// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// InsertSelect.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides methods to generate the InsertSelect procedure.
  internal class InsertSelect
  {
    #region Constructors

    // Initializes an object instance.
    internal InsertSelect(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the InsertSelect SQL.
    internal void InsertSelectProc()
    {
      // Cannot change Table Name, PK or FK columns?
      // Decrease Length: Check for truncation?
      // Decrease Int size: Check for truncation?

      var tableID = ParentObject.DataTableID();
      var tableName = ParentObject.DataTableName();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var insertColumns = Managers.TableDataColumns(tableID
        , orderByNames);
      if (NetCommon.HasItems(insertColumns))
      {
        var dataConfigName = "DataUtility";
        var manager = new DataManager(dataConfigName, tableName);
        var selectColumns = manager.BaseDefinition;
        var columnLists = ColumnLists(insertColumns, selectColumns, "  ");

        TextBuilder b = new TextBuilder(256);
        string toTableName = $"New{tableName}";
        string dbName = ParentObject.DataConfigCombo.Text;
        b.Line($"USE [{dbName}]");

        // Create new Table.
        var proc = new ProcBuilder(ParentObject, dbName, toTableName);
        var createTable = proc.CreateTable(insertColumns);
        b.Text(createTable);

        b.Line($"SET IDENTITY_INSERT {toTableName} ON");
        b.Line($"INSERT INTO {toTableName}");
        b.Line(columnLists.InsertList);

        b.Line("select");
        b.Line(columnLists.SelectList);
        b.Line($"FROM {tableName};");
        b.Line($"SET IDENTITY_INSERT {toTableName} OFF");
        var showText = b.ToString();

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
          , "Insert Select SQL", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }

    // Adds a newline.
    private void AddInsertNewLine()
    {
      InsertBuilder.AppendLine();
      InsertLength = 0;
      var value = $"{Indent} ";
      InsertLength += value.Length;
      InsertBuilder.Append(value);
    }

    // Adds a newline.
    private void AddSelectNewLine()
    {
      SelectBuilder.AppendLine();
      SelectLength = 0;
      var value = $"{Indent} ";
      SelectLength += value.Length;
      SelectBuilder.Append(value);
    }

    // Appends a value.
    private void AppendInsert(string value)
    {
      InsertLength += value.Length;
      InsertBuilder.Append(value);
    }

    // Appends a value.
    private void AppendSelect(string value)
    {
      SelectLength += value.Length;
      SelectBuilder.Append(value);
    }

    // Creates the column lists.
    private ColumnLists ColumnLists(DataColumns insertColumns
      , DbColumns selectColumns, string indent = null)
    {
      ColumnLists retLists = null;

      Indent = indent;
      if (NetCommon.HasItems(insertColumns)
        && NetCommon.HasItems(selectColumns))
      {
        retLists = new ColumnLists();
        InsertBuilder = new StringBuilder(256);
        SelectBuilder = new StringBuilder(256);
        InsertLength = 0;
        SelectLength = 0;

        // Add beginning value.
        AppendInsert($"{indent}(");
        AppendSelect(indent);

        // Remove column.
        // Columns not in insertColumns will not be included.
        var isFirst = true;
        foreach (var insertColumn in insertColumns)
        {
          // Get Insert list value.
          var insertName = insertColumn.Name;
          if (NetString.HasValue(insertColumn.NewName))
          {
            // Rename column.
            insertName = insertColumn.NewName;
          }
          // Calculate length before adding to insert newline.
          InsertLength += insertName.Length;

          // Get Select list value.
          var selectName = insertColumn.Name;
          var selectColumn
            = selectColumns.LJCSearchColumnName(insertColumn.Name);
          if (null == selectColumn)
          {
            // Add column uses the default value.
            selectName = DefaultValue(insertColumn); ;
          }
          // Calculate length before adding to insert newline.
          SelectLength += selectName.Length;

          // Null to not null.
          var useDefault = false;
          if (!insertColumn.AllowNull
            && selectColumn != null
            && selectColumn.AllowDBNull)
          {
            useDefault = true;
            var defaultValue = DefaultValue(insertColumn);
            selectName = DefaultNameValue(selectName, defaultValue);
            SelectLength = 81;
          }

          // Add NewLine.
          if (InsertLength > 80)
          {
            AddInsertNewLine();
          }
          if (SelectLength > 80)
          {
            if (!HasNewLine(SelectBuilder))
            {
              AddSelectNewLine();
            }
          }

          // Add comma separator.
          if (!isFirst)
          {
            var value = ", ";
            AppendInsert(value);
            if (!useDefault)
            {
              AppendSelect(value);
            }
          }
          isFirst = false;

          // Add the list values.
          InsertBuilder.Append(insertName);
          SelectBuilder.Append(selectName);

          // Add newline after default.
          if (useDefault)
          {
            AddSelectNewLine();
          }
        }
        InsertBuilder.Append(")");
        retLists.InsertList = InsertBuilder.ToString();
        retLists.SelectList = SelectBuilder.ToString();
      }
      return retLists;
    }

    // Gets the default ISNULL value.
    private string DefaultNameValue(string selectName
      , string defaultValue)
    {
      string retValue;

      retValue = $", {selectName} = ISNULL({selectName}";
      retValue += $", {defaultValue})";
      return retValue;
    }

    // Get the default value.
    private string DefaultValue(DataUtilColumn insertColumn)
    {
      string retValue = null;

      if (insertColumn != null)
      {
        retValue = insertColumn.DefaultValue;
      }
      if (!NetString.HasValue(retValue))
      {
        switch (insertColumn.TypeName)
        {
          case "nvarchar":
          case "varchar":
            retValue = "''";
            break;

          default:
            retValue = "-1";
            break;
        }
      }
      return retValue;
    }

    // Checks if the list ends with a new line.
    private bool HasNewLine(StringBuilder builder)
    {
      var retValue = false;

      var list = builder.ToString();
      if (list.EndsWith($"\r\n{Indent} "))
      {
        retValue = true;
      }
      return retValue;
    }

    //// Create custom InsertSelect list.
    //// (No parens, NewName value, NewMaxLength value)
    //private string InsertSelectList(DataColumns selectColumns)
    //{
    //  var b = new StringBuilder(256);
    //  var value = "  ";
    //  b.Append(value);
    //  var insertLength = value.Length;

    //  var first = true;
    //  foreach (DataUtilColumn insertColumn in selectColumns)
    //  {
    //    var insertName = insertColumn.Name;
    //    insertLength += insertName.Length;

    //    // Calculate length before adding to insert newline.
    //    var useDefault = false;
    //    if ("Name" == insertName)
    //    {
    //      useDefault = true;
    //      insertName = "Name = ISNULL(NewName, Name)";
    //      insertLength = 81;
    //    }
    //    if ("MaxLength" == insertName)
    //    {
    //      useDefault = true;
    //      insertName = "MaxLength =\r\n";
    //      insertName += "      CASE NewMaxLength\r\n";
    //      insertName += "        WHEN 0 THEN MaxLength ELSE NewMaxLength\r\n";
    //      insertName += "      END";
    //      insertLength = 81;
    //    }

    //    if (insertLength > 80)
    //    {
    //      var newLine = "\r\n    ";
    //      b.Append(newLine);

    //      // Do not include crlf in length.
    //      insertLength = newLine.Length - 2;
    //      insertLength += insertName.Length;
    //    }

    //    if (!first)
    //    {
    //      var firstValue = ", ";
    //      b.Append(firstValue);
    //      insertLength += firstValue.Length;
    //    }
    //    first = false;

    //    b.Append(insertName);
    //    if (useDefault)
    //    {
    //      var newLine = "\r\n    ";
    //      b.Append(newLine);

    //      // Do not include crlf in length.
    //      insertLength = newLine.Length - 2;
    //    }
    //  }

    //  var retSelect = b.ToString();
    //  return retSelect;
    //}
    #endregion

    #region Properties

    private string Indent { get; set; }

    private StringBuilder InsertBuilder { get; set; }

    private int InsertLength { get; set; }

    private StringBuilder SelectBuilder { get; set; }

    private int SelectLength { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }

  /// <summary></summary>
  internal class ColumnLists
  {
    /// <summary>The insert column list.</summary>
    internal string InsertList { get; set; }

    /// <summary>The select column/value list.</summary>
    internal string SelectList { get; set; }
  }
}
