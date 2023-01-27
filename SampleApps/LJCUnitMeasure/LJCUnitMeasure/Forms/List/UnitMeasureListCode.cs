// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitMeasureListCode.cs
using System;
using System.IO;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCUnitMeasureDAL;
using LJCWinFormCommon;

namespace LJCUnitMeasure
{
	internal partial class UnitMeasureList
	{
		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesUnitMeasure.Instance;
			mSettings = values.StandardSettings;

			// Initialize Class Data.
			Managers = new UnitMeasureManagers();
			Managers.SetDBProperties(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			try
			{
				mUnitSystemComboCode = new UnitSystemComboCode(Managers, this);
			}
			catch (SystemException e)
			{
				CreateTables(e, mSettings.DataConfigName);
				mUnitSystemComboCode = new UnitSystemComboCode(Managers, this);
			}

			mUnitCategoryComboCode = new UnitCategoryComboCode(Managers, this);
			mUnitMeasureGridCode = new UnitMeasureGridCode(this);

			// Testing
			//int gallonsMeasureID = 14;
			//int litersMeasureID = 36;
			//var unitConversion
			//	= Managers.UnitConversionManager.RetrieveWithIDs(gallonsMeasureID
			//	, litersMeasureID);
			//if (unitConversion != null)
			//{
			//	var value = unitConversion.ConvertUnit(1.5);
			//}

			// Set initial control values.
			NetFile.CreateFolder("ExportFiles");
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\UnitMeasure.xml";

			BackColor = mSettings.BeginColor;

			mUnitMeasureGridCode.SetupGrid();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Create the application tables.
		internal static void CreateTables(SystemException e, string dataConfigName)
		{
			ManagerCommon.GetConfigValues(dataConfigName, out string connectionType
				, out string _, out string _);

			string[] fileSpecs;
			switch (connectionType)
			{
				case "MySQL":
					string[] fileSpecs1 = {
						@"MySQLScript\2msp_CreateUnitCategory.sql",
						@"MySQLScript\2msp_CreateUnitConversion.sql",
						@"MySQLScript\2msp_CreateUnitMeasure.sql",
						@"MySQLScript\2msp_CreateUnitSystem.sql",
						@"MySQLScript\3mUnitMeasureTables.sql"
					};
					fileSpecs = fileSpecs1;
					break;

				default:
					string[] fileSpecs2 = {
						@"SQLScript\2sp_CreateUnitCategory.sql",
						@"SQLScript\2sp_CreateUnitConversion.sql",
						@"SQLScript\2sp_CreateUnitMeasure.sql",
						@"SQLScript\2sp_CreateUnitSystem.sql",
						@"SQLScript\3UnitMeasureTables.sql"
					};
					fileSpecs = fileSpecs2;
					break;
			}

			int errorCode = ManagerCommon.GetMissingTableErrorCode(dataConfigName);
			if (e.HResult == errorCode)
			{
				if (FormCommon.CreateTablesPrompt(e.Message, fileSpecs))
				{
					if (false == ManagerCommon.CreateTables(dataConfigName, fileSpecs))
					{
						throw new SystemException(e.Message);
					}
				}
			}
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				UnitMeasureHeading.Height = 24;
			}
		}

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			UnitMeasureGrid.LJCSaveColumnValues(controlValues);

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
					UnitMeasureGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		// Gets or sets the ControlValues item.
		private ControlValues ControlValues { get; set; }
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
					mUnitSystemComboCode.DataRetrieve();
					mUnitCategoryComboCode.DataRetrieve();
					break;

				case Change.UnitSystem:
					mUnitMeasureGridCode.DataRetrieve();
					break;

				case Change.UnitCategory:
					mUnitMeasureGridCode.DataRetrieve();
					break;

				case Change.UnitMeasure:
					UnitMeasureGrid.LJCSetLastRow();
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			UnitSystem,
			UnitCategory,
			UnitMeasure
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
			//bool enableNew = Combo.SelectedIndex != -1; ;
			//bool enableEdit = UnitMeasureGrid.CurrentRow != null;
			//FormCommon.SetToolState(UnitMeasureTool, enableNew, enableEdit);
			//FormCommon.SetMenuState(UnitMeasureMenu, enableNew, enableEdit);
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
		internal string LJCHelpPageList
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
		internal UnitMeasureManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private UnitSystemComboCode mUnitSystemComboCode;
		private UnitCategoryComboCode mUnitCategoryComboCode;
		private UnitMeasureGridCode mUnitMeasureGridCode;
		#endregion
	}
}
