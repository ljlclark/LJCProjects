// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumnDetail.cs
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
	/// <summary>The LayoutColumn detail dialog.</summary>
	public partial class LayoutColumnDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LayoutColumnDetail()
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
		private void LayoutColumnDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			CenterToParent();
			DataRetrieve();
		}

		// Handles the for Shown event.
		private void LayoutColumnDetail_Shown(object sender, EventArgs e)
		{
			LayoutColumnNameTextbox.Select();
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
				var record = mLayoutColumnManager.RetrieveWithID(LJCID);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new LayoutColumn();
				int stringID = 10;
				DataTypeCombo.LJCSetByItemID(stringID);
				ParentNameTextbox.Text = LJCParentName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(LayoutColumn dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.SourceLayoutID;
				ParentNameTextbox.Text = LJCParentName;
				LayoutColumnNameTextbox.Text = dataRecord.Name;
				LayoutColumnDescriptionTextbox.Text = dataRecord.Description;
				DataTypeCombo.LJCSetByItemID(dataRecord.DataTypeID);
				LengthTextbox.Text = dataRecord.Length.ToString();
				SequenceTextbox.Text = dataRecord.Sequence.ToString();
				IdentityCheckbox.Checked = dataRecord.IdentityKey;
				PrimaryKeyCheckbox.Checked = dataRecord.PrimaryKey;
				AllowNullCheckbox.Checked = dataRecord.AllowNull;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private LayoutColumn SetRecordValues()
		{
			int.TryParse(LengthTextbox.Text, out int length);
			short.TryParse(SequenceTextbox.Text, out short sequence);
			LayoutColumn retValue = new LayoutColumn()
			{
				LayoutColumnID = LJCID,
				SourceLayoutID = LJCParentID,
				Name = FormCommon.SetString(LayoutColumnNameTextbox.Text),
				Description = FormCommon.SetString(LayoutColumnDescriptionTextbox.Text),
				DataTypeID = (short)DataTypeCombo.LJCSelectedItemID(),
				Length = length,
				Sequence = sequence,
				IdentityKey = IdentityCheckbox.Checked,
				PrimaryKey = PrimaryKeyCheckbox.Checked,
				AllowNull = AllowNullCheckbox.Checked
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(LayoutColumn record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			LayoutColumn lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ LayoutColumn.ColumnSourceLayoutID, LJCRecord.SourceLayoutID },
				{ LayoutColumn.ColumnName, (object)LJCRecord.Name }
			};
			lookupRecord = mLayoutColumnManager.Retrieve(keyColumns);
			if (mLayoutColumnManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The LayoutColumn record already exists.";
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
						{ LayoutColumn.ColumnSourceLayoutID, LJCRecord.SourceLayoutID },
						{ LayoutColumn.ColumnLayoutColumnID, LJCRecord.LayoutColumnID }
					};
					mLayoutColumnManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					LayoutColumn addedRecord = mLayoutColumnManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.LayoutColumnID = addedRecord.LayoutColumnID;
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

			if (!NetString.HasValue(LayoutColumnNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {LayoutColumnNameLabel.Text}");
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
			mLayoutColumnManager = mManagers.LayoutColumnManager;
			mDataTypeManager = mManagers.DataTypeManager;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			ParentNameLabel.BackColor = BeginColor;
			LayoutColumnNameLabel.BackColor = BeginColor;
			LayoutColumnDescriptionLabel.BackColor = BeginColor;
			DataTypeLabel.BackColor = BeginColor;
			LengthLabel.BackColor = BeginColor;
			SequenceLabel.BackColor = BeginColor;

			LayoutColumnNameTextbox.MaxLength = LayoutColumn.LengthName;
			LayoutColumnDescriptionTextbox.MaxLength = LayoutColumn.LengthDescription;
			LengthTextbox.MaxLength = 5;
			SequenceTextbox.MaxLength = 2;

			// Load control data.
			LJCDataTransformDAL.DataTypes dataTypes = mDataTypeManager.Load();
			foreach (DataType dataType in dataTypes)
			{
				DataTypeCombo.LJCAddItem(dataType.DataTypeID, dataType.Name);
			}

			// Set control layout.
			if (0 == LJCParentID)
			{
				int adjust = ParentNameTextbox.Height;
				ParentNameLabel.Visible = false;
				ParentNameTextbox.Visible = false;
				SequenceLabel.Visible = false;
				SequenceTextbox.Visible = false;
				IdentityCheckbox.Height -= adjust * 2;
				PrimaryKeyCheckbox.Height -= adjust * 2;
				AllowNullCheckbox.Height -= adjust * 2;
				Height -= adjust;
			}
		}
		#endregion

		#region Control Event Handle

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
		private void LayoutColumnNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void LayoutColumnNameTextbox_TextChanged(object sender, EventArgs e)
		{
			LayoutColumnNameTextbox.Text = FormCommon.StripBlanks(LayoutColumnNameTextbox.Text);
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
		public short LJCID { get; set; }

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
		public LayoutColumn LJCRecord { get; private set; }

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
		private LayoutColumnManager mLayoutColumnManager;
		private DataTypeManager mDataTypeManager;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
