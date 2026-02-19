// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataGen.cs
using LJCDocDataDAL;
using LJCNetCommon;
using System.Diagnostics;
using System.IO;

namespace LJCDocDataGenLib
{
  // Provides methods to generate DocData XML files from a code file.
  /// <include path="members/LJCDocDataGen/*" file="Doc/LJCDocDataGen.xml"/>
  public class LJCDocDataGen
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataGen.xml"/>
    public LJCDocDataGen()
    {
      Comments = new LJCComments();
      DocDataFile = null;
      LibName = null;
      MethodName = null;
      PropertyName = null;
    }

    // Sets the GenCodeDoc config.
    /// <include path="members/SetConfig/*" file="Doc/LJCDocDataGen.xml"/>
    public void SetConfig(LJCGenDocConfig config)
    {
      GenDocConfig = config;
    }
    #endregion

    #region Public Methods

    // Creates and potentially writes the DocData XML.
    /// <include path="members/SerializeDocData/*" file="Doc/LJCDocDataGen.xml"/>
    public string SerializeDocData(string codeFileSpec)
    {
      string retXMLString;

      // Populate Library(File) XMLComment values.
      var fileName = Path.GetFileName(codeFileSpec);
      LibName = Path.GetFileNameWithoutExtension(codeFileSpec);
      Comments.LibName = LibName;
      DocDataFile = new LJCDocDataFile(LibName);

      retXMLString = ProcessCode(codeFileSpec);
      WriteLibDocXML(retXMLString, fileName, LibName);
      return retXMLString;
    }

    /// Generates the Doc data for the file.
    /// <include path="members/ProcessCode/*" file="Doc/LJCDocDataGen.xml"/>
    public string ProcessCode(string codeFileSpec)
    {
      string retXMLString = null;

      bool success = true;
      if (!File.Exists(codeFileSpec))
      {
        success = false;
      }

      string[] lines = null;
      if (success)
      {
        lines = File.ReadAllLines(codeFileSpec);
        if (null == lines)
        {
          success = false;
        }
      }

      if (success)
      {
        // Testing
        //DebugBreak("LJCDocDataGen.cs", codeFileSpec);

        if (lines.Length > 0)
        {
          foreach (var line in lines)
          {
            Line = line;

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

            // Process Class, Method or Property.
            ProcessItem(tokens);
          }
        }
      }

      if (DocDataFile != null)
      {
        retXMLString = NetCommon.XmlSerializeToString(typeof(LJCDocDataFile)
          , DocDataFile, null);
      }
      return retXMLString;
    }
    #endregion

    #region Private Processing Methods

    // Process XML Comment or Skip Null line and Comment Line.
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
            Comments.SetComment(trimLine, codeFileSpec);
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

    // Copy the Class XML comments into the DocData objects.
    private void ProcessClass(string className)
    {
      var classes = DocDataFile.Classes;
      if (null == classes)
      {
        classes = new LJCDocDataClasses();
        DocDataFile.Classes = classes;
      }
      var summary = Comments.Summary;
      var newClass = new LJCDocDataClass(className, summary);
      classes.Add(newClass);
      newClass.Syntax = Line.Trim();

      // Get Comment values.
      newClass.Code = Comments.Code;
      foreach (var group in Comments.Groups)
      {
        newClass.Groups.Add(group);
      }
      newClass.Remarks = Comments.Remarks;
      Comments.ClearComments();
    }

    // Processes the Class, Function or Property.
    private void ProcessItem(string[] tokens)
    {
      var parse = new LJCParse();
      var isFound = false;
      var className = parse.ClassName(tokens);
      if (NetString.HasValue(className))
      {
        isFound = true;
        ProcessClass(className);
      }
      if (!isFound
        && parse.IsMethod(tokens))
      {
        isFound = true;
        ProcessMethod();
      }
      if (!isFound
        && parse.IsProperty(tokens))
      {
        ProcessProperty();
      }
    }

    // Copy the Lib XML comments into the DocData objects.
    private void ProcessLib()
    {
      DocDataFile.Summary = Comments.Summary;
      DocDataFile.Remarks = Comments.Remarks;
      Comments.ClearComments();
    }

    // Copy the Method XML comments into the DocData objects.
    private void ProcessMethod()
    {
    }

    // Copy the Property XML comments into the DocData objects.
    private void ProcessProperty()
    {
    }
    #endregion

    #region Other Private Methods

    // Break on a file name.
    private void DebugBreak(string fileName, string fileSpec)
    {
      var nameOnly = Path.GetFileName(fileSpec);
      if (fileName == nameOnly)
      {
        Debugger.Break();
      }
    }

    // Creates a Lib DocData XML output file spec.
    private string LibGenXMLSpec(string codeFileSpec, string outputPath = null)
    {
      string retFilespec = null;

      if (null == outputPath)
      {
        outputPath = "../XMLDocData";
      }
      NetFile.CreateFolder(outputPath);
      var fileName = Path.GetFileNameWithoutExtension(codeFileSpec) + ".xml";
      retFilespec = $"{outputPath}/{fileName}";
      return retFilespec;
    }

    // Writes the LibGenXML file.
    private bool WriteLibDocXML(string libDocXML, string codeFileSpec
      , string fileName)
    {
      var retValue = false;

      var writeDocDataXML = GenDocConfig.WriteDocDataXML;
      // *** Begin *** Debug Output
      if ("" == fileName)
      {
        writeDocDataXML = true;
      }
      // *** End ***

      if (writeDocDataXML
        && libDocXML != null)
      {
        retValue = true;
        var docDataXMLPath = GenDocConfig.DocDataXMLPath;
        var fileSpec = LibGenXMLSpec(codeFileSpec, docDataXMLPath);
        File.WriteAllText(fileSpec, libDocXML);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The XML Comments object.</summary>
    private LJCComments Comments { get; set; }

    /// <summary>The DocDataFile object.</summary>
    private LJCDocDataFile DocDataFile { get; set; }

    /// <summary>The GenDoc configuration.</summary>
    private LJCGenDocConfig GenDocConfig { get; set; }

    /// <summary>The Lib name.</summary>
    private string LibName { get; set; }

    /// <summary>The current process line.</summary>
    private string Line { get; set; }

    /// <summary>The method name.</summary>
    private string MethodName { get; set; }

    /// <summary>The property name.</summary>
    private string PropertyName { get; set; }
    #endregion
  }
}
