// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddressList.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The Address list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
	public partial class AddressList : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AddressList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			LJCHelpFile = "FacilityManager.chm";
			LJCHelpPageList = "AddressList.htm";
			LJCHelpPageDetail = "AddressDetail.htm";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void AddressList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Data Methods

		#region CodeTypeClass

		// Retrieves the list rows.
		private void DataRetrieveCodeTypeClass()
		{
			Cursor = Cursors.WaitCursor;
			TypeClassCombo.LJCInit();
			var codeTypeClassManager = Managers.CodeTypeClassManager;
			TypeClassCombo.LJCLoad(codeTypeClassManager.GetCodeClassID("Address"));
			TypeClassCombo.Items.Insert(0, "");

			// #002 Begin - Add
			if (LJCSelectedRecord != null
				&& LJCSelectedRecord.CodeTypeID > 0)
			{
				TypeClassCombo.LJCSetSelectedItem(LJCSelectedRecord.CodeTypeID);
			}
			else
			{
				TypeClassCombo.SelectedIndex = 0;
			}
			// #002 End - Add
			Cursor = Cursors.Default;
		}
		#endregion

		#region Address

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveAddress()
		{
			Addresses records;
			DbColumns keyColumns = null;

			Cursor = Cursors.WaitCursor;
			AddressGrid.LJCRowsClear();

			var addressManager = Managers.AddressManager;
			int typeID = TypeClassCombo.LJCGetSelectedItemID();
			if (typeID >= 0)
			{
				keyColumns = addressManager.GetCodeTypeIDKey(typeID);
			}

			// Added joins to create City, State Zip.
			DbJoins dbJoins = addressManager.GetLoadJoins();
			records = addressManager.Load(keyColumns, joins: dbJoins);

			if (NetCommon.HasItems(records))
			{
				foreach (Address record in records)
				{
					RowAddAddress(record);
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Address);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddAddress(Address dataRecord)
		{
			LJCGridRow retValue;

			retValue = AddressGrid.LJCRowAdd();
			SetStoredValuesAddress(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(AddressGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateAddress(Address dataRecord)
		{
			if (AddressGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesAddress(row, dataRecord);
				row.LJCSetValues(AddressGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesAddress(LJCGridRow row, Address dataRecord)
		{
			row.LJCSetInt32(Address.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectAddress(Address dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in AddressGrid.Rows)
				{
					rowID = row.LJCGetInt32(Address.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						AddressGrid.LJCSetCurrentRow(row, true);
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

		// Performs the default list action.
		/// <include path='items/DoDefault/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoDefaultAddress()
		{
			if (LJCIsSelect)
			{
				DoSelectAddress();
			}
			else
			{
				DoEditAddress();
			}
		}

		// Displays a detail dialog for a new record.
		/// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoNewAddress()
		{
			AddressDetail detail;

			detail = new AddressDetail()
			{
				LJCParentName = LJCParentName,
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = LJCHelpPageDetail
			};
			detail.LJCChange += new EventHandler<EventArgs>(AddressDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoEditAddress()
		{
			AddressDetail detail;

			if (AddressGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Address.ColumnID);

				detail = new AddressDetail()
				{
					LJCID = id,
					LJCParentName = LJCParentName,
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = LJCHelpPageDetail
				};
				detail.LJCChange += new EventHandler<EventArgs>(AddressDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		void AddressDetail_Change(object sender, EventArgs e)
		{
			AddressDetail detail;
			LJCGridRow row;

			detail = sender as AddressDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateAddress(detail.LJCRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddAddress(detail.LJCRecord);
				AddressGrid.LJCSetCurrentRow(row, true);
				TimedChange(Change.Address);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoDeleteAddress()
		{
			DbColumns keyColumns;
			string title;
			string message;

			if (AddressGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Address.ColumnID);

					keyColumns = new DbColumns()
					{
						{ Address.ColumnID, id }
					};
					Managers.AddressManager.Delete(keyColumns);
					if (Managers.AddressManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						AddressGrid.Rows.Remove(row);
						TimedChange(Change.Address);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoRefreshAddress()
		{
			Address record;
			int id = 0;

			if (AddressGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Address.ColumnID);
			}
			DataRetrieveAddress();

			// Select the original row.
			if (id > 0)
			{
				record = new Address()
				{
					ID = id
				};
				RowSelectAddress(record);
			}
		}

		// Sets the selected item and returns to the parent form.
		/// <include path='items/DoSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoSelectAddress()
		{
			Address record;
			int id;

			LJCSelectedRecord = null;
			if (AddressGrid.CurrentRow is LJCGridRow row)
			{
				Cursor = Cursors.WaitCursor;
				id = row.LJCGetInt32(Address.ColumnID);

				var keyColumns = new DbColumns()
				{
					{ Address.ColumnID, id }
				};
				record = Managers.AddressManager.Retrieve(keyColumns);
				if (record != null)
				{
					LJCSelectedRecord = record;
				}
			}
			Cursor = Cursors.Default;
			DialogResult = DialogResult.OK;
		}
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
				Text = "Address Selection";
				AddressMenuEdit.ShortcutKeyDisplayString = "";
				AddressMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
			}
			else
			{
				// This is a display list.
				Text = "Address List";
				AddressMenuSelect.Visible = false;
			}

			SetupGridAddress();
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
				ListHelper.SetPanelControls(mFilterSplit.Panel2, AddressHeading
					, AddressToolPanel, AddressGrid);
			}
		}

		// Setup the grid columns.
		private void SetupGridAddress()
		{
			AddressGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == AddressGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					Address.PropertyProvinceName,
					Address.PropertyCityName,
					Address.ColumnStreet,
					Address.ColumnPostalCode,
					Address.ColumnCityStateZip
				};

				// Get the grid columns from the manager Data Definition.
				var columns = Managers.AddressManager.GetColumns(propertyNames);

				// Setup the grid columns.
				AddressGrid.LJCAddColumns(columns);
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

					// Load first List.
					DataRetrieveCodeTypeClass();
					break;

				case Change.CodeTypeClass:
					DataRetrieveAddress();
					break;

				case Change.Address:
					AddressGrid.LJCSetLastRow();
					AddressGrid.LJCSetCounter(Counter);
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
			Address
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
		/// <include path='items/SetControlState/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetControlState()
		{
			bool enableNew = true;
			bool enableEdit = AddressGrid.CurrentRow != null;
			FormCommon.SetToolState(AddressTool, enableNew, enableEdit);
			FormCommon.SetMenuState(AddressMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		#region Address

		// Calls the New method.
		private void AddressToolNew_Click(object sender, EventArgs e)
		{
			DoNewAddress();
		}

		// Calls the Edit method.
		private void AddressToolEdit_Click(object sender, EventArgs e)
		{
			DoEditAddress();
		}

		// Calls the Delete method.
		private void AddressToolDelete_Click(object sender, EventArgs e)
		{
			DoDeleteAddress();
		}

		// Calls the New method.
		private void AddressMenuNew_Click(object sender, EventArgs e)
		{
			DoNewAddress();
		}

		// Calls the Edit method.
		private void AddressMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditAddress();
		}

		// Calls the Delete method.
		private void AddressMenuDelete_Click(object sender, EventArgs e)
		{
			DoDeleteAddress();
		}

		// Calls the Refresh method.
		private void AddressMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshAddress();
		}

		// Calls the Select method.
		private void AddressMenuSelect_Click(object sender, EventArgs e)
		{
			DoSelectAddress();
		}

		// Performs the Close function.
		private void AddressMenuClose_Click(object sender, EventArgs e)
		{
			//SaveControlValues();
			Close();
		}

		// Display the help page.
		private void AddressMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, LJCHelpPageList);
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region CodeTypeClass

		// <summary> Handles the SelectedIndexChanged event.</summary>
		private void TypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.CodeTypeClass);
		}
		#endregion

		#region Address

		// Handles the form keys.
		private void AddressGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, LJCHelpPageList);
					break;

				case Keys.F5:
					DoRefreshAddress();
					e.Handled = true;
					break;

				case Keys.Enter:
					DoDefaultAddress();
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
		private void AddressGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& AddressGrid.LJCIsDifferentRow(e))
			{
				AddressGrid.LJCSetCurrentRow(e);
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				TimedChange(Change.Address);
			}
		}

		// Handles the SelectionChanged event.
		private void AddressGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (AddressGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Address);
			}
			AddressGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void AddressGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (AddressGrid.LJCGetMouseRow(e) != null)
			{
				DoDefaultAddress();
			}
		}
		#endregion
		#endregion

		#region Properties

		/// <summary>Gets or sets the LJCParentName value.</summary>
		public string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		/// <summary>Gets or sets the LJCIsSelect value.</summary>
		public bool LJCIsSelect { get; set; }

		/// <summary>Gets a reference to the selected record.</summary>
		// #002 Next Statement - Change
		public Address LJCSelectedRecord { get; set; }

		/// <summary>The help file name.</summary>
		public string LJCHelpFile { get; set; }

		/// <summary>The List help page name.</summary>
		public string LJCHelpPageList { get; set; }

		/// <summary>The Detail help page name.</summary>
		public string LJCHelpPageDetail { get; set; }

		// The Managers object.
		internal FacilityManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		#endregion
	}
}
