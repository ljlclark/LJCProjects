// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataPara.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialied XML documentation "para" node.
  /// <include path='items/DocPara/*' file='Doc/DocPara.xml'/>
  [XmlType("link")]
  public class DocLink
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocLink()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="text"></param>
    public DocLink(string fileName, string text)
    {
      FileName = fileName;
      Text = text;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the XML element text value.</summary>
    [XmlAttribute("file")]
    public string FileName { get; set; }

    /// <summary>The doc element name.</summary>
    [XmlText()]
    public string Text { get; set; }
    #endregion
  }
}
