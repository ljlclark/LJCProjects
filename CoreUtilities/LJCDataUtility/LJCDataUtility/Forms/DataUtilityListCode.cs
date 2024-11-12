// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityListCode.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LJCDataUtility
{
  public partial class DataUtilityList : Form
  {
    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      InitializeClassData();
      InitialControlValues();
      SetupGridCode();
      SetupGrids();
      Cursor = Cursors.Default;
    }
    #endregion

    #region Setup Support

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      Values = ValuesDataUtility.Instance;
      Values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = Values.Errors;
      if (NetString.HasValue(errors))
      {
        MessageBox.Show(errors, "Config Errors", MessageBoxButtons.OK
          , MessageBoxIcon.Error);
      }
      Managers = Values.Managers;
      Settings = Values.StandardSettings;
      Text += $" - {Settings.DataConfigName}";
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      ControlValuesFileName = @"ControlValues\DataUtility.xml";
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(ControlValuesFileName))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , ControlValuesFileName) as ControlValues;

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
          ModuleGrid.LJCRestoreColumnValues(ControlValues);
          TableGrid.LJCRestoreColumnValues(ControlValues);
          ColumnGrid.LJCRestoreColumnValues(ControlValues);
          KeyGrid.LJCRestoreColumnValues(ControlValues);
          MapTableGrid.LJCRestoreColumnValues(ControlValues);
          MapColumnGrid.LJCRestoreColumnValues(ControlValues);

          ModuleGrid.LJCRestoreFontSize(ControlValues);
          TableGrid.LJCRestoreFontSize(ControlValues);
          ColumnGrid.LJCRestoreFontSize(ControlValues);
          KeyGrid.LJCRestoreFontSize(ControlValues);
          MapTableGrid.LJCRestoreFontSize(ControlValues);
          MapColumnGrid.LJCRestoreFontSize(ControlValues);
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      ModuleGrid.LJCSaveColumnValues(controlValues);
      TableGrid.LJCSaveColumnValues(controlValues);
      ColumnGrid.LJCSaveColumnValues(controlValues);
      KeyGrid.LJCSaveColumnValues(controlValues);
      MapTableGrid.LJCSaveColumnValues(controlValues);
      MapColumnGrid.LJCSaveColumnValues(controlValues);

      // Save Window values.
      // Tabs Parent is not this module form.
      controlValues.Add(Name, Left, Top
        , Width, Height);

      // Save other values.
      ModuleGrid.LJCSaveFontSize(controlValues);
      TableGrid.LJCSaveFontSize(controlValues);
      ColumnGrid.LJCSaveFontSize(controlValues);
      KeyGrid.LJCSaveFontSize(controlValues);
      MapTableGrid.LJCSaveFontSize(controlValues);
      MapColumnGrid.LJCSaveFontSize(controlValues);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , ControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      ModuleGridCode = new DataModuleGridCode(this);
      TableGridCode = new DataTableGridCode(this);
      ColumnGridCode = new DataColumnGridCode(this);
      KeyGridCode = new DataKeyGridCode(this);
      MapTableGridCode = new DataMapTableGridCode(this);
      MapColumnGridCode = new DataMapColumnGridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      ModuleGridCode.SetupGrid();
      TableGridCode.SetupGrid();
      ColumnGridCode.SetupGrid();
      KeyGridCode.SetupGrid();
      MapTableGridCode.SetupGrid();
      MapColumnGridCode.SetupGrid();
    }
    #endregion

    #region Class Data

    internal ManagersDataUtility Managers;

    // Grid Code
    private DataColumnGridCode ColumnGridCode { get; set; }
    private DataKeyGridCode KeyGridCode { get; set; }
    private DataMapColumnGridCode MapColumnGridCode { get; set; }
    private DataMapTableGridCode MapTableGridCode { get; set; }
    private DataModuleGridCode ModuleGridCode { get; set; }
    private DataTableGridCode TableGridCode { get; set; }

    /// <summary>Gets or sets the ControlValues item.</summary>
    private ControlValues ControlValues { get; set; }
    private string ControlValuesFileName { get; set; }
    private StandardUISettings Settings { get; set; }
    private ValuesDataUtility Values { get; set; }
    #endregion

    // Test the XMLBuilder class.
    private string TestXML()
    {
      var xml = new XMLBuilder();
      var attributes = xml.StartAttributes();

      xml.Begin("Sections", null, attributes);
      xml.Begin("Section");
      xml.Element("Name", "Main");

      xml.Begin("RepeatItems");
      xml.Begin("RepeatItem");
      xml.Element("Name", "Item1");

      xml.Begin("Replacements");
      xml.Begin("Replacement");
      xml.Element("Name", "_AssemblyName_");
      xml.Element("Value", "LJCDataAccess");
      xml.End("Replacement");
      xml.End("Replacements");

      xml.End("RepeatItem");
      xml.End("RepeatItems");

      xml.End("Section");
      xml.End("Sections");

      string retXML = xml.ToString();
      return retXML;
    }
  }
}
