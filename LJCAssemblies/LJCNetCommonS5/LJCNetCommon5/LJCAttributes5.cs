// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAttributes5.cs

namespace LJCNetCommon5
{
  // Represents a collection of Attribute elements.
  /// <include file='Doc/ProjectAttributes.xml'
  ///  path='members/LJCAttributes/*'/>
  /// <group name="collection">Collection</group>
  public class LJCAttributes : List<LJCAttribute>
  {
    #region Collection Methods

    // Creates and adds an Attribute.
    /// <include file='Doc/Attributes.xml'
    ///  path='members/Add/*'/>
    /// <parentGroup>collection</parentGroup>
    public LJCAttribute Add(string name, string value)
    {
      var retAttrib = new LJCAttribute(name, value);
      Add(retAttrib);
      return retAttrib;
    }
    #endregion
  }
}
