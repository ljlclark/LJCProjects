// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnGridCode.cs
using static LJCDataUtility.DataUtilityList;
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LJCDataUtility
{
  // Provides DataColumnGrid methods for the DataUtilityList window.
  internal class DataColumnGridCode
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataColumnGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      TableGrid = UtilityList.TableGrid;
      ColumnGrid = UtilityList.ColumnGrid;
      ColumnMenu = UtilityList.ColumnMenu;
      Managers = UtilityList.Managers;
      ColumnManager = Managers.DataColumnManager;

      // Set Fonts
      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      ColumnGrid.Font = new Font(fontFamily, 11, style);
      ColumnMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(UtilityList, ColumnGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(ColumnMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = UtilityList;
      list.ColumnNew.Click += ColumnNew_Click;
      list.ColumnEdit.Click += ColumnEdit_Click;
      list.ColumnDelete.Click += ColumnDelete_Click;
      list.ColumnRefresh.Click += ColumnRefresh_Click;
      list.ColumnExit.Click += list.Exit_Click;

      // Grid events.
      var grid = ColumnGrid;
      grid.KeyDown += ColumnGrid_KeyDown;
      grid.MouseDoubleClick += ColumnGrid_MouseDoubleClick;
      grid.MouseDown += ColumnGrid_MouseDown;
      grid.SelectionChanged += ColumnGrid_SelectionChanged;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      ColumnGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DataUtilTable.ColumnID);

        var keyColumns = ColumnManager.ParentKey(parentID);
        var items = ColumnManager.Load(keyColumns);
        if (NetCommon.HasItems(items))
        {
          foreach (var item in items)
          {
            RowAdd(item);
          }
        }
      }
      SetControlState();
      UtilityList.Cursor = Cursors.Default;
      UtilityList.DoChange(Change.Column);
    }

    // Adds a grid row and updates it with the record values.
    // ********************
    private LJCGridRow RowAdd(DataUtilityColumn dataRecord)
    {
      var retValue = ColumnGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ColumnGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(DataUtilityColumn dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ColumnGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataUtilityColumn.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ColumnGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        UtilityList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    // ********************
    private void RowUpdate(DataUtilityColumn dataRecord)
    {
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ColumnGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    // ********************
    private void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = ColumnGrid.CurrentRow != null;
      FormCommon.SetMenuState(ColumnMenu, enableNew, enableEdit);
      UtilityList.ColumnHeading.Enabled = true;
    }

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataUtilityColumn dataRecord)
    {
      row.LJCSetInt32(DataUtilityColumn.ColumnID
        , dataRecord.ID);
      row.LJCSetString(DataUtilityColumn.ColumnName
        , dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = ColumnGrid.CurrentRow as LJCGridRow;
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
        var id = row.LJCGetInt32(DataUtilityColumn.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataUtilityColumn.ColumnID, id }
        };
        ColumnManager.Delete(keyColumns);
        if (0 == ColumnManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        ColumnGrid.Rows.Remove(row);
        SetControlState();
        UtilityList.TimedChange(Change.Column);
      }
    }

    // Displays a detail dialog to edit a record.
    // ********************
    internal void Edit()
    {
      if (TableGrid.CurrentRow is LJCGridRow parentRow
        && ColumnGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        int id = row.LJCGetInt32(DataUtilityColumn.ColumnID);
        int parentID = parentRow.LJCGetInt32(DataUtilTable.ColumnID);
        string parentName = parentRow.LJCGetString(DataUtilTable.ColumnName);

        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail()
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
    // ********************
    internal void New()
    {
      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(DataUtilTable.ColumnID);
        string parentName = parentRow.LJCGetString(DataUtilTable.ColumnName);

        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail
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
    // ********************
    internal void Refresh()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataUtilityColumn.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataUtilityColumn()
        {
          ID = id
        };
        RowSelect(record);
      }
      UtilityList.Cursor = Cursors.Default;
    }

    // Shows the help page
    // ********************
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds new row or updates row with control values.
    // ********************
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as DataColumnDetail;
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
          ColumnGrid.LJCSetCurrentRow(row, true);
          SetControlState();
          UtilityList.TimedChange(Change.Column);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the DataColumn Grid.
    // ********************
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == ColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataUtilityColumn.ColumnDataTableID,
          DataUtilityColumn.ColumnName,
          DataUtilityColumn.ColumnDescription,
          DataUtilityColumn.ColumnSequence,
          DataUtilityColumn.ColumnTypeName,
          DataUtilityColumn.ColumnMaxLength
        };

        // Get the grid columns from the manager Data Definition.
        var manager = ColumnManager;
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        ColumnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    // ********************
    private void ColumnNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    // ********************
    private void ColumnEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    // ********************
    private void ColumnDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    // ********************
    private void ColumnRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid FontChange event.
    private void GridFont_FontChange(object sender, EventArgs e)
    {
      var text = UtilityList.Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = UtilityList.Text.Substring(0, index - 1);
      }
      var fontSize = GridFont.FontSize;
      UtilityList.Text = $"{text} [{fontSize}]";
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
    // ********************
    private void ColumnGrid_KeyDown(object sender, KeyEventArgs e)
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
            var position = FormPoint.MenuScreenPoint(ColumnGrid
              , Control.MousePosition);
            var menu = UtilityList.ColumnMenu;
            menu.Show(position);
            menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            UtilityList.MainTabs.Select();
          }
          else
          {
            UtilityList.MainTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the Grid MouseDoubleClick event.
    // ********************
    private void ColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ColumnGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    // ********************
    private void ColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ColumnGrid.Select();
        if (ColumnGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ColumnGrid.LJCSetCurrentRow(e);
          UtilityList.TimedChange(Change.Column);
        }
      }
    }

    // Handles the SelectionChanged event.
    // ********************
    private void ColumnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ColumnGrid.LJCAllowSelectionChange)
      {
        UtilityList.TimedChange(Change.Column);
      }
      ColumnGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the parent List reference.
    private DataUtilityList UtilityList { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ColumnGrid { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip ColumnMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataColumnManager ColumnManager { get; set; }

    // Provides the Grid font event handlers.
    private GridFont GridFont { get; set; }

    // Provides the Menu font event handlers.
    private MenuFont MenuFont { get; set; }
    #endregion
  }
}
