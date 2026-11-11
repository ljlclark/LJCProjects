// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDoc2.cs
using LJCDocDataDAL;
using LJCDocDataGenLib;
using LJCGenDataGenLib;
using LJCNetCommon;
using System.IO;

namespace LJCGenDoc2
{
  // Generates HTML Doc for sources in SourceList.txt.
  /// <include file='Doc/LJCGenDoc2.xml'
  ///  path='members/LJCGenDoc2/*'/>
  public class LJCGenDoc2
  {
    #region Properties

    // The Generate DocData XML object.
    /// <include file='Doc/LJCGenDoc2.xml'
    ///  path='members/DocDataGen/*'/>
    public LJCDocDataGen DocDataGen { get; set; }

    // The Generate GenData XML object.
    /// <include file='Doc/LJCGenDoc2.xml'
    ///  path='members/GenDataGen/*'/>
    public LJCGenDataGen GenDataGen { get; set; }

    // The GenDoc configuration.
    /// <include file='Doc/LJCGenDoc2.xml'
    ///  path='members/GenDocConfig/*'/>
    public LJCGenDocConfig GenDocConfig { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCGenDoc2.xml'
    ///  path='members/Constructor/*'/>
    public LJCGenDoc2()
    {
      // Initialize Properties
      DocDataGen = new LJCDocDataGen();
      GenDataGen = new LJCGenDataGen();
    }
    #endregion

    #region Methods

    // Creates CodeDoc pages from source list.
    /// <include file='Doc/LJCGenDoc2.xml'
    ///  path='members/CreateFromList/*'/>
    public void CreateFromList(string sourceListSpec)
    {
      if (null == GenDocConfig)
      {
        GenDocConfig = new LJCGenDocConfig();
      }

      // Gets the list of folders to read.
      var sourceList = File.ReadAllLines(sourceListSpec);
      foreach (var line in sourceList)
      {
        if (!line.Trim().StartsWith("//"))
        {
          // Sets config property or returns true if a folder or file.
          if (GenDocConfig.SetProperty(line))
          {
            DocDataGen.SetConfig(GenDocConfig);
            GenDataGen.SetConfig(GenDocConfig);
            CreateFilePages(line, "*.cs");
          }
        }
      }
    }

    /// Creates the HTML documentation files.
    /// <include file='Doc/LJCGenDoc2.xml'
    ///  path='members/CreateFilePages/*'/>
    public void CreateFilePages(string sourcePath, string pattern)
    {
      string docDataXML;
      var ext = Path.GetExtension(sourcePath);
      if (!NetString.HasValue(ext))
      {
        // Process files in a folder.
        var codeFileSpecs = Directory.GetFiles(sourcePath, pattern);
        foreach (var codeFileSpec in codeFileSpecs)
        {
          docDataXML = DocDataGen.CreateDocData(codeFileSpec);
          if (docDataXML != null)
          {
            var genDataXML = GenDataGen.CreateGenData(docDataXML
              , codeFileSpec);
          }
        }
      }
      else
      {
        // Process specific file.
        docDataXML = DocDataGen.CreateDocData(sourcePath);
        if (docDataXML != null)
        {
          var genDataXML = GenDataGen.CreateGenData(docDataXML, sourcePath);
        }
      }
    }

    // Sets the GenCodeDoc config.
    /// <include file='Doc/LJCGenDoc2.xml'
    ///  path='members/SetConfig/*'/>
    public void SetConfig(LJCGenDocConfig config)
    {
      GenDocConfig = config;
    }
    #endregion
  }
}
