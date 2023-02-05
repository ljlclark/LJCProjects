// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using LJCGridDataLib;
using LJCDBViewDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCViewEditor
{
  // Provides ViewData methods for the ViewEditorList window.
  internal class ViewGridClass
  {
    #region Constructors

    // Initializes an object instance.
    internal ViewGridClass(ViewEditorList parent)
    {
      Parent = parent;
      ResetData();
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      mViewTableManager = Parent.ViewHelper.ViewTableManager;
      mViewDataManager = Parent.ViewHelper.ViewDataManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieveViewData(bool doRelated = true)
    {
      string tableName = Parent.TableCombo.Text;

      Parent.Cursor = Cursors.WaitCursor;
      Parent.ViewGrid.Rows.Clear();

      // *** Begin *** Add - 9/11
      Parent.DataGrid.Rows.Clear();
      Parent.DataGrid.Columns.Clear();
      // *** End   *** Add - 9/11

      // Check for existing ViewTable and write a new one if not found.
      ViewTable viewTable = mViewTableManager.RetrieveWithUniqueKey(tableName);
      if (null == viewTable)
      {
        ViewTable dataRecord = new ViewTable()
        {
          Name = tableName,
          Description = $"The {tableName} table."
        };
        viewTable = mViewTableManager.AddData(dataRecord);
      }

      ConfigureViewGrid();
      Views dataRecords = mViewDataManager.LoadWithParentID(viewTable.ID);
      if (dataRecords != null && dataRecords.Count > 0)
      {
        foreach (ViewData dataRecord in dataRecords)
        {
          RowAddViewData(dataRecord);
        }
      }
      Parent.Cursor = Cursors.Default;
      if (doRelated)
      {
        Parent.DoChange(ViewEditorList.Change.View);
      }
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAddViewData(ViewData dataRecord)
    {
      LJCGridRow retValue;

      retValue = Parent.ViewGrid.LJCRowAdd();
      SetStoredValuesViewData(retValue, dataRecord);
      Parent.ViewGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdateViewData(ViewData dataRecord)
    {
      LJCGridRow row;

      row = Parent.ViewGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        SetStoredValuesViewData(row, dataRecord);
        Parent.ViewGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValuesViewData(LJCGridRow row, ViewData dataRecord)
    {
      row.LJCSetInt32(ViewData.ColumnID, dataRecord.ID);
      row.LJCSetString(ViewData.ColumnName, dataRecord.Name);
    }

    // Selects a row based on the key record values.
    internal void RowSelectViewData(ViewData record)
    {
      int rowID;

      foreach (LJCGridRow row in Parent.ViewGrid.Rows)
      {
        rowID = row.LJCGetInt32(ViewData.ColumnID);
        if (rowID == record.ID)
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          Parent.ViewGrid.LJCSetCurrentRow(row, true);
          break;
        }
      }
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../LJCDocLib/Common/List.xml'/>
    internal void DoNewViewData()
    {
      ViewDataDetail detail;

      if (true == NetString.HasValue(Parent.TableCombo.Text))
      {
        string tableName = Parent.TableCombo.Text;
        ViewTable viewTable = mViewTableManager.RetrieveWithUniqueKey(tableName);

        var grid = Parent.ViewGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        detail = new ViewDataDetail
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
    internal void DoEditViewData()
    {
      ViewDataDetail detail;

      if (Parent.ViewGrid.CurrentRow is LJCGridRow row)
      {
        int id = row.LJCGetInt32(ViewData.ColumnID);
        string tableName = Parent.TableCombo.Text;
        ViewTable parentID = mViewTableManager.RetrieveWithUniqueKey(tableName);

        var grid = Parent.ViewGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        detail = new ViewDataDetail()
        {
          LJCID = id,
          LJCParentID = parentID.ID,
          LJCParentName = tableName,
          LJCLocation = location
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDeleteViewData()
    {
      string title;
      string message;

      if (Parent.ViewGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          var keyColumns = new DbColumns()
          {
            { ViewData.ColumnID, row.LJCGetInt32(ViewData.ColumnID) }
          };
          mViewDataManager.Delete(keyColumns);
          if (mViewDataManager.AffectedCount < 1)
          {
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
          else
          {
            Parent.ViewGrid.Rows.Remove(row);
            Parent.TimedChange(ViewEditorList.Change.View);
          }
        }
      }
    }

    // Refreshes the list.
    internal void DoRefreshViewData()
    {
      ViewData record;
      int id = 0;

      if (Parent.ViewGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(ViewData.ColumnID);
      }

      Parent.ViewGrid.Rows.Clear();
      DataRetrieveViewData(false);

      // Select the originally selected row.
      if (id > 0)
      {
        record = new ViewData()
        {
          ID = id
        };
        RowSelectViewData(record);
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      ViewDataDetail detail;
      ViewData record;
      LJCGridRow row;

      detail = sender as ViewDataDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdateViewData(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAddViewData(record);
        Parent.ViewGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.View);
      }
    }

    // Shows the Data.
    internal void DoShowData()
    {
      DbRequest dbRequest = Parent.DoGetViewRequest();
      if (dbRequest != null)
      {
        DataManager dataManager = new DataManager(Parent.DbServiceRef
          , Parent.DataConfigName, dbRequest.TableName);

        // Execute the dbRequest directly since it was retrieved.
        DbResult dbResult = dataManager.ExecuteRequest(dbRequest);
        if (DbResult.HasData(dbResult))
        {
          Parent.DataGrid.Columns.Clear();

          // Setup grid columns.
          ResultGridData resultGridData = new ResultGridData(Parent.DataGrid);
          resultGridData.SetDisplayColumns(dbResult.Columns);

          // Set Grid data.
          Parent.DataGrid.LJCAddDisplayColumns(resultGridData.DisplayColumns);
          resultGridData.LoadRows(dbResult);
        }
      }
    }

    // Shows the SQL statement.
    internal void DoShowSQL()
    {
      DbRequest dbRequest = Parent.DoGetViewRequest();
      if (dbRequest != null)
      {
        DbSqlBuilder dbSqlBuilder = new DbSqlBuilder(dbRequest);
        string sql = dbSqlBuilder.CreateLoadSql();
        if (null == Parent.mInfoWindow)
        {
          Parent.mInfoWindow = new InfoWindow
          {
            LJCInfoData = sql
          };
          Parent.mInfoWindow.LJCCloseEvent += Parent.InfoWindow_CloseEvent;
          Parent.mInfoWindow.Show(Parent);
        }
        else
        {
          sql = dbSqlBuilder.CreateUpdateSql();
          Parent.mInfoWindow.LJCInfoData = sql;
        }
      }
    }

    // Show the DbRequest code.
    internal void ShowCode()
    {
      DbRequest dbRequest = Parent.DoGetViewRequest();
      if (dbRequest != null)
      {
        string code = GenRequest.RequestCode(dbRequest);
        if (null == Parent.mInfoWindow)
        {
          Parent.mInfoWindow = new InfoWindow
          {
            LJCInfoData = code
          };
          Parent.mInfoWindow.LJCCloseEvent += Parent.InfoWindow_CloseEvent;
          Parent.mInfoWindow.Show(Parent);
        }
        else
        {
          Parent.mInfoWindow.LJCInfoData = code;
        }
      }
    }
    #endregion

    #region Setup Methods

    // Configures the ViewData Grid.
    private void ConfigureViewGrid()
    {
      if (0 == Parent.ViewGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string> {
          ViewData.ColumnName,
          ViewData.ColumnDescription
        };

        // Get the display columns from the manager Data Definition.
        DbColumns viewDisplayColumns = mViewDataManager.GetColumns(propertyNames);

        // Setup the grid display columns.
        Parent.ViewGrid.LJCAddDisplayColumns(viewDisplayColumns);
      }
    }
    #endregion

    #region Class Data

    private readonly ViewEditorList Parent;
    private ViewDataManager mViewDataManager;
    private ViewTableManager mViewTableManager;
    #endregion
  }
}
