// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateFileChanges.cs
using LJCBackupCommonLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.XPath;

namespace LJCCreateFileChangesLib
{
  // The Create FileChanges class.
  /// <include path='items/CreateFileChanges/*'
  ///   file='Doc/ProjectCreateFileChanges.xml'/>
  public class CreateFileChanges
  {
    #region Constructors

    // Initializes an object instance with the supplied values.
    /// <include path='items/CreateFileChangesC/*'
    ///   file='Doc/CreateFileChanges.xml'/>
    public CreateFileChanges(string sourceRoot, string targetRoot
      , string changesFilespec, string includeFilter)
    {
      mSourceRoot = sourceRoot;
      mTargetRoot = targetRoot;

      mChangesFilespec = changesFilespec;
      mIncludeFilter = includeFilter;

      mChangeCommands = new List<string>();
      mMissingLines = new List<string>();
      SkipFiles = new List<string>();
    }
    #endregion

    #region Public Methods

    /// <summary>Runs the create "Changes"file process.</summary>
    /// <include path='items/Run/*' file='Doc/CreateFileChanges.xml'/>
    public void Run()
    {
      string[] filters = mIncludeFilter.Split('|');
      foreach (var filter in filters)
      {
        DeleteTargetNoSourceFiles(filter);
        CopyMissingOrChangedFiles(filter);
      }

      if (File.Exists(mChangesFilespec))
      {
        File.Delete(mChangesFilespec);
      }
      var text = string.Join("\r\n", mChangeCommands);
      File.AppendAllText(mChangesFilespec, text);

      var missingFolders = "MissingFolders.txt";
      if (File.Exists(missingFolders))
      {
        File.Delete(missingFolders);
      }
      text = $"Target Folders missing in:\r\n{mTargetRoot}\r\n";
      text += string.Join("\r\n", mMissingLines);
      File.AppendAllText(missingFolders, text);
    }
    #endregion

    #region Private Methods

    // Appends new FileChange commands to the ChangeFile.
    private bool AppendChangeFile(FileChange fileChange)
    {
      bool retValue = true;

      // Add if line is not already there.
      if (!HasLine(mChangeCommands, fileChange.Text()))
      {
        mChangeCommands.Add(fileChange.Text());
      }
      return retValue;
    }

    // Creates a "Copy" FileChange command for changed files.
    private void CopyChangedFiles(string sourceSpec, string targetSpec)
    {
      var sourceLines = File.ReadAllLines(sourceSpec);
      var targetLines = File.ReadAllLines(targetSpec);

      bool copy = false;
      if (sourceLines.Length != targetLines.Length)
      {
        copy = true;
      }
      if (!copy)
      {
        for (int index = 0; index < sourceLines.Length; index++)
        {
          var sourceLine = sourceLines[index];
          var targetLine = targetLines[index];

          var hasValue = true;
          if (!NetString.HasValue(sourceLine)
            && !NetString.HasValue(targetLine))
          {
            hasValue = false;
          }

          if (hasValue
            && sourceLine != targetLine)
          {
            // Do not consider lines that start with Generated as different?
            if (!sourceLine.StartsWith("<!-- Generated"))
            {
              copy = true;
              break;
            }
          }
        }
      }
      if (copy)
      {
        FileChange fileChange = new FileChange("Copy", sourceSpec);
        AppendChangeFile(fileChange);
      }
    }

    // Creates a "Copy" FileChange command for missing or changed files.
    private void CopyMissingOrChangedFiles(string filter)
    {
      // Get the source folder from end of a path.
      var sourceCodeLineFolder = FinalFolder(mSourceRoot);
      var filterPath = GetFilterPath(ref filter);

      var sourceSpecs = Directory.GetFiles(mSourceRoot, filter
        , SearchOption.AllDirectories);
      foreach (var sourceSpec in sourceSpecs)
      {
        // If filter has path, skip file that does not end with the filter path.
        if (NetString.HasValue(filterPath)
          && !HasFilterPath(sourceSpec, filterPath))
        {
          continue;
        }

        // Create the targetSpec from the targetRoot, sourceSpec folders and
        // filename after the sourceCodeLineFolder.
        var targetSpec = GetToSpec(mTargetRoot, sourceSpec, sourceCodeLineFolder
          , out string codePath);

        if (Skip(targetSpec, codePath))
        {
          continue;
        }

        //if (IsSkipFile(targetSpec)
        //  || HasExtraDots(targetSpec, filter))
        if (IsSkipFile(targetSpec))
        {
          // ToDo: Delete target skipped files?
          continue;
        }

        if (File.Exists(targetSpec))
        {
          CopyChangedFiles(sourceSpec, targetSpec);
        }
        else
        {
          // Copy missing file.
          FileChange fileChange = new FileChange("Copy", sourceSpec);
          AppendChangeFile(fileChange);
        }
      }
    }

    // Creates a "Delete" FileChange command for target files not in the source.
    private void DeleteTargetNoSourceFiles(string filter)
    {
      // Get the target folder from end of a path.
      var targetCodeLineFolder = FinalFolder(mTargetRoot);
      var filterPath = GetFilterPath(ref filter);

      // Get all target files by filter.
      var targetSpecs = Directory.GetFiles(mTargetRoot, filter
        , SearchOption.AllDirectories);
      foreach (var targetSpec in targetSpecs)
      {
        // If filter has path, skip file that does not end with the filter path.
        if (NetString.HasValue(filterPath)
          && !HasFilterPath(targetSpec, filterPath))
        {
          continue;
        }

        // Create the sourceSpec from the sourceRoot and targetSpec folders and
        // filename afer the targetCodeLineFolder.
        var sourceSpec = GetToSpec(mSourceRoot, targetSpec, targetCodeLineFolder
          , out string _);
        if (!File.Exists(sourceSpec))
        {
          // Target file is not found in source path.
          FileChange fileChange = new FileChange("Delete", targetSpec);
          AppendChangeFile(fileChange);
        }
      }
    }

    // Gets the final folder name.
    private string FinalFolder(string fileSpec)
    {
      string retFolder = null;

      var path = fileSpec;
      var extension = Path.GetExtension(fileSpec);
      if (File.Exists(fileSpec)
        || NetString.HasValue(extension))
      {
        // Strips everything from last folder separator "\".
        path = Path.GetDirectoryName(fileSpec);
      }
      var folders = path.Split('\\');
      retFolder = folders[folders.Length - 1];
      return retFolder;
    }

    // Create a 'to' file spec using the toFilePath and adding the folders and
    // file name using the fromFileSpec starting after the fromStartFolder.
    private string GetToSpec(string toFilePath
      , string fromFilespec, string fromStartFolder, out string codePath)
    {
      var retValue = toFilePath;

      codePath = "";
      var fromPath = Path.GetDirectoryName(fromFilespec);
      var fromFolders = fromPath.Split('\\');
      for (int index = fromFolders.Length - 1; index >= 0; index--)
      {
        if (fromFolders[index].ToLower() == fromStartFolder.ToLower())
        {
          // Skip fromStartFolder and add remaining folders.
          for (int index1 = index + 1; index1 < fromFolders.Length; index1++)
          {
            var fromFolder = fromFolders[index1].Trim();
            if (NetString.HasValue(fromFolder))
            {
              retValue = Path.Combine(retValue, fromFolder);
              codePath = Path.Combine(codePath, fromFolder);
            }
          }
          break;
        }
      }
      var fromFileName = Path.GetFileName(fromFilespec);
      retValue = Path.Combine(retValue, fromFileName);
      return retValue;
    }

    // Returns the filter path if it exists and updates the filter.
    private string GetFilterPath(ref string filter)
    {
      var retValue = Path.GetDirectoryName(filter);
      if (NetString.HasValue(retValue))
      {
        filter = Path.GetFileName(filter);
      }
      return retValue;
    }

    // Checks if a file has more than one dot.
    private bool HasExtraDots(string filespec, string filter)
    {
      bool retValue = false;

      var fileName = Path.GetFileName(filespec);

      // Has a dot before the extension dot.
      if (fileName.IndexOf(".") < fileName.Length - filter.Length
        && filter != ".config")
      {
        retValue = true;
      }
      return retValue;
    }

    // Check for filter path and if file is in the filter path.
    private bool HasFilterPath(string targetSpec, string filterPath)
    {
      bool retValue = false;

      var targetPath = Path.GetDirectoryName(targetSpec);
      string targetLastFolder = null;
      var targetFolders = targetPath.Split('\\');
      if (targetFolders.Length > 0)
      {
        targetLastFolder = targetFolders[targetFolders.Length - 1];
      }
      if (NetString.HasValue(targetLastFolder))
      {
        if (NetString.HasValue(filterPath)
          && (targetLastFolder == filterPath))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if text file already has a text line.
    private bool HasLine(List<string> list, string textLine)
    {
      bool retValue = false;

      foreach (string line in list)
      {
        // File already has the change command.
        if (line.ToLower() == textLine.ToLower())
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Checks if file is a skiped file.
    private bool IsSkipFile(string targetSpec)
    {
      bool retValue = false;

      var fileName = Path.GetFileName(targetSpec);
      foreach (string skipFile in SkipFiles)
      {
        if (skipFile.ToLower() == fileName.ToLower())
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Skips common unpromoted folders/files and missing target folders.
    private bool Skip(string fileSpec, string codePath)
    {
      var retValue = false;

      var filePath = Path.GetDirectoryName(fileSpec);

      // Skip common unpromoved folders/files.
      if (filePath.Contains("\\.vs")
        || filePath.Contains("\\obj\\"))
      {
        retValue = true;
      }
      if (!retValue)
      {
        var finalFolder = FinalFolder(filePath);
        var skipFolders = new List<string>()
          {
            "obj",
            //"Debug",
            "Release",
          };
        if (skipFolders.Contains(finalFolder))
        {
          retValue = true;
        }
      }

      // Skip updates to target folders that do not exist.
      if (!retValue
        && !Directory.Exists(filePath))
      {
        if (!retValue
          && !HasLine(mMissingLines, codePath))
        {
          mMissingLines.Add($"{codePath}");
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the skip file list.</summary>
    public List<string> SkipFiles { get; set; }
    #endregion

    #region Class Data

    private readonly string mSourceRoot;
    private readonly string mTargetRoot;

    private readonly string mChangesFilespec;
    private readonly string mIncludeFilter;

    private readonly List<string> mChangeCommands;
    private readonly List<string> mMissingLines;
    #endregion
  }
}
