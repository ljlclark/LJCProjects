// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCreateFileChanges.cs
using LJCBackupCommonLib5;
using LJCNetCommon5;

namespace LJCCreateFileChangesLib5
{
  // The Create FileChanges class.
  /// <include file='Doc/LJCCreateFileChanges.xml'
  ///  path='items/LJCCreateFileChanges/*'/>
  public class LJCCreateFileChanges
  {
    #region Static Methods

    // Gets the final folder name.
    private static string? FinalFolder(string fileSpec)
    {
      string? retFolder = null;

      var path = fileSpec;
      var extension = Path.GetExtension(fileSpec);
      if (File.Exists(fileSpec)
        || LJC.HasText(extension))
      {
        // Strips everything from last folder separator "\".
        path = Path.GetDirectoryName(fileSpec);
      }
      if (LJC.HasText(path))
      {
        var folders = path.Split('\\');
        //retFolder = folders[folders.Length - 1];
        retFolder = folders[^1];
      }
      return retFolder;
    }

    // Returns the filter path if it exists and updates the filter.
    private static string? GetFilterPath(ref string filter)
    {
      var retValue = Path.GetDirectoryName(filter);
      if (LJC.HasText(retValue))
      {
        filter = Path.GetFileName(filter);
      }
      return retValue;
    }

    // Creates a "to" file spec.
    // Takes the toFilePath and adds the folders and file name from the
    // fromFileSpec starting after the fromStartFolder.
    // Example:
    // toFilePath = C:\CodeLineRoot\LJCProjectsDev
    // fromFileSpec = C:\CodeLineRoot\LJCProjects\ViewEditorList.cs
    // fromStartFolder = LJCProjects
    //                           |ToCodeline|                                  
    //   returns C:\CodeLineRoot\LJCProjectsDev\ViewEditorList.cs</pre>
    private static string GetToSpec(string toFilePath
      , string fromFilespec, string fromStartFolder, out string codePath)
    {
      var retValue = toFilePath;

      LJC.CheckArgument(toFilePath, nameof(toFilePath));
      LJC.CheckArgument(fromFilespec, nameof(fromFilespec));
      LJC.CheckArgument(fromStartFolder, nameof(fromStartFolder));

      codePath = "";
      var fromPath = Path.GetDirectoryName(fromFilespec);
      string[] fromFolders = [];
      if (LJC.HasText(fromPath))
      {
        fromFolders = fromPath.Split('\\');
      }

      for (int index = fromFolders.Length - 1; index >= 0; index--)
      {
        //if (fromFolders[index].ToLower() == fromStartFolder.ToLower())
        if (LJC.Equals(fromFolders[index], fromStartFolder))
        {
          // Skip fromStartFolder and add remaining folders.
          for (int index1 = index + 1; index1 < fromFolders.Length; index1++)
          {
            var fromFolder = fromFolders[index1].Trim();
            if (LJC.HasText(fromFolder))
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

    // Check for filter path and if file is in the filter path.
    private static bool HasFilterPath(string targetSpec, string filterPath)
    {
      bool retValue = false;

      LJC.CheckArgument(targetSpec, nameof(targetSpec));

      var targetPath = Path.GetDirectoryName(targetSpec);
      if (LJC.HasText(targetPath))
      {
        var targetFolders = targetPath?.Split('\\');
        //if (targetFolders.Length > 0)
        if (LJC.HasArrayElements(targetFolders))
        {
          //targetLastFolder = targetFolders[targetFolders.Length - 1];
          var targetLastFolder = targetFolders[^1];
          if (LJC.HasText(targetLastFolder))
          {
            if (LJC.HasText(filterPath)
              && (targetLastFolder == filterPath))
            {
              retValue = true;
            }
          }
        }
      }
      return retValue;
    }

    // Check if text file already has a text line.
    private static bool HasLine(List<string> list, string textLine)
    {
      bool retValue = false;

      if (LJC.HasText(textLine))
      {
        foreach (string line in list)
        {
          // File already has the line.
          if (LJC.Equals(line, textLine))
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Check if skip folder file has a code path.
    private static bool HasSkipCodePath(List<string> folderList, string codePath)
    {
      bool retValue = false;

      if (LJC.HasText(codePath))
      {
        foreach (string folder in folderList)
        {
          if (!codePath.EndsWith('\\'))
          {
            codePath += "\\";
          }
          if (codePath.StartsWith(folder))
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/Constructor/*'/>
    public LJCCreateFileChanges()
    {
      IncludeFilters = [];
      SkipFiles = [];
      SkipSubfolders = [];
      IncludeMissingTargetFolders = false;

      mSkipCodePathspec = "";
      mSkipCodePaths = [];
      mChangeCommands = [];
      mChangesFilespec = "";
      mMissingFolders = [];
      mSourcePath = "";
      mTargetPath = "";
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/ConstructorParams/*'/>
    public LJCCreateFileChanges(string sourcePath, string targetPath
      , string changesFilespec) : this()
    {
      mSourcePath = sourcePath;
      mTargetPath = targetPath;
      mChangesFilespec = changesFilespec;
    }
    #endregion

    #region Public Methods

    // Runs the create "Changes" file process.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/Run/*'/>
    public void Run()
    {
      foreach (var filter in IncludeFilters)
      {
        DeleteTargetNoSource(filter);
        CopyMissingOrChanged(filter);
      }

      WriteChanges();
      WriteMissingFolders();
    }

    // Creates a "Copy" FileChange command for missing or changed files.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/CopyMissingOrChanged/*'/>
    public void CopyMissingOrChanged(string filter)
    {
      // Get the source folder from end of a path.
      var sourceCodeLineFolder = FinalFolder(mSourcePath);
      LJC.CheckArgument(sourceCodeLineFolder, nameof(sourceCodeLineFolder));

      var filterPath = GetFilterPath(ref filter);

      var sourceSpecs = Directory.GetFiles(mSourcePath, filter
        , SearchOption.AllDirectories);
      foreach (var sourceSpec in sourceSpecs)
      {
        // If filter has path, skip file that does not end with the filter path.
        if (LJC.HasText(filterPath)
          && !HasFilterPath(sourceSpec, filterPath))
        {
          continue;
        }

        // Create the targetSpec using the mTargetRoot and adding the folders and
        // file name using the sourceSpec starting after the sourceCodeLineFolder.
        var targetSpec = GetToSpec(mTargetPath, sourceSpec, sourceCodeLineFolder!
          , out string codePath);

        // Skips common unpromoted folders and folders in AlwaysSkipFolders.txt.
        if (Skip(targetSpec, codePath))
        {
          continue;
        }

        if (!IncludeMissingTargetFolders
          && IsSkipFile(targetSpec))
        {
          if (File.Exists(targetSpec))
          {
            var fileChange = new LJCFileChange("Delete", targetSpec);
            AppendChangeFile(fileChange);
          }
          continue;
        }

        if (File.Exists(targetSpec))
        {
          CopyChangedFiles(sourceSpec, targetSpec);
        }
        else
        {
          // Copy missing file.
          var fileChange = new LJCFileChange("Copy", sourceSpec);
          AppendChangeFile(fileChange);
        }
      }
    }

    // Creates a "Delete" FileChange command for target files not in the source.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/DeleteTargetNoSource/*'/>
    public void DeleteTargetNoSource(string filter)
    {
      // Get the target folder from end of a path.
      var targetCodeLineFolder = FinalFolder(mTargetPath);
      LJC.CheckArgument(targetCodeLineFolder, nameof(targetCodeLineFolder));
      var filterPath = GetFilterPath(ref filter);

      // Get all target files by filter.
      var targetSpecs = Directory.GetFiles(mTargetPath, filter
        , SearchOption.AllDirectories);
      foreach (var targetSpec in targetSpecs)
      {
        // If filter has path, skip file that does not end with the filter path.
        if (LJC.HasText(filterPath)
          && !HasFilterPath(targetSpec, filterPath))
        {
          continue;
        }

        // Create the sourceSpec using the sourceRoot and adding the folders and
        // file name using the targetSpec starting after the targetCodeLineFolder.
        var sourceSpec = GetToSpec(mSourcePath, targetSpec, targetCodeLineFolder!
          , out string _);
        if (!File.Exists(sourceSpec))
        {
          // Target file is not found in source path.
          var fileChange = new LJCFileChange("Delete", targetSpec);
          AppendChangeFile(fileChange);
        }
      }
    }

    // Write the changes file.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/WriteChanges/*'/>
    public void WriteChanges()
    {
      LJCNetFile.CreateFolder(mChangesFilespec);
      if (File.Exists(mChangesFilespec))
      {
        File.Delete(mChangesFilespec);
      }
      var text = string.Join("\r\n", mChangeCommands);
      File.AppendAllText(mChangesFilespec, text);
    }

    // Write the missing folders file.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/WriteMissingFolders/*'/>
    public void WriteMissingFolders()
    {
      var missingFolders = @"RunFiles\MissingFolders.txt";
      if (File.Exists(missingFolders))
      {
        File.Delete(missingFolders);
      }
      var text = $"Target Folders missing in:\r\n{mTargetPath}\r\n\r\n";
      text += string.Join("\r\n", mMissingFolders);
      File.AppendAllText(missingFolders, text);
    }
    #endregion

    #region Private Methods

    // Appends new FileChange commands to the ChangeFile.
    private bool AppendChangeFile(LJCFileChange fileChange)
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
          if (!LJC.HasText(sourceLine)
            && !LJC.HasText(targetLine))
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
        var fileChange = new LJCFileChange("Copy", sourceSpec);
        AppendChangeFile(fileChange);
      }
    }

    // Checks if file is a skiped file.
    private bool IsSkipFile(string targetSpec)
    {
      bool retValue = false;

      var fileName = Path.GetFileName(targetSpec);
      foreach (string skipFile in SkipFiles)
      {
        //if (skipFile.ToLower() == fileName.ToLower())
        if (LJC.Equals(skipFile, fileName))
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Skips common unpromoted folders/files and missing target folders.
    private bool Skip(string targetSpec, string codePath)
    {
      var retSkip = false;

      LJC.CheckArgument(targetSpec, nameof(targetSpec));

      var targetPath = Path.GetDirectoryName(targetSpec);

      // Skip listed code path folders if not included.
      if (!IncludeMissingTargetFolders
        && HasSkipCodePath(mSkipCodePaths, codePath))
      {
        if (Directory.Exists(targetPath))
        {
          var fileChange = new LJCFileChange("DeleteFolder", targetPath);
          AppendChangeFile(fileChange);
        }
        retSkip = true;
      }

      // Skip common unpromoted folders.
      if (!retSkip)
      {
        foreach (string skipSubfolder in SkipSubfolders)
        {
          if (targetSpec!.Contains(skipSubfolder))
          {
            retSkip = true;
            break;
          }
        }
      }

      // Skip updates to target folders that do not exist if not included.
      if (!retSkip
        && !IncludeMissingTargetFolders
        && !Directory.Exists(targetPath))
      {
        // If not a skip codePath.
        if (!HasLine(mMissingFolders, codePath))
        {
          mMissingFolders.Add($"{codePath}");
        }
        retSkip = true;
      }
      return retSkip;
    }
    #endregion

    #region Properties

    // Gets or sets the include filters.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/IncludeFilters/*'/>
    public List<string> IncludeFilters { get; set; }

    // Gets or sets the skipped code path file.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/SkipCodePathspec/*'/>
    public string SkipCodePathspec
    {
      get { return mSkipCodePathspec; }
      set
      {
        if (LJC.HasText(value))
        {
          mSkipCodePathspec = value.Trim();
          if (File.Exists(mSkipCodePathspec))
          {
            var lines = File.ReadAllLines(mSkipCodePathspec);
            //mSkipCodePaths = lines.ToList();
            mSkipCodePaths = [.. lines];
          }
        }
      }
    }
    private string mSkipCodePathspec;

    // Gets or sets the skip file list.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/SkipFiles/*'/>
    public List<string> SkipFiles { get; set; }

    // Gets or sets the skip common folder list.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/SkipSubfolders/*'/>
    public List<string> SkipSubfolders { get; set; }

    // Gets or sets the include missing target folders flag.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/IncludeMissingTargetFolders/*'/>
    public bool IncludeMissingTargetFolders { get; set; }
    #endregion

    #region Class Data

    private List<string> mSkipCodePaths;

    private readonly List<string> mChangeCommands;
    private readonly string mChangesFilespec;
    private readonly List<string> mMissingFolders;
    private readonly string mSourcePath;
    private readonly string mTargetPath;
    #endregion
  }
}
