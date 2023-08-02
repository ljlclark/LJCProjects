// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateFileChanges.cs
using LJCBackupCommonLib;
using LJCNetCommon;
using System.Collections.Generic;
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
      SkipFiles = new List<string>();
    }
    #endregion

    #region Public Methods

    /// <summary>Runs the create FileChanges process.</summary>
    public void Run()
    {
      if (File.Exists(mChangeFileSpec))
      {
        File.Delete(mChangeFileSpec);
      }

      string[] filters = mMultiFilter.Split('|');
      foreach (var filter in filters)
      {
        DeleteTargetNoSourceFiles(filter);
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

      bool copy = false;
      if (sourceLines.Length != targetLines.Length)
      {
        copy = true;
      }
      if (false == copy)
      {
        for (int index = 0; index < sourceLines.Length; index++)
        {
          if (sourceLines[index] != targetLines[index]
            && false == sourceLines[index].StartsWith("<!-- Generated"))
          {
            copy = true;
            break;
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
      var folders = mSourcePath.Split('\\');
      var sourceStartFolder = folders[folders.Length - 1];
      var filterPath = GetFilterPath(ref filter);

      var sourceSpecs = Directory.GetFiles(mSourcePath, filter
        , SearchOption.AllDirectories);
      foreach (var sourceSpec in sourceSpecs)
      {
        // File spec does not end with the filter path.
        if (NetString.HasValue(filterPath)
          && false == HasFilterPath(sourceSpec, filterPath))
        {
          continue;
        }

        // Skip file for target folders that do not exist.
        var targetSpec = GetToSpec(mTargetPath, sourceSpec, sourceStartFolder);
        var filePath = Path.GetDirectoryName(targetSpec);
        if (false == Directory.Exists(filePath)
          || IsSkipFile(targetSpec)
          || HasExtraDots(targetSpec, filter))
        {
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
      var folders = mTargetPath.Split('\\');
      var targetStartFolder = folders[folders.Length - 1];
      var filterPath = GetFilterPath(ref filter);

      var targetSpecs = Directory.GetFiles(mTargetPath, filter
        , SearchOption.AllDirectories);
      foreach (var targetSpec in targetSpecs)
      {
        // File spec does not end with the filter path.
        if (NetString.HasValue(filterPath)
          && false == HasFilterPath(targetSpec, filterPath))
        {
          continue;
        }

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
      var fromPath = Path.GetDirectoryName(fromFileSpec);
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
            }
          }
          break;
        }
      }
      var fromFileName = Path.GetFileName(fromFileSpec);
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
    #endregion

    #region Properties

    /// <summary>The skip file list.</summary>
    public List<string> SkipFiles { get; set; }
    #endregion

    #region Class Data

    private readonly string mSourcePath;
    private readonly string mTargetPath;
    private readonly string mChangeFileSpec;
    private readonly string mMultiFilter;
    #endregion
  }
}
