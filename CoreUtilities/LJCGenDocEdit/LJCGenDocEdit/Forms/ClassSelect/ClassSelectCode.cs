// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassSelectCode.cs
using LJCDBClientLib;
using LJCDocLibDAL;
using LJCDocObjLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.IO;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  internal partial class ClassSelect : Form
  {
    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      InitializeClassData();
      SetupGridCode();
      InitialControlValues();
      SetupGrids();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\ClassSelect.xml";

      BackColor = mSettings.BeginColor;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesGenDocEdit.Instance;
      mSettings = values.StandardSettings;

      Managers = new ManagersDocGen();
      Managers.SetDBProperties(mSettings.DbServiceRef
        , mSettings.DataConfigName);
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(mControlValuesFileName))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , mControlValuesFileName) as ControlValues;

        if (ControlValues != null)
        {
          // Restore Window values.
          controlValue = ControlValues.LJCSearchName(Name);
          if (controlValue != null)
          {
            Left = controlValue.Left;
            Top = controlValue.Top;
            Width = controlValue.Width;
            Height = controlValue.Height;
          }

          // Restore Splitter, Grid and other values.
          ClassGrid.LJCRestoreColumnValues(ControlValues);
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      ClassGrid.LJCSaveColumnValues(controlValues);

      // Save Window values.
      controlValues.Add(this.Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      ClassGridCode = new ClassGridCode(this)
      {
        LJCAssemblyID = LJCAssemblyID
      };
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      ClassGridCode.SetupGrid();
    }

    // Gets or sets the ControlValues item.
    private ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Item Change Processing

    // Execute the list and related item functions.
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          RestoreControlValues();

          // Load first control.
          ClassGridCode.DataRetrieve();
          break;
      }
      //SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
    }

    #region Item Change Support

    // Start the Change processing.
    private void StartChangeProcessing()
    {
      ChangeTimer = new ChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Gets or sets the ChangeTimer object.
    private ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Internal Properties

    /// <summary>Gets or sets the Assembly ID value.</summary>
    internal short LJCAssemblyID { get; set; }

    /// <summary>Gets or sets the LJCIsSelect value.</summary>
    internal bool LJCIsSelect { get; set; }

    /// <summary>Gets a reference to the selected record.</summary>
    internal DataType LJCSelectedRecord { get; set; }

    /// <summary>The Managers object.</summary>
    internal ManagersDocGen Managers { get; set; }
    #endregion

    #region Private Properties

    // Gets or sets the ClassGridCode value.
    private ClassGridCode ClassGridCode { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private StandardUISettings mSettings;
    #endregion
  }
}
