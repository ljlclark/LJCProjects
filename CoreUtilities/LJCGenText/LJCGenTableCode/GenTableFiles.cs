// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenTableFiles.cs
using LJCNetCommon;
using LJCDBClientLib;
using LJCGenTextLib;
using System;
using System.IO;
using System.Text;

namespace LJCGenTableCode
{
  // Generates the table DAL source files.
  /// <include path='items/GenTableFiles/*' file='Doc/GenTableFiles.xml'/>
  public class GenTableFiles
  {
    // Generates the table DAL source files.
    /// <include path='items/GenFiles/*' file='Doc/GenTableFiles.xml'/>
    public void GenFiles(GenFileSpecs genFileSpecs, DataManager dataManager
      , out bool hasError)
    {
      hasError = false;
      string xmlData = GetDataXml(dataManager);
      WriteDataXml(genFileSpecs, dataManager.TableName, xmlData, out hasError);

      if (!hasError)
      {
        GenerateText genText = new GenerateText();
        foreach (GenFileSpec genFileSpec in genFileSpecs)
        {
          string xmlFileSpec = GetXmlSpec(genFileSpec.XMLFormat, dataManager.TableName
            , genFileSpec.IsPlural);
          //genText.Generate(genFileSpec.TemplateFileSpec, xmlFileSpec
          //  , genFileSpec.OutputFileSpec);

          Sections sections = genText.GetDataSections(xmlFileSpec);
          var templateFileSpec = genFileSpec.TemplateFileSpec;
          var templateLines = GenCommon.GetTemplateLines(templateFileSpec
            , out string errorText);

          TextGenLib textGenLib = new TextGenLib();
          var outputText = textGenLib.TextGen(sections, templateLines);
          var dataFileSpec = xmlFileSpec;
          var outputFileSpec = genFileSpec.OutputFileSpec;
          var outFileSpec = genText.GetOutFileSpec(dataFileSpec, outputFileSpec);
          File.WriteAllText(outFileSpec, outputText);
        }
      }
    }

    // Creates the XML data file spec.
    /// <include path='items/GetXmlSpec/*' file='Doc/GenTableFiles.xml'/>
    private string GetXmlSpec(string xmlFormat, string name, bool isPlural = false)
    {
      string retValue;

      if (isPlural)
      {
        name = GetPlural(name);
      }
      retValue = string.Format(xmlFormat, name);
      return retValue;
    }

    // Generates the data XML files.
    /// <include path='items/GetDataXml/*' file='Doc/GenTableFiles.xml'/>
    public string GetDataXml(DataManager dataManager)
    {
      string retValue;

      DefaultValues inValues = GetDefaults(dataManager.TableName);

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("<?xml version = '1.0' encoding = 'utf-8'?>");
      builder.Append("<Sections xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'");
      builder.AppendLine(" xmlns:xsd='http://www.w3.org/2001/XMLSchema'>");
      builder.AppendLine("  <Section>");
      builder.AppendLine("    <Name>Class</Name>");
      builder.AppendLine("    <RepeatItems>");
      builder.AppendLine("      <RepeatItem>");
      builder.AppendLine("        <Name>Item1</Name>");
      builder.AppendLine("        <Replacements>");
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_AppName_</Name>");
      builder.AppendLine($"            <Value>{inValues.AppName}</Value>");
      builder.AppendLine("          </Replacement>");
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_ClassName_</Name>");
      builder.AppendLine($"            <Value>{inValues.ClassName}</Value>");
      builder.AppendLine("          </Replacement>");
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_CollectionName_</Name>");
      builder.AppendLine($"            <Value>{inValues.CollectionName}</Value>");
      builder.AppendLine("          </Replacement>");
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_CompareToName_</Name>");
      builder.AppendLine($"            <Value>{inValues.CompareToName}</Value>");
      builder.AppendLine("          </Replacement>");
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_FullAppName_</Name>");
      builder.AppendLine($"            <Value>{inValues.FullAppName}</Value>");
      builder.AppendLine("          </Replacement>");
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_Namespace_</Name>");
      builder.AppendLine($"            <Value>{inValues.Namespace}</Value>");
      builder.AppendLine("          </Replacement>");
      // *** Begin *** Add - 12/25/23
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_ParentName_</Name>");
      builder.AppendLine($"            <Value>{inValues.ParentName}</Value>");
      builder.AppendLine("          </Replacement>");
      // *** End   *** Add - 12/25/23
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_TableName_</Name>");
      builder.AppendLine($"            <Value>{inValues.TableName}</Value>");
      builder.AppendLine("          </Replacement>");
      builder.AppendLine("          <Replacement>");
      builder.AppendLine("            <Name>_ToStringName_</Name>");
      builder.AppendLine($"            <Value>{inValues.ToStringName}</Value>");
      builder.AppendLine("          </Replacement>");
      builder.AppendLine("        </Replacements>");
      builder.AppendLine("      </RepeatItem>");
      builder.AppendLine("    </RepeatItems>");
      builder.AppendLine("  </Section>");

      builder.AppendLine("  <Section>");
      builder.AppendLine("    <Name>Properties</Name>");
      builder.AppendLine("    <RepeatItems>");

      int index = 0;
      foreach (DbColumn dbColumn in dataManager.DataDefinition)
      {
        builder.AppendLine("      <RepeatItem>");
        index++;
        builder.AppendLine($"          <Name>Item{index}</Name>");
        builder.AppendLine("        <Replacements>");
        builder.AppendLine("          <Replacement>");
        builder.AppendLine("            <Name>_DataType_</Name>");
        builder.AppendLine($"            <Value>{dbColumn.DataTypeName}</Value>");
        builder.AppendLine("          </Replacement>");
        builder.AppendLine("          <Replacement>");
        builder.AppendLine("            <Name>_ColumnName_</Name>");
        builder.AppendLine($"            <Value>{dbColumn.ColumnName}</Value>");
        builder.AppendLine("          </Replacement>");
        builder.AppendLine("          <Replacement>");
        builder.AppendLine("            <Name>_PropertyName_</Name>");
        builder.AppendLine($"            <Value>{dbColumn.PropertyName}</Value>");
        builder.AppendLine("          </Replacement>");
        builder.AppendLine("          <Replacement>");
        builder.AppendLine("            <Name>_MaxLength_</Name>");
        builder.AppendLine($"            <Value>{dbColumn.MaxLength}</Value>");
        builder.AppendLine("          </Replacement>");
        builder.AppendLine("        </Replacements>");
        builder.AppendLine("      </RepeatItem>");
      }
      builder.AppendLine("    </RepeatItems>");
      builder.AppendLine("  </Section>");
      builder.AppendLine("</Sections>");

      retValue = builder.ToString();
      return retValue;
    }

    // Gets the default values from a file.
    private DefaultValues GetDefaults(string tableName
      , string fileSpec = null)
    {
      DefaultValues retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = "DefaultValues.xml";
      }
      retValue = NetCommon.XmlDeserialize(typeof(DefaultValues)
        , fileSpec) as DefaultValues;
      if (retValue != null)
      {
        retValue.TableName = tableName;
        if (0 == string.Compare(retValue.CollectionName, "TableName", true))
        {
          retValue.CollectionName = GetPlural(tableName);
        }
        if (0 == string.Compare(retValue.ClassName, "TableName", true))
        {
          retValue.ClassName = tableName;
        }
      }
      return retValue;
    }

    // Gets the plural value for the supplied name.
    private string GetPlural(string name)
    {
      string retValue;

      char prevChar = name.ToLower()[name.Length - 2];
      char lastChar = name.ToLower()[name.Length - 1];
      switch (lastChar)
      {
        case 's':
          retValue = name + "es";
          break;

        case 'y':
          if ("aeiou".IndexOf(prevChar) >= 0)
          {
            retValue = name + "s";
          }
          else
          {
            retValue = name.Substring(0, name.Length - 1) + "ies";
          }
          break;

        default:
          retValue = name + "s";
          break;
      }
      return retValue;
    }

    // Writes the XML data file.
    /// <include path='items/WriteDataXml/*' file='Doc/GenTableFiles.xml'/>
    private void WriteDataXml(GenFileSpecs genFileSpecs, string tableName
      , string xmlData, out bool hasError)
    {
      string xmlFileSpec;

      hasError = false;
      foreach (GenFileSpec genFileSpec in genFileSpecs)
      {
        xmlFileSpec = GetXmlSpec(genFileSpec.XMLFormat, tableName, genFileSpec.IsPlural);
        if (File.Exists(xmlFileSpec))
        {
          hasError = true;
          Console.WriteLine($"File '{xmlFileSpec}' already exists.");
        }
        else
        {
          File.WriteAllText(xmlFileSpec, xmlData);
        }
      }
    }
  }
}
