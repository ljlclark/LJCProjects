// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityList5.cs
using LJCDataUtilityDAL5;
using System.Configuration.Provider;

// Install-Package Microsoft.Data.SqlClient
// Install-Package System.Configuration.ConfigurationManager
// Microsoft.Extensions.Configuration?

namespace LJCDataUtility5
{
  // The list form.
  internal partial class DataUtilityList : Form
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal DataUtilityList()
    {
      InitializeComponent();
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void DataUtilityList_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToScreen();
    }
    #endregion

    #region Item Change Processing

    // Execute the list and related item functions.
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          //ConfigureControls();
          RestoreControlValues();

          // Load controls.
          ModuleComboCode.DataRetrieve();
          ConfigComboCode.DataRetrieve();
          break;

        case Change.Module:
          ModuleCombo.Select();
          TableGridCode.DataRetrieve();
          break;

        case Change.Config:
          Managers.Reset(ConfigCombo.Text);
          ModuleComboCode.Reset();
          ModuleComboCode.DataRetrieve();
          break;

        case Change.Table:
          ColumnGridCode.DataRetrieve();
          KeyGridCode.DataRetrieve();
          break;

        case Change.Column:
          ColumnGrid.LJCSetLastRow();
          break;

        case Change.Key:
          KeyGrid.LJCSetLastRow();
          break;
      }
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      Module,
      Config,
      Table,
      Column,
      Key
    }

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Start the Change processing.
    private void StartChangeProcessing()
    {
      ChangeTimer = new LJCChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    private void ChangeTimer_ItemChange(object? sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Gets or sets the ChangeTimer object.
    private LJCChangeTimer ChangeTimer { get; set; } = null!;
    #endregion

    #region Action Event Handlers

    // Shared menu Exit event handler.
    internal void Exit_Click(object? sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion
  }
}
