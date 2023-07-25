// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataLinks.cs
using System.Collections.Generic;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation "param" nodes.
  /// <include path='items/DocParams/*' file='Doc/DocParams.xml'/>
  public class DocLinks : List<DocLink>
  {
    #region Methods

    // Creates an element from the supplied values and adds it to the list.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="text"></param>
    /// <returns></returns>
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
