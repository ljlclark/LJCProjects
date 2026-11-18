// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAttributes5.cs

namespace LJCNetCommon5
{
  // Represents a collection of Attribute elements.
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
      var retAttrib = new LJCAttribute(name, value);
      Add(retAttrib);
      return retAttrib;
    }
    #endregion
  }
}
