// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StepDetail.cs
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
	/// <summary>The Step detail dialog.</summary>
	public partial class StepDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public StepDetail()
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
		private void StepDetail_Load(object sender, EventArgs e)
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
			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var record = mStepManager.RetrieveWithID(LJCID);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new Step();
				ParentNameTextbox.Text = LJCParentName;
				TaskStatusCombo.SelectedIndex = 0;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Step dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.DataProcessID;
				ParentNameTextbox.Text = LJCParentName;
				StepNameTextbox.Text = dataRecord.Name;
				StepDescriptionTextbox.Text = dataRecord.Description;
				mTaskStatusID = dataRecord.StatusID;
				TaskStatusCombo.SelectedItem = (StepTaskStatus)mTaskStatusID;
				SequenceTextbox.Text = dataRecord.Sequence.ToString();
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Step SetRecordValues()
		{
			short.TryParse(SequenceTextbox.Text, out short sequence);
			Step retValue = new Step()
			{
				StepID = LJCID,
				DataProcessID = LJCParentID,
				Name = FormCommon.SetString(StepNameTextbox.Text),
				Description = FormCommon.SetString(StepDescriptionTextbox.Text),
				StatusID = (short)TaskStatusCombo.SelectedItem,
				Sequence = sequence
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Step record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Step lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ Step.ColumnDataProcessID, LJCRecord.DataProcessID },
				{ Step.ColumnName, (object)LJCRecord.Name }
			};
			lookupRecord = mStepManager.Retrieve(keyColumns);
			if (mStepManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
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
					keyColumns = new DbColumns()
					{
						{ Step.ColumnDataProcessID, LJCRecord.DataProcessID },
						{ Step.ColumnStepID, LJCRecord.StepID }
					};
					mStepManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					Step addedRecord = mStepManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.StepID = addedRecord.StepID;
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

			if (!NetString.HasValue(StepNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {StepNameLabel.Text}");
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
		private void InitializeControls()
		{
			// Get singleton values.
			var values = ValuesTransformManager.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			mManagers = new TransformManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			//mDataProcessManager = mManagers.DataProcessManager;
			mStepManager = mManagers.StepManager;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			ParentNameLabel.BackColor = BeginColor;
			StepNameLabel.BackColor = BeginColor;
			StepDescriptionLabel.BackColor = BeginColor;
			SequenceLabel.BackColor = BeginColor;

			StepNameTextbox.MaxLength = Step.LengthName;
			StepDescriptionTextbox.MaxLength = Step.LengthDescription;
			SequenceTextbox.MaxLength = 2;

			// Load control data.
			foreach (StepTaskStatus stepTaskStatus in (StepTaskStatus[])Enum.GetValues
				(typeof(StepTaskStatus)))
			{
				TaskStatusCombo.Items.Add(stepTaskStatus);
			}

			// Set control layout.
			if (0 == LJCParentID)
			{
				ParentNameLabel.Visible = false;
				ParentNameTextbox.Visible = false;
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
		#endregion

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void StepNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void StepNameTextbox_TextChanged(object sender, EventArgs e)
		{
			StepNameTextbox.Text = FormCommon.StripBlanks(StepNameTextbox.Text);
		}

		// Allow 'Copy' operation from control.
		private void StatusCombo_KeyDown(object sender, KeyEventArgs e)
		{
			mAllowStatusCopy = false;
			if (Keys.Control == (ModifierKeys & Keys.Control)
				&& Keys.C == e.KeyCode)
			{
				mAllowStatusCopy = true;
			}
		}
		private bool mAllowStatusCopy;

		// Prevent typing and pasting in the combo textbox.
		private void StatusCombo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!mAllowStatusCopy)
			{
				e.Handled = true;
			}
		}

		// Prevent changing the combobox selection.
		private void StatusCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (mTaskStatusID > 0)
			{
				TaskStatusCombo.SelectedItem = (ProcessStatusType)mTaskStatusID;
			}
		}
		private short mTaskStatusID;

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
		public Step LJCRecord { get; private set; }

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
		//private DataProcessManager mDataProcessManager;
		private StepManager mStepManager;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
