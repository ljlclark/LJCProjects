// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;

namespace LJCCodeLineCounter
{
  ///<summary>Performs "Find" processing on text files.</summary>
  public class CountProcessor
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CountProcessor()
    {
      TotalFiles = 0;
    }
    #endregion

    #region Public Methods

    // Processes the files and sub-folders in this folder.
    /// <include path='items/ProcessFolder/*' file='Doc/LineProcessor.xml'/>
    public void ProcessFolder(string folderPath, string folderPathName
      , string pattern, ref long totalLineCount)
    {
      long folderTotalLineCount = 0;

      string folderName = folderPath.Substring(folderPath.LastIndexOf("\\") + 1);
      if (CodeCommon.IsValidFolder(folderName))
      {
        // Process the files in this folder.
        string[] filePaths = Directory.GetFiles(folderPath, pattern);
        if (filePaths.Length > 0)
        {
          Console.WriteLine($"Folder: {folderPathName}");
          int fileCount = 0; int levelZero = 0; int levelOne = 0; int levelTwo = 0;
          int levelThree = 0; int levelFour = 0; int levelFive = 0;
          for (int fileIndex = 0; fileIndex < filePaths.Length; fileIndex++)
          {
            if (CodeCommon.IsValidFile(filePaths[fileIndex]))
            {
              fileCount++;
              folderTotalLineCount += ProcessFile(filePaths[fileIndex], out int lineCountLevel);

              switch (lineCountLevel)
              {
                case 0:
                  levelZero++;
                  break;
                case 1:
                  levelOne++;
                  break;
                case 2:
                  levelTwo++;
                  break;
                case 3:
                  levelThree++;
                  break;
                case 4:
                  levelFour++;
                  break;
                case 5:
                  levelFive++;
                  break;
              }
            }
          }
          TotalFiles += fileCount;
          TotalLevelZero += levelZero;
          TotalLevelOne += levelOne;
          TotalLevelTwo += levelTwo;
          TotalLevelThree += levelThree;
          TotalLevelFour += levelFour;
          TotalLevelFive += levelFive;

          Console.WriteLine($"File Count: {fileCount}");
          Console.WriteLine($"Folder Lines Total: {folderTotalLineCount}");
          Console.WriteLine();
          totalLineCount += folderTotalLineCount;
        }

        // Recursively process child folders.
        string[] folderPaths = Directory.GetDirectories(folderPath);
        if (folderPaths.Length > 0)
        {
          for (int folderIndex = 0; folderIndex < folderPaths.Length; folderIndex++)
          {
            folderName = folderPaths[folderIndex].Substring(folderPaths[folderIndex].LastIndexOf("\\") + 1);
            string newFolderPathName = Path.Combine(folderPathName, folderName);
            ProcessFolder(folderPaths[folderIndex], newFolderPathName, pattern
              , ref totalLineCount);
          }
        }
      }
    }
    #endregion

    #region Private Methods

    // Processes a file.
    private long ProcessFile(string filePath, out int lineCountLevel)
    {
      long ncStatementCount = 0;
      string inputFilePath = filePath;

      lineCountLevel = 0;
      string fileName = Path.GetFileName(filePath);

      IEnumerable<string> lines = File.ReadLines(inputFilePath);
      int lineNumber = 0;
      foreach (string line in lines)
      {
        // Count statements.
        lineNumber++;
        if (false == line.Trim().StartsWith("/")
          && false == line.Trim().StartsWith(",")
          && false == line.Trim().StartsWith("&")
          && false == line.Trim().StartsWith("|")
          && false == line.Trim().StartsWith("=")
          && false == line.Trim().StartsWith("*")
          && false == line.Trim().StartsWith("+")
          && false == line.Trim().StartsWith("-"))
        {
          ncStatementCount++;
        }
      }

      string leader = "";
      lineCountLevel = 0;
      if (ncStatementCount > 99 && ncStatementCount < 300)
      {
        lineCountLevel = 1;
        leader += " -*";
      }
      if (ncStatementCount > 299 && ncStatementCount < 500)
      {
        lineCountLevel = 2;
        leader += "*";
      }
      if (ncStatementCount > 499 && ncStatementCount < 1000)
      {
        lineCountLevel = 3;
        leader += "*";
      }
      if (ncStatementCount > 999 && ncStatementCount < 2000)
      {
        File.AppendAllText("LargeFiles.txt", $"{fileName} - {ncStatementCount}"
          + $" {leader}\r\n");
        lineCountLevel = 4;
        leader += "*";
      }
      if (ncStatementCount > 1999)
      {
        File.AppendAllText("LargeFiles.txt", $"{fileName} - {ncStatementCount}"
          + $" {leader}\r\n");
        lineCountLevel = 5;
        //leader += "*";
      }

      //Console.WriteLine($"{fileName} - {ncStatementCount} {leader}");
      Console.WriteLine($"{fileName} - {ncStatementCount}");
      return ncStatementCount;
    }
    #endregion

    #region Public Properties

    /// <summary>The total files with less than 100 lines.</summary>
    public int TotalLevelZero;

    /// <summary>The total files with greater than 100 lines.</summary>
    public int TotalLevelOne;

    /// <summary>The total files with greater than 300 lines.</summary>
    public int TotalLevelTwo;

    /// <summary>The total files with greater than 500 lines.</summary>
    public int TotalLevelThree;

    /// <summary>The total files with greater than 1000 lines.</summary>
    public int TotalLevelFour;

    /// <summary>The total files with greater than 2000 lines.</summary>
    public int TotalLevelFive;

    /// <summary>The total files processed.</summary>
    public long TotalFiles;
    #endregion
  }
}
