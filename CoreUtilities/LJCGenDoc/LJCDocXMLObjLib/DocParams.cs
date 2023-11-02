// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocParams.cs
using System.Collections.Generic;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation "param" nodes.
  /// <include path='items/DocParams/*' file='Doc/DocParams.xml'/>
  public class DocParams : List<DocParam>
  {
    #region Methods

    // Creates an element from the supplied values and adds it to the list.
    /// <include path='items/Add/*' file='Doc/DocParams.xml'/>
    public DocParam Add(string name, string text)
    {
      DocParam retValue;

      retValue = new DocParam(name, text);
      Add(retValue);
      return retValue;
    }
    #endregion
  }
}
