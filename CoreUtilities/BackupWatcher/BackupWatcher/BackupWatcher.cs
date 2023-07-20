// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Backup Watcher.cs
using LJCNetCommon;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace BackupWatcher
{
  /// <summary>The Backup Watcher class.</summary>
  public class BackupWatcher
  {
    // Initializes an object instance.
    /// <include path='items/BackupWatcherC/*' file='Doc/BackupWatcher.xml'/>
    public BackupWatcher(string watchPath, string changeFile)
    {
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

    // Removes the matching line.
    private bool RemoveMatchingLine(ref string line, string changeType
      , string fileSpec, string toFileSpec = null)
    {
      bool retValue = false;

      var change = new FileChange(changeType, fileSpec, toFileSpec);
      if (line == change.Text())
      {
        retValue = true;
        line = null;
      }
      return retValue;
    }

    // Checks if the ChangeFile has a matching line.
    private bool UpdateChangeFile(FileChange change)
    {
      bool retValue = true;

      if (File.Exists(ChangeFile))
      {
        var lines = File.ReadAllLines(ChangeFile);
        for (int index = 0; index < lines.Count(); index++)
        {
          var line = lines[index];

          // File already has the change command.
          if (line == change.Text())
          {
            retValue = false;
            break;
          }

          bool scrub = false;

          switch (change.ChangeType.ToLower())
          {
            case "copy":
              // Remove previous redundant delete.
              if (RemoveMatchingLine(ref line, "Delete", change.FileSpec
                , change.ToFileSpec))
              {
                scrub = true;
              }
              break;

            case "delete":
              // Remove previous redundant copy.
              if (RemoveMatchingLine(ref line, "Copy", change.FileSpec
                , change.ToFileSpec))
              {
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
          File.AppendAllText(ChangeFile, change.Text());
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

    /// <summary>Gets or sets the ChangeFile name.</summary>
    public string ChangeFile { get; set; }

    /// <summary>Gets or sets the MulitFilter value.</summary>
    public string MultiFilter
    {
      get { return mMultiFilter; }
      set
      {
        mMultiFilter = value;
        mExtValues = CreateExtValues(mMultiFilter);
      }
    }
    private string mMultiFilter;

    /// <summary>Gets or sets the WatchPath value.</summary>
    public string WatchPath { get; set; }
    #endregion

    #region Class Data

    private const string Log = "Watcher.log";

    private string[] mExtValues;
    private readonly FileSystemWatcher mWatcher;
    #endregion
  }
}
