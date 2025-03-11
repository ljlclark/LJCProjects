// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	// The CodeType detail dialog.
	/// <include path='items/CodeTypeDetail/*' file='Doc/CodeTypeDetail.xml'/>
	public partial class CodeTypeDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CodeTypeDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "CodeTypeDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void CodeTypeDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		//// Handles the form keys.
		private void CodeTypeDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
						, LJCHelpPageName);
					break;
			}
		}

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, mSettings.BeginColor, mSettings.EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			CodeType record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ CodeType.ColumnID, LJCID }
				};
				record = mCodeTypeManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new CodeType();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(CodeType dataRecord)
		{
			if (dataRecord != null)
			{
				CodeTextbox.Text = dataRecord.Code;
				DescriptionTextbox.Text = dataRecord.Description;
				CodeTypeClassCombo.LJCSetSelectedItem(dataRecord.CodeTypeClassID);
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private CodeType SetRecordValues()
		{
			CodeType retValue = new CodeType()
			{
				ID = LJCID,
				Code = FormCommon.SetString(CodeTextbox.Text),
				Description = DescriptionTextbox.Text,
				CodeTypeClassID = CodeTypeClassCombo.LJCGetSelectedItemID(),
				CodeTypeClassDescription = CodeTypeClassCombo.Text
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(CodeType record)
		{
			record.Code = FormCommon.SetString(record.Code);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			CodeType record;
			DbColumns keyColumns;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			if (NetString.HasValue(LJCRecord.Code))
			{
				keyColumns = new DbColumns()
				{
					{ CodeType.ColumnCode, (object)LJCRecord.Code }
				};
				record = mCodeTypeManager.Retrieve(keyColumns);
				if (record != null
					&& (LJCIsUpdate == false
					|| (LJCIsUpdate && record.ID != LJCRecord.ID)))
				{
					retValue = false;
					title = "Data Entry Error";
					message = "A duplicate code already exists.";
					MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			keyColumns = new DbColumns()
			{
				{ CodeType.ColumnDescription, (object)LJCRecord.Description }
			};
			record = mCodeTypeManager.Retrieve(keyColumns);
			if (record != null
				&& (LJCIsUpdate == false
				|| (LJCIsUpdate && record.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "A duplicate description already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = new DbColumns()
					{
						{ CodeType.ColumnID, LJCRecord.ID }
					};
					mCodeTypeManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					CodeType addedRecord = mCodeTypeManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.ID = addedRecord.ID;
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

			if (!NetString.HasValue(DescriptionTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {DescriptionLabel.Text}");
			}
			if (CodeTypeClassCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {CodeTypeClassLabel.Text}");
			}

			if (retValue == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
			Cursor = Cursors.WaitCursor;
			ValuesFacility values = ValuesFacility.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			mCodeTypeManager = new CodeTypeManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			CodeLabel.BackColor = mSettings.BeginColor;
			DescriptionLabel.BackColor = mSettings.BeginColor;
			CodeTypeClassLabel.BackColor = mSettings.BeginColor;

			CodeTextbox.MaxLength = 25;
			DescriptionTextbox.MaxLength = 60;

			// Load control data.
			CodeTypeClassCombo.LJCInit();
			CodeTypeClassCombo.LJCLoad();
			Cursor = Cursors.Default;
		}
		#endregion

		#region Action Event Handlers

		// Shows the help page.
		private void DialogMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
				, LJCHelpPageName);
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

		/// <summary>Gets the LJCIsUpdate value.</summary>
		public bool LJCIsUpdate { get; private set; }

		/// <summary>Gets a reference to the record object.</summary>
		public CodeType LJCRecord { get; private set; }

		/// <summary>Gets or sets the LJCHelpFileName value.</summary>
		public string LJCHelpFileName
		{
			get { return mHelpFileName; }
			set { mHelpFileName = NetString.InitString(value); }
		}
		private string mHelpFileName;

		/// <summary>Gets or sets the LJCHelpPageName value.</summary>
		public string LJCHelpPageName
		{
			get { return mHelpPageName; }
			set { mHelpPageName = NetString.InitString(value); }
		}
		private string mHelpPageName;
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private CodeTypeManager mCodeTypeManager;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
