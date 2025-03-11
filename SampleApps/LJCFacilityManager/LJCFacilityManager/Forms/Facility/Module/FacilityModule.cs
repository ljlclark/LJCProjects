// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityModule.cs
using System;
using System.Collections.Generic;
using System.IO;
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

	// The tab composite user control.
	/// <include path='items/ModuleA/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
	public partial class FacilityModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public FacilityModule()
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

			// Initialize Grid Code
			mEquipmentGridCode = new EquipmentGridCode(this);
			mFacilityGridCode = new FacilityGridCode(this);
			mFixtureGridCode = new FixtureGridCode(this);
			mUnitGridCode = new UnitGridCode(this);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = "ControlValues\\Facility.xml";

			try
			{
				SetupGridFacility();
			}
			catch (SystemException e)
			{
				CreateTables(e, mSettings.DataConfigName);
				SetupGridFacility();
			}

			SetupGridUnit();
			SetupGridFixture();
			SetupGridEquipment();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Create the application tables.
		internal static void CreateTables(SystemException e, string dataConfigName)
		{
			string[] fileSpecs = {
				@"SQLScript\CreateFacilityDataTables.sql"
			};

			int errorCode = ManagerCommon.GetMissingTableErrorCode(dataConfigName);
			if (e.HResult == errorCode)
			{
				if (FormCommon.CreateTablesPrompt(e.Message, fileSpecs))
				{
					if (!ManagerCommon.CreateTables(dataConfigName, fileSpecs))
					{
						throw new SystemException(e.Message);
					}
				}
			}
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				FacilitySplit.SplitterWidth = 5;
				UnitSplit.SplitterWidth = 5;
				int oneThird = FacilitySplit.Height / 3;
				FacilitySplit.SplitterDistance = oneThird;
				UnitSplit.SplitterDistance = oneThird;

				// Modify FacilityTab, FacilitySplit.
				ListHelper.SetPageSplitControl(FacilityPage, FacilitySplit);

				// Modify MainSplit.Panel1 controls.
				ListHelper.SetPanelControls(FacilitySplit.Panel1, null, FacilityToolPanel
					, FacilityGrid);

				// Modify MainSplit.Panel2, ChildSplit.
				ListHelper.SetPanelSplitControl(FacilitySplit.Panel2, UnitSplit);

				// Modify ChildSplit.Panel1 controls.
				ListHelper.SetPanelControls(UnitSplit.Panel1, UnitHeading
					, UnitToolPanel, UnitGrid);

				// Modify ChildSplit.Panel2 controls.
				ListHelper.SetPanelControls(UnitSplit.Panel2, FixtureHeading
					, FixtureToolPanel, FixtureGrid);

				// Modify TabPage controls.
				ListHelper.SetPageControls(EquipmentPage, null, EquipmentToolPanel
					, EquipmentGrid);
			}
		}

		// Setup the grid columns.
		private void SetupGridFacility()
		{
			FacilityGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == FacilityGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Facility.ColumnCode,
					Facility.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns columns = Managers.FacilityDbManager.GetColumns(propertyNames);

				// Setup the grid columns.
				FacilityGrid.LJCAddColumns(columns);
			}
		}

		// Setup the grid columns.
		private void SetupGridUnit()
		{
			UnitGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == UnitGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Unit.ColumnCode,
					Unit.ColumnDescription,
					Unit.ColumnPersonName,
					Unit.ColumnTypeDescription
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns gridColumns = Managers.UnitManager.GetColumns(propertyNames);

				// Setup the grid columns.
				UnitGrid.LJCAddColumns(gridColumns);
			}
		}

		// Setup the grid columns.
		private void SetupGridFixture()
		{
			FixtureGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == FixtureGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Fixture.ColumnCode,
					Fixture.ColumnDescription,
					Fixture.ColumnTypeDescription
				};

				// Get the grid columns from the manager Data Definition.
				var gridColumns = Managers.FixtureManager.GetColumns(propertyNames);

				// Setup the grid columns.
				FixtureGrid.LJCAddColumns(gridColumns);
			}
		}

		// Setup the grid columns.
		private void SetupGridEquipment()
		{
			EquipmentGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == EquipmentGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>{
					Equipment.ColumnCode,
					Equipment.ColumnDescription,
					Equipment.ColumnTypeDescription,
					Equipment.ColumnUnitDescription
				};

				// Get the grid columns from the manager Data Definition.
				var gridColumns = Managers.EquipmentManager.GetColumns(propertyNames);

				// Setup the grid columns.
				EquipmentGrid.LJCAddColumns(gridColumns);
			}
		}

		// Saves the control values. 
		private void SaveControlValues()
		{
			Control parent = FacilityTabs.Parent;
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			FacilityGrid.LJCSaveColumnValues(controlValues);
			UnitGrid.LJCSaveColumnValues(controlValues);
			FixtureGrid.LJCSaveColumnValues(controlValues);
			EquipmentGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("FacilitySplit.SplitterDistance", 0, 0, 0
				, FacilitySplit.SplitterDistance);
			controlValues.Add("UnitSplit.SplitterDistance", 0, 0, 0
				, UnitSplit.SplitterDistance);

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
			Control parent = FacilityTabs.Parent;

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
					FormCommon.RestoreSplitDistance(FacilitySplit, ControlValues);
					FormCommon.RestoreSplitDistance(UnitSplit, ControlValues);

					FacilityGrid.LJCRestoreColumnValues(ControlValues);
					UnitGrid.LJCRestoreColumnValues(ControlValues);
					FixtureGrid.LJCRestoreColumnValues(ControlValues);
					EquipmentGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		// Gets or sets the ControlValues item.
		private ControlValues ControlValues { get; set; }
		#endregion

		#region AppModule Implementation

		// Initializes the module.
		/// <include path='items/LJCInit/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public void LJCInit()
		{
			InitializeControls();
		}

		// Returns a reference to the module tab control.
		/// <include path='items/LJCTabs/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public TabControl LJCTabs()
		{
			return FacilityTabs;
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
					mFacilityGridCode.DataRetrieveFacility();
					mEquipmentGridCode.DataRetrieveEquipment();
					break;

				case Change.Facility:
					mUnitGridCode.DataRetrieveUnit();
					FacilityGrid.LJCSetLastRow();
					FacilityGrid.LJCSetCounter(FacilityCounter);
					break;

				case Change.Unit:
					mFixtureGridCode.DataRetrieveFixture();
					UnitGrid.LJCSetLastRow();
					UnitGrid.LJCSetCounter(UnitCounter);
					break;

				case Change.Fixture:
					FixtureGrid.LJCSetLastRow();
					FixtureGrid.LJCSetCounter(FixtureCounter);
					break;

				case Change.Equipment:
					EquipmentGrid.LJCSetLastRow();
					EquipmentGrid.LJCSetCounter(EquipmentCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Facility,
			Unit,
			Fixture,
			Equipment
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
			bool enableNew;
			bool enableEdit;

			enableNew = true;
			enableEdit = FacilityGrid.CurrentRow != null;
			FormCommon.SetToolState(FacilityTool, enableNew, enableEdit);
			FormCommon.SetMenuState(FacilityMenu, enableNew, enableEdit);
			FacilityFileEdit.Enabled = true;

			enableNew = FacilityGrid.CurrentRow != null;
			enableEdit = UnitGrid.CurrentRow != null;
			FormCommon.SetToolState(UnitTool, enableNew, enableEdit);
			FormCommon.SetMenuState(UnitMenu, enableNew, enableEdit);

			enableNew = UnitGrid.CurrentRow != null;
			enableEdit = FixtureGrid.CurrentRow != null;
			FormCommon.SetToolState(FixtureTool, enableNew, enableEdit);
			FormCommon.SetMenuState(FixtureMenu, enableNew, enableEdit);

			enableNew = true;
			enableEdit = EquipmentGrid.CurrentRow != null;
			FormCommon.SetToolState(EquipmentTool, enableNew, enableEdit);
			FormCommon.SetMenuState(EquipmentMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		#region Facility

		// Calls the New method.
		private void FacilityToolNew_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoNewFacility();
		}

		// Calls the Edit method.
		private void FacilityToolEdit_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoEditFacility();
		}

		// Calls the Delete method.
		private void FacilityToolDelete_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoDeleteFacility();
		}

		// Calls the New method.
		private void FacilityMenuNew_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoNewFacility();
		}

		// Calls the Edit method.
		private void FacilityMenuEdit_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoEditFacility();
		}

		// Calls the Delete method.
		private void FacilityMenuDelete_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoDeleteFacility();
		}

		// Calls the Refresh method.
		private void FacilityMenuRefresh_Click(object sender, EventArgs e)
		{
			mFacilityGridCode.DoRefreshFacility();
		}

		// 
		private void FacilityMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFacility.{mSettings.ExportTextExtension}";
			FacilityGrid.LJCExportData(fileSpec);
		}

		// 
		private void FacilityMenuCSV_Click(object sender, EventArgs e)
		{
			FacilityGrid.LJCExportData("ExportFacility.csv");
		}

		// Allow display and edit of text file.
		private void FacilityFileEdit_Click(object sender, EventArgs e)
		{
			FormCommon.ShellFile("NotePad.exe");
		}

		// Performs the Close function.
		private void FacilityMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(FacilityPage);
		}

		// Display the help page.
		private void FacilityMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "FacilityList.htm");
		}
		#endregion

		#region Unit

		// Calls the New method.
		private void UnitToolNew_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoNewUnit();
		}

		// Calls the Edit method.
		private void UnitToolEdit_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoEditUnit();
		}

		// Calls the Delete method.
		private void UnitToolDelete_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoDeleteUnit();
		}

		// Calls the New method.
		private void UnitMenuNew_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoNewUnit();
		}

		// Calls the Edit method.
		private void UnitMenuEdit_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoEditUnit();
		}

		// Calls the Delete method.
		private void UnitMenuDelete_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoDeleteUnit();
		}

		// 
		private void UnitMenuOccupant_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoViewOccupant();
		}

		// Calls the Refresh method.
		private void UnitMenuRefresh_Click(object sender, EventArgs e)
		{
			mUnitGridCode.DoRefreshUnit();
		}

		// 
		private void UnitMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportUnit.{mSettings.ExportTextExtension}";
			UnitGrid.LJCExportData(fileSpec);
		}

		// 
		private void UnitMenuCSV_Click(object sender, EventArgs e)
		{
			UnitGrid.LJCExportData("ExportUnit.csv");
		}

		// Performs the Close function.
		private void UnitMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(FacilityPage);
		}

		// Display the help page.
		private void UnitMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "UnitList.htm");
		}
		#endregion

		#region Fixture

		// Calls the New method.
		private void FixtureToolNew_Click(object sender, EventArgs e)
		{
			mFixtureGridCode.DoNewFixture();
		}

		// Calls the Edit method.
		private void FixtureToolEdit_Click(object sender, EventArgs e)
		{
			mFixtureGridCode.DoEditFixture();
		}

		// Calls the Delete method.
		private void FixtureToolDelete_Click(object sender, EventArgs e)
		{
			mFixtureGridCode.DoDeleteFixture();
		}

		// Calls the New method.
		private void FixtureMenuNew_Click(object sender, EventArgs e)
		{
			mFixtureGridCode.DoNewFixture();
		}

		// Calls the Edit method.
		private void FixtureMenuEdit_Click(object sender, EventArgs e)
		{
			mFixtureGridCode.DoEditFixture();
		}

		// Calls the Delete method.
		private void FixtureMenuDelete_Click(object sender, EventArgs e)
		{
			mFixtureGridCode.DoDeleteFixture();
		}

		// Calls the Refresh method.
		private void FixtureMenuRefresh_Click(object sender, EventArgs e)
		{
			mFixtureGridCode.DoRefreshFixture();
		}

		// 
		private void FixtureMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportFixture.{mSettings.ExportTextExtension}";
			FixtureGrid.LJCExportData(fileSpec);
		}

		// 
		private void FixtureMenuCSV_Click(object sender, EventArgs e)
		{
			FixtureGrid.LJCExportData("ExportFixture.csv");
		}

		// Performs the Close function.
		private void FixtureMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(FacilityPage);
		}

		// Show the help page.
		private void FixtureMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "FixtureList.htm");
		}
		#endregion

		#region Equipment

		// Calls the New method.
		private void EquipmentToolNew_Click(object sender, EventArgs e)
		{
			mEquipmentGridCode.DoNewEquipment();
		}

		// Calls the Edit method.
		private void EquipmentToolEdit_Click(object sender, EventArgs e)
		{
			mEquipmentGridCode.DoEditEquipment();
		}

		// Calls the Delete method.
		private void EquipmentToolDelete_Click(object sender, EventArgs e)
		{
			mEquipmentGridCode.DoDeleteEquipment();
		}

		// Calls the New method.
		private void EquipmentMenuNew_Click(object sender, EventArgs e)
		{
			mEquipmentGridCode.DoNewEquipment();
		}

		// Calls the Edit method.
		private void EquipmentMenuEdit_Click(object sender, EventArgs e)
		{
			mEquipmentGridCode.DoEditEquipment();
		}

		// Calls the Delete method.
		private void EquipmentMenuDelete_Click(object sender, EventArgs e)
		{
			mEquipmentGridCode.DoDeleteEquipment();
		}

		// Calls the Refresh method.
		private void EquipmentMenuRefresh_Click(object sender, EventArgs e)
		{
			mEquipmentGridCode.DoRefreshEquipment();
		}

		// 
		private void EquipmentMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportEquipment.{mSettings.ExportTextExtension}";
			EquipmentGrid.LJCExportData(fileSpec);
		}

		// 
		private void EquipmentMenuCSV_Click(object sender, EventArgs e)
		{
			EquipmentGrid.LJCExportData("ExportEquipment.csv");
		}

		// Performs the Close function.
		private void EquipmentMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(EquipmentPage);
		}

		// Show the help page.
		private void EquipmentMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "EquipmentList.htm");
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region Facility

		// Handles the form keys.
		private void FacilityGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "FacilityList.htm");
					break;

				case Keys.F5:
					mFacilityGridCode.DoRefreshFacility();
					e.Handled = true;
					break;

				case Keys.Enter:
					mFacilityGridCode.DoEditFacility();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						FixtureGrid.Select();
					}
					else
					{
						UnitGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void FacilityGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& FacilityGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				FacilityGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Facility);
			}
		}

		// Handles the SelectionChanged event.
		private void FacilityGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (FacilityGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Facility);
			}
			FacilityGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void FacilityGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (FacilityGrid.LJCGetMouseRow(e) != null)
			{
				mFacilityGridCode.DoEditFacility();
			}
		}
		#endregion

		#region Unit

		// Handles the form keys.
		private void UnitGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "UnitList.htm");
					break;

				case Keys.F5:
					mUnitGridCode.DoRefreshUnit();
					e.Handled = true;
					break;

				case Keys.Enter:
					mUnitGridCode.DoEditUnit();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						FacilityGrid.Select();
					}
					else
					{
						FixtureGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void UnitGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& UnitGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				UnitGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Unit);
			}
		}

		// Handles the SelectionChanged event.
		private void UnitGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (UnitGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Unit);
			}
			UnitGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void UnitGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (UnitGrid.LJCGetMouseRow(e) != null)
			{
				mUnitGridCode.DoEditUnit();
			}
		}
		#endregion

		#region Fixture

		// Handles the form keys.
		private void FixtureGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "FixtureList.htm");
					break;

				case Keys.F5:
					mFixtureGridCode.DoRefreshFixture();
					e.Handled = true;
					break;

				case Keys.Enter:
					mFixtureGridCode.DoEditFixture();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						UnitGrid.Select();
					}
					else
					{
						FacilityGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void FixtureGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& FixtureGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				FixtureGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Fixture);
			}
		}

		// Handles the SelectionChanged event.
		private void FixtureGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (FixtureGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Fixture);
			}
			FixtureGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void FixtureGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (FixtureGrid.LJCGetMouseRow(e) != null)
			{
				mFixtureGridCode.DoEditFixture();
			}
		}
		#endregion

		#region Equipment

		// Handles the form keys.
		private void EquipmentGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "EquipmentList.htm");
					break;

				case Keys.F5:
					mEquipmentGridCode.DoRefreshEquipment();
					break;

				case Keys.Enter:
					mEquipmentGridCode.DoEditEquipment();
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void EquipmentGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& EquipmentGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				EquipmentGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Equipment);
			}
		}

		// Handles the SelectionChanged event.
		private void EquipmentGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (EquipmentGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Equipment);
			}
			EquipmentGrid.LJCAllowSelectionChange = false;
		}

		// Handles the MouseDoubleClick event.
		private void EquipmentGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (EquipmentGrid.LJCGetMouseRow(e) != null)
			{
				mEquipmentGridCode.DoEditEquipment();
			}
		}
		#endregion
		#endregion

		#region Properties

		/// <summary> The help file name.</summary>
		public string LJCHelpFile { get; set; }

		// The Managers object.
		internal FacilityManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private EquipmentGridCode mEquipmentGridCode;
		private FacilityGridCode mFacilityGridCode;
		private FixtureGridCode mFixtureGridCode;
		private UnitGridCode mUnitGridCode;
		#endregion
	}
}
