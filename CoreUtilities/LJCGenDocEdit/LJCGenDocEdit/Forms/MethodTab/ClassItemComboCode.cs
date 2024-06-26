﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ClassItemComboCode.cs
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System.Reflection;
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
      mDocList = parent;
      ArgError = new ArgError("LJCGenDocEdit.ClassItemComboCode");
      mClassCombo = mDocList.ClassCombo;
      mClassGroupGrid = mDocList.ClassGroupGrid;
      mManagers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the combo items.</summary>
    internal void DataRetrieve()
    {
      mClassCombo.Items.Clear();

      if (mDocList.ClassGroupGrid.CurrentRow is LJCGridRow _)
      {
        mDocList.Cursor = Cursors.WaitCursor;

        var manager = mManagers.DocClassManager;
        var dataRecords = manager.LoadWithGroup(ClassGroupID());

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocClass dataRecord in dataRecords)
          {
            var text = $"{dataRecord.Name} - {dataRecord.Description}";
            mDocList.ClassCombo.LJCAddItem(dataRecord.ID, text);
          }
          mDocList.ClassCombo.SelectedIndex = 0;
        }
        mDocList.Cursor = Cursors.Default;
      }
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCGenDoc/Common/List.xml'/>
    internal bool RowSelect(DocClass dataRecord)
    {
      bool retValue = false;

      ArgError.MethodName = "RowSelect(dataRecord)";
      ArgError.Add(dataRecord, "dataRecord");
      NetString.ThrowArgError(ArgError.ToString());

      mDocList.Cursor = Cursors.WaitCursor;
      for (int index = 0; index < mClassCombo.Items.Count; index++)
      {
        var classID = ClassComboID(index);
        if (classID == dataRecord.ID)
        {
          mClassCombo.LJCSetByItemID(classID);
          retValue = true;
          break;
        }
      }
      mDocList.Cursor = Cursors.Default;
      return retValue;
    }
    #endregion

    #region Other Methods

    // Retrieves the current combo item ID.
    /// <include path='items/ClassComboID/*' file='../../Doc/ClassItemComboCode.xml'/>
    internal short ClassComboID(int classComboIndex = -1)
    {
      short retValue = default;

      if (-1 == classComboIndex)
      {
        classComboIndex = mClassCombo.SelectedIndex;
      }
      if (classComboIndex >= 0)
      {
        if (mClassCombo.Items[classComboIndex] is LJCItem item)
        {
          retValue = (short)item.ID;
        }
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/ClassGroupID/*' file='../../Doc/ClassItemComboCode.xml'/>
    internal short ClassGroupID(LJCGridRow classGroupGridRow = null)
    {
      short retValue = default;

      if (null == classGroupGridRow)
      {
        classGroupGridRow = mClassGroupGrid.CurrentRow as LJCGridRow;
      }
      if (classGroupGridRow != null)
      {
        retValue = (short)classGroupGridRow.LJCGetInt32(DocClassGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the currently selecteditem.
    /// <include path='items/CurrentItem/*' file='../../../../LJCGenDoc/Common/List.xml'/>
    internal DocClass CurrentClass()
    {
      DocClass retValue = null;

      if (mClassCombo.SelectedItem != null)
      {
        var classID = (short)mClassCombo.LJCSelectedItemID();
        if (classID > 0)
        {
          var manager = mManagers.DocClassManager;
          retValue = manager.RetrieveWithID(classID);
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the ArgError object.
    private ArgError ArgError { get; set; }
    #endregion

    #region Class Data

    private readonly LJCItemCombo mClassCombo;
    private readonly LJCDataGrid mClassGroupGrid;
    private readonly LJCGenDocList mDocList;
    private readonly ManagersGenDoc mManagers;
    #endregion
  }
}
