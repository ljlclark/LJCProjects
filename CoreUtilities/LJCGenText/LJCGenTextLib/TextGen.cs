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
    }

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

        if (Directive.IsSectionBegin(line))
        {
          // *** Begin *** Add
          var directive = Directive.GetDirective(line, CommentChars);
          var section = Sections.Retrieve(directive.Name);
          // *** End   *** Add
          if (null == section)
          {
            // No Section data.
            SkipSection(lineIndex);
          }
          else
          {
            lineIndex++;
            section.StartLineIndex = lineIndex;
            ProcessItems(section, ref lineIndex);
          }
          continue;
        }
        AddOutput(line);
      }
      return Output;
    }

    // Process the RepeatItems.
    private void ProcessItems(Section section, ref int lineIndex)
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
              lineIndex = section.StartLineIndex;
            }
            continue;
          }

          for (var index = lineIndex; index < Lines.Length; index++)
          {
            var line = Lines[index];
            lineIndex = index;

            if (Directive.IsSectionBegin(line))
            {
              // *** Begin *** Add
              var directive = Directive.GetDirective(line, CommentChars);
              var nextSection = Sections.Retrieve(directive.Name);
              // *** End   *** Add
              if (null == nextSection)
              {
                // No Section data.
                index = SkipSection(lineIndex);
                continue;
              }

              // RepeatItem processing starts with first line.
              lineIndex++;
              nextSection.StartLineIndex = lineIndex;

              var hasActiveItem = false;
              if (NetCommon.HasItems(item.Replacements))
              {
                // Add current replacements to Active array.
                hasActiveItem = true;
                ActiveReplacements.Add(item.Replacements);
              }
              ProcessItems(nextSection, ref lineIndex);
              if (hasActiveItem)
              {
                // Remove Replacements that are no longer active.
                ActiveReplacements.RemoveAt(ActiveReplacements.Count - 1);
              }

              // Continue with returned line index after the processed section.
              index = lineIndex;
            }

            if (Directive.IsSectionEnd(line))
            {
              // If not last item.
              if (itemIndex < items.Count - 1)
              {
                // Do section again for following items.
                lineIndex = section.StartLineIndex;
                break;
              }
            }

            var process = DoIf(item.Replacements, line, ref lineIndex);
            index = lineIndex;

            if (process)
            {
              // Does not output directives.
              DoOutput(item.Replacements, line);
            }
          }
        }
      }
    }

    // Perform the line replacements.
    private void DoReplacements(Replacements replacements, ref string lineItem)
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
            for (index = active.Count - 1; index >= 0; index--)
            {
              replacement = active[index].Retrieve(match.Value);
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

    // Process the #IfBegin directive.
    private bool DoIf(Replacements replacements, string line, ref int lineIndex)
    {
      var retValue = true;

      // Check for #IfBegin.
      var success = true;
      if (Directive.IsIfBegin(line))
      {
        success = false;
      }

      // Check replacement value against directive value.
      var isMatch = false;
      if (success)
      {
        // Is #IfBegin so do not output.
        retValue = false;

        var directive = Directive.GetDirective(line, CommentChars);
        var replacement = replacements.Retrieve(directive.Name);
        if ("hasvalue" == directive.Modifier.ToLower())
        {
          if (replacement != null
            && NetString.HasValue(replacement.Value))
          {
            isMatch = true;
          }
        }
        else
        {
          if (replacement != null
            && replacement.Value == directive.Modifier)
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
          line = Lines[index];
          if (isMatch)
          {
            DoOutput(replacements, line);
          }
          if (Directive.IsIfEnd(line))
          {
            lineIndex = index;
            break;
          }
        }
      }
      return retValue;
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

    // Skips to the end of the current section.
    private int SkipSection(int lineIndex)
    {
      var retValue = lineIndex;

      // Skip to end of section.
      for (var index = lineIndex; index < Lines.Length; index++)
      {
        var line = Lines[index];
        if (Directive.IsSectionEnd(line))
        {
          retValue = index++;
          break;
        }
      }
      return retValue;
    }

    // Other Methods

    // Add the line to the output.
    private void AddOutput(string line)
    {
      if (Output.Length > 0)
      {
        Output += "\r\n";
      }
      Output += line;
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