// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupChanges.cs
using LJCNetCommon5;

namespace LJCBackup5
{
  // The Backup Changes class.
  /// <include file='Doc/ProjectBackupChanges.xml'
  ///  path='members/BackupChanges/*'/>
  public class LJCBackupChanges
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/Constructor/*'/>
    public LJCBackupChanges()
    {
      mChangeFilespec = "";
      mStartFolder = "";
      TargetRoot = "";
    }

    // Initializes an object instance.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/ConstructorParam/*'/>
    public LJCBackupChanges(string startFolder, string changeFilespec)
      : this()
    {
      mStartFolder = startFolder;
      mChangeFilespec = changeFilespec;
    }
    #endregion

    #region Methods

    // Applies the change commands.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/Run/*'/>
    public void Run(string targetRoot)
    {
      if (File.Exists(mChangeFilespec))
      {
        string log = "BackupLog.txt";
        File.WriteAllText(log, "");
        TargetRoot = targetRoot;
        var fileChange = new LJCFileChange();
        var lines = File.ReadAllLines(mChangeFilespec);
        foreach (string line in lines)
        {
          var tokens = line.Split(',');
          if (tokens.Length < 2)
          {
            continue;
          }
          fileChange.ChangeType = tokens[0];
          fileChange.FileSpec = tokens[1];
          if (tokens.Length > 2)
          {
            fileChange.ToFileSpec = tokens[2];
          }

          string filespec = fileChange.FileSpec;
          string? targetFilespec = null;
          var changeType = fileChange.ChangeType;
          if (!changeType.Equals("delete", LJC.IgnoreCase))
          {
            targetFilespec = GetMatchFolderFilespec(filespec, mStartFolder);
          }
          string? toFileName = null;
          if (LJC.HasText(fileChange.ToFileSpec))
          {
            toFileName = Path.GetFileName(fileChange.ToFileSpec);
          }

          switch (fileChange.ChangeType.ToLower())
          {
            case "copy":
              if (File.Exists(filespec))
              {
                if (LJC.HasText(targetFilespec))
                {
                  LJCNetFile.CreateFolder(targetFilespec);
                  File.Copy(filespec, targetFilespec, true);
                  File.AppendAllText(log, $"copy {filespec}\r\n");
                  File.AppendAllText(log, $" - {targetFilespec}\r\n");
                }
              }
              break;

            case "delete":
              if (File.Exists(filespec))
              {
                File.Delete(filespec);
                File.AppendAllText(log, $"del {filespec}\r\n");
              }
              break;

            case "rename":
              if (File.Exists(targetFilespec))
              {
                if (LJC.HasText(toFileName))
                {
                  var targetToFilespec = Path.Combine(TargetRoot, toFileName);
                  File.Move(targetFilespec, targetToFilespec);
                  File.AppendAllText(log, $"ren {targetFilespec}\r\n");
                  File.AppendAllText(log, $" - {targetToFilespec}\r\n");
                }
              }
              break;
          }
        }
      }
    }

    // Creates the Target Filespec.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/GetMatchFolderFilespec/*'/>
    public string? GetMatchFolderFilespec(string sourceFilespec
      , string startFolder)
    {
      string? retValue = null;

      if (File.Exists(sourceFilespec))
      {
        retValue = TargetRoot;
        var filePath = Path.GetDirectoryName(sourceFilespec);
        if (filePath != null)
        {
          var folders = filePath.Split('\\');
          for (int index = folders.Length - 1; index >= 0; index--)
          {
            var folder = folders[index];
            if (folder.Equals(startFolder, LJC.IgnoreCase))
            {
              for (int index1 = index + 1; index1 < folders.Length; index1++)
              {
                retValue = Path.Combine(retValue, folders[index1]);
              }
              break;
            }
          }
          var targetFile = Path.GetFileName(sourceFilespec);
          retValue = Path.Combine(retValue, targetFile);
        }
      }
      return retValue;
    }
    #endregion

    // Gets or sets the TargetRoot value.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/TargetRoot/*'/>
    public string TargetRoot { get; set; }

    private readonly string mChangeFilespec;
    private readonly string mStartFolder;
  }
}
