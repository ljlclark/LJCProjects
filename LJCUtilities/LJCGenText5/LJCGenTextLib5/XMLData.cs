// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// XMLData.cs
using LJCNetCommon5;
using System.Text;

namespace LJCGenTextLib5
{
  /// <summary>Provides methods to generate DataXML files from a Table.</summary>
  public class XMLData
  {
    #region Static Methods

    // Gets the plural value for the supplied name.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/GetPlural/*'/>
    public static string GetPlural(string name)
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
          //if ("aeiou".IndexOf(prevChar) >= 0)
          if ("aeiou".Contains(prevChar))
          {
            retValue = name + "s";
          }
          else
          {
            //retValue = name.Substring(0, name.Length - 1) + "ies";
            retValue = string.Concat(name.AsSpan(0, name.Length - 1), "ies");
          }
          break;

        default:
          retValue = name + "s";
          break;
      }
      return retValue;
    }

    // Gets the default values from a file.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/GetDefaults/*'/>
    public static DefaultValues? GetDefaults(string tableName
      , string? fileSpec = null)
    {
      DefaultValues? retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = "DefaultValues.xml";
      }
      retValue = LJC.XmlDeserialize(typeof(DefaultValues)
        , fileSpec) as DefaultValues;
      if (retValue != null)
      {
        retValue.TableName = tableName;
        if (0 == string.Compare(retValue.CollectionName, "TableNames", true))
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
    #endregion

    #region Properties

    // Gets or sets the Builder value.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/Builder/*'/>
    public StringBuilder Builder { get; set; } = null!;

    // Gets or sets the PadLength value.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/PadLength/*'/>
    public int PadLength { get; set; }
    #endregion

    #region Methods

    // Adds padded and formatted text line.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/Add/*'/>
    public void Add(string text, params object[] parms)
    {
      Builder.AppendLine(Pad(text, parms));
    }

    // Creates the Class Items.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/ClassItems/*'/>
    public string ClassItems(DefaultValues defaultValues)
    {
      string retValue;

      Builder = new StringBuilder(128);
      Add("<RepeatItem>");
      Add("  <Name>Item1</Name>");
      Add("  <Replacements>");
      Add("    <Replacement>");
      Add("      <Name>_AppName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.AppName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_ClassName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.ClassName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_CollectionName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.CollectionName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_ComparerName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.ComparerName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_CompareToName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.CompareToName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_FullAppName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.FullAppName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_Namespace_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.Namespace);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_ParentName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.ParentName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_TableName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.TableName);
      Add("    </Replacement>");
      Add("    <Replacement>");
      Add("      <Name>_ToStringName_</Name>");
      Add("      <Value>{0}</Value>", defaultValues.ToStringName);
      Add("    </Replacement>");
      Add("  </Replacements>");
      Builder.Append(Pad("</RepeatItem>"));
      retValue = Builder.ToString();
      return retValue;
    }

    // Generates the data XML files.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/Create/*'/>
    public string Create(string tableName, LJCDataColumns dbColumns)
    {
      string retValue;

      var defaultValues = GetDefaults(tableName);

      PadLength = 6;
      StringBuilder build = new(64);
      build.AppendLine("<?xml version = '1.0' encoding = 'utf-8'?>");
      build.Append("<Sections xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'");
      build.AppendLine(" xmlns:xsd='http://www.w3.org/2001/XMLSchema'>");
      build.AppendLine("  <Section>");
      build.AppendLine("    <Name>Class</Name>");
      if (defaultValues != null)
      {
        build.AppendLine("    <RepeatItems>");
        build.AppendLine(ClassItems(defaultValues));
        build.AppendLine("    </RepeatItems>");
      }
      build.AppendLine("  </Section>");
      build.AppendLine("  <Section>");
      build.AppendLine("    <Name>Properties</Name>");
      build.AppendLine("    <RepeatItems>");
      build.AppendLine(PropertyItems(dbColumns));
      build.AppendLine("    </RepeatItems>");
      build.AppendLine("  </Section>");
      build.Append("</Sections>");
      retValue = build.ToString();
      return retValue;
    }

    // Pads and formats the text.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/Pad/*'/>
    public string Pad(string text, params object[] parms)
    {
      string retValue;

      string padValue = new(' ', PadLength);
      string readyText = string.Format(text, parms);
      retValue = $"{padValue}{readyText}";
      return retValue;
    }

    // Creates the Property Items.
    /// <include file='Doc/XMLData.xml'
    ///  path='items/PropertyItems/*'/>
    public string PropertyItems(LJCDataColumns dbColumns)
    {
      string retValue;

      Builder = new StringBuilder(128);
      int index = 0;
      foreach (LJCDataColumn dbColumn in dbColumns)
      {
        Add("<RepeatItem>");
        index++;
        Add("  <Name>Item{0}</Name>", index);
        Add("  <Replacements>");
        Add("    <Replacement>");
        Add("      <Name>_AllowDBNull_</Name>");
        Add("      <Value>{0}</Value>", dbColumn.AllowDBNull);
        Add("    </Replacement>");
        if (LJC.HasText(dbColumn.DataTypeName))
        {
          Add("    <Replacement>");
          Add("      <Name>_DataType_</Name>");
          Add("      <Value>{0}</Value>", dbColumn.DataTypeName);
          Add("    </Replacement>");
        }
        if (LJC.HasText(dbColumn.SQLTypeName))
        {
          Add("    <Replacement>");
          Add("      <Name>_DBType_</Name>");
          Add("      <Value>{0}</Value>", dbColumn.SQLTypeName);
          Add("    </Replacement>");
        }
        Add("    <Replacement>");
        Add("      <Name>_ColumnName_</Name>");
        Add("      <Value>{0}</Value>", dbColumn.ColumnName);
        Add("    </Replacement>");
        Add("    <Replacement>");
        Add("      <Name>_PropertyName_</Name>");
        Add("      <Value>{0}</Value>", dbColumn.PropertyName);
        Add("    </Replacement>");
        Add("    <Replacement>");
        Add("      <Name>_MaxLength_</Name>");
        Add("      <Value>{0}</Value>", dbColumn.MaxLength);
        Add("    </Replacement>");
        Add("  </Replacements>");
        Builder.Append(Pad("</RepeatItem>"));
      }
      retValue = Builder.ToString();
      return retValue;
    }
    #endregion
  }
}
