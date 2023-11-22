// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataGridClass.cs
using DataDetail;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System.Windows.Forms;

namespace LJCViewEditor
{
  /// <summary>Provides DataGrid methods for the ViewEditorList window.</summary>
  internal class DataGridClass
  {
    #region Constructors

    // Initializes an object instance.
    internal DataGridClass(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      DataGrid = EditList.DataGrid;
      mUserID = "-null";
      TableName = null;
      EditList.Cursor = Cursors.Default;
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      DbRequest dbRequest = EditList.DoGetViewRequest();
      if (dbRequest != null)
      {
        DataManager dataManager = new DataManager(EditList.DbServiceRef
          , EditList.DataConfigName, dbRequest.TableName);

        SetKeyValues(dbRequest, dataManager.DataDefinition);

        // Execute the dbRequest directly since it was retrieved.
        DbResult dbResult = dataManager.ExecuteRequest(dbRequest);
        if (DbResult.HasData(dbResult))
        {
          // Get data.
          var dataColumns = dbResult.CreateResultColumns(dbResult);

          // Create and show DataDetail dialog.
          var dialog = new DataDetailDialog(mUserID, EditList.DataConfigName
            , TableName)
          {
            LJCDataColumns = dataColumns
          };
          if (DialogResult.OK == dialog.ShowDialog())
          {
            //DbColumns resultColumns = dialog.LJCDataColumns;
          }
        }
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      if (DataGrid.CurrentRow is LJCGridRow row)
      {
        bool success = false;
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }

        if (success)
        {
          DbRequest dbRequest = EditList.DoGetViewRequest();
          if (dbRequest != null)
          {
            dbRequest.RequestTypeName = "Delete";
            DataManager dataManager = new DataManager(EditList.DbServiceRef
            , EditList.DataConfigName, dbRequest.TableName);
            SetKeyValues(dbRequest, dataManager.DataDefinition);
            //DbResult dbResult = dataManager.ExecuteRequest(dbRequest);

            DataGrid.Rows.Remove(row);
          }
        }
      }
    }
    #endregion

    #region Private Methods

    // Sets the DbRequest Key values.
    private void SetKeyValues(DbRequest dbRequest, DbColumns dataDefinition)
    {
      if (DataGrid.CurrentRow is LJCGridRow row)
      {
        foreach (DbColumn dbColumn in dataDefinition)
        {
          if (dbColumn.IsPrimaryKey)
          {
            if (null == dbRequest.KeyColumns)
            {
              dbRequest.KeyColumns = new DbColumns();
            }

            switch (dbColumn.DataTypeName)
            {
              case "Int64":
                dbColumn.Value = row.LJCGetInt64(dbColumn.ColumnName);
                break;

              case "Int32":
                dbColumn.Value = row.LJCGetInt32(dbColumn.ColumnName);
                break;

              case "String":
                dbColumn.Value = row.LJCGetString(dbColumn.ColumnName);
                break;
            }
            dbRequest.KeyColumns.Add(dbColumn);
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the DataGrid reference.
    private LJCDataGrid DataGrid { get; set; }

    // Gets or sets the Parent List reference.
    private ViewEditorList EditList { get; set; }

    // Gets or sets the Table name.
    public string TableName { get; set; }
    #endregion

    #region Class Data

    private readonly string mUserID;
    #endregion
  }
}
