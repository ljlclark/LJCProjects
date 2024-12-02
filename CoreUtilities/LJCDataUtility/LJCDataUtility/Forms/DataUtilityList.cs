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

// Item Change Processing
//   internal void DoChange(Change change)
//   internal void TimedChange(Change change)
// Action Event Handlers
//   internal void Exit_Click(object sender, EventArgs e)

namespace LJCDataUtility
{
  // The list form.
  internal partial class DataUtilityList : Form
  {
    // ******************************
    #region Constructors
    // ******************************

    // Initializes an object instance.
    // ********************
    internal DataUtilityList()
    {
      InitializeComponent();

      // Initialize property values.
      _ = new TabsFont(MainTabs);
    }
    #endregion

    // ******************************
    #region Form Event Handlers
    // ******************************

    // ********************
    private void DataUtilityList_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToScreen();
    }
    #endregion

    // ******************************
    #region Item Change Processing
    // ******************************

    // Execute the list and related item functions.
    // ********************
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          RestoreControlValues();

          // Load first control.
          ModuleGridCode.DataRetrieve();
          break;

        case Change.Module:
          TableGridCode.DataRetrieve();
          break;

        case Change.Table:
          ColumnGridCode.DataRetrieve();
          KeyGridCode.DataRetrieve();
          //MapTableGridCode.DataRetrieve();
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
    // ********************
    internal enum Change
    {
      Startup,
      Module,
      Table,
      Column,
      Key
    }

    // Starts the Timer with the Change value.
    // ********************
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Start the Change processing.
    // ********************
    private void StartChangeProcessing()
    {
      ChangeTimer = new ChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    // ********************
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Gets or sets the ChangeTimer object.
    // ********************
    private ChangeTimer ChangeTimer { get; set; }
    #endregion

    // ******************************
    #region Action Event Handlers
    // ******************************

    // Shared menu Exit event handler.
    // ********************
    internal void Exit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion
  }
}
