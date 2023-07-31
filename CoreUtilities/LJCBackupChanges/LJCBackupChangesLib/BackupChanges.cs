// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupChanges.cs
using LJCBackupWatcherLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCBackupChangesLib
{
  /// <summary>The Backup Changes class.</summary>
  public class BackupChanges
  {
    // Initializes an object instance.
    /// <include path='items/BackupChangesC/*' file='Doc/BackupChanges.xml'/>
    public BackupChanges(string startFolder, string changeFileSpec)
    {
      mStartFolder = startFolder;
      mChangeFileSpec = changeFileSpec;
    }

    #region Methods

    // Applies the change commands.
    /// <include path='items/Apply/*' file='Doc/BackupChanges.xml'/>
    public void Run(string targetPath)
    {
      if (File.Exists(mChangeFileSpec))
      {
        string log = "BackupLog.txt";
        File.WriteAllText(log, "");
        TargetPath = targetPath;
        var fileChange = new FileChange(null, null, null);
        var lines = File.ReadAllLines(mChangeFileSpec);
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

          // Testing
          //var fileName = Path.GetFileName(fileChange.FileSpec);
          //if ("BackupChanges.cs" == fileName)
          //{
          //  int i = 0;
          //}
          string fileSpec = fileChange.FileSpec;
          string targetFileSpec = null;
          if (fileChange.ChangeType.ToLower() != "delete")
          {
            targetFileSpec = GetMatchFolderFileSpec(fileSpec, mStartFolder);
          }
          string toFileName = null;
          if (NetString.HasValue(fileChange.ToFileSpec))
          {
            toFileName = Path.GetFileName(fileChange.ToFileSpec);
          }

          switch (fileChange.ChangeType.ToLower())
          {
            case "copy":
              if (File.Exists(fileSpec))
              {
                NetFile.CreateFolder(targetFileSpec);
                File.Copy(fileSpec, targetFileSpec, true);
                File.AppendAllText(log, $"copy {fileSpec}\r\n");
                File.AppendAllText(log, $" - {targetFileSpec}\r\n");
              }
              break;

            case "delete":
              if (File.Exists(fileSpec))
              {
                File.Delete(fileSpec);
                File.AppendAllText(log, $"del {fileSpec}\r\n");
              }
              break;

            case "rename":
              if (File.Exists(targetFileSpec))
              {
                var targetToFileSpec = Path.Combine(TargetPath, toFileName);
                File.Move(targetFileSpec, targetToFileSpec);
                File.AppendAllText(log, $"ren {targetFileSpec}\r\n");
                File.AppendAllText(log, $" - {targetToFileSpec}\r\n");
              }
              break;
          }
        }
      }
    }

    // Creates the Target FileSpec.
    /// <summary>
    /// Creates the Target FileSpec.
    /// </summary>
    /// <param name="fileSpec"></param>
    /// <param name="startFolder">The start folder of the matching path.</param>
    /// <returns></returns>
    public string GetMatchFolderFileSpec(string fileSpec, string startFolder)
    {
      string retValue = null;

      if (File.Exists(fileSpec))
      {
        retValue = TargetPath;
        var filePath = Path.GetDirectoryName(fileSpec);
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
        var targetFile = Path.GetFileName(fileSpec);
        retValue = Path.Combine(retValue, targetFile);
      }
      return retValue;
    }
    #endregion

    /// <summary></summary>
    public string TargetPath { get; set; }

    private readonly string mChangeFileSpec;
    private readonly string mStartFolder;
  }
}
