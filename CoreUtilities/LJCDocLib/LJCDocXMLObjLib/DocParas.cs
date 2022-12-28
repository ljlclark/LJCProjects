// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataParas.cs
using System.Collections.Generic;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation "para" nodes.
  /// <include path='items/DocParas/*' file='Doc/DocParas.xml'/>
  public class DocParas : List<DocPara>
  {
    #region Methods

    // Creates an element from the supplied values and adds it to the list.
    /// <include path='items/Add/*' file='Doc/DocParas.xml'/>
    public DocPara Add(string text)
    {
      DocPara retValue;

      retValue = new DocPara(text);
      Add(retValue);
      return retValue;
    }
    #endregion
  }
}
