// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCGenType.cs
using LJCDocDataDAL;
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenDataGenLib
{
  // Provides methods to create the type (class) GenData.
  /// <include file='Doc/LJCGenType.xml'
  ///  path='members/LJCGenType/*'/>
  public class LJCGenType
  {
    #region Properties

    // Gets or sets the GenDoc configuration.
    /// <include file='Doc/LJCGenType.xml'
    ///  path='members/GenDocConfig/*'/>
    public LJCGenDocConfig GenDocConfig { get; set; }

    // Gets or sets the HTML folder name.
    /// <include file='Doc/LJCGenType.xml'
    ///  path='members/HTMLFolderName/*'/>
    string HTMLFolderName { get; set; }

    // Gets or sets the HTML file name.
    /// <include file='Doc/LJCGenType.xml'
    ///  path='members/HTMLFileName/*'/>
    string HTMLFileName { get; set; }

    // Gets or sets the HTML file specification.
    /// <include file='Doc/LJCGenType.xml'
    ///  path='members/HTMLFileSpec/*'/>
    string HTMLFileSpec { get; set; }
    #endregion

    #region Class Values
    private LJCDocDataMethods RemainingMethods { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCGenType.xml'
    ///  path='members/Constructor/*'/>
    public LJCGenType(LJCGenDocConfig config, LJCDocDataClass docDataClass)
    {
      GenDocConfig = config;
      HTMLFolderName = Path.Combine(config.OutputPath
        , $@"HTML\{docDataClass.Name}");
      HTMLFileName = $"{docDataClass.Name}.html";
      HTMLFileSpec = Path.Combine(HTMLFolderName, HTMLFileName);
    }
    #endregion

    #region Methods

    // Creates the Class GenData.
    /// <include file='Doc/LJCGenType.xml'
    ///  path='members/GenDataClass/*'/>
    public Sections GenDataClass(LJCDocDataClass docDataClass)
    {
      var retSections = new Sections();

      var section = retSections.Add("Main");
      var item = section.RepeatItems.Add(docDataClass.Name);
      var replacements = item.Replacements;

      replacements.Add("_GenDate_", DateTime.Now.ToShortDateString());
      replacements.Add("_AssemblyName_", "AssemblyName");
      replacements.Add("_AssemblyHtm_", "AssemblyHtml");
      replacements.Add("_Namespace_", docDataClass.Namespace);

      replacements.Add("_TypeName_", docDataClass.Name);
      replacements.Add("_Summary_", docDataClass.Summary);
      replacements.Add("_Syntax_", docDataClass.Syntax);

      if (NetString.HasValue(docDataClass.Remarks))
      {
        replacements.Add("_RemarkText_", docDataClass.Remarks);
        replacements.Add("_HasRemarks_", "true");
      }
      if (NetString.HasValue(docDataClass.Code))
      {
        replacements.Add("_Code_", docDataClass.Code);
        replacements.Add("_HasExample_", "true");
      }

      replacements.Add("_MethodListPreface_", "");
      if (NetCommon.HasListItems(docDataClass.Methods))
      {
        RemainingMethods = docDataClass.Methods;
        CreateGroupMethods(retSections, docDataClass);

        var methodCount = docDataClass.Methods.Count.ToString();
        replacements.Add("_OtherMethodCount_", methodCount);
        CreateMethods(retSections, docDataClass);
      }

      if (NetCommon.HasListItems(docDataClass.Properties))
      {
        var propertyCount = docDataClass.Properties.Count.ToString();
        replacements.Add("_PropertyCount_", propertyCount);
        CreateProperties(retSections, docDataClass);
      }

      var copyright = "Copyright (c) Lester J. Clark and Contributors.<br />";
      copyright += "Licensed under the MIT License.";
      replacements.Add("_Copyright_", copyright);
      return retSections;
    }

    private void CreateGroupMethods(Sections sections
      , LJCDocDataClass docDataClass)
    {
      Section groupSection = null;
      RepeatItems groupItems = null;
      var hasRemaining = false;
      if (NetCommon.HasListItems(RemainingMethods))
      {
        groupSection = sections.Add("MethodGroups");
        groupItems = groupSection.RepeatItems;
        hasRemaining = true;
      }

      while (hasRemaining)
      {
        foreach (var group in docDataClass.Groups)
        {
          var groupItem = groupItems.Add(group.Name);
          var replacements = groupItem.Replacements;
          replacements.Add("_Heading_", group.Name);
          var subsection = new Section("subsection");
          groupItems[0].Subsection = subsection;
          var subItems = subsection.RepeatItems;

          var methods = RemainingMethods.FindAll(method
            => method.ParentGroup == group.Name);
          if (NetCommon.HasListItems(methods))
          {
            var subItem = subItems.Add(group.Name);
            foreach (var method in methods)
            {
              subItem.Replacements.Add("_Name_", method.Name);
              subItem.Replacements.Add("_Summary_", method.Summary);
              RemainingMethods.Remove(method);
              hasRemaining = NetCommon.HasListItems(RemainingMethods);
            }
          }
        }
        hasRemaining = false;
      }
    }

    // Creates the Methods GenData.
    private void CreateMethods(Sections sections, LJCDocDataClass docDataClass)
    {
      if (NetCommon.HasListItems(docDataClass.Methods))
      {
        var section = sections.Add("PublicMethods");
        foreach (var method in docDataClass.Methods)
        {
          var item = section.RepeatItems.Add(method.Name);
          var replacements = item.Replacements;

          replacements.Add("_AssemblyName_", "AssemblyName");
          replacements.Add("_AssemblyHtm_", "AssemblyHtml");
          replacements.Add("_Namespace_", "Namespace");
          //replacements.Add("_TypeName_", method.TypeName);

          replacements.Add("_MethodName_", method.Name);
          replacements.Add("_MethodSummary_", method.Summary);
          replacements.Add("_Returns_", method.Returns);
          replacements.Add("_RemarkText_", method.Remarks);
          replacements.Add("_Syntax_", method.Syntax);

          replacements.Add("_GenDate_", DateTime.Now.ToShortDateString());
          if (NetCommon.HasListItems(method.Params))
          {
            replacements.Add("_HasParams_", "true");
          }
          if (NetString.HasValue(docDataClass.Remarks))
          {
            replacements.Add("_RemarkText_", docDataClass.Remarks);
            replacements.Add("_HasRemarks_", "true");
          }
          if (NetString.HasValue(docDataClass.Code))
          {
            replacements.Add("_Code_", docDataClass.Code);
            replacements.Add("_HasExample_", "true");
          }
        }
      }
    }

    // Creates the Properties GenData.
    private void CreateProperties(Sections sections
      , LJCDocDataClass docDataClass)
    {
      if (NetCommon.HasListItems(docDataClass.Properties))
      {
        var section = sections.Add("Properties");
        foreach (var property in docDataClass.Properties)
        {
          var item = section.RepeatItems.Add(property.Name);
          var replacements = item.Replacements;

          replacements.Add("_AssemblyName_", "AssemblyName");
          replacements.Add("_AssemblyHtm_", "AssemblyHtml");
          replacements.Add("_Namespace_", "Namespace");
          //replacements.Add("_TypeName_", property.TypeName);

          replacements.Add("_PropertyName_", property.Name);
          replacements.Add("_PropertySummary_", property.Summary);
          //replacements.Add("_Returns_", property.Returns);
          replacements.Add("_Syntax_", property.Syntax);

          replacements.Add("_GenDate_", DateTime.Now.ToShortDateString());
          if (NetString.HasValue(property.Remarks))
          {
            replacements.Add("_RemarkText_", property.Remarks);
            replacements.Add("_HasRemarks_", "true");
          }
        }
      }
    }
    #endregion
  }
}
