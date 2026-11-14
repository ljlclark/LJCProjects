// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDataGen.cs
using LJCDocDataDAL;
using LJCNetCommon;
using System.IO;

namespace LJCGenDataGenLib
{
  // Contains methods to create GenData XML and HTML Doc from DocData XML.
  /// <include path="members/LJCGenDataGen/*" file="Doc/LJCGenDataGen.xml"/>
  public class LJCGenDataGen
  {
    #region Properties

    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/CodeFileSpec/*'/>
    public string CodeFileSpec { get; set; }

    // Gets or sets the DocData.
    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/DocDataFile/*'/>
    public LJCDocDataFile DocDataFile { get; set; }

    // Gets or sets the GenDoc configuration.
    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/GenDocConfig/*'/>
    public LJCGenDocConfig GenDocConfig { get; set; }

    // Gets or sets the path for HTML output.
    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/HTMLPath/*'/>
    public string HTMLPath { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/Constructor/*'/>
    public LJCGenDataGen()
    {
    }

    // Sets the GenCodeDoc config.
    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/SetConfig/*'/>
    public void SetConfig(LJCGenDocConfig config)
    {
      HTMLPath = "CodeDoc/HTML";
      GenDocConfig = config;
      if (NetString.HasValue(config.OutputPath))
      {
        HTMLPath = config.OutputPath;
      }
      Directory.CreateDirectory(HTMLPath);
    }
    #endregion

    #region Methods

    // Creates the GenData xml from the DocData xml.
    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/CreateGenData/*'/>
    public string CreateGenData(string docDataXML, string codeFileSpec)
    {
      string retGenDataXML = null;

      CodeFileSpec = codeFileSpec;
      DocDataFile = NetCommon.XmlDeserializeMessage(typeof(LJCDocDataFile)
        , docDataXML) as LJCDocDataFile;

      GenTypePages();
      return retGenDataXML;
    }

    // Generates the type (class) pages.
    /// <include file='Doc/LJCGenDataGen.xml'
    ///  path='members/GenTypePages/*'/>
    public void GenTypePages()
    {
      foreach (LJCDocDataClass docDataClass in DocDataFile.Classes)
      {
        var genType = new LJCGenType(GenDocConfig, docDataClass);
        var sections = genType.GenDataClass(docDataClass);
        var sectionsXML = sections.LJCSerializeToString();
        WriteClassGenXML(sectionsXML, CodeFileSpec);
      }
    }
    #endregion

    #region Other Methods

    // Creates a Lib DocData XML output file spec.
    private string ClassGenXMLSpec(string codeFileSpec
      , string outputPath = null)
    {
      string retFilespec;

      if (null == outputPath)
      {
        outputPath = @"../XMLGenData/";
      }
      NetFile.CreateFolder(outputPath);
      var fileName = Path.GetFileNameWithoutExtension(codeFileSpec) + ".xml";
      retFilespec = $"{outputPath}/{fileName}";
      return retFilespec;
    }

    // Writes the ClassGenXML file.
    private bool WriteClassGenXML(string classGenXML, string codeFileSpec)
    {
      var retValue = false;

      var writeGenDataXML = GenDocConfig.WriteGenDataXML;
      if (writeGenDataXML
        && classGenXML != null)
      {
        retValue = true;
        var genDataXMLPath = GenDocConfig.GenDataXMLPath;
        var fileSpec = ClassGenXMLSpec(codeFileSpec, genDataXMLPath);
        File.WriteAllText(fileSpec, classGenXML);
      }
      return retValue;
    }
    #endregion
  }
}
