// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _Namespace_
// #Value _ParentName_
// _ClassName_GridCode.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static _FullAppName_._AppName_List;

namespace _Namespace_
{
	// Provides _ClassName_Grid methods for the _AppName_List window.
	internal class _ClassName_GridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal _ClassName_GridCode(_AppName_List parentList)
		{
			parentList.Cursor = Cursors.WaitCursor;
			_AppName_List = parentList;
			_ClassName_Grid = _AppName_List._ClassName_Grid;
			LJCHelpFile = "_AppName_.chm";
			LJCHelpPageList = "_ClassName_List.html";
			LJCHelpPageDetail = "_ClassName_Detail.html";
			_ParentName_Grid = _AppName_List._ParentName_Grid;
			ResetData();
			_AppName_List.Cursor = Cursors.Default;
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			Managers = _AppName_List.Managers;
			m_ClassName_Manager = Managers._ClassName_Manager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			_AppName_List.Cursor = Cursors.WaitCursor;
			_ClassName_Grid.LJCRowsClear();

			SetupGrid();
			if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(_ParentName_.ColumnID);

				var result = m_ClassName_Manager.ResultWithParentID(parentID);
				if (DbResult.HasRows(result))
				{
					foreach (var dbRow in result.Rows)
					{
						RowAddValues(dbRow.Values);
					}
				}
			}
			_AppName_List.Cursor = Cursors.Default;
			_AppName_List.DoChange(Change._ClassName_);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(_ClassName_ dataRecord)
		{
			var retValue = _ClassName_Grid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);
			retValue.LJCSetValues(_ClassName_Grid, dataRecord);
			return retValue;
		}

		// Adds a grid row and updates it with the result values.
		private LJCGridRow RowAddValues(DbValues dbValues)
		{
			var ljcGrid = _ClassName_Grid;
			var retValue = ljcGrid.LJCRowAdd();

			var columnName = _ClassName_.ColumnID;
			var id = dbValues.LJCGetInt32(columnName);
			retValue.LJCSetInt32(columnName, id);

			retValue.LJCSetValues(ljcGrid, dbValues);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdate(_ClassName_ dataRecord)
		{
			if (_ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(_ClassName_Grid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, _ClassName_ dataRecord)
		{
			row.LJCSetInt32(_ClassName_.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelect(_ClassName_ dataRecord)
		{
			bool retValue = false;
			if (dataRecord != null)
			{
				_AppName_List.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in _ClassName_Grid.Rows)
				{
					var rowID = row.LJCGetInt32(_ClassName_.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						_ClassName_Grid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				_AppName_List.Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(_ParentName_.ColumnID);
				string parentName = parentRow.LJCGetString(_ParentName_.ColumnName);

				var location = FormCommon.GetDialogScreenPoint(_ClassName_Grid);
				var detail = new _ClassName_Detail
				{
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = LJCHelpPageDetail,
					LJCLocation = location,
					LJCParentID = parentID,
					LJCParentName = parentName,
				};
				detail.LJCChange += Detail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow
				&& _ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(_ClassName_.ColumnID);
				int parentID = parentRow.LJCGetInt32(_ParentName_.ColumnID);
				string parentName = parentRow.LJCGetString(_ParentName_.ColumnName);

				var location = FormCommon.GetDialogScreenPoint(_ClassName_Grid);
				var detail = new _ClassName_Detail()
				{
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = LJCHelpPageDetail,
					LJCID = id,
					LJCLocation = location,
					LJCParentID = parentID,
					LJCParentName = parentName,
				};
				detail.LJCChange += Detail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow
				&& _ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				bool success = false;
				var title = "Delete Confirmation";
				var message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					success = true;
				}

				if (success)
				{
					// Data from items.
					var id = row.LJCGetInt32(_ClassName_.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ _ClassName_.ColumnID, id }
					};
					m_ClassName_Manager.Delete(keyColumns);
					if (m_ClassName_Manager.AffectedCount < 1)
					{
						success = false;
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
				}

				if (success)
				{
					_ClassName_Grid.Rows.Remove(row);
					_AppName_List.TimedChange(Change._ClassName_);
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			_AppName_List.Cursor = Cursors.WaitCursor;
			int id = 0;
			if (_ClassName_Grid.CurrentRow is LJCGridRow row)
			{
				// Save the original row.
				id = row.LJCGetInt32(_ClassName_.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				var record = new _ClassName_()
				{
					ID = id
				};
				RowSelect(record);
			}
			_AppName_List.Cursor = Cursors.Default;
		}

		// Adds new row or updates row with changes from the detail dialog.
		private void Detail_Change(object sender, EventArgs e)
		{
			var detail = sender as _ClassName_Detail;
			var record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdate(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				var row = RowAdd(record);
				_ClassName_Grid.LJCSetCurrentRow(row, true);
				_AppName_List.TimedChange(Change._ClassName_);
			}
		}
		#endregion

		#region Setup Methods

		// Configures the _ClassName_ Grid.
		private void SetupGrid()
		{
			if (0 == _ClassName_Grid.Columns.Count)
			{
				List<string> propertyNames = new List<string>()
				{
					_ClassName_.ColumnName,
					_ClassName_.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns gridColumns
					= m_ClassName_Manager.GetColumns(propertyNames);

				// Setup the grid columns.
				_ClassName_Grid.LJCAddColumns(gridColumns);
			}
		}
		#endregion

		#region Properties

		// Gets or sets the Managers reference.
		internal Managers_AppName_ Managers { get; set; }

		// Gets or sets the Parent List reference.
		private _AppName_List _AppName_List { get; set; }

		// Gets or sets the _ClassName_Grid reference.
		private LJCDataGrid _ClassName_Grid { get; set; }

		// The help file name.
		private string LJCHelpFile
		{
			get { return mHelpFile; }
			set { mHelpFile = NetString.InitString(value); }
		}
		private string mHelpFile;

		// The List help page name.
		private string LJCHelpPageList
		{
			get { return mHelpPageList; }
			set { mHelpPageList = NetString.InitString(value); }
		}
		private string mHelpPageList;

		// The Detail help page name.
		private string LJCHelpPageDetail
		{
			get { return mHelpPageDetail; }
			set { mHelpPageDetail = NetString.InitString(value); }
		}
		private string mHelpPageDetail;

		// Gets or sets the _ParentName_Grid reference.
		private LJCDataGrid _ParentName_Grid { get; set; }
		#endregion

		#region Class Data

		private _ClassName_Manager m_ClassName_Manager;
		#endregion
	}
}
// #SectionEnd Class
