// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConvertTable5.cs
using LJCDataAccessConfig5;
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCNetCommon5;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LJCDataUtility5
{
  // Provides methods to generate the InsertSelect procedure.
  internal class ConvertTable
  {
    #region Constructors

    // Initializes an object instance.
    internal ConvertTable(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      var configComboCode = ParentObject.ConfigComboCode;
      Config = configComboCode.DataConfigItem();
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the InsertSelect SQL.
    internal void ConvertTableProc()
    {
      // Cannot change Table Name, PK or FK columns?
      // Decrease Length: Check for truncation?
      // Decrease Int size: Check for truncation?

      var tableGridCode = ParentObject.TableGridCode;
      var id = tableGridCode.RowId(out short dbID);
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var insertColumns = Managers.TableDataColumns(dbID, id
        , orderByNames);

      while (true)
      {
        if (null == Config
          || !LJC.HasText(Config.Name)
          || !LJC.HasListItems(insertColumns))
        {
          break;
        }

        var connectionType = Config.ConnectionType;
        if (!LJC.HasText(connectionType))
        {
          // Default value.
          connectionType = "SQLServer";
        }

        var parentTableName = tableGridCode.RowName();
        if (null == parentTableName)
        {
          break;
        }

        var dataConfigName = Config.Name;
        string? showText = null;
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
        if (null == showText)
        {
          break;
        }

        var infoValue = ParentObject.InfoValue;
        var scriptWindow = new ShowInfoDialog();
        var controlValue = scriptWindow.ShowInfo(showText
          , "Insert Select SQL", infoValue);
        ParentObject.InfoValue = controlValue;
        break;
      }
    }

    // Generates the MySQL InsertSelect SQL.
    internal string? MyInsertSelect(string dataConfigName, string tableName
      , DataColumns insertColumns)
    {
      string? retSql = null;

      var manager = new LJCDataManager(dataConfigName, tableName);
      var selectColumns = manager.BaseDefinition;
      var columnLists = ColumnLists(insertColumns, selectColumns, "  "
        , "MySql");

      while (true)
      {
        if (null == columnLists)
        {
          break;
        }

        // Create new Table.
        //string toTableName = $"New{tableName}";
        //if (null == Config)
        //{
        //  break;
        //}

        //var myProc = new MyProcBuilder(ParentObject, Config.Database
        //  , toTableName);
        //var createTable = myProc.CreateTable(insertColumns);
        //LJCTextBuilder tb = new();
        //tb.Text(createTable);

        //tb.Line($"INSERT INTO `{toTableName}`");
        //tb.Line(columnLists.InsertList);

        //tb.Line("select");
        //tb.Line(columnLists.SelectList);
        //tb.Line($"FROM `{tableName}`;");
        //retSql = tb.ToString();
        break;
      }
      return retSql;
    }

    // Generates the SQLServer InsertSelect SQL.
    internal string? SQLInsertSelect(string dataConfigName, string tableName
      , DataColumns insertColumns)
    {
      string? retSql = null;

      var manager = new LJCDataManager(dataConfigName, tableName);
      var selectColumns = manager.BaseDefinition;
      var columnLists = ColumnLists(insertColumns, selectColumns, "  ");

      if (columnLists != null)
      {
        LJCTextBuilder tb = new();
        var dbName = "DatabaseName";
        if (Config != null
          && LJC.HasText(Config.Database))
        {
          dbName = Config.Database;
        }
        tb.Line($"USE [{dbName}]");

        // Create new Table.
        string toTableName = $"New{tableName}";
        var proc = new ProcBuilder(ParentObject, dbName, toTableName);
        var createTable = proc.CreateTable(insertColumns);
        tb.Text(createTable);

        tb.Line();
        tb.Line($"SET IDENTITY_INSERT {toTableName} ON");
        tb.Line($"INSERT INTO {toTableName}");
        tb.Line(columnLists.InsertList);

        tb.Line("select");
        tb.Line(columnLists.SelectList);
        tb.Line($"FROM {tableName};");
        tb.Line($"SET IDENTITY_INSERT {toTableName} OFF");
        retSql = tb.ToString();
      }
      return retSql;
    }

    // Creates the column lists.
    private ColumnLists? ColumnLists(DataColumns insertColumns
      , LJCDataColumns selectColumns, string? indent = null
      , string connectionType = "SqlServer")
    {
      ColumnLists? retLists = null;

      Indent = "";
      if (indent != null)
      {
        Indent = indent;
      }

      while (true)
      {
        if (!LJC.HasListItems(insertColumns)
          || !LJC.HasListItems(selectColumns))
        {
          break;
        }

        retLists = new ColumnLists();
        InsertBuilder = new LJCTextBuilder();
        SelectBuilder = new LJCTextBuilder();

        // Add beginning value.
        InsertBuilder.Text($"{Indent}(");
        SelectBuilder.Text(Indent);
        SelectBuilder.IsFirst = true;

        // Remove column.
        // Columns not in insertColumns will not be included.
        InsertBuilder.IsFirst = true;
        foreach (DataUtilColumn insertColumn in insertColumns)
        {
          // Get Insert list value.
          var insertName = insertColumn.Name;
          if (LJC.HasText(insertColumn.NewName))
          {
            // Rename column.
            insertName = insertColumn.NewName;
          }

          // Get Select list value.
          string selectValue = insertColumn.Name;
          var selectColumn = selectColumns[insertColumn.Name];
          if (null == selectColumn)
          {
            // Add column uses the default value.
            selectValue = DefaultValue(insertColumn); ;
          }

          // Null to not null.
          var useDefault = false;
          if (!insertColumn.AllowNull
            && selectColumn != null
            && selectColumn.AllowDBNull)
          {
            useDefault = true;
            var defaultValue = DefaultValue(insertColumn);
            selectValue = DefaultNameValue(selectValue, defaultValue);
            SelectBuilder.Line();
          }

          // Add the list values.
          if (0 == string.Compare(connectionType, "SqlServer", true))
          {
            InsertBuilder.Item(insertName);
            SelectBuilder.Item(selectValue);
          }
          else
          {
            InsertBuilder.Item($"`{insertName}`");
            SelectBuilder.Item($"`{selectValue}`");
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
        break;
      }
      return retLists;
    }

    // Gets the default ISNULL value.
    private static string DefaultNameValue(string selectName
      , string defaultValue)
    {
      string retValue;

      retValue = $", {selectName} = ISNULL({selectName}";
      retValue += $", {defaultValue})";
      return retValue;
    }

    // Get the default value.
    [SuppressMessage("Style", "IDE0066:Convert switch statement to expression"
      , Justification = "<Pending>")]
    private static string DefaultValue(DataUtilColumn insertColumn)
    {
      string retValue = "";

      if (insertColumn.DefaultValue != null)
      {
        retValue = insertColumn.DefaultValue;
      }
      if (!LJC.HasText(retValue))
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

    // Save *** Create custom InsertSelect list.
    // (No parens, NewName value, NewMaxLength value)
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

    // Gets or sets the DataConfig value.
    private LJCDataConfig? Config { get; set; }

    private string Indent { get; set; } = null!;

    private LJCTextBuilder InsertBuilder { get; set; } = null!;

    private int InsertLength { get; set; }

    private LJCTextBuilder SelectBuilder { get; set; } = null!;

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
    internal string InsertList { get; set; } = null!;

    /// <summary>The select column/value list.</summary>
    internal string SelectList { get; set; } = null!;
  }
}
