// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyItemComboCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.Security.Cryptography;
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

      if (mParent.AssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
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

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocAssembly CurrentItem()
    {
      DocAssembly retValue = null;

      if (mCombo.SelectedItem != null)
      {
        var id = (short)mCombo.LJCSelectedItemID();
        if (id > 0)
        {
          var manager = mManagers.DocAssemblyManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Selects a row based on the key record values.
    /// <summary>
    /// Selects a row based on the key record values.
    /// </summary>
    /// <param name="dataRecord">The data record to be selected.</param>
    /// <returns>True if the item was selected, otherwise false.</returns>
    internal bool RowSelect(DocAssembly dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        for (int index = 0; index < mCombo.Items.Count; index++)
        {
          var item = mCombo.Items[index] as LJCItem;
          if (item.ID == dataRecord.ID)
          {
            mCombo.LJCSetByItemID(item.ID);
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
