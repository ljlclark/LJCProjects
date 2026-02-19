// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDoc2.cs
using LJCDocDataDAL;
using LJCDocDataGenLib;
using LJCNetCommon;
using System.IO;

namespace LJCGenDoc2
{
  // Generates HTML Doc for sources in SourceList.txt.
  /// <include path="members/LJCGenDoc2/*" file="Doc/LJCGenDoc2.xml"/>
  public class LJCGenDoc2
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCGenDoc2.xml"/>
    public LJCGenDoc2()
    {
      // Initialize Properties
      DocDataGen = new LJCDocDataGen();
      GenDataGen = new LJCGenDataGen();
    }
    #endregion

    #region Public Methods

    // Creates CodeDoc pages from source list.
    /// <include path="members/CreateFromList/*" file="Doc/LJCGenDoc2.xml"/>
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
          // Sets config properties or returns true if a folder or file.
          if (GenDocConfig.SetProperty(line))
          {
            DocDataGen.SetConfig(GenDocConfig);
            GenDataGen.SetConfig(GenDocConfig);
            CreateFilePages(line, "*.cs");
          }
        }
      }
    }

    /// <summary>
    /// Creates the HTML documentation files.
    /// </summary>
    /// <param name="folderPath">The folder path.</param>
    public void CreateFilePages(string sourcePath, string pattern)
    {
      string docDataXML = null;
      var ext = Path.GetExtension(sourcePath);
      if (!NetString.HasValue(ext))
      {
        // Process files in a folder.
        var files = Directory.GetFiles(sourcePath, pattern);
        foreach (var file in files)
        {
          docDataXML = DocDataGen.SerializeDocData(file);
          if (docDataXML != null)
          {
            //var genDataXML = GenDataGen.SerializeList(docDataXML, file);
          }
        }
      }
      else
      {
        // Process specific file.
        docDataXML = DocDataGen.SerializeDocData(sourcePath);
        if (docDataXML != null)
        {
          //var genDataXML = GenDataGen.SerializeList(docDataXML, sourcePath);
        }
      }
    }

    // Sets the GenCodeDoc config.
    /// <include path="members/SetConfig/*" file="Doc/LJCGenDoc2.xml"/>
    public void SetConfig(LJCGenDocConfig config)
    {
      GenDocConfig = config;
    }
    #endregion

    #region Private Methods
    #endregion

    #region Properties

    /// <summary>
    // The Generate DocData XML object.
    /// </summary>
    public LJCDocDataGen DocDataGen { get; set; }

    /// <summary>
    // The Generate DocData XML object.
    /// </summary>
    public LJCGenDataGen GenDataGen { get; set; }

    /// <summary>
    // The GenDoc configuration.
    /// </summary>
    public LJCGenDocConfig GenDocConfig { get; set; }
    #endregion
  }
}
