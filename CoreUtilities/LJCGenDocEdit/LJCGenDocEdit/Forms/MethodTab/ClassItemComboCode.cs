// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ClassItemComboCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The ClassItem combo code.</summary>
  internal class ClassItemComboCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassItemComboCode(LJCGenDocList parent)
    {
      mParent = parent;
      mCombo = mParent.ClassCombo;
      mManagers = mParent.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the combo items.</summary>
    internal void DataRetrieve()
    {
      mCombo.Items.Clear();

      if (mParent.ClassGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        mParent.Cursor = Cursors.WaitCursor;
        var parentID = (short)parentRow.LJCGetInt32(DocClass.ColumnID);

        var manager = mManagers.DocClassManager;
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

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
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

    #region Other Methods

    // Retrieves the currently selecteditem.
    /// <include path='items/CurrentItem/*' file='../../../../LJCDocLib/Common/List.xml'/>
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
    #endregion

    #region Class Data

    private readonly LJCItemCombo mCombo;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    #endregion
  }
}
