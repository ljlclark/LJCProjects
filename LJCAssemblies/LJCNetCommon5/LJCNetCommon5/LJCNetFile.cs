// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCNetFile.cs
using System.Diagnostics;

namespace LJCNetCommon5
{
  // Contains common file related static functions. (R)
  /// <include file='Doc/LJCNetFile.xml'
  ///  path='items/LJCNetFile/*'/>
  public class LJCNetFile
  {
    #region Static Methods

    // Creates a Folder Path if it does not already exist.
    /// <include file='Doc/LJCNetFile.xml'
    ///  path='items/CreateFolder/*'/>
    public static void CreateFolder(string path)
    {
      if (LJC.HasText(path))
      {
        var makePath = Path.GetDirectoryName(path);
        if (LJC.HasText(makePath)
          && !Directory.Exists(makePath))
        {
          Directory.CreateDirectory(makePath);
        }
      }
    }

    // Returns the relative path.
    /// <include file='Doc/LJCNetFile.xml'
    ///  path='items/GetRelativePath/*'/>
    public static string GetRelativePath(string fromPath, string toFileSpec)
    {
      string retValue;

      // Folders must end in a slash
      if (!fromPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
      {
        fromPath += Path.DirectorySeparatorChar;
      }

      Uri fromUri = new(fromPath);
      Uri toUri = new(toFileSpec);
      Uri relativeUri = fromUri.MakeRelativeUri(toUri);
      retValue = Uri.UnescapeDataString(relativeUri.ToString());
      retValue = retValue.Replace('/', Path.DirectorySeparatorChar);
      return retValue;
    }

    // Get all text lines from a file.
    /// <include file='Doc/LJCNetFile.xml'
    ///  path='items/ReadAllLines/*'/>
    public static string[] ReadAllLines(string fileSpec)
    {
      string errorText;
      string[] retValue;

      if (!LJC.HasText(fileSpec))
      {
        errorText = "Missing file specification.\r\n";
        throw new ArgumentException(errorText);
      }
      else
      {
        if (!File.Exists(fileSpec))
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

    // Executes an external program.
    /// <include file='Doc/LJCNetFile.xml'
    ///   path='items/ShellProgram/*'/>
    public static string? ShellProgram(string programFileSpec
      , string? arguments = null)
    {
      string? retValue = null;

      bool success = true;
      if (LJC.HasText(programFileSpec))
      {
        if (!File.Exists(programFileSpec))
        {
          success = false;
          retValue = $"The File '{programFileSpec}'\r\n was not found.";
        }
      }

      ProcessStartInfo startInfo = null;
      if (success)
      {
        startInfo = new ProcessStartInfo()
        {
          Arguments = arguments,
          FileName = programFileSpec,
          UseShellExecute = true
        };

        // If no programFileSpec, then arguments must contain only a
        // file specification.
        if (null == programFileSpec)
        {
          if (!File.Exists(arguments))
          {
            success = false;
            retValue = $"The File '{arguments}'\r\n was not found.";
          }
          else
          {
            string filePath = Path.GetDirectoryName(arguments);
            string fileName = Path.GetFileName(arguments);
            startInfo = new ProcessStartInfo()
            {
              FileName = fileName,
              UseShellExecute = true,
              WorkingDirectory = filePath
            };
          }
        }
      }

      if (success)
      {
        if (startInfo != null)
        {
          Process.Start(startInfo);
        }
      }
      return retValue;
    }

    // Writes a string format to the Log file with the current date and time.
    /// <include file='Doc/LJCNetFile.xml'
    ///   path='items/WriteLog/*'/>
    public static void WriteLog(string logFileSpec, string? formatText = null
      , params object[] parameters)
    {
      string message;

      CreateFolder(logFileSpec);
      if (LJC.HasText(formatText))
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
    /// <include file='Doc/LJCNetFile.xml'
    ///   path='items/WriteLogLine/*'/>
    public static void WriteLogLine(string logFileSpec
      , string? formatText = null, params object[] parameters)
    {
      if (LJC.HasText(formatText))
      {
        formatText += "\r\n";
      }
      WriteLog(logFileSpec, formatText, parameters);
    }
    #endregion
  }
}
