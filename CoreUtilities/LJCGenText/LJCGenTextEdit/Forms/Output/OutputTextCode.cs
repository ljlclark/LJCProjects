// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// OutputTextCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  internal class OutputTextCode
  {
    #region Constructors

    // Initializes an object instance.
    internal OutputTextCode(EditList parentList)
    {
      // Set default class data.
      EditList = parentList;
      mOutputTextBox = EditList.OutputTextbox;
      mOutputText = parentList.OutputRichText;
    }
    #endregion

    #region Action Methods

    // Load the Output file.
    internal void DoOutputLoad()
    {
      string targetFileSpec = EditList.mFilePaths.OutputPath;
      string prevTargetPath = Path.GetDirectoryName(targetFileSpec);

      string sourceFolder = Directory.GetCurrentDirectory();
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
      if (!Directory.Exists(sourceFolder))
      {
        NetFile.CreateFolder($@"{sourceFolder}\");
      }

      string filter = "C#(*.cs)|*.cs|All Files(*.*)|*.*";
      targetFileSpec = FormCommon.SelectFile(filter, sourceFolder, "*.cs");
      if (targetFileSpec != null)
      {
        EditList.OutputTextbox.Text = Path.GetFileName(targetFileSpec);

        mOutputText.Font = new Font("Courier New", 9.0f);
        mOutputText.WordWrap = false;
        mOutputText.LJCLoadFromFile(targetFileSpec);

        string fromPath = Environment.CurrentDirectory;
        targetFileSpec = NetFile.GetRelativePath(fromPath, targetFileSpec);

        // Target Path changed.
        string outputPath = Path.GetDirectoryName(targetFileSpec);
        if (0 != string.Compare(prevTargetPath, outputPath, true))
        {
          var message = $"Save changed Output Path '{targetFileSpec}'?";
          if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            EditList.mFilePaths.OutputPath = targetFileSpec;
          }
        }

        EditList.CreateColorSettings(mOutputText);
        EditList.SetTextColor(mOutputText);

        // Save for Comparison/Testing
        //LJCRtfSyntaxHighlight syntaxHighlight
        //	= new LJCRtfSyntaxHighlight(OutputRichText);
        //syntaxHighlight.FormatSyntax();
      }
    }

    // Save the Output file.
    internal void DoOutputSave()
    {
      string targetFileSpec = EditList.mFilePaths.OutputPath;
      string prevTargetPath = Path.GetDirectoryName(targetFileSpec);
      string sourceFileName = Path.GetFileName(targetFileSpec);
      string targetFileName = mOutputTextBox.Text.Trim();

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
        string message = $"Save Output file '{targetFileSpec}'?";
        if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          File.WriteAllText(targetFileSpec, "");
          StreamWriter writer = File.CreateText(targetFileSpec);
          int count = 0;
          foreach (string line in mOutputText.Lines)
          {
            count++;
            if (count >= mOutputText.Lines.Length
              && !NetString.HasValue(line))
            {
              break;
            }
            string output = LJCRtControl.LJCSetLeadingSpacesToTabs(line, 4);
            writer.WriteLine(output);
          }
          writer.Close();

          // Target Path changed.
          string targetPath = Path.GetDirectoryName(targetFileSpec);
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

    #region Class Data

    private readonly EditList EditList;
    private readonly TextBox mOutputTextBox;
    private readonly LJCRtControl mOutputText;
    #endregion
  }
}
