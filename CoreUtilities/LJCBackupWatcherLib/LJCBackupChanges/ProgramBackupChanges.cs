// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramBackupChanges.cs
using LJCBackupChangesLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace LJCBackupChanges
{
  // The program entry point class.
  internal class Program
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      GetDefaults(out string targetPath, out string changeFile
        , out string startFolder);
      if (GetArgs(args, ref targetPath, ref changeFile, ref startFolder))
      {
        AddDriveLetter(ref targetPath);
        if (IsTarget(targetPath))
        {
          var backupChanges = new BackupChanges(startFolder, changeFile);
          backupChanges.Apply(targetPath);
        }
      }
    }

    #region Private Functions

    // Prompts for and adds the drive letter if not included.
    private static void AddDriveLetter(ref string targetPath)
    {
      var driveLetter = targetPath.Substring(0, 2);
      var hasDrive = false;
      if (':' == driveLetter[1])
      {
        hasDrive = true;
        driveLetter = driveLetter[0].ToString();
      }
      else
      {
        driveLetter = null;
      }
      while (null == driveLetter)
      {
        Console.Write("Enter the Target drive letter: ");
        driveLetter = Console.ReadLine();
        if (null == driveLetter || driveLetter.Length > 1)
        {
          driveLetter = null;
          Console.WriteLine("The drive letter must be a single character.");
        }
      }
      if (false == hasDrive)
      {
        targetPath = $"{driveLetter}:{targetPath}";
      }
    }

    // Gets the command line parameters.
    private static bool GetArgs(string[] args, ref string targetPath
      , ref string changeFile, ref string startFolder)
    {
      bool retValue = true;
      if (args.Length >= 1)
      {
        targetPath = args[0];
      }
      if (args.Length >= 2)
      {
        changeFile = args[1];
      }
      if (args.Length >= 3)
      {
        startFolder = args[2];
      }
      if (false == NetString.HasValue(targetPath)
        || false == NetString.HasValue(changeFile)
        || false == NetString.HasValue(startFolder))
      {
        retValue = false;
        var syntax = "Syntax: ProgramBackupChanges \"targetPath\"";
        syntax += "\"changeFile\" \"startFolder\"";
        Console.WriteLine(syntax);
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }

    // Gets the default parameters.
    private static void GetDefaults(out string targetPath
      , out string changeFile, out string startFolder)
    {
      targetPath = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDevBKP";
      changeFile = @"..\..\..\BackupWatcher\bin\Debug\ChangeFile.txt";
      startFolder = @"LJCProjectsDev";

      var fileSpec = "BackupChangesDefaults.txt";
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
            case "targetpath":
              targetPath = tokens[1].Trim();
              break;

            case "changefile":
              changeFile = tokens[1].Trim();
              break;

            case "startfolder":
              startFolder = tokens[1].Trim();
              break;
          }
        }
      }
    }

    // Checks the target path.
    private static bool IsTarget(string targetPath)
    {
      bool retValue = true;
      if (false == Directory.Exists(targetPath))
      {
        retValue = false;
        Console.WriteLine($"Target {targetPath} does not exist.");
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }
    #endregion
  }
}
