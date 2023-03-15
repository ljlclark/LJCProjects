// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenCommon.cs
using System;
using System.IO;

namespace LJCGenTextLib
{
  internal static class GenCommon
  {
    // Get the template lines from the template file.
    internal static string[] GetTemplateLines(string templateFileSpec
      , out string errorText)
    {
      string[] retValue = null;

      errorText = null;
      if (string.IsNullOrWhiteSpace(templateFileSpec))
      {
        errorText += "Missing Template file specification.\r\n";
      }
      else
      {
        if (false == File.Exists(templateFileSpec))
        {
          errorText += $"Template file '{templateFileSpec}' is not found.\r\n";
        }
        else
        {
          retValue = File.ReadAllLines(templateFileSpec);
          if (null == retValue || 0 == retValue.Length)
          {
            errorText += $"No lines found in Template file '{templateFileSpec}'.";
          }
        }
      }
      return retValue;
    }

    // Checks the line for a directive and returns the directive name and value.
    internal static Directive GetDirective(string line)
    {
      string[] values;
      char[] separator = { ' ' };
      Directive retValue = null;

      // Templates directive is in a comment.
      if (line.Trim().StartsWith("//")
        || line.Trim().StartsWith("<!--"))
      {
        // Template directive starts with "#".
        int index = line.IndexOf('#');
        if (index > -1)
        {
          retValue = new Directive();
          values = line.Substring(index).Split(separator, StringSplitOptions.RemoveEmptyEntries);
          if (values.Length > 0)
          {
            // The directive identifier.
            retValue.ID = values[0];
          }
          if (values.Length > 1)
          {
            retValue.Name = values[1];
          }
          if (values.Length > 2)
          {
            retValue.Modifier = values[2];
          }
        }
      }
      return retValue;
    }

    // Checks for a valid SectionBegin or SectionEnd directive.
    internal static bool IsSectionDirective(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && (IsBeginSection(directive)
        || IsEndSection(directive)))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionBegin directive.
    internal static bool IsBeginSection(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && BeginSection == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionEnd directive.
    internal static bool IsEndSection(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && EndSection == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid Value directive.
    internal static bool IsValue(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && ValueDirective == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfBegin directive.
    internal static bool IsIfBegin(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && BeginIf == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfElse directive.
    internal static bool IsIfElse(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && ElseIf == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfEnd directive.
    internal static bool IsIfEnd(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && EndIf == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    internal const string BeginSection = "#sectionbegin";
    internal const string EndSection = "#sectionend";
    internal const string ValueDirective = "#value";
    internal const string BeginIf = "#ifbegin";
    internal const string ElseIf = "#ifelse";
    internal const string EndIf = "#ifend";
  }
}
