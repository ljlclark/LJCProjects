// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;
using LJCNetCommon;

namespace LJCCodeLineCounter
{
  /// <summary>The Find processor.</summary>
  public class FindProcessor
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public FindProcessor()
    {
      mTokenizer = new CodeTokenizer();
      mTokenizer.InitializeKeywords();
    }
    #endregion

    #region Public Methods

    // Processes the files and sub-folders in this folder.
    /// <include path='items/ProcessFolder/*' file='Doc/FindProcessor.xml'/>
    public void ProcessFolder(string folderPath, string folderPathName
      , string pattern)
    {
      mFolderPathName = folderPathName;
      mShowFolder = true;

      string folderName = folderPath.Substring(folderPath.LastIndexOf("\\") + 1);
      if (CodeCommon.IsValidFolder(folderName))
      {
        // Process the files in this folder.
        string[] filePaths = Directory.GetFiles(folderPath, pattern);
        if (filePaths.Length > 0)
        {
          for (int fileIndex = 0; fileIndex < filePaths.Length; fileIndex++)
          {
            if (CodeCommon.IsValidFile(filePaths[fileIndex]))
            {
              ProcessFile(filePaths[fileIndex]);
            }
          }
        }

        // Recursively process child folders.
        string[] folderPaths = Directory.GetDirectories(folderPath);
        if (folderPaths.Length > 0)
        {
          for (int folderIndex = 0; folderIndex < folderPaths.Length; folderIndex++)
          {
            folderName = folderPaths[folderIndex].Substring(folderPaths[folderIndex].LastIndexOf("\\") + 1);
            string newFolderPathName = Path.Combine(folderPathName, folderName);
            ProcessFolder(folderPaths[folderIndex], newFolderPathName, pattern);
          }
        }
      }
    }
    #endregion

    #region Private Methods

    // Processes a file.
    private void ProcessFile(string filePath)
    {
      mFileName = Path.GetFileName(filePath);
      mShowFile = true;

      if (FindStrings != null && FindStrings.Length > 0)
      {
        IEnumerable<string> lines = File.ReadLines(filePath);
        int lineNumber = 0;
        foreach (string line in lines)
        {
          if (IsMethod(line))
          {
            mShowMethod = true;
          }
          lineNumber++;
          ShowText(line, lineNumber);
        }
      }
    }

    // Outputs the found values.
    private void ShowText(string line, int lineNumber)
    {
      if (FindStrings != null)
      {
        foreach (string findValue in FindStrings)
        {
          string value = findValue.Trim();
          //if (line.Contains(value))
          if (line.IndexOf(value
            , StringComparison.InvariantCultureIgnoreCase) > -1)
          {
            if (mShowFolder)
            {
              mShowFolder = false;
              Console.WriteLine($"\r\nFolder: {mFolderPathName}");
              Console.WriteLine("****************************************");
            }

            if (mShowFile)
            {
              mShowFile = false;
              Console.WriteLine($"\r\n *** {mFileName}");
            }

            if (mShowMethod)
            {
              mShowMethod = false;
              Console.WriteLine($"\r\nMethod:{mMethodText.Trim()}");
            }

            Console.WriteLine($"Line: {lineNumber}, {line.Trim()}");
          }
        }
      }
    }

    // Determines if line if a method definition.
    private bool IsMethod(string line)
    {
      string token;
      bool retValue = false;

      mTokenizer.SetTokens(line);
      short tokenIndex = -1;
      token = mTokenizer.GetNextToken(ref tokenIndex);

      while (token != null)
      {
        // Skip comment lines.
        if (mTokenizer.IsCodeXmlComment(token)
          || mTokenizer.IsComment(token)
          || token.StartsWith("#"))
        {
          break;
        }

        if (mTokenizer.IsDelimiters(token))
        {
          break;
        }

        if ("+-*/".IndexOf(token) > -1)
        {
          break;
        }

        if ("&&" == token
          || "||" == token)
        {
          break;
        }

        if (token != null
          && token.Contains("("))
        {
          break;
        }

        // Check for one or two modifiers.
        if (mTokenizer.IsModifier(token))
        {
          token = mTokenizer.GetNextToken(ref tokenIndex);
          if (mTokenizer.IsModifier(token))
          {
            token = mTokenizer.GetNextToken(ref tokenIndex);
          }
          else
          {
            if (token != null
              && token.Contains("("))
            {
              // Constructor
              retValue = true;
              mMethodText = token;
              break;
            }
          }
        }

        // Check for Reference Type identifier.
        if (mTokenizer.IsRefType(token))
        {
          break;
        }

        if (mTokenizer.IsKeyword(token))
        {
          break;
        }

        //// Check for return data type.
        if (mTokenizer.IsDataType(token))
        {
          token = mTokenizer.GetNextToken(ref tokenIndex);
        }
        else
        {
          if (tokenIndex < mTokenizer.Tokens.Length - 1)
          {
            token = mTokenizer.GetNextToken(ref tokenIndex);
          }
          else
          {
            break;
          }
        }

        if (token != null
          && token.Contains("("))
        {
          retValue = true;
          mMethodText = token;
          break;
        }
        break;
      }
      return retValue;
    }
    #endregion

    #region Public Properties

    /// <summary>Gets or sets the "Find" strings.</summary>
    public string[] FindStrings { get; set; }
    #endregion

    #region Class Data

    private string mFolderPathName;
    private bool mShowFolder;
    private string mFileName;
    private bool mShowFile;
    private bool mShowMethod;
    private string mMethodText;
    private readonly CodeTokenizer mTokenizer;
    #endregion
  }
}
