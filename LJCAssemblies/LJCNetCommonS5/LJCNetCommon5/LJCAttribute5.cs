// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAttribute5.cs

namespace LJCNetCommon5
{
  // Represents an HTML or XML element attribute.
  /// <include file='Doc/Attribute.xml'
  ///  path='members/LJCAttribute/*'/>
  /// <group name="constructor">Constructor</group>
  /// <group name="properties">Properties</group>
  public class LJCAttribute
  {
    #region Properties

    // Gets or sets the attribute name.
    /// <include file='Doc/Attribute.xml'
    ///  path='members/Name/*'/>
    /// <parentGroup>properties</parentGroup>
    public string? Name
    {
      get => mName;
      set => mName = value?.Trim();
    }
    private string? mName;

    // Gets or sets the attribute value.
    /// <include file='Doc/Attribute.xml'
    ///  path='members/Value/*'/>
    /// <parentGroup>properties</parentGroup>
    public string? Value
    {
      get => mValue;
      set => mValue = value?.Trim();
    }
    private string? mValue;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/Attribute.xml'
    ///  path='members/Constructor/*'/>
    /// <parentGroup>constructor</parentGroup>
    public LJCAttribute(string name, string? value = null)
    {
      Name = name;
      Value = value;
    }
    #endregion
  }
}
