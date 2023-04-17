// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingSelectCode.cs
using LJCDBClientLib;
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using System.Drawing;
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
        HeadingEdit.ShortcutKeyDisplayString = "";
        HeadingEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
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
      mControlValuesFileName = @"ControlValues\_ClassName_.xml";

      BackColor = mSettings.BeginColor;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesGenDocEdit.Instance;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;

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

    /// <summary>Gets or sets the ControlValues item.</summary>
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

    #region Internal Properties

    // Gets or sets the LJCIsSelect value.
    internal bool LJCIsSelect { get; set; }

    // Gets a reference to the selected record.
    internal DocClassGroupHeading LJCSelectedRecord { get; set; }

    // The Managers object.
    internal ManagersDocGen Managers { get; set; }
    #endregion

    #region Private Properties

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // Gets or sets the _ClassName_GridClass value.
    private ClassHeadingGridCode ClassHeadingGridCode { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private StandardUISettings mSettings;
    #endregion
  }
}
