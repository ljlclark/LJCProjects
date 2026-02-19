// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataGen.cs

using LJCNetCommon;
using System.IO;

namespace LJCGenCodeDoc
{
  /// <summary>
  /// Provides methods to generate DocData XML files from a code file.
  /// </summary>
  public class LJCDocDataGen
  {
    #region Constructor Methods

    /// <summary>
    /// Sets the GenCodeDoc config.
    /// </summary>
    /// <param name="config">The GenCodeDocConfig object.</param>
    public void SetConfig(LJCGenCodeDocConfig config)
    {
      GenDocConfig = config;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Creates and potentially writes the DocData XML.
    /// </summary>
    /// <param name="codeFileSpec">The code filespec.</param>
    /// <returns>The DocData XML string.</returns>
    public string SerializeDocData(string codeFileSpec)
    {
      string retXMLString = null;

      LibName = Path.GetFileNameWithoutExtension(codeFileSpec);
      //Comments.LibName = LibName;

      return retXMLString;
    }

    /// <summary>
    /// Generates the Doc data for the file.
    /// </summary>
    /// <param name="codeFileSpec">The code filespec.</param>
    /// <returns>The DocData XML string.</returns>
    public string ProcessCode(string codeFileSpec)
    {
      string retXMLString = null;

      bool success = true;
      if (!File.Exists(codeFileSpec))
      {
        success = false;
      }
      if (success)
      {
        var lines = File.ReadAllLines(codeFileSpec);
        if (lines.Length > 0)
        {
          foreach (var line in lines)
          {
            // Process XML Comment, empty line and Comment Line.
            if (LineProcessed(line, codeFileSpec))
            {
              continue;
            }

            // Check for Class, Method or Property.
            var tokens = NetString.Split(line, " ");
            if (tokens.Length < 2)
            {
              continue;
            }
            ProcessItem(tokens);
          }
        }
      }

      //if (DocDataFile != null)
      //{
      //  retXMLString = DocDataFile.SerializeToString(null);
      //}
      return retXMLString;
    }

    /// <summary>
    /// Process XML Comment or Skip Null line and Comment Line.
    /// </summary>
    /// <param name="line">The code line.</param>
    /// <param name="codeFileSpec">The code filespec.</param>
    /// <returns>true if processed; otherwise false.</returns>
    private bool LineProcessed(string line, string codeFileSpec)
    {
      var retProcessed = false;

      // Process blank line.
      if (!NetString.HasValue(line))
      {
        retProcessed = true;
      }

      string trimLine = null;

      // Process XML comments.
      if (!retProcessed)
      {
        trimLine = line.Trim();
        if (trimLine.StartsWith("///"))
        {
          var tokens = NetString.Split(trimLine, " ");
          if (tokens.Length > 0)
          {
            if ("libname:" == tokens[1].ToLower())
            {
              ProcessLib();
              retProcessed = true;
            }
          }

          if (!retProcessed)
          {
            //Comments.SetComment(trimLine, codeFileSpec);
            retProcessed = true;
          }
        }
      }

      // Process comment.
      if (!retProcessed)
      {
        if (trimLine.StartsWith("//"))
        {
          retProcessed = true;
        }
      }
      return retProcessed;
    }

    /// <summary>
    // Processes the Class, Function or Property.
    /// </summary>
    /// <param name="tokens">The array of line tokens.</param>
    private void ProcessItem(string[] tokens)
    {
    }

    /// <summary>
    /// Copy the Lib XML comments into the DocData objects.
    /// </summary>
    private void ProcessLib()
    {
    }
    #endregion

    #region Properties

    /// <summary>
    /// The XML Comments object.
    /// </summary>
    //private LJCComments Comments;

    /// <summary>
    /// The DocDataFile object.
    /// </summary>
    //private LJCDocDataFile DocDataFile {get; set; }

    /// <summary>
    /// The GenDoc configuration.
    /// </summary>
    public LJCGenCodeDocConfig GenDocConfig { get; set; }

    // The Lib name.
    private string LibName { get; set; }
    #endregion
  }
}
