// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Attributes.cs
using System.Collections.Generic;

namespace LJCNetCommon
{
  // Move to LJCNetCommon
  /// <summary>
  /// Represents a collection of Attribute elements.
  /// </summary>
  public class Attributes : List<LJCAttribute>
  {
    #region Collection Methods

    // Creates and adds an Attribute.
    /// <include file='Doc/Attributes.xml'
    ///  path='items/Add/*'/>
    public LJCAttribute Add(string name, string value)
    {
      var retAttribute = new LJCAttribute(name, value);
      Add(retAttribute);
      return retAttribute;
    }
    #endregion
  }
}
