// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddressDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCRegionDAL;
using LJCRegionManager;
using LJCRegionForm;
using LJCWinFormCommon;

namespace LJCFacilityManager
{
	/// <summary>The Address detail dialog.</summary>
	public partial class AddressDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AddressDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "AddressDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void AddressDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void AddressDetail_KeyDown(object sender, KeyEventArgs e)
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
			Address record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				record = mAddressManager.RetrieveJoinWithID(LJCID);
				if (record != null)
				{
					GetRecordValues(record);
				}
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new Address();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Address dataRecord)
		{
			if (dataRecord != null)
			{
				mRegionID = dataRecord.RegionID;
				mProvinceCode = dataRecord.ProvinceName;
				mProvinceID = dataRecord.ProvinceID;
				mCityID = dataRecord.CityID;
				mCitySectionID = dataRecord.CitySectionID;
				ProvinceTextbox.Text = dataRecord.ProvinceName;
				CityTextbox.Text = dataRecord.CityName;
				CitySectionTextbox.Text = dataRecord.CitySectionName;
				StreetTextbox.Text = dataRecord.Street;
				PostalCodeTextbox.Text = dataRecord.PostalCode;
				TypeCombo.LJCSetSelectedItem(dataRecord.CodeTypeID);
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Address SetRecordValues()
		{
			Address retValue = new Address()
			{
				ID = LJCID,
				RegionID = mRegionID,
				ProvinceID = mProvinceID,
				CityID = mCityID,
				CitySectionID = mCitySectionID,
				Street = StreetTextbox.Text,
				PostalCode = FormCommon.SetString(PostalCodeTextbox.Text),
				CodeTypeID = TypeCombo.LJCGetSelectedItemID(),

				// Get join display values
				TypeDescription = TypeCombo.Text
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Address record)
		{
			record.PostalCode = FormCommon.SetString(record.PostalCode);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Address lookupRecord;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ Address.ColumnProvinceID, mProvinceID },
				{ Address.ColumnCityID, mCityID },
				{ Address.ColumnStreet, (object)LJCRecord.Street }
			};
			lookupRecord = mAddressManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (!LJCIsUpdate
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
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
					keyColumns = new DbColumns()
					{
						{ Address.ColumnID, LJCRecord.ID }
					};
					mAddressManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					Address addedRecord = mAddressManager.Add(LJCRecord);
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

			if (NetString.HasValue(StreetTextbox.Text) == false)
			{
				retValue = false;
				builder.AppendLine($"  {StreetLabel.Text}");
			}
			if (NetString.HasValue(CityTextbox.Text) == false)
			{
				retValue = false;
				builder.AppendLine($"  {CityLabel.Text}");
			}
			if (NetString.HasValue(ProvinceTextbox.Text) == false)
			{
				retValue = false;
				builder.AppendLine($"  {ProvinceLabel.Text}");
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
			mAddressManager = new AddressManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			StreetLabel.BackColor = mSettings.BeginColor;
			ProvinceLabel.BackColor = mSettings.BeginColor;
			CityLabel.BackColor = mSettings.BeginColor;
			CitySectionLabel.BackColor = mSettings.BeginColor;
			PostalCodeLabel.BackColor = mSettings.BeginColor;
			TypeLabel.BackColor = mSettings.BeginColor;

			ProvinceTextbox.MaxLength = 60;
			CityTextbox.MaxLength = 60;
			CitySectionTextbox.MaxLength = 60;
			StreetTextbox.MaxLength = 45;
			PostalCodeTextbox.MaxLength = 10;

			// Load control data.
			CodeTypeClassManager manager
				= new CodeTypeClassManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			TypeCombo.LJCInit();
			TypeCombo.LJCLoad(manager.GetCodeClassID("Address"));

			// Set control layout.
			if (NetString.HasValue(mParentName))
			{
				ParentNameTextbox.Text = mParentName;
			}
			else
			{
				ParentNameLabel.Visible = false;
				ParentNameTextbox.Visible = false;
				Height -= 26;
			}
			Cursor = Cursors.Default;
		}
		#endregion

		#region Action Event Handlers

		// Display the help page.
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

		// 
		private void CitySectionButton_Click(object sender, EventArgs e)
		{
			// Initialize existing values.
			RegionTabForm regionForm = new RegionTabForm
			{
				LJCProvinceCode = mProvinceCode,
				LJCCityName = CityTextbox.Text,
				LJCCitySectionName = CitySectionTextbox.Text,
				IsChildWindow = true
			};

			if (DialogResult.OK == regionForm.ShowDialog())
			{
				RegionModule regionModule = regionForm.RegionModule;

				if (regionModule != null
					&& regionModule.LJCSelectedProvince != null)
				{
					mRegionID = 0;
					mProvinceID = 0;
					mProvinceCode = null;
					mCityID = 0;
					mCitySectionID = 0;
					ProvinceTextbox.Text = null;
					CityTextbox.Text = null;
					CitySectionTextbox.Text = null;

					RegionData regionData = regionModule.LJCSelectedRegion;
					if (regionData != null)
					{
						mRegionID = regionData.ID;
					}

					Province province = regionModule.LJCSelectedProvince;
					if (province != null)
					{
						mProvinceID = province.ID;
						mProvinceCode = province.Abbreviation;
						ProvinceTextbox.Text = province.Name;
					}

					City city = regionModule.LJCSelectedCity;
					if (city != null)
					{
						mCityID = city.ID;
						CityTextbox.Text = city.Name;
						PostalCodeTextbox.Text = city.ZipCode;
					}

					CitySection citySection = regionModule.LJCSelectedCitySection;
					if (citySection != null)
					{
						mCitySectionID = citySection.ID;
						CitySectionTextbox.Text = citySection.Name;
					}
				}
			}
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
		public Address LJCRecord { get; private set; }

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
		private AddressManager mAddressManager;

		private int mRegionID;
		private string mProvinceCode;
		private int mProvinceID;
		private int mCityID;
		private int mCitySectionID;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
