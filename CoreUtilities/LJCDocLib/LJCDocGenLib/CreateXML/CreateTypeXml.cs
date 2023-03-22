// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateTypeXml.cs
using System;
using System.IO;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocObjLib;

namespace LJCDocGenLib
{
  // Creates the Type XML Data values.
  /// <include path='items/CreateTypeXml/*' file='Doc/CreateTypeXml.xml'/>
  public partial class CreateTypeXml
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CreateTypeXmlC/*' file='Doc/CreateTypeXml.xml'/>
    public CreateTypeXml(GenRoot genRoot, GenAssembly genAssembly, DataAssembly dataAssembly
      , DataType dataType, LJCAssemblyReflect assemblyReflect)
    {
      GenRoot = genRoot;
      GenAssembly = genAssembly;
      DataAssembly = dataAssembly;
      DataType = dataType;
      AssemblyReflect = assemblyReflect;
    }
    #endregion

    #region Public Methods

    // Returns the XML Data values.
    /// <include path='items/GetXmlData/*' file='Doc/CreateTypeXml.xml'/>
    public string GetXmlData()
    {
      Section section;
      RepeatItem repeatItem;
      Replacements replacements;
      string retValue;

      Sections sections = new Sections();
      section = sections.Add("Main");
      repeatItem = section.RepeatItems.Add("Main");
      replacements = repeatItem.Replacements;
      replacements.Add("_AssemblyName_", DataAssembly.Name);
      replacements.Add("_AssemblyHtm_", GenAssembly.HTMLFileName);
      replacements.Add("_GenDate_", $"{DateTime.Now.ToShortDateString()}"
        + $" {DateTime.Now.ToShortTimeString()}");
      replacements.Add("_Namespace_", DataType.NamespaceValue);
      replacements.Add("_TypeName_", DataType.Name);

      bool hasRemarks = false;
      if (DataType.Remark != null
        && DataType.Remark.Text != null)
      {
        hasRemarks = true;
        replacements.Add("_RemarkText_", DataType.Remark.Text);
      }
      if (SetTypeRemarks())
      {
        hasRemarks = true;
        section = sections.Add("TypeRemarks");
        SetTypeRemarks(section);
      }
      if (hasRemarks)
      {
        replacements.Add("_HasRemarks_", "True");
      }

      if (false == NetString.HasValue(DataType.Summary))
      {
        GenRoot.LogMissing("Type Summary", DataType.NamespaceValue, DataType.Name);
      }
      else
      {
        replacements.Add("_Summary_", DataType.Summary);
      }
      AddSyntax(replacements);

      int publicMethodCount = MethodCount(true);
      if (publicMethodCount > 0)
      {
        replacements.Add("_PublicMethodCount_", publicMethodCount.ToString());
        section = sections.Add("PublicMethods");
        AddMethods(section, true);
      }

      int privateMethodCount = MethodCount(false);
      if (privateMethodCount > 0)
      {
        replacements.Add("_PrivateMethodCount_", privateMethodCount.ToString());
        section = sections.Add("PrivateMethods");
        AddMethods(section, false);
      }

      int propertyCount = PropertyCount();
      if (propertyCount > 0)
      {
        replacements.Add("_PropertyCount_", propertyCount.ToString());
        section = sections.Add("Properties");
        AddProperties(section);
      }

      int fieldCount = FieldCount();
      if (fieldCount > 0)
      {
        replacements.Add("_FieldCount_", fieldCount.ToString());
        section = sections.Add("Fields");
        AddFields(section);
      }

      bool hasExample = false;
      if (DataType.Example != null)
      {
        if (SetExampleRemarks())
        {
          hasExample = true;
          section = sections.Add("ExampleRemarks");
          SetExampleRemarks(section);
        }
        if (NetString.HasValue(DataType.Example.Code))
        {
          hasExample = true;
          string code = DataType.Example.Code;
          if (NetString.HasValue(code))
          {
            SyntaxHighlightHtml highlight = new SyntaxHighlightHtml();
            code = highlight.FormatCode(DataType.Name, null, code);
            replacements.Add("_Code_", code.Trim());
          }
        }
      }
      if (hasExample)
      {
        replacements.Add("_HasExample_", "True");
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

    // Adds the Field data elements.
    /// <include path='items/AddFields/*' file='Doc/CreateTypeXml.xml'/>
    private void AddFields(Section section)
    {
      RepeatItem repeatItem;
      Replacements replacements;

      GenField genField = new GenField(GenRoot, GenAssembly, DataAssembly
        , DataType, null);
      foreach (DataField dataField in DataType.DataFields)
      {
        repeatItem = section.RepeatItems.Add(dataField.Name);
        replacements = repeatItem.Replacements;

        // Create relative path.
        genField.DataField = dataField;
        string fileName = Path.Combine("Fields", genField.HTMLFileName);

        replacements.Add("_FileName_", fileName);
        replacements.Add("_FieldName_", dataField.Name);
        replacements.Add("_FieldSummary_", dataField.Summary);
      }
    }

    // Adds the Methods data elements.
    /// <include path='items/AddMethods/*' file='Doc/CreateTypeXml.xml'/>
    private void AddMethods(Section section, bool usePublic)
    {
      RepeatItem repeatItem;
      Replacements replacements;

      GenMethod genMethod = new GenMethod(GenRoot, GenAssembly, DataAssembly
        , DataType, null);
      foreach (DataMethod dataMethod in DataType.DataMethods)
      {
        bool gen = true;
        if (dataMethod.Summary != null
          && "nogen" == dataMethod.Summary.ToLower())
        {
          gen = false;
        }
        if (usePublic == dataMethod.IsPublic
          && true == gen)
        {
          repeatItem = section.RepeatItems.Add(dataMethod.Name);
          replacements = repeatItem.Replacements;

          // Create relative path.
          genMethod.DataMethod = dataMethod;
          string fileName = Path.Combine("Methods", genMethod.HTMLFileName);

          string displayString = dataMethod.Name;
          if ("#ctor" == displayString)
          {
            displayString = DataType.Name + " #ctor";
          }
          replacements.Add("_FileName_", fileName);
          replacements.Add("_MethodName_", displayString);
          replacements.Add("_MethodSummary_", dataMethod.Summary);
        }
      }
    }

    // Adds the Properties data elements.
    /// <include path='items/AddProperties/*' file='Doc/CreateTypeXml.xml'/>
    private void AddProperties(Section section)
    {
      RepeatItem repeatItem;
      Replacements replacements;

      GenProperty genProperty = new GenProperty(GenRoot, GenAssembly
        , DataAssembly, DataType, null);
      foreach (DataProperty dataProperty in DataType.DataProperties)
      {
        repeatItem = section.RepeatItems.Add(dataProperty.Name);
        replacements = repeatItem.Replacements;

        // Create relative path.
        genProperty.DataProperty = dataProperty;
        string fileName = Path.Combine("Properties", genProperty.HTMLFileName);

        replacements.Add("_FileName_", fileName);
        replacements.Add("_PropertyName_", dataProperty.Name);
        replacements.Add("_PropertySummary_", dataProperty.Summary);
      }
    }

    // Adds the Type syntax elements.
    /// <include path='items/AddSyntax/*' file='Doc/CreateTypeXml.xml'/>
    private void AddSyntax(Replacements replacements)
    {
      string syntax = DataCommon.GetSyntax(DataType.Remark
        , out bool hasSyntax);
      if (false == hasSyntax && AssemblyReflect != null)
      {
        string fullName = $"{DataType.NamespaceValue}.{DataType.Name}";
        //if (DataType.Name.Contains("Delegate"))
        //{
        //	int i = 0;
        //	fullName = $"{DataType.NamespaceValue}+{DataType.Name}";
        //}

        //if ("TextDataReader" == DataType.Name)
        //{
        //	Debugger.Break();
        //	Type[] types = AssemblyReflect.Assembly.GetTypes();
        //}

        AssemblyReflect.SetTypeReference(fullName);
        if (AssemblyReflect.TypeReference != null)
        {
          syntax = AssemblyReflect.GetTypeSyntax();
        }
      }
      if (false == NetString.HasValue(syntax))
      {
        GenRoot.LogMissing("Type Syntax", DataType.NamespaceValue, DataType.Name);
      }
      else
      {
        syntax = syntax.Replace("<", "&lt;");
        syntax = syntax.Replace(">", "&gt;");
        replacements.Add("_Syntax_", syntax);
      }
    }

    // Gets the Field count.
    /// <include path='items/FieldCount/*' file='Doc/CreateTypeXml.xml'/>
    private int FieldCount()
    {
      int retValue = 0;

      if (DataType.DataFields != null
        && DataType.DataFields.Count > 0)
      {
        foreach (DataField dataField in DataType.DataFields)
        {
          retValue++;
        }
      }
      return retValue;
    }

    // Gets the Method count.
    /// <include path='items/MethodCount/*' file='Doc/CreateTypeXml.xml'/>
    private int MethodCount(bool usePublic)
    {
      int retValue = 0;

      if (DataType.DataMethods != null
        && DataType.DataMethods.Count > 0)
      {
        foreach (DataMethod dataMethod in DataType.DataMethods)
        {
          if (usePublic == dataMethod.IsPublic)
          {
            retValue++;
          }
        }
      }
      return retValue;
    }

    // Gets the Property count.
    /// <include path='items/PropertyCount/*' file='Doc/CreateTypeXml.xml'/>
    private int PropertyCount()
    {
      int retValue = 0;

      if (DataType.DataProperties != null
        && DataType.DataProperties.Count > 0)
      {
        foreach (DataProperty dataProperty in DataType.DataProperties)
        {
          retValue++;
        }
      }
      return retValue;
    }

    // The Example Remarks elements.
    /// <include path='items/SetExampleRemarks/*' file='Doc/CreateTypeXml.xml'/>
    private bool SetExampleRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      // Only this line is different from Method.
      DataExample example = DataType.Example;

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

    // Sets the Object Remarks elements.
    /// <include path='items/SetTypeRemarks/*' file='Doc/CreateTypeXml.xml'/>
    private bool SetTypeRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      // This line is different from Method, Property and Field.
      DataRemark remark = DataType.Remark;

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
      }
      return retValue;
    }
    #endregion

    #region Properties

    private LJCAssemblyReflect AssemblyReflect { get; }
    private DataAssembly DataAssembly { get; }
    private DataType DataType { get; }
    private GenAssembly GenAssembly { get; }
    private GenRoot GenRoot { get; }
    #endregion
  }
}
