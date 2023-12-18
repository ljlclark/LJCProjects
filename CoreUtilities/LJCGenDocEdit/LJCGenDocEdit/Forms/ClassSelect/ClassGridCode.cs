// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassGridCode.cs
using LJCGenDocDAL;
using LJCDocObjLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCWinFormCommon;

namespace LJCGenDocEdit
{
  // Provides Class Grid methods for the ClassSelect window.
  internal class ClassGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassGridCode(ClassSelect parentList)
    {
      ClassSelect = parentList;
      ClassGrid = ClassSelect.ClassGrid;
      Managers = ClassSelect.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ClassSelect.Cursor = Cursors.WaitCursor;
      ClassGrid.LJCRowsClear();

      var success = true;
      var docAssembly = GetAssembly(LJCAssemblyID);
      var assemblyGroup
        = GetAssemblyGroup(docAssembly.DocAssemblyGroupID);
      if (null == assemblyGroup
        || null == docAssembly)
      {
        success = false;
      }

      DataAssembly dataAssembly = null;
      if (success)
      {
        var assemblyGroups = new DocAssemblyGroups
        {
          assemblyGroup
        };

        // Creates the DataAssemblies collection with the deserialized
        // "Doc" XML converted to the "Data" XML format.
        DataRoot dataRoot = new DataRoot(assemblyGroups);

        var dataAssemblies = dataRoot.DataAssemblies;
        dataAssembly = dataAssemblies.LJCSearchUnique(docAssembly.Name);
        if (null == dataAssembly)
        {
          success = false;
        }
      }

      if (success)
      {
        mDataTypes = dataAssembly.DataTypes;
        foreach (var dataType in mDataTypes)
        {
          if (dataType.Summary != null)
          {
            dataType.Summary = dataType.Summary.Trim();
          }
          RowAdd(dataType);
        }
      }
      ClassSelect.Cursor = Cursors.Default;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataType dataRecord)
    {
      var retValue = ClassGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ClassGrid, dataRecord);
      return retValue;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataType dataRecord)
    {
      row.LJCSetString("Name", dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Sets the selected item and returns to the parent form.
    internal void DoSelect()
    {
      var selectList = ClassSelect;
      ClassSelect.LJCSelectedRecord = null;
      var rows = ClassGrid.SelectedRows;
      var startIndex = rows.Count - 1;
      for (var index = startIndex; index >= 0; index--)
      {
        selectList.Cursor = Cursors.WaitCursor;
        var row = rows[index] as LJCGridRow;
        var name = ClassName(row);
        var dataObject = mDataTypes.Find(x => x.Name == name);
        if (dataObject != null)
        {
          selectList.LJCSelectedRecord = dataObject;
          selectList.LastMultiSelect = false;
          if (0 == index)
          {
            selectList.LastMultiSelect = true;
          }
          selectList.LJCOnChange();
        }
      }
      DoResetSequence();
      selectList.Cursor = Cursors.Default;
      selectList.DialogResult = DialogResult.OK;
    }

    // Resets the Sequence column values.
    internal void DoResetSequence()
    {
      var manager = Managers.DocClassManager;
      manager.ClassGroupID = ClassSelect.LJCGroupID;
      manager.ResetSequence();
    }
    #endregion

    #region Other Methods

    // Setup the grid columns.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      ClassGrid.MultiSelect = true;
      if (0 == ClassGrid.Columns.Count)
      {
        GridColumns = new DbColumns()
        {
          { "Name" },
          { "Summary" }
        };

        // Setup the grid columns.
        ClassGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(ClassGrid);
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the current row item Name.
    internal string ClassName(LJCGridRow classRow = null)
    {
      string retValue = null;

      if (null == classRow)
      {
        classRow = ClassGrid.CurrentRow as LJCGridRow;
      }
      if (classRow != null)
      {
        retValue = classRow.LJCGetString("Name");
      }
      return retValue;
    }

    // Retrieves the AssemblyItem with the ID value.
    private DocAssembly GetAssembly(short docAssemblyID)
    {
      DocAssembly retValue = null;

      if (docAssemblyID > 0)
      {
        var manager = Managers.DocAssemblyManager;
        retValue = manager.RetrieveWithID(docAssemblyID);
      }
      return retValue;
    }

    // Retrieves the AssemblyGroup with the ID value.
    private DocAssemblyGroup GetAssemblyGroup(short assemblyGroupID)
    {
      DocAssemblyGroup retValue = null;

      if (assemblyGroupID > 0)
      {
        var manager = Managers.DocAssemblyGroupManager;
        retValue = manager.RetrieveWithID(assemblyGroupID);
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the Assembly ID value.
    internal short LJCAssemblyID { get; set; }

    // Gets or sets the GridColumns value.
    private DbColumns GridColumns { get; set; }

    // Gets or sets the Class Grid reference.
    private LJCDataGrid ClassGrid { get; set; }

    // Gets or sets the Parent List reference.
    private ClassSelect ClassSelect { get; set; }

    // The Managers object.
    private ManagersGenDoc Managers { get; set; }
    #endregion

    #region Class Data

    private List<DataType> mDataTypes;
    #endregion
  }
}
