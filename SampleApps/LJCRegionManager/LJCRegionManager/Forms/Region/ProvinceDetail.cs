// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProvinceDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCRegionDAL;
using LJCWinFormCommon;

namespace LJCRegionManager
{
  /// <summary>The Province detail dialog.</summary>
  public partial class ProvinceDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ProvinceDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCParentID = 0;
      LJCParentName = null;
      LJCIsUpdate = false;
      LJCRecord = null;
      LJCHelpFileName = "LJCRegionManager.chm";
      LJCHelpPageName = @"Province\ProvinceDetail.html";
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ProvinceDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;

      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Handles the form keys.
    private void ProvinceDetail_KeyDown(object sender, KeyEventArgs e)
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
      Province record;

      Cursor = Cursors.WaitCursor;
      if (LJCID > 0)
      {
        LJCIsUpdate = true;
        var keyColumns = new DbColumns()
        {
          { Province.ColumnID, LJCID }
        };
        record = mProvinceManager.Retrieve(keyColumns);
        GetRecordValues(record);
      }
      else
      {
        LJCIsUpdate = false;
        LJCRecord = new Province();
        ParentNameTextbox.Text = LJCParentName;
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    /// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private void GetRecordValues(Province dataRecord)
    {
      if (dataRecord != null)
      {
        LJCParentID = dataRecord.RegionID;
        ParentNameTextbox.Text = LJCParentName;
        NameTextbox.Text = dataRecord.Name;
        DescriptionTextbox.Text = dataRecord.Description;
        AbbrevTextbox.Text = dataRecord.Abbreviation;
      }
    }

    // Creates and returns a record object with the data from
    /// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private Province SetRecordValues()
    {
      Province retVal = new Province()
      {
        ID = LJCID,
        RegionID = LJCParentID,
        Name = NameTextbox.Text.Trim(),
        Description = FormCommon.SetString(DescriptionTextbox.Text),
        Abbreviation = FormCommon.SetString(AbbrevTextbox.Text)
      };
      return retVal;
    }

    /// <summary>
    /// Resets the empty record values.
    /// </summary>
    /// <param name="record">The data record object.</param>
    private void ResetRecordValues(Province record)
    {
      record.Description = FormCommon.SetString(record.Description);
      record.Abbreviation = FormCommon.SetString(record.Abbreviation);
    }

    // Saves the data.
    /// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private bool DataSave()
    {
      Province lookupRecord;
      string title;
      string message;
      bool retValue = true;

      LJCRecord = SetRecordValues();

      var keyColumns = new DbColumns()
      {
        { Province.ColumnRegionID, LJCRecord.RegionID },
        { Province.ColumnName, (object)LJCRecord.Name }
      };
      lookupRecord = mProvinceManager.Retrieve(keyColumns);
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
            { Province.ColumnID, LJCRecord.ID }
          };

          mProvinceManager.Update(LJCRecord, keyColumns);
          ResetRecordValues(LJCRecord);
        }
        else
        {
          Province addedRecord = mProvinceManager.Add(LJCRecord);
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
        builder.AppendLine("  {NameLabel.Text}");
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
      mProvinceManager = new ProvinceManager(mSettings.DbServiceRef
        , mSettings.DataConfigName);

      ParentNameLabel.BackColor = mSettings.BeginColor;
      AbbrevLabel.BackColor = mSettings.BeginColor;
      NameLabel.BackColor = mSettings.BeginColor;
      DescriptionLabel.BackColor = mSettings.BeginColor;

      AbbrevTextbox.MaxLength = Province.LengthAbbreviation;
      NameTextbox.MaxLength = Province.LengthName;
      DescriptionTextbox.MaxLength = Province.LengthDescription;
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

    // Does not allow spaces.
    private void NumberTextbox_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleSpace(e.KeyChar);
    }

    // Strips blanks from the text value.
    private void NumberTextbox_TextChanged(object sender, EventArgs e)
    {
      AbbrevTextbox.Text = FormCommon.StripBlanks(AbbrevTextbox.Text);
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
    public Province LJCRecord { get; private set; }

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
    private ProvinceManager mProvinceManager;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
