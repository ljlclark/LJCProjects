// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// DataRemarks.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  /// <summary>The deserialized XML documentation remarks node.</summary>
  [XmlType("remarks")]
  public class DocRemarks
  {
    #region Properties

    /// <summary>The "para" nodes.</summary>
    [XmlElement("para")]
    public DocParas Paras { get; set; }

    /// <summary>The XML element text value.</summary>
    [XmlText()]
    public string Text { get; set; }
    #endregion
  }
}
