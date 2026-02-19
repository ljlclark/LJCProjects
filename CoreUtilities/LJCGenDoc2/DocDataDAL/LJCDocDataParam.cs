// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataParam.cs
using LJCNetCommon;

namespace LJCDocDataDAL
{
  // Represents a DocData parameter.
  /// <include path="members/LJCDocDataParam/*" file="Doc/LJCDocDataParam.xml"/>
  public class LJCDocDataParam
  {
    #region Constructor Methods

    public LJCDocDataParam()
    {
    }

    public LJCDocDataParam(string name, string summary)
    {
      Name = name;
      Summary = summary;
    }
    #endregion

    #region Public Methods

    public LJCDocDataParam GetParam(string line)
    {
      LJCDocDataParam retParam = null;

      if (NetString.HasValue(line))
      {
        var startIndex = 0;
        var name = NetString.GetDelimitedString(line, "name=\"", ref startIndex
          , "\">");
        var summary = NetString.GetDelimitedString(line, ">", ref startIndex, "</");
        retParam = new LJCDocDataParam(name, summary);        
      }
      return retParam;
    }
    #endregion

    #region Properties

    /// <summary>The Name value.</summary>
    public string Name;

    /// <summary>The Summary value.</summary>
    public string Summary;
    #endregion
  }
}
