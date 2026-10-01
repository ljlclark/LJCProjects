// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TemplateTextCode.cs
using LJCControls5;
using LJCGenTextLib5;
using LJCGenTextXAL5;
using LJCNetCommon5;

namespace LJCGenTextEdit5
{
  internal class TemplateTextCode
  {
    #region Parent Object Properties

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    private LJCRtControl TemplateText { get; set; }

    private LJCRtControl OutputRtControl { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    internal TemplateTextCode(EditList parentList)
    {
      // Initialize property values.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;

      OutputRtControl = EditList.OutputRichText;
      TemplateText = EditList.TemplateRichText;

      MenuEventHandlers();
      ControlEventHandlers();
      EditList.Cursor = Cursors.Default;
    }

    // Creates the template menu event handlers.
    private void MenuEventHandlers()
    {
      EditList.TemplateFileEdit.Click += TemplateFileEdit_Click;
      EditList.TemplateMenuSections.Click += TemplateMenuSections_Click;
      EditList.TemplateGenerate.Click += TemplateGenerate_Click;
      EditList.TemplateSave.Click += TemplateSave_Click;
      EditList.TemplateExit.Click += TemplateExit_Click;
      EditList.TemplateHelp.Click += TemplateHelp_Click;
      EditList.TemplateAbout.Click += TemplateAbout_Click;
    }

    // Creates the control event handlers.
    private void ControlEventHandlers()
    {
      EditList.TemplateButton.Click += TemplateButton_Click;
      EditList.TemplateRichText.KeyDown += TemplateRichText_KeyDown;
      EditList.TemplateRichText.KeyUp += TemplateRichText_KeyUp;
    }
    #endregion

    #region Action Methods

    // Show the About dialog.
    internal static void About(bool isSplash = false)
    {
      GenTextEditSplash splash = new(isSplash: isSplash);
      splash.ShowDialog();
    }

    // Load the Template file.
    internal void TemplateLoad()
    {
      string? targetFileSpec = EditList.mFilePaths.TemplatePath;
      string? prevTargetPath = Path.GetDirectoryName(targetFileSpec);

      string? sourceFolder = Environment.CurrentDirectory;
      if (LJC.HasText(targetFileSpec))
      {
        if (targetFileSpec.StartsWith(".."))
        {
          sourceFolder = Path.GetDirectoryName(targetFileSpec);
          if (LJC.HasText(sourceFolder))
          {
            sourceFolder = Path.GetFullPath(sourceFolder);
          }
        }
        else
        {
          var targetFolder = Path.GetDirectoryName(targetFileSpec);
          if (LJC.HasText(targetFolder))
          {
            sourceFolder = Path.Combine(sourceFolder, targetFolder);
          }
        }
      }

      string filter = "C#(*.cs)|*.cs|All Files(*.*)|*.*";
      targetFileSpec = FormCommon.SelectFile(filter, sourceFolder, "*.cs");
      if (targetFileSpec != null)
      {
        EditList.TemplateTextbox.Text = Path.GetFileName(targetFileSpec);

        TemplateText.Font = new Font("Courier New", 12f
          , FontStyle.Bold);
        TemplateText.WordWrap = false;
        TemplateText.LJCLoadFromFile(targetFileSpec);

        string fromPath = Environment.CurrentDirectory;
        targetFileSpec = LJCNetFile.GetRelativePath(fromPath, targetFileSpec);

        // Target Path changed.
        string? targetPath = Path.GetDirectoryName(targetFileSpec);
        if (0 != string.Compare(prevTargetPath, targetPath, true))
        {
          string message = $"Save changed Target Path '{targetFileSpec}'?";
          if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            EditList.mFilePaths.TemplatePath = targetFileSpec;
          }
        }

        EditList.CreateColorSettings(TemplateText);
      }
    }

    /// <summary>Generates the Output code.</summary>
    internal void Generate()
    {
      FilePaths filePaths = EditList.mFilePaths;

      //mOutputRtControl.Font = new Font("Courier New", 12f
      //  , FontStyle.Bold);
      OutputRtControl.Font = new Font("Segoe UI", 11f);
      OutputRtControl.WordWrap = false;

      // Get data.
      var dataXMLPath = filePaths.DataXMLPath;
      var sections = GenSample.GetDataSections(dataXMLPath);

      // Generate text.
      if (sections != null)
      {
        var templateLines = EditList.TemplateRichText.Lines;
        var genTextLib = new GenTextLib();
        OutputRtControl.Text = genTextLib.TextGen(sections, templateLines);

        EditList.CreateColorSettings(OutputRtControl);
      }
    }

    // Save the Template file.
    internal void TemplateSave()
    {
      string? targetFileSpec = EditList.mFilePaths.TemplatePath;
      string? prevTargetPath = Path.GetDirectoryName(targetFileSpec);
      string sourceFileName = Path.GetFileName(targetFileSpec);
      string targetFileName = EditList.TemplateTextbox.Text.Trim();

      string? sourcefolder = Path.GetDirectoryName(targetFileSpec);
      if (LJC.HasText(sourcefolder)
        && !sourcefolder.StartsWith(".."))
      {
        sourcefolder = Path.Combine(Directory.GetCurrentDirectory(), sourcefolder);
      }

      // The File name has changed.
      if (0 != string.Compare(sourceFileName, targetFileName, true))
      {
        string filter = "C#(*.cs)|*.cs|All Files(*.*)|*.*";
        targetFileSpec = FormCommon.SaveFile(filter, sourcefolder
          , targetFileName);
        if (LJC.HasText(targetFileSpec))
        {
          string fromPath = Directory.GetCurrentDirectory();
          targetFileSpec = LJCNetFile.GetRelativePath(fromPath, targetFileSpec);
        }
      }

      // A Target File was selected.
      if (targetFileSpec != null)
      {
        string message = $"Save Template '{targetFileSpec}'?";
        if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          File.WriteAllText(targetFileSpec, "");
          StreamWriter writer = File.CreateText(targetFileSpec);
          int count = 0;
          foreach (string line in TemplateText.Lines)
          {
            count++;
            if (count >= TemplateText.Lines.Length
              && !LJC.HasText(line))
            {
              break;
            }
            string output = LJCRtControl.LJCSetLeadingSpacesToTabs(line, 2);
            writer.WriteLine(output);
          }
          writer.Close();

          // Target Path changed.
          string? targetPath = Path.GetDirectoryName(targetFileSpec);
          if (0 != string.Compare(prevTargetPath, targetPath, true))
          {
            message = $"Save changed Template Path '{targetFileSpec}'?";
            if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
              EditList.mFilePaths.TemplatePath = targetFileSpec;
            }
          }
        }
      }
    }

    // Generates the Sections data from the template.
    internal void CreateDataFromTemplate()
    {
      FilePaths filePaths = EditList.mFilePaths;

      var genSample = new GenSample();
      string[] lines = TemplateText.Lines;
      Sections? sections = genSample.CreateSections(lines);

      if (LJC.HasListItems(sections))
      {
        LJCNetFile.CreateFolder("DataXML");
        string fileSpec = @"DataXML\GenSections.xml";
        sections.LJCSerialize(fileSpec);

        EditList.SectionManager = new SectionManager(fileSpec);
        SectionManager manager = EditList.SectionManager;
        string fromPath = Environment.CurrentDirectory;
        string fullSpec = Path.GetFullPath(fileSpec);
        filePaths.DataXMLPath = LJCNetFile.GetRelativePath(fromPath, fullSpec);
        EditList.DataXMLTextbox.Text = manager.FileName;
        EditList.SectionGridCode.DataRetrieve();
      }
    }

    // Closes the application.
    internal void DoClose()
    {
      EditList.mFilePaths.Serialize();
      EditList.SaveControlValues();
      EditList.Close();
    }

    // Set ColorSettings for the current line.
    internal void SetLineColors(Keys keyCode, LJCRtControl rtControl
      , CodeTokenizer tokens)
    {
      if (!IsControlKey(keyCode))
      {
        int lineIndex = rtControl.LJCGetCurrentLineIndex();
        string? lineText = rtControl.LJCGetCurrentLine();
        if (LJC.HasText(lineText))
        {
          // Reset the line to Black.
          rtControl.LJCSetTextColor(lineIndex, 0, lineText.Length, Color.Black);

          EditList.mSyntaxColors.ColorSettings = [];
          EditList.mSyntaxColors.CreateLineColorSettings(tokens, lineText, lineIndex);
          EditList.SetTextColor(rtControl);
        }
      }
    }

    // Check if the key is a control key.
    private static bool IsControlKey(Keys keyCode)
    {
      bool retValue = false;

      if (keyCode == Keys.ShiftKey
        || keyCode == Keys.Left || keyCode == Keys.Right
        || keyCode == Keys.Up || keyCode == Keys.Down
        || keyCode == Keys.ControlKey || keyCode == Keys.Alt)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Action Event Handlers

    // Allows for display and edit of a text file.
    private void TemplateFileEdit_Click(object? sender, EventArgs e)
    {
      FormCommon.ShellFile("NotePad.exe");
    }

    // Performs the Create Sections function.
    private void TemplateMenuSections_Click(object? sender, EventArgs e)
    {
      CreateDataFromTemplate();
    }

    // Performs the Generate Output function.
    private void TemplateGenerate_Click(object? sender, EventArgs e)
    {
      Generate();
    }

    // Performs the Save function.
    private void TemplateSave_Click(object? sender, EventArgs e)
    {
      TemplateSave();
    }

    // Performs the Close function.
    private void TemplateExit_Click(object? sender, EventArgs e)
    {
      DoClose();
    }

    // Displays the context sensitive help.
    private void TemplateHelp_Click(object? sender, EventArgs e)
    {
      Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
        , @"Template\TemplateText.html");
    }

    // Displays the Splash dialog as an about dialog.
    private void TemplateAbout_Click(object? sender, EventArgs e)
    {
      About();
    }
    #endregion

    #region Control Event Handlers

    // Performs the Select Template file function.
    private void TemplateButton_Click(object? sender, EventArgs e)
    {
      TemplateLoad();
    }

    // Handles the text keys.
    private void TemplateRichText_KeyDown(object? sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
            , @"Template\TemplateText.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            RichTextBox richText = EditList.TemplateRichText;
            var position = FormCommon.GetMenuScreenPoint(richText
              , Control.MousePosition);
            EditList.TemplateMenu.Show(position);
            EditList.TemplateMenu.Select();
            e.Handled = true;
          }
          break;
      }
    }

    // Set ColorSettings for the current line.
    private void TemplateRichText_KeyUp(object? sender, KeyEventArgs e)
    {
      if (!e.Control)
      {
        LJCRtControl richText = EditList.TemplateRichText;
        CodeTokenizer tokenizer = EditList.mTokenizer;
        SetLineColors(e.KeyCode, richText, tokenizer);
      }
    }
    #endregion
  }
}
