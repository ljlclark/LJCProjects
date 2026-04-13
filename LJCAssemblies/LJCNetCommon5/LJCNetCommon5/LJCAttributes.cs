// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAttributes.cs

namespace LJCNetCommon5
{
  // Represents a collection of Attribute elements.
  /// <include path="members/LJCAttributes/*" file="Doc/LJCAttributes.xml"/>
  /// <group name="collection">Collection</group>
  public class LJCAttributes : List<LJCAttribute>
  {
    #region Collection Methods

    // Creates and adds an Attribute.
    /// <include path="members/Add/*" file="Doc/LJCAttributes.xml"/>
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
