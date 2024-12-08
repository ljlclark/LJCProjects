// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableGridCode.cs
using static LJCDataUtility.DataUtilityList;
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Data;
using System.IO;
using LJCDataUtility;

namespace LJCDataUtility
{
  // Provides DataTableGrid methods for the DataUtilityList window.
  internal class DataTableGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataTableGridCode(DataUtilityList parentList)
    {
      Parent = parentList;

      // Initialize property values.
      Parent.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      ModuleCombo = Parent.ModuleCombo;
      TableGrid = Parent.TableGrid;
      TableMenu = Parent.TableMenu;
      Managers = Parent.Managers;
      TableManager = Managers.DataTableManager;

      // Fonts
      var fontFamily = Parent.Font.FontFamily;
      var style = Parent.Font.Style;
      TableGrid.Font = new Font(fontFamily, 11, style);
      TableMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(Parent, TableGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(TableMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      Parent.TableNew.Click += TableNew_Click;
      Parent.TableEdit.Click += TableEdit_Click;
      Parent.TableDelete.Click += TableDelete_Click;
      Parent.TableRefresh.Click += TableRefresh_Click;
      Parent.TableAddDataProc.Click += TableAddDataProc_Click;
      Parent.TableCreateProc.Click += TableCreateProc_Click;
      Parent.TableInsertInto.Click += TableInsertInto_Click;
      Parent.TableExit.Click += Parent.Exit_Click;

      // Grid events.
      var grid = TableGrid;
      grid.KeyDown += TableGrid_KeyDown;
      grid.MouseDoubleClick += TableGrid_MouseDoubleClick;
      grid.MouseDown += TableGrid_MouseDown;
      grid.SelectionChanged += TableGrid_SelectionChanged;
      Parent.Cursor = Cursors.Default;
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == TableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataUtilTable.ColumnDataModuleID,
          DataUtilTable.ColumnName,
          DataUtilTable.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = TableManager.GetColumns(propertyNames);

        // Setup the grid columns.
        TableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      Parent.Cursor = Cursors.WaitCursor;
      TableGrid.LJCRowsClear();

      int parentID = ModuleCombo.LJCSelectedItemID();
      if (parentID > 0)
      {
        var keyColumns = TableManager.ParentKey(parentID);
        var items = TableManager.Load(keyColumns);
        if (NetCommon.HasItems(items))
        {
          foreach (var item in items)
          {
            RowAdd(item);
          }
        }
      }
      SetControlState();
      Parent.Cursor = Cursors.Default;
      Parent.DoChange(Change.Table);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataUtilTable dataRecord)
    {
      var retValue = TableGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(TableGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DataUtilTable dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        Parent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in TableGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataUtilTable.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            TableGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Parent.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataUtilTable dataRecord)
    {
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(TableGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = ModuleCombo.SelectedItem != null;
      bool enableEdit = TableGrid.CurrentRow != null;
      FormCommon.SetMenuState(TableMenu, enableNew, enableEdit);
      Parent.TableHeading.Enabled = true;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataUtilTable dataRecord)
    {
      row.LJCSetInt32(DataUtilTable.ColumnID
        , dataRecord.ID);
      row.LJCSetString(DataUtilTable.ColumnName
        , dataRecord.Name);
    }
    #endregion

    #region Common Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool success = false;
      var row = TableGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      if (success)
      {
        // Data from items.
        var id = row.LJCGetInt32(DataUtilTable.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataUtilTable.ColumnID, id }
        };
        TableManager.Delete(keyColumns);
        if (0 == TableManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        TableGrid.Rows.Remove(row);
        SetControlState();
        Parent.TimedChange(Change.Table);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (ModuleCombo.SelectedIndex >= 0
        && TableGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        int id = row.LJCGetInt32(DataUtilTable.ColumnID);
        int parentID = ModuleCombo.LJCSelectedItemID();
        string parentName = ModuleCombo.SelectedText;

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName,
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      if (ModuleCombo.SelectedItem != null)
      {
        // Data from list items.
        int parentID = ModuleCombo.LJCSelectedItemID();
        string parentName = ModuleCombo.SelectedText;

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail
        {
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    internal void Refresh()
    {
      Parent.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataUtilTable.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataUtilTable()
        {
          ID = id
        };
        RowSelect(record);
      }
      Parent.Cursor = Cursors.Default;
    }

    // Shows the help page
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds new row or updates row with control values.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as DataTableDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(record);
          TableGrid.LJCSetCurrentRow(row, true);
          SetControlState();
          Parent.TimedChange(Change.Table);
        }
      }
    }
    #endregion

    #region Custom Action Methods

    // Generates the Add data procedure.
    internal void AddDataProc()
    {
      string dbName = "LJCDataUtility";
      var tableID = Parent.DataTableID();
      var tableName = Parent.DataTableName();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , orderByNames);
      if (NetCommon.HasItems(dataColumns))
      {
        AddProcData procData;
        DataColumns parentColumns;
        string parentTableName;
        switch (tableName)
        {
          case "DataModule":
            procData = new AddProcData(dbName, dataColumns
              , tableName);
            CreateAddProc(procData);
            break;

          case "DataTable":
            parentTableName = "DataModule";
            parentColumns = Managers.TableDataColumns(tableID);
            procData = new AddProcData(dbName, dataColumns
              , tableName, parentColumns, parentTableName);
            CreateAddProc(procData);
            break;

          case "DataColumn":
            parentTableName = "DataUtilTable";
            parentColumns = Managers.TableDataColumns(tableID);
            procData = new AddProcData(dbName, dataColumns
              , tableName, parentColumns, parentTableName);
            CreateAddProc(procData);
            break;

          case "DataKey":
            parentTableName = "DataUtilTable";
            parentColumns = Managers.TableDataColumns(tableID);
            procData = new AddProcData(dbName, dataColumns
              , tableName, parentColumns, parentTableName);
            CreateAddProc(procData);
            break;
        }
      }
    }

    // Generates the create Table procedure.
    internal void CreateTableProc()
    {
      string dbName = "LJCDataUtility";
      var tableID = Parent.DataTableID();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , orderByNames);
      if (NetCommon.HasItems(dataColumns))
      {
        var tableName = Parent.DataTableName();
        var proc = new ProcBuilder(dbName, tableName);
        var primaryKeyList = "ID";
        var uniqueKeyList = "Name";
        var value = proc.CreateTableProc(dataColumns, primaryKeyList
          , uniqueKeyList);

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , infoValue.ControlName, infoValue);
        Parent.InfoValue = controlValue;
      }
    }

    // Create the Add data procedure.
    private string CreateAddProc(AddProcData data)
    {
      string retString = null;

      if (data.TableColumns != null)
      {
        var proc = new ProcBuilder(data.DBName, data.TableName);
        proc.Begin(proc.AddProcName);

        // Parameters
        var parentFindColumnName = "Name";
        var parmFindName = "";

        // Include parent table.
        var isFirst = true;
        if (NetCommon.HasItems(data.ParentColumns)
          && NetString.HasValue(data.ParentTableName))
        {
          // "@tableNameFindName"
          var typeValue = "nvarchar(60)";
          var findColumn
            = data.ParentColumns.LJCSearchUnique(parentFindColumnName);
          if (findColumn != null)
          {
            typeValue = findColumn.TypeName;
            if (findColumn.MaxLength > 0)
            {
              typeValue += $"({findColumn.MaxLength})";
            }
          }
          // *** Begin *** Change
          parmFindName = proc.SQLVarName(data.ParentTableName);
          parmFindName += parentFindColumnName;
          // *** End   *** Change
          proc.Text($"  {parmFindName} {typeValue}");
          isFirst = false;
        }

        var parameters = proc.Parameters(data.TableColumns
          , isFirst);
        proc.Line(parameters);

        proc.Line("AS");
        proc.Line("BEGIN");

        // Include Parent table.
        var parentIDColumnName = "ID";
        var varRefName = "";
        if (NetCommon.HasItems(data.ParentColumns)
          && NetString.HasValue(data.ParentTableName))
        {
          // *** Begin *** Change
          varRefName = proc.SQLVarName(data.ParentTableName);
          varRefName += parentIDColumnName;
          // *** End   *** Change
          var line = proc.IFItem(data.ParentTableName
            , parentIDColumnName, parentFindColumnName
            , parmFindName);
          line += "\r\n";
          line += $"IF {varRefName} IS NOT NULL";
          proc.Line(line);
        }

        // Table
        proc.Line($"IF NOT EXISTS(SELECT ID FROM {data.TableName}");
        proc.Line(" WHERE Name = @name)");
        proc.Line($"  INSERT INTO {data.TableName}");

        // Column list (Parens, not NewName, no IDs).
        var insertList = proc.ColumnsList(data.TableColumns);
        proc.Line(insertList);

        // Values list.
        var valuesList
          = proc.ValuesList(data.TableColumns, varRefName);
        proc.Line(valuesList);

        proc.Line("END");

        retString = proc.ToString();
      }

      var infoValue = Parent.InfoValue;
      var controlValue = DataUtilityCommon.ShowInfo(retString
        , infoValue.ControlName, infoValue);
      Parent.InfoValue = controlValue;
      return retString;
    }

    // Create the Insert Into SQL.
    private void InsertSelectSQL()
    {
      var tableID = Parent.DataTableID();
      var tableName = Parent.DataTableName();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(tableID
        , orderByNames);
      if (NetCommon.HasItems(dataColumns))
      {
        StringBuilder b = new StringBuilder(256);
        string dbName = "LJCDataUtility";
        string newTableName = $"New{tableName}";
        b.AppendLine($"USE [{dbName}]");
        b.AppendLine($"SET IDENTITY_INSERT {newTableName} ON");
        b.AppendLine();

        b.AppendLine($"INSERT INTO {newTableName}");
        var proc = new ProcBuilder(dbName, newTableName);
        // Parens, NewName, IDs.
        var insertList = proc.ColumnsList(dataColumns, true
          , true, true);
        insertList = insertList.Substring(2);
        b.AppendLine(insertList);

        b.AppendLine("select");
        // No parens, NewName value, NewMaxLength value.
        var selectList = InsertSelectList(dataColumns);
        b.AppendLine(selectList);
        b.AppendLine($"FROM {tableName};");

        b.AppendLine();
        b.AppendLine($"SET IDENTITY_INSERT {newTableName} OFF");
        var showText = b.ToString();

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
          , infoValue.ControlName, infoValue);
        Parent.InfoValue = controlValue;
      }
    }

    // Create custom InsertSelect list.
    // (No parens, NewName value, NewMaxLength value)
    private string InsertSelectList(DataColumns dataColumns)
    {
      var b = new StringBuilder(256);
      var value = "  ";
      b.Append(value);
      var lineLength = value.Length;

      var first = true;
      foreach (DataUtilColumn column in dataColumns)
      {
        var nameValue = column.Name;
        lineLength += nameValue.Length;

        // Calculate length before adding to insert newline.
        var addNewline = false;
        if ("Name" == nameValue)
        {
          addNewline = true;
          nameValue = "Name = ISNULL(NewName, Name)";
          lineLength = 81;
        }
        if ("MaxLength" == nameValue)
        {
          addNewline = true;
          nameValue = "MaxLength =\r\n";
          nameValue += "      CASE NewMaxLength\r\n";
          nameValue += "        WHEN 0 THEN MaxLength ELSE NewMaxLength\r\n";
          nameValue += "      END";
          lineLength = 81;
        }

        if (lineLength > 80)
        {
          var newLine = "\r\n    ";
          b.Append(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
          lineLength += nameValue.Length;
        }

        if (!first)
        {
          var firstValue = ", ";
          b.Append(firstValue);
          lineLength += firstValue.Length;
        }
        first = false;

        b.Append(nameValue);
        if (addNewline)
        {
          var newLine = "\r\n    ";
          b.Append(newLine);

          // Do not include crlf in length.
          lineLength = newLine.Length - 2;
        }
      }

      var retSelect = b.ToString();
      return retSelect;
    }
    #endregion

    #region Common Action Event Handlers

    // Handles the New menu item event.
    private void TableNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void TableEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void TableDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void TableRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Custom Action Event Handlers

    // Handles the Generate Add Procedure menu item event.
    private void TableAddDataProc_Click(object sender, EventArgs e)
    {
      AddDataProc();
    }

    // Handles the Generate Create Procedure menu item event.
    private void TableCreateProc_Click(object sender, EventArgs e)
    {
      CreateTableProc();
    }

    // Handles the Create Insert Into menu item event.
    private void TableInsertInto_Click(object sender, EventArgs e)
    {
      InsertSelectSQL();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid FontChange event.
    private void GridFont_FontChange(object sender, EventArgs e)
    {
      var text = Parent.Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = Parent.Text.Substring(0, index - 1);
      }
      var fontSize = GridFont.FontSize;
      Parent.Text = $"{text} [{fontSize}]";
    }

    // Handles the Menu FontChange event.
    private void MenuFont_FontChange(object sender, EventArgs e)
    {
      var menu = sender as ToolStripDropDownMenu;
      var text = menu.Items[0].Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = text.Substring(0, index - 1);
      }
      var fontSize = MenuFont.FontSize;
      menu.Items[0].Text = $"{text} [{fontSize}]";
    }

    // Handles the Grid KeyDown event.
    private void TableGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          Edit();
          e.Handled = true;
          break;

        case Keys.F1:
          ShowHelp();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormPoint.MenuScreenPoint(TableGrid
              , Control.MousePosition);
            Parent.TableMenu.Show(position);
            Parent.TableMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            Parent.ColumnTabs.Select();
          }
          else
          {
            Parent.ColumnTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the Grid MouseDoubleClick event.
    private void TableGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (TableGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void TableGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        TableGrid.Select();
        if (TableGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          TableGrid.LJCSetCurrentRow(e);
          Parent.TimedChange(Change.Table);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void TableGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (TableGrid.LJCAllowSelectionChange)
      {
        Parent.TimedChange(Change.Table);
      }
      TableGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList Parent { get; set; }

    // Gets or sets the parent Combo reference.
    private LJCItemCombo ModuleCombo { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip TableMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataTableManager TableManager { get; set; }

    // Provides the Grid font event handlers.
    private GridFont GridFont { get; set; }

    // Provides the menu font event handlers.
    private MenuFont MenuFont { get; set; }
    #endregion
  }
}
