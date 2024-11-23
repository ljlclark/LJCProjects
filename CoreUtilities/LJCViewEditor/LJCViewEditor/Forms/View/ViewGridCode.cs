// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using LJCDBViewDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCViewEditor
{
  // Provides ViewData methods for the ViewEditorList window.
  internal class ViewGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ViewGridCode(ViewEditorList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      ViewEditorList = parentList;
      DataGrid = ViewEditorList.DataGrid;
      mInfoWindow = ViewEditorList.mInfoWindow;
      ViewGrid = ViewEditorList.ViewGrid;
      ResetData();
      parentList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = ViewEditorList.Managers;
      TableManager = Managers.ViewTableManager;
      ViewManager = Managers.ViewDataManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve(bool doRelated = true)
    {
      ViewEditorList.Cursor = Cursors.WaitCursor;
      ViewGrid.Rows.Clear();
      DataGrid.Rows.Clear();
      DataGrid.Columns.Clear();

      // Check for existing ViewTable and write a new one if not found.
      string tableName = ViewEditorList.TableCombo.Text;
      var viewTable = TableManager.RetrieveWithUniqueKey(tableName);
      if (null == viewTable)
      {
        var dataRecord = new ViewTable()
        {
          Name = tableName,
          Description = $"The {tableName} table."
        };
        viewTable = TableManager.AddData(dataRecord);
      }

      SetupGrid();
      var viewResult = ViewManager.ResultWithParentID(viewTable.ID);
      if (DbResult.HasRows(viewResult))
      {
        foreach (var dbRow in viewResult.Rows)
        {
          RowAddValues(dbRow.Values);
        }
      }
      ViewEditorList.Cursor = Cursors.Default;
      if (doRelated)
      {
        ViewEditorList.DoChange(ViewEditorList.Change.View);
      }
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewData dataRecord)
    {
      var retValue = ViewGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ViewGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = ViewGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewData.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      columnName = ViewData.ColumnName;
      var name = dbValues.LJCGetString(columnName);
      retValue.LJCSetString(columnName, name);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    internal bool RowSelect(ViewData dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        ViewEditorList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ViewGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewData.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ViewGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        ViewEditorList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewData dataRecord)
    {
      if (ViewGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ViewGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, ViewData dataRecord)
    {
      row.LJCSetInt32(ViewData.ColumnID, dataRecord.ID);
      row.LJCSetString(ViewData.ColumnName, dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../LJCGenDoc/Common/List.xml'/>
    internal void DoNew()
    {
      if (NetString.HasValue(ViewEditorList.TableCombo.Text))
      {
        // Data from list items.
        string tableName = ViewEditorList.TableCombo.Text;

        var viewTable = TableManager.RetrieveWithUniqueKey(tableName);
        var location = FormPoint.DialogScreenPoint(ViewGrid);
        var detail = new ViewDataDetail
        {
          LJCLocation = location,
          LJCParentID = viewTable.ID,
          LJCParentName = tableName
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (ViewGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewData.ColumnID);
        string tableName = ViewEditorList.TableCombo.Text;
        var viewTable = TableManager.RetrieveWithUniqueKey(tableName);

        var location = FormPoint.DialogScreenPoint(ViewGrid);
        var detail = new ViewDataDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCParentID = viewTable.ID,
          LJCParentName = tableName
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = ViewGrid.CurrentRow as LJCGridRow;
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

      //int id = 0;
      if (success)
      {
        var keyColumns = new DbColumns()
        {
          { ViewData.ColumnID, row.LJCGetInt32(ViewData.ColumnID) }
        };
        ViewManager.Delete(keyColumns);
        if (0 == ViewManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        ViewGrid.Rows.Remove(row);
        ViewEditorList.TimedChange(ViewEditorList.Change.View);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      ViewEditorList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ViewGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(ViewData.ColumnID);
      }
      ViewGrid.Rows.Clear();
      DataRetrieve(false);

      // Select the originally selected row.
      if (id > 0)
      {
        var record = new ViewData()
        {
          ID = id
        };
        RowSelect(record);
      }
      ViewEditorList.Cursor = Cursors.Default;
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewDataDetail;
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
          ViewGrid.LJCSetCurrentRow(row, true);
          ViewEditorList.TimedChange(ViewEditorList.Change.View);
        }
      }
    }

    // Shows the Data.
    internal void DoShowData()
    {
      var dbRequest = ViewEditorList.DoGetViewRequest();
      if (dbRequest != null)
      {
        var dataManager = new DataManager(ViewEditorList.DbServiceRef
          , ViewEditorList.DataConfigName, dbRequest.TableName);

        // Execute the dbRequest directly since it was retrieved.
        var dbResult = dataManager.ExecuteRequest(dbRequest);
        if (DbResult.HasData(dbResult))
        {
          DataGrid.Columns.Clear();

          // Setup grid columns.
          DataGrid.LJCAddColumns(dbResult.Columns);

          // Set Grid data.
          foreach (var dbRow in dbResult.Rows)
          {
            var gridRow = DataGrid.LJCRowAdd();
            gridRow.LJCSetValues(DataGrid, dbRow.Values);
          }
        }
      }
    }

    // Shows the SQL statement.
    internal void DoShowSQL()
    {
      var dbRequest = ViewEditorList.DoGetViewRequest();
      if (dbRequest != null)
      {
        var dbSqlBuilder = new DbSqlBuilder(dbRequest);
        string sql = dbSqlBuilder.CreateLoadSql();
        if (null == mInfoWindow)
        {
          mInfoWindow = new InfoWindow
          {
            LJCInfoData = sql
          };
          mInfoWindow.LJCCloseEvent += ViewEditorList.InfoWindow_CloseEvent;
          mInfoWindow.Show(ViewEditorList);
        }
        else
        {
          sql = dbSqlBuilder.CreateUpdateSql();
          mInfoWindow.LJCInfoData = sql;
          mInfoWindow.Show(ViewEditorList);
        }
      }
    }

    // Show the DbRequest code.
    internal void ShowCode()
    {
      var dbRequest = ViewEditorList.DoGetViewRequest();
      if (dbRequest != null)
      {
        string code = GenRequest.RequestCode(dbRequest);
        if (null == mInfoWindow)
        {
          mInfoWindow = new InfoWindow
          {
            LJCInfoData = code
          };
          mInfoWindow.LJCCloseEvent += ViewEditorList.InfoWindow_CloseEvent;
          mInfoWindow.Show(ViewEditorList);
        }
        else
        {
          mInfoWindow.LJCInfoData = code;
        }
      }
    }
    #endregion

    #region Other Methods

    // Configures the ViewData Grid.
    private void SetupGrid()
    {
      if (0 == ViewGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          ViewData.ColumnName,
          ViewData.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var viewGridColumns = ViewManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ViewGrid.LJCAddColumns(viewGridColumns);
      }
    }
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid DataGrid { get; set; }

    private ViewTableManager TableManager { get; set; }

    // Gets or sets the Parent List reference.
    private ViewEditorList ViewEditorList { get; set; }

    // Gets or sets the ViewData Grid reference.
    private LJCDataGrid ViewGrid { get; set; }

    // Gets or sets the Manager reference.
    private ViewDataManager ViewManager { get; set; }
    #endregion

    #region Class Data

    private InfoWindow mInfoWindow;
    #endregion
  }
}
