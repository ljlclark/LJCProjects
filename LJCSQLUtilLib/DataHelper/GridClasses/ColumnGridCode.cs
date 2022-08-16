// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using DataDetail;
using LJCSQLUtilLibDAL;
using LJCDataDetailLib;

namespace DataHelper
{
	// Contains the Grid methods.
	internal class ColumnGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal ColumnGridCode(MainList parent)
		{
			// Set default class data.
			mParent = parent;
			mManagers = mParent.Managers;
			mConfigRows = ControlRows.Deserialize(ConfigRowFileName);
			mDataConfigName = mManagers.DbMetaDataColumnManager.DataConfigName;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveColumn()
		{
			DbMetaDataColumns records;

			mParent.Cursor = Cursors.WaitCursor;
			mParent.ColumnGrid.LJCRowsClear();

			var dataColumnManager = mManagers.DbMetaDataColumnManager;
			if (mParent.TableGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(DbMetaDataColumn.ColumnID);

				var keyColumns = new DbColumns()
				{
					{ DbMetaDataColumn.ColumnDbMetaDataTableID, parentID }
				};
				records = dataColumnManager.Load(keyColumns);

				if (records != null && records.Count > 0)
				{
					foreach (DbMetaDataColumn record in records)
					{
						RowAddColumn(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(MainList.ChangeColumn);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddColumn(DbMetaDataColumn record)
		{
			LJCGridRow retValue = null;

			retValue = mParent.ColumnGrid.LJCRowAdd();
			SetStoredValuesColumn(retValue, record);

			// Sets the row values from a data object.
			mParent.ColumnGrid.LJCRowSetValues(retValue, record);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateColumn(DbMetaDataColumn record)
		{
			if (mParent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesColumn(row, record);
				mParent.ColumnGrid.LJCRowSetValues(row, record);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesColumn(LJCGridRow row, DbMetaDataColumn record)
		{
			row.LJCSetInt32(DbMetaDataColumn.ColumnID, record.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectColumn(DbMetaDataColumn record)
		{
			int rowID;
			bool retValue = false;

			if (record != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mParent.ColumnGrid.Rows)
				{
					rowID = row.LJCGetInt32(DbMetaDataColumn.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mParent.ColumnGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				mParent.Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewColumn()
		{
			DbColumns dataColumns
				= mManagers.DbMetaDataColumnManager.DataDefinition;

			DataDetailDialog detail = new DataDetailDialog(mDataConfigName)
			{
				LJCConfigRows = mConfigRows,
				LJCDataColumns = dataColumns,
				LJCIsUpdate = false
			};
			detail.LJCChange += ColumnDetail_Change;
			if (DialogResult.OK == detail.ShowDialog())
			{
				mConfigRows = detail.LJCConfigRows;
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditColumn()
		{
			if (mParent.TableGrid.CurrentRow is LJCGridRow parentRow
				&& mParent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int id = row.LJCGetInt32(DbMetaDataColumn.ColumnID);
				int parentID = parentRow.LJCGetInt32(DbMetaDataTable.ColumnID);

				// Retrieve Data
				var keyColumns = new DbColumns()
				{
					{ DbMetaDataColumn.ColumnID, id }
				};
				DataManager dataColumnManager
					= mManagers.DbMetaDataColumnManager.DataManager;
				DbResult dbResult = dataColumnManager.Retrieve(keyColumns);
				if (DbResult.HasData(dbResult))
				{
					// The Data Definition and Record values are merged. 
					DbColumns dataColumns = dbResult.GetValueColumns();

					DataDetailDialog detail = new DataDetailDialog(mDataConfigName)
					{
						LJCConfigRows = mConfigRows,
						LJCDataColumns = dataColumns,
						LJCIsUpdate = true
					};
					detail.LJCChange += ColumnDetail_Change;
					if (DialogResult.OK == detail.ShowDialog())
					{
						mConfigRows = detail.LJCConfigRows;
					}
				}
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void ColumnDetail_Change(object sender, EventArgs e)
		{
			DataDetailDialog detail;
			DbColumns dataColumns;

			detail = sender as DataDetailDialog;
			dataColumns = detail.LJCDataColumns;
			ResultConverter<DbMetaDataColumn, DbMetaDataColumns> resultConverter
				= new ResultConverter<DbMetaDataColumn, DbMetaDataColumns>();
			DbMetaDataColumn mdColumn = resultConverter.CreateData(dataColumns);

			var dataColumnManager = mManagers.DbMetaDataColumnManager;
			if (detail.LJCIsUpdate)
			{
				var keyColumns = new DbColumns()
				{
					{ DbMetaDataColumn.ColumnID, mdColumn.ID }
				};
				dataColumnManager.Update(mdColumn, keyColumns);
				if (dataColumnManager.AffectedCount > 0)
				{
					RowUpdateColumn(mdColumn);
				}
			}
			else
			{
				dataColumnManager.Add(mdColumn);
				if (dataColumnManager.AffectedCount > 0)
				{
					LJCGridRow row = RowAddColumn(mdColumn);
					mParent.ColumnGrid.LJCSetCurrentRow(row, true);
					mParent.ChangeTimer.DoChange(MainList.ChangeColumn);
				}
			}
		}

		// Deletes the selected row.
		internal void DoDeleteColumn()
		{
			string title;
			string message;

			if (mParent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					int id = row.LJCGetInt32(DbMetaDataColumn.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ DbMetaDataColumn.ColumnID, id }
					};
					var dataColumnManager = mManagers.DbMetaDataColumnManager;
					dataColumnManager.Delete(keyColumns);
					if (dataColumnManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mParent.ColumnGrid.Rows.Remove(row);
						mParent.ChangeTimer.DoChange(MainList.ChangeColumn);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshColumn()
		{
			DbMetaDataColumn record;
			int id = 0;

			if (mParent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(DbMetaDataColumn.ColumnID);
			}
			DataRetrieveColumn();

			// Select the original row.
			if (id > 0)
			{
				record = new DbMetaDataColumn()
				{
					ID = id
				};
				RowSelectColumn(record);
			}
		}

		// Performs the Close function.
		internal void DoCloseColumn()
		{
			if (mConfigRows != null)
			{
				RowOrderComparer comparer = new RowOrderComparer();
				mConfigRows.SortRowOrder(comparer);
				mConfigRows.Serialize(ConfigRowFileName);
			}
		}
		#endregion

		#region Class Data

		private readonly string ConfigRowFileName = @"DetailConfigs/ColumnDetailConfig.xml";
		private ControlRows mConfigRows;
		private readonly string mDataConfigName;
		private MainList mParent;
		private SQLUtilLibManagers mManagers;
		#endregion
	}
}
