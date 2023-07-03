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
using Section = LJCGenTextLib.Section;

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
      // Testing
      //if ("LJCDBClientLib" == DataAssembly.Name)
      //{
      //  int i = 0;
      //}
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

      AddLinks(sections, replacements);

      var classListHeading = "Classes";
      mOtherTypes = new List<DataType>();
      foreach (var dataType in DataAssembly.DataTypes)
      {
        mOtherTypes.Add(dataType);
      }
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
    #endregion

    #region Private Methods

    // Creates the Class Groups
    // <include path='items/AddClassGroups/*' file='Doc/CreateAssemblyXml.xml'/>
    private bool AddClassGroups(Sections sections)
    {
      bool retValue = false;

      var managers = ValuesDocGen.Instance.Managers;

      var docAssemblyManager = managers.DocAssemblyManager;
      var docAssembly = docAssemblyManager.RetrieveWithName(DataAssembly.Name);

      // Get the DocClassGroups for the DocAssembly.
      if (docAssembly != null)
      {
        var classGroupManager = managers.DocClassGroupManager;
        var orderColumns = new List<string>()
        {
          DocClassGroup.ColumnSequence
        };
        classGroupManager.SetOrderBy(orderColumns);
        var classGroups = classGroupManager.LoadWithParentID(docAssembly.ID);
        if (NetCommon.HasItems(classGroups))
        {
          var section = sections.Add("ClassGroups");
          var repeatItems = section.RepeatItems;
          foreach (var classGroup in classGroups)
          {
            if ("ungrouped" == classGroup.HeadingName.ToLower())
            {
              continue;
            }

            // Get the DocClasses for the DocClassGroup.
            var classManager = managers.DocClassManager;
            orderColumns = new List<string>()
            {
              DocClass.ColumnSequence
            };
            classManager.SetOrderBy(orderColumns);
            var docClasses
              = classManager.LoadWithGroup(classGroup.ID);
            if (NetCommon.HasItems(docClasses))
            {
              retValue = true;
              var repeatItem = repeatItems.Add(classGroup.HeadingName);
              repeatItem.Replacements.Add("_Heading_", classGroup.HeadingName);

              repeatItem.Subsection = new Section("Subsection");
              var subRepeatItems = repeatItem.Subsection.RepeatItems;
              foreach (var docClass in docClasses)
              {
                var className = docClass.Name;

                var dataType = mOtherTypes.Find(x => x.Name == className);
                if (dataType != null)
                {
                  mOtherTypes.Remove(dataType);
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
      }
      return retValue;
    }

    // Creates the Class List.
    // <include path='items/AddClassList/*' file='Doc/CreateAssemblyXml.xml'/>
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
    // <include path='items/AddLinks/*' file='Doc/CreateAssemblyXml.xml'/>
    private void AddLinks(Sections sections, Replacements mainReplacements)
    {
      // Look through all data types for Assembly Remarks.
      foreach (DataType dataType in DataAssembly.DataTypes)
      {
        if (NetCommon.HasItems(dataType.DataLinks))
        {
          Section section = null;
          foreach (DataLink dataLink in dataType.DataLinks)
          {
            var fileSpec = dataLink.FileName;
            var text = dataLink.Text;
            if (text.StartsWith("--"))
            {
              if (null == section)
              {
                mainReplacements.Add("_HasLinks_", "true");
                section = sections.Add("Links");
              }

              int startIndex = 0;
              text = NetString.GetDelimitedString(text, "--", ref startIndex
                , "#NoDelimiter");

              var fileName = Path.GetFileNameWithoutExtension(fileSpec);
              var repeatItem = section.RepeatItems.Add(fileName);
              var replacements = repeatItem.Replacements;
              replacements.Add("_LinkFile_", fileSpec);
              if (false == NetString.HasValue(text))
              {
                var errorText = $"{dataType.NamespaceValue}.{dataType.Name}";
                GenRoot.LogMissing("Link FileName", errorText, fileSpec);
              }
              replacements.Add("_LinkText_", text);
            }
          }
        }
      }
    }

    // Sets the Object Remarks elements.
    // <include path='items/SetAssemblyRemarks/*' file='Doc/CreateAssemblyXml.xml'/>
    private bool SetAssemblyRemarks(Section section = null)
    {
      bool retValue = false;

      // Look through all data types for Assembly Remarks.
      foreach (DataType dataType in DataAssembly.DataTypes)
      {
        DataRemark remark = dataType.Remark;
        if (remark != null
          && NetCommon.HasItems(remark.Paras))
        {
          bool showParas = false;
          foreach (DataPara para in remark.Paras)
          {
            if (para.Text != null
              && para.Text.Contains("--"))
            {
              // Show all remaining paragraphs.
              // Do not show the "--" paragraph.
              showParas = true;
            }
            else
            {
              if (showParas)
              {
                retValue = true;
                if (null == section)
                {
                  break;
                }
                if (NetString.HasValue(para.Text))
                {
                  var repeatItem = section.RepeatItems.Add("Para");
                  var replacements = repeatItem.Replacements;
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

    // Gets the DataAssembly reference.
    private DataAssembly DataAssembly { get; }
    #endregion

    private List<DataType> mOtherTypes;
  }
}
