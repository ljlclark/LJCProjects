// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenerateText.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace LJCGenTextLib
{
  // Generates text using a text template.
  /// <include file='Doc/GenerateText.xml'
  ///  path='items/GenerateText/*'/>
  public class GenerateText
  {
    #region Public Methods

    // Create Sections from template definition.
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/CreateSections/*'/>
    public Sections CreateSections(string[] templateLines)
    {
      Sections retValue;

      if (null == CommentChars)
      {
        CommentChars = "//";
      }

      DefaultValues defaultValues = NetCommon.XmlDeserialize(typeof(DefaultValues)
        , "DefaultValues.xml") as DefaultValues;
      LJCReflect reflect = new LJCReflect(defaultValues);

      retValue = new Sections();
      Section section = null;
      for (int lineIndex = 0; lineIndex < templateLines.Length; lineIndex++)
      {
        string line = templateLines[lineIndex];
        var directive = Directive.GetDirective(line, CommentChars);
        if (null == directive)
        {
          continue;
        }

        bool isSectionDirective = false;
        if (Directive.IsSectionDirective(line, CommentChars))
        {
          isSectionDirective = true;

          switch (directive.ID.ToLower())
          {
            case Directive.SectionBegin:
              section = retValue.Retrieve(directive.Name);
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

        // A section is active
        // and line is not a section directive.
        if (section != null
          && !isSectionDirective)
        {
          if (Directive.IsValue(directive))
          {
            // Create a repeat item.
            if (0 == section.RepeatItems.Count)
            {
              section.RepeatItems.Add("Item1");
            }

            // Check for existing replacement.
            Replacement replacement = null;
            var replacements = section.RepeatItems[0].Replacements;
            if (replacements.Count > 0)
            {
              replacement = replacements.Retrieve(directive.Name);
            }

            // Create undefined replacement.
            if (null == replacement)
            {
              string propertyValue = directive.Value;

              // Get default value if available.
              if (null == propertyValue)
              {
                var propertyName = directive.Name.Replace("_", "");
                if (reflect.HasProperty(propertyName))
                {
                  propertyValue = reflect.GetString(propertyName);
                }
              }

              replacements.Add(directive.Name, propertyValue);
            }
          }
        }
      }
      return retValue;
    }

    // </summary>Get the Sections from the data file.
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/GetDataSections/*'/>
    public Sections GetDataSections(string dataFileSpec)
    {
      string errorText = null;
      Sections retValue;

      if (!NetString.HasValue(dataFileSpec))
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
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/GetOutFileSpec/*'/>
    public string GetOutFileSpec(string dataFileSpec, string outputFileSpec)
    {
      string retValue = null;

      if (NetString.HasValue(outputFileSpec))
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

    // Gets or sets the line comment start characters.
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/CommentChars/*'/>
    public string CommentChars { get; set; }
    #endregion
  }
}
