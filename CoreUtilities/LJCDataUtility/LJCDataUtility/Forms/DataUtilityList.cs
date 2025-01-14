// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityList.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // The list form.
  internal partial class DataUtilityList : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal DataUtilityList()
    {
      InitializeComponent();

      // Initialize property values.
      _ = new TabsFont(ColumnTabs);
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
          ConfigureControls();
          RestoreControlValues();

          // Load first control.
          ModuleComboCode.DataRetrieve();
          break;

        case Change.Module:
          ModuleCombo.Select();
          TableGridCode.DataRetrieve();
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

    // Gets or sets the ChangeTimer object.
    private ChangeTimer ChangeTimer { get; set; }
    #endregion

    #region Action Event Handlers

    // Shared menu Exit event handler.
    internal void Exit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion
  }
}
