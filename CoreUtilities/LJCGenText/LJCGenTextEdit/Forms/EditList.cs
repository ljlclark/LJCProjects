// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// EditList.cs
using LJCDocGenLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  // The GenText Edit list form.
  /// <include path='items/EditList/*' file='Doc/ProjectGenTextEdit.xml'/>
  public partial class EditList : Form
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public EditList()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "GenTextEdit.chm";

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
      mSyntaxColors = new SyntaxColors();
      mTokenizer = new CodeTokenizer();
      mTokenizer.InitializeKeywords();
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void EditForm_Load(object sender, EventArgs e)
    {
      mTemplateTextCode = new TemplateTextCode(this);
      //mTemplateTextCode.DoAbout(true);
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    // Performs a Move of the selected Main Tab to the TileTabs control.
    private void MainTabsMove_Click(object sender, EventArgs e)
    {
      if (MainTabs.TabPages.Count > 1)
      {
        MainSplit.Panel2Collapsed = false;
        MainTabs.SelectedTab.Parent = TileTabs;
      }
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void TileTabsMove_Click(object sender, EventArgs e)
    {
      TileTabs.SelectedTab.Parent = MainTabs;
      if (0 == TileTabs.TabPages.Count)
      {
        MainSplit.Panel2Collapsed = true;
      }
    }

    #region Template

    // Allows for display and edit of a text file.
    private void TemplateFileEdit_Click(object sender, EventArgs e)
    {
      FormCommon.ShellFile("NotePad.exe");
    }

    // <summary>Performs the Create Sections function.</summary>
    private void TemplateMenuSections_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoCreateDataFromTemplate();
    }

    // <summary>Performs the Generate Output function.</summary>
    private void TemplateGenerate_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoGenerate();
    }

    // <summary>Performs the Save function.</summary>
    private void TemplateSave_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoTemplateSave();
    }

    // <summary>Performs the Close function.</summary>
    private void TemplateExit_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void TemplateHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Template\TemplateText.html");
    }

    // Displays the Splash dialog as an about dialog.
    private void TemplateAbout_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoAbout();
    }
    #endregion

    #region Section

    // Calls the New method.
    private void SectionToolNew_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoNewSection();
    }

    // Calls the Edit method.
    private void SectionToolEdit_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoEditSection();
    }

    // Calls the Delete method.
    private void SectionToolDelete_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoDeleteSection();
    }

    // Calls the New method.
    private void SectionMenuNew_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoNewSection();
    }

    // Calls the Edit method.
    private void SectionMenuEdit_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoEditSection();
    }

    // Calls the Delete method.
    private void SectionMenuDelete_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoDeleteSection();
    }

    // Calls the Refresh method.
    private void SectionMenuRefresh_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoRefreshSection();
    }

    // Creates the XML data.
    private void SectionMenuCreateData_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoCreateDataFromTable();
    }

    // Performs the Generate Output function.
    private void SectionMenuGenerate_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoGenerate();
    }

    // Performs the Save function.
    private void SectionMenuSave_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoDataXMLSave();
    }

    // <summary>Performs the Close function.</summary>
    private void SectionMenuExit_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void SectionMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Data\SectionList.html");
    }

    // Displays the Splash dialog as an about dialog.
    private void SectionMenuAbout_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoAbout();
    }
    #endregion

    #region Item

    // <summary>Calls the New method.</summary>
    private void ItemToolNew_Click(object sender, EventArgs e)
    {
      mItemGridCode.DoNewItem();
    }

    // <summary>Calls the Edit method.</summary>
    private void ItemToolEdit_Click(object sender, EventArgs e)
    {
      mItemGridCode.DoEditItem();
    }

    // <summary>Calls the Delete method.</summary>
    private void ItemToolDelete_Click(object sender, EventArgs e)
    {
      mItemGridCode.DoDeleteItem();
    }

    // <summary>Calls the New method.</summary>
    private void ItemMenuNew_Click(object sender, EventArgs e)
    {
      mItemGridCode.DoNewItem();
    }

    // <summary>Calls the Edit method.</summary>
    private void ItemMenuEdit_Click(object sender, EventArgs e)
    {
      mItemGridCode.DoEditItem();
    }

    // <summary>Calls the Delete method.</summary>
    private void ItemMenuDelete_Click(object sender, EventArgs e)
    {
      mItemGridCode.DoDeleteItem();
    }

    // <summary>Calls the Refresh method.</summary>
    private void ItemMenuRefresh_Click(object sender, EventArgs e)
    {
      mItemGridCode.DoRefreshItem();
    }

    // <summary>Performs the Generate Output function.</summary>
    private void ItemMenuGenerate_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoGenerate();
    }

    // <summary>Performs the Save function.</summary>
    private void ItemMenuSave_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoDataXMLSave();
    }

    // <summary>Performs the Close function.</summary>
    private void ItemMenuExit_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void ItemMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Data\ItemList.html");
    }
    #endregion

    #region Replacement

    // <summary>Calls the New method.</summary>
    private void ReplacementToolNew_Click(object sender, EventArgs e)
    {
      mReplacementGridCode.DoNewReplacement();
    }

    // <summary>Calls the Edit method.</summary>
    private void ReplacementToolEdit_Click(object sender, EventArgs e)
    {
      mReplacementGridCode.DoEditReplacement();
    }

    // <summary>Calls the Delete method.</summary>
    private void ReplacementToolDelete_Click(object sender, EventArgs e)
    {
      mReplacementGridCode.DoDeleteReplacement();
    }

    // <summary>Calls the New method.</summary>
    private void ReplacementMenuNew_Click(object sender, EventArgs e)
    {
      mReplacementGridCode.DoNewReplacement();
    }

    // <summary>Calls the Edit method.</summary>
    private void ReplacementMenuEdit_Click(object sender, EventArgs e)
    {
      mReplacementGridCode.DoEditReplacement();
    }

    // <summary>Calls the Delete method.</summary>
    private void ReplacementMenuDelete_Click(object sender, EventArgs e)
    {
      mReplacementGridCode.DoDeleteReplacement();
    }

    // <summary>Calls the Refresh method.</summary>
    private void ReplacementMenuRefresh_Click(object sender, EventArgs e)
    {
      mReplacementGridCode.DoRefreshReplacement();
    }

    // <summary>Performs the Generate Output function.</summary>
    private void ReplacementGenerate_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoGenerate();
    }

    // <summary>Performs the Save function.</summary>
    private void ReplacementSave_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoDataXMLSave();
    }

    // <summary>Performs the Close function.</summary>
    private void ReplacementMenuExit_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void ReplacementMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Data\ReplacementList.html");
    }
    #endregion

    #region Output

    // <summary>XML Decodes the output text.</summary>
    private void XMLDecode_Click(object sender, EventArgs e)
    {
      OutputRichText.Text = NetCommon.XmlDecode(OutputRichText.Text);
    }

    // <summary>XML Encodes the output text.</summary>
    private void XMLEncode_Click(object sender, EventArgs e)
    {
      OutputRichText.Text = NetCommon.XmlEncode(OutputRichText.Text);
    }

    // <summary>HTML XML Decodes the output text.</summary>
    private void HTMLXMLDecode_Click(object sender, EventArgs e)
    {
      var text = OutputRichText.Text;

      if (false == string.IsNullOrWhiteSpace(text))
      {
        text = Decode(text);
        text = text.Replace("_ab_", "<span class=\"attrib\">");
        text = text.Replace("_nb_", Decode("<span class=\"name\">_lt_"));
        text = text.Replace("_ne_", Decode("</span>_gt_"));
        text = text.Replace("_se_", "</span>");
        OutputRichText.Text = text;
      }
    }

    // <summary>HTML XML Encodes the output text.</summary>
    private void HTMLXMLEncode_Click(object sender, EventArgs e)
    {
      var text = OutputRichText.Text;

      if (false == string.IsNullOrWhiteSpace(text))
      {
        text = Encode(text);
        text = text.Replace("<span class=\"attrib\">", "_ab_");
        text = text.Replace(Encode("<span class=\"name\">_lt_"), "_nb_");
        text = text.Replace(Encode("</span>_gt_"), "_ne_");
        text = text.Replace("</span>", "_se_");
        OutputRichText.Text = text;
      }
    }

    private void HTMLCodeDecode_Click(object sender, EventArgs e)
    {

    }

    private void HTMLCodeEncode_Click(object sender, EventArgs e)
    {
      List<string> lines = new List<string>();
      var syntaxHtml = new SyntaxHighlightHtml();
      foreach (var line in OutputRichText.Lines)
      {
        lines.Add(syntaxHtml.AddSyntaxHighlight(line));
      }
      OutputRichText.Lines = lines.ToArray();
    }

    // <summary>Performs the Generate Output function.</summary>
    private void OutputGenerate_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoGenerate();
    }

    // <summary>Performs the Save function.</summary>
    private void OutputSave_Click(object sender, EventArgs e)
    {
      mOutputTextCode.DoOutputSave();
    }

    // <summary>Performs the Close function.</summary>
    private void OutputExit_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void OutputHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Output\OutputText.html");
    }

    // <summary>HTML Syntax Decodes < and >.</summary>
    private string Decode(string text)
    {
      var retValue = text;

      retValue = retValue.Replace("_lt_", "<span class=\"ltgt\"><</span>");
      retValue = retValue.Replace("_gt_", "<span class=\"ltgt\">></span>");
      return retValue;
    }

    // <summary>HTML Syntax Encodes < and >.</summary>
    private string Encode(string text)
    {
      var retValue = text;

      retValue = retValue.Replace("<span class=\"ltgt\"><</span>", "_lt_");
      retValue = retValue.Replace("<span class=\"ltgt\">></span>", "_gt_");
      return retValue;
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Template

    // <summary>Performs the Select Template file function.</summary>
    private void TemplateButton_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.DoTemplateLoad();
    }

    // Handles the control keys.
    private void TemplateRichText_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Template\TemplateText.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(TemplateRichText
              , MousePosition);
            TemplateMenu.Show(position);
            TemplateMenu.Select();
            e.Handled = true;
          }
          break;
      }
    }

    // Set ColorSettings for the current line.
    private void TemplateRichText_KeyUp(object sender, KeyEventArgs e)
    {
      if (false == e.Control)
      {
        mTemplateTextCode.DoSetLineColors(e.KeyCode, TemplateRichText, mTokenizer);
      }
    }
    #endregion

    #region Section

    // <summary>Performs the Select Data XML file function.</summary>
    private void DataXMLButton_Click(object sender, EventArgs e)
    {
      mSectionGridCode.DoDataXMLLoad();
    }

    // <summary>Handles the form keys.</summary>
    private void SectionGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mSectionGridCode.DoDefault();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Data\SectionList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          mSectionGridCode.DoRefreshSection();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(SectionGrid
              , MousePosition);
            SectionMenu.Show(position);
            SectionMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ReplacementGrid.Select();
          }
          else
          {
            ItemGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // <summary>Handles the MouseDoubleClick event.</summary>
    private void SectionGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (SectionGrid.LJCGetMouseRow(e) != null)
      {
        mSectionGridCode.DoDefault();
      }
    }

    // <summary>Handles the MouseDown event.</summary>
    private void SectionGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        SectionGrid.Select();
        if (SectionGrid.LJCIsDifferentRow(e))
        {
          SectionGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Section);
        }
      }
    }

    // <summary>Handles the SelectionChanged event.</summary>
    private void SectionGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (SectionGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Section);
      }
      SectionGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Item

    // <summary>Handles the form keys.</summary>
    private void ItemGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mItemGridCode.DoEditItem();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Data\ItemList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          mItemGridCode.DoRefreshItem();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ItemGrid
              , MousePosition);
            ItemMenu.Show(position);
            ItemMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            SectionGrid.Select();
          }
          else
          {
            ReplacementGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // <summary>Handles the MouseDoubleClick event.</summary>
    private void ItemGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ItemGrid.LJCGetMouseRow(e) != null)
      {
        mItemGridCode.DoEditItem();
      }
    }

    // <summary>Handles the MouseDown event.</summary>
    private void ItemGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ItemGrid.Select();
        if (ItemGrid.LJCIsDifferentRow(e))
        {
          ItemGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Item);
        }
      }
    }

    // <summary>Handles the SelectionChanged event.</summary>
    private void ItemGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ItemGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Item);
      }
      ItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Replacement

    // <summary>Handles the form keys.</summary>
    private void ReplacementGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mReplacementGridCode.DoEditReplacement();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Data\ReplacementList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          mReplacementGridCode.DoRefreshReplacement();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ReplacementGrid
              , MousePosition);
            ReplacementMenu.Show(position);
            ReplacementMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ItemGrid.Select();
          }
          else
          {
            SectionGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // <summary>Handles the MouseDoubleClick event.</summary>
    private void ReplacementGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ReplacementGrid.LJCGetMouseRow(e) != null)
      {
        mReplacementGridCode.DoEditReplacement();
      }
    }

    // <summary>Handles the MouseDown event.</summary>
    private void ReplacementGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ReplacementGrid.Select();
        if (ReplacementGrid.LJCIsDifferentRow(e))
        {
          ReplacementGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Replacement);
        }
      }
    }

    // <summary>Handles the SelectionChanged event.</summary>
    private void ReplacementGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ReplacementGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Replacement);
      }
      ReplacementGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Output

    // <summary>Performs the Select Output file function.</summary>
    private void OutputButton_Click(object sender, EventArgs e)
    {
      mOutputTextCode.DoOutputLoad();
    }

    // <summary>Handles the form keys.</summary>
    private void OutputRichText_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Output\OutputText.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(OutputRichText
              , MousePosition);
            OutputMenu.Show(position);
            OutputMenu.Select();
            e.Handled = true;
          }
          break;
      }
    }

    // Set ColorSettings for the current line.
    private void OutputRichText_KeyUp(object sender, KeyEventArgs e)
    {
      if (false == e.Control)
      {
        mTemplateTextCode.DoSetLineColors(e.KeyCode, OutputRichText, mTokenizer);
      }
    }
    #endregion
    #endregion

    #region Internal Properties

    // The help file name.
    internal string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;
    #endregion

    #region Private Properties

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion
  }
}

