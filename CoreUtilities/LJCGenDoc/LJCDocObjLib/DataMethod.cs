// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMethod.cs
using LJCDocXMLObjLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LJCDocObjLib
{
  // Represents the Method documentation data.
  /// <include path='items/DataMethod/*' file='Doc/DataMethod.xml'/>
  public class DataMethod
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataMethod()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataMethod(DataMethod item)
    {
      Example = item.Example;
      IsPublic = item.IsPublic;
      Name = item.Name;
      OverloadName = item.OverloadName;
      Params = item.Params;
      Remark = item.Remark;
      Returns = item.Returns;
      Summary = item.Summary;
    }

    // Initializes an object instance.
    /// <include path='items/DataMethodC/*' file='Doc/DataMethod.xml'/>
    public DataMethod(DataAssembly dataAssembly, DataType dataType
      , DocMember methodMember, string overloadName)
    {
      Doc = dataAssembly.Doc;
      MethodMember = methodMember;
      AssemblyName = dataAssembly.Name;
      TypeName = dataType.Name;

      MethodMemberName = methodMember.Name;
      Name = Doc.GetMemberName(MethodMemberName);
      OverloadName = overloadName;

      // Testing
      //if (Name.StartsWith("CheckArgument"))
      //{
      //  int i = 0;
      //}
      Params = DataCommon.GetDataParams(methodMember.Params);
      Exceptions = DataCommon.GetDataExceptions(methodMember.Exceptions);
      TypeParams = DataCommon.GetDataTypeParams(methodMember.TypeParams);
      Summary = methodMember.Summary;
      Returns = methodMember.Returns;
      Remark = DataCommon.GetDataRemark(methodMember.Remarks);
      Example = DataCommon.GetDataExample(MethodMember.Example);
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue;

      retValue = $"{AssemblyName}.{TypeName}.{Name}";
      return retValue;
    }

    /// <summary>Sets the IsPublic property.</summary>
    public void SetIsPublic()
    {
      // Get Remarks IsPublic.
      IsPublic = DataCommon.IsPublic(Remark, out bool hasSyntax);
      if (!hasSyntax)
      {
        // Get reflection IsPublic.
        SetMethodInfo();
        IsPublic = AssemblyReflect.IsPublic();
      }
    }
    #endregion

    #region Private Methods

    // Sets the MethodInfoValue property.
    /// <include path='items/SetMethodInfo/*' file='Doc/DataMethod.xml'/>
    private void SetMethodInfo()
    {
      if (AssemblyReflect != null)
      {
        // Create parameter name array.
        List<string> paramNames = new List<string>();

        if (Params != null)
        {
          foreach (DataParam param in Params)
          {
            paramNames.Add(param.Name);
          }
        }
        string[] parameterNames = new String[paramNames.Count];
        paramNames.CopyTo(parameterNames);

        if ("#ctor" == Name)
        {
          AssemblyReflect.SetConstructorInfo(Name, parameterNames);
        }
        else
        {
          AssemblyReflect.SetMethodInfo(Name, parameterNames);
        }
      }
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the Remarks value.</summary>
    public DataExample Example { get; set; }

    /// <summary>
    /// Gets or sets the deserialized Exceptions XML data list.
    /// </summary>
    public DataExceptions Exceptions { get; set; }

    /// <summary>Gets or sets the public flag.</summary>
    public bool IsPublic { get; set; }

    /// <summary>Gets or sets the Method Name value.</summary>
    public string Name
    {
      get { return mName; }
      set
      {
        mName = NetString.InitString(value);
        if (!NetString.HasValue(mOverloadName))
        {
          mOverloadName = mName;
        }
      }
    }
    private string mName;

    /// <summary>The overload method unique name if required.</summary>
    public string OverloadName
    {
      get { return mOverloadName; }
      set { mOverloadName = NetString.InitString(value); }
    }
    private string mOverloadName;

    /// <summary>Gets or sets the deserialized Params XML data list.</summary>
    public DataParams Params { get; set; }

    /// <summary>Gets or sets the Remarks value.</summary>
    public DataRemark Remark { get; set; }

    /// <summary>Gets or sets the Returns value.</summary>
    public string Returns { get; set; }

    /// <summary>Gets or sets the Summary value.</summary>
    public string Summary { get; set; }

    /// <summary>
    /// Gets or sets the deserialized TypeParams XML data list.
    /// </summary>
    public DataTypeParams TypeParams { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the parent Assembly name value.</summary>
    public string AssemblyName { get; set; }

    /// <summary>Gets or sets the LJCAssemblyReflect object.</summary>
    public LJCAssemblyReflect AssemblyReflect { get; set; }

    // Gets or sets the Doc value.
    /// <include path='items/Doc/*' file='Doc/DataMethod.xml'/>
    public Doc Doc { get; set; }

    /// <summary>Gets or sets the MethodInfo value.</summary>
    public MethodInfo MethodInfoValue { get; set; }

    // Gets or sets the MethodMember value.
    /// <include path='items/MethodMember/*' file='Doc/DataMethod.xml'/>
    public DocMember MethodMember { get; set; }

    /// <summary>Gets or sets the object MemberName value.</summary>
    public string MethodMemberName { get; set; }

    /// <summary>Gets or sets the class/type name value.</summary>
    public string TypeName { get; set; }
    #endregion
  }
}

