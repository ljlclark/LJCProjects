// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// OutputTextCode.cs
using LJCControls5;
using LJCNetCommon5;

namespace LJCGenTextEdit5
{
  internal class OutputTextCode
  {
    #region Parent Control Properties

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    private TextBox OutputTextBox { get; set; }

    private LJCRtControl OutputText { get; set; }

    private LJCRtControl TemplateText { get; set; }

    private TemplateTextCode TemplateTextCode { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    internal OutputTextCode(EditList parentList)
    {
      // Set default class data.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;

      OutputTextBox = EditList.OutputTextbox;
      OutputText = parentList.OutputRichText;
      TemplateText = parentList.TemplateRichText;
      TemplateTextCode = EditList.TemplateTextCode;

      MenuEventHandlers();
      ControlEventHandlers();
      EditList.Cursor = Cursors.WaitCursor;
    }

    // Creates the menu event handlers.
    private void MenuEventHandlers()
    {
      EditList.XMLDecode.Click += XMLDecode_Click;
      EditList.XMLEncode.Click += XMLEncode_Click;
      EditList.HTMLXMLDecode.Click += HTMLXMLDecode_Click;
      EditList.HTMLXMLEncode.Click += HTMLXMLEncode_Click;
      EditList.HTMLCodeDecode.Click += HTMLCodeDecode_Click;
      EditList.OutputGenerate.Click += OutputGenerate_Click;
      EditList.OutputSave.Click += OutputSave_Click;
      EditList.OutputExit.Click += OutputExit_Click;
      EditList.OutputHelp.Click += OutputHelp_Click;
    }

    // Creates the control event handlers.
    private void ControlEventHandlers()
    {
      var richText = EditList.OutputRichText;
      EditList.OutputButton.Click += OutputButton_Click;
      richText.KeyDown += OutputRichText_KeyDown;
      richText.KeyUp += OutputRichText_KeyUp;  
    }
    #endregion

    #region Action Methods

    // Load the Output file.
    internal void OutputLoad()
    {
      string? targetFileSpec = EditList.mFilePaths.OutputPath;
      string? prevTargetPath = Path.GetDirectoryName(targetFileSpec);

      string? sourceFolder = Directory.GetCurrentDirectory();
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
      if (!Directory.Exists(sourceFolder))
      {
        LJCNetFile.CreateFolder($@"{sourceFolder}\");
      }

      string filter = "C#(*.cs)|*.cs|All Files(*.*)|*.*";
      targetFileSpec = FormCommon.SelectFile(filter, sourceFolder, "*.cs");
      if (targetFileSpec != null)
      {
        EditList.OutputTextbox.Text = Path.GetFileName(targetFileSpec);

        OutputText.Font = new Font("Courier New", 9.0f);
        OutputText.WordWrap = false;
        OutputText.LJCLoadFromFile(targetFileSpec);

        string fromPath = Environment.CurrentDirectory;
        targetFileSpec = LJCNetFile.GetRelativePath(fromPath, targetFileSpec);

        // Target Path changed.
        string? outputPath = Path.GetDirectoryName(targetFileSpec);
        if (0 != string.Compare(prevTargetPath, outputPath, true))
        {
          var message = $"Save changed Output Path '{targetFileSpec}'?";
          if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            EditList.mFilePaths.OutputPath = targetFileSpec;
          }
        }

        EditList.CreateColorSettings(OutputText);
        EditList.SetTextColor(OutputText);

        // Save for Comparison/Testing
        //LJCRtfSyntaxHighlight syntaxHighlight
        //	= new LJCRtfSyntaxHighlight(OutputRichText);
        //syntaxHighlight.FormatSyntax();
      }
    }

    // Save the Output file.
    internal void OutputSave()
    {
      string? targetFileSpec = EditList.mFilePaths.OutputPath;
      string? prevTargetPath = Path.GetDirectoryName(targetFileSpec);
      string sourceFileName = Path.GetFileName(targetFileSpec);
      string targetFileName = OutputTextBox.Text.Trim();

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
        if (targetFileSpec != null)
        {
          string fromPath = Directory.GetCurrentDirectory();
          targetFileSpec = LJCNetFile.GetRelativePath(fromPath, targetFileSpec);
        }
      }

      // A Target File was selected.
      if (targetFileSpec != null)
      {
        string message = $"Save Output file '{targetFileSpec}'?";
        if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          File.WriteAllText(targetFileSpec, "");
          StreamWriter writer = File.CreateText(targetFileSpec);
          int count = 0;
          foreach (string line in OutputText.Lines)
          {
            count++;
            if (count >= OutputText.Lines.Length
              && !LJC.HasText(line))
            {
              break;
            }
            string output = LJCRtControl.LJCSetLeadingSpacesToTabs(line, 4);
            writer.WriteLine(output);
          }
          writer.Close();

          // Target Path changed.
          string? targetPath = Path.GetDirectoryName(targetFileSpec);
          if (0 != string.Compare(prevTargetPath, targetPath, true))
          {
            message = $"Save changed Output Path '{targetFileSpec}'?";
            if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
              , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
              EditList.mFilePaths.OutputPath = targetFileSpec;
            }
          }
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // XML Decodes the output text.
    private void XMLDecode_Click(object? sender, EventArgs e)
    {
      OutputText.Text = LJC.XmlDecode(OutputText.Text);
    }

    // XML Encodes the output text.
    private void XMLEncode_Click(object? sender, EventArgs e)
    {
      OutputText.Text = LJC.XmlEncode(OutputText.Text);
    }

    // HTML XML Decodes the output text.
    private void HTMLXMLDecode_Click(object? sender, EventArgs e)
    {
      var text = OutputText.Text;

      if (LJC.HasText(text))
      {
        text = Decode(text);
        text = text.Replace("_ab_", "<span class=\"attrib\">");
        text = text.Replace("_nb_", Decode("<span class=\"name\">_lt_"));
        text = text.Replace("_ne_", Decode("</span>_gt_"));
        text = text.Replace("_se_", "</span>");
        OutputText.Text = text;
      }
    }

    // HTML XML Encodes the output text.
    private void HTMLXMLEncode_Click(object? sender, EventArgs e)
    {
      var text = OutputText.Text;

      if (LJC.HasText(text))
      {
        text = Encode(text);
        text = text.Replace("<span class=\"attrib\">", "_ab_");
        text = text.Replace(Encode("<span class=\"name\">_lt_"), "_nb_");
        text = text.Replace(Encode("</span>_gt_"), "_ne_");
        text = text.Replace("</span>", "_se_");
        OutputText.Text = text;
      }
    }

    private void HTMLCodeDecode_Click(object? sender, EventArgs e)
    {
      List<string> lines = [];
      var syntaxHtml = new SyntaxHighlightHtml();
      foreach (var line in OutputText.Lines)
      {
        lines.Add(syntaxHtml.AddSyntaxHighlight(line));
      }
      //OutputText.Lines = lines.ToArray();
      OutputText.Lines = [.. lines];
    }

    // Performs the Generate Output function.
    private void OutputGenerate_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.Generate();
    }

    // Performs the Save function.
    private void OutputSave_Click(object? sender, EventArgs e)
    {
      OutputSave();
    }

    // Performs the Close function.
    private void OutputExit_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void OutputHelp_Click(object? sender, EventArgs e)
    {
      Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
        , @"Output\OutputText.html");
    }

    // HTML Syntax Decodes < and >.
    private static string Decode(string text)
    {
      var retValue = text;

      retValue = retValue.Replace("_lt_", "<span class=\"ltgt\"><</span>");
      retValue = retValue.Replace("_gt_", "<span class=\"ltgt\">></span>");
      return retValue;
    }

    // HTML Syntax Encodes < and >.s
    private static string Encode(string text)
    {
      var retValue = text;

      retValue = retValue.Replace("<span class=\"ltgt\"><</span>", "_lt_");
      retValue = retValue.Replace("<span class=\"ltgt\">></span>", "_gt_");
      return retValue;
    }
    #endregion

    #region Control Event Handlers

    // Performs the Select Output file function.
    private void OutputButton_Click(object? sender, EventArgs e)
    {
      OutputLoad();
    }

    // Handles the form keys.
    private void OutputRichText_KeyDown(object? sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
            , @"Output\OutputText.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var richText = EditList.OutputRichText;
            var position = FormCommon.GetMenuScreenPoint(richText
              , Control.MousePosition);
            EditList.OutputMenu.Show(position);
            EditList.OutputMenu.Select();
            e.Handled = true;
          }
          break;
      }
    }

    // Set ColorSettings for the current line.
    private void OutputRichText_KeyUp(object? sender, KeyEventArgs e)
    {
      if (!e.Control)
      {
        var richText = EditList.OutputRichText;
        var tokenizer = EditList.mTokenizer;
        TemplateTextCode.SetLineColors(e.KeyCode, richText, tokenizer);
      }
    }
    #endregion
  }
}
