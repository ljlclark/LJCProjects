// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewOrderByDetail.cs
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCViewEditor
{
	// The ViewOrderBy detail dialog.
	internal partial class ViewOrderByDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewOrderByDetail()
		{
			InitializeComponent();

      // Initialize property values.
      LJCHelpFileName = "ViewEditor.chm";
      LJCHelpPageName = @"OrderBy\OrderByDetail.html";
      LJCID = 0;
      LJCIsUpdate = false;
      LJCRecord = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Handles the control keys.
		private void ViewOrderByDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
          Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
            , LJCHelpPageName);
          break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewOrderByDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			DataRetrieve();
			Location = LJCLocation;
		}

		// Paint the form background.
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			//FormCommon.CreateGradient(e.Graphics, ClientRectangle
			//	, BeginColor, EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		private void DataRetrieve()
		{
			Cursor = Cursors.WaitCursor;
			Text = "ViewOrderBy Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
        var manager = Managers.ViewOrderByManager;
				mOriginalRecord = manager.RetrieveWithID(LJCID);
				GetRecordValues(mOriginalRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				ParentTextbox.Text = LJCParentName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewOrderBy dataRecord)
		{
			if (dataRecord != null)
			{
        // In control order.
        ParentTextbox.Text = LJCParentName;
				ColumnNameTextbox.Text = dataRecord.ColumnName;

        // Reference key values.
        LJCParentID = dataRecord.ViewDataID;
      }
    }

		// Creates and returns a record object with the data from
		private ViewOrderBy SetRecordValues()
		{
      ViewOrderBy retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new ViewOrderBy();
      }

      // In control order.
      retValue.ColumnName = ViewEditorCommon.TruncateAtHyphen(ColumnNameTextbox.Text);

      // Get Reference key values.
      retValue.ID = LJCID;
      retValue.ViewDataID = LJCParentID;
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

      var manager = Managers.ViewOrderByManager;
			//lookupRecord = mViewOrderByManager.RetrieveWithUniqueKey(LJCRecord.ViewDataID
   //     , LJCRecord.ColumnName);
      var lookupRecord = manager.RetrieveWithUniqueKey(LJCRecord.ColumnName);
      if (lookupRecord != null
				&& (!LJCIsUpdate
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
        FormCommon.DataError(this);
      }

      if (retValue)
			{
				if (LJCIsUpdate)
				{
					var updateKeyColumns = new DbColumns()
					{
						{ ViewOrderBy.ColumnID, LJCRecord.ID }
					};

					LJCRecord.ID = 0;
					manager.Update(LJCRecord, updateKeyColumns);
					LJCRecord.ID = LJCID;
          retValue = !FormCommon.UpdateError(this, manager.AffectedCount);
        }
        else
				{
					LJCRecord.ID = 0;
					var addedRecord = manager.Add(LJCRecord);
          if (addedRecord != null)
          {
            LJCRecord.ID = addedRecord.ID;
          }
          retValue = !FormCommon.AddError(this, manager.AffectedCount);
        }
      }
			Cursor = Cursors.Default;
			return retValue;
		}

    // Check for saved data.
    private bool IsDataSaved()
    {
      bool retValue = false;

      FormCancelButton.Select();
      if (IsValid() && DataSave())
      {
        retValue = true;
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

			if (!NetString.HasValue(ColumnNameTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {ColumnNameLabel.Text}");
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
			DataHelper dataHelper;

      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      ValuesViewEditor values = ValuesViewEditor.Instance;
      Managers = values.Managers;
			mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Initialize Class Data.
      //mViewOrderByManager = new ViewOrderByManager(mSettings.DbServiceRef
      //	, mSettings.DataConfigName);
      dataHelper = new DataHelper(mSettings.DbServiceRef
				, mSettings.DataConfigName);

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      ColumnNameTextbox.MaxLength = ViewOrderBy.LengthColumnName;

			// Load control data.
			if (dataHelper != null)
			{
				mTableColumns = dataHelper.GetTableColumns(LJCTableName);
				foreach (DbColumn dbColumn in mTableColumns)
				{
					ColumnNameTextbox.Items.Add(dbColumn);
				}
			}
      Cursor = Cursors.Default;
    }
    #endregion

    #region Action Event Handlers

    // Shows the Help page.
    private void OrderByHelp_Click(object sender, EventArgs e)
		{
      Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
        , LJCHelpPageName);
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
		}

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (IsDataSaved())
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

    #region Properties

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

    // Gets or sets the ID value.
    internal int LJCID { get; set; }

		// Gets the IsUpdate value.
		internal bool LJCIsUpdate { get; private set; }

		// The form position.
		internal Point LJCLocation { get; set; }

		// Gets or sets the Parent ID value.
		internal int LJCParentID { get; set; }

		// Gets or sets the Parent name value.
		internal string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		// Gets a reference to the record object.
		internal ViewOrderBy LJCRecord { get; private set; }

		// Gets or sets the BeginColor value.
		private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }

    // The Managers object.
    private ManagersDbView Managers { get; set; }
    #endregion

    #region Custom Properties

    // Gets or sets the TableName value.
    internal string LJCTableName { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    // Singleton values.
    private ViewOrderBy mOriginalRecord;
    private StandardUISettings mSettings;
		private DbColumns mTableColumns;
		#endregion
	}
}
