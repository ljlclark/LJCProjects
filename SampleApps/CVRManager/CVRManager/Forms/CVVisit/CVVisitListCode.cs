// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CVRDAL;
using LJCDataAccess;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;

namespace CVRManager
{
	// The list form.
	internal partial class CVVisitList : Form
	{
		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesCVRManager.Instance;
			Settings = values.StandardSettings;

			// Initialize Class Data.
			ValuesCVRDAL.Instance.SetProperties("CVRManager.exe.config");
			Managers = ValuesCVRDAL.Instance.Managers;

			// Initialize Grid Code.
			mCVVisitGridCode = new CVVisitGridCode(this);

			// Load control data.
			Facilities facilities;
			try
			{
				facilities = Managers.FacilityManager.Load();
			}
			catch (SystemException e)
			{
				CVCommon.CreateTables(e, Settings.DataConfigName);
				facilities = Managers.FacilityManager.Load();
			}

			foreach (Facility facility in facilities)
			{
				FacilityCombo.LJCAddItem(facility.ID, facility.Description);
			}

			// Set initial control values.
			NetFile.CreateFolder("ExportFiles");
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\CVRManagerList.xml";

			BackColor = Settings.BeginColor;
			MainTools.BackColor = Settings.BeginColor;

			DateMask.LJCText = DataCommon.GetUIDateString(DateTime.Now);
			mCVVisitGridCode.DoClearFilters();

			SetupGridCVVisit();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				CVVisitGrid.Height = ClientSize.Height - MainTools.Height + 1;

				DateButton.Height = DateMask.Height;
				DateButton.Width = DateButton.Height + 1;
				ShowButton.Height = DateMask.Height;
				FiltersButton.Height = ShowButton.Height;
				ClearFiltersButton.Height = ShowButton.Height;
				ClearFiltersButton.Top = FiltersButton.Top;
			}
		}

		// Setup the grid display columns.
		private void SetupGridCVVisit()
		{
			CVVisitGrid.BackgroundColor = Settings.BeginColor;

			if (0 == CVVisitGrid.Columns.Count)
			{
				mRegisterDateColumn = new DbColumn()
				{
					ColumnName = "RegisterDate",
					DataTypeName = "DateTime",
					Caption = "Date",
					MaxLength = -1
				};
				List<string> columnNames = new List<string>() {
					mRegisterDateColumn.ColumnName,
					CVVisit.ColumnRegisterTime,
					CVVisit.ColumnEnterTime,
					CVVisit.ColumnExitTime,
					CVPerson.ColumnFirstName,
					CVPerson.ColumnMiddleName,
					CVPerson.ColumnLastName
				};

				// Get the display columns from the manager Data Definition.
				try
				{
					mDisplayColumnsCVVisit = Managers.CVVisitManager.GetColumns(columnNames);
				}
				catch (SystemException e)
				{
					CVCommon.CreateTables(e, Settings.DataConfigName);
					mDisplayColumnsCVVisit = Managers.CVVisitManager.GetColumns(columnNames);
				}

				mRealDisplayColumnsCVVisit = mDisplayColumnsCVVisit.Clone();
				mDisplayColumnsCVVisit.Insert(0, mRegisterDateColumn);

				// Setup the grid display columns.
				CVVisitGrid.LJCAddDisplayColumns(mDisplayColumnsCVVisit);
			}
		}
		public DbColumns mRealDisplayColumnsCVVisit;
		private DbColumns mDisplayColumnsCVVisit;
		internal DbColumn mRegisterDateColumn;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			CVVisitGrid.LJCSaveColumnValues(controlValues);

			// Save Window values.
			controlValues.Add(Name, Left, Top, Width, Height);

			// Save other values.
			int facilityID = FacilityCombo.LJCSelectedItemID();
			controlValues.Add("Facility", facilityID, 0, 0, 0);

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
					CVVisitGrid.LJCRestoreColumnValues(ControlValues);

					controlValue = ControlValues.LJCSearchName("Facility");
					if (controlValue != null)
					{
						mFacilityID = controlValue.Left;
						FacilityCombo.LJCSetByItemID(mFacilityID);
					}
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
					// Combo loaded in InitializeControls().
					break;

				case Change.Facility:
					// From FacilityCombo_SelectedIndexChanged().
					mCVVisitGridCode.DoShow();
					break;

				case Change.CVVisit:
					CVVisitGrid.LJCSetLastRow();
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Facility,
			CVVisit
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
			bool enableEdit = CVVisitGrid.CurrentRow != null;
			FormCommon.SetToolState(MainTools, enableNew, enableEdit);
			FormCommon.SetMenuState(MainMenu, enableNew, enableEdit);
			MainMenuEnter.Enabled = enableEdit;
			MainMenuExit.Enabled = enableEdit;
			MainFileEdit.Enabled = enableNew;
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

		// The Settings object.
		internal StandardUISettings Settings { get; set; }
		#endregion

		#region Class Data

		//private StandardSettings mSettings;
		private string mControlValuesFileName;
		private CVVisitGridCode mCVVisitGridCode;

		// Foreign Keys
		internal int mFacilityID;

		internal string mEndDateString;
		internal string mLastName;
		internal string mMiddleName;
		internal string mFirstName;
		#endregion
	}
}
