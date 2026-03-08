// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDataGen.cs
using LJCDocDataDAL;
using LJCNetCommon;
using System.IO;

namespace LJCGenDoc2
{
  // Contains methods to create GenData XML and HTML Doc from DocData XML.
  /// <include path="members/LJCGenDataGen/*" file="Doc/LJCGenDataGen.xml"/>
  public class LJCGenDataGen
  {
    #region Constructor Methods

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    public LJCGenDataGen()
    {
    }

    // Sets the GenCodeDoc config.
    /// <include path="members/SetConfig/*" file="Doc/LJCGenDataGen.xml"/>
    public void SetConfig(LJCGenDocConfig config)
    {
      HTMLPath = "../../../WebSitesDev/CodeDoc/LJCCodeDoc2/HTML";
      GenDocConfig = config;
      if (NetString.HasValue(config.OutputPath))
      {
        HTMLPath = config.OutputPath;
      }
      Directory.CreateDirectory(HTMLPath);
    }
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the GenDoc configuration.
    /// </summary>
    public LJCGenDocConfig GenDocConfig { get; set; }

    /// <summary>
    /// Gets or sets the path for HTML output.
    /// </summary>
    public string HTMLPath { get; set; }
    #endregion
  }
}
