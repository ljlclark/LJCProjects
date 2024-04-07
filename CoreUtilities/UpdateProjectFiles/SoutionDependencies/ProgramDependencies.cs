// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProgramDepencencies.cs
using LJCNetCommon;
using System;
using System.IO;

namespace SolutionDependencies
{
  // The program entry point class.
  internal class ProgramDependencies
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      var action = GetAction(args);

      var fileName = "SolutionDependencies.exe.config";
      var settings = new AppSettings(fileName);
      var actionPath = settings.GetString("DependencyActionPath");

      // Get the ProjectFile object for the current folder..
      var currentFolder = $"\"{Environment.CurrentDirectory}\"";
      var arguments = $"{currentFolder} {action}";
      var actionFileSpec = Path.Combine(actionPath, "DependencyAction.exe");
      var error = NetFile.ShellProgram(actionFileSpec, arguments);
      if (NetString.HasValue(error))
      {
        Console.WriteLine(error);
        Console.ReadKey();
      }
      else
      {
        Console.WriteLine($"Solution Project Dependencies \"{action}\" is Complete");
      }
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
    private static string GetAction(string[] args)
    {
      var retValue = "Copy";

      bool hasError = false;
      if (args.Length > 1)
      {
        hasError = true;
      }

      if (!hasError
        && 1 == args.Length)
      {
        retValue = args[0].ToLower(); ;
        if (retValue.CompareTo("delete") != 0
          && retValue.CompareTo("copy") != 0)
        {
          hasError = true;
        }
      }

      if (hasError)
      {
        var syntax = "Syntax: SolutionDependencies fileSpec"
          + " {Delete|(Copy)}";
        Console.WriteLine(syntax);
        Environment.Exit(1);
      }
      return retValue;
    }
  }
}
