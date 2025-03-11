// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonList.cs
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

	// The Person list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
	public partial class PersonList : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public PersonList()
		{
			InitializeComponent();

			// Set default class data.
			LJCHelpFile = "FacilityManager.chm";
			LJCHelpPageList = "PersonList.htm";
			LJCHelpPageDetail = "PersonDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void PersonList_Load(object sender, EventArgs e)
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
			TypeClassCombo.LJCInit();
			var codeTypeClassManager = Managers.CodeTypeClassManager;
			TypeClassCombo.LJCLoad(codeTypeClassManager.GetCodeClassID("Person"));
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
		}
		#endregion

		#region Person

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrievePerson()
		{
			Persons dataRecords;
			DbColumns keyColumns = null;

			Cursor = Cursors.WaitCursor;
			PersonGrid.LJCRowsClear();

			//  // Data from items.
			int parentID = TypeClassCombo.LJCGetSelectedItemID();

			if (parentID >= 0)
			{
				keyColumns = new DbColumns()
				{
					{ Person.ColumnCodeTypeID, parentID }
				};
			}
			var personManager = Managers.PersonManager;
			DbJoins dbJoins = personManager.GetLoadJoins();
			List<string> loadColumnNames = GetLoadColumnNamesPerson();
			personManager.SetOrderByFirstLast();
			dataRecords = personManager.Load(keyColumns, loadColumnNames, joins: dbJoins);

			if (NetCommon.HasItems(dataRecords))
			{
				foreach (Person record in dataRecords)
				{
					RowAddPerson(record);
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Person);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddPerson(Person dataRecord)
		{
			LJCGridRow retValue;

			retValue = PersonGrid.LJCRowAdd();
			SetStoredValuesPerson(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(PersonGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdatePerson(Person dataRecord)
		{
			if (PersonGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesPerson(gridRow, dataRecord);
				gridRow.LJCSetValues(PersonGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesPerson(LJCGridRow row, Person dataRecord)
		{
			row.LJCSetInt32(Person.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectPerson(Person dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in PersonGrid.Rows)
				{
					rowID = row.LJCGetInt32(Person.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						PersonGrid.LJCSetCurrentRow(row, true);
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
		private void DoDefaultPerson()
		{
			if (LJCIsSelect)
			{
				DoSelectPerson();
			}
			else
			{
				DoEditPerson();
			}
		}

		// Displays a detail dialog for a new record.
		/// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoNewPerson()
		{
			PersonDetail detail;

			detail = new PersonDetail
			{
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = LJCHelpPageDetail
			};
			detail.LJCChange += new EventHandler<EventArgs>(PersonDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoEditPerson()
		{
			PersonDetail detail;

			if (PersonGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Person.ColumnID);

				detail = new PersonDetail()
				{
					LJCID = id,
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = LJCHelpPageDetail
				};
				detail.LJCChange += new EventHandler<EventArgs>(PersonDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		void PersonDetail_Change(object sender, EventArgs e)
		{
			PersonDetail detail;
			Person record;
			LJCGridRow row;

			detail = sender as PersonDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdatePerson(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddPerson(record);
				PersonGrid.LJCSetCurrentRow(row, true);
				ChangeTimer.DoChange(Change.Person.ToString());
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoDeletePerson()
		{
			string title;
			string message;

			if (PersonGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Person.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ Person.ColumnID, id }
					};
					Managers.PersonManager.Delete(keyColumns);
					if (Managers.PersonManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						PersonGrid.Rows.Remove(row);
						ChangeTimer.DoChange(Change.Person.ToString());
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoRefreshPerson()
		{
			Person record;
			int id = 0;

			if (PersonGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Person.ColumnID);
			}
			DataRetrievePerson();

			// Select the original row.
			if (id > 0)
			{
				record = new Person()
				{
					ID = id
				};
				RowSelectPerson(record);
			}
		}

		// Sets the selected item and returns to the parent form.
		/// <include path='items/DoSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoSelectPerson()
		{
			Person record;
			int id;

			LJCSelectedRecord = null;
			if (PersonGrid.CurrentRow is LJCGridRow row)
			{
				Cursor = Cursors.WaitCursor;
				id = row.LJCGetInt32(Person.ColumnID);

				var keyColumns = new DbColumns()
				{
					{ Person.ColumnID, id }
				};
				record = Managers.PersonManager.Retrieve(keyColumns);
				if (record != null)
				{
					LJCSelectedRecord = record;
				}
				Cursor = Cursors.Default;
				DialogResult = DialogResult.OK;
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
					DataRetrievePerson();
					RowSelectPerson(LJCSelectedRecord);
					break;

				case Change.Person:
					PersonGrid.LJCSetLastRow();
					PersonGrid.LJCSetCounter(Counter);
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
			Person
		}

		#region Item Change Support

		// Change Event Handler
		private void ChangeTimer_ItemChange(object sender, EventArgs e)
		{
			Change changeType;

			changeType = (Change)Enum.Parse(typeof(Change)
				, ChangeTimer.ChangeName);
			DoChange(changeType);
		}

		// Gets or sets the ChangeTimer object.
		internal ChangeTimer ChangeTimer { get; set; }
		#endregion
		#endregion

		#region Setup Methods

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				// Modify MainSplit.Panel2 controls.
				ListHelper.SetPanelControls(FilterSplit.Panel2, PersonHeading
					, PersonToolPanel, PersonGrid);
			}
		}

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

			ControlSetup();
			SetupGridPerson();

			// Start Change processing.
			ChangeTimer = new ChangeTimer();
			ChangeTimer.ItemChange += ChangeTimer_ItemChange;
			ChangeTimer.DoChange(Change.Startup.ToString());
			Cursor = Cursors.Default;
		}

		#region Setup Support.

		// Initial Control setup.
		private void ControlSetup()
		{
			if (LJCIsSelect)
			{
				// This is a Selection List.
				Text = "Person Selection";
				PersonMenuEdit.ShortcutKeyDisplayString = "";
				PersonMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
			}
			else
			{
				// This is a display list.
				Text = "Person List";
				PersonMenuSelect.Visible = false;
			}
		}

		// Setup the grid columns.
		private void SetupGridPerson()
		{
			PersonGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == PersonGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Person.ColumnID,
					Person.ColumnFirstName,
					Person.ColumnLastName,
					Person.ColumnPrincipleTitle,
					Person.ColumnUnitDescription
				};

				// Get the grid columns from the manager Data Definition.
				var columns = Managers.PersonManager.GetColumns(propertyNames);

				// Setup the grid columns.
				PersonGrid.LJCAddColumns(columns);
			}
		}
		#endregion
		#endregion

		#region Private Methods

		// 
		private List<string> GetLoadColumnNamesPerson()
		{
			List<string> retValue = new List<string>()
			{
				Person.ColumnID,
				Person.ColumnFirstName,
				Person.ColumnLastName,
				Person.ColumnPrincipleTitle,
				//Person.ColumnUnitDescription
			};
			return retValue;
		}

		// Sets the control states based on the current control values.
		private void SetControlState()
		{
			bool enableNew = true;
			bool enableEdit = PersonGrid.CurrentRow != null;
			FormCommon.SetToolState(PersonTool, enableNew, enableEdit);
			FormCommon.SetMenuState(PersonMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		// Calls the New method.
		private void PersonToolNew_Click(object sender, EventArgs e)
		{
			DoNewPerson();
		}

		// Calls the Edit method.
		private void PersonToolEdit_Click(object sender, EventArgs e)
		{
			DoEditPerson();
		}

		// Calls the Delete method.
		private void PersonToolDelete_Click(object sender, EventArgs e)
		{
			DoDeletePerson();
		}

		// Calls the New method.
		private void PersonMenuNew_Click(object sender, EventArgs e)
		{
			DoNewPerson();
		}

		// Calls the Edit method.
		private void PersonMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditPerson();
		}

		// Calls the Delete method.
		private void PersonMenuDelete_Click(object sender, EventArgs e)
		{
			DoDeletePerson();
		}

		// Calls the Refresh method.
		private void PersonMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshPerson();
		}

		// Calls the Select method.
		private void PersonMenuSelect_Click(object sender, EventArgs e)
		{
			DoSelectPerson();
		}

		// Performs the Close function.
		private void PersonMenuClose_Click(object sender, EventArgs e)
		{
			//SaveControlValues();
			Close();
		}
		#endregion

		#region Control Event Handlers

		#region CodeTypeClass

		// Handles the SelectedIndexChanged event.
		private void TypeClassCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChangeTimer.DoChange(Change.CodeTypeClass.ToString());
		}
		#endregion

		#region Person

		// Handles the form keys.
		private void PersonGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic, LJCHelpPageList);
					break;

				case Keys.F5:
					DoRefreshPerson();
					break;

				case Keys.Enter:
					DoDefaultPerson();
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
		private void PersonGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& PersonGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				PersonGrid.LJCSetCurrentRow(e);
				ChangeTimer.DoChange(Change.Person.ToString());
			}
		}

		// Handles the SelectionChanged event.
		private void PersonGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (PersonGrid.LJCAllowSelectionChange)
			{
				ChangeTimer.DoChange(Change.Person.ToString());
			}
			PersonGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void PersonGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (PersonGrid.LJCGetMouseRow(e) != null)
			{
				DoDefaultPerson();
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
		public Person LJCSelectedRecord { get; set; }

		/// <summary> The help file name.</summary>
		public string LJCHelpFile { get; set; }

		/// <summary> The List help page name.</summary>
		public string LJCHelpPageList { get; set; }

		/// <summary> The Detail help page name.</summary>
		public string LJCHelpPageDetail { get; set; }

		/// <summary> The CodeTypeClass ID.</summary>
		public int CodeTypeClassID { get; set; }

		// The Managers object.
		internal FacilityManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		#endregion
	}
}
