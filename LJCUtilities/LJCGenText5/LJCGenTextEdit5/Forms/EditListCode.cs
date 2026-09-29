// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// EditListCode.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using System.Text;

namespace LJCGenTextEdit5
{
  // The GenText Edit list form.
  public partial class EditList : Form
  {
    #region Control Code Properties

    // Gets or sets the ItemGridCode reference.
    internal ItemGridCode ItemGridCode { get; set; } = null!;

    // Gets or sets the OutputTextCode reference.
    internal OutputTextCode OutputTextCode { get; set; } = null!;

    // Gets or sets the ReplacementGridCode reference.
    internal ReplacementGridCode ReplacementGridCode { get; set; } = null!;

    // Gets or sets the SectionGridCode reference.
    internal SectionGridCode SectionGridCode { get; set; } = null!;

    // Gets or sets the TemplateTextCode reference.
    internal TemplateTextCode TemplateTextCode { get; set; } = null!;
    #endregion

    #region Properties

    // Gets the RepeatItemManager reference.
    internal RepeatItemManager ItemManager
    {
      get => mItemManager;
      set
      {
        if (value != null)
        {
          mItemManager = value;
          ItemGridCode.ItemManager = mItemManager;
        }
      }
    }
    private RepeatItemManager mItemManager = null!;

    // Gets the ReplacementManager reference.
    internal ReplacementManager ReplacementManager
    {
      get => mReplacementManager;
      set
      {
        if (value != null)
        {
          mReplacementManager = value;
          ItemGridCode.ItemManager = mItemManager;
        }
      }
    }
    private ReplacementManager mReplacementManager = null!;

    // Gets the SectionManager reference.
    internal SectionManager SectionManager
    {
      get => mSectionManager;
      set
      {
        if (value != null)
        {
          mSectionManager = value;
          SectionGridCode.SectionManager = mSectionManager;
          ItemGridCode.ItemManager = mItemManager;
          ReplacementGridCode.ReplacementManager = mReplacementManager;
        }
      }
    }
    private SectionManager mSectionManager = null!;
    #endregion

    #region Class Data

    internal FilePaths mFilePaths = null!;
    internal SyntaxColors mSyntaxColors;
    internal readonly CodeTokenizer mTokenizer;
    private string mControlValuesFileName = null!;
    #endregion

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
          if (ControlValues != null)
          {
            SectionGrid.LJCRestoreColumnValues(ControlValues);
            ItemGrid.LJCRestoreColumnValues(ControlValues);
            ReplacementGrid.LJCRestoreColumnValues(ControlValues);
          }

          // Load first list.
          SectionGridCode.DataRetrieve();
          break;

        case Change.Section:
          ItemGridCode.DataRetrieve();
          break;

        case Change.Item:
          //mReplacementGridCode.DataRetrieve();
          break;

        case Change.Replacement:
          ReplacementGrid.LJCSetLastRow();
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
      ChangeTimer = new LJCChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    private void ChangeTimer_ItemChange(object? sender, EventArgs e)
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
    internal LJCChangeTimer ChangeTimer { get; set; } = null!;
    #endregion
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      // Make sure lists scroll vertically.
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        MainTabs.Height = ClientSize.Height;
        MainTabs.Width = ClientSize.Width;
        TemplateRichText.Width = MainTabs.TabPages[0].Width;
        var pageHeight = MainTabs.TabPages[0].Height;
        var pageWidth = MainTabs.TabPages[0].Width;
        TemplateRichText.Height = pageHeight - TemplateRichText.Top;
        SectionSplit.Height = ClientSize.Height - SectionSplit.Top;
        SectionSplit.Width = ClientSize.Width;
        OutputRichText.Height = pageHeight - OutputRichText.Top;
        OutputRichText.Width = pageWidth;
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
      LJCNetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\Section.xml";
      mFilePaths = FilePaths.Deserialize(FilePaths.DefaultFileName);
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue? controlValue;

      if (File.Exists(mControlValuesFileName))
      {
        try
        {
          ControlValues = LJC.XmlDeserialize(typeof(ControlValues)
            , mControlValuesFileName) as ControlValues;
        }
        catch (Exception e)
        {
          StringBuilder build = new(128);
          build.AppendLine("The Control Values could not be restored.");
          build.AppendLine("The program will continue.");
          build.AppendLine(LJCNetString.ExceptionString(e));
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
      ControlValues controlValues = [];

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

      LJC.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      _ = new TabsCode(this);
      TemplateTextCode = new TemplateTextCode(this);
      SectionGridCode = new SectionGridCode(this);
      ItemGridCode = new ItemGridCode(this);
      ReplacementGridCode = new ReplacementGridCode(this);
      OutputTextCode = new OutputTextCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      SetupGridSection();
      SetupGridItem();
      SetupGridReplacement();
    }

    // Setup the grid columns.
    private void SetupGridSection()
    {
      //SectionGrid.BackgroundColor = BeginColor;

      if (0 == SectionGrid.Columns.Count)
      {
        mGridColumnsSection = new LJCDataColumns()
        {
          "Name"
        };

        // Setup the grid columns and column values.
        SectionGrid.LJCAddColumns(mGridColumnsSection);
      }
    }
    private LJCDataColumns mGridColumnsSection = null!;

    // Setup the grid columns.
    private void SetupGridItem()
    {
      //ItemGrid.BackgroundColor = BeginColor;

      if (0 == ItemGrid.Columns.Count)
      {
        mGridColumnsItem = new LJCDataColumns()
        {
          "Name"
        };

        // Setup the grid columns and column values.
        ItemGrid.LJCAddColumns(mGridColumnsItem);
      }
    }
    private LJCDataColumns mGridColumnsItem = null!;

    // Setup the grid columns.
    private void SetupGridReplacement()
    {
      //ReplacementGrid.BackgroundColor = BeginColor;

      if (0 == ReplacementGrid.Columns.Count)
      {
        mGridColumnsReplacement = new LJCDataColumns()
        {
          "Name",
          "Value"
        };

        // Setup the grid columns and column values.
        ReplacementGrid.LJCAddColumns(mGridColumnsReplacement);
      }
    }
    private LJCDataColumns mGridColumnsReplacement = null!;

    // Gets or sets the ControlValues item.
    private ControlValues? ControlValues { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = SectionGrid.CurrentRow != null;
      FormCommon.SetMenuState(SectionMenu, enableNew, enableEdit);
      SectionTitle.Enabled = true;
      SectionMenuCreateData.Enabled = true;
      SectionMenuAbout.Enabled = true;
      SectionMenuHelp.Enabled = true;

      enableNew = SectionGrid.CurrentRow != null;
      enableEdit = ItemGrid.CurrentRow != null;
      FormCommon.SetMenuState(ItemMenu, enableNew, enableEdit);
      ItemTitle.Enabled = true;
      ItemMenuHelp.Enabled = true;

      enableNew = ItemGrid.CurrentRow != null;
      enableEdit = ReplacementGrid.CurrentRow != null;
      FormCommon.SetMenuState(ReplacementMenu, enableNew, enableEdit);
      ReplacementTitle.Enabled = true;
      ReplacementMenuHelp.Enabled = true;
    }
    #endregion

    #region RTF Methods

    // Create the ColorSettings.
    internal void CreateColorSettings(LJCRtControl rtControl)
    {
      mSyntaxColors.ColorSettings = [];
      mSyntaxColors.CreateColorSettings(rtControl.Lines);
    }

    // Set the text colors.
    internal void SetTextColor(LJCRtControl rtControl)
    {
      if (null == mSyntaxColors)
      {
        MessageBox.Show("mSyntaxColors is null");
      }

      if (mSyntaxColors != null
        && LJC.HasListItems(mSyntaxColors.ColorSettings))
      {
        foreach (ColorSetting setting in mSyntaxColors.ColorSettings)
        {
          rtControl.LJCSetTextColor(setting.LineIndex, setting.BeginIndex
            , setting.TextLength, setting.Color);
        }
      }
    }
    #endregion
  }
}
