// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupModuleCode.cs
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
	public partial class ProcessGroupModule : UserControl
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
			mGroupGridCode = new ProcessGroupGridCode(this);
			mProcessGridCode = new ModuleProcessGridCode(this);

			// Configure controls.
			DataProcessToolNew.ToolTipText = "Add";
			DataProcessToolDelete.ToolTipText = "Remove";
			DataProcessToolNew.Text = "&Add";
			DataProcessToolDelete.Text = "&Remove";

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\ProcessGroup.xml";

			SetupGridGroup();
			SetupGridProcess();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Setup the grid columns.
		private void SetupGridGroup()
		{
			// Setup the default grid columns.
			if (0 == ProcessGroupGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					ProcessGroup.ColumnName,
					ProcessGroup.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsGroup
					= Managers.ProcessGroupManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				ProcessGroupGrid.LJCAddColumns(mGridColumnsGroup);
				//ProcessGroupGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsGroup;

		// Setup the grid columns.
		private void SetupGridProcess()
		{
			// Setup the default grid columns.
			if (0 == DataProcessGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					DataProcess.ColumnName,
					DataProcess.ColumnDescription,
					DataProcess.ColumnProcessStatusID
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsProcess
					= Managers.DataProcessManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				DataProcessGrid.LJCAddColumns(mGridColumnsProcess);
			}
		}
		private DbColumns mGridColumnsProcess;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();
			Control parent = ProcessGroupTabs.Parent;

			// Save Grid Column values.
			ProcessGroupGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("GroupSplit.SplitterDistance", 0, 0, 0
				, GroupSplit.SplitterDistance);

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
			Control parent = ProcessGroupTabs.Parent;

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

					// Restore Splitter and other values.
					FormCommon.RestoreSplitDistance(GroupSplit, ControlValues);

					ProcessGroupGrid.LJCRestoreColumnValues(ControlValues);
					DataProcessGrid.LJCRestoreColumnValues(ControlValues);
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
			return ProcessGroupTabs;
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
					GroupSplit.SplitterWidth = 5;
					RestoreControlValues();
					//ProcessGroupGrid.LJCRestoreColumnValues(ControlValues);
					//DataProcessGrid.LJCRestoreColumnValues(ControlValues);

					// Setup primary grid here if no ViewData.
					//SetupGridGroup();

					// Load first list.
					mGroupGridCode.DataRetrieveProcessGroup();
					break;

				case Change.ProcessGroup:
					mProcessGridCode.DataRetrieveDataProcess();
					ProcessGroupGrid.LJCSetLastRow();
					ProcessGroupGrid.LJCSetCounter(ProcessGroupCounter);
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
			ProcessGroup,
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
			bool enableEdit = ProcessGroupGrid.CurrentRow != null;
			FormCommon.SetToolState(ProcessGroupTool, enableNew, enableEdit);
			FormCommon.SetMenuState(ProcessGroupMenu, enableNew, enableEdit);
			ProcessGroupFileEdit.Enabled = true;

			enableNew = ProcessGroupGrid.CurrentRow != null;
			enableEdit = DataProcessGrid.CurrentRow != null;
			FormCommon.SetToolState(DataProcessTool, enableNew, enableEdit);
			FormCommon.SetMenuState(DataProcessMenu, enableNew, enableEdit);
		}
		#endregion

		#region Properties

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private ProcessGroupGridCode mGroupGridCode;
		private ModuleProcessGridCode mProcessGridCode;
		#endregion
	}
}
