// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumnListCode.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	// The list form.
	internal partial class LayoutColumnList : Form
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
			mLayoutGridCode = new LayoutGridCode(this);
			mLayoutColumnGridCode = new LayoutColumnGridCode(this);

			// Configure controls.
			if (LJCIsSelect)
			{
				// This is a Selection List.
				Text = "Layout Column Selection";
				LayoutMenuEdit.ShortcutKeyDisplayString = "";
				LayoutMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
			}
			else
			{
				// This is a display list.
				Text = "Layout Column List";
				LayoutMenuSelectSeparator.Visible = false;
				LayoutMenuSelect.Visible = false;
			}

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = "ControlValues\\LayoutColumnList.xml";

			try
			{
				SetupGridLayout();
			}
			catch (SystemException e)
			{
				TransformCommon.CreateTables(e, mSettings.DataConfigName);
				SetupGridLayout();
			}

			SetupGridLayoutColumn();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				LayoutSplit.SplitterWidth = 5;

				// Modify MainSplit.Panel1 controls.
				ListHelper.SetPanelControls(LayoutSplit.Panel1, LayoutHeader
					, LayoutToolPanel, LayoutGrid);

				// Modify MainSplit.Panel2 controls.
				ListHelper.SetPanelControls(LayoutSplit.Panel2, LayoutColumnHeader
					, LayoutColumnToolPanel, LayoutColumnGrid);
			}
		}

		// Setup the grid columns.
		private void SetupGridLayout()
		{
			// Setup the default grid columns.
			if (0 == LayoutGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					SourceLayout.ColumnName,
					SourceLayout.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsLayout
					= Managers.SourceLayoutManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				LayoutGrid.LJCAddColumns(mGridColumnsLayout);
				//LayoutGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsLayout;

		// Setup the grid columns.
		private void SetupGridLayoutColumn()
		{
			// Setup the default grid columns.
			if (0 == LayoutColumnGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					LayoutColumn.ColumnName,
					LayoutColumn.ColumnDescription,
					LayoutColumn.ColumnSequence
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsLayoutColumn
					= Managers.LayoutColumnManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				LayoutColumnGrid.LJCAddColumns(mGridColumnsLayoutColumn);
				LayoutColumnGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsLayoutColumn;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			LayoutGrid.LJCSaveColumnValues(controlValues);
			LayoutColumnGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("LayoutSplit.SplitterDistance", 0, 0, 0
				, LayoutSplit.SplitterDistance);

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
					FormCommon.RestoreSplitDistance(LayoutSplit, ControlValues);

					LayoutGrid.LJCRestoreColumnValues(ControlValues);
					LayoutColumnGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		/// <summary>Gets or sets the ControlValues item.</summary>
		public ControlValues ControlValues { get; set; }
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
					//LayoutGrid.LJCRestoreColumnValues(ControlValues);
					//LayoutColumnGrid.LJCRestoreColumnValues(ControlValues);

					// Load first list.
					mLayoutGridCode.DataRetrieveLayout();
					break;

				case Change.Layout:
					mLayoutColumnGridCode.DataRetrieveLayoutColumn();
					LayoutGrid.LJCSetLastRow();
					LayoutGrid.LJCSetCounter(LayoutCounter);
					break;

				case Change.LayoutColumn:
					LayoutColumnGrid.LJCSetLastRow();
					LayoutColumnGrid.LJCSetCounter(LayoutColumnCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Layout,
			LayoutColumn
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
			bool enableEdit = LayoutGrid.CurrentRow != null;
			FormCommon.SetToolState(LayoutTool, enableNew, enableEdit);
			FormCommon.SetMenuState(LayoutMenu, enableNew, enableEdit);

			enableNew = LayoutGrid.CurrentRow != null;
			enableEdit = LayoutColumnGrid.CurrentRow != null;
			FormCommon.SetToolState(LayoutColumnTool, enableNew, enableEdit);
			FormCommon.SetMenuState(LayoutColumnMenu, enableNew, enableEdit);
			LayoutColumnMenuImport.Enabled = true;
		}
		#endregion

		#region Properties

		// Gets or sets the LJCIsSelect value.
		internal bool LJCIsSelect { get; set; }

		// Gets a reference to the selected record.
		internal SourceLayout LJCSelectedRecord { get; set; }

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private LayoutGridCode mLayoutGridCode;
		private LayoutColumnGridCode mLayoutColumnGridCode;
		#endregion
	}
}
