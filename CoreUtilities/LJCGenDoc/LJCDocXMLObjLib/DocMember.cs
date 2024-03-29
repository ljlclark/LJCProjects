// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocMember.cs
using System;
using System.Xml.Serialization;

namespace LJCDocXMLObjLib
{
  /// <summary>The deserialized XML documentation member node.</summary>
  [XmlType("member")]
  public class DocMember : IComparable<DocMember>
  {
    #region IComparable Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(DocMember other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        retValue = Name.CompareTo(other.Name);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the member example node.</summary>
    [XmlElement("example")]
    public DocExample Example { get; set; }

    /// <summary>Gets or sets the member remarks node.</summary>
    [XmlElement("exception")]
    public DocExceptions Exceptions { get; set; }

    /// <summary>Gets or sets the member name.</summary>
    [XmlAttribute("name")]
    public string Name { get; set; }

    /// <summary>Gets or sets the member remarks node.</summary>
    [XmlElement("link")]
    public DocLinks Links { get; set; }
    
    /// <summary>Gets or sets the parameters (param) nodes.</summary>
    [XmlElement("param")]
    public DocParams Params { get; set; }

    /// <summary>Gets or sets the member remarks node.</summary>
    [XmlElement("remarks")]
    public DocRemarks Remarks { get; set; }

    /// <summary>Gets or sets the member "returns" node.</summary>
    [XmlElement("returns")]
    public string Returns { get; set; }

    /// <summary>Gets or sets the member summary value.</summary>
    [XmlElement("summary")]
    public string Summary { get; set; }

    /// <summary>Gets or sets the member remarks node.</summary>
    [XmlElement("typeparam")]
    public DocTypeParams TypeParams { get; set; }
    #endregion
  }
}
