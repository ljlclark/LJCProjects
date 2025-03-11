// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataSourceListCode.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	internal partial class DataSourceList : Form
	{
		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesTransformManager.Instance;
			mSettings = values.StandardSettings;

			// Initialize Class Data.
			Managers = new TransformManagers(mSettings.DbServiceRef
			, mSettings.DataConfigName);

			// Initialize Grid Code.
			mDataSourceGridCode = new DataSourceGridCode(this);

			// Configure controls.
			if (LJCIsSelect)
			{
				// This is a Selection List.
				Text = "DataSource Selection";
				DataSourceMenuEdit.ShortcutKeyDisplayString = "";
				DataSourceMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
			}
			else
			{
				// This is a display list.
				Text = "DataSource List";
				DataSourceMenuSelect.Visible = false;
			}

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\DataSource.xml";

			try
			{
				SetupGridSource();
			}
			catch (SystemException e)
			{
				TransformCommon.CreateTables(e, mSettings.DataConfigName);
				SetupGridSource();
			}

			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Setup the grid columns.
		private void SetupGridSource()
		{
			// Setup the default grid columns.
			if (0 == DataSourceGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					DataSource.ColumnName,
					DataSource.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsProcess
					= Managers.DataSourceManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				DataSourceGrid.LJCAddColumns(mGridColumnsProcess);
				//DataSourceGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsProcess;

		// Saves the control values. 
		internal void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			DataSourceGrid.LJCSaveColumnValues(controlValues);

			// Save Window values.
			controlValues.Add(Name, Left, Top, Width, Height);

			// Save other values.

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

					// Restore Splitter and other values.
					DataSourceGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		// Gets or sets the ControlValues item.
		internal ControlValues ControlValues { get; set; }
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
					//SetupGridSource();

					// Load first list.
					mDataSourceGridCode.DataRetrieveDataSource();
					break;

				case Change.DataSource:
					DataSourceGrid.LJCSetLastRow();
					DataSourceGrid.LJCSetCounter(DataSourceCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			DataSource
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
			bool enableEdit = DataSourceGrid.CurrentRow != null;
			FormCommon.SetToolState(DataSourceTool, enableNew, enableEdit);
			FormCommon.SetMenuState(DataSourceMenu, enableNew, enableEdit);
		}
		#endregion

		#region Properties

		// Gets or sets the Parent ID value.
		internal int LJCParentID { get; set; }

		// Gets or sets the LJCParentName value.
		internal string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		// Gets or sets the LJCIsSelect value.
		internal bool LJCIsSelect { get; set; }

		// Gets a reference to the selected record.
		internal DataSource LJCSelectedRecord { get; set; }

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private DataSourceGridCode mDataSourceGridCode;
		#endregion
	}
}
