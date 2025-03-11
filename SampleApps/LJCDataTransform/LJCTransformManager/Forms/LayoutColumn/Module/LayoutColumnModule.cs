// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumnModule.cs
using System;
using System.Windows.Forms;

namespace LJCTransformManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The tab composite user control.
	/// <include path='items/Module/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
	public partial class LayoutColumnModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LayoutColumnModule()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();

			// Set default class data.
			Cursor = Cursors.Default;
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

		// Performs the Close function.
		private void LayoutMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(LayoutPage);
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

		// Performs the Close function.
		private void LayoutColumnMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(LayoutPage);
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
					mLayoutGridCode.DoEditLayout();
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
				mLayoutGridCode.DoEditLayout();
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
