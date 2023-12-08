// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenerateText.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace LJCGenTextLib
{
  // Generates text using a text template.
  /// <include path='items/GenerateText/*' file='Doc/GenerateText.xml'/>
  public class GenerateText
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public GenerateText(string commentStart = "//")
    {
      Reset(commentStart);
    }

    // Resets the current values.
    /// <include path='items/Reset/*' file='Doc/GenerateText.xml'/>
    public void Reset(string commentStart = "//")
    {
      ActiveSections = new Sections();
      CommentStart = commentStart;
      OutLines = new List<string>();
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
      if (!overwrite
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
          if (!string.IsNullOrWhiteSpace(OutFileSpec))
          {
            string folderPath = Path.GetDirectoryName(OutFileSpec);
            if (NetString.HasValue(folderPath)
              && !Directory.Exists(folderPath))
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
        directive = Directive.GetDirective(line, CommentStart);

        if (directive != null)
        {
          if (Directive.IsSectionDirective(directive))
          {
            IsSectionDirective = true;

            switch (directive.ID.ToLower())
            {
              case Directive.SectionBegin:
                section = retValue.LJCSearchName(directive.Name);
                if (null == section)
                {
                  section = retValue.Add(directive.Name);
                }
                break;

              case Directive.SectionEnd:
                section = null;
                break;
            }
          }

          if (section != null
            && !IsSectionDirective)
          {
            if (Directive.IsValue(directive))
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

    #region Private Main Processing Methods

    // Adds the current Section object to the Active List.
    // <include path='items/PushSection/*' file='Doc/GenerateText.xml'/>
    private Section AddActiveSection(Section currentSection, Directive directive
      , int lineIndex)
    {
      // Get directive Section data.
      Section retValue = Sections.LJCSearchName(directive.Name);

      // No Section data.
      if (null == retValue
        && currentSection != null)
      {
        if (currentSection.HasSubsection())
        {
          // Add SubSection if it exists.
          Section subSection = currentSection.CurrentRepeatItem.Subsection;
          if (subSection != null)
          {
            retValue = subSection;
          }
        }
      }

      // Add directive Section even if there is no Section data.
      if (null == retValue)
      {
        retValue = new Section()
        {
          Name = directive.Name
        };
      }

      retValue.EndProcessing = true;
      if (retValue.HasData())
      {
        if (directive.Modifier != null
          && "list" == directive.Modifier.ToLower())
        {
          retValue.IsList = true;
        }
        if (retValue.RepeatItems.Count > 1)
        {
          retValue.EndProcessing = false;
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

      if (Directive.IsSectionBegin(directive))
      {
        retValue = true;

        // Add section to ActiveSections with the section starting index.
        var addSection = AddActiveSection(currentSection, directive, lineIndex);
        if (addSection.HasData())
        {
          GenSection(addSection);

          // Set index to continue at end of processed section.
          lineIndex = SectionEndLineIndex;
        }
        else
        {
          mProcessLines = false;
        }
      }
      return retValue;
    }

    // Generates the code for the current RepeatItem.
    // <include path='items/GenRepeatItem/*' file='Doc/GenerateText.xml'/>
    private void GenRepeatItem(Section currentSection)
    {
      // Start at the beginning of the section.
      var startLineIndex = currentSection.StartLineIndex;
      for (int lineIndex = startLineIndex; lineIndex < TemplateLines.Length;
        lineIndex++)
      {
        string line = TemplateLines[lineIndex];
        var directive = Directive.GetDirective(line, CommentStart);

        // Debugging
        //if (Directive.IsName(directive, "Subsection")
        //  || Directive.IsName(directive, "PublicMethods"))
        //{
        //  int i = 0;
        //}

        // Sets line to null if a Section directive.
        // May change lineIndex to end of section or template.
        if (!SectionDirective(currentSection, directive, ref line
          , ref lineIndex))
        {
          if (line != null
            && currentSection.HasData())
          {
            // Sets line to null if other directive.
            if (!OtherDirective(currentSection, directive, ref line
              , ref lineIndex))
            {
              line = ReplaceValues(line);
              if (currentSection.IsList
                && !currentSection.EndProcessing)
              {
                line += ",";
              }
            }
          }

          if (mProcessLines
            && line != null)
          {
            OutLines.Add(line);
          }
        }
      }
    }

    // Generates each RepeatItem in a section.
    // <include path='items/GenSection/*' file='Doc/GenerateText.xml'/>
    private void GenSection(Section currentSection)
    {
      mProcessLines = true;

      //currentSection.LastRepeatItem = false;
      for (int index = 0; index < currentSection.RepeatItems.Count; index++)
      {
        RepeatItem repeatItem = currentSection.RepeatItems[index];
        currentSection.CurrentRepeatItem = repeatItem;
        if (index == currentSection.RepeatItems.Count - 1)
        {
          currentSection.EndProcessing = true;
        }
        GenRepeatItem(currentSection);
      }
    }

    // Generates lines outside of any Section.
    private void GenSectionNone()
    {
      int startLineIndex = 0;
      for (int lineIndex = startLineIndex; lineIndex < TemplateLines.Length;
        lineIndex++)
      {
        string line = TemplateLines[lineIndex];
        var directive = Directive.GetDirective(line, CommentStart);

        if (Directive.IsSectionBegin(directive))
        {
          line = null;

          // Calls GenSection() with section values.
          Section section = null;
          BeginSection(section, directive, ref lineIndex);
          // Returns when no longer in a section.
        }

        if (line != null
          && !line.Trim().StartsWith("<!--X-"))
        {
          OutLines.Add(line);
        }
      }
    }

    // Handle Section Directives
    private bool SectionDirective(Section currentSection, Directive directive
      , ref string line, ref int lineIndex)
    {
      bool retValue = false;

      if (line != null
        && line.Trim().StartsWith("<!--X-"))
      {
        line = null;
      }

      // Handle directive and do not generate line.
      if (line != null)
      {
        retValue = BeginSection(currentSection, directive, ref lineIndex);
        if (retValue)
        {
          line = null;
        }
      }

      if (Directive.IsSectionEnd(directive))
      {
        line = null;
        retValue = true;
        SectionEndLineIndex = lineIndex;

        if (IsProcessing(currentSection, directive)
          || SubSectionParentHasData(directive))
        {
          // Stop processing RepeatItem.
          lineIndex = TemplateLines.Length;
        }

        if (currentSection.EndProcessing
          || IsEmptySubsection(directive))
        {
          RemoveActiveSection();
          if (ActiveSections.Count > 0)
          {
            var section = ActiveSections[ActiveSections.Count - 1];
            mProcessLines = section.HasData();
          }
          else
          {
            // Continue processing outside of section.
            mProcessLines = true;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Other Private Methods

    // Retrieves the active session replacement value.
    // <include path='items/ActiveSessionReplacementValue/*' file='Doc/GenerateText.xml'/>
    private string ActiveSessionReplacementValue(string replacementName)
    {
      string retValue = null;

      RepeatItems repeatItems = ActiveSessionsRepeatItems();
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
    private RepeatItems ActiveSessionsRepeatItems()
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

    // Generate the If Block statements.
    // <include path='items/GenIfBegin/*' file='Doc/GenerateText.xml'/>
    private void GenIfBlock(Section section, Directive directive
      , ref int lineIndex)
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
        Directive ifDirective = Directive.GetDirective(line, CommentStart);
        if (ifDirective != null)
        {
          if (Directive.IsIfEnd(ifDirective))
          {
            isValid = false;
            lineIndex = ifIndex++;
            ifIndex = TemplateLines.Length;
          }
          if (Directive.IsIfElse(ifDirective))
          {
            if (section.HasData())
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
            if (section.IsList && !section.EndProcessing)
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
        if (!File.Exists(dataFileSpec))
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

      if (!string.IsNullOrWhiteSpace(outputFileSpec))
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

    // Check if an empty subsection.
    private bool IsEmptySubsection(Directive directive)
    {
      bool retValue = false;

      var index = ActiveSections.Count - 1;
      var currentSection = ActiveSections[index];
      if (Directive.IsSubsection(directive)
        && !currentSection.HasData())
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if parent has data.
    private bool SubSectionParentHasData(Directive directive)
    {
      bool retValue = false;

      var index = ActiveSections.Count - 1;
      var currentSection = ActiveSections[index];
      if (Directive.IsSubsection(directive)
        || Section.HasSubsection(currentSection))
      {
        if (Section.IsName(currentSection, "Subsection"))
        {
          index--;
          currentSection = ActiveSections[index];
        }
        if (Section.HasData(currentSection))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Checks if processing repeat items.
    private bool IsProcessing(Section currentSection, Directive directive)
    {
      bool retValue = false;

      if ((Section.HasData(currentSection)
        && currentSection.Name.ToLower() == directive.Name.ToLower()))
      {
        retValue = true;
      }
      return retValue;
    }

    // Handle other directives.
    private bool OtherDirective(Section section, Directive directive
      , ref string line, ref int lineIndex)
    {
      bool retValue = false;

      if (directive != null)
      {
        if (Directive.IsValue(directive))
        {
          retValue = true;
          line = null;
        }

        if (Directive.IsIfBegin(directive))
        {
          // Debugging
          //if (directive.IsName("_PublicMethodCount_"))
          //{
          //  int i = 0;
          //}
          retValue = true;
          line = null;
          GenIfBlock(section, directive, ref lineIndex);
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
        RepeatItems repeatItems = ActiveSessionsRepeatItems();
        foreach (RepeatItem repeatItem in repeatItems)
        {
          var replacements = repeatItem.Replacements;
          bool doNextLevel = false;
          foreach (string token in genTokens)
          {
            // Debugging
            //if ("_methodlistpreface_" == token.ToLower())
            //{
            //  int i = 0;
            //}

            // Replace values if the token has a replacement value.
            Replacement replacement = replacements.LJCSearchName(token);
            if (replacement != null)
            {
              retValue = retValue.Replace(token, replacement.Value);
            }
            else
            {
              doNextLevel = true;
            }
          }
          if (!doNextLevel)
          {
            break;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the active sections.</summary>
    public Sections ActiveSections { get; private set; }

    /// <summary>Gets or sets the line comment start characters.</summary>
    public string CommentStart { get; set; }

    /// <summary>Gets or sets the output file specification.</summary>
    public string OutFileSpec { get; set; }

    /// <summary>Gets or sets the output text lines.</summary>
    public List<string> OutLines { get; private set; }

    /// <summary>Gets or sets the data sections.</summary>
    public Sections Sections { get; private set; }

    // Gets or sets the index to the section last line.
    private int SectionEndLineIndex { get; set; }

    /// <summary>Gets or sets the template text lines.</summary>
    public string[] TemplateLines { get; private set; }
    #endregion

    #region Class Data

    //private bool mSectionHasData;
    private bool mProcessLines;
    #endregion
  }
}
