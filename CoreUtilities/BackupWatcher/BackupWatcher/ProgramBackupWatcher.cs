// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramBackupWatcher.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BackupWatcher
{
  // The program entry point class.
  internal class Program
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      GetDefaults(out string watchPath, out string changeFile
        , out string multiFilter);
      if (GetArgs(args, ref watchPath, ref changeFile, ref multiFilter))
      {
        var _ = new BackupWatcher(watchPath, changeFile)
        {
          MultiFilter = multiFilter
        };

        Console.WriteLine("Press ENTER to exit.");
        Console.ReadLine();
      }
    }

    #region Private Functions

    // Gets the command line parameters.
    private static bool GetArgs(string[] args, ref string watchPath
      , ref string changeFile, ref string multiFilter)
    {
      bool retValue = true;
      if (args.Length >= 1)
      {
        watchPath = args[0];
      }
      if (args.Length >= 2)
      {
        changeFile = args[1];
      }
      if (args.Length >= 3)
      {
        multiFilter = args[2];
      }
      if (false == NetString.HasValue(watchPath)
        || false == NetString.HasValue(changeFile)
        || false == NetString.HasValue(multiFilter))
      {
        retValue = false;
        var syntax = "Syntax: ProgramBackupWatcher \"watchPath\"";
        syntax += "\"changeFile\" \"multiFilter\"";
        Console.WriteLine(syntax);
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }

    // Gets the default parameters.
    private static void GetDefaults(out string watchPath
      , out string changeFile, out string multiFilter)
    {
      watchPath = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev";
      changeFile = @"ChangeFile.txt";
      multiFilter = @"*.cs, *.cproj, *.sln, *.config, *.cmd, Doc\*.xml, -ChangeFile.txt, *.txt";

      var fileSpec = "BackupWatcherDefaults.txt";
      if (File.Exists(fileSpec))
      {
        IEnumerable<string> lines = File.ReadLines(fileSpec);
        foreach (string line in lines)
        {
          var tokens = line.Split(',');
          if (tokens.Length < 2)
          {
            continue;
          }
          switch (tokens[0].ToLower())
          {
            case "watchpath":
              watchPath = tokens[1].Trim();
              break;

            case "changefile":
              changeFile = tokens[1].Trim();
              break;

            case "multifilter":
              multiFilter = tokens[1].Trim();
              break;
          }
        }
      }
    }
    #endregion
  }
}
