// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocAssembly.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation assembly node.
  /// <include path='items/DocAssembly/*' file='Doc/DocAssembly.xml'/>
  public class DocXMLAssembly
  {
    #region Properties

    /// <summary>The assembly name.</summary>
    [XmlElement("name")]
    public string Name { get; set; }
    #endregion
  }
}
