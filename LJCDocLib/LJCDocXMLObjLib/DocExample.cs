// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataExample.cs
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  /// <summary>The deserialized XML documentation member\example node.</summary>
  [XmlType("example")]
  public class DocExample
  {
    #region Properties

    /// <summary>Gets or sets the example/code node.</summary>
    [XmlElement("code")]
    public string Code { get; set; }

    /// <summary>Gets or sets the example/para paragraph nodes.</summary>
    [XmlElement("para")]
    public DocParas Paras { get; set; }
    #endregion
  }
}
