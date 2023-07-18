// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Backup Watcher.cs
using LJCNetCommon;
using System;
using System.IO;
using System.Linq;

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

    // Creates a change string.
    private string CreateChange(string changeType, string fileSpec
      , string toFileSpec = null)
    {
      var retValue = $"{changeType},{fileSpec}";
      if (NetString.HasValue(toFileSpec))
      {
        retValue += $",{toFileSpec}";
      }
      return retValue;
    }

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

    // Gets the change values from a change string.
    private void GetChangeValues(string line, out string changeType
      , out string fileSpec, out string toFileSpec)
    {
      changeType = null;
      fileSpec = null;
      toFileSpec = null;

      var tokens = line.Split(',');
      if (tokens.Count() > 1)
      {
        changeType = tokens[0];
        fileSpec = tokens[1];
      }
      if (tokens.Count() > 2)
      {
        toFileSpec = tokens[2];
      }
    }

    // Checks if files are valid.
    private bool IsAllowed(string fileSpec
      , string toFileSpec = null)
    {
      bool retValue = false;
      if (IsWatched(fileSpec))
      {
        retValue = true;
      }
      if (NetString.HasValue(toFileSpec)
      && false == IsWatched(toFileSpec))
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

    // Checks if the ChangeFile has a matching line.
    private void ScrubChangeFile(string changeType, string fileSpec
      , string toFileSpec = null)
    {
      if (File.Exists(ChangeFile))
      {
        string[] lines = File.ReadAllLines(ChangeFile);
        for (int index = 0; index < lines.Count(); index++)
        {
          var line = lines[index];

          RemoveMatchLine(lines, index, changeType, fileSpec, toFileSpec);
          GetChangeValues(line, out string lineChangeType
            , out string lineFileSpec, out string lineToFileSpec);
          switch (changeType.ToLower())
          {
            case "copy":
              RemoveMatchLine(lines, index, "Delete", fileSpec, toFileSpec);
              break;

            case "delete":
              RemoveMatchLine(lines, index, "Copy", fileSpec, toFileSpec);
              break;
          }
        }
        WriteLines(ChangeFile, lines);
      }
    }

    // Removes the matching line.
    private void RemoveMatchLine(string[] lines, int index, string changeType
      , string fileSpec, string toFileSpec = null)
    {
      var checkLine = CreateChange(changeType, fileSpec, toFileSpec);
      if (lines[index] == checkLine)
      {
        lines[index] = null;
      }
    }

    // Writes valid changes to the ChangeFile.
    private void WriteChange(string changeType, string fileSpec
      , string toFileSpec = null)
    {
      if (IsAllowed(fileSpec, toFileSpec))
      {
        ScrubChangeFile(changeType, fileSpec, toFileSpec);
        string text = CreateChange(changeType, fileSpec, toFileSpec);
        File.AppendAllText(ChangeFile, text);
      }
    }

    // Writes lines that have a value.
    private void WriteLines(string changeFile, string[] lines)
    {
      using (StreamWriter writer = new StreamWriter(changeFile))
      {
        foreach (string line in lines)
        {
          if (NetString.HasValue(line))
          {
            writer.WriteLine(line);
          }
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
        WriteChange("Copy", e.FullPath);
      }
    }

    // Handles the Created event.
    private void Watcher_Created(object sender, FileSystemEventArgs e)
    {
      WriteChange("Copy", e.FullPath);
    }

    // Handles the Deleted event.
    private void Watcher_Deleted(object sender, FileSystemEventArgs e)
    {
      WriteChange("Delete", e.FullPath);
    }

    // Handles the Error event.
    private void Watcher_Error(object sender, ErrorEventArgs e)
    {
      throw new NotImplementedException();
    }

    // Handles the Renamed event.
    private void Watcher_Renamed(object sender, RenamedEventArgs e)
    {
      if (IsWatched(e.OldFullPath))
      {
        WriteChange("Rename", e.OldFullPath, e.FullPath);
      }
      else
      {
        // Old file is not watched so copy new.
        WriteChange("Copy", e.FullPath);
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
    private readonly FileSystemWatcher mWatcher;
  }
}
