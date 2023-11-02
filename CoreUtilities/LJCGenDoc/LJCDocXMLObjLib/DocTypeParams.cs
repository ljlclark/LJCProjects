// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocTypeParams.cs
using System.Collections.Generic;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation "typeparam" nodes.
  /// <include path='items/DocParams/*' file='Doc/DocParams.xml'/>
  public class DocTypeParams : List<DocTypeParam>
  {
    #region Methods

    // Creates an element from the supplied values and adds it to the list.
    /// <include path='items/Add/*' file='Doc/DocParams.xml'/>
    public DocTypeParam Add(string name, string text)
    {
      DocTypeParam retValue;

      retValue = new DocTypeParam(name, text);
      Add(retValue);
      return retValue;
    }
    #endregion
  }
}
