// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskDetail.cs
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
	/// <summary>The Task detail dialog.</summary>
	public partial class TaskDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TaskDetail()
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
		private void TaskDetail_Load(object sender, EventArgs e)
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
				var record = mTaskManager.RetrieveWithID(LJCID);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new StepTask();
				ParentNameTextbox.Text = LJCParentName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(StepTask dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.StepID;
				ParentNameTextbox.Text = LJCParentName;
				TaskNameTextbox.Text = dataRecord.Name;
				TaskDescriptionTextbox.Text = dataRecord.Description;
				TaskTypeCombo.LJCSetByItemID(dataRecord.TaskTypeID);
				ActionItemTextbox.Text = dataRecord.ActionItemName;
				mTaskStatusID = dataRecord.TaskStatusID;
				TaskStatusCombo.SelectedItem = (StepTaskStatus)mTaskStatusID;
				SequenceTextbox.Text = dataRecord.Sequence.ToString();
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private StepTask SetRecordValues()
		{
			short.TryParse(SequenceTextbox.Text, out short sequence);
			StepTask retValue = new StepTask()
			{
				StepTaskID = LJCID,
				StepID = LJCParentID,
				Name = FormCommon.SetString(TaskNameTextbox.Text),
				Description = FormCommon.SetString(TaskDescriptionTextbox.Text),
				TaskTypeID = (short)TaskTypeCombo.LJCSelectedItemID(),
				ActionItemName = ActionItemTextbox.Text,
				TaskStatusID = (short)TaskStatusCombo.SelectedItem,
				Sequence = sequence
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(StepTask record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			StepTask lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ StepTask.ColumnStepID, LJCRecord.StepID },
				{ StepTask.ColumnName, (object)LJCRecord.Name }
			};
			lookupRecord = mTaskManager.Retrieve(keyColumns);
			if (mTaskManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The Task record already exists.";
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
						{ StepTask.ColumnStepID, LJCRecord.StepID },
						{ StepTask.ColumnStepTaskID, LJCRecord.StepTaskID }
					};
					mTaskManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					StepTask addedRecord = mTaskManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.StepTaskID = addedRecord.StepTaskID;
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

			if (!NetString.HasValue(TaskNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {TaskNameLabel.Text}");
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
			//mStepManager = mManagers.StepManager;
			mTaskManager = mManagers.StepTaskManager;
			mTaskTypeManager = mManagers.TaskTypeManager;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			ParentNameLabel.BackColor = BeginColor;
			TaskNameLabel.BackColor = BeginColor;
			TaskDescriptionLabel.BackColor = BeginColor;
			TaskTypeLabel.BackColor = BeginColor;
			ActionItemLabel.BackColor = BeginColor;
			TaskStatusLabel.BackColor = BeginColor;
			SequenceLabel.BackColor = BeginColor;

			TaskNameTextbox.MaxLength = StepTask.LengthName;
			TaskDescriptionTextbox.MaxLength = StepTask.LengthDescription;
			ActionItemTextbox.MaxLength = StepTask.LengthCodeItemName;
			SequenceTextbox.MaxLength = 2;

			// Load control data.
			TaskTypes taskTypes = mTaskTypeManager.Load();
			foreach (TaskType taskType in taskTypes)
			{
				TaskTypeCombo.LJCAddItem(taskType.TaskTypeID, taskType.Name);
			}
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
			TaskNameTextbox.Text = FormCommon.StripBlanks(TaskNameTextbox.Text);
		}

		// Allow 'Copy' operation from control.
		private void TaskTypeCombo_KeyDown(object sender, KeyEventArgs e)
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
		private void TaskTypeCombo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!mAllowCopy)
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
			ActionItemTextbox.Text = FormCommon.StripBlanks(ActionItemTextbox.Text);
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
		public StepTask LJCRecord { get; private set; }

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
		//private StepManager mStepManager;
		private StepTaskManager mTaskManager;
		private TaskTypeManager mTaskTypeManager;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
