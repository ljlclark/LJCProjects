// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocGenAssemblyDetail.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDocLibDAL;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCDocGroupEditor
{
	// The DocGenAssembly detail dialog.
	/// <include path='items/DocGenAssemblyDetail/*' file='Doc/ProjectDocGroupEditor.xml'/>
	public partial class DocGenAssemblyDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DocGenAssemblyDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCName = null;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "_AppName_.chm";
			LJCHelpPageName = "_ClassName_Detail.htm";
			DocGenGroupManager = new DocGenGroupManager();
		}
		#endregion

		#region Form Event Handlers

		// <summary>Configures the form and loads the initial control data.</summary>
		private void DocGenAssemblyDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// <summary>Handles the form keys.</summary>
		private void DocGenAssemblyDetail_KeyDown(object sender, KeyEventArgs e)
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
			DocGenAssembly record = null;

			Cursor = Cursors.WaitCursor;
			if (NetString.HasValue(LJCName))
			{
				LJCIsUpdate = true;
				//NameTextbox.ReadOnly = true;
				record = DocGenGroupManager.SearchNameAssembly(LJCParentName
					, LJCName);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new DocGenAssembly();
				ParentNameTextbox.Text = LJCParentName;
				SequenceTextbox.Text = "0";
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private void GetRecordValues(DocGenAssembly dataRecord)
		{
			if (dataRecord != null)
			{
				//LJCParentID = record.Name;
				ParentNameTextbox.Text = LJCParentName;
				NameTextbox.Text = dataRecord.Name;
				DescriptionTextbox.Text = dataRecord.Description;
				XmlFileTextbox.Text = dataRecord.FileSpec;
				ImageFileTextbox.Text = dataRecord.MainImage;
				SequenceTextbox.Text = dataRecord.Sequence.ToString();

				// Preserve for update.
				PreviousName = dataRecord.Name;
			}
		}

		// Creates and returns a record object with the data from
		// the controls.
		/// <include path='items/SetRecordValues/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private DocGenAssembly SetRecordValues()
		{
			if (false == NetString.HasValue(SequenceTextbox.Text))
			{
				SequenceTextbox.Text = "0";
			}
			DocGenAssembly retValue = new DocGenAssembly()
			{
				//ParentID = LJCParentID;
				Name = NameTextbox.Text.Trim(),
				Description = DescriptionTextbox.Text.Trim(),
				FileSpec = XmlFileTextbox.Text.Trim(),
				MainImage = ImageFileTextbox.Text.Trim(),
				Sequence = Convert.ToInt32(SequenceTextbox.Text)
			};

			// Get additional join display values.
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='Doc/DocGenAssemblyDetail.xml'/>
		private void ResetRecordValues(DocGenAssembly record)
		{
			record.Description = FormCommon.SetString(record.Description);
			record.MainImage = FormCommon.SetString(record.MainImage);
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

			if (false == NetString.HasValue(NameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {NameLabel.Text}");
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

			NameTextbox.MaxLength = DocGenAssembly.LengthName;
			DescriptionTextbox.MaxLength = DocGenAssembly.LengthDescription;
			XmlFileTextbox.MaxLength = DocGenAssembly.LengthFileSpec;
			ImageFileTextbox.MaxLength = DocGenAssembly.LengthMainImage;

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

		// <summary>Select the XML file.</summary>
		private void XmlFileButton_Click(object sender, EventArgs e)
		{
			string initialFolder = Environment.CurrentDirectory;
			string filter = "XML(*.xml)|*.xml|All Files(*.*)|*.*";
			string fileSpec = FormCommon.SelectFile(filter, initialFolder, "*.xml");
			if (fileSpec != null)
			{
				XmlFileTextbox.Text = NetFile.GetRelativePath(initialFolder, fileSpec);
			}
		}

		// <summary>Select the Image file.</summary>
		private void ImageFileButton_Click(object sender, EventArgs e)
		{
			string initialFolder = Environment.CurrentDirectory;
			string filter = "*.jpg|*.jpg|*.png|*.png|*.bmp|*.bmp|All Files(*.*)|*.*";
			string fileSpec = FormCommon.SelectFile(filter, initialFolder, "*.jpg");
			if (fileSpec != null)
			{
				ImageFileTextbox.Text = NetFile.GetRelativePath(initialFolder, fileSpec);
			}
		}

		/// <summary>Fires the Change event.</summary>
		/// <remarks><para>Syntax: protected void OnChange()</para></remarks>
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

		//// <summary>Only allows numbers or edit keys.</summary>
		//private void Textbox_KeyPress(object sender, KeyPressEventArgs e)
		//{
		//	e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
		//}

		// <summary>Does not allow spaces.</summary>
		private void NameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// <summary>Strips blanks from the text value.</summary>
		private void NameTextbox_TextChanged(object sender, EventArgs e)
		{
			NameTextbox.Text = FormCommon.StripBlanks(NameTextbox.Text);
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
		public string LJCName { get; set; }

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
		public DocGenAssembly LJCRecord { get; private set; }

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

		/// <summary>Gets or sets the Previous name.</summary>
		public string PreviousName { get; set; }
		#endregion

		#region Class Data

		private readonly Color mBeginColor = Color.AliceBlue;
		private readonly Color mEndColor = Color.LightSkyBlue;

		/// <summary>The Change event.</summary>
		/// <remarks><para>Syntax: public event EventHandler&lt;EventArgs&gt; Change
		/// </para></remarks>
		public event EventHandler<EventArgs> Change;
		#endregion
	}
}
