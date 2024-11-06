// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// XMLAttribute.cs
using LJCNetCommon;

namespace LJCDataUtility
{
  /// <summary>
  /// Represents an XML element attribute.
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

    /// <summary>
    /// Gets or sets the attribute name.
    /// </summary>
    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        name = NetString.InitString(value);
      }
    }
    private string name;

    /// <summary>
    /// Gets or sets the attribute value.
    /// </summary>
    public string Value
    {
      get
      {
        return attributeValue;
      }
      set
      {
        attributeValue = NetString.InitString(value);
      }
    }
    private string attributeValue;
    #endregion
  }
}
