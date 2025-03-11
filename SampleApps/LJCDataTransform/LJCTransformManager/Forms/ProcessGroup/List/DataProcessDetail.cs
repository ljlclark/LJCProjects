// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataProcessDetail.cs
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
	/// <summary>The DataProcess detail dialog.</summary>
	public partial class DataProcessDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public DataProcessDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCRecord = null;
			LJCIsUpdate = false;
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void DataProcessDetail_Load(object sender, EventArgs e)
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
			DataProcess record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				if (LJCParentID > 0)
				{
					record = mDataProcessManager.RetrieveWithGroupID(LJCParentID, LJCID);
				}
				else
				{
					record = mDataProcessManager.RetrieveWithID(LJCID);
				}
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				ParentNameTextbox.Text = LJCParentName;

				// Set default values.
				LJCRecord = new DataProcess();
				StatusCombo.SelectedItem = ProcessStatusType.Active;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(DataProcess dataRecord)
		{
			if (dataRecord != null)
			{
				ParentNameTextbox.Text = LJCParentName;
				ProcessNameTextbox.Text = dataRecord.Name;
				ProcessDescriptionTextbox.Text = dataRecord.Description;
				mProcessStatusID = dataRecord.ProcessStatusID;
				StatusCombo.SelectedItem = (ProcessStatusType)mProcessStatusID;
				SequenceTextbox.Text = dataRecord.Sequence.ToString();
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private DataProcess SetRecordValues()
		{
			short.TryParse(SequenceTextbox.Text, out short sequence);
			DataProcess retValue = new DataProcess()
			{
				DataProcessID = LJCID,
				Name = FormCommon.SetString(ProcessNameTextbox.Text),
				Description = FormCommon.SetString(ProcessDescriptionTextbox.Text),
				ProcessStatusID = (short)StatusCombo.SelectedItem,
				Sequence = sequence
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(DataProcess record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			DataProcess lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = mDataProcessManager.GetNameKey(LJCRecord.Name);
			lookupRecord = mDataProcessManager.Retrieve(keyColumns);
			if (mDataProcessManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The DataProcess record already exists.";
				Cursor = Cursors.Default;
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = mDataProcessManager.GetIDKey(LJCRecord.DataProcessID);
					mDataProcessManager.Update(LJCRecord, keyColumns);
					if (mDataProcessManager.AffectedCount > 0
						&& LJCParentID > 0)
					{
						retValue = ParentRecordSave();
					}
					ResetRecordValues(LJCRecord);
				}
				else
				{
					DataProcess addedRecord = mDataProcessManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (mDataProcessManager.AffectedCount > 0)
					{
						LJCRecord.DataProcessID = addedRecord.DataProcessID;
					}
				}
			}
			Cursor = Cursors.Default;
			return retValue;
		}

		// Saves the parent associated data.
		private bool ParentRecordSave()
		{
			bool retValue = true;

			// Update ProcessGroupProcess record.
			if (retValue)
			{
				ProcessGroupProcess dataRecord = new ProcessGroupProcess()
				{
					ProcessGroupID = LJCParentID,
					DataProcessID = LJCRecord.DataProcessID,
					Sequence = LJCRecord.Sequence
				};
				var keyColumns = mProcessGroupProcessManager.GetIDKeys(LJCParentID
					, LJCRecord.DataProcessID);
				mProcessGroupProcessManager.Update(dataRecord, keyColumns);
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

			if (!NetString.HasValue(ProcessNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {ProcessNameLabel.Text}");
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
			mDataProcessManager = mManagers.DataProcessManager;
			mProcessGroupProcessManager = mManagers.ProcessGroupProcessManager;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			ParentNameLabel.BackColor = BeginColor;
			ProcessNameLabel.BackColor = BeginColor;
			ProcessDescriptionLabel.BackColor = BeginColor;
			StatusLabel.BackColor = BeginColor;
			SequenceLabel.BackColor = BeginColor;

			ProcessNameTextbox.MaxLength = DataProcess.LengthName;
			ProcessDescriptionTextbox.MaxLength = DataProcess.LengthDescription;
			SequenceTextbox.MaxLength = 2;

			// Load control data.
			foreach (ProcessStatusType processStatusType in (ProcessStatusType[])Enum.GetValues
				(typeof(ProcessStatusType)))
			{
				StatusCombo.Items.Add(processStatusType);
			}

			// Set control layout.
			if (0 == LJCParentID)
			{
				ParentNameLabel.Visible = false;
				ParentNameTextbox.Visible = false;
				SequenceLabel.Visible = false;
				SequenceTextbox.Visible = false;
				Height -= ParentNameTextbox.Height;
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

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
		}
		#endregion

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void ProcessNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void ProcessNameTextbox_TextChanged(object sender, EventArgs e)
		{
			ProcessNameTextbox.Text = FormCommon.StripBlanks(ProcessNameTextbox.Text);
		}

		// Allow 'Copy' operation from control.
		private void StatusCombo_KeyDown(object sender, KeyEventArgs e)
		{
			mAllowCopy = false;
			if (Keys.Control == (ModifierKeys & Keys.Control)
				&& Keys.C == e.KeyCode)
			{
				mAllowCopy = true;
			}
		}
		private bool mAllowCopy;

		// Prevent typing and pasting in the combo textbox.
		private void StatusCombo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!mAllowCopy)
			{
				e.Handled = true;
			}
		}

		// Prevent changing the combobox selection.
		private void StatusCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (mProcessStatusID > 0)
			{
				StatusCombo.SelectedItem = (ProcessStatusType)mProcessStatusID;
			}
		}
		private short mProcessStatusID;

		// Only allows numbers or edit keys.
		private void SequenceTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
      if (sender is TextBox textBox)
      {
        e.Handled = FormCommon.HandleNumber(textBox.Text, e.KeyChar);
      }
    }
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
		public DataProcess LJCRecord { get; private set; }

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
		private DataProcessManager mDataProcessManager;
		private ProcessGroupProcessManager mProcessGroupProcessManager;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
