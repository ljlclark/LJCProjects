// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MainListCode.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCSQLUtilLibDAL;
using LJCDBClientLib;
using DataDetail;

namespace DataHelper
{
	public partial class MainList : Form
	{
		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../LJCGenDoc/Common/List.xml'/>
		private void InitializeControls()
		{
			Cursor = Cursors.WaitCursor;
      // Set DAL config before using anywhere in the program.
      var configValues = ValuesDataHelper.Instance;
			mSettings = configValues.StandardSettings;
      Text += $" - {mSettings.DataConfigName}";

      // Initialize Class Data.
      Managers = new SQLUtilLibManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mTableGridCode = new TableGridCode(this);
			mColumnGridCode = new ColumnGridCode(this);
			mKeyGridCode = new KeyGridCode(this);
			mDataGridCode = new DataGridCode(this);
			mChildGridCode = new ChildGridCode(this);

			// Load control data.

			// Configure controls.
			DataSplit.Resize += DataSplit_Resize;

			// Set initial control values.
			NetFile.CreateFolder("ExportFiles");
			NetFile.CreateFolder("DetailConfigs");
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = "ControlValues\\MainList.xml";

			SetupGridTable();
			SetupGridColumn();
			SetupGridKey();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Setup panel manager for main tab splitter.
			//mTablePanelManager = new LJCPanelManager(TableTabsSplit, DataTabs, DataTileTabs);
			//mColumnPanelManager = new LJCPanelManager(ColumnTabsSplit, ColumnTabs, ColumnTileTabs);

			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				TableTabsSplit.SplitterWidth = 5;
				TableSplit.SplitterWidth = 5;
				DataSplit.SplitterWidth = 5;
			}
		}

		// Setup the grid columns.
		private void SetupGridTable()
		{
			TableGrid.BackgroundColor = mSettings.BeginColor;

			if (0 == TableGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					DbMetaDataTable.ColumnName,
					DbMetaDataTable.ColumnDescription,
					DbMetaDataTable.ColumnCaption
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsTable
					= Managers.DbMetaDataTableManager.GetColumns(propertyNames);

				// Setup the grid columns.
				TableGrid.LJCAddColumns(mGridColumnsTable);
			}
		}
		private DbColumns mGridColumnsTable;

		// Setup the grid columns.
		private void SetupGridColumn()
		{
			ColumnGrid.BackgroundColor = mSettings.BeginColor;

			if (0 == ColumnGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					DbMetaDataColumn.ColumnColumnName,
					DbMetaDataColumn.ColumnDescription,
					DbMetaDataColumn.ColumnCaption
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsColumn
					= Managers.DbMetaDataColumnManager.GetColumns(propertyNames);

				// Setup the grid columns.
				ColumnGrid.LJCAddColumns(mGridColumnsColumn);
			}
		}
		private DbColumns mGridColumnsColumn;

		// Setup the grid columns.
		private void SetupGridKey()
		{
			KeyGrid.BackgroundColor = mSettings.BeginColor;

			if (0 == KeyGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					DbMetaDataKey.ColumnColumnName,
					DbMetaDataKey.ColumnToTableName,
					DbMetaDataKey.ColumnToColumnName
				};

				// Get the grid columns from the manager Data Definition.
				mGridColumnsKey
					= Managers.DbMetaDataKeyManager.GetColumns(propertyNames);

				// Setup the grid columns.
				KeyGrid.LJCAddColumns(mGridColumnsKey);
			}
		}
		private DbColumns mGridColumnsKey;

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			TableGrid.LJCSaveColumnValues(controlValues);
			ColumnGrid.LJCSaveColumnValues(controlValues);
			KeyGrid.LJCSaveColumnValues(controlValues);
			DataGrid.LJCSaveColumnValues(controlValues);
			ChildGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("TableSplit.SplitterDistance", 0, 0, 0
				, TableSplit.SplitterDistance);
			controlValues.Add("DataSplit.SplitterDistance", 0, 0, 0
				, DataSplit.SplitterDistance);

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
				try
				{
					ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
						, mControlValuesFileName) as ControlValues;
				}
				catch (Exception e)
				{
					StringBuilder build = new StringBuilder(128);
					build.AppendLine("The Control Values could not be restored.");
					build.AppendLine("The program will continue.");
					build.AppendLine(NetString.ExceptionString(e));
					string message = build.ToString();
					MessageBox.Show(message, "Deserialize Notification", MessageBoxButtons.OK
						, MessageBoxIcon.Information);
				}

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
					FormCommon.RestoreSplitDistance(TableSplit, ControlValues);
					FormCommon.RestoreSplitDistance(DataSplit, ControlValues);

					TableGrid.LJCRestoreColumnValues(ControlValues);
					ColumnGrid.LJCRestoreColumnValues(ControlValues);
					KeyGrid.LJCRestoreColumnValues(ControlValues);
					DataGrid.LJCRestoreColumnValues(ControlValues);
					ChildGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		/// <summary>Gets or sets the ControlValues item.</summary>
		public ControlValues ControlValues { get; set; }

		// Splitter is not in the first TabPage so Set values on first display.
		private void DataSplit_Resize(object sender, EventArgs e)
		{
			if (ControlValues != null)
			{
				if (false == mIsDataSplitSet)
				{
					FormCommon.RestoreSplitDistance(DataSplit, ControlValues);
				}
				mIsDataSplitSet = true;
			}
		}
		private bool mIsDataSplitSet;
		#endregion

		#region Item Change Processing

		/// <summary>
		/// Execute the related item functions.
		/// </summary>
		/// <param name="changeName">The ChangeName value.</param>
		public void DoChange(string changeName)
		{
			Cursor = Cursors.WaitCursor;
			switch (changeName)
			{
				case ChangeStartup:
					ConfigureControls();
					RestoreControlValues();

					// Load first List.
					mTableGridCode.DataRetrieveTable();
					break;

				case ChangeTable:
					TableGrid.LJCSetLastRow();
					mColumnGridCode.DataRetrieveColumn();
					break;

				case ChangeColumn:
					ColumnGrid.LJCSetLastRow();
					mKeyGridCode.DataRetrieveKey();
					break;

				case ChangeKey:
					KeyGrid.LJCSetLastRow();
					break;

				case ChangeData:
					DataGrid.LJCSetLastRow();
					mChildGridCode.DataRetrieveChild();
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// Sets the control states based on the current control values.
		private void SetControlState()
		{
			bool enableNew = true;
			bool enableEdit = TableGrid.CurrentRow != null;
			FormCommon.SetMenuState(TableMenu, enableNew, enableEdit);
		}

		#region Change Constants

		/// <summary>The Table Change Name.</summary>
		public const string ChangeTable = "Table";

		/// <summary>The Column Change Name.</summary>
		public const string ChangeColumn = "Column";

		/// <summary>The Key Change Name.</summary>
		public const string ChangeKey = "Key";

		/// <summary>The Data Change Name.</summary>
		public const string ChangeData = "Data";

		/// <summary>The Child Change Name.</summary>
		public const string ChangeChild = "Child";
		#endregion

		#region Item Change Support

		// Start the Change processing.
		private void StartItemChange()
		{
			ChangeTimer = new ChangeTimer();
			ChangeTimer.ItemChange += ChangeTimer_ItemChange;
			ChangeTimer.DoChange(ChangeStartup);
		}

		// Change Event Handler
		private void ChangeTimer_ItemChange(object sender, EventArgs e)
		{
			DoChange(ChangeTimer.ChangeName);
		}

		/// <summary>Gets or sets the ChangeTimer object.</summary>
		public ChangeTimer ChangeTimer { get; set; }

		/// <summary>The Startup Change Name.</summary>
		private const string ChangeStartup = "Startup";
		#endregion
		#endregion

		#region Properties

		// The Managers object.
		internal SQLUtilLibManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;

		//private LJCPanelManager mTablePanelManager;
		//private LJCPanelManager mColumnPanelManager;

		private TableGridCode mTableGridCode;
		private ColumnGridCode mColumnGridCode;
		private KeyGridCode mKeyGridCode;
		private DataGridCode mDataGridCode;
		private ChildGridCode mChildGridCode;
		#endregion
	}
}
