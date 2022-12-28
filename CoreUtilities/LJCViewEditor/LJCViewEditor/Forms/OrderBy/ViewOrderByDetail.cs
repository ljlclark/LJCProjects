// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewOrderByDetail.cs
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
	// The ViewOrderBy detail dialog.
	internal partial class ViewOrderByDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewOrderByDetail()
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
		private void ViewOrderByDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"OrderBy\OrderByDetail.html");
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
			Text = "ViewOrderBy Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ ViewOrderBy.ColumnID, LJCID }
				};
				var dataRecord = mViewOrderByManager.Retrieve(keyColumns);
				GetRecordValues(dataRecord);
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
				LJCParentID = dataRecord.ViewDataID;
				ParentTextbox.Text = LJCParentName;
				ColumnNameTextbox.Text = dataRecord.ColumnName;
			}
		}

		// Creates and returns a record object with the data from
		private ViewOrderBy SetRecordValues()
		{
			ViewOrderBy retValue = new ViewOrderBy()
			{
				ID = LJCID,
				ViewDataID = LJCParentID,
				ColumnName = ViewEditorCommon.TruncateAtHyphen(ColumnNameTextbox.Text)
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			ViewOrderBy lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ ViewOrderBy.ColumnViewDataID, LJCRecord.ViewDataID },
				{ ViewOrderBy.ColumnColumnName, (object)LJCRecord.ColumnName }
			};
			lookupRecord = mViewOrderByManager.Retrieve(keyColumns);
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
						{ ViewOrderBy.ColumnID, LJCRecord.ID }
					};

					LJCRecord.ID = 0;
					mViewOrderByManager.Update(LJCRecord, updateKeyColumns);
					LJCRecord.ID = LJCID;
				}
				else
				{
					LJCRecord.ID = 0;
					ViewOrderBy viewOrderBy = mViewOrderByManager.Add(LJCRecord);
					if (viewOrderBy != null)
					{
						LJCRecord.ID = viewOrderBy.ID;
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

			if (false == NetString.HasValue(ColumnNameTextbox.Text))
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
			ValuesViewEditor values = ValuesViewEditor.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			mViewOrderByManager = new ViewOrderByManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			dataHelper = new DataHelper(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Set control values.
			ParentLabel.BackColor = mSettings.BeginColor;
			ColumnNameLabel.BackColor = mSettings.BeginColor;

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
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void OrderByHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"OrderBy\OrderByDetail.html");
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
		internal ViewOrderBy LJCRecord { get; private set; }

		// Gets or sets the BeginColor value.
		private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }
		#endregion

		#region Custom Properties

		// Gets or sets the TableName value.
		internal string LJCTableName { get; set; }
		#endregion

		#region Class Data

		// Singleton values.
		private StandardSettings mSettings;
		private ViewOrderByManager mViewOrderByManager;
		private DbColumns mTableColumns;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
