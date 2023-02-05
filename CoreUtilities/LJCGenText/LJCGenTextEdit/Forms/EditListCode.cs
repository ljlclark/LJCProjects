// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// EditListCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  // The GenText Edit list form.
  public partial class EditList : Form
  {
    #region Item Change Processing

    // Execute the related item functions.
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          ConfigureControls();
          RestoreControlValues();
          SectionSplit.SplitterDistance = SectionSplit.Height / 4;
          ItemSplit.SplitterDistance = ItemSplit.Height / 3;
          MainSplit.SplitterDistance = MainSplit.Width / 2;
          SectionGrid.LJCRestoreColumnValues(ControlValues);
          ItemGrid.LJCRestoreColumnValues(ControlValues);
          ReplacementGrid.LJCRestoreColumnValues(ControlValues);

          // Load first list.
          mSectionGridCode.DataRetrieveSection();
          break;

        case Change.Section:
          SectionGrid.LJCSetCounter(SectionCounter);
          mItemGridCode.DataRetrieveItem();
          break;

        case Change.Item:
          ItemGrid.LJCSetCounter(ItemCounter);
          mReplacementGridCode.DataRetrieveReplacement();
          break;

        case Change.Replacement:
          ReplacementGrid.LJCSetLastRow();
          ReplacementGrid.LJCSetCounter(ReplacementCounter);
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      Section,
      Item,
      Replacement
    }

    #region Item Change Support

    // Start the Change processing.
    private void StartChangeProcessing()
    {
      ChangeTimer = new ChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Gets or sets the ChangeTimer object.
    internal ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      // Make sure lists scroll vertically and counter labels show.
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        TemplateTextbox.Top -= 1;
        TemplateLabel.Top = TemplateTextbox.Top + 3;
        TemplateButton.Top = TemplateTextbox.Top + 1;

        DataXMLTextbox.Top -= 1;
        DataXMLLabel.Top = DataXMLTextbox.Top + 3;
        DataXMLButton.Top = DataXMLTextbox.Top + 1;

        OutputTextbox.Top -= 1;
        OutputLabel.Top = OutputTextbox.Top + 3;
        OutputButton.Top = OutputTextbox.Top + 1;

        SectionSplit.SplitterWidth = 5;
        ItemSplit.SplitterWidth = 5;

        // Modify MainSplit.Panel1 controls.
        ListHelper.SetPanelControls(SectionSplit.Panel1, SectionHeading
          , SectionToolPanel, SectionGrid, gridTop: -1);

        // Modify SectionSplit.Panel2, ItemSplit.
        ListHelper.SetPanelSplitControl(SectionSplit.Panel2, ItemSplit);

        // Modify ItemSplit.Panel1 controls.
        ListHelper.SetPanelControls(ItemSplit.Panel1, ItemHeading
          , ItemToolPanel, ItemGrid, gridTop: -1);

        // Modify ItemSplit.Panel2 controls.
        ListHelper.SetPanelControls(ItemSplit.Panel2, ReplacementHeading
          , ReplacementToolPanel, ReplacementGrid, gridTop: -1);
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      SetupGridCode();
      ControlSetup();
      InitialControlValues();
      SetupGrids();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Initial Control setup.
    private void ControlSetup()
    {
      MainSplit.Panel2Collapsed = true;
      MainTabs.LJCAllowDrag = true;
      MainTabs.AllowDrop = true;
      TileTabs.LJCAllowDrag = true;
      TileTabs.AllowDrop = true;
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\Section.xml";
      if (File.Exists(FilePaths.DefaultFileName))
      {
        mFilePaths = FilePaths.Deserialize(FilePaths.DefaultFileName);
      }
      else
      {
        mFilePaths = new FilePaths();
      }
      BackColor = BeginColor;
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(mControlValuesFileName))
      {
        try
        {
          ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
            , mControlValuesFileName) as ControlValues;
        }
        catch (Exception e)
        {
          StringBuilder build = new StringBuilder(128);
          build.AppendLine("The Control Values could not be restored.");
          build.AppendLine("The program will continue.");
          build.AppendLine(NetString.ExceptionString(e));
          string message = build.ToString();
          MessageBox.Show(message, "Deserialize Notification", MessageBoxButtons.OK
            , MessageBoxIcon.Information);
        }

        if (ControlValues != null)
        {
          // Restore Window values.
          controlValue = ControlValues.LJCSearchName(Name);
          if (controlValue != null)
          {
            Left = controlValue.Left;
            Top = controlValue.Top;
            Width = controlValue.Width;
            Height = controlValue.Height;
          }

          // Restore Splitter and other values.
          FormCommon.RestoreSplitDistance(SectionSplit, ControlValues);
          FormCommon.RestoreSplitDistance(ItemSplit, ControlValues);
        }
      }
    }

    // Saves the control values. 
    internal void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      SectionGrid.LJCSaveColumnValues(controlValues);
      ItemGrid.LJCSaveColumnValues(controlValues);
      ReplacementGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("SectionSplit.SplitterDistance", 0, 0, 0
        , SectionSplit.SplitterDistance);
      controlValues.Add("ItemSplit.SplitterDistance", 0, 0, 0
        , ItemSplit.SplitterDistance);

      // Save Window values.
      controlValues.Add(Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      mSectionGridCode = new SectionGridCode(this);
      mItemGridCode = new ItemGridCode(this);
      mReplacementGridCode = new ReplacementGridCode(this);
      mOutputTextCode = new OutputTextCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      SetupGridSection();
      SetupGridItem();
      SetupGridReplacement();
    }

    // Setup the grid display columns.
    private void SetupGridSection()
    {
      SectionGrid.BackgroundColor = BeginColor;

      if (0 == SectionGrid.Columns.Count)
      {
        mDisplayColumnsSection = new DbColumns()
        {
          "Name"
        };

        // Setup the grid display columns and column values.
        SectionGrid.LJCAddDisplayColumns(mDisplayColumnsSection);
      }
    }
    private DbColumns mDisplayColumnsSection;

    // Setup the grid display columns.
    private void SetupGridItem()
    {
      ItemGrid.BackgroundColor = BeginColor;

      if (0 == ItemGrid.Columns.Count)
      {
        mDisplayColumnsItem = new DbColumns()
        {
          "Name"
        };

        // Setup the grid display columns and column values.
        ItemGrid.LJCAddDisplayColumns(mDisplayColumnsItem);
      }
    }
    private DbColumns mDisplayColumnsItem;

    // Setup the grid display columns.
    private void SetupGridReplacement()
    {
      ReplacementGrid.BackgroundColor = BeginColor;

      if (0 == ReplacementGrid.Columns.Count)
      {
        mDisplayColumnsReplacement = new DbColumns()
        {
          "Name",
          "Value"
        };

        // Setup the grid display columns and column values.
        ReplacementGrid.LJCAddDisplayColumns(mDisplayColumnsReplacement);
      }
    }
    private DbColumns mDisplayColumnsReplacement;

    // Gets or sets the ControlValues item.
    private ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = SectionGrid.CurrentRow != null;
      FormCommon.SetToolState(SectionTool, enableNew, enableEdit);
      FormCommon.SetMenuState(SectionMenu, enableNew, enableEdit);
      SectionTitle.Enabled = true;
      SectionMenuCreateData.Enabled = true;
      SectionMenuAbout.Enabled = true;
      SectionMenuHelp.Enabled = true;

      enableNew = SectionGrid.CurrentRow != null;
      enableEdit = ItemGrid.CurrentRow != null;
      FormCommon.SetToolState(ItemTool, enableNew, enableEdit);
      FormCommon.SetMenuState(ItemMenu, enableNew, enableEdit);
      ItemTitle.Enabled = true;
      ItemMenuHelp.Enabled = true;

      enableNew = ItemGrid.CurrentRow != null;
      enableEdit = ReplacementGrid.CurrentRow != null;
      FormCommon.SetToolState(ReplacementTool, enableNew, enableEdit);
      FormCommon.SetMenuState(ReplacementMenu, enableNew, enableEdit);
      ReplacementTitle.Enabled = true;
      ReplacementMenuHelp.Enabled = true;
    }
    #endregion

    #region RTF Methods

    // Create the ColorSettings.
    internal void CreateColorSettings(LJCRtControl rtControl)
    {
      mSyntaxColors.ColorSettings = new ColorSettings();
      mSyntaxColors.CreateColorSettings(rtControl.Lines);
    }

    // Set the text colors.
    internal void SetTextColor(LJCRtControl rtControl)
    {
      if (null == mSyntaxColors)
      {
        MessageBox.Show("mSyntaxColors is null");
      }

      if (mSyntaxColors.ColorSettings != null
        && mSyntaxColors.ColorSettings.Count > 0)
      {
        foreach (ColorSetting setting in mSyntaxColors.ColorSettings)
        {
          rtControl.LJCSetTextColor(setting.LineIndex, setting.BeginIndex
            , setting.TextLength, setting.Color);
        }
      }
    }
    #endregion

    #region Properties

    // Gets the GenDataManager reference.
    internal GenDataManager GenDataManager { get; set; }
    #endregion

    #region Class Data

    internal FilePaths mFilePaths;
    internal TemplateTextCode mTemplateTextCode;
    internal SectionGridCode mSectionGridCode;
    internal SyntaxColors mSyntaxColors;

    private ItemGridCode mItemGridCode;
    private OutputTextCode mOutputTextCode;
    private ReplacementGridCode mReplacementGridCode;

    private string mControlValuesFileName;
    private readonly CodeTokenizer mTokenizer;
    #endregion
  }
}
