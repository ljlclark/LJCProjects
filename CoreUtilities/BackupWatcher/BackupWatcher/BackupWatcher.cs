﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Backup Watcher.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;

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
        NotifyFilters.CreationTime
        //| NotifyFilters.DirectoryName
        | NotifyFilters.FileName
        //| NotifyFilters.LastAccess
        | NotifyFilters.LastWrite
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
      for (int i = 0; i < retValue.Length; i++)
      {
        var ext = Path.GetExtension(retValue[i]);
        retValue[i] = ext.ToLower();
      }
      return retValue;
    }

    // Checks if the ChangeFile has a matching line.
    private bool HasText(string changeType, string fileSpec
      , string toFileSpec = null)
    {
      bool retValue = false;

      if (File.Exists(ChangeFile))
      {
        IEnumerable<string> lines = File.ReadLines(ChangeFile);
        int lineNumber = 0;
        foreach (string line in lines)
        {
          lineNumber++;
          var text = $"{changeType},{fileSpec}";
          if (NetString.HasValue(toFileSpec))
          {
            text += $",{toFileSpec}";
          }
          if (line.Contains(text))
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Checks if the file is allowed.
    private bool IsAllowed(string fileSpec)
    {
      bool retValue = true;
      var ext = Path.GetExtension(fileSpec);
      if (".tmp" == ext.ToLower())
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
        foreach (var extValue in mExtValues)
        {
          if (ext.ToLower() == extValue)
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Writes valid changes to the ChangeFile.
    private void WriteChange(string changeType, string fileSpec
      , string toFileSpec = null)
    {
      if (IsWatched(fileSpec)
        || IsWatched(toFileSpec))
      {
        // Attempt to prevent double entries.
        if (false == HasText(changeType, fileSpec, toFileSpec))
        {
          string text = "";
          text += $"{changeType},{fileSpec}";
          if (NetString.HasValue(toFileSpec))
          {
            text += $",{toFileSpec}";
          }
          text += "\r\n";
          File.AppendAllText(ChangeFile, text);
          Console.Write(text);
        }
      }
    }
    #endregion

    #region Event Handlers

    // Handles the Changed event.
    private void Watcher_Changed(object sender, FileSystemEventArgs e)
    {
      if (e.ChangeType == WatcherChangeTypes.Changed)
      {
        mFileName = Path.GetFileName(e.FullPath);
        if ("" == mFileName) { };
        if (IsAllowed(e.FullPath))
        {
          WriteChange("Copy", e.FullPath);
        }
      }
    }

    // Handles the Created event.
    private void Watcher_Created(object sender, FileSystemEventArgs e)
    {
      // For debugging.
      mFileName = Path.GetFileName(e.FullPath);
      if (IsAllowed(e.FullPath))
      {
        WriteChange("Copy", e.FullPath);
      }
    }

    // Handles the Deleted event.
    private void Watcher_Deleted(object sender, FileSystemEventArgs e)
    {
      // For debugging.
      mFileName = Path.GetFileName(e.FullPath);
      if (IsAllowed(e.FullPath))
      {
        WriteChange("Delete", e.FullPath);
      }
    }

    // Handles the Error event.
    private void Watcher_Error(object sender, ErrorEventArgs e)
    {
      throw new NotImplementedException();
    }

    // Handles the Renamed event.
    private void Watcher_Renamed(object sender, RenamedEventArgs e)
    {
      // For debugging.
      mFileName = Path.GetFileName(e.FullPath);

      if (false == IsWatched(e.OldFullPath))
      {
        if (IsAllowed(e.FullPath))
        {
          WriteChange("Copy", e.FullPath);
        }
      }
      else
      {
        if (IsAllowed(e.FullPath))
        {
          WriteChange("Rename", e.OldFullPath, e.FullPath);
        }
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

    private string[] mExtValues;
    private string mFileName;
    private readonly FileSystemWatcher mWatcher;
  }
}
