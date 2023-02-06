// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocGenGroupDetail.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDocLibDAL;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCDocGroupEditor
{
	/// <summary>The DocGenGroup detail dialog.</summary>
	public partial class DocGenGroupDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DocGenGroupDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "DocGenGroupDetail.htm";
			DocGenGroupManager = new DocGenGroupManager();
		}
		#endregion

		#region Form Event Handlers

		// <summary>Configures the form and loads the initial control data.</summary>
		private void DocGenGroupDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// <summary>Handles the form keys.</summary>
		private void DocGenGroupDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic, LJCHelpPageName);
					break;
			}
		}

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, mBeginColor, mEndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			DocGenGroup record;

			Cursor = Cursors.WaitCursor;
			if (NetString.HasValue(LJCName))
			{
				LJCIsUpdate = true;
				NameTextbox.ReadOnly = true;
				record = DocGenGroupManager.SearchName(LJCName);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new DocGenGroup();
				SequenceTextbox.Text = "0";
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private void GetRecordValues(DocGenGroup dataRecord)
		{
			if (dataRecord != null)
			{
				NameTextbox.Text = dataRecord.Name;
				DescriptionTextbox.Text = dataRecord.Description;
				SequenceTextbox.Text = dataRecord.Sequence.ToString();
			}
		}

		// Creates and returns a record object with the data from
		// the controls.
		/// <include path='items/SetRecordValues/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private DocGenGroup SetRecordValues()
		{
			if (false == NetString.HasValue(SequenceTextbox.Text))
			{
				SequenceTextbox.Text = "0";
			}
			DocGenGroup retValue = new DocGenGroup()
			{
				Name = NameTextbox.Text.Trim(),
				Description = DescriptionTextbox.Text.Trim(),
				Sequence = Convert.ToInt32(SequenceTextbox.Text)
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='Doc/DocGenGroupDetail.xml'/>
		private void ResetRecordValues(DocGenGroup record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Validates the data.
		/// <include path='items/IsValid/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private bool IsValid()
		{
			StringBuilder builder;
			string title;
			string message;
			bool retValue = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			// Modify
			if (false == NetString.HasValue(NameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine("  {NameLabel.Text}");
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
		/// <include path='items/InitializeControls/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private void InitializeControls()
		{
			// Initialize Class Data.
			NameLabel.BackColor = mBeginColor;
			DescriptionLabel.BackColor = mBeginColor;

			NameTextbox.MaxLength = DocGenGroup.LengthName;
			DescriptionTextbox.MaxLength = DocGenGroup.LengthDescription;

			// Load control data.

			// Set control layout.
		}
		#endregion

		#region Control Event Handlers

		// <summary>Saves the data and closes the form.</summary>
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (IsValid())
			{
				LJCRecord = SetRecordValues();
				OnChange();
				DialogResult = DialogResult.OK;
			}
		}

		// <summary>Closes the form without saving the data.</summary>
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>Fires the Change event.</summary>
		protected void OnChange()
		{
			Change?.Invoke(this, new EventArgs());
		}
		#endregion

		#region KeyEdit Event Handlers

		// <summary>Only allows numbers or edit keys.</summary>
		private void SequenceTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
		}

		//// <summary>Does not allow spaces.</summary>
		private void NameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		//// <summary>Strips blanks from the text value.</summary>
		private void NameTextbox_TextChanged(object sender, EventArgs e)
		{
			NameTextbox.Text = FormCommon.StripBlanks(NameTextbox.Text);
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
		public string LJCName { get; set; }

		/// <summary>Gets the LJCIsUpdate value.</summary>
		public bool LJCIsUpdate { get; private set; }

		/// <summary>Gets a reference to the record object.</summary>
		public DocGenGroup LJCRecord { get; private set; }

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

		/// <summary>Gets or sets the DocGenGroupManager reference.</summary>
		public DocGenGroupManager DocGenGroupManager { get; private set; }
		#endregion

		#region Class Data

		private Color mBeginColor = Color.AliceBlue;
		private Color mEndColor = Color.LightSkyBlue;

		/// <summary>The Change event.</summary>
		/// <remarks><para>Syntax: public event EventHandler&lt;EventArgs&gt; Change
		/// </para></remarks>
		public event EventHandler<EventArgs> Change;
		#endregion
	}
}
