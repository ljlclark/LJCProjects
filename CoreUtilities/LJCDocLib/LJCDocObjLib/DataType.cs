// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataType.cs
using LJCDocXMLObjLib;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDocObjLib
{
  // Represents the Class/Type documentation object.
  /// <include path='items/DataType/*' file='Doc/DataType.xml'/>
  public class DataType
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataTypeC/*' file='Doc/DataType.xml'/>
    public DataType(DataAssembly dataAssembly, DocMember typeMember
      , LJCAssemblyReflect assemblyReflect)
    {
      Doc = dataAssembly.Doc;
      TypeMember = typeMember;
      AssemblyName = dataAssembly.Name;
      AssemblyReflect = assemblyReflect;

      NamespaceValue = DataCommon.GetNamespace(TypeMember.Name);
      Name = DataCommon.GetMemberName(TypeMember.Name);

      Summary = TypeMember.Summary;
      Returns = TypeMember.Returns;
      Remark = DataCommon.GetDataRemark(TypeMember.Remarks);
      Example = DataCommon.GetDataExample(TypeMember.Example);
      DataLinks = DataCommon.GetDataLinks(TypeMember.Links);

      string typeName = $"{NamespaceValue}.{Name}";
      AssemblyReflect.SetTypeReference(typeName);

      CreateMethodsData(dataAssembly);
      CreatePropertiesData(dataAssembly);
      CreateFieldsData(dataAssembly);
    }
    #endregion

    #region Public Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{AssemblyName}.{Name}";
      return retValue;
    }

    // Creates the child fields data.
    /// <include path='items/CreateFieldsData/*' file='Doc/DataType.xml'/>
    public void CreateFieldsData(DataAssembly dataAssembly)
    {
      var fieldMembers = Doc.GetFields(NamespaceValue, Name);
      DataFields = new List<DataField>();
      foreach (DocMember fieldMember in fieldMembers)
      {
        string memberName = fieldMember.Name;
        string prefix = $"F:{NamespaceValue}.{Name}";
        string fieldName = DataCommon.GetMemberName(memberName, prefix);

        // Make sure prefix is not part of a contained type.
        // If "." then only matches beginning of fullName.
        if (-1 == fieldName.IndexOf('.'))
        {
          DataFields.Add(new DataField(dataAssembly, this, fieldMember));
        }
      }
    }

    // Creates the child methods data.
    /// <include path='items/CreateMethodsData/*' file='Doc/DataType.xml'/>
    public void CreateMethodsData(DataAssembly dataAssembly)
    {
      // Get all methods for this type.
      var methodMembers = Doc.GetMethods(NamespaceValue, Name);
      DataMethods = new DataMethods();
      foreach (DocMember methodMember in methodMembers)
      {
        string memberName = methodMember.Name;
        string prefix = $"M:{NamespaceValue}.{Name}";
        string methodName = DataCommon.GetMemberName(memberName, prefix);

        // Make sure prefix is not part of a contained type.
        // If "." then only matches beginning of memberName.
        if (-1 == methodName.IndexOf('.'))
        {
          string overloadName = DataMethods.GetOverloadName(methodName);
          DataMethod dataMethod = new DataMethod(dataAssembly, this
            , methodMember, overloadName)
          {
            AssemblyReflect = AssemblyReflect
          };
          dataMethod.SetIsPublic();
          DataMethods.Add(dataMethod);
        }
      }
    }

    // Creates the child properties data.
    /// <include path='items/CreatePropertiesData/*' file='Doc/DataType.xml'/>
    public void CreatePropertiesData(DataAssembly dataAssembly)
    {
      var propertyMembers = Doc.GetProperties(NamespaceValue, Name);
      DataProperties = new DataProperties();
      foreach (DocMember propertyMember in propertyMembers)
      {
        string memberName = propertyMember.Name;
        string prefix = $"P:{NamespaceValue}.{Name}";
        string propertyName = DataCommon.GetMemberName(memberName, prefix);

        // Make sure prefix is not part of contained type.
        // If "." then only matches beginning of fullName.
        if (-1 == propertyName.IndexOf('.'))
        {
          DataProperties.Add(new DataProperty(dataAssembly, this
            , propertyMember));
        }
      }
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or Sets the DataFields list.</summary>
    public List<DataField> DataFields { get; set; }

    /// <summary>Gets or sets the DataMethods list.</summary>
    public DataMethods DataMethods { get; set; }

    /// <summary>Gets or Sets the DataProperties list.</summary>
    public DataProperties DataProperties { get; set; }

    /// <summary>Gets or sets the Remarks value.</summary>
    public DataExample Example { get; set; }

    /// <summary>Gets or sets the DataLinks list.</summary>
    public DataLinks DataLinks { get; set; }

    /// <summary>Gets or sets the Class/Type Name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Remarks value.</summary>
    public DataRemark Remark { get; set; }

    /// <summary>Gets or sets the Returns value.</summary>
    public string Returns { get; set; }

    /// <summary>Gets or sets the Summary value.</summary>
    public string Summary { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the parent Assembly name value.</summary>
    public string AssemblyName { get; private set; }

    /// <summary>Gets or sets the LJCAssemblyReflect object.</summary>
    public LJCAssemblyReflect AssemblyReflect { get; private set; }

    // Gets or sets the Doc value.
    /// <include path='items/Doc/*' file='Doc/DataType.xml'/>
    public Doc Doc { get; private set; }

    /// <summary>Gets or sets the Namespace value.</summary>
    public string NamespaceValue { get; set; }

    // Gets or sets the TypeMember value.
    /// <include path='items/TypeMember/*' file='Doc/DataType.xml'/>
    public DocMember TypeMember { get; private set; }
    #endregion
  }
}
