using LJCDataDetailDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DataDetail
{
  /// <summary>The Tab detail dialog.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  internal partial class TabDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Detail.xml'/>
    internal TabDetail(DataDetailData dataDetailData)
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;
      LJCIsUpdate = false;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
      DataDetailData = dataDetailData;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void TabDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Paint the form background.
    /// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
        , EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    /// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/Detail.xml'/>
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Tab Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = DataDetailData.Managers.ControlTabManager;
        var dataRecord = manager.RetrieveWithID(LJCID);
        LJCTabOriginalIndex = dataRecord.TabIndex;
        GetRecordValues(dataRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        ParentNameText.Text = LJCParentName;
        TabIndexText.Text = LJCSetIndex.ToString();

        // Set default values.
        LJCRecord = new ControlTab();
      }
      TabIndexText.Select();
      TabIndexText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(ControlTab dataRecord)
    {
      if (dataRecord != null)
      {
        LJCParentID = dataRecord.ControlDetailID;
        ParentNameText.Text = LJCParentName;

        TabIndexText.Text = dataRecord.TabIndex.ToString();
        TabTextText.Text = dataRecord.Caption;
        DescriptionText.Text = dataRecord.Description;
      }
    }

    // Creates and returns a record object with the data from
    private ControlTab SetRecordValues()
    {
      ControlTab retValue = new ControlTab()
      {
        ID = LJCID,
        ControlDetailID = LJCParentID,
        Caption = TabTextText.Text.Trim(),
        Description = DescriptionText.Text.Trim()
      };
      int.TryParse(TabIndexText.Text, out int value);
      retValue.TabIndex = value;
      return retValue;
    }

    // Saves the data.
    private bool DataSave()
    {
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      var manager = DataDetailData.Managers.ControlTabManager;

      if (LJCTabOriginalIndex == LJCRecord.TabIndex)
      {
        var lookupRecord = manager.RetrieveWithUnique(LJCRecord.ControlDetailID
          , LJCRecord.TabIndex);
        if (manager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
        {
          retValue = false;
          title = "Data Entry Error";
          message = "The record already exists.";
          Cursor = Cursors.Default;
          MessageBox.Show(message, title, MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyRecord = manager.GetIDKey(LJCRecord.ID);
          if (LJCTabOriginalIndex != LJCRecord.TabIndex)
          {
            MoveTab(keyRecord);
          }
          manager.Update(LJCRecord, keyRecord);
          if (0 == manager.AffectedCount)
          {
            title = "Update Error";
            message = "The Record was not updated.";
            MessageBox.Show(message, title, MessageBoxButtons.OK
              , MessageBoxIcon.Information);
          }
        }
        else
        {
          var addedRecord = manager.Add(LJCRecord);
          if (null == addedRecord)
          {
            if (manager.AffectedCount < 1)
            {
              title = "Add Error";
              message = "The Record was not added.";
              MessageBox.Show(message, title, MessageBoxButtons.OK
                , MessageBoxIcon.Information);
            }
          }
          else
          {
            LJCRecord.ID = addedRecord.ID;
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
      bool retValue = true;

      builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (false == NetString.HasValue(TabIndexText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {TabIndexLabel.Text}");
      }
      if (false == NetString.HasValue(TabTextText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {TabTextLabel.Text}");
      }
      if (false == NetString.HasValue(DescriptionText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {DescriptionLabel.Text}");
      }

      if (retValue == false)
      {
        title = "Data Entry Error";
        message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }

    // Moves a TabPage.
    private void MoveTab(DbColumns keyColumns)
    {
      // tab will move to the left.
      if (LJCRecord.TabIndex < LJCTabOriginalIndex)
      {
        MoveLeft(keyColumns);
      }
      else
      {
        MoveRight(keyColumns);
      }
    }

    // Moves a TabPage to the left.
    private void MoveLeft(DbColumns keyColumns)
    {
      var manager = DataDetailData.Managers.ControlTabManager;

      // Move source tab out of the way.
      int targetIndex = LJCRecord.TabIndex;
      LJCRecord.TabIndex = -1;
      manager.Update(LJCRecord, keyColumns);
      if (1 == manager.AffectedCount)
      {
        // Move each tab index one to the right starting with left of
        // original source to right of target.
        DbColumns updateKeyColumns = new DbColumns()
        {
          { ControlTab.ColumnControlDetailID, LJCRecord.ControlDetailID },
        };
        int beginIndex = LJCTabOriginalIndex - 1;
        int endIndex = targetIndex;
        for (int index = beginIndex; index >= endIndex; index--)
        {
          // Get tab at old position.
          var controlTab
            = manager.RetrieveWithUnique(LJCRecord.ControlDetailID, index);
          if (controlTab != null)
          {
            // Update data object
            int newIndex = index + 1;
            controlTab.TabIndex = newIndex;

            // Update target keys.
            updateKeyColumns.LJCSetValue("TabIndex", index);

            // Move to new position.
            DbCommon.AddChangedName(controlTab, ControlTab.ColumnTabIndex);
            manager.Update(controlTab, updateKeyColumns);
          }
        }

        // Set source tab index to target index.
        LJCRecord.TabIndex = targetIndex;
      }
    }

    // Moves a TabPage to the right.
    private void MoveRight(DbColumns keyColumns)
    {
      var manager = DataDetailData.Managers.ControlTabManager;

      // Move source tab out of the way.
      int targetIndex = LJCRecord.TabIndex;
      LJCRecord.TabIndex = -1;
      manager.Update(LJCRecord, keyColumns);
      if (1 == manager.AffectedCount)
      {
        // Move each tab index one to the left starting with right of
        // original source to left of target.
        DbColumns updateKeyColumns = new DbColumns()
        {
          { ControlTab.ColumnControlDetailID, LJCRecord.ControlDetailID },
        };
        int beginIndex = LJCTabOriginalIndex + 1;
        int endIndex = targetIndex;
        for (int index = beginIndex; index <= endIndex; index++)
        {
          // Get tab at old position.
          var controlTab
            = manager.RetrieveWithUnique(LJCRecord.ControlDetailID, index);
          if (controlTab != null)
          {
            // Update data object
            int newIndex = index - 1;
            controlTab.TabIndex = newIndex;

            // Update target keys.
            updateKeyColumns.LJCSetValue("TabIndex", index);

            // Move to new position.
            DbCommon.AddChangedName(controlTab, ControlTab.ColumnTabIndex);
            manager.Update(controlTab, updateKeyColumns);
          }
        }

        // Set source tab index to target index.
        LJCRecord.TabIndex = targetIndex;
      }
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      //var values = Values_AppName_.Instance;
      //mSettings = values.StandardSettings;
      //BeginColor = mSettings.BeginColor;
      //EndColor = mSettings.EndColor;

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);

      TabIndexText.MaxLength = 3;
      TabTextText.MaxLength = ControlTab.LengthCaption;
      DescriptionText.MaxLength = ControlTab.LengthDescription;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
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
    #endregion

    #region Properties

    /// <summary>Gets or sets the primary ID value.</summary>
    public long LJCID { get; set; }

    /// <summary>Gets the LJCIsUpdate value.</summary>
    public bool LJCIsUpdate { get; private set; }

    /// <summary>Gets or sets the Parent ID value.</summary>
    public long LJCParentID { get; set; }

    /// <summary>Gets or sets the LJCParentName value.</summary>
    public string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    /// <summary>Gets a reference to the record object.</summary>
    public ControlTab LJCRecord { get; private set; }

    /// <summary>Sets the Index value.</summary>
    public int LJCSetIndex { get; set; }

    public int LJCTabOriginalIndex { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // Gets or sets ConfigData.
    private DataDetailData DataDetailData { get; set; }
    #endregion

    #region Class Data

    //private StandardSettings mSettings;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
