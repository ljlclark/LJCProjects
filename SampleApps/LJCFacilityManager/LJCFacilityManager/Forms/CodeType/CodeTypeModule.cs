// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeModule.cs
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
	public partial class CodeTypeModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CodeTypeModule()
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

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveCodeType()
		{
			CodeTypes records;

			CodeTypeGrid.LJCRowsClear();
			var codeTypeManager = Managers.CodeTypeManager;
			DbJoins dbJoins = codeTypeManager.GetLoadJoins();
			codeTypeManager.SetOrderByClassDescription();
			records = codeTypeManager.Load(joins: dbJoins);

			if (NetCommon.HasItems(records))
			{
				foreach (CodeType record in records)
				{
					RowAddCodeType(record);
				}
			}
			DoChange(Change.CodeType);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddCodeType(CodeType dataRecord)
		{
			LJCGridRow retValue;

			retValue = CodeTypeGrid.LJCRowAdd();
			SetStoredValuesCodeType(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(CodeTypeGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateCodeType(CodeType dataRecord)
		{
			if (CodeTypeGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesCodeType(row, dataRecord);
				row.LJCSetValues(CodeTypeGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesCodeType(LJCGridRow row, CodeType dataRecord)
		{
			row.LJCSetInt32(CodeType.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectCodeType(CodeType dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in CodeTypeGrid.Rows)
				{
					rowID = row.LJCGetInt32(CodeType.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						CodeTypeGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		/// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoNewCodeType()
		{
			CodeTypeDetail detail;

			detail = new CodeTypeDetail()
			{
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = "CodeTypeDetail.htm"
			};
			detail.LJCChange += new EventHandler<EventArgs>(CodeTypeDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoEditCodeType()
		{
			CodeTypeDetail detail;

			if (CodeTypeGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(CodeType.ColumnID);

				detail = new CodeTypeDetail()
				{
					LJCID = id,
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = "CodeTypeDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(CodeTypeDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		void CodeTypeDetail_Change(object sender, EventArgs e)
		{
			CodeTypeDetail detail;
			LJCGridRow row;

			detail = sender as CodeTypeDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateCodeType(detail.LJCRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddCodeType(detail.LJCRecord);
				CodeTypeGrid.LJCSetCurrentRow(row, true);
				TimedChange(Change.CodeType);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoDeleteCodeType()
		{
			string title;
			string message;

			if (CodeTypeGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ CodeType.ColumnID, row.LJCGetInt32(CodeType.ColumnID) }
					};
					Managers.CodeTypeManager.Delete(keyColumns);
					if (Managers.CodeTypeManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						CodeTypeGrid.Rows.Remove(row);
						TimedChange(Change.CodeType);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoRefreshCodeType()
		{
			CodeType record;
			int id = 0;

			if (CodeTypeGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(CodeType.ColumnID);
			}
			DataRetrieveCodeType();

			// Select the original row.
			if (id > 0)
			{
				record = new CodeType()
				{
					ID = id
				};
				RowSelectCodeType(record);
			}
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

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\CodeType.xml";

			SetupGridCodeType();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Setup the grid columns.
		private void SetupGridCodeType()
		{
			CodeTypeGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == CodeTypeGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					CodeType.ColumnCode,
					CodeType.ColumnDescription,
					CodeType.ColumnCodeTypeClassDescription
				};

				// Get the grid columns from the manager Data Definition.
				var columns = Managers.CodeTypeManager.GetColumns(propertyNames);

				// Setup the grid columns.
				CodeTypeGrid.LJCAddColumns(columns);
			}
		}

		// Saves the control values. 
		private void SaveControlValues()
		{
			Control parent = CodeTypeTabs.Parent;
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			CodeTypeGrid.LJCSaveColumnValues(controlValues);

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
			Control parent = CodeTypeTabs.Parent;

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
					CodeTypeGrid.LJCRestoreColumnValues(ControlValues);
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
			return CodeTypeTabs;
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
					RestoreControlValues();

					// Load first List.
					DataRetrieveCodeType();
					break;

				case Change.CodeType:
					CodeTypeGrid.LJCSetLastRow();
					CodeTypeGrid.LJCSetCounter(CodeTypeCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			CodeType
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
			bool enableEdit = CodeTypeGrid.CurrentRow != null;
			FormCommon.SetToolState(CodeTypeTool, enableNew, enableEdit);
			FormCommon.SetMenuState(CodeTypeMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		// Calls the New method.
		private void CodeTypeToolNew_Click(object sender, EventArgs e)
		{
			DoNewCodeType();
		}

		// Calls the Edit method.
		private void CodeTypeToolEdit_Click(object sender, EventArgs e)
		{
			DoEditCodeType();
		}

		// Calls the Delete method.
		private void CodeTypeToolDelete_Click(object sender, EventArgs e)
		{
			DoDeleteCodeType();
		}

		// Calls the New method.
		private void CodeTypeMenuNew_Click(object sender, EventArgs e)
		{
			DoNewCodeType();
		}

		// Calls the Edit method.
		private void CodeTypeMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditCodeType();
		}

		// Calls the Delete method.
		private void CodeTypeMenuDelete_Click(object sender, EventArgs e)
		{
			DoDeleteCodeType();
		}

		// Calls the Refresh method.
		private void CodeTypeMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshCodeType();
		}

		// 
		private void CodeTypeMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportCodeType.{mSettings.ExportTextExtension}";
			CodeTypeGrid.LJCExportData(fileSpec);
		}

		// 
		private void CodeTypeMenuCSV_Click(object sender, EventArgs e)
		{
			CodeTypeGrid.LJCExportData("ExportCodeType.csv");
		}

		// Performs the Close function.
		private void CodeTypeMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(CodeTypePage);
		}

		// Display the help page.
		private void CodeTypeMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "CodeTypeList.htm");
		}
		#endregion

		#region Control Event Handlers

		// Handles the form keys.
		private void CodeTypeGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "CodeTypeList.htm");
					break;

				case Keys.F5:
					DoRefreshCodeType();
					break;

				case Keys.Enter:
					DoEditCodeType();
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void CodeTypeGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& CodeTypeGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				CodeTypeGrid.LJCSetCurrentRow(e);
				TimedChange(Change.CodeType);
			}
		}

		// Handles the SelectionChanged event.
		private void CodeTypeGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (CodeTypeGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.CodeType);
			}
			CodeTypeGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void CodeTypeGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (CodeTypeGrid.LJCGetMouseRow(e) != null)
			{
				DoEditCodeType();
			}
		}
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
		#endregion
	}
}
