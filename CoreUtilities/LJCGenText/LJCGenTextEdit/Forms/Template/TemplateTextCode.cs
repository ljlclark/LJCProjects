// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TemplateTextCode.cs
using LJCGenTextLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  internal class TemplateTextCode
  {
    #region Constructors

    // Initializes an object instance.
    internal TemplateTextCode(EditList parentList)
    {
      // Initialize property values.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;
      mOutputRtControl = EditList.OutputRichText;
      mTemplateRtControl = EditList.TemplateRichText;
      EditList.Cursor = Cursors.Default;
    }
    #endregion

    #region Action Methods

    // Load the Template file.
    internal void DoTemplateLoad()
    {
      string targetFileSpec = EditList.mFilePaths.TemplatePath;
      string prevTargetPath = Path.GetDirectoryName(targetFileSpec);

      string sourceFolder = Environment.CurrentDirectory;
      if (NetString.HasValue(targetFileSpec))
      {
        if (targetFileSpec.StartsWith(".."))
        {
          sourceFolder = Path.GetDirectoryName(targetFileSpec);
          sourceFolder = Path.GetFullPath(sourceFolder);
        }
        else
        {
          sourceFolder = Path.Combine(sourceFolder
            , Path.GetDirectoryName(targetFileSpec));
        }
      }

      string filter = "C#(*.cs)|*.cs|All Files(*.*)|*.*";
      targetFileSpec = FormCommon.SelectFile(filter, sourceFolder, "*.cs");
      if (targetFileSpec != null)
      {
        EditList.TemplateTextbox.Text = Path.GetFileName(targetFileSpec);

        mTemplateRtControl.Font = new Font("Courier New", 12f
          , FontStyle.Bold);
        mTemplateRtControl.WordWrap = false;
        mTemplateRtControl.LJCLoadFromFile(targetFileSpec);

        string fromPath = Environment.CurrentDirectory;
        targetFileSpec = NetFile.GetRelativePath(fromPath, targetFileSpec);

        // Target Path changed.
        string targetPath = Path.GetDirectoryName(targetFileSpec);
        if (0 != string.Compare(prevTargetPath, targetPath, true))
        {
          string message = $"Save changed Target Path '{targetFileSpec}'?";
          if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            EditList.mFilePaths.TemplatePath = targetFileSpec;
          }
        }

        EditList.CreateColorSettings(mTemplateRtControl);
        //EditList.SetTextColor(mTemplateRtControl);
      }
    }

    /// <summary>Generates the Output code.</summary>
    internal void DoGenerate()
    {
      FilePaths filePaths = EditList.mFilePaths;

      //var templatePath = Path.GetDirectoryName(filePaths.TemplatePath);
      //var templateFile = EditList.TemplateTextbox.Text.Trim();
      //var templateFileSpec = Path.Combine(templatePath, templateFile);
      mOutputRtControl.Font = new Font("Courier New", 12f
        , FontStyle.Bold);
      mOutputRtControl.WordWrap = false;

      // Get data.
      var dataXMLPath = filePaths.DataXMLPath;
      var genText = new GenerateText();
      var sections = genText.GetDataSections(dataXMLPath);
      var templateLines = EditList.TemplateRichText.Lines;

      // Generate text.
      var textGenLib = new TextGenLib();
      mOutputRtControl.Text = textGenLib.TextGen(sections, templateLines);

      EditList.CreateColorSettings(mOutputRtControl);
      //EditList.SetTextColor(mOutputRtControl);
    }

    // Save the Template file.
    internal void DoTemplateSave()
    {
      string targetFileSpec = EditList.mFilePaths.TemplatePath;
      string prevTargetPath = Path.GetDirectoryName(targetFileSpec);
      string sourceFileName = Path.GetFileName(targetFileSpec);
      string targetFileName = EditList.TemplateTextbox.Text.Trim();

      string sourcefolder = Path.GetDirectoryName(targetFileSpec);
      if (!sourcefolder.StartsWith(".."))
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
          targetFileSpec = NetFile.GetRelativePath(fromPath, targetFileSpec);
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
          foreach (string line in mTemplateRtControl.Lines)
          {
            count++;
            if (count >= mTemplateRtControl.Lines.Length
              && !NetString.HasValue(line))
            {
              break;
            }
            string output = LJCRtControl.LJCSetLeadingSpacesToTabs(line, 2);
            writer.WriteLine(output);
          }
          writer.Close();

          // Target Path changed.
          string targetPath = Path.GetDirectoryName(targetFileSpec);
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
    internal void DoCreateDataFromTemplate()
    {
      FilePaths filePaths = EditList.mFilePaths;

      GenerateText genText = new GenerateText();
      string[] lines = mTemplateRtControl.Lines;
      Sections sections = genText.CreateSections(lines);

      NetFile.CreateFolder("DataXML");
      string fileSpec = @"DataXML\GenSections.xml";
      sections.LJCSerialize(fileSpec);

      EditList.GenDataManager = new GenDataManager(fileSpec);
      GenDataManager manager = EditList.GenDataManager;
      string fromPath = Environment.CurrentDirectory;
      string fullSpec = Path.GetFullPath(fileSpec);
      filePaths.DataXMLPath = NetFile.GetRelativePath(fromPath, fullSpec);
      EditList.DataXMLTextbox.Text = manager.FileName;
      EditList.mSectionGridCode.DataRetrieve();
    }

    // Show the About dialog.
    internal void DoAbout(bool isSplash = false)
    {
      GenTextEditSplash splash = new GenTextEditSplash(isSplash: isSplash);
      splash.ShowDialog();
    }

    // Closes the application.
    internal void DoClose()
    {
      EditList.mFilePaths.Serialize();
      EditList.SaveControlValues();
      EditList.Close();
    }

    // Set ColorSettings for the current line.
    internal void DoSetLineColors(Keys keyCode, LJCRtControl rtControl
      , CodeTokenizer tokens)
    {
      if (!IsControlKey(keyCode))
      {
        int lineIndex = rtControl.LJCGetCurrentLineIndex();
        string lineText = rtControl.LJCGetCurrentLine();
        if (NetString.HasValue(lineText))
        {
          // Reset the line to Black.
          rtControl.LJCSetTextColor(lineIndex, 0, lineText.Length, Color.Black);

          EditList.mSyntaxColors.ColorSettings = new ColorSettings();
          EditList.mSyntaxColors.CreateLineColorSettings(tokens, lineText, lineIndex);
          EditList.SetTextColor(rtControl);
        }
      }
    }

    // Check if the key is a control key.
    private bool IsControlKey(Keys keyCode)
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

    #region Properties

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }
    #endregion

    #region Class Data

    private readonly LJCRtControl mTemplateRtControl;
    private readonly LJCRtControl mOutputRtControl;
    #endregion
  }
}
