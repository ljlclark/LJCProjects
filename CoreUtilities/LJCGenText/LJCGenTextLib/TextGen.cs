// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGen.cs
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

    /// <summary>
    /// Generate the Output text.
    /// </summary>
    /// <param name="sections">The Sections object.</param>
    /// <param name="templateLines">The template lines.</param>
    public string TextGen(Sections sections, string[] templateLines)
    {
      Sections = sections;
      Lines = templateLines;

      for (var lineIndex = 0; lineIndex < templateLines.Length
        ; lineIndex++)
      {
        var line = Lines[lineIndex];
        if (IsConfig(line))
        {
          continue;
        }

        var directive = Directive.GetDirective(line, CommentChars);
        if (directive != null
          && directive.IsSectionBegin())
        {
          var section = Sections.Retrieve(directive.Name);
          if (null == section)
          {
            // No Section data.
            lineIndex = SkipSection(lineIndex);
          }
          else
          {
            lineIndex++;
            section.BeginLineIndex = lineIndex;
            DoItems(section, ref lineIndex);
          }
          continue;
        }
        AddOutput(line);
      }
      return Output;
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
        var replacement = replacements.Retrieve(directive.Name);
        if ("hasvalue" == directive.Value.ToLower())
        {
          isIf = true;
          if (replacement != null
            && NetString.HasValue(replacement.Value))
          {
            isMatch = true;
          }
        }
        else
        {
          isIf = true;
          if (replacement != null
            && replacement.Value == directive.Value)
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
    private void DoItems(Section section, ref int lineIndex)
    {
      var success = true;
      var items = section.RepeatItems;

      // No Section data.
      if (!NetCommon.HasItems(items))
      {
        success = false;
        lineIndex = SkipSection(lineIndex);
        lineIndex++;
      }

      if (success)
      {
        for (var itemIndex = 0; itemIndex < items.Count; itemIndex++)
        {
          var item = items[itemIndex];

          // No Replacement data.
          if (!NetCommon.HasItems(item.Replacements))
          {
            lineIndex = SkipSection(lineIndex);

            // If not last item.
            if (itemIndex < items.Count - 1)
            {
              // Do section again for following items.
              lineIndex = section.BeginLineIndex;
            }
            continue;
          }

          for (var index = lineIndex; index < Lines.Length; index++)
          {
            var line = Lines[index];
            lineIndex = index;

            var directive = Directive.GetDirective(line, CommentChars);
            if (directive != null)
            {
              if (directive.IsSectionBegin())
              {
                var nextSection = GetBeginSection(line);
                if (null == nextSection)
                {
                  // No Section data.
                  index = SkipSection(lineIndex);
                  continue;
                }

                // RepeatItem processing starts with first line.
                lineIndex++;
                nextSection.BeginLineIndex = lineIndex;

                AddActive(item);
                DoItems(nextSection, ref lineIndex);
                RemoveActive();

                // Continue with returned line index after the processed section.
                index = lineIndex;
              }

              if (directive.IsSectionEnd())
              {
                // If not last item.
                if (itemIndex < items.Count - 1)
                {
                  // Do section again for following items.
                  lineIndex = section.BeginLineIndex;
                  break;
                }
              }

              if (directive.IsIfBegin())
              {
                DoIf(directive, item.Replacements, ref lineIndex);
              }
              index = lineIndex;
            }

            // Does not output directives.
            DoOutput(item.Replacements, line);
          }
        }
      }
    }

    // If not directive, process replacements and add to output.
    private void DoOutput(Replacements replacements, string line)
    {
      if (!Directive.IsDirective(line, CommentChars))
      {
        if (line != null
          && line.Trim().Length > 0)
        {
          DoReplacements(replacements, ref line);
        }
        AddOutput(line);
      }
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
            // Replacement not found in current collection.
            // Search active replacements.
            var active = ActiveReplacements;
            for (var activeIndex = active.Count - 1; activeIndex >= 0; activeIndex--)
            {
              replacement = active[activeIndex].Retrieve(match.Value);
              if (replacement != null)
              {
                lineItem = lineItem.Replace(match.Value, replacement.Value);
                break;
              }
            }
          }
        }
      }
    }

    // Skips to the end of the current section.
    private int SkipSection(int lineIndex)
    {
      var retValue = lineIndex;

      // Skip to end of section.
      for (var index = lineIndex; index < Lines.Length; index++)
      {
        var line = Lines[index];
        if (Directive.IsSectionEnd(line, CommentChars))
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
    private void AddOutput(string line)
    {
      if (Output.Length > 0)
      {
        Output += "\r\n";
      }
      Output += line;
    }

    // Gets the begin section.
    private Section GetBeginSection(string line)
    {
      var directive = Directive.GetDirective(line, CommentChars);
      var retValue = Sections.Retrieve(directive.Name);
      return retValue;
    }

    // Sets the configuration values.
    private bool IsConfig(string line)
    {
      var retValue = false;

      // Sets the configuration.
      if (Directive.IsDirective(line, CommentChars))
      {
        var directive = Directive.GetDirective(line, CommentChars);
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