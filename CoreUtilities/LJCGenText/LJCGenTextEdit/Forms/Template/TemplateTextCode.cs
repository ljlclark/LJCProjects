// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TemplateTextCode.cs
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCGenTextLib;

namespace LJCGenTextEdit
{
  internal class TemplateTextCode
  {
    #region Constructors

    // Initializes an object instance.
    internal TemplateTextCode(EditList parent)
    {
      // Set default class data.
      mParent = parent;
      //mOutputTextBox = parent.OutputTextbox;
      mTemplateRtControl = parent.TemplateRichText;
      mOutputRtControl = parent.OutputRichText;
    }
    #endregion

    #region Action Methods

    // Load the Template file.
    internal void DoTemplateLoad()
    {
      string targetFileSpec = mParent.mFilePaths.TemplatePath;
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
        mParent.TemplateTextbox.Text = Path.GetFileName(targetFileSpec);

        mTemplateRtControl.Font = new Font("Courier New", 9.0f);
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
            mParent.mFilePaths.TemplatePath = targetFileSpec;
          }
        }

        mParent.CreateColorSettings(mTemplateRtControl);
        mParent.SetTextColor(mTemplateRtControl);
      }
    }

    /// <summary>Generates the Output code.</summary>
    internal void DoGenerate()
    {
      FilePaths filePaths = mParent.mFilePaths;

      NetFile.CreateFolder("TempFiles");
      string tempPath = @"TempFiles\GenFile.cs";

      string templatePath = Path.GetDirectoryName(filePaths.TemplatePath);
      string templateFile = mParent.TemplateTextbox.Text.Trim();
      string templateFileSpec = Path.Combine(templatePath, templateFile);

      GenerateText genText = new GenerateText();
      genText.Generate(templateFileSpec, filePaths.DataXMLPath
        , tempPath, true);

      mOutputRtControl.Font = new Font("Courier New", 9.0f);
      mOutputRtControl.WordWrap = false;
      mOutputRtControl.LJCLoadFromFile(tempPath);

      mParent.CreateColorSettings(mOutputRtControl);
      mParent.SetTextColor(mOutputRtControl);
    }

    // Save the Template file.
    internal void DoTemplateSave()
    {
      string targetFileSpec = mParent.mFilePaths.TemplatePath;
      string prevTargetPath = Path.GetDirectoryName(targetFileSpec);
      string sourceFileName = Path.GetFileName(targetFileSpec);
      string targetFileName = mParent.TemplateTextbox.Text.Trim();

      string sourcefolder = Path.GetDirectoryName(targetFileSpec);
      if (false == sourcefolder.StartsWith(".."))
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
              && false == NetString.HasValue(line))
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
              mParent.mFilePaths.TemplatePath = targetFileSpec;
            }
          }
        }
      }
    }

    // Generates the Sections data from the template.
    internal void DoCreateDataFromTemplate()
    {
      FilePaths filePaths = mParent.mFilePaths;

      GenerateText genText = new GenerateText();
      string[] lines = mTemplateRtControl.Lines;
      Sections sections = genText.CreateSections(lines);

      NetFile.CreateFolder("DataXML");
      string fileSpec = @"DataXML\GenSections.xml";
      sections.LJCSerialize(fileSpec);

      mParent.GenDataManager = new GenDataManager(fileSpec);
      GenDataManager manager = mParent.GenDataManager;
      string fromPath = Environment.CurrentDirectory;
      string fullSpec = Path.GetFullPath(fileSpec);
      filePaths.DataXMLPath = NetFile.GetRelativePath(fromPath, fullSpec);
      mParent.DataXMLTextbox.Text = manager.FileName;
      mParent.mSectionGridCode.DataRetrieveSection();
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
      mParent.mFilePaths.Serialize();
      mParent.SaveControlValues();
      mParent.Close();
    }

    // Set ColorSettings for the current line.
    internal void DoSetLineColors(Keys keyCode, LJCRtControl rtControl
      , CodeTokenizer tokens)
    {
      if (false == IsControlKey(keyCode))
      {
        int lineIndex = rtControl.LJCGetCurrentLineIndex();
        string lineText = rtControl.LJCGetCurrentLine();
        if (NetString.HasValue(lineText))
        {
          // Reset the line to Black.
          rtControl.LJCSetTextColor(lineIndex, 0, lineText.Length, Color.Black);

          mParent.mSyntaxColors.ColorSettings = new ColorSettings();
          mParent.mSyntaxColors.CreateLineColorSettings(tokens, lineText, lineIndex);
          mParent.SetTextColor(rtControl);
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

    #region Class Data

    private readonly EditList mParent;
    //private readonly TextBox mOutputTextBox;
    private readonly LJCRtControl mTemplateRtControl;
    private readonly LJCRtControl mOutputRtControl;
    #endregion
  }
}
