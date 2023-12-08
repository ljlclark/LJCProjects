// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocAssemblyGridCode.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LJCGenDocEdit.GenDocEditList;

namespace LJCGenDocEdit
{
	// Provides DocAssemblyGrid methods for the GenDocEditList window.
	internal class DocAssemblyGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal DocAssemblyGridCode(GenDocEditList parentList)
		{
			parentList.Cursor = Cursors.WaitCursor;
			GenDocEditList = parentList;
			DocAssemblyGrid = GenDocEditList.DocAssemblyGrid;
			LJCHelpFile = "GenDocEdit.chm";
			LJCHelpPageList = "DocAssemblyList.html";
			LJCHelpPageDetail = "DocAssemblyDetail.html";
			DocAssemblyGroupGrid = GenDocEditList.DocAssemblyGroupGrid;
			ResetData();
			GenDocEditList.Cursor = Cursors.Default;
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			Managers = GenDocEditList.Managers;
			mDocAssemblyManager = Managers.DocAssemblyManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			GenDocEditList.Cursor = Cursors.WaitCursor;
			DocAssemblyGrid.LJCRowsClear();

			SetupGrid();
			if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);

				var result = mDocAssemblyManager.ResultWithParentID(parentID);
				if (DbResult.HasRows(result))
				{
					foreach (var dbRow in result.Rows)
					{
						RowAddValues(dbRow.Values);
					}
				}
			}
			GenDocEditList.Cursor = Cursors.Default;
			GenDocEditList.DoChange(Change.DocAssembly);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(DocAssembly dataRecord)
		{
			var retValue = DocAssemblyGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);
			retValue.LJCSetValues(DocAssemblyGrid, dataRecord);
			return retValue;
		}

		// Adds a grid row and updates it with the result values.
		private LJCGridRow RowAddValues(DbValues dbValues)
		{
			var ljcGrid = DocAssemblyGrid;
			var retValue = ljcGrid.LJCRowAdd();

			var columnName = DocAssembly.ColumnID;
			var id = dbValues.LJCGetInt32(columnName);
			retValue.LJCSetInt32(columnName, id);

			retValue.LJCSetValues(ljcGrid, dbValues);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdate(DocAssembly dataRecord)
		{
			if (DocAssemblyGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(DocAssemblyGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, DocAssembly dataRecord)
		{
			row.LJCSetInt32(DocAssembly.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelect(DocAssembly dataRecord)
		{
			bool retValue = false;
			if (dataRecord != null)
			{
				GenDocEditList.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in DocAssemblyGrid.Rows)
				{
					var rowID = row.LJCGetInt32(DocAssembly.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						DocAssemblyGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				GenDocEditList.Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
				string parentName = parentRow.LJCGetString(DocAssemblyGroup.ColumnName);

				var location = FormCommon.GetDialogScreenPoint(DocAssemblyGrid);
				var detail = new DocAssemblyDetail
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
			if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow
				&& DocAssemblyGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(DocAssembly.ColumnID);
				int parentID = parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
				string parentName = parentRow.LJCGetString(DocAssemblyGroup.ColumnName);

				var location = FormCommon.GetDialogScreenPoint(DocAssemblyGrid);
				var detail = new DocAssemblyDetail()
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
			if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow
				&& DocAssemblyGrid.CurrentRow is LJCGridRow row)
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
					var id = row.LJCGetInt32(DocAssembly.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ DocAssembly.ColumnID, id }
					};
					mDocAssemblyManager.Delete(keyColumns);
					if (mDocAssemblyManager.AffectedCount < 1)
					{
						success = false;
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
				}

				if (success)
				{
					DocAssemblyGrid.Rows.Remove(row);
					GenDocEditList.TimedChange(Change.DocAssembly);
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			GenDocEditList.Cursor = Cursors.WaitCursor;
			int id = 0;
			if (DocAssemblyGrid.CurrentRow is LJCGridRow row)
			{
				// Save the original row.
				id = row.LJCGetInt32(DocAssembly.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				var record = new DocAssembly()
				{
					ID = id
				};
				RowSelect(record);
			}
			GenDocEditList.Cursor = Cursors.Default;
		}

		// Adds new row or updates row with changes from the detail dialog.
		private void Detail_Change(object sender, EventArgs e)
		{
			var detail = sender as DocAssemblyDetail;
			var record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdate(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				var row = RowAdd(record);
				DocAssemblyGrid.LJCSetCurrentRow(row, true);
				GenDocEditList.TimedChange(Change.DocAssembly);
			}
		}
		#endregion

		#region Setup Methods

		// Configures the DocAssembly Grid.
		private void SetupGrid()
		{
			if (0 == DocAssemblyGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>()
				{
					DocAssembly.ColumnName,
					DocAssembly.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns gridColumns
					= mDocAssemblyManager.GetColumns(propertyNames);

				// Setup the grid columns.
				DocAssemblyGrid.LJCAddColumns(gridColumns);
			}
		}
		#endregion

		#region Properties

		// Gets or sets the Managers reference.
		internal ManagersGenDocEdit Managers { get; set; }

		// Gets or sets the Parent List reference.
		private GenDocEditList GenDocEditList { get; set; }

		// Gets or sets the DocAssemblyGrid reference.
		private LJCDataGrid DocAssemblyGrid { get; set; }

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

		// Gets or sets the DocAssemblyGroupGrid reference.
		private LJCDataGrid DocAssemblyGroupGrid { get; set; }
		#endregion

		#region Class Data

		private DocAssemblyManager mDocAssemblyManager;
		#endregion
	}
}
