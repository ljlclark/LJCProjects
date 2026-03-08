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

      Lines = null;
      if (success)
      {
        Lines = File.ReadAllLines(codeFileSpec);
        if (null == Lines)
        {
          success = false;
        }
      }

      if (success)
      {
        // Testing
        //FileNameBreak("LJCDocDataGen.cs", codeFileSpec);

        if (Lines.Length > 0)
        {
          LineIndex = 0;
          while (LineIndex < Lines.Length)
          {
            Line = Lines[LineIndex];
            // *** Begin *** Add
            NextLine = null;
            var nextIndex = LineIndex + 1;
            var maxIndex = Lines.Length - 1;
            if (nextIndex < maxIndex)
            {
              NextLine = Lines[LineIndex + 1];
            }
            // *** End ***
            LineIndex++;

            // Process XML Comment, empty line and Comment Line.
            if (LineProcessed(codeFileSpec))
            {
              continue;
            }

            // Check for Class, Method or Property.
            var tokens = NetString.Split(Line, " ");
            if (tokens.Length < 2)
            {
              continue;
            }

            // Process Class, Method or Property.
            ProcessItem();
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
    private bool LineProcessed(string codeFileSpec)
    {
      var retProcessed = false;

      // Process blank line.
      if (!NetString.HasValue(Line))
      {
        retProcessed = true;
      }

      string trimLine = null;

      // Process XML comments.
      if (!retProcessed)
      {
        trimLine = Line.Trim();
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
    private void ProcessClass()
    {
      if (NetString.HasValue(ClassName))
      {
        InitializeClasses();
        var classes = DocDataFile.Classes;
        var summary = Comments.Summary;
        var classItem = new LJCDocDataClass(ClassName, summary);
        classes.Add(classItem);
        classItem.Code = Comments.Code;
        foreach (var group in Comments.Groups)
        {
          InitializeGroups(classItem);
          classItem.Groups.Add(group);
        }
        classItem.Remarks = Comments.Remarks;
        classItem.Syntax = Line.Trim();
        Comments.ClearComments();
      }
    }

    // Processes the Class, Function or Property.
    private void ProcessItem()
    {
      var isFound = false;

      if (!isFound)
      {
        var codeParse = new LJCCSParser();
        var className = codeParse.ClassName(Line);
        if (NetString.HasValue(className))
        {
          isFound = true;
          ClassName = className;
          ProcessClass();
        }
      }

      if (!isFound)
      {
        var methodName = ProcessMethod();
        if (NetString.HasValue(methodName))
        {
          isFound = true;
        }
      }

      if (!isFound)
      {
        var propertyName = ProcessProperty();
        if (NetString.HasValue(propertyName))
        {
          //isFound = true;
        }
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
    private string ProcessMethod()
    {
      string retMethodName = null;

      var classes = DocDataFile.Classes;
      if (classes != null)
      {
        var codeParse = new LJCCSParser();
        retMethodName = codeParse.MethodName(Line);
        if (retMethodName != null)
        {
          var classItem = classes.Find(x => x.Name == ClassName);
          if (classItem != null)
          {
            InitializeMethods(classItem);
            var methods = classItem.Methods;
            var method = new LJCDocDataMethod(retMethodName, Comments.Summary)
            {
              Code = Comments.Code,
              Params = Comments.Params,
              ParentGroup = Comments.ParentGroup,
              Remarks = Comments.Remarks,
              Returns = Comments.Returns,
              Syntax = MethodSyntax()
            };
            methods.Add(method);
          }
          Comments.ClearComments();
        }
      }
      return retMethodName;
    }

    // Copy the Property XML comments into the DocData objects.
    private string ProcessProperty()
    {
      string retPropertyName = null;

      var classes = DocDataFile.Classes;
      if (classes != null)
      {
        var codeParse = new LJCCSParser();
        retPropertyName = codeParse.PropertyName(Line, NextLine);
        if (retPropertyName != null)
        {
          var classItem = classes.Find(x => x.Name == ClassName);
          if (classItem != null)
          {
            InitializeProperties(classItem);
            var properties = classItem.Properties;
            var property = new LJCDocDataProperty(retPropertyName, Comments.Summary)
            {
              Remarks = Comments.Remarks,
              Returns = Comments.Returns,
              Syntax = PropertySyntax()
            };
            properties.Add(property);
          }
          Comments.ClearComments();
        }
      }
      return retPropertyName;
    }
    #endregion

    #region Other Private Methods

    // Break on a file name.
    private void FileNameBreak(string fileName, string fileSpec)
    {
      var nameOnly = Path.GetFileName(fileSpec);
      if (fileName == nameOnly)
      {
        Debugger.Break();
      }
    }

    // Initialize the file Classes collection.
    private void InitializeClasses()
    {
      if (null == DocDataFile.Classes)
      {
        DocDataFile.Classes = new LJCDocDataClasses();
      }
    }

    // Initialize the class Groups collection.
    private void InitializeGroups(LJCDocDataClass classItem)
    {
      if (null == classItem.Groups)
      {
        classItem.Groups = new LJCDocDataParams();
      }
    }

    // Initialize the class Methods collection.
    private void InitializeMethods(LJCDocDataClass classItem)
    {
      if (null == classItem.Methods)
      {
        classItem.Methods = new LJCDocDataMethods();
      }
    }

    // Initialize the class Properties collection.
    private void InitializeProperties(LJCDocDataClass classItem)
    {
      if (null == classItem.Properties)
      {
        classItem.Properties = new LJCDocDataProperties();
      }
    }

    // Creates a Lib DocData XML output file spec.
    private string LibGenXMLSpec(string codeFileSpec, string outputPath = null)
    {
      string retFilespec;

      if (null == outputPath)
      {
        outputPath = "../XMLDocData";
      }
      NetFile.CreateFolder(outputPath);
      var fileName = Path.GetFileNameWithoutExtension(codeFileSpec) + ".xml";
      retFilespec = $"{outputPath}/{fileName}";
      return retFilespec;
    }

    // Create method syntax value.
    private string MethodSyntax()
    {
      string retSyntax;

      retSyntax = Line.Trim();
      var index = Line.IndexOf(')');
      while (index < 0)
      {
        LineIndex++;
        Line = Lines[LineIndex];
        retSyntax += Line;
        index = Line.IndexOf(')');
      }
      return retSyntax;
    }

    // Create property syntax value.
    private string PropertySyntax()
    {
      string retSyntax;

      retSyntax = Line.Trim();
      return retSyntax;
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

    private string ClassName { get; set; }

    // The XML Comments object.
    private LJCComments Comments { get; set; }

    // The DocDataFile object.
    private LJCDocDataFile DocDataFile { get; set; }

    // The GenDoc configuration.
    private LJCGenDocConfig GenDocConfig { get; set; }

    // The Lib name.
    private string LibName { get; set; }

    // The current process line.
    private string Line { get; set; }

    private int LineIndex { get; set; }

    private string[] Lines { get; set; }

    // The method name.
    private string MethodName { get; set; }

    // The next process line.
    private string NextLine { get; set; }

    // The property name.
    private string PropertyName { get; set; }
    #endregion
  }
}
