// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodGridCode.cs
using LJCDocLibDAL;
using LJCDocObjLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The Method grid code.</summary>
  internal class MethodGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodGridCode(MethodSelect parentList)
    {
      mMethodSelect = parentList;
      mMethodGrid = mMethodSelect.MethodGrid;
      Managers = mMethodSelect.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mMethodSelect.Cursor = Cursors.WaitCursor;
      mMethodGrid.LJCRowsClear();

      var docClass = GetClass(LJCClassID);
      var docAssembly = GetAssembly(docClass.DocAssemblyID);
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
            var dataTypes = dataAssembly.DataTypes;
            var dataType = dataTypes.Find(x => x.Name == docClass.Name);
            mDataMethods = dataType.DataMethods;
            foreach (var dataMethod in mDataMethods)
            {
              if (NetString.HasValue(dataMethod.Summary))
              {
                dataMethod.Summary = dataMethod.Summary.Trim();
              }
              RowAdd(dataMethod);
            }
          }
        }
      }
      mMethodSelect.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DataMethod dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mMethodSelect.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mMethodGrid.Rows)
        {
          // *** Next Statement *** Change - 7/9/23
          if (DocMethodOverload(row) == dataRecord.OverloadName)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mMethodGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mMethodSelect.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataMethod dataRecord)
    {
      var retValue = mMethodGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(mMethodGrid, dataRecord);
      return retValue;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataMethod dataRecord)
    {
      row.LJCSetString("OverloadName", dataRecord.OverloadName);
    }
    #endregion

    #region Action Methods

    /// <summary>Sets the selected item and returns to the parent form.</summary>
    internal void DoSelect()
    {
      mMethodSelect.LJCSelectedRecord = null;
      if (mMethodGrid.CurrentRow is LJCGridRow _)
      {
        mMethodSelect.Cursor = Cursors.WaitCursor;
        // *** Begin *** Change - 7/9/23
        var overloadName = DocMethodOverload();
        var dataRecord = mDataMethods.Find(x => x.OverloadName == overloadName);
        // *** End   *** Change - 7/9/23
        if (dataRecord != null)
        {
          mMethodSelect.LJCSelectedRecord = dataRecord;
        }
        mMethodSelect.Cursor = Cursors.Default;
      }
      mMethodSelect.DialogResult = DialogResult.OK;
    }
    #endregion

    #region Get Data Methods

    // Retrieves the AssemblyItem with the ID value.
    private DocAssembly GetAssembly(short assemblyID)
    {
      DocAssembly retValue = null;

      if (assemblyID > 0)
      {
        var manager = Managers.DocAssemblyManager;
        retValue = manager.RetrieveWithID(assemblyID);
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

    // Retrieves the AssemblyItem with the ID value.
    private DocClass GetClass(short classID)
    {
      DocClass retValue = null;

      if (classID > 0)
      {
        var manager = Managers.DocClassManager;
        retValue = manager.RetrieveWithID(classID);
      }
      return retValue;
    }
    #endregion

    #region Other Methods

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == mMethodGrid.Columns.Count)
      {
        GridColumns = new DbColumns()
        {
          { "Name" },
          { "OverloadName" },
          { "Summary" }
        };

        // Setup the grid columns.
        mMethodGrid.LJCAddColumns(GridColumns);
      }
    }

    // Retrieves the current row item Name.
    private string DocMethodOverload(LJCGridRow docMethodRow = null)
    {
      string retValue = null;

      if (null == docMethodRow)
      {
        docMethodRow = mMethodGrid.CurrentRow as LJCGridRow;
      }
      if (docMethodRow != null)
      {
        retValue = docMethodRow.LJCGetString("OverloadName");
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Class ID value.</summary>
    internal short LJCClassID { get; set; }

    /// <summary>Gets or sets the GridColumns value.</summary>
    internal DbColumns GridColumns { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mMethodGrid;
    private readonly MethodSelect mMethodSelect;
    private DataMethods mDataMethods;
    #endregion
  }
}
