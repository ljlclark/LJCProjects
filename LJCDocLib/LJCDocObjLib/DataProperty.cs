// DataProperty.cs
using LJCDocXMLObjLib;
using LJCNetCommon;
using System;

namespace LJCDocObjLib
{
  // Represents the Property documentation data.
  /// <include path='items/DataProperty/*' file='Doc/DataProperty.xml'/>
  public class DataProperty : IComparable<DataProperty>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataProperty()
    {
    }

    // Initializes an object instance.
    /// <include path='items/DataPropertyC/*' file='Doc/DataProperty.xml'/>
    public DataProperty(DataAssembly dataAssembly, DataType dataType
      , DocMember propertyMember, string overriddenName)
    {
      Doc = dataAssembly.Doc;
      PropertyMember = propertyMember;
      AssemblyName = dataAssembly.Name;
      TypeName = dataType.Name;

      FullName = propertyMember.Name;
      Name = DataCommon.GetMemberName(FullName);
      OverriddenName = overriddenName;

      Summary = PropertyMember.Summary;
      Returns = PropertyMember.Returns;
      Remark = DataCommon.GetDataRemark(PropertyMember.Remarks);
      IsPublic = DataCommon.IsPublic(Remark, out bool hasSyntax);
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

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataProperty other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        retValue = OverriddenName.CompareTo(other.OverriddenName);
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the public flag.</summary>
    public bool IsPublic { get; set; }

    /// <summary>Gets or sets the Property Name value.</summary>
    public string Name { get; set; }

    /// <summary>The overriden method unique name if required.</summary>
    public string OverriddenName
    {
      get { return mOverriddenName; }
      set { mOverriddenName = NetString.InitString(value); }
    }
    private string mOverriddenName;

    /// <summary>Gets or sets the Remark value.</summary>
    public DataRemark Remark { get; set; }

    /// <summary>Gets or sets the Returns value.</summary>
    public string Returns { get; set; }

    /// <summary>Gets or sets the Summary value.</summary>
    public string Summary { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the parent Assembly name value.</summary>
    public string AssemblyName { get; set; }

    // Gets or sets the Doc value.
    /// <include path='items/Doc/*' file='Doc/DataProperty.xml'/>
    public Doc Doc { get; set; }

    /// <summary>Gets or sets the object FullName value.</summary>
    public string FullName { get; set; }

    // <remarks>
    /// <include path='items/PropertyMember/*' file='Doc/DataProperty.xml'/>
    public DocMember PropertyMember { get; set; }

    /// <summary>Gets or sets the class/type name value.</summary>
    public string TypeName { get; set; }
    #endregion
  }
}
