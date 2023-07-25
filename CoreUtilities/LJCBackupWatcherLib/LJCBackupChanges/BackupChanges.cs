// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupChanges.cs
using LJCBackupWatcherLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCBackupChanges
{
  /// <summary>The Backup Changes class.</summary>
  public class BackupChanges
  {
    // Initializes an object instance.
    /// <include path='items/BackupChangesC/*' file='Doc/BackupChanges.xml'/>
    public BackupChanges(string startFolder, string changeFile)
    {
      mStartFolder = startFolder;
      mChangeFile = changeFile;
    }

    #region Methods

    // Applies the change commands.
    /// <include path='items/Apply/*' file='Doc/BackupChanges.xml'/>
    public void Apply(string targetPath)
    {
      if (File.Exists(mChangeFile))
      {
        mTargetPath = targetPath;
        var fileChange = new FileChange(null, null, null);
        var lines = File.ReadAllLines(mChangeFile);
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

          string fileSpec = fileChange.FileSpec;
          string targetFileSpec = GetTargetFileSpec(fileSpec);
          string toFileName = null;
          if (NetString.HasValue(fileChange.ToFileSpec))
          {
            toFileName = Path.GetFileName(fileChange.ToFileSpec);
          }

          switch (fileChange.ChangeType)
          {
            case "Copy":
              if (File.Exists(fileSpec))
              {
                NetFile.CreateFolder(targetFileSpec);
                File.Copy(fileSpec, targetFileSpec, true);
                Console.WriteLine($"copy {fileSpec} {targetFileSpec}\r\n");
              }
              break;

            case "Delete":
              if (File.Exists(targetFileSpec))
              {
                File.Delete(targetFileSpec);
                Console.WriteLine($"del {fileSpec}\r\n");
              }
              break;

            case "Rename":
              if (File.Exists(targetFileSpec))
              {
                var targetToFileSpec = Path.Combine(mTargetPath, toFileName);
                File.Move(targetFileSpec, targetToFileSpec);
                Console.WriteLine($"ren {targetFileSpec} {targetToFileSpec}\r\n");
              }
              break;
          }
        }
      }
    }

    // Creates the Target FileSpec.
    private string GetTargetFileSpec(string fileSpec)
    {
      string retValue = null;

      if (File.Exists(fileSpec))
      {
        retValue = mTargetPath;
        var filePath = Path.GetDirectoryName(fileSpec);
        var folders = filePath.Split('\\');
        for (int index = folders.Length - 1; index >= 0; index--)
        {
          if (folders[index].ToLower() == mStartFolder.ToLower())
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

    private readonly string mChangeFile;
    private readonly string mStartFolder;
    private string mTargetPath;
  }
}
