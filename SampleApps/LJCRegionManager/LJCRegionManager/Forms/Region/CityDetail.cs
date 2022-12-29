// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using System.Text;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCRegionDAL;
using LJCWinFormCommon;

namespace LJCRegionManager
{
  /// <summary>The City detail dialog.</summary>
  public partial class CityDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public CityDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCParentID = 0;
      LJCParentName = null;
      LJCIsUpdate = false;
      LJCRecord = null;
      LJCHelpFileName = "LJCRegionManager.chm";
      LJCHelpPageName = @"City\CityDetail.html";
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void CityDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;

      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Handles the form keys.
    private void CityDetail_KeyDown(object sender, KeyEventArgs e)
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
    /// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);

      FormCommon.CreateGradient(e.Graphics, ClientRectangle
        , mSettings.BeginColor, mSettings.EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    /// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    private void DataRetrieve()
    {
      City record;

      Cursor = Cursors.WaitCursor;
      if (LJCID > 0)
      {
        LJCIsUpdate = true;
        var keyColumns = new DbColumns()
        {
          { City.ColumnID, LJCID }
        };
        record = mCityManager.Retrieve(keyColumns);
        GetRecordValues(record);
      }
      else
      {
        LJCIsUpdate = false;
        LJCRecord = new City();
        ParentNameTextbox.Text = LJCParentName;
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    /// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    private void GetRecordValues(City dataRecord)
    {
      if (dataRecord != null)
      {
        LJCParentID = dataRecord.ProvinceID;
        ParentNameTextbox.Text = LJCParentName;
        NameTextbox.Text = dataRecord.Name;
        DescriptionTextbox.Text = dataRecord.Description;
        CityCheckbox.Checked = (bool)dataRecord.CityFlag;
      }
    }

    // Creates and returns a record object with the data from
    /// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    private City SetRecordValues()
    {
      City retVal = new City()
      {
        ID = LJCID,
        ProvinceID = LJCParentID,
        Name = NameTextbox.Text.Trim(),
        Description = FormCommon.SetString(DescriptionTextbox.Text),
        CityFlag = CityCheckbox.Checked
      };
      return retVal;
    }

    /// <summary>
    /// Resets the empty record values.
    /// </summary>
    /// <param name="dataRecord">The data record object.</param>
    private void ResetRecordValues(City dataRecord)
    {
      dataRecord.Description = FormCommon.SetString(dataRecord.Description);
    }

    // Saves the data.
    /// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    private bool DataSave()
    {
      City lookupRecord;
      string title;
      string message;
      bool retValue = true;

      LJCRecord = SetRecordValues();

      var keyColumns = new DbColumns()
      {
        { City.ColumnProvinceID, LJCRecord.ProvinceID },
        { City.ColumnName, LJCRecord.Name }
      };
      lookupRecord = mCityManager.Retrieve(keyColumns);
      if (lookupRecord != null
        && (false == LJCIsUpdate
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
          var updateKeyColumns = new DbColumns()
          {
            { City.ColumnID, LJCRecord.ID }
          };
          mCityManager.Update(LJCRecord, updateKeyColumns);
          ResetRecordValues(LJCRecord);
        }
        else
        {
          City city = mCityManager.Add(LJCRecord);
          ResetRecordValues(LJCRecord);
          if (city != null)
          {
            LJCRecord.ID = city.ID;
          }
        }
      }
      return retValue;
    }

    // Validates the data.
    /// <include path='items/IsValid/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    private bool IsValid()
    {
      StringBuilder builder;
      string title;
      string message;
      bool retVal = true;

      builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (false == NetString.HasValue(NameTextbox.Text))
      {
        retVal = false;
        builder.AppendLine($"  {NameLabel.Text}");
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
    /// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    private void InitializeControls()
    {
      // Get singleton values.
      ValuesRegion values = ValuesRegion.Instance;

      // Initialize Class Data.
      mSettings = values.StandardSettings;

      mCityManager = new CityManager(mSettings.DbServiceRef
        , mSettings.DataConfigName);

      ParentNameLabel.BackColor = mSettings.BeginColor;
      NameLabel.BackColor = mSettings.BeginColor;
      DescriptionLabel.BackColor = mSettings.BeginColor;

      NameTextbox.MaxLength = City.LengthName;
      DescriptionTextbox.MaxLength = City.LengthDescription;
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
    /// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ID value.</summary>
    public int LJCID { get; set; }

    /// <summary>Gets or sets the Parent ID value.</summary>
    public int LJCParentID { get; set; }

    /// <summary>Gets or sets the Parent name value.</summary>
    public string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    /// <summary>Gets the IsUpdate value.</summary>
    public bool LJCIsUpdate { get; private set; }

    /// <summary>Gets a reference to the record object.</summary>
    public City LJCRecord { get; private set; }

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

    private StandardSettings mSettings;
    private CityManager mCityManager;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
