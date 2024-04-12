// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodGridCode.cs
using LJCGenDocDAL;
using LJCDocObjLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Windows.Forms;
using LJCWinFormCommon;

namespace LJCGenDocEdit
{
  // Provides MethodGrid methods for the MethodSelect window.
  internal class MethodGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal MethodGridCode(MethodSelect parentList)
    {
      MethodSelect = parentList;
      ArgError = new ArgError("LJCGenDocEdit.MethodGridCode");
      MethodGrid = MethodSelect.MethodGrid;
      Managers = MethodSelect.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      MethodSelect.Cursor = Cursors.WaitCursor;
      MethodGrid.LJCRowsClear();

      var success = true;
      var docClass = GetClass(LJCClassID);
      var docAssembly = GetAssembly(docClass.DocAssemblyID);
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

      DataType dataType = null;
      if (success)
      {
        var dataTypes = dataAssembly.DataTypes;
        dataType = dataTypes.Find(x => x.Name == docClass.Name);
        if (null == dataType)
        {
          success = false;
        }
      }

      if (success)
      {
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
      MethodSelect.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    internal bool RowSelect(DataMethod dataRecord)
    {
      bool retValue = false;

      ArgError.MethodName = "RowSelect(dataRecord)";
      ArgError.Add(dataRecord, "dataRecord");
      NetString.ThrowArgError(ArgError.ToString());

      MethodSelect.Cursor = Cursors.WaitCursor;
      foreach (LJCGridRow row in MethodGrid.Rows)
      {
        if (dataRecord.OverloadName != null)
        {
          if (DocMethodOverload(row) == dataRecord.OverloadName)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MethodGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        else
        {
          if (DocMethodName(row) == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MethodGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        MethodSelect.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataMethod dataRecord)
    {
      ArgError.MethodName = "RowAdd(dataRecord)";
      ArgError.Add(dataRecord, "dataRecord");
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = MethodGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(MethodGrid, dataRecord);
      return retValue;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataMethod dataRecord)
    {
      ArgError.MethodName = "SetStoredValues(row, dataRecod)";
      ArgError.Add(dataRecord, "dataRecord");
      NetString.ThrowArgError(ArgError.ToString());

      row.LJCSetString("Name", dataRecord.Name);
      row.LJCSetString("OverloadName", dataRecord.OverloadName);
    }
    #endregion

    #region Action Methods

    // Sets the selected item and returns to the parent form.
    internal void DoSelect()
    {
      var selectList = MethodSelect;
      selectList.LJCSelectedRecord = null;
      var rows = MethodGrid.SelectedRows;
      var startIndex = rows.Count - 1;
      for (var index = startIndex; index >= 0; index--)
      {
        selectList.Cursor = Cursors.WaitCursor;
        var row = rows[index] as LJCGridRow;
        DataMethod dataObject = null;
        var overloadName = DocMethodOverload(row);
        if (overloadName != null)
        {
          dataObject = mDataMethods.Find(x => x.OverloadName == overloadName);
        }
        else
        {
          var name = DocMethodName(row);
          dataObject = mDataMethods.Find(x => x.Name == name);
        }
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
      var manager = Managers.DocMethodManager;
      manager.MethodGroupID = MethodSelect.LJCGroupID;
      manager.ResetSequence();
    }
    #endregion

    #region Other Methods

    // Setup the grid columns.
    internal void SetupGrid()
    {
      MethodGrid.MultiSelect = true;

      // Setup default grid columns if no columns are defined.
      if (0 == MethodGrid.Columns.Count)
      {
        GridColumns = new DbColumns()
        {
          { "Name" },
          { "OverloadName" },
          { "Summary" }
        };

        // Setup the grid columns.
        MethodGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(MethodGrid);
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the current row item Name.
    private string DocMethodName(LJCGridRow docMethodRow = null)
    {
      string retValue = null;

      if (null == docMethodRow)
      {
        docMethodRow = MethodGrid.CurrentRow as LJCGridRow;
      }
      if (docMethodRow != null)
      {
        retValue = docMethodRow.LJCGetString("Name");
      }
      return retValue;
    }

    // Retrieves the current row item Name.
    private string DocMethodOverload(LJCGridRow docMethodRow = null)
    {
      string retValue = null;

      if (null == docMethodRow)
      {
        docMethodRow = MethodGrid.CurrentRow as LJCGridRow;
      }
      if (docMethodRow != null)
      {
        retValue = docMethodRow.LJCGetString("OverloadName");
      }
      return retValue;
    }

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

    #region Properties

    // Gets or sets the Class ID value.
    internal short LJCClassID { get; set; }

    // Gets or sets the GridColumns value.
    internal DbColumns GridColumns { get; set; }

    // Gets or sets the ArgError object.
    private ArgError ArgError { get; set; }

    // The Managers object.
    private ManagersGenDoc Managers { get; set; }

    // Gets or sets the Method Grid reference.
    private LJCDataGrid MethodGrid { get; set; }

    // Gets or sets the Parent List reference.
    private MethodSelect MethodSelect { get; set; }
    #endregion

    #region Class Data

    private DataMethods mDataMethods;
    #endregion
  }
}
