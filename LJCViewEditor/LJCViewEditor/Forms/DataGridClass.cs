// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using LJCGridDataLib;
using DataDetail;
using LJCDataDetailLib;

namespace LJCViewEditor
{
	/// <summary>Provides DataGrid methods for the ViewEditorList window.</summary>
	internal class DataGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal DataGridClass(ViewEditorList parent)
		{
			Parent = parent;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewData()
		{
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditData()
		{
			DbColumns dataColumns;

			DbRequest dbRequest = Parent.DoGetViewRequest();
			if (dbRequest != null)
			{
				DataManager dataManager = new DataManager(Parent.DbServiceRef
					, Parent.DataConfigName, dbRequest.TableName);

				SetKeyValues(dbRequest, dataManager.DataDefinition);
				DbResult dbResult = dataManager.ExecuteRequest(dbRequest);
				if (DbResult.HasData(dbResult))
				{
					ResultGridData resultGridData = new ResultGridData(Parent.DataGrid);
					resultGridData.SetDisplayColumns(dbRequest);
					dataColumns = CreateDbColumnsFromDbValues(resultGridData.DisplayColumns
						, dbResult.Rows[0].Values);
					DataDetailDialog dialog = new DataDetailDialog(Parent.DataConfigName)
					{
						LJCConfigRows = mConfigRows,
						LJCDataColumns = dataColumns
					};
					if (DialogResult.OK == dialog.ShowDialog())
					{
						mConfigRows = dialog.LJCConfigRows;
						//DbColumns resultColumns = dialog.LJCDataColumns;
					}
				}
			}
		}
		private ControlRows mConfigRows;

		// Deletes the selected row.
		internal void DoDeleteData()
		{
			string title;
			string message;

			if (Parent.DataGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					DbRequest dbRequest = Parent.DoGetViewRequest();
					if (dbRequest != null)
					{
						dbRequest.RequestTypeName = "Delete";
						DataManager dataManager = new DataManager(Parent.DbServiceRef
						, Parent.DataConfigName, dbRequest.TableName);
						SetKeyValues(dbRequest, dataManager.DataDefinition);
						//DbResult dbResult = dataManager.ExecuteRequest(dbRequest);

						Parent.DataGrid.Rows.Remove(row);
					}
				}
			}
		}

		#endregion

		#region Private Methods

		// Creates combined DbColumns from DbColumns and DbValues.
		private static DbColumns CreateDbColumnsFromDbValues(DbColumns dbColumns
			, DbValues dbValues)
		{
			DbColumn findColumn;
			DbColumns retValue = null;

			//if (dbColumns != null && dbValues != null)
			if (DbColumns.HasItems(dbColumns) && DbValues.HasItems(dbValues))
			{
				retValue = new DbColumns();
				foreach (DbValue dbValue in dbValues)
				{
					findColumn = dbColumns.LJCSearchName(dbValue.PropertyName);
					DbColumn dbColumn = new DbColumn()
					{
						ColumnName = findColumn.ColumnName,
						Caption = findColumn.Caption,
						DataTypeName = findColumn.DataTypeName,
						MaxLength = findColumn.MaxLength,
						Value = dbValue.Value
					};
					if (0 == dbColumn.MaxLength)
					{
						dbColumn.MaxLength = 10;
					}
					if (dbColumn.MaxLength < 5)
					{
						dbColumn.MaxLength += 3;
					}
					retValue.Add(dbColumn);
				}
			}
			return retValue;
		}

		// Sets the DbRequest Key values.
		private void SetKeyValues(DbRequest dbRequest, DbColumns dataDefinition)
		{
			if (Parent.DataGrid.CurrentRow is LJCGridRow row)
			{
				foreach (DbColumn dbColumn in dataDefinition)
				{
					if (dbColumn.IsPrimaryKey)
					{
						if (null == dbRequest.KeyColumns)
						{
							dbRequest.KeyColumns = new DbColumns();
						}

						switch (dbColumn.DataTypeName)
						{
							case "Int32":
								dbColumn.Value = row.LJCGetInt32(dbColumn.ColumnName);
								break;

							case "String":
								dbColumn.Value = row.LJCGetString(dbColumn.ColumnName);
								break;
						}
						dbRequest.KeyColumns.Add(dbColumn);
					}
				}
			}
		}
		#endregion

		#region Class Data

		private readonly ViewEditorList Parent;
		#endregion
	}
}
