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
  }
}

