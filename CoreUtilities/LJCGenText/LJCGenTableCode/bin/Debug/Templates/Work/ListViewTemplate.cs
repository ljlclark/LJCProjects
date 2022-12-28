// Copyright (c) Lester J. Clark 2020, 2021 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJC.Net.Common;
using LJC.WinForm.Common;
using LJC.WinForm.Controls;
using LJC.DBServiceLib;
using LJC.QueryGridLib;
using LJC.DBView.DAL;
// #SectionBegin Class
using _FullAppName_.DAL;

namespace _Namespace_
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../CommonList.xml'/>
	public partial class _ClassName_List : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../CommonData.xml'/>
		public _ClassName_List()
		{
			InitializeComponent();

			// Set default class data.
			mViewTableName = _ClassName_.TableName;
			LJCHelpFile = "_AppName_.chm";
			LJCHelpPageList = "_ClassName_List.htm";
			LJCHelpPageDetail = "_ClassName_Detail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void _ClassName_List_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Data Methods

		#region View

		// Retrieves the View combo items.
		private void DataRetrieveView()
		{
			ViewTable viewTable = mViewTableManager.RetrieveByUniqueKey(mViewTableName);
			if (null == viewTable)
			{
				mViewHelper.CreateDefaultViewTable(mViewTableName);
				DoListChange(ListType.View);
			}
			else
			{
				mViewTableId = viewTable.Id;
				Views views = mViewDataManager.LoadByParentId(mViewTableId);
				if (views != null)
				{
					//foreach (ViewData viewData in views)
					//{
					//	ViewCombo.LJCAddItem(viewData.Id, viewData.Description);
					//}
					//if (ViewCombo.Items.Count > 0)
					//{
					//	ViewCombo.LJCSetByItemID(mViewDataId);
					//	if (ViewCombo.SelectedIndex < 0)
					//	{
					//		ViewCombo.SelectedIndex = 0;
					//	}
					//}
				}
			}
		}

		// Retrieve the View list rows.
		private bool DataRetrieve_ClassName_View()
		{
			bool retValue = true;

			// Get the defined View data.
			int viewDataId = ViewCombo.LJCSelectedItemId();
			DBResult dbResult = mViewHelper.GetViewData(_ClassName_.TableName, viewDataId);
			if (LJCFormCommon.ShowError(mViewHelper.ErrorText)
				|| null == dbResult)
			{
				retValue = false;
			}
			else
			{
				// Display the defined View data.
				SetupGrid_ClassName_();
				QueryGrid queryGrid = new QueryGrid(_ClassName_Grid)
				{
					DisplayColumns = mDisplayColumns_ClassName_
				};
				//queryGrid.AddRow += QueryGrid_AddRow;

				// Sets the row values from DbResult records.
				queryGrid.DataRetrieveColumns(dbResult);
			}
			return retValue;
		}

		//// <summary>
		//// Handles the AddRow event.
		//// </summary>
		//private void QueryGrid_AddRow(object sender, EventArgs e)
		//{
		//	QueryGrid queryGrid = sender as QueryGrid;
		//
		//	LJCGridRow row = queryGrid.GridRow;
		//	DbColumns dbColumns = queryGrid.DataRecord;
		//	_ClassName_ record = new _ClassName_()
		//	{
		//		Id = dbColumns.GetInt(_ClassName_.ColumnId),
		//		FirstName = dbColumns.GetValue(_ClassName_.ColumnFirstName),
		//		MiddleInitial = dbColumns.GetValue(_ClassName_.ColumnMiddleInitial),
		//		LastName = dbColumns.GetValue(_ClassName_.ColumnLastName)
		//	};
		//	SetStoredValues_ClassName_(row, record);
		//}
		#endregion

		#region _ClassName_

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../CommonList.xml'/>
		private void DataRetrieve_ClassName_()
		{
			_CollectionName_ records;

			Cursor = Cursors.WaitCursor;
			_ClassName_Grid.LJCRowsClear();

			// Load the selected view data.
			if (false == DataRetrieve_ClassName_View())
			{
				//// The requested view was not found.
				//// if is child grid.
				//if (ParentGrid.CurrentRow is LJCGridRow parentRow)
				//{
				//	_ClassName_ keyRecord = new _ClassName_()
				//	{
				//		ParentId = parentRow.LJCGetInt(Parent.ColumnId)
				//	};
				//	records = m_ClassName_Manager.Load(keyRecord);
				records = m_ClassName_Manager.Load();

				if (false == LJCFormCommon.ShowError(m_ClassName_Manager.ErrorText)
					&& records != null && records.Count > 0)
				{
					// Create a standard view.
					DBQuery dbQuery = m_ClassName_Manager.DataManager.Query;
					ViewData viewData = mViewHelper.CreateDefaultViewData(dbQuery);
					mViewDataId = viewData.Id;

					// Reload the available views.
					DataRetrieveView();

					//// Use if there is no View capability.
					//foreach (_ClassName_ record in records)
					//{
					//	RowAdd_ClassName_(record);
					//}
				}
			}

			// Set to seeded startup Select record.
			if (LJCSelectedRecord != null)
			{
				RowSelectPerson(LJCSelectedRecord);
				LJCSelectedRecord = null;
			}

			Cursor = Cursors.Default;
			ListChange(ListType._ClassName_);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../CommonList.xml'/>
		private LJCGridRow RowAdd_ClassName_(_ClassName_ record)
		{
			LJCGridRow retValue;

			retValue = _ClassName_Grid.LJCRowAdd();
			SetStoredValues_ClassName_(retValue, record);

			// Sets the row values from a data object.
			_ClassName_Grid.LJCRowSetValues(retValue, record);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../CommonList.xml'/>
		private void RowUpdate_ClassName_(_ClassName_ record)
		{
			if (_ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues_ClassName_(row, record);
				_ClassName_Grid.LJCRowSetValues(row, record);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../CommonList.xml'/>
		private void SetStoredValues_ClassName_(LJCGridRow row, _ClassName_ record)
		{
			row.LJCSetInt(_ClassName_.ColumnID, record.ID);

			//// if has child records.
			//row.LJCSetString(_ClassName_.ColumnName, record.Name);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../CommonList.xml'/>
		private bool RowSelect_ClassName_(_ClassName_ record)
		{
			int rowId;
			bool retValue = false;

			if (record != null)
			{
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in _ClassName_Grid.Rows)
				{
					rowId = row.LJCGetInt(_ClassName_.ColumnId);
					if (rowId == record.Id)
					{
						_ClassName_Grid.LJCSetCurrentRow(row, true);
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

		#region _ClassName_

		// Performs the default list action.
		/// <include path='items/DoDefault/*' file='../../CommonList.xml'/>
		private void DoDefault_ClassName_()
		{
			if (LJCIsSelect)
			{
				DoSelect_ClassName_();
			}
			else
			{
				DoEdit_ClassName_();
			}
		}

		// Displays a detail dialog for a new record.
		/// <include path='items/DoNew/*' file='../../CommonList.xml'/>
		private void DoNew_ClassName_()
		{
			_ClassName_Detail detail;

			//// If child grid.
			//if (ParentGrid.CurrentRow is LJCGridRow parentRow)
			//{
			//  // Data from list items.
			//  int parentID = parentRow.LJCGetInt(Parent.ColumnId);
			//  string parentName = parentRow.LJCGetString(Parent.ColumnName);

			detail = new _ClassName_Detail()
			{
				//LJCParentID = parentID,
				//LJCParentName = parentName,
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = LJCHelpPageDetail,
			};
			detail.LJCChange += _ClassName_Detail_Change;
			detail.ShowDialog();
			//}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../CommonList.xml'/>
		private void DoEdit_ClassName_()
		{
			_ClassName_Detail detail;

			//// If child grid.
			//if (ParentGrid.CurrentRow is LJCGridRow parentRow
			//  && _ClassName_Grid.CurrentRow is LJCGridRow row)
			//{
			//  // Data from list items.
			//  int id = row.LJCGetInt(_ClassName_.ColumnID);
			//  int parentID = parentRow.LJCGetInt(Parent.ColumnID);
			//  string parentName = parentRow.LJCGetString(Parent.ColumnName);
			//
			if (_ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				detail = new _ClassName_Detail()
				{
					LJCID = id,
					//LJCParentId = parentID,
					//LJCParentName = parentName,
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = LJCHelpPageDetail,
				};
				detail.LJCChange += _ClassName_Detail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		/// <include path='items/Detail_Change/*' file='../../CommonList.xml'/>
		void _ClassName_Detail_Change(object sender, EventArgs e)
		{
			_ClassName_Detail detail;
			_ClassName_ record;
			LJCGridRow row;

			detail = sender as _ClassName_Detail;
			if (false == detail.LJCHasError)
			{
				record = detail.LJCRecord;
				if (detail.LJCIsUpdate)
				{
					RowUpdate_ClassName_(record);
				}
				else
				{
					row = RowAdd_ClassName_(record);
					_ClassName_Grid.LJCSetCurrentRow(row);
					DoListChange(ListType._ClassName_);
				}
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../CommonList.xml'/>
		private void DoDelete_ClassName_()
		{
			_ClassName_ keyRecord;
			string title;
			string message;

			//// If child grid.
			//if (ParentGrid.CurrentRow is LJCGridRow parentRow
			//  && _ClassName_Grid.CurrentRow is LJCGridRow row)
			//{
			if (_ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = LJCFormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int parentID = parentRow.LJCGetInt(_ClassName_.ColumnId);
					//int childID = row.LJCGetInt(Child.ColumnID);

					keyRecord = new _ClassName_()
					{
						ID = parentID
						//ChildID = childID
					};
					m_ClassName_Manager.Delete(keyRecord);
					if (LJCNetCommon.HasValue(m_ClassName_Manager.ErrorText))
					{
						message = LJCFormCommon.DeleteError + "\r\n"
							+ m_ClassName_Manager.ErrorText;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						_ClassName_Grid.Rows.Remove(row);
						DoListChange(ListType._ClassName_);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../CommonList.xml'/>
		private void DoRefresh_ClassName_()
		{
			_ClassName_ record;
			int id = 0;

			if (_ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt(_ClassName_.ColumnId);
			}
			DataRetrieve_ClassName_();

			// Select the original row.
			if (id > 0)
			{
				record = new _ClassName_()
				{
					Id = id
				};
				RowSelect_ClassName_(record);
			}
		}

		// Sets the selected item and returns to the parent form.
		/// <include path='items/DoSelect/*' file='../../CommonList.xml'/>
		private void DoSelect_ClassName_()
		{
			_ClassName_ record;
			int id;

			LJCSelectedRecord = null;
			if (_ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				Cursor = Cursors.WaitCursor;
				id = row.LJCGetInt(_ClassName_.ColumnId);

				_ClassName_ keyRecord = new _ClassName_()
				{
					ID = id
				};
				record = m_ClassName_Manager.Retrieve(keyRecord);
				if (record != null)
				{
					LJCSelectedRecord = record;
				}
				Cursor = Cursors.Default;
				DialogResult = DialogResult.OK;
			}
		}
		#endregion
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../CommonList.xml'/>
		private void InitializeControls()
		{
			// Get singleton values.
			mValues = _AppName_Values.Instance;
			LJCFormCommon.ShowError(mValues.ErrorText);
			if (mValues.IsSuccess)
			{
				mDbServiceRef = mValues.DbServiceRef;
				mDataConfigName = mValues.DataConfigName;

				// Initialize Class Data.
				mViewHelper = new ViewHelper(mDataConfigName);
				LJCFormCommon.ShowError(mViewHelper.ErrorText);
				m_ClassName_Manager = new _ClassName_Manager(mDbServiceRef
					, mDataConfigName);
				LJCFormCommon.ShowError(m_ClassName_Manager.ErrorText);

				if (m_ClassName_Manager.IsSuccess)
				{
					// Load control data.

					// Configure controls.
					if (LJCIsSelect)
					{
						// This is a Selection List.
						_ClassName_MenuEdit.ShortcutKeyDisplayString = "";
						_ClassName_MenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
					}
					else
					{
						// This is a display list.
						_ClassName_Separator.Visible = false;
						_ClassName_MenuSelect.Visible = false;
					}

					// Set initial control values.
					mControlValuesFileName = "ControlValues_ClassName_.xml";

					// Setup primary grid here if no ViewData.
					//SetupGrid_ClassName_();
					//SetupGridRelated();
					StartWorkList();
				}
			}
		}

		// Setup the grid display columns.
		private void SetupGrid_ClassName_()
		{
			_ClassName_Grid.BackgroundColor = mValues.BeginColor;

			// Clear the previous display columns definition as the view
			// may have changed.
			_ClassName_Grid.Columns.Clear();

			// Get the view display columns
			int viewDataId = ViewCombo.LJCSelectedItemId();
			mDisplayColumns_ClassName_ = mViewHelper.GetDisplayColumns(viewDataId);

			// Setup the default display columns if the view display columns
			// are not found.
			// Does not use a view.
			//if (0 == _ClassName_Grid.Columns.Count)
			if (null == mDisplayColumns_ClassName_)
			{
				// Modify
				string[] columnNames = new string[] {
					_ClassName_.ColumnName,
					_ClassName_.ColumnDescription
				};

				// Get the display columns from the manager Data Definition.
				mDisplayColumns_ClassName_ = m_ClassName_Manager.GetColumns(columnNames);
			}

			// Setup the grid display columns and column values.
			_ClassName_Grid.LJCAddDisplayColumns(mDisplayColumns_ClassName_);
			_ClassName_Grid.LJCRestoreColumnValues(ControlValues);  // View only.
		}
		private DbColumns mDisplayColumns_ClassName_;

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				// Modify MainSplit.Panel1 controls.
				ListHelper.SetSplitPanelControls(_ClassName_Split.Panel1, _ClassName_Heading
					, _ClassName_ToolPanel, 0, 0, 0);
			}
		}

		// Saves the control values. 
		private void SaveControlValues()
		{
			Control parentWindow = _ClassName_Tabs.Parent;
			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			_ClassName_Grid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("_ClassName_Split.SplitterDistance", 0, 0, 0
				, _ClassName_Split.SplitterDistance);

			// Save Window values.
			// Tabs ParentWindow is not this form.
			if (parentWindow != null
				&& parentWindow.GetType().Name != Name)
			{
				controlValues.Add(formName, parent.Left, parent.Top
					, parent.Width, parent.Height);
			}

			// Save other values.
			//mViewDataId = ViewCombo.LJCSelectedItemId();
			//controlValues.Add("View", mViewDataId, 0, 0, 0);

			LJCNetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
				, mControlValuesFileName, out string errorText);
		}

		// Restores the control values.
		private void RestoreControlValues()
		{
			ControlValue controlValue;
			Control parentWindow = _ClassName_Tabs.Parent;

			if (File.Exists(mControlValuesFileName))
			{
				ControlValues = LJCNetCommon.XmlDeserialize(typeof(ControlValues)
					, mControlValuesFileName, out string errorText) as ControlValues;
				if (ControlValues != null)
				{
					// Restore Window values.
					controlValue = ControlValues.LJCSearchByName(Name);

					// The next line is for list windows only
					// The Form startup WindowState should be set to Minimized.
					WindowState = FormWindowState.Normal;

					if (controlValue != null)
					{
						// Tabs ParentWindow is not this form.
						if (parentWindow != null
							&& parentWindow.GetType().Name != Name)
						{
							parentWindow.Left = controlValue.Left;
							parentWindow.Top = controlValue.Top;
							parentWindow.Width = controlValue.Width;
							parentWindow.Height = controlValue.Height;
						}
					}

					// Restore Splitter, Grid and other values.
					LJCFormCommon.RestoreSplitDistance(_ClassName_Split, ControlValues);

					//_ClassName_Grid.LJCRestoreColumnValues(ControlValues);

					controlValue = ControlValues.Search("View");
					if (controlValue != null)
					{
						mViewDataId = controlValue.Left;
					}
				}
			}
		}

		/// <summary>Gets or sets the ControlValues item.</summary>
		public ControlValues ControlValues { get; set; }
		#endregion

		#region Worklist Implementation

		// Execute the list and related item functions.
		/// <include path='items/ListChange/*' file='../../CommonList.xml'/>
		private void ListChange(ListType listType)
		{
			switch (listType)
			{
				case ListType.Startup:
					ConfigureControls();
					RestoreControlValues();

					// Load first list.
					DataRetrieveView();
					break;

				case ListType.View:
					// Setup primary grid here to load the grid ViewData.
					SetupGrid_ClassName_();
					DataRetrieve_ClassName_();
					break;

				case ListType._ClassName_:
					_ClassName_Grid.LJCSetLastRow();
					_ClassName_Grid.LJCSetCounter(_ClassName_Counter);
					break;
			}
			SetControlState();
		}

		// Sets the control states based on the current control values.
		/// <include path='items/SetControlState/*' file='../../CommonList.xml'/>
		private void SetControlState()
		{
			bool enableNew = true;
			bool enableEdit = _ClassName_Grid.CurrentRow != null;
			LJCFormCommon.SetToolState(_ClassName_Tool, enableNew, enableEdit);
			LJCFormCommon.SetMenuState(_ClassName_Menu, enableNew, enableEdit);
		}

		// Starts the work list processing.
		private void StartWorkList()
		{
			mListChangeTimer = new Timer()
			{
				Interval = 100
			};
			mListChangeTimer.Tick += ListChangeTimer_Tick;
			DoListChange(ListType.Startup);
		}

		// Uses a timer to allow the list to process outstanding messages.
		/// <include path='items/DoListChange/*' file='../../CommonList.xml'/>
		private void DoListChange(ListType listType)
		{
			mListType = listType;
			mListChangeTimer.Start();
		}

		// The timer event handler.
		private void ListChangeTimer_Tick(object sender, EventArgs e)
		{
			mListChangeTimer.Stop();
			ListChange(mListType);
		}

		private Timer mListChangeTimer;
		private ListType mListType;
		private enum ListType
		{
			Startup,
			View,
			_ClassName_
		}
		#endregion

		#region Action Event Handlers

		#region View

		// Displays the ViewBuilder to create a new view.
		private void ViewMenuNew_Click(object sender, EventArgs e)
		{
			ViewBuilderList viewBuilderList = new ViewBuilderList
			{
				LJCParentId = mViewTableId,
				LJCParentName = mViewTableName
			};
			viewBuilderList.Show();
		}

		// Displays the ViewBuilder to create a new view from the
		// existing view.
		private void ViewMenuCopy_Click(object sender, EventArgs e)
		{
			ViewBuilderList viewBuilderList = new ViewBuilderList
			{
				LJCParentId = mViewTableId,
				LJCParentName = mViewTableName,
				LJCViewId = ViewCombo.LJCSelectedItemId()
			};
			viewBuilderList.Show();
		}

		// Displays the ViewBuilder to edit the current view.
		private void ViewMenuEdit_Click(object sender, EventArgs e)
		{
			ViewBuilderList viewBuilderList = new ViewBuilderList
			{
				LJCParentId = mViewTableId,
				LJCParentName = mViewTableName,
				LJCViewId = ViewCombo.LJCSelectedItemId()
			};
			viewBuilderList.ShowDialog();
			if (viewBuilderList.LJCSelectedRecord != null)
			{
				int itemId = viewBuilderList.LJCSelectedRecord.Id;
				if (ViewCombo.LJCSelectedItemId() == itemId)
				{
					DoListChange(ListType.View);
				}
				else
				{
					//ViewCombo.LJCSetByItemID(itemId);
				}
			}
		}
		#endregion

		#region _ClassName_

		// Calls the New method.
		private void _ClassName_ToolNew_Click(object sender, EventArgs e)
		{
			DoNew_ClassName_();
		}

		// Calls the Edit method.
		private void _ClassName_ToolEdit_Click(object sender, EventArgs e)
		{
			DoEdit_ClassName_();
		}

		// Calls the Delete method.
		private void _ClassName_ToolDelete_Click(object sender, EventArgs e)
		{
			DoDelete_ClassName_();
		}

		// Calls the New method.
		private void _ClassName_MenuNew_Click(object sender, EventArgs e)
		{
			DoNew_ClassName_();
		}

		// Calls the Edit method.
		private void _ClassName_MenuEdit_Click(object sender, EventArgs e)
		{
			DoEdit_ClassName_();
		}

		// Calls the Delete method.
		private void _ClassName_MenuDelete_Click(object sender, EventArgs e)
		{
			DoDelete_ClassName_();
		}

		// Calls the Refresh method.
		private void _ClassName_MenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefresh_ClassName_();
		}

		// Calls the Select method.
		private void _ClassName_MenuSelect_Click(object sender, EventArgs e)
		{
			DoSelect_ClassName_();
		}

		// Performs the Close function.
		private void _ClassName_MenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region View

		//// Handles the SelectedIndexChanged event.
		//private void ViewCombo_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	DoListChange(ListType.View);
		//}
		#endregion

		#region _ClassName_

		// Handles the form keys.
		private void _ClassName_Grid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic, LJCHelpPageList);
					break;

				case Keys.F5:
					DoRefresh_ClassName_();
					e.Handled = true;
					break;

				case Keys.Enter:
					DoDefault_ClassName_();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						//Combo.Focus();
					}
					else
					{
						//Combo.Focus();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void _ClassName_Grid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& _ClassName_Grid.LJCIsDifferentRow(e))
			{
				_ClassName_Grid.LJCSetMouseCurrentRow(e);
				DoListChange(ListType._ClassName_);
			}
		}

		// Handles the SelectionChanged event.
		private void _ClassName_Grid_SelectionChanged(object sender, EventArgs e)
		{
			if (_ClassName_Grid.LJCAllowSelectionChange)
			{
				DoListChange(ListType._ClassName_);
			}
			_ClassName_Grid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void _ClassName_Grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (_ClassName_Grid.LJCGetMouseRow(e) != null)
			{
				DoDefault_ClassName_();
			}
		}
		#endregion
		#endregion

		#region Properties

		/// <summary>Gets or sets the LJCIsSelect value.</summary>
		public bool LJCIsSelect { get; set; }

		/// <summary>Gets or sets the parent ID value.</summary>
		public int LJCParentID { get; set; }

		/// <summary>Gets or sets the LJCParentName value.</summary>
		public string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = LJCNetCommon.InitString(value); }
		}
		private string mParentName;

		/// <summary>Gets a reference to the selected record.</summary>
		public _ClassName_ LJCSelectedRecord { get; private set; }

		/// <summary>The help file name.</summary>
		public string LJCHelpFile
		{
			get { return mHelpFile; }
			set { mHelpFile = LJCNetCommon.InitString(value); }
		}
		private string mHelpFile;

		/// <summary>The List help page name.</summary>
		public string LJCHelpPageList
		{
			get { return mHelpPageList; }
			set { mHelpPageList = LJCNetCommon.InitString(value); }
		}
		private string mHelpPageList;

		/// <summary>The Detail help page name.</summary>
		public string LJCHelpPageDetail
		{
			get { return mHelpPageDetail; }
			set { mHelpPageDetail = LJCNetCommon.InitString(value); }
		}
		private string mHelpPageDetail;
		#endregion

		#region Class Data

		private Values_AppName_ mValues;
		private DbServiceRef mDbServiceRef;
		private string mDataConfigName;

		private string mViewTableName;
		private int mViewTableId;
		private int mViewDataId;
		private ViewHelper mViewHelper;

		private _ClassName_Manager m_ClassName_Manager;

		private string mControlValuesFileName;
		#endregion
	}
}
// #SectionEnd Class
