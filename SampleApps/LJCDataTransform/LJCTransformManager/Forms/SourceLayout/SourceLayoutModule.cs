// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceLayoutModule.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The tab composite user control.
	/// <include path='items/Module/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
	public partial class SourceLayoutModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public SourceLayoutModule()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			string errorText = TransformCommon.CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				MessageBox.Show(errorText);
				ClosePage(SourceLayoutPage);
			}

			// Set default class data.
			Cursor = Cursors.Default;
		}
		#endregion

		#region Action Event Handlers

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

		// Performs the Close function.
		private void LayoutColumnMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(SourceLayoutPage);
		}
		#endregion

		#region Control Event Handlers

		#region DataProcess

		// Handles the SelectedIndexChanged event.
		private void DataSourceCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimedChange(Change.DataSource);
		}
		#endregion

		#region Layout

		// Handles the SourceLayout selection button.
		private void SourceLayoutButton_Click(object sender, EventArgs e)
		{
			SourceLayout selectedRecord = null;
			LayoutColumnList list;

			if (mSourceLayoutID > 0)
			{
				// Startup Record
				selectedRecord = new SourceLayout()
				{
					SourceLayoutID = mSourceLayoutID
				};
			}

			list = new LayoutColumnList
			{
				LJCIsSelect = true,
				LJCSelectedRecord = selectedRecord
			};
			if (DialogResult.OK == list.ShowDialog())
			{
				SourceLayout sourceLayout = list.LJCSelectedRecord;
				int dataSourceID = DataSourceCombo.LJCSelectedItemID();

				if (UpdateDataSource(dataSourceID, sourceLayout.SourceLayoutID))
				{
					mSourceLayoutID = sourceLayout.SourceLayoutID;
					SourceLayoutTextbox.Text = sourceLayout.Description;
					TimedChange(Change.Layout);
				}
			}
		}

		// Update the DataSource.
		private bool UpdateDataSource(int dataSourceID, int sourceLayoutID)
		{
			bool retValue = true;

			var dataSourceManager = Managers.DataSourceManager;
			DataSource dataSource = dataSourceManager.RetrieveWithID(dataSourceID);
			if (dataSource != null)
			{
				DataSource dataRecord = new DataSource()
				{
					SourceLayoutID = sourceLayoutID
				};
				List<string> columnNames = new List<string>()
				{
					"SourceLayoutID"
				};

				var keyColumns = dataSourceManager.GetIDKey(dataSourceID);
				dataSourceManager.Update(dataRecord, keyColumns, columnNames);
				if (dataSourceManager.AffectedCount < 1)
				{
					retValue = false;
				}
			}
			return retValue;
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

				// Modify
				case Keys.Tab:
					if (e.Shift)
					{
						SourceLayoutTextbox.Select();
					}
					else
					{
						DataSourceCombo.Select();
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
