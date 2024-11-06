// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// XMLAttributes.cs
using System.Collections.Generic;

namespace LJCDataUtility
{
  /// <summary>
  /// Represents a collection of XMLAttribute elements.
  /// </summary>
  public class XMLAttributes : List<XMLAttribute>
  {
    #region Collection Methods

    // Creates and adds an XMLAttribute.
    /// <include path='items/Add/*' file='Doc/XMLAttributes.xml'/>
    public XMLAttribute Add(string name, string value)
    {
      var retXMLAttribute = new XMLAttribute(name, value);
      Add(retXMLAttribute);
      return retXMLAttribute;
    }
    #endregion
  }
}
