// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateAssemblyXml.cs
using System;
using LJCNetCommon;
using LJCGenTextLib;
using LJCDocObjLib;
using System.IO;

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

      // Main section.
      Sections sections = new Sections();
      section = sections.Add("Main");
      repeatItem = section.RepeatItems.Add("Main");
      replacements = repeatItem.Replacements;
      replacements.Add("_AssemblyName_", DataAssembly.Name);
      if (NetString.HasValue(DataAssembly.MainImage))
      {
        replacements.Add("_Image_", DataAssembly.MainImage);
      }
      replacements.Add("_GenDate_", $"{DateTime.Now.ToShortDateString()}"
        + $" {DateTime.Now.ToShortTimeString()}");

      // AssemblyRemarks section.
      if (SetAssemblyRemarks())
      {
        section = sections.Add("AssemblyRemarks");
        SetAssemblyRemarks(section);
      }

      if (AddLinks(sections))
      {
        replacements.Add("_HasLinks_", "true");
      }

      // ClassList section.
      section = sections.Add("ClassList");
      AddClassList(section);
      string typeCount = DataAssembly.DataTypes.Count.ToString();
      replacements.Add("_ClassCount_", typeCount);

      replacements.Add("_Copyright_"
        , "Copyright &copy; Lester J. Clark 2021,2022 - All Rights Reserved");
      retValue = NetCommon.XmlSerializeToString(sections.GetType(), sections
        , null);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Gets the Class List elements.
    /// <include path='items/AddClassList/*' file='Doc/CreateAssemblyXml.xml'/>
    private void AddClassList(Section section)
    {
      RepeatItem repeatItem;
      Replacements replacements;

      foreach (DataType dataType in DataAssembly.DataTypes)
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
