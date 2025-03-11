// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessStepModuleCode.cs
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
	public partial class ProcessStepModule : UserControl
	{
		#region Data Methods

		// Retrieves the list rows.
		private void DataRetrieveDataProcess()
		{
			DataProcesses records;

			Cursor = Cursors.WaitCursor;
			DataProcessCombo.Items.Clear();

			var dataProcessManager = Managers.DataProcessManager;
			records = dataProcessManager.Load();

			if (NetCommon.HasItems(records))
			{
				foreach (DataProcess record in records)
				{
					DataProcessCombo.LJCAddItem(record.DataProcessID, record.Description);
				}

				if (DataProcessCombo.Items.Count > 0
					&& DataProcessCombo.SelectedIndex < 0)
				{
					DataProcessCombo.SelectedIndex = 0;
				}
			}
			Cursor = Cursors.Default;
		}
		#endregion

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
			mStepGridCode = new ModuleStepGridCode(this);
			mTaskGridCode = new ModuleTaskGridCode(this);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\ProcessStep.xml";
			mRefreshTimer = new Timer()
			{
				Interval = 2000
			};
			mRefreshTimer.Tick += RefreshTimer_Tick;
			mRefreshTimer.Start();

			SetupGridStep();
			SetupGridTask();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		//
		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			//WriteLogLine("RefreshTimer_Tick");
			mStepGridCode.RefreshStatusStep();
			mTaskGridCode.RefreshStatusTask();
		}

		// Setup the grid columns.
		private void SetupGridStep()
		{
			// Setup the default grid columns.
			if (0 == StepGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					Step.ColumnName,
					Step.ColumnDescription,
					Step.ColumnSequence,
					Step.ColumnStatusID
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsStep
					= Managers.StepManager.GetColumns(propertyNames);

				// Setup the grid columns.
				StepGrid.LJCAddColumns(mGridColumnsStep);
				//StepGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsStep;

		// Setup the grid columns.
		private void SetupGridTask()
		{
			// Setup the default grid columns.
			if (0 == TaskGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					StepTask.ColumnName,
					StepTask.ColumnDescription,
					StepTask.ColumnSequence,
					StepTask.ColumnTaskStatusID
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsTask
					= Managers.StepTaskManager.GetColumns(propertyNames);

				// Setup the grid columns.
				TaskGrid.LJCAddColumns(mGridColumnsTask);
				//TaskGrid.LJCRestoreColumnValues(ControlValues);
			}
		}
		private DbColumns mGridColumnsTask;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();
			Control parent = ProcessStepTabs.Parent;

			// Save Grid Column values.
			StepGrid.LJCSaveColumnValues(controlValues);
			TaskGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("StepSplit.SplitterDistance", 0, 0, 0
				, StepSplit.SplitterDistance);

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
			Control parent = ProcessStepTabs.Parent;

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

					// Restore Splitter, Grid and other values.
					FormCommon.RestoreSplitDistance(StepSplit, ControlValues);

					StepGrid.LJCRestoreColumnValues(ControlValues);
					TaskGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		/// <summary>Gets or sets the ControlValues item.</summary>
		public ControlValues ControlValues { get; set; }
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
			return ProcessStepTabs;
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
					StepSplit.SplitterWidth = 5;
					RestoreControlValues();
					//StepGrid.LJCRestoreColumnValues(ControlValues);
					//TaskGrid.LJCRestoreColumnValues(ControlValues);

					// Load first list.
					DataRetrieveDataProcess();
					break;

				case Change.Process:
					mStepGridCode.DataRetrieveStep();
					break;

				case Change.Step:
					mTaskGridCode.DataRetrieveTask();
					StepGrid.LJCSetLastRow();
					StepGrid.LJCSetCounter(StepCounter);
					break;

				case Change.Task:
					TaskGrid.LJCSetLastRow();
					TaskGrid.LJCSetCounter(TaskCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Process,
			Step,
			Task
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
			bool enableEdit = StepGrid.CurrentRow != null;
			FormCommon.SetToolState(StepTool, enableNew, enableEdit);
			FormCommon.SetMenuState(StepMenu, enableNew, enableEdit);

			enableNew = StepGrid.CurrentRow != null;
			enableEdit = TaskGrid.CurrentRow != null;
			FormCommon.SetToolState(TaskTool, enableNew, enableEdit);
			FormCommon.SetMenuState(TaskMenu, enableNew, enableEdit);
		}
		#endregion

		#region Properties

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private ModuleStepGridCode mStepGridCode;
		private ModuleTaskGridCode mTaskGridCode;
		private Timer mRefreshTimer;
		#endregion
	}
}
