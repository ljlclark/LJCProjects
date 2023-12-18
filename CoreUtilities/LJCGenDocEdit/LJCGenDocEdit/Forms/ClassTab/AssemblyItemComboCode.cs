// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyItemComboCode.cs
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The AssemblyItem combo code.</summary>
  internal class AssemblyItemComboCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal AssemblyItemComboCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mAssemblyCombo = mDocList.AssemblyCombo;
      mAssemblyGroupGrid = mDocList.AssemblyGroupGrid;
      Managers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the combo items.</summary>
    internal void DataRetrieve()
    {
      mAssemblyCombo.Items.Clear();

      if (mDocList.AssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        mDocList.Cursor = Cursors.WaitCursor;

        var manager = Managers.DocAssemblyManager;
        var dataRecords = manager.LoadWithParentID(AssemblyGroupID());

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocAssembly dataRecord in dataRecords)
          {
            var text = $"{dataRecord.Name} - {dataRecord.Description}";
            mDocList.AssemblyCombo.LJCAddItem(dataRecord.ID, text);
          }
          mDocList.AssemblyCombo.SelectedIndex = 0;
        }
        mDocList.Cursor = Cursors.Default;
      }
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCGenDoc/Common/List.xml'/>
    internal bool RowSelect(DocAssembly dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        for (int index = 0; index < mAssemblyCombo.Items.Count; index++)
        {
          var assemblyID = AssemblyComboID(index);
          if (assemblyID == dataRecord.ID)
          {
            mAssemblyCombo.LJCSetByItemID(assemblyID);
            retValue = true;
            break;
          }
        }
        mDocList.Cursor = Cursors.Default;
      }
      return retValue;
    }
    #endregion

    #region Other Methods

    // Retrieves the current combo item ID.
    /// <include path='items/AssemblyComboID/*' file='../../Doc/AssemblyItemComboCode.xml'/>
    internal short AssemblyComboID(int assemblyComboIndex = -1)
    {
      short retValue = default;

      if (-1 == assemblyComboIndex)
      {
        assemblyComboIndex = mAssemblyCombo.SelectedIndex;
      }
      if (assemblyComboIndex >= 0)
      {
        if (mAssemblyCombo.Items[assemblyComboIndex] is LJCItem item)
        {
          retValue = (short)item.ID;
        }
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/AssemblyGroupID/*' file='../../Doc/AssemblyItemComboCode.xml'/>
    internal short AssemblyGroupID(LJCGridRow assemblyGroupRow = null)
    {
      short retValue = 0;

      if (null == assemblyGroupRow)
      {
        assemblyGroupRow = mAssemblyGroupGrid.CurrentRow as LJCGridRow;
      }
      if (assemblyGroupRow != null)
      {
        retValue = (short)assemblyGroupRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the currently selecteditem.
    /// <include path='items/CurrentItem/*' file='../../../../LJCGenDoc/Common/List.xml'/>
    internal DocAssembly CurrentAssembly()
    {
      DocAssembly retValue = null;

      if (mAssemblyCombo.SelectedItem != null)
      {
        var assemblyID = (short)mAssemblyCombo.LJCSelectedItemID();
        if (assemblyID > 0)
        {
          var manager = Managers.DocAssemblyManager;
          retValue = manager.RetrieveWithID(assemblyID);
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    // The Managers object.
    private ManagersGenDoc Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCItemCombo mAssemblyCombo;
    private readonly LJCDataGrid mAssemblyGroupGrid;
    private readonly LJCGenDocList mDocList;
    #endregion
  }
}
