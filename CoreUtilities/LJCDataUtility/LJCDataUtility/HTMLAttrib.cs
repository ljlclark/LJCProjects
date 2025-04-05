// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLAttrib.cs

using LJCNetCommon;

namespace LJCDataUtility
{
  /// <summary>
  /// Represents an XML element attribute.
  /// </summary>
  public class HTMLAttrib
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CHTMLAttribute/*' file='Doc/HTMLAttrib.xml'/>
    public HTMLAttrib(string name, string value = null)
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
      get { return mAttributeValue; }
      set
      {
        mAttributeValue = NetString.InitString(value);
      }
    }
    private string mAttributeValue;
    #endregion
  }
}
