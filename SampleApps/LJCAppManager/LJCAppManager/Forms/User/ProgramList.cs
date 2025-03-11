// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramList.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBClientLib;
using LJCAppManagerDAL;

namespace LJCAppManager
{
	// The Program list form.
	/// <include path='items/ProgramList/*' file='Doc/ProgramList.xml'/>
	public partial class ProgramList : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProgramList()
		{
			InitializeComponent();
			LJCHelpFile = "AppManager.chm";
			LJCHelpPageList = "ProgramList.htm";
			LJCHelpPageDetail = "ProgramDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void ProgramList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		private void DataRetrieveProgram()
		{
			AppPrograms records;

			ProgramGrid.LJCRowsClear();
			var programManager = Managers.AppProgramManager;
			records = programManager.Load();

			if (NetCommon.HasItems(records))
			{
				foreach (AppProgram record in records)
				{
					RowAddProgram(record);
				}
			}
			DoChange(Change.AppProgram);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddProgram(AppProgram dataRecord)
		{
			LJCGridRow retValue;

			retValue = ProgramGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);
			retValue.LJCSetValues(ProgramGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateProgram(AppProgram dataRecord)
		{
			if (ProgramGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(ProgramGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, AppProgram dataRecord)
		{
			row.LJCSetInt32(AppProgram.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectProgram(AppProgram dataRecord)
		{
			int rowID;
			bool retVal = false;

			foreach (LJCGridRow row in ProgramGrid.Rows)
			{
				rowID = row.LJCGetInt32(AppProgram.ColumnID);
				if (rowID == dataRecord.ID)
				{
					ProgramGrid.LJCSetCurrentRow(row, AllowTrue);
					retVal = true;
					break;
				}
			}
			return retVal;
		}
		#endregion

		#region Action Methods

		// Performs the default list action.
		private void DoDefaultProgram()
		{
			if (LJCIsSelect)
			{
				DoSelect();
			}
			else
			{
				DoEditProgram();
			}
		}

		// Displays a detail dialog for a new record.
		private void DoNewProgram()
		{
			ProgramDetail detail;

			detail = new ProgramDetail()
			{
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = LJCHelpPageDetail
			};
			detail.LJCChange += new EventHandler<EventArgs>(ProgramDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		private void DoEditProgram()
		{
			ProgramDetail detail;

			if (ProgramGrid.CurrentRow is LJCGridRow row)
			{
				detail = new ProgramDetail()
				{
					LJCID = row.LJCGetInt32(AppProgram.ColumnID),
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = LJCHelpPageDetail
				};
				detail.LJCChange += new EventHandler<EventArgs>(ProgramDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		void ProgramDetail_Change(object sender, EventArgs e)
		{
			ProgramDetail detail;
			LJCGridRow row;

			detail = sender as ProgramDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateProgram(detail.LJCRecord);
			}
			else
			{
				row = RowAddProgram(detail.LJCRecord);
				ProgramGrid.LJCSetCurrentRow(row, AllowTrue);
				TimedChange(Change.AppProgram);
			}
		}

		// Deletes the selected row.
		private void DoDeleteProgram()
		{
			string title;
			string message;

			if (ProgramGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = "Are you sure you want to delete the selected item?";
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int id = row.LJCGetInt32(AppProgram.ColumnID);
					var programManager = Managers.AppProgramManager;
					var keyColumns = programManager.GetIDKey(id);
					programManager.Delete(keyColumns);
					if (programManager.AffectedCount > 0)
					{
						ProgramGrid.Rows.Remove(row);
						TimedChange(Change.AppProgram);
					}
				}
			}
		}

		// Refreshes the list.
		private void DoRefreshProgram()
		{
			AppProgram record;
			int id = 0;

			if (ProgramGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(AppProgram.ColumnID);
			}
			DataRetrieveProgram();

			// Select the original row.
			if (id > 0)
			{
				record = new AppProgram()
				{
					ID = id
				};
				RowSelectProgram(record);
			}
		}

		// Sets the selected item and returns to the parent form.
		private void DoSelect()
		{
			AppProgram record;
			int id;

			LJCSelectedRecord = null;
			if (ProgramGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(AppProgram.ColumnID);
				var programManager = Managers.AppProgramManager;
				var keyColumns = programManager.GetIDKey(id);
				record = programManager.Retrieve(keyColumns);
				if (record != null)
				{
					LJCSelectedRecord = record;
				}
			}
			DialogResult = DialogResult.OK;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesAppManager.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			Managers = new AppManagers(mSettings.DbServiceRef
			, mSettings.DataConfigName);

			// Configure controls.
			if (LJCIsSelect)
			{
				Text = "Program Selection";
				ProgramMenuEdit.ShortcutKeyDisplayString = "";
				ProgramMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
			}
			else
			{
				Text = "Program List";
				ProgramMenuSelect.Visible = false;
			}

			SetupGridProgram();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Setup the AppProgram grid.
		//private void SetupGridProgram(AppProgram record = null)
    private void SetupGridProgram()
    {
      ProgramGrid.BackgroundColor = mSettings.BeginColor;

			// Configure the first time only.
			//if (record != null
			if (0 == ProgramGrid.Columns.Count)
			{
				//DbColumns columns = GetGridColumns(record);
        DbColumns gridColumns = GetGridColumns();
        ProgramGrid.LJCAddColumns(gridColumns);
			}
		}

		//private DbColumns GetGridColumns(AppProgram module = null)
    private DbColumns GetGridColumns()
    {
      DbColumns retValue;

			retValue = new DbColumns();
			retValue.Add("FileName", caption: "File Name");
			retValue.Add("Title");
			return retValue;
		}
		#endregion

		#region Item Change Processing

		// Execute the list and related item functions.
		internal void DoChange(Change change)
		{
			Cursor = Cursors.WaitCursor;
			switch (change)
			{
				case Change.Startup:
					//RestoreControlValues();
					//ProgramGrid.LJCRestoreColumnValues(ControlValues);
					DataRetrieveProgram();
					break;

				case Change.AppProgram:
					ProgramGrid.LJCSetLastRow();
					ProgramGrid.LJCSetCounter(ProgramCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			AppProgram
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
			bool enableEdit = ProgramGrid.CurrentRow != null;
			FormCommon.SetToolState(ProgramTools, enableNew, enableEdit);
			FormCommon.SetMenuState(ProgramMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		// <summary> Call the New method.</summary>
		private void ProgramToolNew_Click(object sender, EventArgs e)
		{
			DoNewProgram();
		}

		// <summary> Call the Edit method.</summary>
		private void ProgramToolEdit_Click(object sender, EventArgs e)
		{
			DoEditProgram();
		}

		// <summary> Call the Delete method.</summary>
		private void ProgramToolDelete_Click(object sender, EventArgs e)
		{
			DoDeleteProgram();
		}

		// <summary> Call the New method.</summary>
		private void ProgramMenuNew_Click(object sender, EventArgs e)
		{
			DoNewProgram();
		}

		// <summary> Call the Edit method.</summary>
		private void ProgramMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditProgram();
		}

		// <summary> Call the Delete method.</summary>
		private void ProgramMenuDelete_Click(object sender, EventArgs e)
		{
			DoDeleteProgram();
		}

		// <summary> Call the Refresh method.</summary>
		private void ProgramMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshProgram();
		}

		// <summary> Call the Select method.</summary>
		private void ProgramMenuSelect_Click(object sender, EventArgs e)
		{
			DoSelect();
		}

		// <summary> Perform the Close function.</summary>
		private void ProgramMenuClose_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Control Event Handlers

		// Handles the form keys.
		private void ProgramGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic, LJCHelpPageList);
					break;

				case Keys.F5:
					DoRefreshProgram();
					e.Handled = true;
					break;

				case Keys.Enter:
					DoDefaultProgram();
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void ProgramGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& ProgramGrid.LJCIsDifferentRow(e))
			{
				ProgramGrid.LJCSetCurrentRow(e);
				TimedChange(Change.AppProgram);
			}
		}

		// Handles the SelectionChanged event.
		private void ProgramGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (ProgramGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.AppProgram);
			}
			ProgramGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void ProgramGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (ProgramGrid.LJCGetMouseRow(e) != null)
			{
				DoDefaultProgram();
			}
		}
		#endregion

		#region Properties

		// Gets or sets the IsSelect value.
		internal bool LJCIsSelect { get; set; }

		// Gets a reference to the selected record.
		internal AppProgram LJCSelectedRecord { get; private set; }

		// The help file name.
		internal string LJCHelpFile { get; set; }

		// The List help page name.
		internal string LJCHelpPageList { get; set; }

		// The Detail help page name.
		internal string LJCHelpPageDetail { get; set; }

		// Gets or sets the Managers value.
		private AppManagers Managers { get; set; }
		#endregion

		#region Class Data

		private const bool AllowTrue = true;
		private StandardUISettings mSettings;
		#endregion
	}
}
