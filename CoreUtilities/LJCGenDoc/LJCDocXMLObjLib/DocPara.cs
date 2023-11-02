// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocPara.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialied XML documentation "para" node.
  /// <include path='items/DocPara/*' file='Doc/DocPara.xml'/>
  [XmlType("para")]
  public class DocPara
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocPara()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DocParaC/*' file='Doc/DocPara.xml'/>
    public DocPara(string text)
    {
      Text = text;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the XML element text value.</summary>
    [XmlText()]
    public string Text { get; set; }
    #endregion
  }
}
