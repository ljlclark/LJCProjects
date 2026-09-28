// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// EditList.cs
using LJCControls5;
using LJCNetCommon5;

namespace LJCGenTextEdit5
{
  // The GenText Edit list form.
  /// <include path='items/EditList/*' file='Doc/ProjectGenTextEdit.xml'/>
  public partial class EditList : Form
  {
    #region Properties

    // The help file name.
    internal string? LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = value?.Trim(); }
    }
    private string? mHelpFile;

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    //private Color EndColor { get; set; }
    #endregion

    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public EditList()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "GenTextEdit.chm";

      // Set default class data.
      BeginColor = Color.AliceBlue;
      //EndColor = Color.LightSkyBlue;
      //EndColor = Color.SkyBlue;
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
      TemplateTextCode.About(true);
      InitializeControls();
      CenterToParent();
    }

    // Resizes the affected form controls.
    private void EditList_Resize(object sender, EventArgs e)
    {
      MainTabs.Height = ClientSize.Height;
      MainTabs.Width = ClientSize.Width;
      var pageHeight = MainTabs.TabPages[0].Height;
      var pageWidth = MainTabs.TabPages[0].Width;
      TemplateRichText.Height = pageHeight - TemplateRichText.Top;
      TemplateRichText.Width = MainTabs.TabPages[0].Width;
      SectionSplit.Height = ClientSize.Height - SectionSplit.Top;
      SectionSplit.Width = ClientSize.Width;
      OutputRichText.Height = pageHeight - OutputRichText.Top;
      OutputRichText.Width = pageWidth;
    }
    #endregion

    #region Action Event Handlers

    // Performs a Move of the selected Main Tab to the TileTabs control.
    private void MainTabsMove_Click(object sender, EventArgs e)
    {
      if (MainTabs.TabPages.Count > 1)
      {
        MainSplit.Panel2Collapsed = false;
        if (MainTabs.SelectedTab != null)
        {
          MainTabs.SelectedTab.Parent = TileTabs;
        }
      }
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void TileTabsMove_Click(object sender, EventArgs e)
    {
      if (TileTabs.SelectedTab != null)
      {
        TileTabs.SelectedTab.Parent = MainTabs;
        if (0 == TileTabs.TabPages.Count)
        {
          MainSplit.Panel2Collapsed = true;
        }
      }
    }

    #region Output

    // <summary>XML Decodes the output text.</summary>
    private void XMLDecode_Click(object sender, EventArgs e)
    {
      OutputRichText.Text = LJC.XmlDecode(OutputRichText.Text);
    }

    // <summary>XML Encodes the output text.</summary>
    private void XMLEncode_Click(object sender, EventArgs e)
    {
      OutputRichText.Text = LJC.XmlEncode(OutputRichText.Text);
    }

    // <summary>HTML XML Decodes the output text.</summary>
    private void HTMLXMLDecode_Click(object sender, EventArgs e)
    {
      var text = OutputRichText.Text;

      if (LJC.HasText(text))
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

      if (LJC.HasText(text))
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
      List<string> lines = [];
      var syntaxHtml = new SyntaxHighlightHtml();
      foreach (var line in OutputRichText.Lines)
      {
        lines.Add(syntaxHtml.AddSyntaxHighlight(line));
      }
      //OutputRichText.Lines = lines.ToArray();
      OutputRichText.Lines = [.. lines];
    }

    // <summary>Performs the Generate Output function.</summary>
    private void OutputGenerate_Click(object sender, EventArgs e)
    {
      mTemplateTextCode.Generate();
    }

    // <summary>Performs the Save function.</summary>
    private void OutputSave_Click(object sender, EventArgs e)
    {
      //mOutputTextCode.DoOutputSave();
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
    private static string Decode(string text)
    {
      var retValue = text;

      retValue = retValue.Replace("_lt_", "<span class=\"ltgt\"><</span>");
      retValue = retValue.Replace("_gt_", "<span class=\"ltgt\">></span>");
      return retValue;
    }

    // <summary>HTML Syntax Encodes < and >.</summary>
    private static string Encode(string text)
    {
      var retValue = text;

      retValue = retValue.Replace("<span class=\"ltgt\"><</span>", "_lt_");
      retValue = retValue.Replace("<span class=\"ltgt\">></span>", "_gt_");
      return retValue;
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Tabs

    // Handles the MouseDown event.
    private void MainTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        MainTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = MainTabs.LJCGetTabPage(e);
      if (tabPage != null)
      {
        SetFocusTab(tabPage);
      }
    }

    // Handles the MouseDown event.
    private void TileTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TileTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = TileTabs.LJCGetTabPage(e);
      if (tabPage != null)
      {
        SetFocusTab(tabPage);
      }
    }
    #endregion

    #region Output

    // <summary>Performs the Select Output file function.</summary>
    private void OutputButton_Click(object sender, EventArgs e)
    {
      //mOutputTextCode.DoOutputLoad();
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
      if (!e.Control)
      {
        mTemplateTextCode.SetLineColors(e.KeyCode, OutputRichText, mTokenizer);
      }
    }
    #endregion
    #endregion
  }
}

