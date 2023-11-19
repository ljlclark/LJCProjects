// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinOnDetail.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCViewEditorDAL;

namespace LJCViewEditor
{
	// The ViewJoinOn detail dialog.
	internal partial class ViewJoinOnDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewJoinOnDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCRecord = null;
			LJCIsUpdate = false;
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Handles the control keys.
		private void ViewJoinOnDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Join\JoinOnDetail.html");
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewJoinOnDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			DataRetrieve();
			//CenterToParent();
			Location = LJCLocation;
		}

		// Paint the form background.
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, BeginColor, EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		private void DataRetrieve()
		{
			Cursor = Cursors.WaitCursor;
			Text = "ViewJoinOn Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ ViewJoinOn.ColumnID, LJCID }
				};
				var dataRecord = mViewJoinOnManager.Retrieve(keyColumns);
				GetRecordValues(dataRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewJoinOn();
				ParentTextbox.Text = LJCParentName;
				OperatorTextbox.Text = "=";
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewJoinOn dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.ViewJoinID;
				ParentTextbox.Text = LJCParentName;

				FromColumnCombo.Text = dataRecord.FromColumnName;
				var dbColumn
					= mJoinTableColumns.LJCSearchPropertyName(dataRecord.FromColumnName);
				if (dbColumn != null)
				{
					FromColumnCombo.SelectedItem = dbColumn;
				}

				ToColumnCombo.Text = dataRecord.ToColumnName;
				dbColumn
					= mJoinOnTableColumns.LJCSearchPropertyName(dataRecord.ToColumnName);
				if (dbColumn != null)
				{
					ToColumnCombo.SelectedItem = dbColumn;
				}

				OperatorTextbox.Text = dataRecord.JoinOnOperator;
			}
		}

		// Creates and returns a record object with the data from
		private ViewJoinOn SetRecordValues()
		{
			ViewJoinOn retValue = new ViewJoinOn()
			{
				ID = LJCID,
				ViewJoinID = LJCParentID,
				FromColumnName = ViewEditorCommon.TruncateAtHyphen(FromColumnCombo.Text),
				ToColumnName = ViewEditorCommon.TruncateAtHyphen(ToColumnCombo.Text),
				JoinOnOperator = OperatorTextbox.Text
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			ViewJoinOn lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ ViewJoinOn.ColumnViewJoinID, LJCRecord.ViewJoinID },
				{ ViewJoinOn.ColumnFromColumnName, (object)LJCRecord.FromColumnName }
			};
			lookupRecord = mViewJoinOnManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (false == LJCIsUpdate
				|| (true == LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The record already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					var updateKeyColumns = new DbColumns()
					{
						{ ViewJoinOn.ColumnID, LJCRecord.ID }
					};

					LJCRecord.ID = 0;
					mViewJoinOnManager.Update(LJCRecord, updateKeyColumns);
					LJCRecord.ID = LJCID;
				}
				else
				{
					LJCRecord.ID = 0;
					ViewJoinOn viewJoinOn = mViewJoinOnManager.Add(LJCRecord);
					if (viewJoinOn != null)
					{
						LJCRecord.ID = viewJoinOn.ID;
					}
				}
			}
			Cursor = Cursors.Default;
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

			if (false == NetString.HasValue(FromColumnCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {FromColumnLabel.Text}");
			}
			if (false == NetString.HasValue(ToColumnCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {ToColumnLabel.Text}");
			}
			if (false == NetString.HasValue(OperatorTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {OperatorLabel.Text}");
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
			ValuesViewEditor values = ValuesViewEditor.Instance;

			mSettings = values.StandardSettings;

      // Initialize Class Data.
      Managers = new ManagersDbView();
      Managers.SetDbProperties(mSettings.DbServiceRef
        , mSettings.DataConfigName);
      //mDataDbView = new DataDbView(Managers);
			mViewJoinOnManager = Managers.ViewJoinOnManager;
			//mViewJoinManager = mViewHelper.ViewJoinManager;
			//mViewDataManager = mViewHelper.ViewDataManager;
			//mViewTableManager = mViewHelper.ViewTableManager;

			// Set control values.
			ParentLabel.BackColor = mSettings.BeginColor;
			FromColumnLabel.BackColor = mSettings.BeginColor;
			ToColumnLabel.BackColor = mSettings.BeginColor;
			OperatorLabel.BackColor = mSettings.BeginColor;

			FromColumnCombo.MaxLength = ViewJoinOn.LengthFromColumnName;
			ToColumnCombo.MaxLength = ViewJoinOn.LengthToColumnName;
			OperatorTextbox.MaxLength = ViewJoinOn.LengthJoinOperator;

			// Load control data.
			// From Columns Combo
			DataHelper dataHelper = new DataHelper(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mJoinTableColumns = dataHelper.GetJoinFromColumns(LJCParentID);
			foreach (DbColumn dbColumn in mJoinTableColumns)
			{
				FromColumnCombo.Items.Add(dbColumn);
			}

			// To Columns Combo
			mJoinOnTableColumns = dataHelper.GetJoinToColumns(LJCParentID);
			foreach (DbColumn dbColumn in mJoinOnTableColumns)
			{
				ToColumnCombo.Items.Add(dbColumn);
			}
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void JoinOnHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Join\JoinOnDetail.html");
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
		private void FromColumnTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void ToColumnTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void OperatorTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void FromColumnTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = FromColumnCombo.SelectionStart;
      FromColumnCombo.Text = FormCommon.StripBlanks(FromColumnCombo.Text);
      FromColumnCombo.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void ToColumnTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = ToColumnCombo.SelectionStart;
      ToColumnCombo.Text = FormCommon.StripBlanks(ToColumnCombo.Text);
      ToColumnCombo.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void OperatorTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = OperatorTextbox.SelectionStart;
      OperatorTextbox.Text = FormCommon.StripBlanks(OperatorTextbox.Text);
      OperatorTextbox.SelectionStart = prevStart;
    }
    #endregion

    #region Properties

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
		internal ViewJoinOn LJCRecord { get; private set; }

    internal ManagersDbView Managers { get; set; }

    // Gets or sets the BeginColor value.
    private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private DbColumns mJoinOnTableColumns;
		private DbColumns mJoinTableColumns;
		private StandardUISettings mSettings;
		//private ViewDataManager mViewDataManager;
		//private DataDbView mDataDbView;
		//private ViewJoinManager mViewJoinManager;
		private ViewJoinOnManager mViewJoinOnManager;
		//private ViewTableManager mViewTableManager;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
