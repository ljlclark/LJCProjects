// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProgramDepencencyAction.cs
using LJCNetCommon;
using ProjectFilesDAL;
using System;
using System.IO;

namespace DependencyAction
{
  // The program entry point class.
  internal class ProgramDependencyAction
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      GetArgs(args, out string currentSpec, out string action);

      // Initialize Library ojbects.
      var data = new ProjectFilesData();
      var dataLib = new DataProjectFiles(data);

      // Get the ProjectParentKey which is the key to the Solution.
      var projectFile = dataLib.ProjectFileValues(currentSpec);
      var parentKey = dataLib.ProjectParentKey(projectFile.SourceCodeLine
        , projectFile.SourceCodeGroup, projectFile.SourceSolution);

      // Execute the Solution dependencies action.
      dataLib.SolutionDependencies(parentKey, action);
    }

    // Debug output function.
    private static void Debug(string text)
    {
      var root = @"C:\Users\Les\Documents\Visual Studio 2022";
      var spec = Path.Combine(root, @"LJCProjectsDev");
      spec = Path.Combine(spec, @"CoreUtilities");
      spec = Path.Combine(spec, @"UpdateProjectFiles");
      spec = Path.Combine(spec, @"debug.txt");
      File.AppendAllText(spec, $"{text}\r\n");
    }

    // Checks the args and gets the action value.
    private static void GetArgs(string[] args, out string currentSpec
      , out string action)
    {
      currentSpec = "";
      action = "Copy";

      bool hasError = false;
      if (args.Length < 1
        || args.Length > 2)
      {
        hasError = true;
      }

      if (!hasError)
      {
        if (args.Length >= 1)
        {
          currentSpec = args[0].Trim();
          var extension = Path.GetExtension(currentSpec);
          if (!NetString.HasValue(extension))
          {
            int pos = currentSpec.LastIndexOf("|");
            if (-1 == pos
              || pos != currentSpec.Length - 1)
            {
              // Need this so Split('\') works correctly
              // in dataLib.ProjectFileValues(.
              currentSpec += @"\";
            }
          }
        }

        if (2 == args.Length)
        {
          action = args[1].ToLower().Trim();
          if (action.CompareTo("delete") != 0
            && action.CompareTo("copy") != 0)
          {
            hasError = true;
          }
        }
      }

      if (hasError)
      {
        var syntax = "Syntax: SolutionDependencies currentSpec"
          + " {Delete|(Copy)}";
        Console.WriteLine(syntax);
      }
    }
  }
}
