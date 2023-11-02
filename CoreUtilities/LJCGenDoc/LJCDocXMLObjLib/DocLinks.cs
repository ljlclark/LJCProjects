// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocLinks.cs
using System.Collections.Generic;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation "param" nodes.
  /// <include path='items/DocLinks/*' file='Doc/DocLinks.xml'/>
  public class DocLinks : List<DocLink>
  {
    #region Methods

    // Creates an element from the supplied values and adds it to the list.
    /// <include path='items/Add/*' file='Doc/DocLinks.xml'/>
    public DocLink Add(string fileName, string text)
    {
      DocLink retValue;

      retValue = new DocLink(fileName, text);
      Add(retValue);
      return retValue;
    }
    #endregion
  }
}
