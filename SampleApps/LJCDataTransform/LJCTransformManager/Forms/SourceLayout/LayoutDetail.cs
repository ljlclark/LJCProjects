// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutDetail.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCDataTransformDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;

namespace LJCTransformManager
{
	/// <summary>The Layout detail dialog.</summary>
	public partial class LayoutDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LayoutDetail()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void LayoutDetail_Load(object sender, EventArgs e)
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
			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var record = mLayoutManager.RetrieveWithID(LJCID);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new SourceLayout();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(SourceLayout dataRecord)
		{
			if (dataRecord != null)
			{
				mPrevLayoutName = dataRecord.Name;
				LayoutNameTextbox.Text = dataRecord.Name;
				LayoutDescriptionTextbox.Text = dataRecord.Description;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private SourceLayout SetRecordValues()
		{
			SourceLayout retValue = new SourceLayout()
			{
				SourceLayoutID = LJCID,
				Name = FormCommon.SetString(LayoutNameTextbox.Text),
				Description = FormCommon.SetString(LayoutDescriptionTextbox.Text),
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(SourceLayout record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			SourceLayout lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = mLayoutManager.GetNameKey(LJCRecord.Name);
			lookupRecord = mLayoutManager.Retrieve(keyColumns);
			if (mLayoutManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The Layout record already exists.";
				Cursor = Cursors.Default;
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = mLayoutManager.GetNameKey(mPrevLayoutName);
					mLayoutManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					SourceLayout addedRecord = mLayoutManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.SourceLayoutID = addedRecord.SourceLayoutID;
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

			if (!NetString.HasValue(LayoutNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {LayoutNameLabel.Text}");
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
			mLayoutManager = mManagers.SourceLayoutManager;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			LayoutNameLabel.BackColor = BeginColor;
			LayoutDescriptionLabel.BackColor = BeginColor;

			LayoutNameTextbox.MaxLength = SourceLayout.LengthName;
			LayoutDescriptionTextbox.MaxLength = SourceLayout.LengthDescription;

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
		private void LayoutNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		private void LayoutNameTextbox_TextChanged(object sender, EventArgs e)
		{
			LayoutNameTextbox.Text = FormCommon.StripBlanks(LayoutNameTextbox.Text);
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
		public int LJCID { get; set; }

		/// <summary>Gets a reference to the record object.</summary>
		public SourceLayout LJCRecord { get; private set; }

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
		private SourceLayoutManager mLayoutManager;

		private string mPrevLayoutName;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
