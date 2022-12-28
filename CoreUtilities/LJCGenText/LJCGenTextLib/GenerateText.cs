// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// GenerateText.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace LJCGenTextLib
{
  // Generates text using a text template.
  /// <include path='items/GenerateText/*' file='Doc/GenerateText.xml'/>
  public class GenerateText
  {
    // #001 - Allow section to work even if the data file has no data for that section.
    // #002 - Handle the #Value directive.
    // #003 - Modify to work with HTML.

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
          GenSection(null);
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
                string propertyName = directive.Name.Replace("_", "");
                string propertyValue = reflect.GetString(propertyName);
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

    // Generates each RepeatItem in a section.
    /// <include path='items/GenSection/*' file='Doc/GenerateText.xml'/>
    private void GenSection(Section section)
    {
      // #001 Next Statement - Add
      mSectionHasData = true;
      if (null == section || 0 == section.RepeatItems.Count)
      {
        // Generate non-section lines.
        GenRepeatItem(null);
      }
      else
      {
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
        PopSection();
      }
    }

    // Generates the code for the current RepeatItem.
    /// <include path='items/GenRepeatItem/*' file='Doc/GenerateText.xml'/>
    private void GenRepeatItem(Section section, bool lastRepeatItem = false)
    {
      Directive directive;
      Section newSection;

      int startLineIndex = 0;

      if (section != null)
      {
        // Start at the beginning of the section.
        startLineIndex = section.StartLineIndex;
      }
      for (int lineIndex = startLineIndex; lineIndex < TemplateLines.Length; lineIndex++)
      {
        string line = TemplateLines[lineIndex];
        directive = GenCommon.GetDirective(line);
        if (GenCommon.IsSectionDirective(directive))
        {
          // Handle directive and do not generate line.
          if (GenCommon.IsBeginSection(directive))
          {
            // Push section to ActiveSections with the section starting index.
            newSection = PushSection(directive, lineIndex);
            // #001 Begin - Add
            if (null == newSection
              || 0 == newSection.RepeatItems.Count)
            {
              mSectionHasData = false;
            }
            else
            {
              mSectionHasData = true;
              // #001 End - Add
              GenSection(newSection);

              // Set the index to continue at the end of the processed section.
              lineIndex = SectionEndLineIndex;
            }
          }
          if (GenCommon.IsEndSection(directive))
          {
            // Continue processing if the section does not have data.
            // #001 Next Statement - Add
            if (mSectionHasData)
            {
              // Save the section ending line and stop processing lines for
              // the current Repeatitem.
              SectionEndLineIndex = lineIndex;
              lineIndex = TemplateLines.Length;
            }
            // #001 Next Statement - Add
            mSectionHasData = true;
          }
        }
        else
        {
          if (section != null)
          {
            if (directive != null)
            {
              // #002 Begin - Add
              if (GenCommon.IsValue(directive))
              {
                line = null;
              }
              // #002 End - Add
              if (GenCommon.IsBeginIf(directive))
              {
                GenIfBlock(section, directive, ref lineIndex, lastRepeatItem);
                line = null;
              }
            }
            else
            {
              line = ReplaceValues(line);
              if (section.IsList && false == lastRepeatItem)
              {
                line += ",";
              }
            }
          }

          // #001 Next Statement - Change
          if (mSectionHasData
            && line != null)
          {
            if (false == line.Trim().StartsWith("<!--X-"))
            {
              OutLines.Add(line);
            }
          }
        }
      }
    }

    // Generate the If Block statements.
    /// <include path='items/GenIfBegin/*' file='Doc/GenerateText.xml'/>
    private void GenIfBlock(Section section, Directive directive, ref int lineIndex
      , bool lastRepeatItem)
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
          if (GenCommon.IsEndIf(ifDirective))
          {
            isValid = false;
            lineIndex = ifIndex++;
            ifIndex = TemplateLines.Length;
          }
          if (GenCommon.IsElseIf(ifDirective))
          {
            // #001 Next Statement - Add
            if (mSectionHasData)
            {
              isValid = !isValid;
            }
            lineIndex++;
          }
        }
        else
        {
          if (isValid)
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

    // Replaces the values for the active RepeateItems.
    /// <include path='items/ReplaceValues/*' file='Doc/GenerateText.xml'/>
    private string ReplaceValues(string line)
    {
      GenTokens genTokens = new GenTokens();
      string retValue;

      retValue = line;

      RepeatItems repeatItems = ActiveSessionRepeateItems();
      foreach (RepeatItem repeatItem in repeatItems)
      {
        // Attempt replacement only if the line contains replacement tokens.
        genTokens.SetTokens(line);
        if (genTokens.Count > 0)
        {
          foreach (string token in genTokens)
          {
            // Replace values if the token has a replacement value.
            Replacement replacement = repeatItem.Replacements.LJCSearchName(token);
            if (replacement != null)
            {
              retValue = retValue.Replace(token, replacement.Value);
            }
          }
        }
      }
      return retValue;
    }

    // Retrieves the active session replacement value.
    /// <include path='items/ActiveSessionReplacementValue/*' file='Doc/GenerateText.xml'/>
    private string ActiveSessionReplacementValue(string replacementName)
    {
      string retValue = null;

      RepeatItems repeatItems = ActiveSessionRepeateItems();
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
    /// <include path='items/ActiveSessionRepeateItems/*' file='Doc/GenerateText.xml'/>
    private RepeatItems ActiveSessionRepeateItems()
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

    // Pushes the current Section object on the Active stack.
    /// <include path='items/PushSection/*' file='Doc/GenerateText.xml'/>
    private Section PushSection(Directive directive, int lineIndex)
    {
      Section retValue = Sections.LJCSearchName(directive.Name);
      if (retValue != null
        && retValue.RepeatItems != null
        && retValue.RepeatItems.Count > 0)
      {
        //Section activeSection = ActiveSections.LJCSearchName(directive.Name);
        if (directive.Modifier != null
          && "list" == directive.Modifier.ToLower())
        {
          retValue.IsList = true;
        }
        retValue.StartLineIndex = lineIndex + 1;
        ActiveSections.Add(retValue);
      }
      return retValue;
    }

    // Pops the completed Section off of the Active stack.
    /// <include path='items/PopSection/*' file='Doc/GenerateText.xml'/>
    private void PopSection()
    {
      if (ActiveSections != null && ActiveSections.Count > 0)
      {
        ActiveSections.RemoveAt(ActiveSections.Count - 1);
      }
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

    // #001 Next Statement - Add
    private bool mSectionHasData;
    #endregion
  }
}
