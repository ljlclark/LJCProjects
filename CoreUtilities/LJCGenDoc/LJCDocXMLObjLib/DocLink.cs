// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocLink.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialied XML documentation "link" node.
  /// <include path='items/DocLink/*' file='Doc/DocLink.xml'/>
  [XmlType("link")]
  public class DocLink
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocLink()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DocLinkC/*' file='Doc/DocLink.xml'/>
    public DocLink(string fileName, string text)
    {
      FileName = fileName;
      Text = text;
    }
    #endregion

    #region Properties

    /// <summary>The doc element name.</summary>
    [XmlAttribute("file")]
    public string FileName { get; set; }

    /// <summary>Gets or sets the XML element text value.</summary>
    [XmlText()]
    public string Text { get; set; }
    #endregion
  }
}
