// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformDetail.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCDataTransformDAL;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCTransformManager
{
	/// <summary>The TaskTransform detail dialog.</summary>
	public partial class TransformDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TransformDetail()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void TransformDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			CenterToParent();
			DataRetrieve();
		}

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, BeginColor, EndColor);
		}

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
		}
		#endregion

		#region Action Event Handlers

		#region Source

		// Displays a view of the Source data.
		private void SourceMenuView_Click(object sender, EventArgs e)
		{
			int dataSourceID = SourceDataCombo.LJCSelectedItemID();
			mDataViewer.Show(dataSourceID);
		}

		// Select the Layout.
		private void SourceMenuLayoutSelect_Click(object sender, EventArgs e)
		{
			int dataSourceID = 0;

			if (SourceDataCombo.SelectedIndex != -1)
			{
				dataSourceID = SourceDataCombo.LJCSelectedItemID();
				SourceLayoutShow(dataSourceID);
			}
		}
		#endregion

		#region Target

		// Displays a view of the Target data.
		private void TargetView_Click(object sender, EventArgs e)
		{
			int dataSourceID = TargetDataCombo.LJCSelectedItemID();
			mDataViewer.Show(dataSourceID);
		}

		// Select the Layout.
		private void TargetMenuLayoutSelect_Click(object sender, EventArgs e)
		{
			int dataSourceID = 0;

			if (TargetDataCombo.SelectedIndex != -1)
			{
				dataSourceID = TargetDataCombo.LJCSelectedItemID();
				SourceLayoutShow(dataSourceID);
			}
		}
		#endregion

		// Show the Layout Selection list.
		// Updates the DataSource layout if a layout is selected.
		private void SourceLayoutShow(int dataSourceID)
		{
			DataSource dataSource;
			SourceLayout sourceLayout = null;
			LayoutColumnList list = null;
			bool isSuccess = true;

			dataSource = mDataSourceManager.RetrieveWithID(dataSourceID);
			if (dataSource != null)
			{
				int id = dataSource.SourceLayoutID;
				sourceLayout = mSourceLayoutManager.RetrieveWithID(id);
				if (sourceLayout != null)
				{
					isSuccess = false;
				}
			}

			if (isSuccess)
			{
				list = new LayoutColumnList()
				{
					LJCIsSelect = true,
					LJCSelectedRecord = sourceLayout
				};
				list.ShowDialog();

				if (list.DialogResult != DialogResult.OK
					|| null == list.LJCSelectedRecord)
				{
					isSuccess = false;
				}
			}

			if (isSuccess)
			{
				// Create data record.
				int sourceLayoutID = list.LJCSelectedRecord.SourceLayoutID;
				dataSource.SourceLayoutID = sourceLayoutID;

				// Define update columns.
				List<string> columnNames = new List<string>()
				{
					DataSource.ColumnSourceLayoutID
				};

				var keyColumns = mDataSourceManager.GetIDKey(dataSource.DataSourceID);
				mDataSourceManager.Update(dataSource, keyColumns, columnNames);
			}
		}
		#endregion

		#region Control Event Handlers

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (IsValid()
				&& DataSave())
			{
				LJCOnChange();
				DialogResult = DialogResult.OK;
			}
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void TransformNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void TransformNameTextbox_TextChanged(object sender, EventArgs e)
		{
			TransformNameTextbox.Text = FormCommon.StripBlanks(TransformNameTextbox.Text);
		}

		// Allow 'Copy' operation from control.
		private void SourceDataCombo_KeyDown(object sender, KeyEventArgs e)
		{
			mAllowCopySource = false;
			if (Keys.Control == (ModifierKeys & Keys.Control)
				&& Keys.C == e.KeyCode)
			{
				mAllowCopySource = true;
			}
		}
		private bool mAllowCopySource;

		// Prevent typing and pasting in the combo textbox.
		private void SourceDataCombo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!mAllowCopySource)
			{
				e.Handled = true;
			}
		}

		// Allow 'Copy' operation from control.
		private void TargetDataCombo_KeyDown(object sender, KeyEventArgs e)
		{
			mAllowCopyTarget = false;
			if (Keys.Control == (ModifierKeys & Keys.Control)
				&& Keys.C == e.KeyCode)
			{
				mAllowCopyTarget = true;
			}
		}
		private bool mAllowCopyTarget;

		// Prevent typing and pasting in the combo textbox.
		private void TargetDataCombo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!mAllowCopyTarget)
			{
				e.Handled = true;
			}
		}
		#endregion
	}
}
