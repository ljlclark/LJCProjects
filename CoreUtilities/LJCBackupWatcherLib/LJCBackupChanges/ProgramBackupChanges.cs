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
      GetDefaults(out string targetPath, out string changeFileSpec
        , out string startFolder);
      if (false == GetArgs(args, ref targetPath, ref changeFileSpec
        , ref startFolder))
      {
        return;
      }
      AddDriveLetter(ref targetPath);
      if (HasTarget(targetPath))
      {
        var backupChanges = new BackupChanges(startFolder, changeFileSpec);
        backupChanges.Apply(targetPath);
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
      , ref string changeFileSpec, ref string startFolder)
    {
      bool retValue = true;
      if (args.Length >= 1)
      {
        targetPath = args[0];
      }
      if (args.Length >= 2)
      {
        changeFileSpec = args[1];
      }
      if (args.Length >= 3)
      {
        startFolder = args[2];
      }
      if (false == NetString.HasValue(targetPath)
        || false == NetString.HasValue(changeFileSpec)
        || false == NetString.HasValue(startFolder))
      {
        retValue = false;
        var syntax = "Syntax: ProgramBackupChanges \"targetPath\"";
        syntax += "\"changeFileSpec\" \"startFolder\"";
        Console.WriteLine(syntax);
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }

    // Gets the default parameters.
    private static void GetDefaults(out string targetPath
      , out string changeFileSpec, out string startFolder)
    {
      string mainPath = @"C:\Users\Les\Documents\Visual Studio 2022";
      targetPath = $@"{mainPath}\LJCProjects_Stage";
      string changeFilePath = @"\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles";
      changeFileSpec = $@"{mainPath}\{changeFilePath}\ChangeFile.txt";
      startFolder = "LJCProjects_Stage";

      var fileSpec = "BackupChangesDefaults.txt";
      if (File.Exists(fileSpec))
      {
        string[] lines = File.ReadAllLines(fileSpec);
        foreach (string line in lines)
        {
          var tokens = line.Split(',');
          if (tokens.Length < 2)
          {
            continue;
          }
          switch (tokens[0].ToLower().Trim())
          {
            case "targetpath":
              targetPath = tokens[1].Trim();
              break;

            case "changefilespec":
              changeFileSpec = tokens[1].Trim();
              break;

            case "startfolder":
              startFolder = tokens[1].Trim();
              break;
          }
        }
      }
    }

    // Checks the target path.
    private static bool HasTarget(string targetPath)
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
