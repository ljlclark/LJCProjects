// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDocConfig.cs
using LJCNetCommon;
using System.Linq;

namespace LJCDocDataDAL
{
  /// <summary>
  /// Represents a GenCodeDoc configuration.
  /// </summary>
  public class LJCGenDocConfig
  {
    #region Constructor Methods

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
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

    /// <summary>
    /// Gets the relative path prefix.
    /// </summary>
    /// <param name="path">The parent folder path.</param>
    /// <returns>The relative path prefix from the program folder.</returns>
    public string GetParentPathPrefix(string path)
    {
      string retPath;

      var folders = path.Split('\\');
      var count = folders.Length - 1;
      retPath = string.Concat(Enumerable.Repeat("../", count));
      return retPath;
    }

    /// <summary>
    /// Sets a configuration property from a template line.
    /// </summary>
    /// <param name="line">The template line.</param>
    /// <returns>true if line is a file; otherwise false.</returns>
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

    #region Properties

    /// <summary>
    /// The DocDataXML target path.
    /// </summary>
    public string DocDataXMLPath { get; set; }

    /// <summary>
    /// The GenDataXML target path.
    /// </summary>
    public string GenDataXMLPath { get; set; }

    /// <summary>
    /// The Output target path.
    /// </summary>
    public string OutputPath { get; set; }

    /// <summary>
    /// Indicates if the DocDataXML will be written to a file.
    /// </summary>
    public bool WriteDocDataXML { get; set; }

    /// <summary>
    /// Indicates if the GenDataXML will be written to a file.
    /// </summary>
    public bool WriteGenDataXML { get; set; }
    #endregion
  }
}
