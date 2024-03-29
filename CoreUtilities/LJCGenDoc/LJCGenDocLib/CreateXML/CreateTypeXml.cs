﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateTypeXml.cs
using System;
using System.IO;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocObjLib;
using LJCGenDocDAL;
using Section = LJCGenTextLib.Section;
using System.Collections.Generic;

namespace LJCGenDocLib
{
  // Creates the Type XML Data values.
  /// <include path='items/CreateTypeXml/*' file='Doc/CreateTypeXml.xml'/>
  public class CreateTypeXml
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
      mGenMethod = new GenMethod(GenRoot, GenAssembly, DataAssembly
        , DataType, null);
    }
    #endregion

    #region Public Methods

    // Returns the XML Data values.
    /// <include path='items/GetXmlData/*' file='Doc/CreateTypeXml.xml'/>
    public string GetXmlData()
    {
      string retValue;

      Sections sections = new Sections();
      var mainSection = sections.Add("Main");
      var repeatItem = mainSection.RepeatItems.Add("Main");
      var mainReplacements = repeatItem.Replacements;
      mainReplacements.Add("_AssemblyName_", DataAssembly.Name);
      mainReplacements.Add("_AssemblyHtm_", GenAssembly.HTMLFileName);
      var copyRight = "Copyright &copy Lester J. Clark and Contributors.<br />\r\n";
      copyRight += "Licensed under the MIT License.";
      mainReplacements.Add("_Copyright_", copyRight);

      int fieldCount = FieldCount();
      if (fieldCount > 0)
      {
        mainReplacements.Add("_FieldCount_", fieldCount.ToString());
        var section = sections.Add("Fields");
        AddFields(section);
      }

      mainReplacements.Add("_GenDate_", $"{DateTime.Now.ToShortDateString()}"
        + $" {DateTime.Now.ToShortTimeString()}");

      AddLinks(sections, mainReplacements);

      bool hasRemarks = false;
      if (DataType.Remark != null
        && DataType.Remark.Text != null)
      {
        hasRemarks = true;
        mainReplacements.Add("_RemarkText_", DataType.Remark.Text);
      }
      if (SetTypeRemarks())
      {
        hasRemarks = true;
        var remarksSection = sections.Add("TypeRemarks");
        SetTypeRemarks(remarksSection);
      }
      if (hasRemarks)
      {
        mainReplacements.Add("_HasRemarks_", "True");
      }
      mainReplacements.Add("_Namespace_", DataType.NamespaceValue);

      if (!NetString.HasValue(DataType.Summary))
      {
        GenRoot.LogMissing("Type Summary", DataType.NamespaceValue, DataType.Name);
      }
      else
      {
        mainReplacements.Add("_Summary_", DataType.Summary);
      }
      AddSyntax(mainReplacements);
      mainReplacements.Add("_TypeName_", DataType.Name);

      string methodListPreface = "";
      mOtherMethods = new DataMethods(DataType.DataMethods);
      // Testing
      //if ("NetCommon" == DataType.Name)
      //{
      //  int i = 0;
      //}
      if (AddMethodGroups(sections))
      {
        methodListPreface = "Other ";
      }
      mainReplacements.Add("_MethodListPreface_", methodListPreface);

      int otherPublicCount = MethodCount(mOtherMethods, true);
      if (otherPublicCount > 0)
      {
        mainReplacements.Add("_OtherMethodCount_", mOtherMethods.Count.ToString());
        var section = sections.Add("PublicMethods");
        AddMethods(section, true);
      }

      int publicMethodCount = MethodCount(DataType.DataMethods, true);
      if (publicMethodCount > 0)
      {
        mainReplacements.Add("_PublicMethodCount_", publicMethodCount.ToString());
      }

      int privateMethodCount = MethodCount(DataType.DataMethods);
      if (privateMethodCount > 0)
      {
        mainReplacements.Add("_PrivateMethodCount_", privateMethodCount.ToString());
        var section = sections.Add("PrivateMethods");
        AddMethods(section, false);
      }

      int propertyCount = PropertyCount();
      if (propertyCount > 0)
      {
        mainReplacements.Add("_PropertyCount_", propertyCount.ToString());
        var section = sections.Add("Properties");
        AddProperties(section);
      }

      bool hasExample = false;
      if (DataType.Example != null)
      {
        if (SetExampleRemarks())
        {
          hasExample = true;
          var section = sections.Add("ExampleRemarks");
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
            mainReplacements.Add("_Code_", code.Trim());
          }
        }
      }
      if (hasExample)
      {
        mainReplacements.Add("_HasExample_", "True");
      }

      retValue = NetCommon.XmlSerializeToString(sections.GetType()
        , sections, null);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Adds the Field data elements.
    // <include path='items/AddFields/*' file='Doc/CreateTypeXml.xml'/>
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

    // Create the Method Groups
    // <include path='items/AddMethodGroups/*' file='Doc/CreateTypeXml.xml'/>
    private bool AddMethodGroups(Sections sections)
    {
      bool retValue = false;

      var managers = ValuesGenDoc.Instance.Managers;

      // Get the DocAssembly data.
      var docAssemblyManager = managers.DocAssemblyManager;
      var docAssembly = docAssemblyManager.RetrieveWithName(DataAssembly.Name);

      if (docAssembly != null)
      {
        var classManager = managers.DocClassManager;
        var docClass = classManager.RetrieveWithUnique(docAssembly.ID
          , DataType.Name);

        // Get the DocMethodGroups for the DocClass.
        if (docClass != null)
        {
          var methodGroupManager = managers.DocMethodGroupManager;
          var orderColumns = new List<string>()
          {
            DocMethodGroup.ColumnSequence
          };
          methodGroupManager.SetOrderBy(orderColumns);
          var methodGroups = methodGroupManager.LoadWithParentID(docClass.ID);
          if (NetCommon.HasItems(methodGroups))
          {
            var section = sections.Add("MethodGroups");
            var repeatItems = section.RepeatItems;
            foreach (var methodGroup in methodGroups)
            {
              // Get the DocMethods for the DocMethodGroup.
              var methodManager = managers.DocMethodManager;
              orderColumns = new List<string>()
              {
                DocMethod.ColumnSequence
              };
              methodManager.SetOrderBy(orderColumns);
              var docMethods
                = methodManager.LoadWithGroup(methodGroup.ID);
              if (NetCommon.HasItems(docMethods)
                && HasValidMethods(docMethods))
              {
                retValue = true;
                var repeatItem = repeatItems.Add(methodGroup.HeadingName);
                var heading = methodGroup.HeadingName;
                if (methodGroup.HeadingTextCustom != null)
                {
                  heading = methodGroup.HeadingTextCustom;
                }
                repeatItem.Replacements.Add("_Heading_", heading);

                repeatItem.Subsection = new Section("Subsection");
                var subRepeatItems = repeatItem.Subsection.RepeatItems;
                DataMethod dataMethod = null;
                foreach (var docMethod in docMethods)
                {
                  var methodName = docMethod.Name;
                  var overloadName = docMethod.OverloadName;
                  // *** Begin *** Change - 9/26/23 #Overload
                  if (NetString.HasValue(overloadName))
                  {
                    dataMethod
                      = mOtherMethods.Find(x => x.OverloadName == overloadName);
                  }
                  else
                  {
                    dataMethod
                      = mOtherMethods.Find(x => x.Name == methodName);
                  }
                  // *** End   *** Change - 9/26/23 #Overload
                  if (dataMethod != null)
                  {
                    // Create relative path.
                    mGenMethod.DataMethod = dataMethod;
                    string fileName = Path.Combine("Methods", mGenMethod.HTMLFileName);

                    mOtherMethods.Remove(dataMethod);
                    var subRepeatItem = subRepeatItems.Add(methodName);
                    var subReplacements = subRepeatItem.Replacements;
                    subReplacements.Add("_HTMLFileName_", fileName);
                    subReplacements.Add("_Name_", methodName);
                    subReplacements.Add("_Summary_", docMethod.Description);
                  }
                }
              }
            }
          }
        }
      }
      return retValue;
    }

    // Adds the Methods data elements.
    // <include path='items/AddMethods/*' file='Doc/CreateTypeXml.xml'/>
    private void AddMethods(Section section, bool usePublic)
    {
      RepeatItem repeatItem;
      Replacements replacements;

      foreach (DataMethod dataMethod in mOtherMethods)
      {
        bool gen = true;
        if (dataMethod.Summary != null
          && "nogen" == dataMethod.Summary.ToLower())
        {
          gen = false;
        }
        if (usePublic == dataMethod.IsPublic
          && gen)
        {
          repeatItem = section.RepeatItems.Add(dataMethod.Name);
          replacements = repeatItem.Replacements;

          // Create relative path.
          mGenMethod.DataMethod = dataMethod;
          string fileName = Path.Combine("Methods", mGenMethod.HTMLFileName);

          string displayString = dataMethod.Name;
          //if ("#ctor" == displayString)
          //{
          //  displayString = DataType.Name + " #ctor";
          //}
          replacements.Add("_FileName_", fileName);
          replacements.Add("_MethodName_", displayString);
          replacements.Add("_MethodSummary_", dataMethod.Summary);
        }
      }
    }

    // Adds the Link data elements.
    // <include path='items/AddLinks/*' file='Doc/CreateTypeXml.xml'/>
    private void AddLinks(Sections sections, Replacements mainReplacements)
    {
      if (NetCommon.HasItems(DataType.DataLinks))
      {
        Section section = null;
        foreach (DataLink dataLink in DataType.DataLinks)
        {
          var fileSpec = dataLink.FileName;
          var text = dataLink.Text;
          if (!text.StartsWith("--"))
          {
            if (null == section)
            {
              mainReplacements.Add("_HasLinks_", "true");
              section = sections.Add("Links");
            }

            var fileName = Path.GetFileNameWithoutExtension(fileSpec);
            var repeatItem = section.RepeatItems.Add(fileName);
            var replacements = repeatItem.Replacements;
            replacements.Add("_LinkFile_", fileSpec);
            if (!NetString.HasValue(text))
            {
              var errorText = $"{DataType.NamespaceValue}.{DataType.Name}";
              GenRoot.LogMissing("Link FileName", errorText, fileSpec);
            }
            replacements.Add("_LinkText_", text);
          }
        }
      }
    }

    // Adds the Properties data elements.
    // <include path='items/AddProperties/*' file='Doc/CreateTypeXml.xml'/>
    private void AddProperties(Section section)
    {
      GenProperty genProperty = new GenProperty(GenRoot, GenAssembly
        , DataAssembly, DataType, null);
      foreach (DataProperty dataProperty in DataType.DataProperties)
      {
        var repeatItem = section.RepeatItems.Add(dataProperty.Name);
        var replacements = repeatItem.Replacements;

        // Create relative path.
        genProperty.DataProperty = dataProperty;
        string fileName = Path.Combine("Properties", genProperty.HTMLFileName);

        replacements.Add("_FileName_", fileName);
        replacements.Add("_PropertyName_", dataProperty.Name);
        replacements.Add("_PropertySummary_", dataProperty.Summary);
      }
    }

    // Adds the Type syntax element.
    // <include path='items/AddSyntax/*' file='Doc/CreateTypeXml.xml'/>
    private void AddSyntax(Replacements replacements)
    {
      string syntax = DataCommon.GetSyntax(DataType.Remark
        , out bool hasSyntax);
      if (!hasSyntax && AssemblyReflect != null)
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
      if (!NetString.HasValue(syntax))
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
    // <include path='items/FieldCount/*' file='Doc/CreateTypeXml.xml'/>
    private int FieldCount()
    {
      int retValue = 0;

      if (DataType.DataFields != null)
      {
        retValue = DataType.DataFields.Count;
      }
      return retValue;
    }

    // Check for valid methods.
    private bool HasValidMethods(DocMethods docMethods)
    {
      bool retValue = false;

      foreach (var docMethod in docMethods)
      {
        var dataType = mOtherMethods.Find(x => x.Name == docMethod.Name);
        if (dataType != null)
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Gets the Method count.
    // <include path='items/MethodCount/*' file='Doc/CreateTypeXml.xml'/>
    private int MethodCount(DataMethods dataMethods, bool usePublic = false)
    {
      int retValue = 0;

      if (NetCommon.HasItems(dataMethods))
      {
        foreach (DataMethod dataMethod in dataMethods)
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
    // <include path='items/PropertyCount/*' file='Doc/CreateTypeXml.xml'/>
    private int PropertyCount()
    {
      int retValue = 0;

      if (DataType.DataProperties != null)
      {
        retValue = DataType.DataProperties.Count;
      }
      return retValue;
    }

    // The Example Remarks elements.
    // <include path='items/SetExampleRemarks/*' file='Doc/CreateTypeXml.xml'/>
    private bool SetExampleRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      DataExample example = DataType.Example;

      if (example != null
        && NetCommon.HasItems(example.Paras))
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
    // <include path='items/SetTypeRemarks/*' file='Doc/CreateTypeXml.xml'/>
    private bool SetTypeRemarks(Section section = null)
    {
      bool retValue = false;

      DataRemark remark = DataType.Remark;
      if (remark != null
        && NetCommon.HasItems(remark.Paras))
      {
        bool showParas = true;
        foreach (DataPara para in remark.Paras)
        {
          if (para.Text != null
            && para.Text.Contains("--"))
          {
            // Do not show the remaining paragraphs.
            // Do not show the "--" paragraph.
            showParas = false;
          }

          if (showParas
            && NetString.HasValue(para.Text))
          {
            retValue = true;
            if (null == section)
            {
              break;
            }
            var repeatItem = section.RepeatItems.Add("Para");
            var replacements = repeatItem.Replacements;
            replacements.Add("_Para_", para.Text);
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

    // Gets the DataType reference.
    private DataType DataType { get; }

    // Gets the GenAssembly reference.
    private GenAssembly GenAssembly { get; }

    // Gets the GenRoot reference.
    private GenRoot GenRoot { get; }
    #endregion

    private readonly GenMethod mGenMethod;
    private DataMethods mOtherMethods;
  }
}
