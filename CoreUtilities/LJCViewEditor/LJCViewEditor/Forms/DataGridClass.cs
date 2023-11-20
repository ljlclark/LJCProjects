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
    internal DataGridClass(ViewEditorList parent)
    {
      Parent = parent;
      DataGrid = Parent.DataGrid;

      mUserID = "-null";
      TableName = null;
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
      DbRequest dbRequest = Parent.DoGetViewRequest();
      if (dbRequest != null)
      {
        DataManager dataManager = new DataManager(Parent.DbServiceRef
          , Parent.DataConfigName, dbRequest.TableName);

        SetKeyValues(dbRequest, dataManager.DataDefinition);

        // Execute the dbRequest directly since it was retrieved.
        DbResult dbResult = dataManager.ExecuteRequest(dbRequest);
        if (DbResult.HasData(dbResult))
        {
          // Get data.
          var dataColumns = dbResult.CreateResultColumns(dbResult);

          // Create and show DataDetail dialog.
          var dialog = new DataDetailDialog(mUserID, Parent.DataConfigName
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
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          DbRequest dbRequest = Parent.DoGetViewRequest();
          if (dbRequest != null)
          {
            dbRequest.RequestTypeName = "Delete";
            DataManager dataManager = new DataManager(Parent.DbServiceRef
            , Parent.DataConfigName, dbRequest.TableName);
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

    // Gets or sets the Table name.
    public string TableName { get; set; }

    private LJCDataGrid DataGrid { get; set; }
    #endregion

    #region Class Data

    private readonly string mUserID;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
