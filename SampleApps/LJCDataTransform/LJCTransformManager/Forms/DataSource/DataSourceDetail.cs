// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataSourceDetail.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	/// <summary>The DataSource detail dialog.</summary>
	public partial class DataSourceDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public DataSourceDetail()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void DataSourceDetail_Load(object sender, EventArgs e)
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

		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			const int File = 1;
			DataSource record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				record = mDataSourceManager.RetrieveWithID(LJCID);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				ParentNameTextbox.Text = LJCParentName;

				// Set default values.
				LJCRecord = new DataSource();
				SourceTypeCombo.LJCSetByItemID(File);
				SourceStatusCombo.SelectedItem = ProcessStatusType.Active;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(DataSource dataRecord)
		{
			if (dataRecord != null)
			{
				ParentNameTextbox.Text = LJCParentName;
				SourceNameTextbox.Text = dataRecord.Name;
				SourceDescriptionTextbox.Text = dataRecord.Description;
				LayoutCombo.LJCSetByItemID(dataRecord.SourceLayoutID);
				SourceTypeCombo.LJCSetByItemID(dataRecord.SourceTypeID);
				DataConfigNameTextbox.Text = dataRecord.DataConfigName;
				SourceItemNameTextbox.Text = dataRecord.SourceItemName;
				mSourceStatusID = dataRecord.SourceStatusID;
				SourceStatusCombo.SelectedItem = (ProcessStatusType)mSourceStatusID;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private DataSource SetRecordValues()
		{
			DataSource retValue = new DataSource()
			{
				DataSourceID = LJCID,
				Name = FormCommon.SetString(SourceNameTextbox.Text),
				Description = FormCommon.SetString(SourceDescriptionTextbox.Text),
				SourceLayoutID = LayoutCombo.LJCSelectedItemID(),
				SourceTypeID = (short)SourceTypeCombo.LJCSelectedItemID(),
				DataConfigName = DataConfigNameTextbox.Text,
				SourceItemName = SourceItemNameTextbox.Text,
				SourceStatusID = (short)SourceStatusCombo.SelectedItem
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(DataSource record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			DataSource lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = mDataSourceManager.GetNameKey(LJCRecord.Name);
			lookupRecord = mDataSourceManager.Retrieve(keyColumns);
			if (mDataSourceManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The DataSource record already exists.";
				Cursor = Cursors.Default;
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = mDataSourceManager.GetIDKey(LJCRecord.DataSourceID);
					mDataSourceManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					DataSource addedRecord = mDataSourceManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.DataSourceID = addedRecord.DataSourceID;
					}
				}
			}
			Cursor = Cursors.Default;
			return retValue;
		}

		// Validates the data.
		/// <include path='items/IsValid/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool IsValid()
		{
			StringBuilder builder;
			string title;
			string message;
			bool retValue = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (!NetString.HasValue(SourceNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {SourceNameLabel.Text}");
			}

			if (retValue == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}
			return retValue;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void InitializeControls()
		{
			// Get singleton values.
			var values = ValuesTransformManager.Instance;

			mSettings = values.StandardSettings;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			// Initialize Class Data.
			mManagers = new TransformManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mDataSourceManager = mManagers.DataSourceManager;
			mSourceTypeManager = mManagers.SourceTypeManager;
			mSourceLayoutManager = mManagers.SourceLayoutManager;

			// Set control values.
			ParentNameLabel.BackColor = BeginColor;
			SourceNameLabel.BackColor = BeginColor;
			SourceDescriptionLabel.BackColor = BeginColor;
			SourceTypeLabel.BackColor = BeginColor;
			DataConfigNameLabel.BackColor = BeginColor;
			SourceItemNameLabel.BackColor = BeginColor;
			SourceStatusLabel.BackColor = BeginColor;

			SourceNameTextbox.MaxLength = StepTask.LengthName;
			SourceDescriptionTextbox.MaxLength = StepTask.LengthDescription;
			DataConfigNameTextbox.MaxLength = StepTask.LengthCodeItemName;

			// Load control data.
			SourceLayouts sourceLayouts = mSourceLayoutManager.Load();
			foreach (SourceLayout sourceLayout in sourceLayouts)
			{
				LayoutCombo.LJCAddItem(sourceLayout.SourceLayoutID
					, sourceLayout.Description);
			}

			SourceTypes sourceTypes = mSourceTypeManager.Load();
			foreach (SourceType sourceType in sourceTypes)
			{
				SourceTypeCombo.LJCAddItem(sourceType.SourceTypeID, sourceType.Name);
			}

			foreach (ProcessStatusType processStatus in (ProcessStatusType[])Enum.GetValues
				(typeof(ProcessStatusType)))
			{
				SourceStatusCombo.Items.Add(processStatus);
			}

			// Set control layout.
			// Relationship is referenced in another entity.
			if (0 == LJCParentID)
			{
				ParentNameLabel.Visible = false;
				ParentNameTextbox.Visible = false;
				Height -= ParentNameTextbox.Height;
			}
		}
		#endregion

		#region Action Event Handlers

		// Selects the Source item fileSpec.
		private void SourceItemButton_Click(object sender, EventArgs e)
		{
			string fileSpec;

			string initialFolder = Environment.CurrentDirectory;
			string filter = "Text(*.txt)|*.txt|All Files(*.*)|*.*";
			fileSpec = FormCommon.SelectFile(filter, initialFolder, "*.txt");
			if (fileSpec != null)
			{
				SourceItemNameTextbox.Text = NetFile.GetRelativePath(initialFolder
					, fileSpec);
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
		private void TaskNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void TaskNameTextbox_TextChanged(object sender, EventArgs e)
		{
			SourceNameTextbox.Text = FormCommon.StripBlanks(SourceNameTextbox.Text);
		}

		// Allow 'Copy' operation from control.
		private void TaskTypeCombo_KeyDown(object sender, KeyEventArgs e)
		{
			mAllowCopyTaskType = false;
			if (Keys.Control == (ModifierKeys & Keys.Control)
				&& Keys.C == e.KeyCode)
			{
				mAllowCopyTaskType = true;
			}
		}
		private bool mAllowCopyTaskType;

		// Prevent typing and pasting in the combo textbox.
		private void TaskTypeCombo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!mAllowCopyTaskType)
			{
				e.Handled = true;
			}
		}

		// Does not allow spaces.
		private void ActionItemTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void ActionItemTextbox_TextChanged(object sender, EventArgs e)
		{
			DataConfigNameTextbox.Text = FormCommon.StripBlanks(DataConfigNameTextbox.Text);
		}

		// Does not allow spaces.
		private void ItemNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void ItemNameTextbox_TextChanged(object sender, EventArgs e)
		{
			SourceItemNameTextbox.Text = FormCommon.StripBlanks(SourceItemNameTextbox.Text);
		}

		// Allow 'Copy' operation from control.
		private void StatusCombo_KeyDown(object sender, KeyEventArgs e)
		{
			mAllowCopyStatusType = false;
			if (Keys.Control == (ModifierKeys & Keys.Control)
				&& Keys.C == e.KeyCode)
			{
				mAllowCopyStatusType = true;
			}
		}
		private bool mAllowCopyStatusType;

		// Prevent typing and pasting in the combo textbox.
		private void StatusCombo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!mAllowCopyStatusType)
			{
				e.Handled = true;
			}
		}

		// Prevent changing the combobox selection.
		private void StatusCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (mSourceStatusID > 0)
			{
				SourceStatusCombo.SelectedItem = (ProcessStatusType)mSourceStatusID;
			}
		}
		private short mSourceStatusID;
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
		public int LJCID { get; set; }

		/// <summary>Gets or sets the Parent ID value.</summary>
		public int LJCParentID { get; set; }

		/// <summary>Gets or sets the LJCParentName value.</summary>
		public string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		/// <summary>Gets a reference to the record object.</summary>
		public DataSource LJCRecord { get; private set; }

		/// <summary>Gets the LJCIsUpdate value.</summary>
		public bool LJCIsUpdate { get; private set; }

		// Gets or sets the Begin Color.
		private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private TransformManagers mManagers;
		private DataSourceManager mDataSourceManager;
		private SourceTypeManager mSourceTypeManager;
		private SourceLayoutManager mSourceLayoutManager;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
