// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupWatcher.cs
using LJCNetCommon;
using System.IO;
using System.Linq;

namespace LJCBackupWatcherLib
{
  /// <summary>The Backup Watcher class.</summary>
  public class BackupWatcher
  {
    // Initializes an object instance.
    /// <include path='items/BackupWatcherC/*' file='Doc/BackupWatcher.xml'/>
    public BackupWatcher(string watchPath, string changeFile, string multiFilter)
    {
      MultiFilter = multiFilter;
      WatchPath = watchPath;
      ChangeFile = changeFile;

      mWatcher = new FileSystemWatcher(watchPath)
      {
        NotifyFilter =
        //NotifyFilters.Attributes
        //NotifyFilters.CreationTime
        //| NotifyFilters.DirectoryName
        NotifyFilters.FileName
        //| NotifyFilters.LastAccess
        //| NotifyFilters.LastWrite
        //| NotifyFilters.Security
        | NotifyFilters.Size,
        Filter = "*.*",
        IncludeSubdirectories = true,
        EnableRaisingEvents = true
      };

      mWatcher.Changed += Watcher_Changed;
      mWatcher.Created += Watcher_Created;
      mWatcher.Deleted += Watcher_Deleted;
      mWatcher.Error += Watcher_Error;
      mWatcher.Renamed += Watcher_Renamed;
    }

    #region Private Methods

    // Creates the ExtValues array.
    private string[] CreateExtValues(string multiFilters)
    {
      var retValue = multiFilters.Split(',');
      for (int index = 0; index < retValue.Length; index++)
      {
        var ext = retValue[index].Trim();
        if (false == ext.StartsWith("-"))
        {
          ext = Path.GetExtension(ext);
        }
        retValue[index] = ext.ToLower();
      }
      return retValue;
    }

    // Checks if files are valid.
    private bool IsAllowed(FileChange change)
    {
      bool retValue = false;
      if (IsWatched(change.FileSpec))
      {
        retValue = true;
      }
      if (NetString.HasValue(change.ToFileSpec)
        && false == IsWatched(change.ToFileSpec))
      {
        retValue = false;
      }
      return retValue;
    }

    // Checks for  matching line.
    private bool IsMatchingLine(string line, string changeType
      , string fileSpec, string toFileSpec = null)
    {
      bool retValue = false;

      var change = new FileChange(changeType, fileSpec, toFileSpec);
      if (line == change.Text())
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the fileSpec is being watched.
    private bool IsWatched(string fileSpec)
    {
      bool retValue = false;

      if (NetString.HasValue(fileSpec))
      {
        var ext = Path.GetExtension(fileSpec);
        var fileName = Path.GetFileName(fileSpec);

        // Skip files with extra dots except for .config.
        var valid = true;
        if (fileName.IndexOf(".") < fileName.Length - ext.Length
          && ext != ".config")
        {
          valid = false;
        }

        if (valid)
        {
          foreach (var extValue in mExtValues)
          {
            // Skip specified files.
            if (extValue.StartsWith("-"))
            {
              if (fileName.ToLower() == extValue.Substring(1))
              {
                break;
              }
            }

            // Include specified extensions.
            if (ext.ToLower() == extValue)
            {
              retValue = true;
              break;
            }
          }
        }
      }
      return retValue;
    }

    // Checks if the ChangeFile has a matching line.
    private bool UpdateChangeFile(FileChange fileChange)
    {
      bool retValue = true;

      if (File.Exists(ChangeFile))
      {
        var lines = File.ReadAllLines(ChangeFile);
        for (int index = 0; index < lines.Count(); index++)
        {
          var line = lines[index];

          // File already has the change command.
          if (line == fileChange.Text())
          {
            retValue = false;
            break;
          }

          bool scrub = false;

          switch (fileChange.ChangeType.ToLower())
          {
            case "copy":
              // Remove previous redundant delete.
              if (IsMatchingLine(line, "Delete", fileChange.FileSpec
                , fileChange.ToFileSpec))
              {
                lines[index] = null;
                scrub = true;
              }
              break;

            case "delete":
              // Remove previous redundant copy.
              if (IsMatchingLine(line, "Copy", fileChange.FileSpec
                , fileChange.ToFileSpec))
              {
                lines[index] = null;
                scrub = true;
              }
              break;
          }
          if (scrub)
          {
            WriteChangeLines(lines);
          }
        }
      }
      return retValue;
    }

    // Writes valid changes to the ChangeFile.
    private void WriteChange(FileChange change)
    {
      if (IsAllowed(change))
      {
        if (UpdateChangeFile(change))
        {
          File.AppendAllText(ChangeFile, $"{change.Text()}\r\n");
        }
      }
    }

    // Writes lines that have a value.
    private void WriteChangeLines(string[] lines)
    {
      lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
      File.WriteAllLines(ChangeFile, lines);
    }
    #endregion

    #region Event Handlers

    // Handles the Changed event.
    private void Watcher_Changed(object sender, FileSystemEventArgs e)
    {
      if (e.ChangeType == WatcherChangeTypes.Changed)
      {
        var change = new FileChange("Copy", e.FullPath);
        WriteChange(change);
      }
    }

    // Handles the Created event.
    private void Watcher_Created(object sender, FileSystemEventArgs e)
    {
      var change = new FileChange("Copy", e.FullPath);
      WriteChange(change);
    }

    // Handles the Deleted event.
    private void Watcher_Deleted(object sender, FileSystemEventArgs e)
    {
      var change = new FileChange("Delete", e.FullPath);
      WriteChange(change);
    }

    // Handles the Error event.
    private void Watcher_Error(object sender, ErrorEventArgs e)
    {
      var exception = e.GetException();
      File.AppendAllText(Log, $"{exception.Message}\r\n");
    }

    // Handles the Renamed event.
    private void Watcher_Renamed(object sender, RenamedEventArgs e)
    {
      FileChange change;
      if (IsWatched(e.OldFullPath))
      {
        change = new FileChange("", e.OldFullPath);
        if (IsWatched(e.FullPath))
        {
          change.ChangeType = "Rename";
          change.ToFileSpec = e.FullPath;
          WriteChange(change);
        }
        else
        {
          // New file is not watched so just delete the old file.
          change.ChangeType = "Delete";
          WriteChange(change);
        }
      }
      else
      {
        // Old file is not watched so copy new.
        change = new FileChange("Copy", e.FullPath);
        WriteChange(change);
      }
    }
    #endregion

    #region Public Properties

    /// <summary>Gets or sets the Change file name.</summary>
    public string ChangeFile
    {
      get { return mChangeFile; }
      set { mChangeFile = NetString.InitString(value); }
    }
    private string mChangeFile;


    /// <summary>Gets or sets the Mulitiple filter value.</summary>
    public string MultiFilter
    {
      get { return mMultiFilter; }
      set
      {
        mMultiFilter = NetString.InitString(value);
        mExtValues = CreateExtValues(mMultiFilter);
      }
    }
    private string mMultiFilter;

    /// <summary>Gets or sets the Watch path.</summary>
    public string WatchPath
    {
      get { return mWatchPath; }
      set
      {
        mWatchPath = NetString.InitString(value);
        mExtValues = CreateExtValues(mMultiFilter);
      }
    }
    private string mWatchPath;
    #endregion

    #region Class Data

    private const string Log = "Watcher.log";

    private string[] mExtValues;
    private readonly FileSystemWatcher mWatcher;
    #endregion
  }
}
