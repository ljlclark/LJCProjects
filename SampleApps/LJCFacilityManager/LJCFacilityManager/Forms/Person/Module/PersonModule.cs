// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonModule.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCDBViewControls;
using LJCDBViewDAL;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCViewEditor;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The Tab Composite User Control.
	/// <include path='items/PersonModule/*' file='../Doc/PersonModule.xml'/>
	public partial class PersonModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public PersonModule()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();
			string errorText = FacilityCommon.CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				MessageBox.Show(errorText);
			}

			// Set default class data.
			LJCHelpFile = "FacilityManager.chm";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Data Methods

		// Load the Person View Combo.
		private void LoadComboPerson()
		{
			if (!ViewComboPerson.LJCLoad())
			{
				// Did not load any Views.
				DoViewEdit(ViewComboPerson, mViewInfoPerson);

				string title = "Reload Confirmation";
				string message = "Reload Person View Combo?";
				if (DialogResult.Yes == MessageBox.Show(message, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					ViewComboPerson.LJCLoad();
				}
			}
		}

		// Load the Relation View Combo.
		private void LoadComboRelation()
		{
			if (!ViewComboRelation.LJCLoad())
			{
				// Did not load any Views.
				DoViewEdit(ViewComboRelation, mViewInfoRelation);

				string title = "Reload Confirmation";
				string message = "Reload Relation View Combo?";
				if (DialogResult.Yes == MessageBox.Show(message, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					ViewComboRelation.LJCLoad();
				}
			}
		}
		#endregion

		#region Action Methods

		// Displays the ViewBuilder to edit the current view.
		internal void DoViewEdit(ViewComboControl viewCombo, ViewInfo viewInfo)
		{
			int viewID = viewCombo.LJCSelectedItemID();
			var viewEditor = new ViewEditorList(viewInfo.TableName, false)
			{
				StartupViewDataID = viewID
			};
			viewEditor.ShowDialog();
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
					SetControlsLayout();
					RestoreControlValues();
					LoadComboPerson();
					LoadComboRelation();
					break;

				case Change.PersonView:
					if (SetupGridPerson())
					{
						mGridCodePerson.DataRetrieve();
					}
					break;

				case Change.Person:
					mGridCodeRelation.DataRetrieveRelation();
					mAddressGridCode.DataRetrieveAddress();
					mUnitPersonGridCode.DataRetrieveUnitPerson();
					mAccountGridCode.DataRetrieveAccount();
					PersonGrid.LJCSetLastRow();
					PersonGrid.LJCSetCounter(PersonCounter);
					break;

				case Change.RelationView:
					if (SetupGridRelation())
					{
						mGridCodeRelation.DataRetrieveRelation();
					}
					break;

				case Change.Relation:
					RelationGrid.LJCSetLastRow();
					RelationGrid.LJCSetCounter(RelationCounter);
					break;

				case Change.UnitPerson:
					UnitPersonGrid.LJCSetLastRow();
					UnitPersonGrid.LJCSetCounter(UnitCounter);
					break;

				case Change.Address:
					AddressGrid.LJCSetLastRow();
					AddressGrid.LJCSetCounter(AddressCounter);
					break;

				case Change.Account:
					AccountGrid.LJCSetLastRow();
					AccountGrid.LJCSetCounter(AccountCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			PersonView,
			Person,
			RelationView,
			Relation,
			Address,
			UnitPerson,
			Account
		}

		#region Item Change Support

		// Starts the Timer with the Change value.
		internal void TimedChange(Change change)
		{
			ChangeTimer.DoChange(change.ToString());
		}

		// Start the Change processing.
		private void StartChangeProcessing()
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

		// Gets or sets the ChangeTimer object.
		private ChangeTimer ChangeTimer { get; set; }
		#endregion
		#endregion

		#region Setup Methods

		// Initialize the Class Data.
		private void SetClassData()
		{
			// Get Singleton values.
			var values = ValuesFacility.Instance;
			mSettings = values.StandardSettings;

			Managers = new FacilityManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);

      // *** Begin *** Add - View
      ManagersDbView = new ManagersDbView();
      ManagersDbView.SetDbProperties(mSettings.DbServiceRef
        , mSettings.DataConfigName);
			mDataDbView = new DataDbView(ManagersDbView);
			SetupViewPerson();
			SetupViewRelation();
			// *** End *** Add - View

			// Setup the grid code references.
			mAccountGridCode = new AccountGridCode(this);
			mAddressGridCode = new AddressGridCode(this);
			mGridCodePerson = new GridCodePerson(this);
			mGridCodeRelation = new GridCodeRelation(this);
			mUnitPersonGridCode = new UnitPersonGridCode(this);
		}

		// Initialize the Control setup.
		private void SetControls()
		{
			PersonMenuPassword.Visible = false;
			AddressToolNew.ToolTipText = "Add";
			AddressToolDelete.ToolTipText = "Remove";
			AddressMenuNew.Text = "&Add";
			AddressMenuRemove.Text = "&Remove";

			// Provides additional Drag features between split LJCTabControls.
			var _ = new LJCPanelManager(TabSplit, RelatedTabs, TileTabs);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\Person.xml";

			// *** Next Statement *** Delete - View
			//SetupGridPerson();
			//SetupGridRelation();
			SetupGridAddress();
			SetupGridUnitPerson();
			SetupGridAccount();
		}

		// Configure the initial control settings.
		private void SetControlsLayout()
		{
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				PersonSplit.SplitterWidth = 5;
				PersonSplit.SplitterDistance = PersonSplit.Height / 2;
				TabSplit.SplitterDistance = TabSplit.Width / 2;

				// *** Begin *** Add - View
				ViewLabelPerson.Left += 10;
				ViewComboPerson.Left += 10;
				ViewLabelRelation.Left += 10;
				ViewComboRelation.Left += 10;
				// *** End   *** Add - View

				// Modify MainSplit.Panel1 controls.
				ListHelper.SetPanelControls(PersonSplit.Panel1, null, PersonToolPanel
					, PersonGrid);

				// Related Tabs
				// Modify MainSplit.Panel2 Tabs control.
				ListHelper.SetPanelTabControl(PersonSplit.Panel2, RelatedTabs);

				// Modify MainSplit.Panel2 Page controls.
				ListHelper.SetPageControls(RelationPage, null, RelationToolPanel
					, RelationGrid);

				// Modify MainSplit.Panel2 TabPage controls.
				ListHelper.SetPageControls(AddressPage, null, AddressToolPanel
					, AddressGrid);

				// Modify MainSplit.Panel2 TabPage controls.
				ListHelper.SetPageControls(UnitPersonPage, null, UnitPersonPanel
					, UnitPersonGrid);

				// Modify MainSplit.Panel2 TabPage controls.
				ListHelper.SetPageControls(AccountPage, null, AccountToolPanel
					, AccountGrid);
			}
		}

		// Setup the grid columns.
		private bool SetupGridPerson()
		{
			bool retValue = true;

			PersonGrid.BackgroundColor = mSettings.BeginColor;

			// *** Begin *** Change - View
			// Clear previous grid columns definition as view may have changed.
			PersonGrid.Columns.Clear();

			// Get the view grid columns
			mViewInfoPerson.DataID = ViewComboPerson.LJCSelectedItemID();
			var gridColumns = mDataDbView.GetGridColumns(mViewInfoPerson.DataID);
			if (null == gridColumns)
			{
				// Did not load any Grid Columns.
				retValue = false;
				DoViewEdit(ViewComboPerson, mViewInfoPerson);

				string title = "Reload Confirmation";
				string message = "Reload Person View Combo?";
				if (DialogResult.Yes == MessageBox.Show(message, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					ViewComboPerson.Items.Clear();
					ViewComboPerson.LJCLoad();
				}
			}
			else
			{
				// Setup the ResultGrid GridColumns object.
				//var gridColumns = mViewInfoPerson.GridData.GetGridColumns(gridColumns);

				// Setup the grid columns.
				PersonGrid.LJCAddColumns(gridColumns);
				PersonGrid.LJCRestoreColumnValues(ControlValues);
			}
			return retValue;
			// *** End   *** Change - View
		}

		// Setup the grid columns.
		private bool SetupGridRelation()
		{
			bool retValue = true;

			RelationGrid.BackgroundColor = mSettings.BeginColor;

			// *** Begin *** Change - View
			// Clear previous grid columns definition as view may have changed.
			RelationGrid.Columns.Clear();

			// Get the view grid columns
			mViewInfoRelation.DataID = ViewComboRelation.LJCSelectedItemID();
			var gridColumns = mDataDbView.GetGridColumns(mViewInfoRelation.DataID);
			if (null == gridColumns)
			{
				// Did not load any Grid Columns.
				retValue = false;
				DoViewEdit(ViewComboRelation, mViewInfoRelation);

				string title = "Reload Confirmation";
				string message = "Reload Relation View Combo?";
				if (DialogResult.Yes == MessageBox.Show(message, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					ViewComboRelation.Items.Clear();
					ViewComboRelation.LJCLoad();
				}
			}
			else
			{
				// Setup the ResultGrid GridColumns object.
				//var gridColumns
				//  = mViewInfoRelation.GridData.GetGridColumns(gridColumns);

				// Setup the grid columns.
				RelationGrid.LJCAddColumns(gridColumns);
				RelationGrid.LJCRestoreColumnValues(ControlValues);
			}
			return retValue;
			// *** End   *** Change - View
		}

		// Setup the grid columns.
		private void SetupGridAddress()
		{
			AddressGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == AddressGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Address.PropertyProvinceName,
					Address.PropertyCityName,
					Address.ColumnStreet,
					Address.ColumnPostalCode,
					Address.ColumnCityStateZip
				};

				// Get the grid columns from the manager Data Definition.
				var gridColumns = Managers.AddressManager.GetColumns(propertyNames);

				// Setup the grid columns.
				AddressGrid.LJCAddColumns(gridColumns);
			}
		}

		// Setup the grid columns.
		private void SetupGridUnitPerson()
		{
			UnitPersonGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == UnitPersonGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					UnitPerson.PropertyUnitDescription,
					UnitPerson.ColumnBeginDate,
					UnitPerson.ColumnEndDate
				};

				// Get the grid columns from the manager Data Definition.
				var gridColumns = Managers.UnitPersonManager.GetColumns(propertyNames);

				// Setup the grid columns.
				UnitPersonGrid.LJCAddColumns(gridColumns);
			}
		}

		// Setup the grid columns.
		private void SetupGridAccount()
		{
			AccountGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == AccountGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Account.ColumnBusinessName,
					Account.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				var gridColumns = Managers.AccountManager.GetColumns(propertyNames);

				// Setup the grid columns.
				AccountGrid.LJCAddColumns(gridColumns);
			}
		}

		// Setup the Person View data.
		private void SetupViewPerson()
		{
			ViewComboPerson.LJCInit(Person.TableName, mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mViewInfoPerson = new ViewInfo()
			{
        TableName = ViewComboPerson.LJCTableName
			};
		}

		// Setup the Relation View data.
		private void SetupViewRelation()
		{
			ViewComboRelation.LJCInit(PersonRelation.TableName, mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mViewInfoRelation = new ViewInfo()
			{
        TableName = ViewComboRelation.LJCTableName
			};
		}

		#region Control Values

		// Setup the grid columns.
		private void SaveControlValues()
		{
			Control parent = PersonTabs.Parent;
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			PersonGrid.LJCSaveColumnValues(controlValues);
			RelationGrid.LJCSaveColumnValues(controlValues);
			AddressGrid.LJCSaveColumnValues(controlValues);
			UnitPersonGrid.LJCSaveColumnValues(controlValues);
			AccountGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("PersonSplit.SplitterDistance", 0, 0, 0, PersonSplit.SplitterDistance);

			// Save Window values.
			controlValues.Add(Name, parent.Left, parent.Top
				, parent.Width, parent.Height);

			// Save other values.
			controlValues.Add("View", mViewInfoPerson.TableID, 0, 0, 0);

			NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
				, mControlValuesFileName);
		}

		// Setup the grid columns.
		private void RestoreControlValues()
		{
			ControlValue controlValue;
			Control parent = PersonTabs.Parent;

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
						parent.Left = controlValue.Left;
						parent.Top = controlValue.Top;
						parent.Width = controlValue.Width;
						parent.Height = controlValue.Height;
					}

					// Restore Splitter, Grid and other values.
					FormCommon.RestoreSplitDistance(PersonSplit, ControlValues);

					// *** Next Statement *** Delete - View
					//PersonGrid.LJCRestoreColumnValues(ControlValues);
					RelationGrid.LJCRestoreColumnValues(ControlValues);
					AddressGrid.LJCRestoreColumnValues(ControlValues);
					UnitPersonGrid.LJCRestoreColumnValues(ControlValues);
					AccountGrid.LJCRestoreColumnValues(ControlValues);

					controlValue = ControlValues.LJCSearchName("View");
					if (controlValue != null)
					{
						mViewInfoPerson.TableID = controlValue.Left;
					}
				}
			}
		}

		/// <summary>Gets or sets the ControlValues item.</summary>
		public ControlValues ControlValues { get; set; }
		#endregion
		#endregion

		#region AppModule Implementation

		// Initializes the module.
		/// <include path='items/LJCInit/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public void LJCInit()
		{
			Cursor = Cursors.WaitCursor;
			SetClassData();
			SetControls();
			StartChangeProcessing();
			Cursor = Cursors.Default;
		}

		// Returns a reference to the module tab control.
		/// <include path='items/LJCTabs/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public TabControl LJCTabs()
		{
			return PersonTabs;
		}

		/// <summary>Gets the module assembly name.</summary>
		public string LJCProgramName
		{
			get { return "LJCFacilityManager.exe"; }
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

		/// <summary>Gets or sets the close tab page.</summary>
		public TabPage CloseTabPage { get; set; }

		/// <summary>Calls the PageClose event handlers.</summary>
		protected void OnPageClose()
		{
			PageClose?.Invoke(this, new EventArgs());
		}

		/// <summary>The page close event.</summary>
		public event EventHandler<EventArgs> PageClose;
		#endregion

		#region Private Methods

		// Sets the control states based on the current control values.
		private void SetControlState()
		{
			bool enableNew;
			bool enableEdit;
			LJCGridRow parentRow;
			LJCGridRow row;

			row = PersonGrid.CurrentRow as LJCGridRow;
			enableNew = true;
			enableEdit = row != null;
			FormCommon.SetToolState(PersonTool, enableNew, enableEdit);
			FormCommon.SetMenuState(PersonMenu, enableNew, enableEdit);

			//PersonMenuPassword.Visible = false;
			//if (row != null
			//	&& (row.LJCGetInt32(Person.ColumnID) == AppFacilityValues.Instance.SignonID
			//	|| AppFacilityValues.Instance.SignonIsAdministrator))
			//{
			//	PersonMenuPassword.Visible = true;
			//}

			parentRow = row;
			row = RelationGrid.CurrentRow as LJCGridRow;
			enableNew = parentRow != null;
			enableEdit = row != null;
			FormCommon.SetToolState(RelationTool, enableNew, enableEdit);
			FormCommon.SetMenuState(RelationMenu, enableNew, enableEdit);

			enableEdit = AddressGrid.CurrentRow != null;
			FormCommon.SetToolState(AddressTool, enableNew, enableEdit);
			FormCommon.SetMenuState(AddressMenu, enableNew, enableEdit);

			enableEdit = UnitPersonGrid.CurrentRow != null;
			FormCommon.SetToolState(UnitPersonTool, enableNew, enableEdit);
			FormCommon.SetMenuState(UnitPersonMenu, enableNew, enableEdit);

			enableEdit = AccountGrid.CurrentRow != null;
			FormCommon.SetToolState(AccountTool, enableNew, enableEdit);
			FormCommon.SetMenuState(AccountMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		#region View

		// Displays the ViewEditor for the current table.
		private void ViewPersonEdit_Click(object sender, EventArgs e)
		{
			DoViewEdit(ViewComboPerson, mViewInfoPerson);
		}

		// Displays the ViewEditor for the current table.
		private void ViewRelationEdit_Click(object sender, EventArgs e)
		{
			DoViewEdit(ViewComboRelation, mViewInfoRelation);
		}
		#endregion

		#region Person

		// Calls the New method.
		private void PersonToolNew_Click(object sender, EventArgs e)
		{
			mGridCodePerson.DoNew();
		}

		// Calls the Edit method.
		private void PersonToolEdit_Click(object sender, EventArgs e)
		{
			mGridCodePerson.DoEdit();
		}

		// Calls the Delete method.
		private void PersonToolDelete_Click(object sender, EventArgs e)
		{
			mGridCodePerson.DoDelete();
		}

		// Calls the New method.
		private void PersonMenuNew_Click(object sender, EventArgs e)
		{
			mGridCodePerson.DoNew();
		}

		// Calls the Edit method.
		private void PersonMenuEdit_Click(object sender, EventArgs e)
		{
			mGridCodePerson.DoEdit();
		}

		// Calls the Delete method.
		private void PersonMenuDelete_Click(object sender, EventArgs e)
		{
			mGridCodePerson.DoDelete();
		}

		// Calls the Refresh method.
		private void PersonMenuRefresh_Click(object sender, EventArgs e)
		{
			mGridCodePerson.DoRefresh();
		}

		// Export a text file.
		private void PersonMenuText_Click(object sender, EventArgs e)
		{
			string extension = mSettings.ExportTextExtension;
			string fileSpec = $@"ExportFiles\Person.{extension}";
			PersonGrid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void PersonMenuCSV_Click(object sender, EventArgs e)
		{
			string fileSpec = @"ExportFiles\Person.csv";
			PersonGrid.LJCExportData(fileSpec);
		}

		private void PersonMenuPassword_Click(object sender, EventArgs e)
		{
			Password detail;

			if (PersonGrid.CurrentRow is LJCGridRow row)
			{
				detail = new Password()
				{
					LJCPersonID = row.LJCGetInt32(Person.ColumnID)
				};
				detail.ShowDialog();
			}
		}

		// Performs the Close function.
		private void PersonMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(PersonPage);
		}
		#endregion

		#region Relation

		// Call the Add method.
		private void RelationToolAdd_Click(object sender, EventArgs e)
		{
			mGridCodeRelation.DoNewRelation();
		}

		// Calls the Edit method.
		private void RelationToolEdit_Click(object sender, EventArgs e)
		{
			mGridCodeRelation.DoEditRelation();
		}

		// Call the Remove method.
		private void RelationToolRemove_Click(object sender, EventArgs e)
		{
			mGridCodeRelation.DoDeleteRelation();
		}

		// Call the Add method.
		private void RelationMenuAdd_Click(object sender, EventArgs e)
		{
			mGridCodeRelation.DoNewRelation();
		}

		// Calls the Edit method.
		private void RelationMenuEdit_Click(object sender, EventArgs e)
		{
			mGridCodeRelation.DoEditRelation();
		}

		// Call the Remove method.
		private void RelationMenuRemove_Click(object sender, EventArgs e)
		{
			mGridCodeRelation.DoDeleteRelation();
		}

		// Calls the Refresh method.
		private void RelationMenuRefresh_Click(object sender, EventArgs e)
		{
			mGridCodeRelation.DoRefreshRelation();
		}

		// Export a text file.
		private void RelationMenuText_Click(object sender, EventArgs e)
		{
			string extension = mSettings.ExportTextExtension;
			string fileSpec = $@"ExportFiles\Relation.{extension}";
			RelationGrid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void RelationMenuCSV_Click(object sender, EventArgs e)
		{
			string fileSpec = @"ExportFiles\Relation.csv";
			RelationGrid.LJCExportData(fileSpec);
		}
		#endregion

		#region Address

		// Call the Add method.
		private void AddressToolAdd_Click(object sender, EventArgs e)
		{
			mAddressGridCode.DoAddAddress();
		}

		// Calls the Edit method.
		private void AddressToolEdit_Click(object sender, EventArgs e)
		{
			mAddressGridCode.DoEditAddress();
		}

		// Call the Remove method.
		private void AddressToolDelete_Click(object sender, EventArgs e)
		{
			mAddressGridCode.DoRemoveAddress();
		}

		// Call the Add method.
		private void AddressMenuNew_Click(object sender, EventArgs e)
		{
			mAddressGridCode.DoAddAddress();
		}

		// Calls the Edit method.
		private void AddressMenuEdit_Click(object sender, EventArgs e)
		{
			mAddressGridCode.DoEditAddress();
		}

		// Call the Remove method.
		private void AddressMenuDelete_Click(object sender, EventArgs e)
		{
			mAddressGridCode.DoRemoveAddress();
		}

		// Calls the Refresh method.
		private void AddressMenuRefresh_Click(object sender, EventArgs e)
		{
			mAddressGridCode.DoRefreshAddress();
		}

		// Export a text file.
		private void AddressMenuText_Click(object sender, EventArgs e)
		{
			string extension = mSettings.ExportTextExtension;
			string fileSpec = $@"ExportFiles\PersonAddress.{extension}";
			AddressGrid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void AddressMenuCSV_Click(object sender, EventArgs e)
		{
			string fileSpec = @"ExportFiles\PersonAddress.csv";
			AddressGrid.LJCExportData(fileSpec);
		}
		#endregion

		#region Unit Person

		// Calls the New method.
		private void UnitPersonToolNew_Click(object sender, EventArgs e)
		{
			mUnitPersonGridCode.DoNewUnit();
		}

		// Calls the Edit method.
		private void UnitPersonToolEdit_Click(object sender, EventArgs e)
		{
			mUnitPersonGridCode.DoEditUnit();
		}

		// Calls the Delete method.
		private void UnitPersonToolDelete_Click(object sender, EventArgs e)
		{
			mUnitPersonGridCode.DoDeleteUnit();
		}

		// Calls the New method.
		private void UnitPersonMenuNew_Click(object sender, EventArgs e)
		{
			mUnitPersonGridCode.DoNewUnit();
		}

		// Calls the Edit method.
		private void UnitPersonMenuEdit_Click(object sender, EventArgs e)
		{
			mUnitPersonGridCode.DoEditUnit();
		}

		// Calls the Delete method.
		private void UnitPersonMenuDelete_Click(object sender, EventArgs e)
		{
			mUnitPersonGridCode.DoDeleteUnit();
		}

		// Calls the Refresh method.
		private void UnitPersonMenuRefresh_Click(object sender, EventArgs e)
		{
			mUnitPersonGridCode.DoRefreshUnit();
		}

		// Export a text file.
		private void UnitPersonMenuText_Click(object sender, EventArgs e)
		{
			string extension = mSettings.ExportTextExtension;
			string fileSpec = $@"ExportFiles\PersonUnit.{extension}";
			UnitPersonGrid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void UnitPersonMenuCSV_Click(object sender, EventArgs e)
		{
			string fileSpec = @"ExportFiles\PersonUnit.csv";
			UnitPersonGrid.LJCExportData(fileSpec);
		}
		#endregion

		#region Account

		// Calls the New method.
		private void AccountToolNew_Click(object sender, EventArgs e)
		{
			mAccountGridCode.DoNewAccount();
		}

		// Calls the Edit method.
		private void AccountToolEdit_Click(object sender, EventArgs e)
		{
			mAccountGridCode.DoEditAccount();
		}

		// Calls the Delete method.
		private void AccountToolDelete_Click(object sender, EventArgs e)
		{
			mAccountGridCode.DoDeleteAccount();
		}

		// Calls the New method.
		private void AccountMenuNew_Click(object sender, EventArgs e)
		{
			mAccountGridCode.DoNewAccount();
		}

		// Calls the Edit method.
		private void AccountMenuEdit_Click(object sender, EventArgs e)
		{
			mAccountGridCode.DoEditAccount();
		}

		// Calls the Delete method.
		private void AccountMenuDelete_Click(object sender, EventArgs e)
		{
			mAccountGridCode.DoDeleteAccount();
		}

		// Calls the Refresh method.
		private void AccountMenuRefresh_Click(object sender, EventArgs e)
		{
			mAccountGridCode.DoRefreshAccount();
		}

		// Export a text file.
		private void AccountMenuText_Click(object sender, EventArgs e)
		{
			string extension = mSettings.ExportTextExtension;
			string fileSpec = $@"ExportFiles\PersonAccount.{extension}";
			AccountGrid.LJCExportData(fileSpec);
		}

		// Export a CSV file.
		private void AccountMenuCSV_Click(object sender, EventArgs e)
		{
			string fileSpec = @"ExportFiles\PersonAccount.csv";
			AccountGrid.LJCExportData(fileSpec);
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region Person

		// Handles the View SelectedIndexChanged event.
		private void ViewComboPerson_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.PersonView);
		}

		// Handles the form keys.
		private void PersonGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic,
						"PersonList.htm");
					break;

				case Keys.F5:
					mGridCodePerson.DoRefresh();
					e.Handled = true;
					break;

				case Keys.Enter:
					mGridCodePerson.DoEdit();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						RelationGrid.Select();
					}
					else
					{
						RelationGrid.Select();
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
				TimedChange(Change.Person);
			}
		}

		// Handles the SelectionChanged event.
		private void PersonGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (PersonGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Person);
			}
			PersonGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void PersonGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (PersonGrid.LJCGetMouseRow(e) != null)
			{
				mGridCodePerson.DoEdit();
			}
		}
		#endregion

		#region Relation

		// Handles the View SelectedIndexChanged event.
		private void ViewComboRelation_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.RelationView);
		}

		// Handles the form keys.
		private void RelationGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "RelationList.htm");
					break;

				case Keys.F5:
					mGridCodeRelation.DoRefreshRelation();
					e.Handled = true;
					break;

				case Keys.Enter:
					mGridCodeRelation.DoEditRelation();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						PersonGrid.Select();
					}
					else
					{
						PersonGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void RelationGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& RelationGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				RelationGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Relation);
			}
		}

		// Handles the SelectionChanged event.
		private void RelationGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (RelationGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Relation);
			}
			RelationGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void RelationGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (RelationGrid.LJCGetMouseRow(e) != null)
			{
				mGridCodeRelation.DoEditRelation();
			}
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
						, "PersonAddressList.htm");
					break;

				case Keys.F5:
					mAddressGridCode.DoRefreshAddress();
					e.Handled = true;
					break;

				case Keys.Enter:
					mAddressGridCode.DoEditAddress();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						PersonGrid.Select();
					}
					else
					{
						PersonGrid.Select();
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
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				AddressGrid.LJCSetCurrentRow(e);
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
				mAddressGridCode.DoEditAddress();
			}
		}
		#endregion

		#region Unit Person

		// Handles the form keys.
		private void UnitPersonGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "PersonUnitList.htm");
					break;

				case Keys.F5:
					mGridCodePerson.DoRefresh();
					e.Handled = true;
					break;

				case Keys.Enter:
					mUnitPersonGridCode.DoEditUnit();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						PersonGrid.Select();
					}
					else
					{
						PersonGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void UnitPersonGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& UnitPersonGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				UnitPersonGrid.LJCSetCurrentRow(e);
				TimedChange(Change.UnitPerson);
			}
		}

		// Handles the SelectionChanged event.
		private void UnitPersonGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (UnitPersonGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.UnitPerson);
			}
			UnitPersonGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void UnitPersonGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (UnitPersonGrid.LJCGetMouseRow(e) != null)
			{
				mUnitPersonGridCode.DoEditUnit();
			}
		}
		#endregion

		#region Account

		// Handles the form keys.
		private void AccountGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "AccountList.htm");
					break;

				case Keys.F5:
					mAccountGridCode.DoRefreshAccount();
					e.Handled = true;
					break;

				case Keys.Enter:
					mAccountGridCode.DoEditAccount();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						PersonGrid.Select();
					}
					else
					{
						PersonGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void AccountGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& AccountGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				AccountGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Account);
			}
		}

		// Handles the SelectionChanged event.
		private void AccountGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (AccountGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Account);
			}
			AccountGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void AccountGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (AccountGrid.LJCGetMouseRow(e) != null)
			{
				mAccountGridCode.DoEditAccount();
			}
		}
		#endregion
		#endregion

		#region Properties

		/// <summary>The help file name.</summary>
		public string LJCHelpFile { get; set; }

		// The Managers object.
		internal FacilityManagers Managers { get; set; }

    internal ManagersDbView ManagersDbView { get; set; }
		#endregion

		#region Class Data

		private string mControlValuesFileName;
		private StandardUISettings mSettings;

		// GridClass values.
		private AccountGridCode mAccountGridCode;
		private AddressGridCode mAddressGridCode;
		private GridCodePerson mGridCodePerson;
		private GridCodeRelation mGridCodeRelation;
		private UnitPersonGridCode mUnitPersonGridCode;

		// *** Begin *** Add - View
		// View values.
		internal DataDbView mDataDbView;
		internal ViewInfo mViewInfoPerson;
		internal ViewInfo mViewInfoRelation;
		// *** End *** Add - View
		#endregion
	}
}
