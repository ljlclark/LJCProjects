// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ClassItemComboCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  internal class ClassItemComboCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassItemComboCode(LJCGenDocList parent)
    {
      mParent = parent;
      mCombo = mParent.ClassCombo;
      mManagers = mParent.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the combo items.
    internal void DataRetrieve()
    {
      mCombo.Items.Clear();

      //if (mParent.AssemblyItemGrid.CurrentRow is LJCGridRow parentRow)
      if (mParent.ClassGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        mParent.Cursor = Cursors.WaitCursor;
        //var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);
        var parentID = (short)parentRow.LJCGetInt32(DocClass.ColumnID);

        var manager = mManagers.DocClassManager;
        //var dataRecords = manager.LoadWithParent(parentID);
        var dataRecords = manager.LoadWithGroup(parentID);

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocClass dataRecord in dataRecords)
          {
            var text = $"{dataRecord.Name} - {dataRecord.Description}";
            mParent.ClassCombo.LJCAddItem(dataRecord.ID, text);
          }
        }
        mParent.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocClass CurrentItem()
    {
      DocClass retValue = null;

      if (mCombo.SelectedItem != null)
      {
        var id = (short)mCombo.LJCSelectedItemID();
        if (id > 0)
        {
          var manager = mManagers.DocClassManager;
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
    internal bool RowSelect(DocClass dataRecord)
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
