// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreatePropertyXml.cs
using System;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocObjLib;
using Section = LJCGenTextLib.Section;

namespace LJCGenDocLib
{
  // Creates the Property XML Data values.
  /// <include path='items/CreatePropertyXml/*' file='Doc/CreatePropertyXml.xml'/>
  public partial class CreatePropertyXml
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CreatePropertyXmlC/*' file='Doc/CreatePropertyXml.xml'/>
    public CreatePropertyXml(GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, DataProperty dataProperty, LJCAssemblyReflect assemblyReflect)
    {
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      DataProperty = dataProperty;
      AssemblyReflect = assemblyReflect;
    }
    #endregion

    #region Public Methods

    // Returns the XML Data values.
    /// <include path='items/GetXmlData/*' file='Doc/CreatePropertyXml.xml'/>
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

      replacements.Add("_Description_", "Property");
      replacements.Add("_PropertyName_", DataProperty.Name);

      // Syntax
      AddSyntax(replacements);
      if (!NetString.HasValue(DataProperty.Summary))
      {
        string text = $"{DataType.NamespaceValue}.{DataType.Name}";
        GenRoot.LogMissing("Property Summary", text, DataProperty.Name);
      }
      else
      {
        replacements.Add("_Summary_", DataProperty.Summary);
      }

      // PropertyRemarks Section
      bool hasRemarks = false;
      if (DataProperty.Remark != null
        && DataProperty.Remark.Text != null)
      {
        hasRemarks = true;
        replacements.Add("_RemarkText_", DataProperty.Remark.Text);
      }
      if (SetPropertyRemarks())
      {
        hasRemarks = true;
        section = sections.Add("PropertyRemarks");
        SetPropertyRemarks(section);
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

    // Adds the Property syntax elements.
    // <include path='items/AddSyntax/*' file='Doc/CreatePropertyXml.xml'/>
    private void AddSyntax(Replacements replacements)
    {
      if (DataType.Name != "Resources")
      {
        string syntax = DataCommon.GetSyntax(DataProperty.Remark
          , out bool hasSyntax);
        if (!hasSyntax
         && AssemblyReflect != null)
        {
          SetPropertyInfo();
          syntax = AssemblyReflect.GetPropertySyntax();
        }
        if (!NetString.HasValue(syntax))
        {
          string text = $"{DataType.NamespaceValue}.{DataType.Name}";
          GenRoot.LogMissing("Property Syntax", text, DataProperty.Name);
        }
        else
        {
          syntax = syntax.Replace("<", "&lt;");
          syntax = syntax.Replace(">", "&gt;");
          replacements.Add("_Syntax_", syntax);
        }
      }
    }

    // Sets the PropertyInfo property.
    // <include path='items/SetPropertyInfo/*' file='Doc/CreatePropertyXml.xml'/>
    private void SetPropertyInfo()
    {
      AssemblyReflect?.SetPropertyInfo(DataProperty.Name
        , DataProperty.PropertyMemberName);
    }

    // Sets the Object Remarks elements.
    // <include path='items/SetPropertyRemarks/*' file='Doc/CreatePropertyXml.xml'/>
    private bool SetPropertyRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      // This line is different from Type, Method and Field.
      DataRemark remark = DataProperty.Remark;

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
            if (!para.Text.Trim().StartsWith("Syntax:")
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

    // Gets the AssemblyReflect reference.
    private LJCAssemblyReflect AssemblyReflect { get; }

    // Gets the DataAssembly reference.
    private DataAssembly DataAssembly { get; }

    // Gets the DataProperty reference.
    private DataProperty DataProperty { get; }

    // Gets the DataType reference.
    private DataType DataType { get; }

    // Gets the GenAssembly reference.
    private GenAssembly GenAssembly { get; }
    #endregion
  }
}
