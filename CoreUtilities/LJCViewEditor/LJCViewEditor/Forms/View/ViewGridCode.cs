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
    internal ViewGridCode(ViewEditorList parent)
    {
      Parent = parent;
      DataGrid = Parent.DataGrid;
      mInfoWindow = Parent.mInfoWindow;
      ViewGrid = Parent.ViewGrid;
      ResetData();
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = Parent.Managers;
      mTableManager = Managers.ViewTableManager;
      mViewManager = Managers.ViewDataManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve(bool doRelated = true)
    {
      Parent.Cursor = Cursors.WaitCursor;
      ViewGrid.Rows.Clear();
      DataGrid.Rows.Clear();
      DataGrid.Columns.Clear();

      // Check for existing ViewTable and write a new one if not found.
      string tableName = Parent.TableCombo.Text;
      var viewTable = mTableManager.RetrieveWithUniqueKey(tableName);
      if (null == viewTable)
      {
        var dataRecord = new ViewTable()
        {
          Name = tableName,
          Description = $"The {tableName} table."
        };
        viewTable = mTableManager.AddData(dataRecord);
      }

      ConfigureViewGrid();
      var viewResult = mViewManager.ResultWithParentID(viewTable.ID);
      if (DbResult.HasRows(viewResult))
      {
        foreach (var dbRow in viewResult.Rows)
        {
          RowAddValues(dbRow.Values);
        }
      }
      Parent.Cursor = Cursors.Default;
      if (doRelated)
      {
        Parent.DoChange(ViewEditorList.Change.View);
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

    // Selects a row based on the key record values.
    internal void RowSelect(ViewData record)
    {
      foreach (LJCGridRow row in ViewGrid.Rows)
      {
        var rowID = row.LJCGetInt32(ViewData.ColumnID);
        if (rowID == record.ID)
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ViewGrid.LJCSetCurrentRow(row, true);
          break;
        }
      }
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../LJCGenDoc/Common/List.xml'/>
    internal void DoNew()
    {
      if (true == NetString.HasValue(Parent.TableCombo.Text))
      {
        // Data from list items.
        string tableName = Parent.TableCombo.Text;

        var viewTable = mTableManager.RetrieveWithUniqueKey(tableName);
        var location = FormCommon.GetDialogScreenPoint(ViewGrid);
        var detail = new ViewDataDetail
        {
          LJCParentID = viewTable.ID,
          LJCParentName = tableName,
          LJCLocation = location
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (ViewGrid.CurrentRow is LJCGridRow row)
      {
        int id = row.LJCGetInt32(ViewData.ColumnID);
        string tableName = Parent.TableCombo.Text;
        var viewTable = mTableManager.RetrieveWithUniqueKey(tableName);

        var location = FormCommon.GetDialogScreenPoint(ViewGrid);
        var detail = new ViewDataDetail()
        {
          LJCID = id,
          LJCParentID = viewTable.ID,
          LJCParentName = tableName,
          LJCLocation = location
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      if (ViewGrid.CurrentRow is LJCGridRow row)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          var keyColumns = new DbColumns()
          {
            { ViewData.ColumnID, row.LJCGetInt32(ViewData.ColumnID) }
          };
          mViewManager.Delete(keyColumns);
          if (mViewManager.AffectedCount < 1)
          {
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
          else
          {
            ViewGrid.Rows.Remove(row);
            Parent.TimedChange(ViewEditorList.Change.View);
          }
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      int id = 0;
      if (ViewGrid.CurrentRow is LJCGridRow row)
      {
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
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewDataDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        ViewGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.View);
      }
    }

    // Shows the Data.
    internal void DoShowData()
    {
      var dbRequest = Parent.DoGetViewRequest();
      if (dbRequest != null)
      {
        var dataManager = new DataManager(Parent.DbServiceRef
          , Parent.DataConfigName, dbRequest.TableName);

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
      var dbRequest = Parent.DoGetViewRequest();
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
          mInfoWindow.LJCCloseEvent += Parent.InfoWindow_CloseEvent;
          mInfoWindow.Show(Parent);
        }
        else
        {
          sql = dbSqlBuilder.CreateUpdateSql();
          mInfoWindow.LJCInfoData = sql;
        }
      }
    }

    // Show the DbRequest code.
    internal void ShowCode()
    {
      var dbRequest = Parent.DoGetViewRequest();
      if (dbRequest != null)
      {
        string code = GenRequest.RequestCode(dbRequest);
        if (null == mInfoWindow)
        {
          mInfoWindow = new InfoWindow
          {
            LJCInfoData = code
          };
          mInfoWindow.LJCCloseEvent += Parent.InfoWindow_CloseEvent;
          mInfoWindow.Show(Parent);
        }
        else
        {
          mInfoWindow.LJCInfoData = code;
        }
      }
    }
    #endregion

    #region Setup Methods

    // Configures the ViewData Grid.
    private void ConfigureViewGrid()
    {
      if (0 == ViewGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          ViewData.ColumnName,
          ViewData.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var viewGridColumns = mViewManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ViewGrid.LJCAddColumns(viewGridColumns);
      }
    }
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid DataGrid { get; set; }

    private LJCDataGrid ViewGrid { get; set; }
    #endregion

    #region Class Data

    private InfoWindow mInfoWindow;
    private readonly ViewEditorList Parent;
    private ViewDataManager mViewManager;
    private ViewTableManager mTableManager;
    #endregion
  }
}
