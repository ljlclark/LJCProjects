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
    #region Static Methods

    // Create an LJCDocDataParam object from an "include" line.
    /// <include path="members/GetParam/*" file="Doc/LJCDocDataParam.xml"/>
    public static LJCDocDataParam? GetParam(string line)
    {
      LJCDocDataParam? retParam = null;

      if (LJC.HasValue(line))
      {
        var parser = new LJCTextParser();
        var name = parser.DelimitedString(line, "name=\"", "\">");
        parser.StartIndex = 0;
        string? summary = parser.DelimitedString(line, ">", "</");
        if (LJC.HasValue(name))
        {
          retParam = new LJCDocDataParam(name, summary);
        }
      }
      return retParam;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataParam.xml"/>
    public LJCDocDataParam()
    {
    }

    // Initializes an object instance with the provided values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataParam.xml"/>
    public LJCDocDataParam(string name, string? summary)
    {
      Name = name;
      Summary = summary;
    }
    #endregion

    #region Properties

    // Gets or sets the Name value.
    /// <include path="members/Name/*" file="Doc/LJCDocDataParam.xml"/>
    public string? Name { get; set; }

    // Gets or sets the Summary value.
    /// <include path="members/Summary/*" file="Doc/LJCDocDataParam.xml"/>
    public string? Summary { get; set; }
    #endregion
  }
}
