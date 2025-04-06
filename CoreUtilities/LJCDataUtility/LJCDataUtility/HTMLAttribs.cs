// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Attributes.cs
using System.Collections.Generic;

namespace LJCDataUtility
{
  /// <summary>
  /// Represents a collection of Attribute elements.
  /// </summary>
  public class Attributes : List<Attribute>
  {
    #region Collection Methods

    // Creates and adds an Attribute.
    /// <include path='items/Add/*' file='Doc/HTMLAttribs.xml'/>
    public Attribute Add(string name, string value)
    {
      var retAttribute = new Attribute(name, value);
      Add(retAttribute);
      return retAttribute;
    }
    #endregion
  }
}
