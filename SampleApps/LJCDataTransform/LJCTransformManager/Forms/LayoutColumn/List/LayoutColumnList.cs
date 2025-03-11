// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumnList.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBClientLib;
using LJCTextDataReaderLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
	internal partial class LayoutColumnList : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal LayoutColumnList()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void LayoutColumnList_Load(object sender, EventArgs e)
		{
			InitializeControls();
			CenterToParent();
		}
		#endregion

		#region Action Event Handlers

		#region Layout

		// Calls the New method.
		private void LayoutToolNew_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoNewLayout();
		}

		// Calls the Edit method.
		private void LayoutToolEdit_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoEditLayout();
		}

		// Calls the Delete method.
		private void LayoutToolDelete_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoDeleteLayout();
		}

		// Calls the New method.
		private void LayoutMenuNew_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoNewLayout();
		}

		// Calls the Edit method.
		private void LayoutMenuEdit_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoEditLayout();
		}

		// Calls the Delete method.
		private void LayoutMenuDelete_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoDeleteLayout();
		}

		// Calls the Refresh method.
		private void LayoutMenuRefresh_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoRefreshLayout();
		}

		// Calls the Select method.
		private void LayoutMenuSelect_Click(object sender, EventArgs e)
		{
			mLayoutGridCode.DoSelectLayout();
		}

		// Performs the Close function.
		private void LayoutMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}
		#endregion

		#region LayoutColumn

		// Calls the New method.
		private void LayoutColumnToolNew_Click(object sender, EventArgs e)
		{
			mLayoutColumnGridCode.DoNewLayoutColumn();
		}

		// Calls the Edit method.
		private void LayoutColumnToolEdit_Click(object sender, EventArgs e)
		{
			mLayoutColumnGridCode.DoEditLayoutColumn();
		}

		// Calls the Delete method.
		private void LayoutColumnToolDelete_Click(object sender, EventArgs e)
		{
			mLayoutColumnGridCode.DoDeleteLayoutColumn();
		}

		// Calls the New method.
		private void LayoutColumnMenuNew_Click(object sender, EventArgs e)
		{
			mLayoutColumnGridCode.DoNewLayoutColumn();
		}

		// Calls the Edit method.
		private void LayoutColumnMenuEdit_Click(object sender, EventArgs e)
		{
			mLayoutColumnGridCode.DoEditLayoutColumn();
		}

		// Calls the Delete method.
		private void LayoutColumnMenuDelete_Click(object sender, EventArgs e)
		{
			mLayoutColumnGridCode.DoDeleteLayoutColumn();
		}

		// Calls the Refresh method.
		private void LayoutColumnMenuRefresh_Click(object sender, EventArgs e)
		{
			mLayoutColumnGridCode.DoRefreshLayoutColumn();
		}

		// Import columns from selected file.
		private void LayoutColumnMenuImport_Click(object sender, EventArgs e)
		{
			DbColumns dbColumns = null;
			string fileSpec = null;
			int sourceLayoutID = 0;
			bool success = false;

			if (LayoutGrid.CurrentRow is LJCGridRow parentRow)
			{
				success = true;
				sourceLayoutID = parentRow.LJCGetInt32(SourceLayout.ColumnSourceLayoutID);
			}

			if (success)
			{
				string initialFolder = Environment.CurrentDirectory;
				string filter = "Text(*.txt)|*.txt|XML(*.xml)|*.xml|All Files(*.*)|*.*";
				fileSpec = FormCommon.SelectFile(filter, initialFolder, "*.txt");
				if (null == fileSpec)
				{
					success = false;
				}
			}

			if (success)
			{
				string extension = Path.GetExtension(fileSpec);

				switch (extension.ToLower())
				{
					case ".txt":
						TextDataReader textDataReader = new TextDataReader();
						textDataReader.LJCSetFile(fileSpec);
						dbColumns = textDataReader.LJCDataFields;
						break;

					case ".xml":
						dbColumns = DbColumns.LJCDeserialize(fileSpec);
						break;

					default:
						string message = "Selected file extension must be '.txt' or '.xml'.";
						MessageBox.Show(message, "Invalid File", MessageBoxButtons.OK
							, MessageBoxIcon.Warning);
						break;
				}

				AddLayoutColumns(dbColumns, sourceLayoutID);
				TimedChange(Change.Layout);
			}
		}

		// Writes the LayoutColumns XML file.
		private void LayoutColumnMenuWriteXML_Click(object sender, EventArgs e)
		{
			if (LayoutGrid.CurrentRow is LJCGridRow parentRow)
			{
				var title = "Write Confirmation";
				var message = "Are you sure you want to write the Layout XML file?";
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int layoutID = parentRow.LJCGetInt32(SourceLayout.ColumnSourceLayoutID);
					LayoutColumns layoutColumns
						= Managers.LayoutColumnManager.LoadWithLayoutID(layoutID);
					if (layoutColumns != null)
					{
						string fileSpec;

						string initialFolder = Environment.CurrentDirectory;
						string filter = "XML(*.xml)|*.xml|All Files(*.*)|*.*";
						fileSpec = FormCommon.SaveFile(filter, initialFolder, "*.xml");
						if (fileSpec != null)
						{
							var dbColumns = CreateDbColumnsFromLayoutColumns(layoutColumns);
							if (NetCommon.HasItems(dbColumns))
							{
								dbColumns.LJCSerialize(fileSpec);
							}
						}
					}
				}
			}
		}

		// Create DbColumns from LayoutColumns.
		private DbColumns CreateDbColumnsFromLayoutColumns(LayoutColumns layoutColumns)
		{
			DbColumns retValue = null;

			if (layoutColumns != null)
			{
				retValue = new DbColumns();
				foreach (LayoutColumn layoutColumn in layoutColumns)
				{
					string dataTypeName = GetDataTypeName(layoutColumn.DataTypeID);
					DbColumn dbColumn = new DbColumn()
					{
						ColumnName = layoutColumn.Name,
						Caption = layoutColumn.Name,
						DataTypeName = dataTypeName,
						MaxLength = layoutColumn.Length,
						AllowDBNull = layoutColumn.AllowNull,
						AutoIncrement = layoutColumn.IdentityKey
					};
					retValue.Add(dbColumn);
				}
			}
			return retValue;
		}

		// Get the DataType name with the DataType ID.
		private string GetDataTypeName(short dataTypeID)
		{
			string retValue = "String";

			DataType dataType = Managers.DataTypeManager.RetrieveWithID(dataTypeID);
			if (dataType != null)
			{
				retValue = dataType.Name;
			}
			return retValue;
		}

		// Performs the Close function.
		private void LayoutColumnMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Adds the LayoutColumns.
		private void AddLayoutColumns(DbColumns dbColumns, int sourceLayoutID)
		{
			LayoutColumn dataRecord;

			if (NetCommon.HasItems(dbColumns))
			{
				foreach (DbColumn dbColumn in dbColumns)
				{
					var searchColumn = GetLayoutColumnWithName(dbColumn.ColumnName
						, sourceLayoutID);
					if (null == searchColumn)
					{
						dataRecord = CreateLayoutColumn(dbColumn, sourceLayoutID);
						if (dataRecord != null)
						{
							Managers.LayoutColumnManager.Add(dataRecord);
						}
					}
				}
			}
		}

		// Retrieve the LayoutColumn with Name.
		private LayoutColumn GetLayoutColumnWithName(string name, int sourceLayoutID)
		{
			LayoutColumn retValue;

			var keyColumns = new DbColumns()
			{
				{ LayoutColumn.ColumnSourceLayoutID, sourceLayoutID },
				{ LayoutColumn.ColumnName, (object)name }
			};
			var layoutColumnManager = Managers.LayoutColumnManager;
			retValue = layoutColumnManager.Retrieve(keyColumns);
			return retValue;
		}

		// Create LayoutColumn from DbColumn.
		private LayoutColumn CreateLayoutColumn(DbColumn dbColumn
			, int sourceLayoutID)
		{
			DataType dataType;
			LayoutColumn retValue;

			var dataTypeManager = Managers.DataTypeManager;
			var keyColumns = dataTypeManager.GetNameKey("String");
			dataType = dataTypeManager.Retrieve(keyColumns);

			retValue = new LayoutColumn()
			{
				SourceLayoutID = sourceLayoutID,
				Name = dbColumn.ColumnName,
				Description = $"Column {dbColumn.ColumnName}",
				DataTypeID = dataType.DataTypeID
			};
			return retValue;
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region Layout

		// Handles the form keys.
		private void LayoutGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mLayoutGridCode.DoRefreshLayout();
					e.Handled = true;
					break;

				case Keys.Enter:
					mLayoutGridCode.DoDefaultLayout();
					e.Handled = true;
					break;

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						LayoutTool.Select();
					}
					else
					{
						LayoutColumnGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void LayoutGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& LayoutGrid.LJCIsDifferentRow(e))
			{
				LayoutGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Layout);
			}
		}

		// Handles the SelectionChanged event.
		private void LayoutGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (LayoutGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Layout);
			}
			LayoutGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void LayoutGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (LayoutGrid.LJCGetMouseRow(e) != null)
			{
				mLayoutGridCode.DoDefaultLayout();
			}
		}
		#endregion

		#region LayoutColumn

		// Handles the form keys.
		private void LayoutColumnGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					mLayoutColumnGridCode.DoRefreshLayoutColumn();
					e.Handled = true;
					break;

				case Keys.Enter:
					mLayoutColumnGridCode.DoEditLayoutColumn();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						LayoutGrid.Select();
					}
					else
					{
						LayoutTool.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void LayoutColumnGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right
				&& LayoutColumnGrid.LJCIsDifferentRow(e))
			{
				LayoutColumnGrid.LJCSetCurrentRow(e);
				TimedChange(Change.LayoutColumn);
			}
		}

		// Handles the SelectionChanged event.
		private void LayoutColumnGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (LayoutColumnGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.LayoutColumn);
			}
			LayoutColumnGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void LayoutColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (LayoutColumnGrid.LJCGetMouseRow(e) != null)
			{
				mLayoutColumnGridCode.DoEditLayoutColumn();
			}
		}
		#endregion
		#endregion
	}
}
