// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableViewer.cs
using System;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using LJCGridDataLib;

namespace LJCTransformManager
{
	// The list form.
	/// <include path='items/ListForm/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
	public partial class TableViewer : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TableViewerC/*' file='Doc/TableViewer.xml'/>
		public TableViewer(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName)
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			DbServiceRef = dbServiceRef;
			DataConfigName = dataConfigName;
			TableName = tableName;
			Cursor = Cursors.Default;
		}

		// Resets the data access values.
		/// <include path='items/Reset/*' file='Doc/TableViewer.xml'/>
		public void Reset(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName)
		{
			mDataManager = new DataManager(DbServiceRef, DataConfigName, TableName);
			StartItemChange();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void TableViewer_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieve()
		{
			DbResult dbResult;

			Cursor = Cursors.WaitCursor;
			RecordsGrid.LJCRowsClear();

			dbResult = mDataManager.Load();

			if (HasRows(dbResult))
			{
				SetupGrid(dbResult);

				// Show Data.
				//mResultGridData.LoadRows(dbResult);
				foreach (DbRow dbRow in dbResult.Rows)
				{
					var gridRow = RecordsGrid.LJCRowAdd();
					gridRow.LJCSetValues(RecordsGrid, dbRow.Values);
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Records);
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void InitializeControls()
		{
			Cursor = Cursors.WaitCursor;

			// Initialize Class Data.
			mDataManager = new DataManager(DbServiceRef, DataConfigName, TableName);

			//mResultGridData = new ResultGridData(RecordsGrid);

			// Set initial control values.
			mControlValuesFileName = "ControlValues\\TableViewer.xml";
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				// Modify MainSplit.Panel1 controls.
				//ListHelper.SetPanelControls(_ClassName_Split.Panel1, _ClassName_Heading
				//	, _ClassName_ToolPanel, _ClassName_Grid);

				// Modify MainSplit.Panel2 controls.
				//ListHelper.SetPanelControls(_ClassName_Split.Panel2, ChildHeading
				//	, ChildToolPanel, ChildGrid);
			}
		}

		// Setup the grid columns.
		private void SetupGrid(DbResult dbResult)
		{
			// Get the grid columns from the manager Data Definition.
			DbColumns gridColumns = dbResult.Columns;
			RecordsGrid.LJCAddColumns(gridColumns);

			// Setup the grid columns.
			RecordsGrid.Columns.Clear();
			RecordsGrid.LJCAddColumns(gridColumns);
		}

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			RecordsGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.

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

					// The next line is for list windows only
					// The Form startup WindowState should be set to Minimized.
					WindowState = FormWindowState.Normal;

					if (controlValue != null)
					{
						Left = controlValue.Left;
						Top = controlValue.Top;
						Width = controlValue.Width;
						Height = controlValue.Height;
					}

					// Restore Splitter, Grid and other values.
					RecordsGrid.LJCRestoreColumnValues(ControlValues);
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

					// Load first List.
					DataRetrieve();
					RecordsGrid.LJCRestoreColumnValues(ControlValues);
					break;

				case Change.Records:
					RecordsGrid.LJCSetLastRow();
					RecordsGrid.LJCSetCounter(RecordsCounter);
					break;
			}
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Records
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

		// Checks if the result has records.
		private bool HasRows(DbResult dbResult)
		{
			bool retValue = false;

			if (HasRows(dbResult))
			{
				retValue = true;
			}
			return retValue;
		}
		#endregion

		#region Action Event Handlers

		// Drops the table.
		private void TableMenuDrop_Click(object sender, EventArgs e)
		{
			string message;

			string title = "Drop Table Confirmation";
			message = "Are you sure you want to Drop the table?";
			if (DialogResult.Yes == MessageBox.Show(message, title
				, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				string sql = $"drop table {TableName}";
				mDataManager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
				RecordsGrid.LJCRowsClear();
			}
		}

		// Performs the Close function.
		private void TableMenuExit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}
		#endregion

		#region Control Event Handlers

		// Handles the form keys.
		private void RecordsGrid_KeyDown(object sender, KeyEventArgs e)
		{
		}

		// Handles the MouseDown event.
		private void RecordsGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& RecordsGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				RecordsGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Records);
			}
		}

		// Handles the SelectionChanged event.
		private void RecordsGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (RecordsGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Records);
			}
			RecordsGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void RecordsGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the DbServiceRef object.</summary>
		public DbServiceRef DbServiceRef { get; set; }

		/// <summary>Gets or sets the DataConfig Name.</summary>
		public string DataConfigName { get; set; }

		/// <summary>Gets or sets the Table Name.</summary>
		public string TableName { get; set; }
		#endregion

		#region Class Data

		private const bool AllowTrue = true;

		private string mControlValuesFileName;
		private DataManager mDataManager;
		//private ResultGridData mResultGridData;
		#endregion
	}
}
