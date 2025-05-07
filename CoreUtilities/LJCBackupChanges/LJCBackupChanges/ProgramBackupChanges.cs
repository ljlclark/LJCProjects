// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramBackupChanges.cs
using LJCBackupChangesLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCBackupChanges
{
  // The Backup File Changes program.
  /// <include path='items/Program/*'
  ///   file='Doc/ProgramBackupChanges.xml' />
  internal class Program
  {
    // The program entry point function.
    /// <include path='items/Main/*'
    ///   file='Doc/ProgramBackupChanges.xml' />
    internal static void Main(string[] args)
    {
      GetDefaults(out string targetRoot, out string changesFilespec
        , out string startFolder);
      if (!GetArgs(args, ref targetRoot, ref changesFilespec, ref startFolder))
      {
        return;
      }
      AddDriveLetter(ref targetRoot);
      if (HasTarget(targetRoot))
      {
        var backupChanges = new BackupChanges(startFolder, changesFilespec);
        backupChanges.Run(targetRoot);
      }
    }

    #region Private Functions

    // Prompts for and adds the drive letter if not included.
    private static void AddDriveLetter(ref string targetRoot)
    {
      var driveLetter = targetRoot.Substring(0, 2);
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
      if (!hasDrive)
      {
        targetRoot = $"{driveLetter}:{targetRoot}";
      }
    }

    // Gets the command line parameters.
    private static bool GetArgs(string[] args, ref string targetRoot
      , ref string changesFilespec, ref string startFolder)
    {
      bool retValue = true;
      if (args.Length >= 1)
      {
        targetRoot = args[0];
      }
      if (args.Length >= 2)
      {
        changesFilespec = args[1];
      }
      if (args.Length >= 3)
      {
        startFolder = args[2];
      }
      if (!NetString.HasValue(targetRoot)
        || !NetString.HasValue(changesFilespec)
        || !NetString.HasValue(startFolder))
      {
        retValue = false;
        var syntax = "Syntax: ProgramBackupChanges \"targetRoot\"";
        syntax += "\"changesFilespec\" \"startFolder\"";
        Console.WriteLine(syntax);
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }

    // Gets the default parameters.
    private static void GetDefaults(out string targetRoot
      , out string changesFilespec, out string startFolder)
    {
      string mainPath = @"C:\Users\Les\Documents\Visual Studio 2022";
      targetRoot = $@"{mainPath}\LJCProjects";
      string changesFilepath = @"\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles";
      changesFilespec = $@"{mainPath}\{changesFilepath}\ChangesFile.txt";
      startFolder = "LJCProjectsDev";

      //var fileSpec = "ChangesArgsLJCProject.txt";
      var fileSpec = "ChangesArgsWeb.txt";
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
            case "targetroot":
              targetRoot = tokens[1].Trim();
              break;

            case "changesfilespec":
              changesFilespec = tokens[1].Trim();
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
      if (!Directory.Exists(targetPath))
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
