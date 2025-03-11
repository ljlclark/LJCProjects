// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessList.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The Business list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
	public partial class BusinessList : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public BusinessList()
		{
			InitializeComponent();

			// Set default class data.
			LJCHelpFile = "FacilityManager.chm";
			LJCHelpPageList = "BusinessList.htm";
			LJCHelpPageDetail = "BusinessDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void BusinessList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Data Methods

		#region CodeTypeClass

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveCodeTypeClass()
		{
			Cursor = Cursors.WaitCursor;
			TypeClassCombo.LJCInit();
			var codeTypeClassManager = Managers.CodeTypeClassManager;
			TypeClassCombo.LJCLoad(codeTypeClassManager.GetCodeClassID("Business"));
			TypeClassCombo.Items.Insert(0, "");

			if (LJCSelectedRecord != null
				&& LJCSelectedRecord.CodeTypeID > 0)
			{
				TypeClassCombo.LJCSetSelectedItem(LJCSelectedRecord.CodeTypeID);
			}
			else
			{
				TypeClassCombo.SelectedIndex = 0;
			}
			Cursor = Cursors.Default;
		}
		#endregion

		#region Business

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveBusiness()
		{
			Businesses records;
			DbColumns keyColumns = null;

			Cursor = Cursors.WaitCursor;
			BusinessGrid.LJCRowsClear();

			if (TypeClassCombo.SelectedIndex > -1)
			{
				int typeID = TypeClassCombo.LJCGetSelectedItemID();
				if (typeID >= 0)
				{
					keyColumns = new DbColumns()
					{
						{ Business.ColumnCodeTypeID, typeID }
					};
				}
				Managers.BusinessManager.SetOrderByName();
				records = Managers.BusinessManager.Load(keyColumns);

				if (NetCommon.HasItems(records))
				{
					foreach (Business record in records)
					{
						RowAddBusiness(record);
					}
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Business);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddBusiness(Business dataRecord)
		{
			LJCGridRow retValue;

			retValue = BusinessGrid.LJCRowAdd();
			SetStoredValuesBusiness(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(BusinessGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateBusiness(Business dataRecord)
		{
			if (BusinessGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesBusiness(gridRow, dataRecord);
				gridRow.LJCSetValues(BusinessGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesBusiness(LJCGridRow row, Business dataRecord)
		{
			row.LJCSetInt32(Business.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectBusiness(Business dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in BusinessGrid.Rows)
				{
					rowID = row.LJCGetInt32(Business.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						BusinessGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion
		#endregion

		#region Action Methods

		#region Business

		// Performs the default list action.
		/// <include path='items/DoDefault/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoDefaultBusiness()
		{
			if (LJCIsSelect)
			{
				DoSelectBusiness();
			}
			else
			{
				DoEditBusiness();
			}
		}

		// Displays a detail dialog for a new record.
		/// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoNewBusiness()
		{
			BusinessDetail detail;

			detail = new BusinessDetail()
			{
				LJCParentName = LJCParentName,
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = LJCHelpPageDetail
			};
			detail.LJCChange += new EventHandler<EventArgs>(BusinessDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoEditBusiness()
		{
			BusinessDetail detail;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Business.ColumnID);

				detail = new BusinessDetail()
				{
					LJCID = id,
					LJCParentName = mParentName,
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = LJCHelpPageDetail
				};
				detail.LJCChange += new EventHandler<EventArgs>(BusinessDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		void BusinessDetail_Change(object sender, EventArgs e)
		{
			BusinessDetail detail;
			Business record;
			LJCGridRow row;

			detail = sender as BusinessDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateBusiness(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddBusiness(record);
				BusinessGrid.LJCSetCurrentRow(row, true);
				TimedChange(Change.Business);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoDeleteBusiness()
		{
			string title;
			string message;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Business.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ Business.ColumnID, id }
					};
					Managers.BusinessManager.Delete(keyColumns);
					if (Managers.BusinessManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						BusinessGrid.Rows.Remove(row);
						TimedChange(Change.Business);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoRefreshBusiness()
		{
			Business record;
			int id = 0;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Business.ColumnID);
			}
			DataRetrieveBusiness();

			// Select the original row.
			if (id > 0)
			{
				record = new Business()
				{
					ID = id
				};
				RowSelectBusiness(record);
			}
		}

		// Sets the selected item and returns to the parent form.
		/// <include path='items/DoSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoSelectBusiness()
		{
			Business record;
			int id;

			LJCSelectedRecord = null;
			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				Cursor = Cursors.WaitCursor;
				id = row.LJCGetInt32(Business.ColumnID);

				var keyColumns = new DbColumns()
				{
					{ Business.ColumnID, id }
				};
				record = Managers.BusinessManager.Retrieve(keyColumns);
				if (record != null)
				{
					LJCSelectedRecord = record;
				}
			}
			Cursor = Cursors.Default;
			DialogResult = DialogResult.OK;
		}
		#endregion
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesFacility.Instance;
			mSettings = values.StandardSettings;

			// Initialize Class Data.
			Managers = new FacilityManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Configure controls.
			if (LJCIsSelect)
			{
				// This is a Selection List.
				Text = "Business Selection";
				BusinessMenuEdit.ShortcutKeyDisplayString = "";
				BusinessMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
			}
			else
			{
				// This is a display list.
				Text = "Business List";
				BusinessMenuSelect.Visible = false;
			}

			SetupGridBusiness();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				// Modify MainSplit.Panel2 controls.
				ListHelper.SetPanelControls(FilterSplit.Panel2, ListHeading
					, ListToolPanel, BusinessGrid);
			}
		}

		// Setup the grid columns.
		private void SetupGridBusiness()
		{
			BusinessGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == BusinessGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					Business.ColumnName,
					Business.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns columns = Managers.BusinessManager.GetColumns(propertyNames);

				// Setup the grid columns.
				BusinessGrid.LJCAddColumns(columns);
			}
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
					ConfigureControls();
					//RestoreControlValues();

					// Load first List.
					DataRetrieveCodeTypeClass();
					break;

				case Change.CodeTypeClass:
					DataRetrieveBusiness();
					RowSelectBusiness(LJCSelectedRecord);
					break;

				case Change.Business:
					BusinessGrid.LJCSetLastRow();
					BusinessGrid.LJCSetCounter(BusinessCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			CodeTypeClass,
			Business
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
			bool enableEdit = BusinessGrid.CurrentRow != null;
			FormCommon.SetToolState(BusinessTool, enableNew, enableEdit);
			FormCommon.SetMenuState(BusinessMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		#region Business

		// Calls the New method.
		private void BusinessToolNew_Click(object sender, EventArgs e)
		{
			DoNewBusiness();
		}

		// Calls the Edit method.
		private void BusinessToolEdit_Click(object sender, EventArgs e)
		{
			DoEditBusiness();
		}

		// Calls the Delete method.
		private void BusinessToolDelete_Click(object sender, EventArgs e)
		{
			DoDeleteBusiness();
		}

		// Calls the New method.
		private void BusinessMenuNew_Click(object sender, EventArgs e)
		{
			DoNewBusiness();
		}

		// Calls the Edit method.
		private void BusinessMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditBusiness();
		}

		// Calls the Delete method.
		private void BusinessMenuDelete_Click(object sender, EventArgs e)
		{
			DoDeleteBusiness();
		}

		// Calls the Refresh method.
		private void BusinessMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshBusiness();
		}

		// Calls the Select method.
		private void BusinessMenuSelect_Click(object sender, EventArgs e)
		{
			DoSelectBusiness();
		}

		// Performs the Close function.
		private void BusinessMenuClose_Click(object sender, EventArgs e)
		{
			//SaveControlValues();
			Close();
		}

		// Displays the Help page.
		private void BusinessMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, LJCHelpPageList);
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region CodeTypeClass

		// Handles the SelectedIndexChanged event.
		private void TypeClassCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.CodeTypeClass);
		}
		#endregion

		#region Business

		// Handles the form keys.
		private void BusinessGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, LJCHelpPageList);
					break;

				case Keys.F5:
					DoRefreshBusiness();
					e.Handled = true;
					break;

				case Keys.Enter:
					DoDefaultBusiness();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						TypeClassCombo.Select();
					}
					else
					{
						TypeClassCombo.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void BusinessGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& BusinessGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				BusinessGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Business);
			}
		}

		// Handles the SelectionChanged event.
		private void BusinessGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (BusinessGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Business);
			}
			BusinessGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void BusinessGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (BusinessGrid.LJCGetMouseRow(e) != null)
			{
				DoDefaultBusiness();
			}
		}
		#endregion
		#endregion

		#region Properties

		/// <summary> Gets or sets the LJCParentName value.</summary>
		public string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		/// <summary> Gets or sets the LJCIsSelect value.</summary>
		public bool LJCIsSelect { get; set; }

		/// <summary> Gets a reference to the selected record.</summary>
		// #002 Next Statement - Change
		public Business LJCSelectedRecord { get; set; }

		/// <summary> The help file name.</summary>
		public string LJCHelpFile { get; set; }

		/// <summary> The List help page name.</summary>
		public string LJCHelpPageList { get; set; }

		/// <summary> The Detail help page name.</summary>
		public string LJCHelpPageDetail { get; set; }

		// The Managers object.
		internal FacilityManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		#endregion
	}
}
