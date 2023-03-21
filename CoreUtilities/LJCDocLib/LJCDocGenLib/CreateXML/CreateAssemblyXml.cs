// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateAssemblyXml.cs
using System;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocObjLib;
using System.IO;
using LJCDocLibDAL;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace LJCDocGenLib
{
  // Creates the Assembly XML Data values.
  /// <include path='items/CreateAssemblyXml/*' file='Doc/CreateAssemblyXml.xml'/>
  public class CreateAssemblyXml
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CreateAssemblyXmlC/*' file='Doc/CreateAssemblyXml.xml'/>
    public CreateAssemblyXml(DataAssembly dataAssembly)
    {
      DataAssembly = dataAssembly;
    }
    #endregion

    #region Public Methods

    // Returns the XML Data values.
    /// <include path='items/GetXmlData/*' file='Doc/CreateAssemblyXml.xml'/>
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

      string typeCount = DataAssembly.DataTypes.Count.ToString();
      replacements.Add("_ClassCount_", typeCount);

      var copyRight = "Copyright &copy Lester J. Clark and Contributors.<br />\r\n";
      copyRight += "Licensed under the MIT License.";
      replacements.Add("_Copyright_", copyRight);

      replacements.Add("_GenDate_", $"{DateTime.Now.ToShortDateString()}"
        + $" {DateTime.Now.ToShortTimeString()}");
      if (NetString.HasValue(DataAssembly.MainImage))
      {
        replacements.Add("_Image_", DataAssembly.MainImage);
      }

      if (SetAssemblyRemarks())
      {
        section = sections.Add("AssemblyRemarks");
        SetAssemblyRemarks(section);
      }

      if (AddLinks(sections))
      {
        replacements.Add("_HasLinks_", "true");
      }

      var classListHeading = "Classes";
      mOtherTypes = DataAssembly.DataTypes;
      if (AddClassGroups(sections))
      {
        classListHeading = "Other Classes";
      }

      if (NetCommon.HasItems(mOtherTypes))
      {
        replacements.Add("_HasClasses_", "true");
        replacements.Add("_ClassListHeading_", classListHeading);
        section = sections.Add("ClassList");
        AddClassList(section);
      }

      retValue = NetCommon.XmlSerializeToString(sections.GetType(), sections
        , null);
      return retValue;
    }
    private List<DataType> mOtherTypes;
    #endregion

    #region Private Methods

    // Creates the Class Groups
    private bool AddClassGroups(Sections sections)
    {
      bool retValue = false;

      var managers = ValuesDocGen.Instance.Managers;

      // Get the DocAssembly data.
      var docAssemblyManager = managers.DocAssemblyManager;
      var docAssembly = docAssemblyManager.RetrieveWithName(DataAssembly.Name);

      // Get the DocGroups for the DocAssembly.
      if (docAssembly != null)
      {
        var classGroupManager = managers.DocClassGroupManager;
        var classGroups = classGroupManager.LoadWithParent(docAssembly.ID);
        if (NetCommon.HasItems(classGroups))
        {
          var section = sections.Add("ClassGroups");
          var repeatItems = section.RepeatItems;
          foreach (var classGroup in classGroups)
          {
            var repeatItem = repeatItems.Add(classGroup.HeadingName);
            repeatItem.Replacements.Add("_Heading_", classGroup.HeadingName);

            // Get the DocClasses for the DocGroup.
            var classManager = managers.DocClassManager;
            var docClasses
              = classManager.LoadWithParent(classGroup.ID);
            if (NetCommon.HasItems(docClasses))
            {
              retValue = true;
              repeatItem.SubSection = new Section("SubSection");
              var subRepeatItems = repeatItem.SubSection.RepeatItems;
              foreach (var docClass in docClasses)
              {
                var className = docClass.Name;

                var dataType = mOtherTypes.Find(x => x.Name == className);
                if (dataType != null)
                {
                  mOtherTypes.Remove(dataType);
                }

                var subRepeatItem = subRepeatItems.Add(className);
                var subReplacements = subRepeatItem.Replacements;
                subReplacements.Add("_HTMLFileName_", $"{className}.html");
                subReplacements.Add("_Name_", className);
                subReplacements.Add("_Summary_", docClass.Description);
              }
            }
          }
        }
      }
      return retValue;
    }

    // Creates the Class List.
    /// <include path='items/AddClassList/*' file='Doc/CreateAssemblyXml.xml'/>
    private void AddClassList(Section section)
    {
      RepeatItem repeatItem;
      Replacements replacements;

      foreach (DataType dataType in mOtherTypes)
      {
        if (dataType.Summary.ToLower() != "nogen")
        {
          repeatItem = section.RepeatItems.Add(dataType.Name);
          replacements = repeatItem.Replacements;
          string htmlFileName = $"{dataType.Name}.html";
          replacements.Add("_HTMLFileName_", htmlFileName);
          replacements.Add("_Name_", dataType.Name);
          if (NetString.HasValue(dataType.Summary))
          {
            replacements.Add("_Summary_", dataType.Summary);
          }
        }
      }
    }

    // Gets the Link elements.
    private bool AddLinks(Sections sections)
    {
      Section section;
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      string linksFileName = $"Links\\{DataAssembly.Name}Links.xml";
      if (File.Exists(linksFileName))
      {
        Links links = Links.LJCDeserialize(linksFileName);
        if (links != null && links.Count > 0)
        {
          retValue = true;
          section = sections.Add("Links");
          int count = 0;
          foreach (Link link in links)
          {
            count++;

            string fileName = Path.GetFileName(link.Name);

            repeatItem = section.RepeatItems.Add($"Link{count}");
            replacements = repeatItem.Replacements;
            replacements.Add("_LinkName_", fileName);
            replacements.Add("_LinkText_", link.Text);
          }
        }
      }
      return retValue;
    }

    // Sets the Object Remarks elements.
    /// <include path='items/SetAssemblyRemarks/*' file='Doc/CreateAssemblyXml.xml'/>
    private bool SetAssemblyRemarks(Section section = null)
    {
      RepeatItem repeatItem;
      Replacements replacements;
      bool retValue = false;

      // Look through all data types for Assembly Remarks.
      foreach (DataType dataType in DataAssembly.DataTypes)
      {
        DataRemark remark = dataType.Remark;
        if (remark != null
          && (remark.Paras != null && remark.Paras.Count > 0))
        {
          bool showGroups = false;
          foreach (DataPara para in remark.Paras)
          {
            if (para.Text != null && para.Text.Contains("--"))
            {
              // Show all remaining paragraphs.
              // Do not show the "--" paragraph.
              showGroups = true;
            }
            else
            {
              if (showGroups)
              {
                retValue = true;
                if (null == section)
                {
                  break;
                }
                if (NetString.HasValue(para.Text))
                {
                  repeatItem = section.RepeatItems.Add("Para");
                  replacements = repeatItem.Replacements;
                  replacements.Add("_Para_", para.Text);
                }
              }
            }
          }
          if (null == section
            && retValue)
          {
            break;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    private DataAssembly DataAssembly { get; }
    #endregion
  }
}
