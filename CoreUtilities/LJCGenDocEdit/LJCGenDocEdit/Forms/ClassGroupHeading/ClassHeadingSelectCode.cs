// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingSelectCode.cs
using LJCDBClientLib;
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using System.IO;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  internal partial class ClassHeadingSelect : Form
  {
    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      InitializeClassData();
      SetupGridCode();
      ControlSetup();
      InitialControlValues();
      SetupGrids();
      ClassHeadingGridCode.DataRetrieve();
      RestoreControlValues();
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Initial Control setup.
    private void ControlSetup()
    {
      if (LJCIsSelect)
      {
        // This is a Selection List.
        Text = "Class Group Heading Selection";
        //HeadingEdit.ShortcutKeyDisplayString = "";
        //HeadingEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
      }
      else
      {
        // This is a display list.
        Text = "Class Group Heading List";
        HeadingSelectSeparator.Visible = false;
        HeadingSelect.Visible = false;
      }
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\ClassHeading.xml";

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
          ClassHeadingGrid.LJCRestoreColumnValues(ControlValues);
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      ClassHeadingGrid.LJCSaveColumnValues(controlValues);

      // Save Window values.
      controlValues.Add(this.Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      ClassHeadingGridCode = new ClassHeadingGridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      ClassHeadingGridCode.SetupGrid();
    }

    // Gets or sets the ControlValues item.
    private ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Methods

    // Sets the control states based on the current control values.
    internal void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = ClassHeadingGrid.CurrentRow != null;
      FormCommon.SetMenuState(HeadingMenu, enableNew, enableEdit);
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the LJCIsSelect value.</summary>
    internal bool LJCIsSelect { get; set; }

    /// <summary>Gets a reference to the selected record.</summary>
    internal DocClassGroupHeading LJCSelectedRecord { get; set; }

    /// <summary>The Managers object.</summary>
    internal ManagersDocGen Managers { get; set; }

    // Gets or sets the ClassHeadingGridCode value.
    private ClassHeadingGridCode ClassHeadingGridCode { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private StandardUISettings mSettings;
    #endregion
  }
}
