// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyItemComboCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  internal class AssemblyItemComboCode
  {
    #region Constructors

    // Initializes an object instance.
    internal AssemblyItemComboCode(LJCGenDocList parent)
    {
      mParent = parent;
      mCombo = mParent.AssemblyCombo;
      mManagers = mParent.Managers;
    }
    #endregion
    #region Data Methods

    // Retrieves the combo items.
    internal void DataRetrieve()
    {
      mParent.Cursor = Cursors.WaitCursor;
      mCombo.Items.Clear();

      if (mParent.AssemblyGrid.CurrentRow is LJCGridRow parentRow)
      {
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);

        var manager = mManagers.DocAssemblyManager;
        var dataRecords = manager.LoadWithParent(parentID);

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocAssembly dataRecord in dataRecords)
          {
            mParent.AssemblyCombo.LJCAddItem(dataRecord.ID
              , dataRecord.Description);
          }
        }
      }
      mParent.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    internal bool RowSelect()
    {
      bool retValue = false;

      if (mParent.AssemblyItemGrid.CurrentRow is LJCGridRow parentRow)
      {
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);

        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mParent.AssemblyGrid.Rows)
        {
          var rowID = (short)row.LJCGetInt32(DocAssembly.ColumnID);
          if (rowID == parentID)
          {
            mParent.AssemblyCombo.LJCSetByItemID(rowID);
            retValue = true;
            break;
          }
        }
        mParent.Cursor = Cursors.Default;
      }
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly LJCItemCombo mCombo;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    #endregion
  }
}
