// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataKeyGridCode.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides DataKeyGrid methods for the _AppName_List window.
  internal class DataKeyGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataKeyGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      KeyGrid = UtilityList.KeyGrid;
      Managers = UtilityList.Managers;
      KeyManager = Managers.DataKeyManager;

      KeyGrid.KeyDown += KeyGrid_KeyDown;

      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      KeyGrid.Font = new Font(fontFamily, 12, style);
      _ = new GridFont(UtilityList, KeyGrid);
      UtilityList.Cursor = Cursors.Default;
    }

    private void KeyGrid_KeyDown1(object sender, KeyEventArgs e)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      KeyGrid.LJCRowsClear();

      var items = KeyManager.Load();
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          RowAdd(item);
        }
      }
      UtilityList.Cursor = Cursors.Default;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataKey dataRecord)
    {
      var retValue = KeyGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(KeyGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DataKey dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in KeyGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataKey.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            KeyGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        UtilityList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataKey dataRecord)
    {
      row.LJCSetInt32(DataKey.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Setup and Other Methods

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == KeyGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataKey.ColumnDataTableID,
          DataKey.ColumnName,
          DataKey.ColumnKeyType,
          DataKey.ColumnSourceColumnName,
          DataKey.ColumnTargetTableName,
          DataKey.ColumnTargetColumnName
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = KeyManager.GetColumns(propertyNames);

        // Setup the grid columns.
        KeyGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool success = false;
      var row = KeyGrid.CurrentRow as LJCGridRow;
      if (KeyGrid.CurrentRow is LJCGridRow parentRow
        && row != null)
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
        var id = row.LJCGetInt32(DataTable.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataTable.ColumnID, id }
        };
        //KeyManager.Delete(keyColumns);
        if (0 == KeyManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        KeyGrid.Rows.Remove(row);
        //UtilityList.TimedChange(Change._ClassName_);
      }
    }

    // Shows the help page
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Refreshes the list.
    internal void Refresh()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (KeyGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataKey.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataKey()
        {
          ID = id
        };
        RowSelect(record);
      }
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void KeyGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          //Edit();
          e.Handled = true;
          break;

        case Keys.F1:
          //Help();
          e.Handled = true;
          break;

        case Keys.F5:
          Refresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(KeyGrid
              , Control.MousePosition);
            UtilityList.KeyMenu.Show(position);
            UtilityList.KeyMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            UtilityList.ljcTabControl1.Select();
          }
          else
          {
            UtilityList.ljcTabControl1.Select();
          }
          e.Handled = true;
          break;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Manager reference.
    private DataKeyManager KeyManager { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid KeyGrid { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
