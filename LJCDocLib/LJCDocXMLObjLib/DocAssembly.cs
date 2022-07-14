// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// DataAssembly.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation assembly node.
  /// <include path='items/DocAssembly/*' file='Doc/DocAssembly.xml'/>
  public class DocAssembly
  {
    #region Properties

    /// <summary>The assembly name.</summary>
    [XmlElement("name")]
    public string Name { get; set; }
    #endregion
  }
}
