// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramCreateFileChanges.cs
using LJCCreateFileChangesLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCCreateFileChanges
{
  // The program entry point class.
  internal class Program
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      GetDefaults(out string sourcePath, out string targetPath
        , out string changeFileSpec, out string multiFilter);
      if (false == GetArgs(args, ref sourcePath, ref targetPath
        , ref changeFileSpec, ref multiFilter))
      {
        return;
      }
      var createFileChanges = new CreateFileChanges(sourcePath, targetPath
        , changeFileSpec, multiFilter);
      createFileChanges.Start();
    }

    #region Private Functions

    // Gets the command line parameters.
    private static bool GetArgs(string[] args, ref string sourcePath
      , ref string targetPath, ref string changeFileSpec, ref string multiFilter)
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
      if (false == NetString.HasValue(sourcePath)
        || false == NetString.HasValue(targetPath)
        || false == NetString.HasValue(changeFileSpec)
        || false == NetString.HasValue(multiFilter))
      {
        retValue = false;
        var syntax = "Syntax: LJCCreateFileChanges \"sourcePath\"";
        syntax += "\"targetPath\" \"changeFileSpec\" \"multiFilter\"";
        Console.WriteLine(syntax);
        Console.Write("Press ENTER to exit. ");
        Console.ReadLine();
      }
      return retValue;
    }

    // Gets the default parameters.
    private static void GetDefaults(out string sourcePath
      , out string targetPath, out string changeFileSpec
      , out string multiFilter)
    {
      sourcePath = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev";
      targetPath = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjects_Stage";
      changeFileSpec = @"FileChanges.txt";
      //multiFilter = @"*.cs|*.cproj|*.sln|*.config|*.cmd|Doc\*.xml|-ChangeFile.txt|*.txt";
      multiFilter = @"*.cs|*.cproj|*.sln|*.config|*.cmd|-ChangeFile.txt|*.txt";

      var fileSpec = "CreateFileChangesDefaults.txt";
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
          switch (tokens[0].ToLower())
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
          }
        }
      }
    }
    #endregion
  }
}
