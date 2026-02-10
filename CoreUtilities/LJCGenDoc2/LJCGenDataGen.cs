// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDataGen.cs
using LJCDocDataDAL;

namespace LJCGenDoc2
{
  // Contains methods to create GenData XML and HTML Doc from DocData XML.
  /// <include path="members/LJCGenDataGen/*" file="Doc/LJCGenDataGen.xml"/>
  public class LJCGenDataGen
  {
    #region Constructor Methods

    // Sets the GenCodeDoc config.
    /// <include path="members/SetConfig/*" file="Doc/LJCGenDataGen.xml"/>
    public void SetConfig(LJCGenDocConfig config)
    {
      GenDocConfig = config;
    }
    #endregion

    #region Properties

    /// <summary>
    /// The GenDoc configuration.
    /// </summary>
    public LJCGenDocConfig GenDocConfig { get; set; }
    #endregion
  }
}
