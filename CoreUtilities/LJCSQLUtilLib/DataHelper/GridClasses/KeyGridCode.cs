// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// KeyGridCode.cs
//using LJCDataDetailLib;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using LJCSQLUtilLibDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using DataDetail;
using System;
using System.Windows.Forms;

namespace DataHelper
{
  // Contains the Grid methods.
  internal class KeyGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal KeyGridCode(MainList parent)
    {
      // Set default class data.
      mParent = parent;
      mManagers = mParent.Managers;
      // ToDo: Convert to new DataDetailDialog.
      //mConfigRows = ControlRows.Deserialize(ConfigRowFileName);
      mDataConfigName = mManagers.DbMetaDataKeyManager.DataConfigName;

      mUserID = "Les";
      mTableName = "ColumnGrid";
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieveKey()
    {
      DbMetaDataKeys records;
      DbMetaDataKeys foreignRecords;

      mParent.Cursor = Cursors.WaitCursor;
      mParent.KeyGrid.LJCRowsClear();

      var dataKeyManager = mManagers.DbMetaDataKeyManager;
      if (mParent.ColumnGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DbMetaDataColumn.ColumnID);

        records = dataKeyManager.LoadPrimaryKeys();
        foreignRecords = dataKeyManager.LoadForeignKey(parentID);
        if (foreignRecords != null)
        {
          foreach (DbMetaDataKey foreignRecord in foreignRecords)
          {
            records.Add(foreignRecord);
          }
        }

        if (records != null && records.Count > 0)
        {
          foreach (DbMetaDataKey record in records)
          {
            RowAddKey(record);
          }
        }
      }
      mParent.Cursor = Cursors.Default;
      mParent.DoChange(MainList.ChangeKey);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAddKey(DbMetaDataKey record)
    {
      LJCGridRow retValue;

      retValue = mParent.KeyGrid.LJCRowAdd();
      SetStoredValuesKey(retValue, record);

      // Sets the row values from a data object.
      mParent.KeyGrid.LJCRowSetValues(retValue, record);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdateKey(DbMetaDataKey record)
    {
      if (mParent.KeyGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesKey(row, record);
        mParent.KeyGrid.LJCRowSetValues(row, record);
      }
    }

    // Sets the row stored values.
    private void SetStoredValuesKey(LJCGridRow row, DbMetaDataKey record)
    {
      row.LJCSetInt32(DbMetaDataKey.ColumnID, record.ID);
    }

    // Selects a row based on the key record values.
    private bool RowSelectKey(DbMetaDataKey record)
    {
      int rowID;
      bool retValue = false;

      if (record != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mParent.KeyGrid.Rows)
        {
          rowID = row.LJCGetInt32(DbMetaDataKey.ColumnID);
          if (rowID == record.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mParent.KeyGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mParent.Cursor = Cursors.Default;
      }
      return retValue;
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNewKey()
    {
      DbColumns dataColumns = mManagers.DbMetaDataKeyManager.DataDefinition;

      // ToDo: Convert to new DataDetailDialog.
      var detail = new DataDetailDialog(mUserID, mDataConfigName, mTableName)
      {
        //LJCConfigRows = mConfigRows,
        LJCDataColumns = dataColumns,
        LJCIsUpdate = false
      };
      detail.LJCChange += KeyDetail_Change;
      if (DialogResult.OK == detail.ShowDialog())
      {
        //mConfigRows = detail.LJCConfigRows;
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEditKey()
    {
      if (mParent.ColumnGrid.CurrentRow is LJCGridRow parentRow
        && mParent.KeyGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        int id = row.LJCGetInt32(DbMetaDataKey.ColumnID);
        int parentID = parentRow.LJCGetInt32(DbMetaDataTable.ColumnID);

        // Retrieve Data
        var keyColumns = new DbColumns()
        {
          { DbMetaDataKey.ColumnID, id }
        };
        DataManager dataKeyManager = mManagers.DbMetaDataKeyManager.DataManager;
        DbResult dbResult = dataKeyManager.Retrieve(keyColumns);
        if (DbResult.HasData(dbResult))
        {
          // The Data Definition and Record values are merged. 
          DbColumns dataColumns = dbResult.GetValueColumns();

          // ToDo: Convert to new DataDetailDialog.
          var detail = new DataDetailDialog(mUserID, mDataConfigName, mTableName)
          {
            //LJCConfigRows = mConfigRows,
            LJCDataColumns = dataColumns,
            LJCIsUpdate = true
          };
          detail.LJCChange += KeyDetail_Change;
          if (DialogResult.OK == detail.ShowDialog())
          {
            //mConfigRows = detail.LJCConfigRows;
          }
        }
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void KeyDetail_Change(object sender, EventArgs e)
    {
      DataDetailDialog detail;
      DbColumns dataColumns;

      detail = sender as DataDetailDialog;
      dataColumns = detail.LJCDataColumns;
      ResultConverter<DbMetaDataKey, DbMetaDataKeys> resultConverter
        = new ResultConverter<DbMetaDataKey, DbMetaDataKeys>();
      DbMetaDataKey mdKey = resultConverter.CreateData(dataColumns);

      var dataKeyManager = mManagers.DbMetaDataKeyManager;
      if (detail.LJCIsUpdate)
      {
        var keyColumns = new DbColumns()
        {
          { DbMetaDataKey.ColumnID, mdKey.ID }
        };
        dataKeyManager.Update(mdKey, keyColumns);
        if (dataKeyManager.AffectedCount > 0)
        {
          DbMetaDataKey lookupRecord = dataKeyManager.RetrieveWithID(mdKey.ID);
          RowUpdateKey(lookupRecord);
        }
      }
      else
      {
        dataKeyManager.Add(mdKey);
        if (dataKeyManager.AffectedCount > 0)
        {
          LJCGridRow row = RowAddKey(mdKey);
          mParent.ColumnGrid.LJCSetCurrentRow(row, true);
          mParent.ChangeTimer.DoChange(MainList.ChangeKey);
        }
      }
    }

    // Deletes the selected row.
    internal void DoDeleteKey()
    {
      string title;
      string message;

      if (mParent.KeyGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          // Data from items.
          int id = row.LJCGetInt32(DbMetaDataKey.ColumnID);

          var keyColumns = new DbColumns()
          {
            { DbMetaDataKey.ColumnID, id }
          };
          var dataKeyManager = mManagers.DbMetaDataKeyManager;
          dataKeyManager.Delete(keyColumns);
          if (dataKeyManager.AffectedCount < 1)
          {
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
          else
          {
            mParent.KeyGrid.Rows.Remove(row);
            mParent.ChangeTimer.DoChange(MainList.ChangeKey);
          }
        }
      }
    }

    // Refreshes the list.
    internal void DoRefreshKey()
    {
      DbMetaDataKey record;
      int id = 0;

      if (mParent.KeyGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(DbMetaDataKey.ColumnID);
      }
      DataRetrieveKey();

      // Select the original row.
      if (id > 0)
      {
        record = new DbMetaDataKey()
        {
          ID = id
        };
        RowSelectKey(record);
      }
    }

    // Performs the Close function.
    internal void DoCloseKey()
    {
      //if (mConfigRows != null)
      //{
      //  // ToDo: Convert to new DataDetailDialog.
      //  //RowOrderComparer comparer = new RowOrderComparer();
      //	//mConfigRows.SortRowOrder(comparer);
      //  ControlRowUniqueComparer comparer = new ControlRowUniqueComparer();
      //  mConfigRows.LJCSortUnique(comparer);
      //	//mConfigRows.Serialize(ConfigRowFileName);
      //}
    }
    #endregion

    #region Class Data

    //private readonly string ConfigRowFileName = @"DetailConfigs/KeyDetailConfig.xml";
    //private ControlRows mConfigRows;
    private readonly string mDataConfigName;
    private readonly MainList mParent;
    private readonly SQLUtilLibManagers mManagers;

    private readonly string mUserID;
    private readonly string mTableName;
    #endregion
  }
}
