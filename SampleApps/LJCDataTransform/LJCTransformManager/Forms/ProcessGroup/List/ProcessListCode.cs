﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessListCode.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	internal partial class DataProcessList : Form
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
			mProcessGridCode = new DataProcessGridCode(this);

			// Configure controls.
			if (LJCIsSelect)
			{
				// This is a Selection List.
				Text = "DataProcess Selection";
				DataProcessMenuEdit.ShortcutKeyDisplayString = "";
				DataProcessMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
			}
			else
			{
				// This is a display list.
				Text = "DataProcess List";
				DataProcessMenuSelect.Visible = false;
			}

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\DataProcess.xml";

			try
			{
				SetupGridProcess();
			}
			catch (SystemException e)
			{
				TransformCommon.CreateTables(e, mSettings.DataConfigName);
				SetupGridProcess();
			}

			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Setup the grid columns.
		private void SetupGridProcess()
		{
			// Setup the default grid columns.
			if (0 == DataProcessGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					DataProcess.ColumnName,
					DataProcess.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsProcess
					= Managers.DataProcessManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				DataProcessGrid.LJCAddColumns(mGridColumnsProcess);
				//DataProcessGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsProcess;

		// Saves the control values. 
		internal void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			DataProcessGrid.LJCSaveColumnValues(controlValues);

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
					DataProcessGrid.LJCRestoreColumnValues(ControlValues);
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

					// Load first list.
					mProcessGridCode.DataRetrieveDataProcess();
					break;

				case Change.DataProcess:
					DataProcessGrid.LJCSetLastRow();
					DataProcessGrid.LJCSetCounter(DataProcessCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			DataProcess
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
			bool enableEdit = DataProcessGrid.CurrentRow != null;
			FormCommon.SetToolState(DataProcessTool, enableNew, enableEdit);
			FormCommon.SetMenuState(DataProcessMenu, enableNew, enableEdit);
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
		internal DataProcess LJCSelectedRecord { get; set; }

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private DataProcessGridCode mProcessGridCode;
		#endregion
	}
}
