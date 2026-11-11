// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDocConfig.cs
using LJCNetCommon;
using System.Linq;

namespace LJCDocDataDAL
{
  // Represents a GenCodeDoc configuration.
  /// <include file='Doc/LJCGenDocConfig.xml'
  ///  path='members/LJCGenDocConfig/*'/>
  public class LJCGenDocConfig
  {
    #region Properties

    // Gets or sets the DocData XML target path.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/DocDataXMLPath/*'/>
    public string DocDataXMLPath { get; set; }

    // Gets or sets the GenData XML target path.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/GenDataXMLPath/*'/>
    public string GenDataXMLPath { get; set; }

    // Gets or sets the output target path.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/OutputPath/*'/>
    public string OutputPath { get; set; }

    // Gets or sets a value indicating if the DocData XML is written to a file.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/WriteDocDataXML/*'/>
    public bool WriteDocDataXML { get; set; }

    // Gets or sets a value indicating if the GenData XML is written to a file.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/WriteGenDataXML/*'/>
    public bool WriteGenDataXML { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/Constructor/*'/>
    public LJCGenDocConfig()
    {
      // Initialize Properties
      DocDataXMLPath = @"../XMLDocData";
      GenDataXMLPath = @"../XMLGenData";
      OutputPath = null;
      WriteDocDataXML = false;
      WriteGenDataXML = false;
    }
    #endregion

    #region Methods

    // Returns the relative path prefix.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/GetParentPathPrefix/*'/>
    public string GetParentPathPrefix(string path)
    {
      string retPath;

      var folders = path.Split('\\');
      var count = folders.Length - 1;
      retPath = string.Concat(Enumerable.Repeat("../", count));
      return retPath;
    }

    // Sets a configuration property from a template line.
    /// <include file='Doc/LJCGenDocConfig.xml'
    ///  path='members/SetProperty/*'/>
    public bool SetProperty(string line)
    {
      var retIsSource = false;

      if (NetString.HasValue(line))
      {
        retIsSource = true;
      }
      if (line.Contains(":"))
      {
        retIsSource = false;
        var tokens = line.Split(':');
        if (2 == tokens.Length)
        {
          var name = tokens[0].Trim();
          var value = tokens[1].Trim();
          switch (name.ToLower())
          {
            case "docdataxmlpath":
              DocDataXMLPath = value;
              break;
            case "gendataxmlpath":
              GenDataXMLPath = value;
              break;
            case "outputpath":
              OutputPath = value;
              break;
            case "writedocdataxml":
              WriteDocDataXML = bool.Parse(value);
              break;
            case "writegendataxml":
              WriteGenDataXML = bool.Parse(value);
              break;
          }
        }
      }
      return retIsSource;
    }
    #endregion
  }
}
