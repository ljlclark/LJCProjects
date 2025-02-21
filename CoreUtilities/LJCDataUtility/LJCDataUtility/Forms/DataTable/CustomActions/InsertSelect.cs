// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// InsertSelect.cs
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
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

      var parentID = ParentObject.DataTableID(out long parentSiteID);
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var insertColumns = Managers.TableDataColumns(parentID, parentSiteID
        , orderByNames);
      if (NetCommon.HasItems(insertColumns))
      {
        var parentTableName = ParentObject.DataTableName();

        //var dataConfigName = "DataUtility";
        var configCombo = ParentObject.DataConfigCombo;
        var dataConfig = configCombo.SelectedItem as DataConfig;
        var dataConfigName = dataConfig.Name;

        var connectionType = ParentObject.ConnectionType;
        if (!NetString.HasValue(connectionType))
        {
          // Default value.
          connectionType = "SQLServer";
        }

        // Testing
        if (DialogResult.Yes == MessageBox.Show("Use MySQL?", "MySQL"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          connectionType = "MySQL";
        }

        string showText = null;
        switch (connectionType.ToLower())
        {
          case "mysql":
            showText = MyInsertSelect(dataConfigName, parentTableName
              , insertColumns);
            break;

          case "sqlserver":
            showText = SQLInsertSelect(dataConfigName, parentTableName
              , insertColumns);
            break;
        }

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
          , "Insert Select SQL", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }

    // Generates the MySQL InsertSelect SQL.
    internal string MyInsertSelect(string dataConfigName, string tableName
      , DataColumns insertColumns)
    {
      var manager = new DataManager(dataConfigName, tableName);
      var selectColumns = manager.BaseDefinition;
      var columnLists = ColumnLists(insertColumns, selectColumns, "  "
        , "MySql");

      TextBuilder b = new TextBuilder(256);
      string toTableName = $"New{tableName}";
      var dbName = ParentObject.ComboConfigValue("Database");

      // Create new Table.
      var myProc = new MyProcBuilder(ParentObject, dbName, toTableName);
      var createTable = myProc.CreateTable(insertColumns);
      b.Text(createTable);

      b.Line($"INSERT INTO `{toTableName}`");
      b.Line(columnLists.InsertList);

      b.Line("select");
      b.Line(columnLists.SelectList);
      b.Line($"FROM `{tableName}`;");
      var retSQL = b.ToString();
      return retSQL;
    }

    // Generates the SQLServer InsertSelect SQL.
    internal string SQLInsertSelect(string dataConfigName, string tableName
      , DataColumns insertColumns)
    {
      var manager = new DataManager(dataConfigName, tableName);
      var selectColumns = manager.BaseDefinition;
      var columnLists = ColumnLists(insertColumns, selectColumns, "  ");

      TextBuilder b = new TextBuilder(256);
      string toTableName = $"New{tableName}";
      var dbName = ParentObject.ComboConfigValue("Database");
      b.Line($"USE [{dbName}]");

      // Create new Table.
      var proc = new ProcBuilder(ParentObject, dbName, toTableName);
      var createTable = proc.CreateTable(insertColumns);
      b.Text(createTable);

      b.Line();
      b.Line($"SET IDENTITY_INSERT {toTableName} ON");
      b.Line($"INSERT INTO {toTableName}");
      b.Line(columnLists.InsertList);

      b.Line("select");
      b.Line(columnLists.SelectList);
      b.Line($"FROM {tableName};");
      b.Line($"SET IDENTITY_INSERT {toTableName} OFF");
      var retSQL = b.ToString();
      return retSQL;
    }

    // Creates the column lists.
    private ColumnLists ColumnLists(DataColumns insertColumns
      , DbColumns selectColumns, string indent = null
      , string connectionType = "SqlServer")
    {
      ColumnLists retLists = null;

      Indent = indent;
      if (NetCommon.HasItems(insertColumns)
        && NetCommon.HasItems(selectColumns))
      {
        retLists = new ColumnLists();
        InsertBuilder = new TextBuilder(256)
        {
          NewLinePrefix = $"{Indent} "
        };
        SelectBuilder = new TextBuilder(256)
        {
          NewLinePrefix = $"{Indent} "
        };

        // Add beginning value.
        InsertBuilder.Text($"{indent}(");
        SelectBuilder.Text(indent);

        // Remove column.
        // Columns not in insertColumns will not be included.
        InsertBuilder.IsFirst = true;
        foreach (var insertColumn in insertColumns)
        {
          // Get Insert list value.
          var insertName = insertColumn.Name;
          if (NetString.HasValue(insertColumn.NewName))
          {
            // Rename column.
            insertName = insertColumn.NewName;
          }

          // Get Select list value.
          var selectName = insertColumn.Name;
          var selectColumn
            = selectColumns.LJCSearchColumnName(insertColumn.Name);
          if (null == selectColumn)
          {
            // Add column uses the default value.
            selectName = DefaultValue(insertColumn); ;
          }

          // Null to not null.
          var useDefault = false;
          if (!insertColumn.AllowNull
            && selectColumn != null
            && selectColumn.AllowDBNull)
          {
            useDefault = true;
            var defaultValue = DefaultValue(insertColumn);
            selectName = DefaultNameValue(selectName, defaultValue);
            SelectBuilder.Line();
          }

          // Add the list values.
          if ("sqlserver" == connectionType.ToLower())
          {
            InsertBuilder.AddExpanded(insertName);
            SelectBuilder.AddExpanded(selectName);
          }
          else
          {
            InsertBuilder.AddExpanded($"`{insertName}`");
            SelectBuilder.AddExpanded($"`{selectName}`");
          }

          // Add newline after default.
          if (useDefault)
          {
            SelectBuilder.Line();
          }
        }
        InsertBuilder.Text(")");
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

    //// Save *** Create custom InsertSelect list.
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

    private TextBuilder InsertBuilder { get; set; }

    private int InsertLength { get; set; }

    private TextBuilder SelectBuilder { get; set; }

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
