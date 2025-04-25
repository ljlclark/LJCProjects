// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramCreateFileChanges.cs
using LJCCreateFileChangesLib;
using LJCNetCommon;
using System;
using System.IO;
using System.Linq;

namespace LJCCreateFileChanges
{
  // The program entry point class.
  internal class Program
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      GetDefaults(out string sourcePath, out string targetPath
        , out string changeFileSpec, out string multiFilter
        , out string skipFiles);
      if (!GetArgs(args, ref sourcePath, ref targetPath
        , ref changeFileSpec, ref multiFilter, ref skipFiles))
      {
        return;
      }
      AddDriveLetter(ref targetPath);
      if (HasTarget(targetPath))
      {
        var createFileChanges = new CreateFileChanges(sourcePath, targetPath
        , changeFileSpec, multiFilter);
        var skipList = skipFiles.Split('|').ToList<string>();
        createFileChanges.SkipFiles = skipList;
        createFileChanges.Run();
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
      if (!hasDrive)
      {
        targetPath = $"{driveLetter}:{targetPath}";
      }
    }

    // Gets the command line parameters.
    private static bool GetArgs(string[] args, ref string sourcePath
      , ref string targetPath, ref string changeFileSpec, ref string multiFilter
      , ref string skipFiles)
    {
      bool retValue = true;
      if (args.Length >= 1)
      {
        sourcePath = args[0];
      }
      if (args.Length >= 2)
      {
        targetPath = args[1];
      }
      if (args.Length >= 3)
      {
        changeFileSpec = args[2];
      }
      if (args.Length >= 4)
      {
        multiFilter = args[3];
      }
      if (args.Length >= 5)
      {
        skipFiles = args[4];
      }
      if (!NetString.HasValue(sourcePath)
        || !NetString.HasValue(targetPath)
        || !NetString.HasValue(changeFileSpec)
        || !NetString.HasValue(multiFilter))
      {
        retValue = false;
        var syntax = "Syntax: LJCCreateFileChanges \"sourcePath\"";
        syntax += "\"targetPath\" \"changeFileSpec\" \"multiFilter\"";
        syntax += "[\"skipFiles\"]";
        Console.WriteLine(syntax);
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }

    // Gets the default parameters.
    private static void GetDefaults(out string sourcePath
      , out string targetPath, out string changeFileSpec
      , out string multiFilter, out string skipFiles)
    {
      var mainPath = @"C:\Users\Les\Documents\Visual Studio 2022";
      sourcePath = $@"{mainPath}\LJCProjectsDev";
      targetPath = $@"{mainPath}\LJCProjects";
      changeFileSpec = $@"{mainPath}\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles\ChangeFile.txt";
      multiFilter = "*.cs|*.cproj|*.sln|*.config|*.cmd|*.txt";
      skipFiles = "ChangeFile.txt|BuildAll.cmd|ClearBuild.cmd|UpdateAll.cmd";

      // *** Next Line *** Change
      //var fileSpec = "CreateFileChangesDefaults.txt";
      var fileSpec = "CreateFileChangesDefaultsWeb.txt";
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
            case "sourcepath":
              sourcePath = tokens[1].Trim();
              break;

            case "targetpath":
              targetPath = tokens[1].Trim();
              break;

            case "changefilespec":
              changeFileSpec = tokens[1].Trim();
              break;

            case "multifilter":
              multiFilter = tokens[1].Trim();
              break;

            case "skipfiles":
              skipFiles = tokens[1].Trim();
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
