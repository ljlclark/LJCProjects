// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskSourceModuleCode.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	public partial class TaskSourceModule : UserControl
	{
		#region Data Methods

		#region DataProcess

		// Retrieves the list rows.
		private void DataRetrieveDataProcess()
		{
			DataProcesses records;

			Cursor = Cursors.WaitCursor;
			DataProcessCombo.Items.Clear();

			DataProcessManager dataProcessManager = Managers.DataProcessManager;
			records = dataProcessManager.Load();

			if (NetCommon.HasItems(records))
			{
				foreach (DataProcess record in records)
				{
					string description = record.Description;
					if (!NetString.HasValue(description))
					{
						description = "Missing Description";
					}
					DataProcessCombo.LJCAddItem(record.DataProcessID, description);
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

		#region Step

		// Retrieves the list rows.
		private void DataRetrieveStep()
		{
			Steps records;

			Cursor = Cursors.WaitCursor;
			StepCombo.Items.Clear();

			if (DataProcessCombo.SelectedIndex >= 0)
			{
				// Data from items.
				int id = DataProcessCombo.LJCSelectedItemID();

				StepManager stepManager = Managers.StepManager;
				records = stepManager.LoadWithProcessID(id);

				if (NetCommon.HasItems(records))
				{
					foreach (Step record in records)
					{
						string description = record.Description;
						if (!NetString.HasValue(description))
						{
							description = "Missing Description";
						}
						StepCombo.LJCAddItem(record.StepID, description);
					}
				}

				if (StepCombo.Items.Count > 0
					&& StepCombo.SelectedIndex < 0)
				{
					StepCombo.SelectedIndex = 0;
				}
			}
			Cursor = Cursors.Default;
		}
		#endregion
		#endregion

		#region DataRow Methods

		// Retrieves the StepTask record.
		private StepTask GetRowStepTask(LJCGridRow row)
		{
			StepTask retValue = null;

			int id = row.LJCGetInt32(StepTask.ColumnStepTaskID);
			if (id > 0)
			{
				StepTaskManager taskManager = Managers.StepTaskManager;
				retValue = taskManager.RetrieveWithID(id);
			}
			return retValue;
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
			mDataViewer = new DataViewer(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Initialize Grid Code.
			mTaskGridCode = new TaskGridCode(this);
			mTransformGridCode = new TransformGridCode(this);
			mSourceGridCode = new SourceGridCode(this);

			// Configure controls.
			SourceToolNew.ToolTipText = "Add";
			SourceToolDelete.ToolTipText = "Remove";
			SourceToolNew.Text = "&Add";
			SourceToolDelete.Text = "&Remove";
			DataSourceMenuNew.ToolTipText = "Add";
			DataSourceMenuDelete.ToolTipText = "Remove";
			DataSourceMenuNew.Text = "&Add";
			DataSourceMenuDelete.Text = "&Remove";

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\TaskSource.xml";

			try
			{
				SetupGridTask();
			}
			catch (SystemException e)
			{
				TransformCommon.CreateTables(e, mSettings.DataConfigName);
				SetupGridTask();
			}

			SetupGridTransform();
			SetupGridDataSource();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			TaskSplit.SplitterDistance = TaskSplit.Height / 2;

			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				TaskSplit.SplitterWidth = 5;
				DataProcessButton.Top = DataProcessCombo.Top + 1;
				StepButton.Top = StepCombo.Top + 1;

				// Modify MainSplit.Panel1 controls.
				ListHelper.SetPanelControls(TaskSplit.Panel1, TaskHeader, TaskToolPanel
				, TaskGrid, gridTop: -1);
			}
		}

		// Setup the grid columns.
		private void SetupGridTask()
		{
			// Setup the default grid columns.
			if (0 == TaskGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					StepTask.ColumnName,
					StepTask.ColumnDescription,
					StepTask.ColumnSequence
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsTask
					= Managers.StepTaskManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				TaskGrid.LJCAddColumns(mGridColumnsTask);
			}
		}
		private DbColumns mGridColumnsTask;

		// Setup the grid columns.
		private void SetupGridTransform()
		{
			// Setup the default grid columns.
			if (0 == TransformGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					TaskTransform.ColumnName,
					TaskTransform.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsTransform
					= Managers.TaskTransformManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				TransformGrid.LJCAddColumns(mGridColumnsTransform);
			}
		}
		private DbColumns mGridColumnsTransform;

		// Setup the grid columns.
		private void SetupGridDataSource()
		{
			// Setup the default grid columns.
			if (0 == DataSourceGrid.ColumnCount)
			{
				List<string> propertyNames = new List<string> {
					DataSource.ColumnName,
					DataSource.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsDataSource
					= Managers.DataSourceManager.GetColumns(propertyNames);

				// Setup the grid columns and column values.
				DataSourceGrid.LJCAddColumns(mGridColumnsDataSource);
			}
		}
		private DbColumns mGridColumnsDataSource;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();
			Control parent = TaskSourceTabs.Parent;

			// Save Grid Column values.
			TaskGrid.LJCSaveColumnValues(controlValues);
			TransformGrid.LJCSaveColumnValues(controlValues);
			DataSourceGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("TaskSplit.SplitterDistance", 0, 0, 0
				, TaskSplit.SplitterDistance);

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
			Control parent = TaskSourceTabs.Parent;

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
					FormCommon.RestoreSplitDistance(TaskSplit, ControlValues);

					TaskGrid.LJCRestoreColumnValues(ControlValues);
					TransformGrid.LJCRestoreColumnValues(ControlValues);
					DataSourceGrid.LJCRestoreColumnValues(ControlValues);
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
			return TaskSourceTabs;
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

					// Load first list.
					DataRetrieveDataProcess();
					break;

				case Change.Process:
					DataRetrieveStep();
					break;

				case Change.Step:
					mTaskGridCode.DataRetrieveTask();
					break;

				case Change.Task:
					mTransformGridCode.DataRetrieveTransform();
					mSourceGridCode.DataRetrieveDataSource();
					TaskGrid.LJCSetLastRow();
					TaskGrid.LJCSetCounter(TaskCounter);
					break;

				case Change.TaskTransform:
					TransformGrid.LJCSetLastRow();
					TransformGrid.LJCSetCounter(TransformCounter);
					break;

				case Change.DataSource:
					DataSourceGrid.LJCSetLastRow();
					DataSourceGrid.LJCSetCounter(SourceCounter);
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
			Task,
			TaskTransform,
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
			bool enableEdit = TaskGrid.CurrentRow != null;
			FormCommon.SetToolState(TaskTool, enableNew, enableEdit);
			FormCommon.SetMenuState(TaskMenu, enableNew, enableEdit);

			enableNew = TaskGrid.CurrentRow != null;
			enableEdit = TransformGrid.CurrentRow != null;
			FormCommon.SetToolState(TransformTool, enableNew, enableEdit);
			FormCommon.SetMenuState(TransformMenu, enableNew, enableEdit);

			enableEdit = DataSourceGrid.CurrentRow != null;
			FormCommon.SetToolState(SourceTool, enableNew, enableEdit);
			FormCommon.SetMenuState(DataSourceMenu, enableNew, enableEdit);

			if (DataSourceGrid.CurrentRow is LJCGridRow sourceRow)
			{
				DataSource dataSource = mSourceGridCode.GetRowDataSource(sourceRow);
				if (dataSource != null)
				{
					DataSourceMenuDrop.Enabled = false;
					if (dataSource.SourceTypeID == TableDataSource)
					{
						DataSourceMenuDrop.Enabled = true;
					}
				}
			}
		}
		#endregion

		#region Properties

		// The Managers object.
		internal TransformManagers Managers { get; set; }
		#endregion

		#region Class Data

		//private const int FileDataSource = 1;
		private const int TableDataSource = 2;

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private TaskGridCode mTaskGridCode;
		private TransformGridCode mTransformGridCode;
		private SourceGridCode mSourceGridCode;

		private DataViewer mDataViewer;
		private DataProcess mDataProcess;
		#endregion
	}
}
