// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// NetFile.cs
using System;
using System.IO;

namespace LJCNetCommon
{
  // Contains common file related static functions. (R)
  /// <include path='items/NetFile/*' file='Doc/NetFile.xml'/>
  public class NetFile
  {
    #region Public Functions

    // Creates a Folder Path if it does not already exist. (E)
    /// <include path='items/CreateFolder/*' file='Doc/NetFile.xml'/>
    public static void CreateFolder(string path)
    {
      if (path.Contains("\\"))
      {
        path = Path.GetDirectoryName(path);
      }
      // Next Statement - Change - 1/22
      if (-1 == path.IndexOf(".")
        && false == Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
    }

    // Returns the relative path.
    /// <include path='items/GetRelativePath/*' file='Doc/NetFile.xml'/>
    public static string GetRelativePath(string fromPath, string toFileSpec)
    {
      string retValue;

      // Folders must end in a slash
      if (false == fromPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
      {
        fromPath += Path.DirectorySeparatorChar;
      }

      Uri fromUri = new Uri(fromPath);
      Uri toUri = new Uri(toFileSpec);
      Uri relativeUri = fromUri.MakeRelativeUri(toUri);
      retValue = Uri.UnescapeDataString(relativeUri.ToString());
      retValue = retValue.Replace('/', Path.DirectorySeparatorChar);
      return retValue;
    }

    // Get all text lines from a file.
    /// <include path='items/ReadAllLines/*' file='Doc/NetFile.xml'/>
    public static string[] ReadAllLines(string fileSpec)
    {
      string errorText;
      string[] retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        errorText = "Missing file specification.\r\n";
        throw new ArgumentException(errorText);
      }
      else
      {
        if (false == File.Exists(fileSpec))
        {
          errorText = $"File '{fileSpec}' was not found.\r\n";
          throw new FileNotFoundException(errorText);
        }
        else
        {
          retValue = File.ReadAllLines(fileSpec);
          if (null == retValue || 0 == retValue.Length)
          {
            errorText = $"No data found in file '{fileSpec}'.";
            throw new Exception(errorText);
          }
        }
      }
      return retValue;
    }

    // Writes a string format to the Log file with the current date and time.
    /// <include path='items/WriteLog/*' file='Doc/NetFile.xml'/>
    public static void WriteLog(string logFileSpec, string formatText = null
      , params object[] parameters)
    {
      string message;

      CreateFolder(logFileSpec);
      if (NetString.HasValue(formatText))
      {
        string text = string.Format(formatText, parameters);
        message = $"{DateTime.Now} - {text}";
      }
      else
      {
        message = "\r\n";
      }
      File.AppendAllText(logFileSpec, message);
    }

    // Writes string format + cr/lf to the Log file with current date and time.
    /// <include path='items/WriteLogLine/*' file='Doc/NetFile.xml'/>
    public static void WriteLogLine(string logFileSpec, string formatText = null
      , params object[] parameters)
    {
      if (NetString.HasValue(formatText))
      {
        formatText += "\r\n";
      }
      WriteLog(logFileSpec, formatText, parameters);
    }
    #endregion
  }
}
