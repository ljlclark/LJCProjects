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
    public DataType(DataAssembly dataAssembly, DocMember docTypeMember
      , LJCAssemblyReflect assemblyReflect)
    {
      Doc = dataAssembly.Doc;
      DocTypeMember = docTypeMember;
      AssemblyName = dataAssembly.Name;
      AssemblyReflect = assemblyReflect;

      Summary = DocTypeMember.Summary;
      Returns = DocTypeMember.Returns;
      Remark = DataCommon.GetDataRemark(DocTypeMember.Remarks);
      Example = DataCommon.GetDataExample(DocTypeMember.Example);
      DataLinks = DataCommon.GetDataLinks(DocTypeMember.Links);

      NamespaceValue = Doc.GetNamespace(DocTypeMember.Name);
      Name = Doc.GetMemberName(DocTypeMember.Name);
      SetTypeName(NamespaceValue, Name);
      Doc.TypeName= TypeName;
      AssemblyReflect.SetTypeReference(TypeName);
      // Testing
      //if ("DbColumns" == Name)
      //{
      //  int i = 0;
      //}
      CreateMethodsData(dataAssembly);
      CreatePropertiesData(dataAssembly);
      CreateFieldsData(dataAssembly);
    }
    #endregion

    #region Data Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{AssemblyName}.{Name}";
      return retValue;
    }
    #endregion

    #region Public Methods

    // Creates the child fields data.
    /// <include path='items/CreateFieldsData/*' file='Doc/DataType.xml'/>
    public void CreateFieldsData(DataAssembly dataAssembly)
    {
      var docFieldMembers = Doc.GetFields();
      DataFields = new List<DataField>();
      foreach (DocMember fieldMember in docFieldMembers)
      {
        DataFields.Add(new DataField(dataAssembly, this, fieldMember));
      }
    }

    // Creates the child methods data.
    /// <include path='items/CreateMethodsData/*' file='Doc/DataType.xml'/>
    public void CreateMethodsData(DataAssembly dataAssembly)
    {
      // Get all methods for this type.
      var docMethodMembers = Doc.GetMethods();
      DataMethods = new DataMethods();
      foreach (DocMember docMethodMember in docMethodMembers)
      {
        var docMethodName = Doc.MethodName(docMethodMember.Name);
        var overloadName = DataMethods.GetOverloadName(docMethodName);
        DataMethod dataMethod = new DataMethod(dataAssembly, this
          , docMethodMember, overloadName)
        {
          AssemblyReflect = AssemblyReflect
        };
        // Testing
        //if ("LJCSortName" == docMethodName)
        //{
        //  int i = 0;
        //}
        dataMethod.SetIsPublic();
        DataMethods.Add(dataMethod);
      }
    }

    // Creates the child properties data.
    /// <include path='items/CreatePropertiesData/*' file='Doc/DataType.xml'/>
    public void CreatePropertiesData(DataAssembly dataAssembly)
    {
      var docPropertyMembers = Doc.GetProperties();
      DataProperties = new DataProperties();
      foreach (DocMember propertyMember in docPropertyMembers)
      {
        DataProperties.Add(new DataProperty(dataAssembly, this
          , propertyMember));
      }
    }

    // Sets the TypeName value.
    private void SetTypeName(string namespaceValue, string typeName)
    {
      TypeName = $"{namespaceValue}.{typeName}";
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

    // Gets or sets the TypeMember value.
    /// <include path='items/TypeMember/*' file='Doc/DataType.xml'/>
    public DocMember DocTypeMember { get; private set; }

    /// <summary></summary>
    public string NamespaceValue { get; set; }

    /// <summary></summary>
    public string TypeName { get; set; }
    #endregion
  }
}
