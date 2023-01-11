// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CVRDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;

namespace CVRManager
{
	internal partial class FacilityList : Form
	{
		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesCVRManager.Instance;
			mSettings = values.StandardSettings;

			// Initialize Class Data.
			Managers = ValuesCVRDAL.Instance.Managers;

			// Initialize Grid Code.
			mFacilityGridCode = new FacilityGridCode(this);

			// Set initial control values.
			NetFile.CreateFolder("ExportFiles");
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\FacilityList.xml";

			BackColor = mSettings.BeginColor;

			SetupGridFacility();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				//FacilityGrid.Height = ClientSize.Height - MainTools.Height + 1;
			}
		}

		// Setup the grid display columns.
		private void SetupGridFacility()
		{
			FacilityGrid.BackgroundColor = mSettings.BeginColor;

			if (0 == FacilityGrid.Columns.Count)
			{
				List<string> columnNames = new List<string>() {
					Facility.ColumnCode,
					Facility.ColumnDescription
				};

				// Get the display columns from the manager Data Definition.
				mDisplayColumnsFacility
					= Managers.FacilityManager.GetColumns(columnNames);

				// Setup the grid display columns.
				FacilityGrid.LJCAddDisplayColumns(mDisplayColumnsFacility);
			}
		}
		private DbColumns mDisplayColumnsFacility;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			FacilityGrid.LJCSaveColumnValues(controlValues);

			// Save Window values.
			controlValues.Add(Name, Left, Top, Width, Height);

			NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
				, mControlValuesFileName);
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
					FacilityGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		// Gets or sets the ControlValues item.
		private ControlValues ControlValues { get; set; }
		#endregion

		#region Item Change Processing

		// Execute the related item functions.
		internal void DoChange(Change change)
		{
			Cursor = Cursors.WaitCursor;
			switch (change)
			{
				case Change.Startup:
					ConfigureControls();
					RestoreControlValues();

					// Load first List.
					mFacilityGridCode.DataRetrieveFacility();
					break;

				case Change.Facility:
					FacilityGrid.LJCSetLastRow();
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Facility
		}

		#region Item Change Support

		// Start the Change processing.
		private void StartItemChange()
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
		internal ChangeTimer ChangeTimer { get; set; }
		#endregion
		#endregion

		#region Private Methods

		// Sets the control states based on the current control values.
		private void SetControlState()
		{
			bool enableNew = true;
			bool enableEdit = FacilityGrid.CurrentRow != null;
			FormCommon.SetMenuState(MainMenu, enableNew, enableEdit);
			MainMenuHelp.Enabled = enableNew;
		}
		#endregion

		#region Properties

		// The help file name.
		internal string LJCHelpFile
		{
			get { return mHelpFile; }
			set { mHelpFile = NetString.InitString(value); }
		}
		private string mHelpFile;

		// The List help page name.
		private string LJCHelpPageList
		{
			get { return mHelpPageList; }
			set { mHelpPageList = NetString.InitString(value); }
		}
		private string mHelpPageList;

		// The Detail help page name.
		internal string LJCHelpPageDetail
		{
			get { return mHelpPageDetail; }
			set { mHelpPageDetail = NetString.InitString(value); }
		}
		private string mHelpPageDetail;

		// The Managers object.
		internal CVRManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private FacilityGridCode mFacilityGridCode;
		#endregion
	}
}
