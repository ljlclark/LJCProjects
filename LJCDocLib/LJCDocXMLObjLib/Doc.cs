// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// Doc.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation data.
  /// <include path='items/Doc/*' file='Doc/ProjectDocXMLObjLib.xml'/>
  [XmlRoot("doc")]
  public class Doc
  {
    #region Properties

    /// <summary>The assembly information.</summary>
    [XmlElement("assembly")]
    public DocAssembly DocAssembly { get; set; }

    // The documentation members.
    /// <include path='items/DocMembers/*' file='Doc/Doc.xml'/>
    [XmlArray("members")]
    public DocMembers DocMembers { get; set; }
    #endregion
  }
}
