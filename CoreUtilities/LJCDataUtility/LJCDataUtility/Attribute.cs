// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Attribute.cs
using LJCNetCommon;

namespace LJCDataUtility
{
  // Move to LJCNetCommon
  /// <summary>
  /// Represents an HTML or XML element attribute.
  /// </summary>
  public class Attribute
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CHTMLAttribute/*' file='Doc/Attribute.xml'/>
    public Attribute(string name, string value = null)
    {
      Name = name;
      Value = value;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the attribute name.</summary>
    public string Name
    {
      get { return mName; }
      set
      {
        mName = NetString.InitString(value);
      }
    }
    private string mName;

    /// <summary>Gets or sets the attribute value.</summary>
    public string Value
    {
      get { return mValue; }
      set
      {
        mValue = NetString.InitString(value);
      }
    }
    private string mValue;
    #endregion
  }
}
