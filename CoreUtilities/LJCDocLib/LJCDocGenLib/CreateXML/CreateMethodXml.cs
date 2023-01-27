// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateMethodXml.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocObjLib;

namespace LJCDocGenLib
{
  // Creates the Method XML Data values.
  /// <include path='items/CreateMethodXml/*' file='Doc/CreateMethodXml.xml'/>
  public partial class CreateMethodXml
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CreateMethodXmlC/*' file='Doc/CreateMethodXml.xml'/>
    public CreateMethodXml(GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, DataMethod dataMethod, LJCAssemblyReflect assemblyReflect)
    {
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      DataMethod = dataMethod;
      AssemblyReflect = assemblyReflect;
    }
    #endregion

    #region Public Methods

    // Returns the XML Data values.
    /// <include path='items/GetXmlData/*' file='Doc/CreateMethodXml.xml'/>
    public string GetXmlData()
    {
      Section section;
      RepeatItem repeatItem;
      Replacements replacements;
      string retValue;

      // Main section.
      Sections sections = new Sections();
      section = sections.Add("Main");
      repeatItem = section.RepeatItems.Add("Main");
      replacements = repeatItem.Replacements;
      replacements.Add("_AssemblyName_", DataAssembly.Name);
      string assemblyHtml = $"..\\{GenAssembly.HTMLFileName}";
      replacements.Add("_AssemblyHtm_", assemblyHtml);
      replacements.Add("_Namespace_", DataType.NamespaceValue);
      replacements.Add("_TypeName_", DataType.Name);
      replacements.Add("_GenDate_", $"{DateTime.Now.ToShortDateString()}"
        + $" {DateTime.Now.ToShortTimeString()}");

      string description = "Method";
      string displayString = DataMethod.Name;
      if ("#ctor" == displayString)
      {
        description = "Constructor";
        displayString = DataType.Name;
      }
      replacements.Add("_Description_", description);
      replacements.Add("_MethodName_", displayString);

      // Parameters Section
      if (DataMethod.Params != null
        && DataMethod.Params.Count > 0)
      {
        replacements.Add("_HasParams_", "true");
        section = sections.Add("Parameters");
        foreach (DataParam param in DataMethod.Params)
        {
          repeatItem = section.RepeatItems.Add(param.Name);
          Replacements paramReplacements = repeatItem.Replacements;
          paramReplacements.Add("_ParameterName_", param.Name);
          if (false == NetString.HasValue(param.Text))
          {
            var text = $"{DataType.NamespaceValue}.{DataType.Name}.{DataMethod.Name}";
            GenRoot.LogMissing("Parameter Text", text, param.Name);
          }
          paramReplacements.Add("_ParameterText_", param.Text);
        }
      }

      if (NetString.HasValue(DataMethod.Returns))
      {
        replacements.Add("_Returns_", DataMethod.Returns);
      }

      // Syntax and Summary
      AddSyntax(replacements);
      if (false == NetString.HasValue(DataMethod.Summary))
      {
        string text = $"{DataType.NamespaceValue}.{DataType.Name}";
        GenRoot.LogMissing("Method Summary", text, DataMethod.Name);
      }
      else
      {
        replacements.Add("_Summary_", DataMethod.Summary);
      }

      // MethodRemarks Section
      bool hasRemarks = false;
      if (DataMethod.Remark != null
        && DataMethod.Remark.Text != null)
      {
        hasRemarks = true;
        replacements.Add("_RemarkText_", DataMethod.Remark.Text);
      }
      if (SetMethodRemarks())
      {
        hasRemarks = true;
        section = sections.Add("MethodRemarks");
        SetMethodRemarks(section);
      }
      if (hasRemarks)
      {
        replacements.Add("_HasRemarks_", "True");
      }

      // Example Section
      bool hasExample = false;
      if (DataMethod.Example != null)
      {
        if (SetExampleRemarks())
        {
          hasExample = true;
          section = sections.Add("ExampleRemarks");
          SetExampleRemarks(section);
        }
        if (NetString.HasValue(DataMethod.Example.Code))
        {
          hasExample = true;
          string code = DataMethod.Example.Code;
          if (NetString.HasValue(code))
          {
            SyntaxHighlightHtml highlight = new SyntaxHighlightHtml();
            code = highlight.FormatCode(DataType.Name, DataMethod.Name, code);
            // *** Next Statement *** Change - 5/24
            replacements.Add("_Code_", code.Trim());
          }
        }
      }
      if (hasExample)
      {
        replacements.Add("_HasExample_", "True");
      }

      replacements.Add("_Copyright_"
      , "Copyright &copy; Lester J. Clark 2021,2022 - All Rights Reserved");
      retValue = NetCommon.XmlSerializeToString(sections.GetType()
        , sections, null);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Adds the Method syntax elements.
    /// <include path='items/AddSyntax/*' file='Doc/CreateMethodXml.xml'/>
    private void AddSyntax(Replacements replacements)
    {
      string syntax = DataCommon.GetSyntax(DataMethod.Remark
        , out bool hasSyntax);
      bool isConstructor = false;
      if (false == hasSyntax
       && AssemblyReflect != null)
      {
        SetMethodInfo();
        if (AssemblyReflect.ConstructorInfo != null)
        {
          isConstructor = true;
          syntax = AssemblyReflect.GetConstructorSyntax();
        }
        else
        {
          syntax = AssemblyReflect.GetMethodSyntax();
        }
      }
      if (isConstructor || AssemblyReflect.IsNotProperty(AssemblyReflect.MethodInfo))
      {
        if (false == NetString.HasValue(syntax))
        {
          string text = $"{DataType.NamespaceValue}.{DataType.Name}";
          GenRoot.LogMissing("Method Syntax", text, DataMethod.Name);
        }
        else
        {
          syntax = syntax.Replace("<", "&lt;");
          syntax = syntax.Replace(">", "&gt;");
          replacements.Add("_Syntax_", syntax);
        }
      }
    }

    // The Example Remarks elements.
    /// <include path='items/SetExampleRemarks/*' file='Doc/CreateMethodXml.xml'/>
    private bool SetExampleRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      // Only this line is different from Type.
      DataExample example = DataMethod.Example;

      if (example != null
        && (example.Paras != null && example.Paras.Count > 0))
      {
        foreach (DataPara para in example.Paras)
        {
          if (NetString.HasValue(para.Text))
          {
            retValue = true;
            if (null == section)
            {
              break;
            }
            repeatItem = section.RepeatItems.Add("Para");
            replacements = repeatItem.Replacements;
            replacements.Add("_Para_", para.Text);
          }
        }
      }
      return retValue;
    }

    // Sets the MethodInfo property.
    /// <include path='items/SetMethodInfo/*' file='Doc/CreateMethodXml.xml'/>
    private void SetMethodInfo()
    {
      if (AssemblyReflect != null)
      {
        // Create parameter name array.
        List<string> paramNames = new List<string>();

        if (DataMethod.Params != null)
        {
          foreach (DataParam param in DataMethod.Params)
          {
            paramNames.Add(param.Name);
          }
        }
        string[] parameterNames = new String[paramNames.Count];
        paramNames.CopyTo(parameterNames);

        if ("#ctor" == DataMethod.Name)
        {
          AssemblyReflect.SetConstructorInfo(DataMethod.Name, parameterNames);
        }
        else
        {
          AssemblyReflect.SetMethodInfo(DataMethod.Name, parameterNames);
        }
      }
    }

    // Sets the Object Remarks elements.
    /// <include path='items/SetMethodRemarks/*' file='Doc/CreateMethodXml.xml'/>
    private bool SetMethodRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      // This line is different from Type, Property and Field.
      DataRemark remark = DataMethod.Remark;

      if (remark != null
        && (remark.Paras != null && remark.Paras.Count > 0))
      {
        bool showGroups = true;
        foreach (DataPara para in remark.Paras)
        {
          if (para.Text != null && para.Text.Contains("--"))
          {
            // Do not show the "--" paragraph.
            // Do not show the remaining paragraphs.
            showGroups = false;
          }

          if (showGroups)
          {
            // This line is different from Type.
            if (false == para.Text.Trim().StartsWith("Syntax:")
              && NetString.HasValue(para.Text))
            {
              retValue = true;
              if (null == section)
              {
                break;
              }
              repeatItem = section.RepeatItems.Add("Para");
              replacements = repeatItem.Replacements;
              replacements.Add("_Para_", para.Text);
            }
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    private LJCAssemblyReflect AssemblyReflect { get; }
    private DataAssembly DataAssembly { get; }
    private DataMethod DataMethod { get; }
    private DataType DataType { get; }
    private GenAssembly GenAssembly { get; }
    #endregion
  }
}
