// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassGridCode.cs
using LJCDocLibDAL;
using LJCDocObjLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The Class grid code.</summary>
  internal class ClassGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassGridCode(ClassSelect parentList)
    {
      mClassSelect = parentList;
      mClassGrid = mClassSelect.ClassGrid;
      Managers = mClassSelect.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mClassSelect.Cursor = Cursors.WaitCursor;
      mClassGrid.LJCRowsClear();

      var docAssembly = GetAssembly(LJCAssemblyID);
      var assemblyGroup
        = GetAssemblyGroup(docAssembly.DocAssemblyGroupID);
      if (assemblyGroup != null)
      {
        var assemblyGroups = new DocAssemblyGroups
        {
          assemblyGroup
        };

        // Creates the DataAssemblies collection with the deserialized
        // "Doc" XML converted to the "Data" XML format.
        DataRoot dataRoot = new DataRoot(assemblyGroups);

        if (docAssembly != null)
        {
          var dataAssemblies = dataRoot.DataAssemblies;
          var dataAssembly = dataAssemblies.LJCSearchUnique(docAssembly.Name);
          if (dataAssembly != null)
          {
            mDataTypes = dataAssembly.DataTypes;
            foreach (var dataType in mDataTypes)
            {
              dataType.Summary = dataType.Summary.Trim();
              RowAdd(dataType);
            }
          }
        }
      }
      mClassSelect.Cursor = Cursors.Default;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataType dataRecord)
    {
      var retValue = mClassGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(mClassGrid, dataRecord);
      return retValue;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataType dataRecord)
    {
      row.LJCSetString("Name", dataRecord.Name);
    }
    #endregion

    #region Action Methods

    /// <summary>Sets the selected item and returns to the parent form.</summary>
    internal void DoSelect()
    {
      var selectList = mClassSelect;
      mClassSelect.LJCSelectedRecord = null;
      // *** Begin *** Change - MultiSelect 10/29/23
      var rows = mClassGrid.SelectedRows;
      var startIndex = rows.Count - 1;
      for (var index = startIndex; index >= 0; index--)
      {
        selectList.Cursor = Cursors.WaitCursor;
        var row = rows[index] as LJCGridRow;
        var name = ClassName();
        var dataObject = mDataTypes.Find(x => x.Name == name);
        if (dataObject != null)
        {
          selectList.LJCSelectedRecord = dataObject;
          selectList.LJCOnChange();
        }
      }
      DoResetSequence();
      selectList.Cursor = Cursors.Default;
      // *** End   *** Change - MultiSelect 10/29/23
      selectList.DialogResult = DialogResult.OK;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var classManager = Managers.DocClassManager;
      classManager.ResetSequence();
    }
    #endregion

    #region Get Data Methods

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

    #region Other Methods

    // Retrieves the current row item Name.
    /// <include path='items/ClassName/*' file='../../Doc/ClassGridCode.xml'/>
    internal string ClassName(LJCGridRow classRow = null)
    {
      string retValue = null;

      if (null == classRow)
      {
        classRow = mClassGrid.CurrentRow as LJCGridRow;
      }
      if (classRow != null)
      {
        retValue = classRow.LJCGetString("Name");
      }
      return retValue;
    }

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // *** Next Statement *** Add - MultiSelect 10/29/23
      mClassGrid.MultiSelect = true;

      // Setup default grid columns if no columns are defined.
      if (0 == mClassGrid.Columns.Count)
      {
        GridColumns = new DbColumns()
        {
          { "Name" },
          { "Summary" }
        };

        // Setup the grid columns.
        mClassGrid.LJCAddColumns(GridColumns);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Assembly ID value.</summary>
    internal short LJCAssemblyID { get; set; }

    /// <summary>Gets or sets the GridColumns value.</summary>
    internal DbColumns GridColumns { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mClassGrid;
    private readonly ClassSelect mClassSelect;
    private List<DataType> mDataTypes;
    #endregion
  }
}
