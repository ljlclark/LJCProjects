// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLAttribs.cs
using System.Collections.Generic;

namespace LJCDataUtility
{
  /// <summary>
  /// Represents a collection of HTMLAttribute elements.
  /// </summary>
  public class HTMLAttribs : List<HTMLAttrib>
  {
    #region Collection Methods

    // Creates and adds an XMLAttribute.
    /// <include path='items/Add/*' file='Doc/HTMLAttribs.xml'/>
    public HTMLAttrib Add(string name, string value)
    {
      var retHTMLAttribute = new HTMLAttrib(name, value);
      Add(retHTMLAttribute);
      return retHTMLAttribute;
    }
    #endregion
  }
}
