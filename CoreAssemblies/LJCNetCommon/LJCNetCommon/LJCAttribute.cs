// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAttribute.cs

namespace LJCNetCommon
{
  // Represents an HTML or XML element attribute.
  /// <include file='Doc/LJCAttribute.xml'
  ///  path='members/LJCAttribute/*'/>
  public class LJCAttribute
  {
    #region Properties

    // Gets or sets the attribute name.
    /// <include file='Doc/LJCAttribute.xml'
    ///  path='members/Name/*'/>
    public string Name
    {
      get => mName;
      set => mName = value?.Trim();
    }
    private string mName;

    // Gets or sets the attribute value.
    /// <include file='Doc/LJCAttribute.xml'
    ///  path='members/Value/*'/>
    public string Value
    {
      get => mValue;
      set => mValue = value?.Trim();
    }
    private string mValue;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCAttribute.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCAttribute(string name, string value = null)
    {
      Name = name;
      Value = value;
    }
    #endregion
  }
}
