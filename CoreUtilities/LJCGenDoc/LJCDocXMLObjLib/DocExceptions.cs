// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocExceptions.cs

using System.Collections.Generic;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation "exception" nodes.
  /// <include path='items/DocExceptions/*' file='Doc/DocException.xml'/>
  public class DocExceptions : List<DocException>
  {
    #region Methods

    // Creates an element from the supplied values and adds it to the list.
    /// <include path='items/Add/*' file='Doc/DocExceptions.xml'/>
    public DocException Add(string cref, string text)
    {
      DocException retValue;

      retValue = new DocException(cref, text);
      Add(retValue);
      return retValue;
    }
    #endregion
  }
}
