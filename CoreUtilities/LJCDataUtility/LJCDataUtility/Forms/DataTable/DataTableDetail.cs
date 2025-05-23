﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableDetail.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // The DataTable detail dialog.
  internal partial class DataTableDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal DataTableDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCIsUpdate = false;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;

      _ = new ControlFont(this);

      NameText.Leave += NameText_Leave;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void DataTableDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      //CenterToParent();
      Location = LJCLocation;
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "DataUtilityColumn Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataTableManager;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataUtilTable();
        ParentNameText.Text = LJCParentName;
        SequenceText.Text = FormCommon.DefaultZero();
        if (LJCSequence > 0)
        {
          SequenceText.Text = LJCSequence.ToString();
        }
        SchemaText.Text = "dbo";
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetValues(DataUtilTable data)
    {
      if (data != null)
      {
        // In control order.
        ParentNameText.Text = LJCParentName;
        NameText.Text = data.Name;
        DescriptionText.Text = data.Description;
        SequenceText.Text = data.Sequence.ToString();
        SchemaText.Text = data.SchemaName;
        if (!NetString.HasValue(data.SchemaName))
        {
          SchemaText.Text = "dbo";
        }
        NewNameText.Text = data.NewName;

        // Reference key values.
        LJCSiteID = data.DataSiteID;
        LJCParentID = data.DataModuleID;
        LJCParentSiteID = data.DataModuleSiteID;
      }
    }

    // Creates and returns a record object with the data from
    private DataUtilTable SetValues()
    {
      var retData = Data();

      // In control order.
      retData.Name = NameText.Text;
      retData.Description
        = FormCommon.SetString(DescriptionText.Text);
      retData.Sequence = NetCommon.ToInt32(SequenceText.Text);
      retData.SchemaName = SchemaText.Text;
      retData.NewName
        = FormCommon.SetString(NewNameText.Text);

      // Get Reference key values.
      retData.ID = LJCID;
      retData.DataSiteID = LJCSiteID;
      retData.DataModuleID = LJCParentID;
      retData.DataModuleSiteID = LJCParentSiteID;
      return retData;
    }

    // Resets the empty record values.
    private void ResetValues(DataUtilTable data)
    {
      // In control order.
      data.Description
        = FormCommon.SetString(data.Description);
      data.NewName
        = FormCommon.SetString(data.NewName);
    }

    // Gets the original or new record.
    private DataUtilTable Data()
    {
      DataUtilTable retData = null;

      if (mOriginalRecord != null)
      {
        retData = mOriginalRecord.Clone();
      }
      if (null == retData)
      {
        retData = new DataUtilTable();
      }
      return retData;
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetValues();
      var manager = LJCManagers.DataTableManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = manager.IDKey(LJCID);
          LJCRecord.ID = 0;
          manager.Update(LJCRecord, keyColumns);
          ResetValues(LJCRecord);
          LJCRecord.ID = LJCID;
          retValue = !FormCommon.UpdateError(this
            , manager.AffectedCount);
        }
        else
        {
          LJCRecord.ID = 0;
          LJCRecord.DataSiteID = mSettings.SiteID;
          var addedRecord = manager.Add(LJCRecord);
          ResetValues(LJCRecord);
          if (addedRecord != null)
          {
            LJCRecord.ID = addedRecord.ID;
          }
          retValue = !FormCommon.AddError(this
            , manager.AffectedCount);
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
      bool retValue = true;

      var builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (!NetString.HasValue(NameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {NameLabel.Text}");
      }

      if (!retValue)
      {
        var title = "Data Entry Error";
        var message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = ValuesDataUtility.Instance;
      LJCManagers = values.Managers;
      mSettings = values.StandardSettings;

      // Set control values.
      SetNoSpace();
      SetNumericOnly();
      NameText.MaxLength = DataUtilTable.LengthName;
      DescriptionText.MaxLength = DataUtilTable.LengthDescription;
      SequenceText.MaxLength = DataUtilTable.LengthSequence;
      NewNameText.MaxLength = DataUtilTable.LengthName;

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NameText.TextChanged += FormCommon.TextNoSpaceChanged;
      NewNameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NewNameText.TextChanged += FormCommon.TextNoSpaceChanged;
    }

    // Sets the Numeric events.
    private void SetNumericOnly()
    {
      SequenceText.KeyPress += mSequence.KeyPress;
      SequenceText.TextChanged += mSequence.TextChanged;
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    //// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Handles the Leave event.
    private void NameText_Leave(object sender, EventArgs e)
    {
      if (!NetString.HasValue(DescriptionText.Text))
      {
        DescriptionText.Text = NameText.Text;
      }
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
    #endregion

    #region Properties

    // Gets or sets the primary ID value.
    internal long LJCID { get; set; }

    // Gets or sets the primary ID value.
    internal long LJCSiteID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; }

    // Gets or sets the Parent ID value.
    internal long LJCParentID { get; set; }

    // Gets or sets the ParentSite ID value.
    internal long LJCParentSiteID { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets a reference to the record object.
    internal DataUtilTable LJCRecord { get; private set; }

    // Gets or sets the Sequence value.
    internal int LJCSequence { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private DataUtilTable mOriginalRecord;

    private StandardUISettings mSettings;

    private readonly TextNumber mSequence = new TextNumber();
    #endregion
  }
}
