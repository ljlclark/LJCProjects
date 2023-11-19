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
      Managers = new ManagersDbView();
      Managers.SetDbProperties(Parent.Managers.DbServiceRef
        , Parent.Managers.DataConfigName);
      mViewTableManager = Managers.ViewTableManager;
      mViewDataManager = Managers.ViewDataManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve(bool doRelated = true)
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

      // *** Begin *** Change- 10/5/23
      var manager = mViewDataManager;
      var result = manager.ResultWithParentID(viewTable.ID);
      if (DbResult.HasRows(result))
      {
        foreach (DbRow dbRow in result.Rows)
        {
          RowAddValues(dbRow.Values);
        }
      }
      // *** End   *** Change- 10/5/23
      Parent.Cursor = Cursors.Default;
      if (doRelated)
      {
        Parent.DoChange(ViewEditorList.Change.View);
      }
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewData dataRecord)
    {
      LJCGridRow retValue;

      retValue = Parent.ViewGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(Parent.ViewGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = Parent.ViewGrid;
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
      LJCGridRow row;

      row = Parent.ViewGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(Parent.ViewGrid, dataRecord);
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
    /// <include path='items/DoNew/*' file='../../LJCGenDoc/Common/List.xml'/>
    internal void DoNew()
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
    internal void DoEdit()
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
    internal void DoDelete()
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
    internal void DoRefresh()
    {
      ViewData record;
      int id = 0;

      if (Parent.ViewGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(ViewData.ColumnID);
      }

      Parent.ViewGrid.Rows.Clear();
      DataRetrieve(false);

      // Select the originally selected row.
      if (id > 0)
      {
        record = new ViewData()
        {
          ID = id
        };
        RowSelect(record);
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
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
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
          Parent.DataGrid.LJCAddColumns(dbResult.Columns);

          // Set Grid data.
          foreach (DbRow dbRow in dbResult.Rows)
          {
            var gridRow = Parent.DataGrid.LJCRowAdd();
            gridRow.LJCSetValues(Parent.DataGrid, dbRow.Values);
          }
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

        // Get the grid columns from the manager Data Definition.
        DbColumns viewGridColumns = mViewDataManager.GetColumns(propertyNames);

        // Setup the grid columns.
        Parent.ViewGrid.LJCAddColumns(viewGridColumns);
      }
    }
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }
    #endregion

    #region Class Data

    private readonly ViewEditorList Parent;
    private ViewDataManager mViewDataManager;
    private ViewTableManager mViewTableManager;
    #endregion
  }
}
