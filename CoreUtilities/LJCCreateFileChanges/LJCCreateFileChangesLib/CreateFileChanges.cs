﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateFileChanges.cs
using LJCBackupWatcherLib;
using LJCNetCommon;
using System.IO;
using System.Linq;

namespace LJCCreateFileChangesLib
{
  /// <summary>The Create FileChanges class.</summary>
  public class CreateFileChanges
  {
    #region Constructors

    // Initializes an object instance.
    /// <summary>
    /// Initializes an object instance with the specified values.
    /// </summary>
    /// <param name="sourcePath">The Source path.</param>
    /// <param name="targetPath">The Target path.</param>
    /// <param name="changeFileSpec">The ChangeFile spec.</param>
    /// <param name="multiFilter">The multiFilter value.</param>
    public CreateFileChanges(string sourcePath, string targetPath
      , string changeFileSpec, string multiFilter)
    {
      mSourcePath = sourcePath;
      mTargetPath = targetPath;
      mChangeFileSpec = changeFileSpec;
      mMultiFilter = multiFilter;
    }
    #endregion

    #region Public Methods

    /// <summary>Starts the create FileChanges process.</summary>
    public void Start()
    {
      string[] filters = mMultiFilter.Split(',');
      foreach (var filter in filters)
      {
        DeleteTargetMissingFiles(filter);
        CopyMissingOrChangedFiles(filter);
      }
    }
    #endregion

    #region Private Methods

    // Appends new FileChange commands to the ChangeFile.
    private bool AppendChangeFile(FileChange fileChange)
    {
      bool retValue = true;

      if (File.Exists(mChangeFileSpec))
      {
        var lines = File.ReadAllLines(mChangeFileSpec);
        for (int index = 0; index < lines.Count(); index++)
        {
          var line = lines[index];

          // File already has the change command.
          if (line == fileChange.Text())
          {
            retValue = false;
            break;
          }
        }
      }

      if (retValue)
      {
        File.AppendAllText(mChangeFileSpec, $"{fileChange.Text()}\r\n");
      }
      return retValue;
    }

    // Creates a "Copy" FileChange command for changed files.
    private void CopyChangedFiles(string sourceSpec, string targetSpec)
    {
      var sourceLines = File.ReadAllLines(sourceSpec);
      var targetLines = File.ReadAllLines(targetSpec);
      for (int index = 0; index < sourceLines.Length; index++)
      {
        if (sourceLines[index] != targetLines[index])
        {
          FileChange fileChange = new FileChange("Copy", sourceSpec);
          AppendChangeFile(fileChange);
          break;
        }
      }
    }

    // Creates a "Copy" FileChange command for missing or changed files.
    private void CopyMissingOrChangedFiles(string filter)
    {
      var folders = mSourcePath.Split('\\');
      var sourceStartFolder = folders[folders.Length - 1];

      var sourceSpecs = Directory.GetFiles(mSourcePath, filter
        , SearchOption.AllDirectories);
      foreach (var sourceSpec in sourceSpecs)
      {
        var targetSpec = GetToSpec(mTargetPath, sourceSpec, sourceStartFolder);
        var filePath = Path.GetDirectoryName(targetSpec);
        if (false == Directory.Exists(filePath)
          || HasExtraDots(targetSpec, filter))
        {
          // Skip file for target folders that do not exist.
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

    // Creates a "Delete" FileChange command for files not in the target.
    private void DeleteTargetMissingFiles(string filter)
    {
      var folders = mTargetPath.Split('\\');
      var targetStartFolder = folders[folders.Length - 1];

      var targetSpecs = Directory.GetFiles(mTargetPath, filter
        , SearchOption.AllDirectories);
      foreach (var targetSpec in targetSpecs)
      {
        var sourceSpec = GetToSpec(mSourcePath, targetSpec, targetStartFolder);
        if (false == File.Exists(sourceSpec))
        {
          // Target file is not found in source path.
          FileChange fileChange = new FileChange("Delete", targetSpec);
          AppendChangeFile(fileChange);
        }
      }
    }

    // Create a 'to' file spec using the toFilePath and adding the folders and
    // file name using the fromFileSpec starting after the fromStartFolder.
    private string GetToSpec(string toFilePath
      , string fromFileSpec, string fromStartFolder)
    {
      var retValue = toFilePath;
      var fromFolders = fromFileSpec.Split('\\');
      for (int index = fromFolders.Length - 1; index >= 0; index--)
      {
        if (fromFolders[index].ToLower() == fromStartFolder.ToLower())
        {
          // Skip fromStartFolder and add remaining folders.
          for (int index1 = index + 1; index1 < fromFolders.Length - 1
            ; index1++)
          {
            var fromFolder = fromFolders[index1].Trim();
            if (NetString.HasValue(fromFolder))
            {
              retValue = Path.Combine(retValue, fromFolder);
            }
          }
          break;
        }
      }
      var fromFileName = Path.GetFileName(fromFileSpec);
      retValue = Path.Combine(retValue, fromFileName);
      return retValue;
    }

    // Checks if a file has more than one dot.
    private bool HasExtraDots(string fileSpec, string filter)
    {
      bool retValue = false;

      var fileName = Path.GetFileName(fileSpec);
      if (fileName.IndexOf(".") < fileName.Length - filter.Length
        && filter != ".config")
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Class Data

    private readonly string mSourcePath;
    private readonly string mTargetPath;
    private readonly string mChangeFileSpec;
    private readonly string mMultiFilter;
    #endregion
  }
}
