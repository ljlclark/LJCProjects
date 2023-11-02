// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateFieldXml.cs
using System;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocObjLib;
using Section = LJCGenTextLib.Section;

namespace LJCGenDocLib
{
  // Creates the Field XML Data values.
  /// <include path='items/CreateFieldXml/*' file='Doc/CreateFieldXml.xml'/>
  public partial class CreateFieldXml
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CreateFieldXmlC/*' file='Doc/CreateFieldXml.xml'/>
    public CreateFieldXml(GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, DataField dataField, LJCAssemblyReflect assemblyReflect)
    {
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      DataField = dataField;
      AssemblyReflect = assemblyReflect;
    }
    #endregion

    #region Public Methods

    // Returns the XML Data values.
    /// <include path='items/GetXmlData/*' file='Doc/CreateFieldXml.xml'/>
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

      replacements.Add("_Description_", "Field");
      replacements.Add("_FieldName_", DataField.Name);

      // Syntax
      AddSyntax(replacements);
      if (false == NetString.HasValue(DataField.Summary))
      {
        string text = $"{DataType.NamespaceValue}.{DataType.Name}";
        GenRoot.LogMissing("Field Summary", text, DataField.Name);
      }
      else
      {
        replacements.Add("_Summary_", DataField.Summary);
      }

      // FieldRemarks Section
      bool hasRemarks = false;
      if (DataField.Remark != null
        && DataField.Remark.Text != null)
      {
        hasRemarks = true;
        replacements.Add("_RemarkText_", DataField.Remark.Text);
      }
      if (SetFieldRemarks())
      {
        hasRemarks = true;
        section = sections.Add("FieldRemarks");
        SetFieldRemarks(section);
      }
      if (hasRemarks)
      {
        replacements.Add("_HasRemarks_", "True");
      }

      var copyRight = "Copyright &copy Lester J. Clark and Contributors.<br />\r\n";
      copyRight += "Licensed under the MIT License.";
      replacements.Add("_Copyright_", copyRight);
      retValue = NetCommon.XmlSerializeToString(sections.GetType()
        , sections, null);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Adds the Field syntax elements.
    // <include path='items/AddSyntax/*' file='Doc/CreateFieldXml.xml'/>
    private void AddSyntax(Replacements replacements)
    {
      string syntax;

      if (DataField.Name != "components")
      {
        syntax = DataCommon.GetSyntax(DataField.Remark, out bool hasSyntax);
        if (false == hasSyntax
         && AssemblyReflect != null)
        {
          SetFieldInfo();
          syntax = AssemblyReflect.GetFieldSyntax();
        }
        if (false == NetString.HasValue(syntax))
        {
          string text = $"{DataType.NamespaceValue}.{DataType.Name}";
          GenRoot.LogMissing("Field Syntax", text, DataField.Name);
        }
        else
        {
          syntax = syntax.Replace("<", "&lt;");
          syntax = syntax.Replace(">", "&gt;");
          replacements.Add("_Syntax_", syntax);
        }
      }
    }

    // Sets the FieldInfo property.
    // <include path='items/SetFieldInfo/*' file='Doc/CreateFieldXml.xml'/>
    private void SetFieldInfo()
    {
      AssemblyReflect?.SetFieldInfo(DataField.Name);
    }

    // Sets the Object Remarks elements.
    // <include path='items/SetFieldRemarks/*' file='Doc/CreateFieldXml.xml'/>
    private bool SetFieldRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      // This line is different from Type, Method and Property.
      DataRemark remark = DataField.Remark;

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
              repeatItem = section.RepeatItems.Add("");
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

    // Gets the AssemblyReflect reference.
    private LJCAssemblyReflect AssemblyReflect { get; }

    // Gets the DataAssembly reference.
    private DataAssembly DataAssembly { get; }

    // Gets the DataField reference.
    private DataField DataField { get; }

    // Gets the DataType reference.
    private DataType DataType { get; }

    // Gets the GenAssembly reference.
    private GenAssembly GenAssembly { get; }
    #endregion
  }
}
