// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataField.cs
using LJCDocXMLObjLib;

namespace LJCDocObjLib
{
  // Represents the Field documentation data.
  /// <include path='items/DataField/*' file='Doc/DataField.xml'/>
  public class DataField
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataFieldC/*' file='Doc/DataField.xml'/>
    public DataField(DataAssembly dataAssembly, DataType dataType
      , DocMember fieldMember)
    {
      Doc = dataAssembly.Doc;
      FieldMember = fieldMember;
      AssemblyName = dataAssembly.Name;
      TypeName = dataType.Name;

      MemberName = fieldMember.Name;
      Name = DataCommon.GetMemberName(MemberName);

      Summary = fieldMember.Summary;
      Remark = DataCommon.GetDataRemark(fieldMember.Remarks);
      IsPublic = DataCommon.IsPublic(Remark, out bool _);
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{AssemblyName}.{TypeName}.{Name}";
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the public flag.</summary>
    public bool IsPublic { get; set; }

    /// <summary>Gets or sets the Field Name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Remarks value.</summary>
    public DataRemark Remark { get; set; }

    /// <summary>Gets or sets the Summary value.</summary>
    public string Summary { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the parent Assembly name value.</summary>
    public string AssemblyName { get; set; }

    // Gets or sets the Doc value.
    /// <include path='items/Doc/*' file='Doc/DataField.xml'/>
    public Doc Doc { get; set; }

    // Gets or sets the FieldMember value.
    /// <include path='items/FieldMember/*' file='Doc/DataField.xml'/>
    public DocMember FieldMember { get; set; }

    /// <summary>Gets or sets the object MemberName value.</summary>
    public string MemberName { get; set; }

    /// <summary>Gets or sets the class/type name value.</summary>
    public string TypeName { get; set; }
    #endregion
  }
}
