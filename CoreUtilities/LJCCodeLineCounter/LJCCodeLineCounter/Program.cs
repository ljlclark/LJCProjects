// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.IO;

namespace LJCCodeLineCounter
{
  // The program entry point class.
  /// <include path = 'items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
  public class Program
  {
    // The program entry point function.
    private static void Main(string[] args)
    {
      // Test Setting: Set to false for release.
      bool testing = false;
      if (testing)
      {
        args = TestArgs.GetTestArgs(isHtml: false);
      }

      if (args.Length < 1)
      {
        Console.WriteLine("Syntax: LJCCodeLineCounter rootFolderPath [filePattern] [findString]");
        Console.WriteLine("Press any key to continue . . .");
        Console.ReadKey();
      }
      else
      {
        string pattern;
        if (args.Length < 2)
        {
          pattern = "*.cs";
        }
        else
        {
          pattern = args[1];
        }

        string folderPathName = args[0].Substring(args[0].LastIndexOf("\\") + 1);
        if (args.Length > 2
          && args[2] != null)
        {
          Console.WriteLine("FindProcessor");
          FindProcessor findProcessor = new FindProcessor
          {
            FindStrings = args[2].Split(',')
          };
          foreach (string findString in findProcessor.FindStrings)
          {
            Console.WriteLine(findString);
          }
          findProcessor.ProcessFolder(args[0], folderPathName, pattern);
        }
        else
        {
          File.WriteAllText("LargeFiles.txt", "");
          CountProcessor countProcessor = new CountProcessor();
          long totalLineCount = 0;
          countProcessor.ProcessFolder(args[0], folderPathName, pattern, ref totalLineCount);

          Console.WriteLine($"Files < 100 Lines {countProcessor.TotalLevelZero}");
          Console.WriteLine($"Files 100 to 299 Lines {countProcessor.TotalLevelOne}");
          Console.WriteLine($"Files 300 to 499 Lines {countProcessor.TotalLevelTwo}");
          Console.WriteLine($"Files 500 to 999 Lines {countProcessor.TotalLevelThree}");
          Console.WriteLine($"Files 1000 to 1999 Lines {countProcessor.TotalLevelFour}");
          Console.WriteLine($"Files > 2000 Lines {countProcessor.TotalLevelFive}");
          Console.WriteLine();
          Console.WriteLine($"Total Files: {countProcessor.TotalFiles}");
          Console.WriteLine($"Total Line Count: {totalLineCount}");
        }
        if (testing)
        {
          Console.WriteLine("Press any key to continue . . .");
          Console.ReadKey();
        }
      }
    }
  }
}
