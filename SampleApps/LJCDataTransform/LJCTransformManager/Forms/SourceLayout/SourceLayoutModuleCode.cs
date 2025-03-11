// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceLayoutModuleCode.cs
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
	public partial class SourceLayoutModule : UserControl
	{
		#region Data Methods

		#region DataSource

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveDataSource()
		{
			DataSources records;

			Cursor = Cursors.WaitCursor;
			DataSourceCombo.Items.Clear();

			var dataSourceManager = Managers.DataSourceManager;
			records = dataSourceManager.Load();

			if (NetCommon.HasItems(records))
			{
				foreach (DataSource record in records)
				{
					DataSourceCombo.LJCAddItem(record.DataSourceID, record.Description);
				}

				// Select startup record.
				if (DataSourceCombo.Items.Count > 0
					&& DataSourceCombo.SelectedIndex < 0)
				{
					DataSourceCombo.SelectedIndex = 0;
				}
			}
			Cursor = Cursors.Default;
		}
		#endregion

		#region Layout

		// Retrieves the item.
		private void DataRetrieveLayout()
		{
			SourceLayout record;

			Cursor = Cursors.WaitCursor;

			if (DataSourceCombo.SelectedIndex > -1)
			{
				// Data from items.
				int parentID = DataSourceCombo.LJCSelectedItemID();

				var dataSourceManager = Managers.DataSourceManager;
				DataSource dataSource = dataSourceManager.RetrieveWithID(parentID);
				if (dataSource != null)
				{
					int sourceLayoutID = dataSource.SourceLayoutID;
					var layoutManager = Managers.SourceLayoutManager;
					record = layoutManager.RetrieveWithID(sourceLayoutID);
					if (record != null)
					{
						mSourceLayoutID = record.SourceLayoutID;
						SourceLayoutTextbox.Text = record.Description;
					}
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Layout);
		}
		#endregion
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
			mLayoutColumnGridCode = new SourceLayoutColumnGridCode(this);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\SourceLayout.xml";

			try
			{
				SetupGridLayoutColumn();
			}
			catch (SystemException e)
			{
				TransformCommon.CreateTables(e, mSettings.DataConfigName);
				SetupGridLayoutColumn();
			}

			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				SourceLayoutButton.Top = SourceLayoutTextbox.Top + 1;
			}
		}

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
			}
		}
		private DbColumns mGridColumnsLayoutColumn;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();
			Control parent = SourceLayoutTabs.Parent;

			// Save Grid Column values.
			LayoutColumnGrid.LJCSaveColumnValues(controlValues);

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
			Control parent = SourceLayoutTabs.Parent;

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
			return SourceLayoutTabs;
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
					DataRetrieveDataSource();
					break;

				case Change.DataSource:
					DataRetrieveLayout();
					break;

				case Change.Layout:
					mLayoutColumnGridCode.DataRetrieveLayoutColumn();
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
			DataSource,
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
			bool enableNew = mSourceLayoutID > 0;
			bool enableEdit = LayoutColumnGrid.CurrentRow != null;
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
		private SourceLayoutColumnGridCode mLayoutColumnGridCode;
		internal int mSourceLayoutID;
		#endregion
	}
}
