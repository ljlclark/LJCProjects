// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupChanges.cs
using LJCBackupCommonLib5;
using LJCNetCommon5;

// Contains code to apply the file changes.
// <include file="LJCBackupChanges.xml"
//  path="members/LJCBackupChangesLib5/*"/>
// Assembly: LJCBackupChangesLib5

namespace LJCBackupChangesLib5
{
  // The Backup Changes class.
  /// <include file='Doc/LJCBackupChanges.xml'
  ///  path='members/LJCBackupChanges/*'/>
  public class LJCBackupChanges
  {
    #region Static Methods

    // Creates the Target Filespec.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/CreateTargetFilespec/*'/>
    public static string? CreateTargetFilespec(string sourceFilespec
      , string sourceCodeline, string targetPath)
    {
      string? retValue = null;

      if (File.Exists(sourceFilespec))
      {
        retValue = targetPath;
        var filePath = Path.GetDirectoryName(sourceFilespec);
        if (filePath != null)
        {
          var folders = filePath.Split('\\');
          for (int index = folders.Length - 1; index >= 0; index--)
          {
            var folder = folders[index];
            if (folder.Equals(sourceCodeline, LJC.IgnoreCase))
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

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/Constructor/*'/>
    public LJCBackupChanges()
    {
      mChangeFilespec = "";
      mSourceCodeLine = "";
      TargetPath = "";
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/ConstructorParams/*'/>
    public LJCBackupChanges(string sourceCodeLine, string changeFilespec)
      : this()
    {
      mSourceCodeLine = sourceCodeLine;
      mChangeFilespec = changeFilespec;
    }
    #endregion

    #region Methods

    // Applies the change commands.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/Run/*'/>
    public void Run(string targetPath)
    {
      if (File.Exists(mChangeFilespec))
      {
        string log = "BackupLog.txt";
        File.WriteAllText(log, "");
        TargetPath = targetPath;
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
          fileChange.Filespec = tokens[1];
          if (tokens.Length > 2)
          {
            fileChange.ToFilespec = tokens[2];
          }

          string changeSpec = fileChange.Filespec;
          string? targetFilespec = null;
          var changeType = fileChange.ChangeType;
          if (!changeType.Equals("delete", LJC.IgnoreCase))
          {
            targetFilespec = CreateTargetFilespec(changeSpec, mSourceCodeLine
                , TargetPath);
          }
          string? toFileName = null;
          if (LJC.HasText(fileChange.ToFilespec))
          {
            toFileName = Path.GetFileName(fileChange.ToFilespec);
          }

          switch (fileChange.ChangeType.ToLower())
          {
            case "copy":
              if (File.Exists(changeSpec))
              {
                if (LJC.HasText(targetFilespec))
                {
                  var path = Path.GetDirectoryName(targetFilespec);
                  if (LJC.HasText(path))
                  {
                    // Indicate that the path is a folder, not a file.
                    path += @"\";
                    LJCNetFile.CreateFolder(path);
                  }
                  File.Copy(changeSpec, targetFilespec, true);
                  File.AppendAllText(log, $"copy {changeSpec}\r\n");
                  File.AppendAllText(log, $" - {targetFilespec}\r\n");
                }
              }
              break;

            case "delete":
              if (File.Exists(changeSpec))
              {
                File.Delete(changeSpec);
                File.AppendAllText(log, $"del {changeSpec}\r\n");
              }
              break;

            case "rename":
              if (File.Exists(targetFilespec))
              {
                if (LJC.HasText(toFileName))
                {
                  var targetToFilespec = Path.Combine(TargetPath, toFileName);
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
    #endregion

    #region Properties

    // Gets or sets the TargetPath value.
    /// <include file='Doc/LJCBackupChanges.xml'
    ///  path='members/TargetPath/*'/>
    public string TargetPath { get; set; }
    #endregion

    private readonly string mChangeFilespec;
    private readonly string mSourceCodeLine;
  }
}
