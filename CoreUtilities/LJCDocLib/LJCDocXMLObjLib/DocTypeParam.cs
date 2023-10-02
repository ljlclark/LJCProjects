// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocTypeParam.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation "param" nodes.
  /// <include path='items/DocParam/*' file='Doc/DocParam.xml'/>
  [XmlType("typeparam")]
  public class DocTypeParam
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocTypeParam()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DocParamC/*' file='Doc/DocParam.xml'/>
    public DocTypeParam(string name, string text)
    {
      Name = name;
      Text = text;
    }
    #endregion

    #region Properties

    /// <summary>The doc element name.</summary>
    [XmlAttribute("name")]
    public string Name { get; set; }

    /// <summary>The XML element text value.</summary>
    [XmlText()]
    public string Text { get; set; }
    #endregion
  }
}
