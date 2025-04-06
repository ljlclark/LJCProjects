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
using System.IO;

namespace LJCDataUtility
{
  // Provides DataColumnGrid methods for the DataUtilityList window.
  internal class DataColumnGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataColumnGridCode(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      TableGrid = ParentObject.TableGrid;
      ColumnGrid = ParentObject.ColumnGrid;
      ColumnMenu = ParentObject.ColumnMenu;
      Managers = ParentObject.Managers;
      ColumnManager = Managers.DataColumnManager;

      // Set Fonts
      var fontFamily = ParentObject.Font.FontFamily;
      var style = ParentObject.Font.Style;
      ColumnGrid.Font = new Font(fontFamily, 11, style);
      ColumnMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(ParentObject, ColumnGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(ColumnMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = ParentObject;
      list.ColumnNew.Click += ColumnNew_Click;
      list.ColumnEdit.Click += ColumnEdit_Click;
      list.ColumnDelete.Click += ColumnDelete_Click;
      list.ColumnRefresh.Click += ColumnRefresh_Click;
      list.ColumnHTML.Click += ColumnHTML_Click;
      list.ColumnExit.Click += list.Exit_Click;

      // Grid events.
      var grid = ColumnGrid;
      grid.KeyDown += ColumnGrid_KeyDown;
      grid.MouseDoubleClick += ColumnGrid_MouseDoubleClick;
      grid.MouseDown += ColumnGrid_MouseDown;
      grid.SelectionChanged += ColumnGrid_SelectionChanged;
      ParentObject.Cursor = Cursors.Default;
    }

    // Configures the DataColumn Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == ColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataUtilColumn.ColumnName,
          DataUtilColumn.ColumnDescription,
          DataUtilColumn.ColumnSequence,
          DataUtilColumn.ColumnTypeName,
          DataUtilColumn.ColumnMaxLength,
          DataUtilColumn.ColumnAllowNull,
          DataUtilColumn.ColumnDefaultValue
        };

        // Get the grid columns from the manager Data Definition.
        var manager = ColumnManager;
        var gridColumns = manager.Columns(propertyNames);

        // Setup the grid columns.
        ColumnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      ColumnGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow)
      {
        var parentID = ParentObject.DataTableRowID(out long parentSiteID);
        var keyColumns = ColumnManager.ParentKey(parentID, parentSiteID);
        var orderByNames = new List<string>()
        {
          DataUtilColumn.ColumnSequence
        };
        ColumnManager.Manager.OrderByNames = orderByNames;
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
      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Column);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataUtilColumn data)
    {
      var retValue = ColumnGrid.LJCRowAdd();
      SetStoredValues(retValue, data);
      retValue.LJCSetValues(ColumnGrid, data);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(long id)
    {
      bool retValue = false;

      if (id > 0)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ColumnGrid.Rows)
        {
          var rowID = ParentObject.DataColumnRowID(row);
          if (rowID == id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ColumnGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        ParentObject.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataUtilColumn data)
    {
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, data);
        row.LJCSetValues(ColumnGrid, data);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = ColumnGrid.CurrentRow != null;
      FormCommon.SetMenuState(ColumnMenu, enableNew, enableEdit);
      ParentObject.ColumnHeading.Enabled = true;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataUtilColumn dataRecord)
    {
      row.LJCSetInt64(DataUtilColumn.ColumnID
        , dataRecord.ID);
      row.LJCSetInt64(DataUtilColumn.ColumnDataSiteID, dataRecord.DataSiteID);
      row.LJCSetString(DataUtilColumn.ColumnName, dataRecord.Name);
    }
    #endregion

    #region Action Methods

    internal void ColumnTableHTML()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        var fileName = "Temp.html";
        var htmlTable = new ColumnHTMLTable(ParentObject, ColumnManager
          , fileName);

        var dataType = "DataObject";
        //dataType = "DataTable";
        dataType = "DbResult";
        string html = htmlTable.GetHTML(dataType);

        File.WriteAllText(fileName, html);
        ParentObject.Cursor = Cursors.Default;
        NetFile.ShellProgram(null, fileName);
      }
      ParentObject.Cursor = Cursors.Default;
    }

    // Deletes the selected row.
    internal void Delete()
    {
      bool isContinue = true;
      var row = ColumnGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (DialogResult.No == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          isContinue = false;
        }
      }

      if (isContinue)
      {
        var id = ParentObject.DataColumnRowID();
        var keyColumns = new DbColumns()
        {
          { DataUtilColumn.ColumnID, id }
        };
        ColumnManager.Delete(keyColumns);
        if (0 == ColumnManager.AffectedCount)
        {
          isContinue = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (isContinue)
      {
        ColumnGrid.Rows.Remove(row);
        SetControlState();
        ParentObject.TimedChange(Change.Column);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (TableGrid.CurrentRow is LJCGridRow
        && ColumnGrid.CurrentRow is LJCGridRow)
      {
        var id = ParentObject.DataColumnRowID();
        var parentID = ParentObject.DataTableRowID(out long parentSiteID);
        string parentName = ParentObject.DataTableRowName();
        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentSiteID = parentSiteID,
          LJCParentName = parentName,
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
        detail.Dispose();
      }
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        int sequence = ColumnGrid.Rows.Count + 1;
        var parentID = ParentObject.DataTableRowID(out long parentSiteID);
        string parentName = ParentObject.DataTableRowName();
        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail
        {
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentSiteID = parentSiteID,
          LJCParentName = parentName,
          LJCSequence = sequence
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
        detail.Dispose();
      }
    }

    // Refreshes the list.
    internal void Refresh()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      long id = 0;
      if (ColumnGrid.CurrentRow is LJCGridRow)
      {
        // Save the original row.
        id = ParentObject.DataColumnRowID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        RowSelect(id);
      }
      ParentObject.Cursor = Cursors.Default;
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
          ParentObject.TimedChange(Change.Column);
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void ColumnNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void ColumnEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void ColumnDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void ColumnRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }

    // Handles the Column HTML menu item event.
    private void ColumnHTML_Click(object sender, EventArgs e)
    {
      ColumnTableHTML();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid FontChange event.
    private void GridFont_FontChange(object sender, EventArgs e)
    {
      var text = ParentObject.Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = ParentObject.Text.Substring(0, index - 1);
      }
      var fontSize = GridFont.FontSize;
      ParentObject.Text = $"{text} [{fontSize}]";
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
            var menu = ParentObject.ColumnMenu;
            menu.Show(position);
            menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ParentObject.ColumnTabs.Select();
          }
          else
          {
            ParentObject.ColumnTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the Grid MouseDoubleClick event.
    private void ColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ColumnGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void ColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        if (ColumnGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ColumnGrid.LJCSetCurrentRow(e);
          SetControlState();
          ParentObject.TimedChange(Change.Column);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ColumnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ColumnGrid.LJCAllowSelectionChange)
      {
        SetControlState();
        ParentObject.TimedChange(Change.Column);
      }
      ColumnGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the parent List reference.
    private DataUtilityList ParentObject { get; set; }

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
