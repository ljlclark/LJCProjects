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

      LJC.CheckArgument(toFilePath,nameof(toFilePath));
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

    // Checks if a file has more than one dot.
    private static bool HasExtraDots(string filespec, string filter)
    {
      bool retValue = false;

      var fileName = Path.GetFileName(filespec);

      // Has a dot before the extension dot.
      if (fileName.IndexOf('.') < fileName.Length - filter.Length
        && filter != ".config")
      {
        retValue = true;
      }
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
          // File already has the change command.
          //if (line.ToLower() == textLine.ToLower())
          if (LJC.Equals(line, textLine))
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

      mAlwaysSkipFolders = [];
      mChangeCommands = [];
      mChangesFilespec = "";
      mMissingFolders = [];
      mSourceRoot = "";
      mTargetRoot = "";
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/ConstructorParams/*'/>
    public LJCCreateFileChanges(string sourceRoot, string targetRoot
      , string changesFilespec) : this()
    {
      mSourceRoot = sourceRoot;
      mTargetRoot = targetRoot;
      mChangesFilespec = changesFilespec;

      var alwaysSkipFileSpec = "AlwaysSkipFolders.txt";
      if (File.Exists(alwaysSkipFileSpec))
      {
        var lines = File.ReadAllLines(alwaysSkipFileSpec);
        //mAlwaysSkipFolders = lines.ToList();
        mAlwaysSkipFolders = [.. lines];
      }
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
        DeleteTargetNoSourceFiles(filter);
        CopyMissingOrChangedFiles(filter);
      }

      LJCNetFile.CreateFolder(mChangesFilespec);
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
      text = $"Target Folders missing in:\r\n{mTargetRoot}\r\n\r\n";
      text += string.Join("\r\n", mMissingFolders);
      File.AppendAllText(missingFolders, text);
    }

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

    // Creates a "Copy" FileChange command for missing or changed files.
    private void CopyMissingOrChangedFiles(string filter)
    {
      // Get the source folder from end of a path.
      var sourceCodeLineFolder = FinalFolder(mSourceRoot);
      LJC.CheckArgument(sourceCodeLineFolder, nameof(sourceCodeLineFolder));

      var filterPath = GetFilterPath(ref filter);

      var sourceSpecs = Directory.GetFiles(mSourceRoot, filter
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
        var targetSpec = GetToSpec(mTargetRoot, sourceSpec, sourceCodeLineFolder!
          , out string codePath);

        // Skips common unpromoted folders and folders in AlwaysSkipFolders.txt.
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
          var fileChange = new LJCFileChange("Copy", sourceSpec);
          AppendChangeFile(fileChange);
        }
      }
    }

    // Creates a "Delete" FileChange command for target files not in the source.
    private void DeleteTargetNoSourceFiles(string filter)
    {
      // Get the target folder from end of a path.
      var targetCodeLineFolder = FinalFolder(mTargetRoot);
      LJC.CheckArgument(targetCodeLineFolder, nameof(targetCodeLineFolder));
      var filterPath = GetFilterPath(ref filter);

      // Get all target files by filter.
      var targetSpecs = Directory.GetFiles(mTargetRoot, filter
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
        var sourceSpec = GetToSpec(mSourceRoot, targetSpec, targetCodeLineFolder!
          , out string _);
        if (!File.Exists(sourceSpec))
        {
          // Target file is not found in source path.
          var fileChange = new LJCFileChange("Delete", targetSpec);
          AppendChangeFile(fileChange);
        }
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
      var retValue = false;

      LJC.CheckArgument(targetSpec, nameof(targetSpec));

      var targetPath = Path.GetDirectoryName(targetSpec);

      // Always skip listed folders.
      // ToDo: Delete target skipped folders?
      if (HasLine(mAlwaysSkipFolders, codePath))
      {
        retValue = true;
      }

      // Skip common unpromoted folders/files.
      if (!retValue
        && (targetPath!.Contains("\\.vs")
        || targetPath.Contains("\\obj\\")))
      {
        retValue = true;
      }
      if (!retValue)
      {
        var finalFolder = FinalFolder(targetPath!);
        if (LJC.HasText(finalFolder))
        {
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
      }

      // Skip updates to target folders that do not exist.
      if (!retValue
        && !IncludeMissingTargetFolders
        && !Directory.Exists(targetPath))
      {
        if (!retValue
          && !HasLine(mMissingFolders, codePath))
        {
          mMissingFolders.Add($"{codePath}");
        }
        // *** Next Statement *** Add
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the include filters.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/IncludeFilters/*'/>
    public List<string> IncludeFilters { get; set; }

    // Gets or sets the skip file list.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/SkipFiles/*'/>
    public List<string> SkipFiles { get; set; }

    // Gets or sets the include missing target folders flag.
    /// <include file='Doc/LJCCreateFileChanges.xml'
    ///  path='items/IncludeMissingTargetFolders/*'/>
    public bool IncludeMissingTargetFolders { get; set; }
    #endregion

    #region Class Data

    private readonly List<string> mAlwaysSkipFolders;
    private readonly List<string> mChangeCommands;
    private readonly string mChangesFilespec;
    private readonly List<string> mMissingFolders;
    private readonly string mSourceRoot;
    private readonly string mTargetRoot;
    #endregion
  }
}
