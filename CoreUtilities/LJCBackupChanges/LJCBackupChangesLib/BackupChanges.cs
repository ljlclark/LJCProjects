// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupChanges.cs
using LJCBackupCommonLib;
using LJCNetCommon;
using System.IO;

namespace LJCBackupChangesLib
{
  /// <summary>The Backup Changes class.</summary>
  /// <include path='items/BackupChanges/*'
  ///   file='Doc/ProjectBackupChanges.xml'/>
  public class BackupChanges
  {
    // Initializes an object instance.
    /// <include path='items/BackupChangesC/*' file='Doc/BackupChanges.xml'/>
    public BackupChanges(string startFolder, string changeFilespec)
    {
      mStartFolder = startFolder;
      mChangeFilespec = changeFilespec;
    }

    #region Methods

    // Applies the change commands.
    /// <include path='items/Run/*' file='Doc/BackupChanges.xml'/>
    public void Run(string targetRoot)
    {
      if (File.Exists(mChangeFilespec))
      {
        string log = "BackupLog.txt";
        File.WriteAllText(log, "");
        TargetRoot = targetRoot;
        var fileChange = new FileChange(null, null, null);
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
          string targetFilespec = null;
          if (fileChange.ChangeType.ToLower() != "delete")
          {
            targetFilespec = GetMatchFolderFilespec(filespec, mStartFolder);
          }
          string toFileName = null;
          if (NetString.HasValue(fileChange.ToFileSpec))
          {
            toFileName = Path.GetFileName(fileChange.ToFileSpec);
          }

          switch (fileChange.ChangeType.ToLower())
          {
            case "copy":
              if (File.Exists(filespec))
              {
                NetFile.CreateFolder(targetFilespec);
                File.Copy(filespec, targetFilespec, true);
                File.AppendAllText(log, $"copy {filespec}\r\n");
                File.AppendAllText(log, $" - {targetFilespec}\r\n");
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
                var targetToFilespec = Path.Combine(TargetRoot, toFileName);
                File.Move(targetFilespec, targetToFilespec);
                File.AppendAllText(log, $"ren {targetFilespec}\r\n");
                File.AppendAllText(log, $" - {targetToFilespec}\r\n");
              }
              break;
          }
        }
      }
    }

    // Creates the Target Filespec.
    /// <include path='items/GetMatchFolderFilespec/*'
    ///   file='Doc/BackupChanges.xml'/>
    public string GetMatchFolderFilespec(string sourceFilespec
      , string startFolder)
    {
      string retValue = null;

      if (File.Exists(sourceFilespec))
      {
        retValue = TargetRoot;
        var filePath = Path.GetDirectoryName(sourceFilespec);
        var folders = filePath.Split('\\');
        for (int index = folders.Length - 1; index >= 0; index--)
        {
          if (folders[index].ToLower() == startFolder.ToLower())
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
      return retValue;
    }
    #endregion

    /// <summary>Gets or sets the TargetRoot value.</summary>
    public string TargetRoot { get; set; }

    private readonly string mChangeFilespec;
    private readonly string mStartFolder;
  }
}
