// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataParam5.cs
using LJCNetCommon5;

namespace LJCDocDataDAL5
{
  // Represents a DocData parameter.
  /// <include path="members/LJCDocDataParam/*" file="Doc/LJCDocDataParam.xml"/>
  public class LJCDocDataParam5
  {
    #region Static Methods

    // Create an LJCDocDataParam object from an "include" line.
    /// <include path="members/GetParam/*" file="Doc/LJCDocDataParam.xml"/>
    public static LJCDocDataParam5? GetParam(string line)
    {
      LJCDocDataParam5? retParam = null;

      if (LJC5.HasValue(line))
      {
        var parser = new LJCTextParser5();
        var name = parser.DelimitedString(line, "name=\"", "\">");
        parser.StartIndex = 0;
        string? summary = parser.DelimitedString(line, ">", "</");
        if (LJC5.HasValue(name))
        {
          retParam = new LJCDocDataParam5(name, summary);
        }
      }
      return retParam;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDocDataParam.xml"/>
    public LJCDocDataParam5()
    {
    }

    // Initializes an object instance with the provided values.
    /// <include path="members/ConstructorParams/*" file="Doc/LJCDocDataParam.xml"/>
    public LJCDocDataParam5(string name, string? summary)
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
