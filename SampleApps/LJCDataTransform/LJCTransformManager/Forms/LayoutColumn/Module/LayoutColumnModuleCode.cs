// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumnModuleCode.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	// The tab composite user control.
	public partial class LayoutColumnModule : UserControl
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
			mLayoutGridCode = new ModuleLayoutGridCode(this);
			mLayoutColumnGridCode = new ModuleLayoutColumnGridCode(this);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\LayoutColumnModule.xml";

			SetupGridLayout();
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
				ListHelper.SetPanelControls(LayoutSplit.Panel1, null, LayoutToolPanel
					, LayoutGrid);

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
				//LayoutColumnGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsLayoutColumn;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();
			Control parent = LayoutTabs.Parent;

			// Save Grid Column values.
			LayoutGrid.LJCSaveColumnValues(controlValues);
			LayoutGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("LayoutSplit.SplitterDistance", 0, 0, 0
				, LayoutSplit.SplitterDistance);

			// Save Window values.
			// Tabs Parent is not this module form.
			if (parent != null
				&& parent.GetType().Name != Name)
			{
				controlValues.Add(Name, parent.Left, parent.Top
					, parent.Width, parent.Height);
			}

			NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
				, mControlValuesFileName);
		}

		// Restores the control values.
		private void RestoreControlValues()
		{
			ControlValue controlValue;
			Control parent = LayoutTabs.Parent;

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
						// Tabs Parent is not this module form.
						if (parent != null
								&& parent.GetType().Name != Name)
						{
							parent.Left = controlValue.Left;
							parent.Top = controlValue.Top;
							parent.Width = controlValue.Width;
							parent.Height = controlValue.Height;
						}
					}

					// Restore Splitter Grid and other values.
					FormCommon.RestoreSplitDistance(LayoutSplit, ControlValues);

					LayoutGrid.LJCRestoreColumnValues(ControlValues);
					LayoutColumnGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		// Gets or sets the ControlValues item.
		internal ControlValues ControlValues { get; set; }
		#endregion

		#region AppModule Implementation

		// Initializes the module.
		/// <include path='items/LJCInit/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public void LJCInit()
		{
			if (null == CloseTabPage)
			{
				InitializeControls();
			}
		}

		// Returns a reference to the module tab control.
		/// <include path='items/LJCTabs/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public TabControl LJCTabs()
		{
			return LayoutTabs;
		}

		/// <summary>Gets the module assembly name.</summary>
		public string LJCProgramName
		{
			get
			{
				string name = Assembly.GetExecutingAssembly().FullName;
				name = name.Substring(0, name.IndexOf(',')) + ".exe";
				return name;
			}
		}

		// Closes the current page.
		/// <include path='items/ClosePage/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public void ClosePage(TabPage tabPage)
		{
			LJCTabControl parentTabControl;

			// Set current page and invoke event.
			CloseTabPage = tabPage;
			OnPageClose();

			// Collapse tile panel if no tab pages left.
			parentTabControl = tabPage.Parent as LJCTabControl;
			parentTabControl?.LJCCloseEmptyPanel();
		}

		/// <summary>Gets or sets the close tab flag.</summary>
		public TabPage CloseTabPage { get; set; }

		/// <summary>Calls the PageClose event handlers.</summary>
		protected void OnPageClose()
		{
			PageClose?.Invoke(this, new EventArgs());
		}

		/// <summary>The page close event.</summary>
		public event EventHandler<EventArgs> PageClose;
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
		}
		#endregion

		#region Properties

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private ModuleLayoutGridCode mLayoutGridCode;
		private ModuleLayoutColumnGridCode mLayoutColumnGridCode;
		#endregion
	}
}
