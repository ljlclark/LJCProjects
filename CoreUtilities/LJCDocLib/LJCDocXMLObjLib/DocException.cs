// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocException.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialied XML documentation "exception" node.
  /// <include path='items/DocException/*' file='Doc/DocException.xml'/>
  [XmlType("exception")]
  public class DocException
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocException()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DocExceptionC/*' file='Doc/DocException.xml'/>
    public DocException(string cref, string text)
    {
      CRef = cref;
      Text = text;
    }
    #endregion

    #region Properties

    /// <summary>The doc element name.</summary>
    [XmlAttribute("cref")]
    public string CRef { get; set; }

    /// <summary>Gets or sets the XML element text value.</summary>
    [XmlText()]
    public string Text { get; set; }
    #endregion
  }
}
