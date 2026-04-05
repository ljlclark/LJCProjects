// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAttribute.cs

namespace LJCNetCommon5
{
  // Represents an HTML or XML element attribute.
  /// <include path="members/LJCAttribute/*" file="Doc/LJCAttribute.xml"/>
  /// <group name="constructor">Constructor</group>
  /// <group name="properties">Properties</group>
  public class LJCAttribute
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCAttribute.xml"/>
    /// <parentGroup>constructor</parentGroup>
    public LJCAttribute(string name, string? value = null)
    {
      Name = name;
      Value = value;
    }
    #endregion

    #region Properties

    // Gets or sets the attribute name.
    /// <include path="members/Name/*" file="Doc/LJCAttribute.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? Name
    {
      get { return mName; }
      set { mName = LJCNetString.InitString(value); }
    }
    private string? mName;

    // Gets or sets the attribute value.
    /// <include path="members/Value/*" file="Doc/LJCAttribute.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? Value
    {
      get { return mValue; }
      set { mValue = LJCNetString.InitString(value); }
    }
    private string? mValue;
    #endregion
  }
}
