// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupChanges.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;

namespace BackupChanges
{
  /// <summary>The Backup Changes class.</summary>
  public class BackupChanges
  {
    // Initializes an object instance.
    /// <include path='items/BackupChangesC/*' file='Doc/BackupChanges.xml'/>
    public BackupChanges(string sourceFolder, string changeFile)
    {
      mSourceFolder = sourceFolder;
      mChangeFile = changeFile;
    }

    // Applies the change commands.
    /// <include path='items/Apply/*' file='Doc/BackupChanges.xml'/>
    public void Apply(string targetPath)
    {
      if (File.Exists(mChangeFile))
      {
        mTargetPath = targetPath;
        IEnumerable<string> lines = File.ReadLines(mChangeFile);
        foreach (string line in lines)
        {
          var tokens = line.Split(',');
          if (tokens.Length < 2)
          {
            continue;
          }

          var changeCommand = tokens[0];
          var fileSpec = tokens[1];
          string targetFileSpec = GetTargetFileSpec(mSourceFolder, fileSpec);

          string toFileName = null;
          if (tokens.Length > 2)
          {
            var toFileSpec = tokens[2];
            toFileName = Path.GetFileName(toFileSpec);
          }

          switch (changeCommand)
          {
            case "Copy":
              if (File.Exists(fileSpec))
              {
                NetFile.CreateFolder(targetFileSpec);
                File.Copy(fileSpec, targetFileSpec, true);
              }
              break;

            case "Delete":
              if (File.Exists(targetFileSpec))
              {
                File.Delete(targetFileSpec);
              }
              break;

            case "Rename":
              if (File.Exists(targetFileSpec))
              {
                var targetToFileSpec = Path.Combine(mTargetPath, toFileName);
                File.Copy(targetFileSpec, targetToFileSpec);
              }
              break;
          }
        }
      }
    }

    // Creates the Target FileSpec.
    private string GetTargetFileSpec(string sourceFolder, string fileSpec)
    {
      string retValue = null;

      if (File.Exists(fileSpec))
      {
        retValue = mTargetPath;
        var filePath = Path.GetDirectoryName(fileSpec);
        var folders = filePath.Split('\\');
        for (int index = folders.Length - 1; index >= 0; index--)
        {
          if (folders[index].ToLower() == sourceFolder.ToLower())
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

    private readonly string mChangeFile;
    private readonly string mSourceFolder;
    private string mTargetPath;
  }
}
