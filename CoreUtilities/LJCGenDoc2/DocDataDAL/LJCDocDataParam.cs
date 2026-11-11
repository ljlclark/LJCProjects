// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDocDataParam.cs
using LJCNetCommon;

namespace LJCDocDataDAL
{
  // Represents a DocData parameter.
  /// <include file='Doc/LJCDocDataParam.xml'
  ///  path='members/LJCDocDataParam/*'/>
  public class LJCDocDataParam
  {
    #region Properties

    // Gets or sets the param name.
    /// <include file='Doc/LJCDocDataParam.xml'
    ///  path='members/Name/*'/>
    public string Name { get; set; }

    // Gets or sets the summary text.
    /// <include file='Doc/LJCDocDataParam.xml'
    ///  path='members/Summary/*'/>
    public string Summary { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCDocDataParam.xml'
    ///  path='members/Constructor/*'/>
    public LJCDocDataParam()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCDocDataParam.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCDocDataParam(string name, string summary)
    {
      Name = name;
      Summary = summary;
    }
    #endregion

    #region Methods

    // Sets the parameter values from a code line.
    /// <include file='Doc/LJCDocDataParam.xml'
    ///  path='members/GetParam/*'/>
    public LJCDocDataParam GetParam(string line)
    {
      LJCDocDataParam retParam = null;

      if (NetString.HasValue(line))
      {
        var parser = new LJCParser();
        var name = parser.DelimitedString(line, "name=\"", "\">");
        var summary = parser.DelimitedString(line, ">", "</");
        retParam = new LJCDocDataParam(name, summary);
      }
      return retParam;
    }
    #endregion
  }
}
