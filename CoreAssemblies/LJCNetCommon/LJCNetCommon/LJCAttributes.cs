// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAttributes.cs
using System.Collections.Generic;

namespace LJCNetCommon
{
  /// Represents a collection of Attribute elements.
  /// <include file='Doc/LJCAttributes.xml'
  ///  path='members/LJCAttributes/*'/>
  public class LJCAttributes : List<LJCAttribute>
  {
    #region Collection Data Methods

    // Creates and adds an Attribute.
    /// <include file='Doc/LJCAttributes.xml'
    ///  path='members/Add/*'/>
    public LJCAttribute Add(string name, string value)
    {
      var retAttribute = new LJCAttribute(name, value);
      Add(retAttribute);
      return retAttribute;
    }
    #endregion
  }
}
