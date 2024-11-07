﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMapTableGridCode.cs
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
  // Provides DataColumnGrid methods for the _AppName_List window.
  internal class DataMapTableGridCode
  {
    #region Constructors

    // Initialize property values.
    internal DataMapTableGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      MapTableGrid = UtilityList.MapTableGrid;
      Managers = UtilityList.Managers;
      MapTableManager = Managers.DataMapTableManager;

      MapTableGrid.KeyDown += MapTableGrid_KeyDown;

      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      MapTableGrid.Font = new Font(fontFamily, 12, style);
      _ = new GridFont(UtilityList, MapTableGrid);
      UtilityList.Cursor = Cursors.Default;
    }

    private void MapTableGrid_KeyDown1(object sender, KeyEventArgs e)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      MapTableGrid.LJCRowsClear();

      var items = MapTableManager.Load();
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
    private LJCGridRow RowAdd(DataMapTable dataRecord)
    {
      var retValue = MapTableGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(MapTableGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DataMapTable dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in MapTableGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataMapTable.ColumnDataTableID);
          if (rowID == dataRecord.DataTableID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MapTableGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        UtilityList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataMapTable dataRecord)
    {
      row.LJCSetInt32(DataMapTable.ColumnDataTableID, dataRecord.DataTableID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool success = false;
      var row = MapTableGrid.CurrentRow as LJCGridRow;
      if (MapTableGrid.CurrentRow is LJCGridRow parentRow
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
        //MapTableManager.Delete(keyColumns);
        if (0 == MapTableManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        MapTableGrid.Rows.Remove(row);
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
      if (MapTableGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataMapTable.ColumnDataTableID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataMapTable()
        {
          DataTableID = id
        };
        RowSelect(record);
      }
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Setup and Other Methods

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == MapTableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataMapTable.ColumnDataTableID,
          DataMapTable.ColumnNewTableName
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = MapTableManager.GetColumns(propertyNames);

        // Setup the grid columns.
        MapTableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void MapTableGrid_KeyDown(object sender, KeyEventArgs e)
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
            var position = FormCommon.GetMenuScreenPoint(MapTableGrid
              , Control.MousePosition);
            UtilityList.MapTableMenu.Show(position);
            UtilityList.MapTableMenu.Select();
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

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid MapTableGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataMapTableManager MapTableManager { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
