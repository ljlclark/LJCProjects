// Copyright(c) Lester J. Clark and Contributors.
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
    #region Public Methods

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

      // *** Begin *** Add
      if (null == CommentChars)
      {
        CommentChars = "//";
      }
      // *** End   *** Add

      DefaultValues defaultValues = NetCommon.XmlDeserialize(typeof(DefaultValues)
        , "DefaultValues.xml") as DefaultValues;
      LJCReflect reflect = new LJCReflect(defaultValues);

      retValue = new Sections();
      for (int lineIndex = 0; lineIndex < templateLines.Length; lineIndex++)
      {
        IsSectionDirective = false;
        string line = templateLines[lineIndex];
        directive = Directive.GetDirective(line, CommentChars);

        if (directive != null)
        {
          if (Directive.IsSectionDirective(line, CommentChars))
          {
            IsSectionDirective = true;

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
              replacement = replacements.Retrieve(directive.Name);
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

    /// <summary></summary>Get the Sections from the data file.
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

    /// <summary>Create the Out filespec from the dataFileSpec and outputFileSpec values.</summary>
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

    /// <summary>Gets or sets the line comment start characters.</summary>
    public string CommentChars { get; set; }
    #endregion
  }
}
