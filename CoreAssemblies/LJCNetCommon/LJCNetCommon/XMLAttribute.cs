// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// XMLAttribute.cs

namespace LJCNetCommon
{
  /// <summary>
  /// Represents an element or node attribute.
  /// </summary>
  public class XMLAttribute
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CXMLAttribute/*' file='Doc/XMLAttribute.xml'/>
    public XMLAttribute(string name, string value = null)
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
