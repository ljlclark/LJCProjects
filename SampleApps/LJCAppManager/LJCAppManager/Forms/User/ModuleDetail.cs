// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleDetail.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCAppManagerDAL;
using LJCDBClientLib;

namespace LJCAppManager
{
	// The Module detail dialog.
	/// <include path='items/ModuleDetail/*' file='Doc/ModuleDetail.xml'/>
	public partial class ModuleDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ModuleDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "AppManager.chm";
			LJCHelpPageName = "ModuleDetail.htm";
			LJCUserID = 0;  // Allows for a related direct update.
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void ModuleDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void ModuleDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic, LJCHelpPageName);
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
		private void DataRetrieve()
		{
			AppModule record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = Managers.AppModuleManager.GetIDKey(LJCID);
				record = Managers.AppModuleManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new AppModule();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(AppModule dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.AppProgramID;

				// Get the parent text.
				//AppProgramManager2 programManager = new AppProgramManager2(mDbServiceRef
				//	, mDataConfigName);
				var programManager = Managers.AppProgramManager;
				var keyColumns = programManager.GetIDKey(LJCParentID);
				AppProgram appProgram = programManager.Retrieve(keyColumns);
				if (appProgram != null)
				{
					ParentNameTextbox.Text = appProgram.Title;
				}

				//mTypeNameKey = dataRecord.TypeName;
				ModuleNameTextbox.Text = dataRecord.TypeName;
				TitleTextbox.Text = dataRecord.Title;
			}
		}

		// Creates and returns a record object with the data from
		private AppModule SetRecordValues()
		{
			AppModule retVal = new AppModule()
			{
				ID = LJCID,
				AppProgramID = LJCParentID,
				TypeName = ModuleNameTextbox.Text.Trim(),
				Title = TitleTextbox.Text.Trim()
			};
			return retVal;
		}

		// Saves the data.
		private bool DataSave()
		{
			AppModule lookupRecord;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ AppModule.ColumnAppProgramID, LJCRecord.AppProgramID },
				{ AppModule.ColumnTypeName, (object)LJCRecord.TypeName }
			};
			var moduleManager = Managers.AppModuleManager;
			lookupRecord = moduleManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (!LJCIsUpdate
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "A duplicate module type name already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			var dbColumn = keyColumns.LJCSearchColumnName("TypeName");
			keyColumns.Remove(dbColumn);
			keyColumns.Add("Title", (object)LJCRecord.Title);
			lookupRecord = moduleManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (LJCIsUpdate == false
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "A duplicate Title already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = moduleManager.GetIDKey(LJCRecord.ID);
					List<string> updateColumns = new List<string>()
					{
						AppModule.ColumnAppProgramID,
						AppModule.ColumnTypeName,
						AppModule.ColumnTitle
					};
					moduleManager.Update(LJCRecord, keyColumns, updateColumns);
				}
				else
				{
					AppModule addedRecord = moduleManager.Add(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.ID = addedRecord.ID;
					}
				}
			}
			return retValue;
		}

		// Validates the data.
		private bool IsValid()
		{
			StringBuilder builder;
			string title;
			string message;
			bool retVal = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (LJCParentID == 0)
			{
				retVal = false;
				builder.AppendLine($"  {ParentNameLabel.Text}");
			}
			if (!NetString.HasValue(ModuleNameTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {ModuleNameLabel.Text}");
			}
			if (!NetString.HasValue(TitleTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {TitleLabel.Text}");
			}

			if (retVal == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return retVal;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesAppManager.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			Managers = new AppManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			ModuleNameLabel.BackColor = mSettings.BeginColor;
			TitleLabel.BackColor = mSettings.BeginColor;

			ModuleNameTextbox.MaxLength = AppModule.LengthTypeName;
			TitleTextbox.MaxLength = AppModule.LengthTitle;
			Cursor = Cursors.Default;
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

		// Gets or sets the primary ID value.
		internal int LJCID { get; set; }

		// Gets or sets the Parent ID value.
		internal int LJCParentID { get; set; }

		// Gets or sets the LJCParentName value.
		internal string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		// Gets the LJCIsUpdate value.
		internal bool LJCIsUpdate { get; private set; }

		// Gets a reference to the record object.
		internal AppModule LJCRecord { get; private set; }

		// Gets or sets the LJCHelpFileName value.
		internal string LJCHelpFileName
		{
			get { return mHelpFileName; }
			set { mHelpFileName = NetString.InitString(value); }
		}
		private string mHelpFileName;

		// Gets or sets the LJCHelpPageName value.
		internal string LJCHelpPageName
		{
			get { return mHelpPageName; }
			set { mHelpPageName = NetString.InitString(value); }
		}
		private string mHelpPageName;

		// Gets or sets the user parent ID value.
		internal int LJCUserID { get; set; }

		// Gets or sets the Managers value.
		private AppManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		//private string mTypeNameKey;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
