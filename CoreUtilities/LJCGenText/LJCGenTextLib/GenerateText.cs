// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenerateText.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using static System.Collections.Specialized.BitVector32;

namespace LJCGenTextLib
{
  // Generates text using a text template.
  /// <include path='items/GenerateText/*' file='Doc/GenerateText.xml'/>
  public class GenerateText
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public GenerateText()
    {
      Reset();
    }

    // Resets the current values.
    /// <include path='items/Reset/*' file='Doc/GenerateText.xml'/>
    public void Reset()
    {
      OutLines = new List<string>();
      ActiveSections = new Sections();
    }
    #endregion

    #region Public Methods

    // Generates a merged output file from the specified template and data file.
    /// <include path='items/Generate1/*' file='Doc/GenerateText.xml'/>
    public void Generate(string templateFileSpec, string dataFileSpec
      , string outputFileSpec = null, bool overwrite = false)
    {
      Sections sections = GetDataSections(dataFileSpec);
      Generate(templateFileSpec, sections, dataFileSpec, outputFileSpec
        , overwrite);
    }

    // Generates a merged output file from the specified template and Sections data.
    /// <include path='items/Generate2/*' file='Doc/GenerateText.xml'/>
    public void Generate(string templateFileSpec, Sections sections
      , string dataFileName, string outputFileSpec = null, bool overwrite = false)
    {
      string[] templateLines = GenCommon.GetTemplateLines(templateFileSpec
        , out string _);
      if (templateLines != null)
      {
        Generate(templateLines, sections, dataFileName, outputFileSpec, overwrite);
      }
    }

    // Generates a merged output file from the template lines and Sections data.
    /// <include path='items/Generate3/*' file='Doc/GenerateText.xml'/>
    public void Generate(string[] templateLines, Sections sections
      , string dataFileName, string outputFileSpec = null, bool overwrite = false)
    {
      string errorText = null;

      TemplateLines = templateLines;
      Sections = sections;
      OutFileSpec = GetOutFileSpec(dataFileName, outputFileSpec);

      // Begin generating the output file.
      if (false == overwrite
        && File.Exists(OutFileSpec))
      {
        errorText += $"File'{OutFileSpec}' already exists.";
        throw new InvalidOperationException(errorText);
      }
      else
      {
        if (Sections != null)
        {
          OutLines.Clear();
          GenSectionNone();
          if (false == string.IsNullOrWhiteSpace(OutFileSpec))
          {
            string folderPath = Path.GetDirectoryName(OutFileSpec);
            if (NetString.HasValue(folderPath)
              && false == Directory.Exists(folderPath))
            {
              Directory.CreateDirectory(folderPath);
            }
            File.WriteAllLines(OutFileSpec, OutLines.ToArray());
          }
        }
      }
    }

    // Create Sections from template definition.
    /// <summary>
    /// Create Sections from template definition.
    /// </summary>
    /// <param name="templateLines">The Template lines.</param>
    /// <returns>The Sections object.</returns>
    public Sections CreateSections(string[] templateLines)
    {
      Section section = null;
      Replacements replacements;
      Replacement replacement;
      Directive directive;
      bool IsSectionDirective;
      Sections retValue;

      DefaultValues defaultValues = NetCommon.XmlDeserialize(typeof(DefaultValues)
        , "DefaultValues.xml") as DefaultValues;
      LJCReflect reflect = new LJCReflect(defaultValues);

      retValue = new Sections();
      for (int lineIndex = 0; lineIndex < templateLines.Length; lineIndex++)
      {
        IsSectionDirective = false;
        string line = templateLines[lineIndex];
        directive = GenCommon.GetDirective(line);

        if (directive != null)
        {
          if (GenCommon.IsSectionDirective(directive))
          {
            IsSectionDirective = true;

            switch (directive.ID.ToLower())
            {
              case GenCommon.BeginSection:
                section = retValue.LJCSearchName(directive.Name);
                if (null == section)
                {
                  section = retValue.Add(directive.Name);
                }
                break;

              case GenCommon.EndSection:
                section = null;
                break;
            }
          }

          if (section != null
            && false == IsSectionDirective)
          {
            if (GenCommon.IsValue(directive))
            {
              if (0 == section.RepeatItems.Count)
              {
                section.RepeatItems.Add("Item1");
              }

              replacements = section.RepeatItems[0].Replacements;
              replacement = replacements.LJCSearchName(directive.Name);
              if (null == replacement)
              {
                string propertyValue = null;
                var propertyName = directive.Name.Replace("_", "");
                if (reflect.HasProperty(propertyName))
                {
                  propertyValue = reflect.GetString(propertyName);
                }
                replacements.Add(directive.Name, propertyValue);
              }
            }
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Retrieves the active session replacement value.
    // <include path='items/ActiveSessionReplacementValue/*' file='Doc/GenerateText.xml'/>
    private string ActiveSessionReplacementValue(string replacementName)
    {
      string retValue = null;

      RepeatItems repeatItems = ActiveSessionsRepeateItems();
      foreach (RepeatItem repeatItem in repeatItems)
      {
        Replacement replacement = repeatItem.Replacements.LJCSearchName(replacementName);
        if (replacement != null)
        {
          retValue = replacement.Value;
        }
      }
      return retValue;
    }

    // Retrieves the active session current repeat items.
    // <include path='items/ActiveSessionRepeateItems/*' file='Doc/GenerateText.xml'/>
    private RepeatItems ActiveSessionsRepeateItems()
    {
      RepeatItems retValue = new RepeatItems();

      if (ActiveSections != null && ActiveSections.Count > 0)
      {
        for (int index = ActiveSections.Count - 1; index >= 0; index--)
        {
          RepeatItem repeatItem = ActiveSections[index].CurrentRepeatItem;
          if (repeatItem != null && repeatItem.Replacements != null)
          {
            retValue.Add(repeatItem);
          }
        }
      }
      return retValue;
    }

    // Adds the current Section object to the Active List.
    // <include path='items/PushSection/*' file='Doc/GenerateText.xml'/>
    private Section AddActiveSection(Section currentSection, Directive directive
      , int lineIndex)
    {
      Section retValue = Sections.LJCSearchName(directive.Name);

      if (null == retValue
        && currentSection != null)
      {
        if (currentSection.CurrentRepeatItem != null
          && currentSection.CurrentRepeatItem.SubSection != null)
        {
          Section subSection = currentSection.CurrentRepeatItem.SubSection;
          if (subSection != null)
          {
            retValue = subSection;
          }
        }
      }
      if (null == retValue)
      {
        retValue = new Section()
        {
          Name = directive.Name,
        };
      }

      retValue.HasData = false;
      if (retValue.RepeatItems.Count > 0)
      {
        retValue.HasData = true;
        if (directive.Modifier != null
          && "list" == directive.Modifier.ToLower())
        {
          retValue.IsList = true;
        }
      }
      retValue.StartLineIndex = lineIndex + 1;
      ActiveSections.Add(retValue);
      return retValue;
    }

    // Handle begin directive and do not generate line.
    private bool BeginSection(Section currentSection, Directive directive
      , ref int lineIndex)
    {
      bool retValue = false;

      if (GenCommon.IsBeginSection(directive))
      {
        retValue = true;

        // Add section to ActiveSections with the section starting index.
        var addSection = AddActiveSection(currentSection, directive, lineIndex);
        if (addSection.HasData)
        {
          mProcessLines = true;
          GenSection(addSection);

          // Set the index to continue at the end of the processed section.
          lineIndex = SectionEndLineIndex;
        }
        else
        {
          mProcessLines = false;
        }
      }
      return retValue;
    }

    // Get the Sections from the data file.
    private Sections GetDataSections(string dataFileSpec)
    {
      string errorText = null;
      Sections retValue;

      if (string.IsNullOrWhiteSpace(dataFileSpec))
      {
        errorText += "Missing Data file specification.\r\n";
        throw new ArgumentException(errorText);
      }
      else
      {
        if (false == File.Exists(dataFileSpec))
        {
          errorText += $"Data file '{dataFileSpec}' was not found.\r\n";
          throw new FileNotFoundException(errorText);
        }
        else
        {
          retValue = NetCommon.XmlDeserialize(typeof(Sections)
            , dataFileSpec) as Sections;
          if (null == retValue || 0 == retValue.Count)
          {
            errorText += "Unable to read replacement data or no 'Sections' are defined.\r\n";
            throw new InvalidOperationException(errorText);
          }
        }
      }
      return retValue;
    }

    // Generate the If Block statements.
    // <include path='items/GenIfBegin/*' file='Doc/GenerateText.xml'/>
    private void GenIfBlock(Section section, Directive directive
      , ref int lineIndex, bool lastRepeatItem)
    {
      lineIndex++;
      bool isValid = false;
      string replacementValue = ActiveSessionReplacementValue(directive.Name);
      switch (directive.Modifier.ToLower())
      {
        case "notnull":
          if (NetString.HasValue(replacementValue))
          {
            isValid = true;
          }
          break;
        default:
          if (NetString.HasValue(replacementValue))
          {
            if (replacementValue.ToLower() == directive.Modifier.ToLower())
            {
              isValid = true;
            }
          }
          break;
      }
      for (int ifIndex = lineIndex; ifIndex < TemplateLines.Length; ifIndex++)
      {
        string line = TemplateLines[ifIndex];
        Directive ifDirective = GenCommon.GetDirective(line);
        if (ifDirective != null)
        {
          if (GenCommon.IsIfEnd(ifDirective))
          {
            isValid = false;
            lineIndex = ifIndex++;
            ifIndex = TemplateLines.Length;
          }
          if (GenCommon.IsIfElse(ifDirective))
          {
            if (section.HasData)
            {
              isValid = !isValid;
            }
            lineIndex++;
          }
        }
        else
        {
          if (mProcessLines && isValid)
          {
            line = ReplaceValues(line);
            if (section.IsList && false == lastRepeatItem)
            {
              string trimLine = line.Trim();
              if (trimLine[trimLine.Length - 1] != ',')
              {
                line += ",";
              }
            }
            OutLines.Add(line);
          }
        }
      }
    }

    // Generates the code for the current RepeatItem.
    // <include path='items/GenRepeatItem/*' file='Doc/GenerateText.xml'/>
    private void GenRepeatItem(Section section, bool lastRepeatItem = false)
    {
      //// Start at beginning of template if not in a section.
      //int startLineIndex = 0;

      //if (section != null)
      //{
      // Start at the beginning of the section.
      var startLineIndex = section.StartLineIndex;
      //}
      for (int lineIndex = startLineIndex; lineIndex < TemplateLines.Length;
        lineIndex++)
      {
        string line = TemplateLines[lineIndex];
        var directive = GenCommon.GetDirective(line);

        // May change lineIndex to end of section or template.
        if (false == SectionDirective(section, directive, ref line
          , lastRepeatItem, ref lineIndex))
        {
          // *** Next Statement *** Change - 3/19/23
          if (section != null
            && section.HasData)
          {
            // Sets line to null if other directive.
            if (false == OtherDirective(section, directive, ref line
              , lastRepeatItem, ref lineIndex))
            {
              line = ReplaceValues(line);
              if (section.IsList && false == lastRepeatItem)
              {
                line += ",";
              }
            }
          }
          if (mProcessLines
            && line != null
            && false == line.Trim().StartsWith("<!--X-"))
          {
            OutLines.Add(line);
          }
        }
      }
    }

    // Generates each RepeatItem in a section.
    // <include path='items/GenSection/*' file='Doc/GenerateText.xml'/>
    private void GenSection(Section section)
    {
      mProcessLines = true;

      bool lastRepeateItem = false;
      for (int index = 0; index < section.RepeatItems.Count; index++)
      {
        RepeatItem repeatItem = section.RepeatItems[index];
        section.CurrentRepeatItem = repeatItem;
        if (index == section.RepeatItems.Count - 1)
        {
          lastRepeateItem = true;
        }
        GenRepeatItem(section, lastRepeateItem);
      }
    }

    //
    private void GenSectionNone()
    {
      int startLineIndex = 0;
      for (int lineIndex = startLineIndex; lineIndex < TemplateLines.Length;
        lineIndex++)
      {
        string line = TemplateLines[lineIndex];
        var directive = GenCommon.GetDirective(line);

        if (GenCommon.IsBeginSection(directive))
        {
          line = null;

          // Calls GenSection() with section values.
          BeginSection(null, directive, ref lineIndex);
          // Returns when no longer in a section.
        }

        if (line != null
          && false == line.Trim().StartsWith("<!--X-"))
        {
          OutLines.Add(line);
        }
      }
    }

    // Create the Out filespec from the dataFileSpec and outputFileSpec values.
    private string GetOutFileSpec(string dataFileSpec, string outputFileSpec)
    {
      string retValue = null;

      if (false == string.IsNullOrWhiteSpace(outputFileSpec))
      {
        outputFileSpec = outputFileSpec.Trim();
        if (outputFileSpec.Contains("*."))
        {
          string dataFileName = Path.GetFileNameWithoutExtension(dataFileSpec);
          retValue = outputFileSpec.Replace("*", dataFileName);
        }
        else
        {
          retValue = outputFileSpec;
        }
      }
      return retValue;
    }

    // Handle other directives.
    private bool OtherDirective(Section section, Directive directive
      , ref string line, bool lastRepeatItem, ref int lineIndex)
    {
      bool retValue = false;

      if (directive != null)
      {
        if (GenCommon.IsValue(directive))
        {
          retValue = true;
          line = null;
        }
        if (GenCommon.IsIfBegin(directive))
        {
          retValue = true;
          GenIfBlock(section, directive, ref lineIndex, lastRepeatItem);
          line = null;
        }
      }
      return retValue;
    }

    // Removes the completed Section from the Active list.
    // <include path='items/PopSection/*' file='Doc/GenerateText.xml'/>
    private void RemoveActiveSection()
    {
      if (ActiveSections != null && ActiveSections.Count > 0)
      {
        ActiveSections.RemoveAt(ActiveSections.Count - 1);
      }
    }

    // Replaces the values for the active RepeateItems.
    // <include path='items/ReplaceValues/*' file='Doc/GenerateText.xml'/>
    private string ReplaceValues(string line)
    {
      GenTokens genTokens = new GenTokens();
      string retValue;

      retValue = line;

      // Attempt replacement only if the line contains replacement tokens.
      genTokens.SetTokens(line);
      if (genTokens.Count > 0)
      {
        RepeatItems repeatItems = ActiveSessionsRepeateItems();
        foreach (RepeatItem repeatItem in repeatItems)
        {
          bool doNextLevel = false;
          foreach (string token in genTokens)
          {
            // Replace values if the token has a replacement value.
            Replacement replacement = repeatItem.Replacements.LJCSearchName(token);
            if (replacement != null)
            {
              retValue = retValue.Replace(token, replacement.Value);
            }
            else
            {
              doNextLevel = true;
            }
          }
          if (false == doNextLevel)
          {
            break;
          }
        }
      }
      return retValue;
    }

    // Handle Section Directives
    private bool SectionDirective(Section section, Directive directive
      , ref string line, bool lastRepeatItem, ref int lineIndex)
    {
      // Handle directive and do not generate line.
      var retValue = BeginSection(section, directive, ref lineIndex);
      if (retValue)
      {
        line = null;
      }
      if (false == retValue
        && GenCommon.IsEndSection(directive))
      {
        retValue = true;
        line = null;

        // *** Next Statement *** Change- 3/19/23
        if (section != null
          && section.Name == directive.Name
          && section.HasData)
        {
          // Save the section ending line and stop processing lines for
          // the current Repeatitem.
          SectionEndLineIndex = lineIndex;
          lineIndex = TemplateLines.Length;
        }
        if ((section != null && false == section.HasData)
          || lastRepeatItem)
        {
          RemoveActiveSection();
          if (ActiveSections.Count > 0)
          {
            mProcessLines = ActiveSections[ActiveSections.Count - 1].HasData;
          }
          else
          {
            mProcessLines = true;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the output file specification.</summary>
    public string OutFileSpec { get; set; }

    /// <summary>Gets or sets the data sections.</summary>
    public Sections Sections { get; private set; }

    /// <summary>Gets or sets the template text lines.</summary>
    public string[] TemplateLines { get; private set; }

    /// <summary>Gets or sets the output text lines.</summary>
    public List<string> OutLines { get; private set; }

    /// <summary>Gets or sets the active sections.</summary>
    public Sections ActiveSections { get; private set; }

    // Gets or sets the index to the section last line.
    private int SectionEndLineIndex { get; set; }
    #endregion

    #region Class Data

    //private bool mSectionHasData;
    private bool mProcessLines;
    #endregion
  }
}
