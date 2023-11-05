// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CitySectionDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCRegionDAL;
using LJCWinFormCommon;

namespace LJCRegionManager
{
  /// <summary>The CitySection detail dialog.</summary>
  public partial class CitySectionDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public CitySectionDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCParentID = 0;
      LJCParentName = null;
      LJCIsUpdate = false;
      LJCRecord = null;
      LJCHelpFileName = "LJCRegionManager.chm";
      LJCHelpPageName = @"CitySection\CitySectionDetail.html";
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void CitySectionDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;

      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Handles the form keys.
    private void CitySectionDetail_KeyDown(object sender, KeyEventArgs e)
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
      CitySection record;

      Cursor = Cursors.WaitCursor;
      if (LJCID > 0)
      {
        LJCIsUpdate = true;
        var keyColumns = new DbColumns()
        {
          { CitySection.ColumnID, LJCID }
        };
        record = mCitySectionManager.Retrieve(keyColumns);
        GetRecordValues(record);
      }
      else
      {
        LJCIsUpdate = false;
        LJCRecord = new CitySection();
        ParentTextbox.Text = LJCParentName;
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    /// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private void GetRecordValues(CitySection dataRecord)
    {
      if (dataRecord != null)
      {
        LJCParentID = dataRecord.CityID;
        ParentTextbox.Text = LJCParentName;
        NameTextbox.Text = dataRecord.Name;
        DescriptionTextbox.Text = dataRecord.Description;
        ZoneTypeTextbox.Text = dataRecord.ZoneType;
        ContactTextbox.Text = dataRecord.Contact;
      }
    }

    // Creates and returns a record object with the data from
    /// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private CitySection SetRecordValues()
    {
      CitySection retVal = new CitySection()
      {
        ID = LJCID,
        CityID = LJCParentID,
        Name = NameTextbox.Text.Trim(),
        Description = FormCommon.SetString(DescriptionTextbox.Text),
        ZoneType = FormCommon.SetString(ZoneTypeTextbox.Text),
        Contact = FormCommon.SetString(ContactTextbox.Text)
      };
      return retVal;
    }

    /// <summary>
    /// Resets the empty record values.
    /// </summary>
    /// <param name="dataRecord">The data record object.</param>
    private void ResetRecordValues(CitySection dataRecord)
    {
      dataRecord.Description = FormCommon.SetString(dataRecord.Description);
      dataRecord.ZoneType = FormCommon.SetString(dataRecord.ZoneType);
      dataRecord.Contact = FormCommon.SetString(dataRecord.Contact);
    }

    // Saves the data.
    /// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private bool DataSave()
    {
      CitySection lookupRecord;
      string title;
      string message;
      bool retValue = true;

      LJCRecord = SetRecordValues();

      var keyColumns = new DbColumns()
      {
        { CitySection.ColumnCityID, LJCRecord.CityID },
        { CitySection.ColumnName, LJCRecord.Name }
      };
      lookupRecord = mCitySectionManager.Retrieve(keyColumns);
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
          keyColumns = new DbColumns()
          {
            { CitySection.ColumnID, LJCRecord.ID }
          };
          mCitySectionManager.Update(LJCRecord, keyColumns);
          ResetRecordValues(LJCRecord);
        }
        else
        {
          CitySection addedRecord = mCitySectionManager.Add(LJCRecord);
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
    /// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private void InitializeControls()
    {
      // Get singleton values.
      ValuesRegion values = ValuesRegion.Instance;

      mSettings = values.StandardSettings;

      // Initialize Class Data.
      mCitySectionManager = new CitySectionManager(mSettings.DbServiceRef
        , mSettings.DataConfigName);

      ParentLabel.BackColor = mSettings.BeginColor;
      NameLabel.BackColor = mSettings.BeginColor;
      DescriptionLabel.BackColor = mSettings.BeginColor;

      NameTextbox.MaxLength = CitySection.LengthName;
      DescriptionTextbox.MaxLength = CitySection.LengthDescription;
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
    /// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
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

    /// <summary>Gets or sets the LJCParent name value.</summary>
    public string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    /// <summary>Gets the LJCIsUpdate value.</summary>
    public bool LJCIsUpdate { get; private set; }

    /// <summary>Gets a reference to the record object.</summary>
    public CitySection LJCRecord { get; private set; }

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
    private CitySectionManager mCitySectionManager;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
