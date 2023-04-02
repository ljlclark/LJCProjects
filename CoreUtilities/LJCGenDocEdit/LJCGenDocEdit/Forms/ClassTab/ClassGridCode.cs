// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassGridCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  internal class ClassGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassGridCode(LJCGenDocList parent)
    {
      mParent = parent;
      mGrid = mParent.ClassGrid;
      mManagers = mParent.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mParent.Cursor = Cursors.WaitCursor;
      mGrid.LJCRowsClear();

      if (mParent.AssemblyGrid.CurrentRow is LJCGridRow parentRow)
      {
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);

        var manager = mManagers.DocClassManager;
        var dataRecords = manager.LoadWithParent(parentID);

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocClass dataRecord in dataRecords)
          {
            RowAdd(dataRecord);
          }
        }
        mParent.Cursor = Cursors.Default;
        mParent.DoChange(LJCGenDocList.Change.ClassGroup);
      }
      mParent.Cursor = Cursors.Default;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClass dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClass dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocClass dataRecord)
    {
      row.LJCSetInt32(DocClass.ColumnID, dataRecord.ID);
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DocClass dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocClass.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mGrid.LJCSetCurrentRow(row, true);
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
    #endregion

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    #endregion
  }
}
