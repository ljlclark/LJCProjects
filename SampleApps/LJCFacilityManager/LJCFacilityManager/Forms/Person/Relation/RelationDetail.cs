// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RelationDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	/// <summary>The PersonRelation detail dialog.</summary>
	public partial class RelationDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public RelationDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "RelationDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// <summary>Configures the form and loads the initial control data.</summary>
		private void RelationDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// <summary>Handles the form keys.</summary>
		private void RelationDetail_KeyDown(object sender, KeyEventArgs e)
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
			PersonRelation record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ PersonRelation.ColumnID, LJCID }
				};
				record = mRelationManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new PersonRelation();
				ParentNameTextbox.Text = DataCommonFacility.GetPersonName(mPersonManager, LJCParentID);
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(PersonRelation dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.PersonID;
				ParentNameTextbox.Text = DataCommonFacility.GetPersonName(mPersonManager, dataRecord.PersonID);
				mRelationID = dataRecord.RelationID;
				RelationTextbox.Text = DataCommonFacility.GetPersonName(mPersonManager, dataRecord.RelationID);
				TypeCombo.LJCSetSelectedItem(dataRecord.RelationCodeTypeID);
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private PersonRelation SetRecordValues()
		{
			Person personRecord;
			PersonRelation retValue = new PersonRelation()
			{
				ID = LJCID,
				PersonID = LJCParentID,
				RelationID = mRelationID,
				RelationCodeTypeID = TypeCombo.LJCGetSelectedItemID(),

				// Get join display values.
				TypeDescription = TypeCombo.Text.Trim()
			};

			// Get additional join display values.
			personRecord = DataCommonFacility.GetPerson(mPersonManager, mRelationID);
			retValue.FirstName = personRecord.FirstName;
			retValue.MiddleInitial = personRecord.MiddleInitial;
			retValue.LastName = personRecord.LastName;
			return retValue;
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			PersonRelation lookupRecord;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ PersonRelation.ColumnPersonID, LJCRecord.PersonID },
				{ PersonRelation.ColumnRelationID, LJCRecord.RelationID },
				{ PersonRelation.ColumnRelationCodeTypeID
					, LJCRecord.RelationCodeTypeID }
			};
			lookupRecord = mRelationManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (LJCIsUpdate == false
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "A duplicate entry already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = new DbColumns()
					{
						{ PersonRelation.ColumnID, LJCRecord.ID }
					};
					mRelationManager.Update(LJCRecord, keyColumns);
				}
				else
				{
					//PersonRelation addedRecord = mRelationManager.Add(LJCRecord);
          mRelationManager.Add(LJCRecord);
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

			if (!NetString.HasValue(ParentNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {ParentNameLabel.Text}");
			}
			if (!NetString.HasValue(RelationTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {RelationLabel.Text}");
			}
			if (TypeCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {TypeLabel.Text}");
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
			mRelationManager = new PersonRelationManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mPersonManager = new PersonManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			RelationLabel.BackColor = mSettings.BeginColor;
			TypeLabel.BackColor = mSettings.BeginColor;

			// Load control data.
			CodeTypeClassManager manager
				= new CodeTypeClassManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			TypeCombo.LJCInit();
			TypeCombo.LJCLoad(manager.GetCodeClassID("Relation"));
			Cursor = Cursors.Default;
		}
		#endregion

		#region Action Event Handlers

		// Show the help page.
		private void DialogMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
				, LJCHelpPageName);
		}
		#endregion

		#region Control Event Handlers

		// <summary>Saves the data and closes the form.</summary>
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (IsValid()
				&& DataSave())
			{
				LJCOnChange();
				DialogResult = DialogResult.OK;
			}
		}

		// <summary>Closes the form without saving the data.</summary>
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		// 
		private void RelationButton_Click(object sender, EventArgs e)
		{
			PersonList personList;
			// #002 Next Statement - Add
			Person personRecord = null;

			// Get current record to seed the selection list.
			// #002 Begin - Add
			if (mRelationID > 0)
			{
				personRecord = mPersonManager.RetrieveWithID(mRelationID);
			}
			// #002 End - Add

			personList = new PersonList()
			{
				LJCIsSelect = true,
				// #002 Next Statement - Add
				LJCSelectedRecord = personRecord
			};
			personList.ShowDialog();
			if (personList.DialogResult == DialogResult.OK
				&& personList.LJCSelectedRecord != null)
			{
				personRecord = personList.LJCSelectedRecord;
				mRelationID = personRecord.ID;
				RelationTextbox.Text = personRecord.FullName;
			}
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
		public PersonRelation LJCRecord { get; private set; }

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
		private PersonRelationManager mRelationManager;
		private PersonManager mPersonManager;

		// Foreign Keys
		private int mRelationID;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
