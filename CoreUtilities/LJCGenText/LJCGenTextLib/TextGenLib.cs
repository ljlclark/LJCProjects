// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenLib.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LJCGenTextLib
{
  /// <summary>Generate output text from a template and data.</summary>
  public class TextGenLib
  {
    /// <summary>Initializes an object instance.</summary>
    public TextGenLib()
    {
      CommentChars = "//";
      PlaceholderBegin = "_";
      PlaceholderEnd = "_";

      // Private Properties
      ActiveReplacements = new List<Replacements>();
      Output = "";
    }

    #region Main Processing Methods

    // Generate the Output text.
    /// <include path='items/TextGen/*' file='Doc/TextGenLib.xml'/>
    public string TextGen(Sections sections, string[] templateLines)
    {
      Sections = sections;
      Lines = templateLines;

      for (var lineIndex = 0; lineIndex < templateLines.Length; lineIndex++)
      {
        var line = Lines[lineIndex];
        if (IsConfig(line, out Directive directive))
        {
          continue;
        }

        // Only SectionBegin is valid. Other directives should not appear.
        if (directive != null)
        {
          var name = directive.ID;
          if (name.ToLower() != "#sectionbegin")
          {
            continue;
          }
        }

        if (directive != null
          && directive.IsSectionBegin())
        {
          var section = Sections.Retrieve(directive.Name);
          if (null == section)
          {
            // No Section data.
            lineIndex = SkipSection(lineIndex, directive.Name);
          }
          else
          {
            lineIndex++;
            section.BeginLineIndex = lineIndex;
            DoItems(section, ref lineIndex);
            continue;
          }
        }

        // Add text before first section and after last section.
        AddOutput(line);
      }
      return Output;
    }

    // Performs replacement from active replacement value.
    private string ActiveReplacement(string line, string replacementName)
    {
      string retValue = line;

      var value = ActiveValue(replacementName);
      if (NetString.HasValue(value))
      {
        retValue = retValue.Replace(replacementName, value);
      }
      return retValue;
    }

    // Gets the active replacement value.
    private string ActiveValue(string replacementName)
    {
      string retValue = null;

      var active = ActiveReplacements;
      for (var index = active.Count - 1; index >= 0; index--)
      {
        var replacement = active[index].Retrieve(replacementName);
        if (replacement != null)
        {
          retValue = replacement.Value;
          break;
        }
      }
      return retValue;
    }

    // Process the #IfBegin directive.
    private void DoIf(Directive directive, Replacements replacements
      , ref int lineIndex)
    {
      // Check replacement value against directive value.
      var success = true;
      var isIf = false;
      var isMatch = false;
      if (!NetString.HasValue(directive.Value))
      {
        success = false;
      }

      // Check replacement value against directive value.
      if (success)
      {
        isIf = true;

        string value;
        var replacement = replacements.Retrieve(directive.Name);
        if (replacement != null)
        {
          value = replacement.Value;
        }
        else
        {
          value = ActiveValue(directive.Name);
        }

        if (NetString.HasValue(value))
        {
          if ("hasvalue" == directive.Value.ToLower()
            || value == directive.Value)
          {
            isMatch = true;
          }
        }
      }

      if (success)
      {
        // Process to IfEnd.
        lineIndex++;
        for (var index = lineIndex; index < Lines.Length; index++)
        {
          var line = Lines[index];
          if (isMatch
            && !Directive.IsIfElse(line, CommentChars))
          {
            DoOutput(replacements, line);
          }
          if (isIf
            && Directive.IsIfElse(line, CommentChars))
          {
            isMatch = !isMatch;
          }
          if (Directive.IsIfEnd(line, CommentChars))
          {
            lineIndex = index;
            break;
          }
        }
      }
    }

    // Process the RepeatItems.
    private void DoItems(Section section, ref int nextLineIndex)
    {
      var success = true;
      var repeatItems = section.RepeatItems;

      // No Section data.
      if (!NetCommon.HasItems(repeatItems))
      {
        success = false;
        nextLineIndex = SkipSection(nextLineIndex, section.Name);
        nextLineIndex++;
      }

      if (success)
      {
        // Process each RepeatItem.
        for (var itemIndex = 0; itemIndex < repeatItems.Count; itemIndex++)
        {
          var repeatItem = repeatItems[itemIndex];

          // No Replacement data.
          if (!NetCommon.HasItems(repeatItem.Replacements))
          {
            nextLineIndex = SkipSection(nextLineIndex, section.Name);

            // If not last repeat item.
            if (itemIndex < repeatItems.Count - 1)
            {
              // Do section again for following items.
              nextLineIndex = section.BeginLineIndex;
            }
            continue;
          }

          // Process template lines.
          for (var lineIndex = nextLineIndex; lineIndex < Lines.Length; lineIndex++)
          {
            var line = Lines[lineIndex];
            nextLineIndex = lineIndex;

            var directive = Directive.GetDirective(line, CommentChars);

            // Skip unprocessed directives.
            if (directive != null
              && IsNotProcessValues(directive.ID))
            {
              continue;
            }

            // Do output if directive is null or is not a processed directive.
            if (directive == null
              || (directive != null
              && !IsProcessValues(directive.ID)))
            {
              DoOutput(repeatItem.Replacements, line);
              continue;
            }

            if (directive.IsSectionBegin())
            {
              if (DoSubsection(directive, repeatItem, ref nextLineIndex))
              {
                lineIndex = nextLineIndex;
                continue;
              }

              var nextSection = GetBeginSection(line);
              if (null == nextSection)
              {
                // No Section data.
                lineIndex = SkipSection(nextLineIndex, directive.Name);
                continue;
              }

              ProcessItems(nextSection, repeatItem, ref nextLineIndex);
              lineIndex = nextLineIndex;
              continue;
            }

            if (directive.IsSectionEnd())
            {
              // If not last item.
              if (itemIndex < repeatItems.Count - 1)
              {
                // Do section again for following items.
                nextLineIndex = section.BeginLineIndex;
              }
              break;
            }

            if (directive.IsIfBegin())
            {
              DoIf(directive, repeatItem.Replacements, ref nextLineIndex);
              lineIndex = nextLineIndex;
            }
          }
        }
      }
    }

    // If not directive, process replacements and add to output.
    private string DoOutput(Replacements replacements, string line)
    {
      string retValue = null;

      if (!Directive.IsDirective(line, CommentChars))
      {
        if (line != null
          && line.Trim().Length > 0)
        {
          DoReplacements(replacements, ref line);
        }
        retValue = AddOutput(line);
      }
      return retValue;
    }

    // Perform the line replacements.
    private void DoReplacements(Replacements replacements
      , ref string lineItem)
    {
      if (lineItem.Contains(PlaceholderBegin))
      {
        replacements.Sort();
        var line = lineItem;

        var pattern = $"{PlaceholderBegin}.+?{PlaceholderEnd}";
        var matches = Regex.Matches(line, pattern);
        for (var index = 0; index < matches.Count; index++)
        {
          var match = matches[index];
          var replacement = replacements.Retrieve(match.Value);
          if (replacement != null)
          {
            lineItem = lineItem.Replace(match.Value, replacement.Value);
          }
          else
          {
            lineItem = ActiveReplacement(lineItem, match.Value);
          }
        }
      }
    }

    private bool DoSubsection(Directive directive, RepeatItem repeatItem
      , ref int nextLineIndex)
    {
      bool retValue = false;

      if (directive.Name.ToLower() == "subsection"
        && repeatItem.Subsection != null)
      {
        var subSection = repeatItem.Subsection;
        if (subSection != null)
        {
          ProcessItems(subSection, repeatItem, ref nextLineIndex);
          retValue = true;
        }
      }
      return retValue;
    }

    // Process the repeat items.
    private void ProcessItems(Section section, RepeatItem repeatItem
      , ref int nextLineIndex)
    {
      // RepeatItem processing starts with first line.
      nextLineIndex++;
      section.BeginLineIndex = nextLineIndex;

      AddActive(repeatItem);
      DoItems(section, ref nextLineIndex);
      RemoveActive();
    }

    // Skips to the end of the current section.
    private int SkipSection(int lineIndex, string name)
    {
      var retValue = lineIndex;

      // Skip to end of section.
      for (var index = lineIndex; index < Lines.Length; index++)
      {
        var line = Lines[index];

        var directive = Directive.GetDirective(line, CommentChars);
        if (Directive.IsSectionEnd(line, CommentChars)
          && directive.Name == name)
        {
          retValue = index++;
          break;
        }
      }
      return retValue;
    }
    #endregion

    #region Other Methods

    // Add current replacements to Active array.
    private void AddActive(RepeatItem item)
    {
      if (NetCommon.HasItems(item.Replacements))
      {
        ActiveReplacements.Add(item.Replacements);
      }
    }

    // Add the line to the output.
    private string AddOutput(string line)
    {
      string retValue = null;

      if (!line.Trim().StartsWith("<!--X"))
      {
        if (Output.Length > 0)
        {
          Output += "\r\n";
        }
        Output += line;
        retValue = line;
      }
      return retValue;
    }

    // Gets the begin section.
    private Section GetBeginSection(string line)
    {
      var directive = Directive.GetDirective(line, CommentChars);
      var retValue = Sections.Retrieve(directive.Name);
      return retValue;
    }

    // Sets the configuration values.
    private bool IsConfig(string line, out Directive directive)
    {
      var retValue = false;

      // Sets the configuration.
      if (line.ToLower().Contains("#commentchars"))
      {
        string[] values = NetString.Split(line, " ");
        if (values.Length >= 2)
        {
          CommentChars = values[2];
        }
      }

      directive = Directive.GetDirective(line, CommentChars);
      if (directive != null)
      {
        switch (directive.ID.ToLower())
        {
          case "#commentchars":
            retValue = true;
            CommentChars = directive.Name;
            break;

          case "#placeholderbegin":
            retValue = true;
            PlaceholderBegin = directive.Name;
            break;

          case "#placeholderend":
            retValue = true;
            PlaceholderEnd = directive.Name;
            break;
        }
      }
      return retValue;
    }

    // Checks if a value is in a list of names.
    private bool IsInValues(string text, params string[] values)
    {
      bool retValue = false;

      foreach (var value in values)
      {
        if (value.ToLower() == text.ToLower())
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // true if not DoItems process values; otherwise false.
    private bool IsNotProcessValues(string name)
    {
      var retValue = IsInValues(name, "#Value", "#IfElse", "#IfEnd");
      return retValue;
    }

    // true if DoItems process values; otherwise false.
    private bool IsProcessValues(string name)
    {
      var retValue = IsInValues(name, "#SectionBegin", "#SectionEnd"
        , "#IfBegin");
      return retValue;
    }

    // Remove Replacements that are no longer active.
    private void RemoveActive()
    {
      if (NetCommon.HasItems(ActiveReplacements))
      {
        ActiveReplacements.RemoveAt(ActiveReplacements.Count - 1);
      }
    }
    #endregion

    #region Properties

    /// <summary></summary>
    public string CommentChars { get; set; }

    /// <summary></summary>
    public string PlaceholderBegin { get; set; }

    /// <summary></summary>
    public string PlaceholderEnd { get; set; }

    private List<Replacements> ActiveReplacements { get; set; }

    private string[] Lines { get; set; }

    private string Output { get; set; }

    private Sections Sections { get; set; }
    #endregion
  }
}