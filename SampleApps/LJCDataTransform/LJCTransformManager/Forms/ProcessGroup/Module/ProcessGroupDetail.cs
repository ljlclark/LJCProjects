// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupDetail.cs
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	/// <summary>The ProcessGroup detail dialog.</summary>
	public partial class ProcessGroupDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProcessGroupDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCRecord = null;
			LJCIsUpdate = false;
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void ProcessGroupDetail_Load(object sender, EventArgs e)
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
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			ProcessGroup record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = mProcessGroupManager.GetIDKey(LJCID);
				record = mProcessGroupManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new ProcessGroup();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(ProcessGroup dataRecord)
		{
			if (dataRecord != null)
			{
				GroupNameTextbox.Text = dataRecord.Name;
				GroupDescriptionTextbox.Text = dataRecord.Description;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private ProcessGroup SetRecordValues()
		{
			ProcessGroup retValue = new ProcessGroup()
			{
				ProcessGroupID = LJCID,
				Name = FormCommon.SetString(GroupNameTextbox.Text),
				Description = FormCommon.SetString(GroupDescriptionTextbox.Text),
			};
			return retValue;
		}

		// Resets the empty record values.
		private void ResetRecordValues(ProcessGroup record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			ProcessGroup lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = mProcessGroupManager.GetNameKey(LJCRecord.Name);
			lookupRecord = mProcessGroupManager.Retrieve(keyColumns);
			if (IsDuplicate(lookupRecord, LJCRecord))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The record already exists.";
				Cursor = Cursors.Default;
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = mProcessGroupManager.GetIDKey(LJCRecord.ProcessGroupID);
					mProcessGroupManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					ProcessGroup addedRecord = mProcessGroupManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.ProcessGroupID = addedRecord.ProcessGroupID;
					}
				}
			}
			Cursor = Cursors.Default;
			return retValue;
		}

		// Check for duplicate unique key.
		private bool IsDuplicate(ProcessGroup lookupRecord, ProcessGroup currentRecord)
		{
			bool retValue = false;

			if (lookupRecord != null)
			{
				if (!LJCIsUpdate)
				{
					// Duplicate for "New" record that already exists.
					retValue = true;
				}
				else
				{
					if (lookupRecord.ProcessGroupID != currentRecord.ProcessGroupID)
					{
						// Duplicate for "Update" where unique key is modified.
						retValue = true;
					}
				}
			}
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

			if (!NetString.HasValue(GroupNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine("  {GroupNameLabel.Text}");
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

			// Initialize Class Data.
			mManagers = new TransformManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mProcessGroupManager = mManagers.ProcessGroupManager;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			GroupNameLabel.BackColor = BeginColor;
			GroupDescriptionLabel.BackColor = BeginColor;

			GroupNameTextbox.MaxLength = ProcessGroup.LengthName;
			GroupDescriptionTextbox.MaxLength = ProcessGroup.LengthDescription;

			// Load control data.

			// Set control layout.
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

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
		}
		#endregion

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void GroupNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void GroupNameTextbox_TextChanged(object sender, EventArgs e)
		{
			GroupNameTextbox.Text = FormCommon.StripBlanks(GroupNameTextbox.Text);
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
		public int LJCID { get; set; }

		/// <summary>Gets a reference to the record object.</summary>
		public ProcessGroup LJCRecord { get; private set; }

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
		private ProcessGroupManager mProcessGroupManager;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
