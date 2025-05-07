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
  // The Create File Changes program.
  /// <include path='items/Program/*'
  ///   file='Doc/ProgramCreateFileChanges.xml' />
  internal class Program
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      GetDefaults(out string sourceRoot, out string targetRoot
        , out string changesFilespec, out string includeFilter
        , out string skipFiles);
      if (!GetArgs(args, ref sourceRoot, ref targetRoot
        , ref changesFilespec, ref includeFilter, ref skipFiles))
      {
        return;
      }
      AddDriveLetter(ref targetRoot);
      if (HasTarget(targetRoot))
      {
        // Creates the Changes file (changeFileSpec).
        var createFileChanges = new CreateFileChanges(sourceRoot, targetRoot
        , changesFilespec, includeFilter);
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
    private static bool GetArgs(string[] args, ref string sourceRoot
      , ref string targetRoot, ref string changesFilespec, ref string includeFilter
      , ref string skipFiles)
    {
      bool retValue = true;
      if (args.Length >= 1)
      {
        sourceRoot = args[0];
      }
      if (args.Length >= 2)
      {
        targetRoot = args[1];
      }
      if (args.Length >= 3)
      {
        changesFilespec = args[2];
      }
      if (args.Length >= 4)
      {
        includeFilter = args[3];
      }
      if (args.Length >= 5)
      {
        skipFiles = args[4];
      }
      if (!NetString.HasValue(sourceRoot)
        || !NetString.HasValue(targetRoot)
        || !NetString.HasValue(changesFilespec)
        || !NetString.HasValue(includeFilter))
      {
        retValue = false;
        var syntax = "Syntax: LJCCreateFileChanges \"sourceRoot\"";
        syntax += "\"targetRoot\" \"changesFileSpec\" \"includeFilter\"";
        syntax += "[\"skipFiles\"]";
        Console.WriteLine(syntax);
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }

    // Gets the default parameters.
    private static void GetDefaults(out string sourceRoot
      , out string targetRoot, out string changesFilespec
      , out string includeFilter, out string skipFiles)
    {
      // Same defaults as in CreateFileChangesDefaults.txt.
      var mainPath = @"C:\Users\Les\Documents\Visual Studio 2022";
      sourceRoot = $@"{mainPath}\LJCProjectsDev";
      targetRoot = $@"{mainPath}\LJCProjects";
      changesFilespec = $@"{mainPath}\LJCProjectsDev\CoreUtilities";
      changesFilespec += @"\BackupWatcher\CmdFiles\ChangesLJCProjects.txt";
      includeFilter = @"*.cs|*.cproj|*.sln|*.config|*.cmd|*.txt|Doc\*.xml";
      skipFiles = "ChangeFile.txt|?Build*.cmd|ClearBuild.cmd|UpdateAll.cmd";

      // *** Next Line *** Change use HTML pages defaults.
      //var fileSpec = "ChangesArgsLJCProjects.txt";
      var fileSpec = "ChangesArgsLJCPHPProjects.txt";
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
            case "sourceroot":
              sourceRoot = tokens[1].Trim();
              break;

            case "targetroot":
              targetRoot = tokens[1].Trim();
              break;

            case "changesfilespec":
              changesFilespec = tokens[1].Trim();
              break;

            case "includefilter":
              includeFilter = tokens[1].Trim();
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
