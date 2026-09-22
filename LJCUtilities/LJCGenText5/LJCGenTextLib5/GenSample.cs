// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenerateText.cs
using LJCGenTextXML5;
using LJCNetCommon5;

namespace LJCGenTextLib5
{
  // Provides methods to generate sample Sections object.
  /// <include file='Doc/GenerateText.xml'
  ///  path='items/GenerateText/*'/>
  public class GenSample
  {
    #region Static Method

    // Create the Out filespec from the dataFileSpec and outputFileSpec values.
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/GetOutFileSpec/*'/>
    public static string? GetOutFileSpec(string dataFileSpec, string outputFileSpec)
    {
      string? retValue = null;

      if (LJC.HasText(outputFileSpec))
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

    // </summary>Get the Sections from the data file.
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/GetDataSections/*'/>
    public static Sections? GetDataSections(string dataFileSpec)
    {
      string? errorText = null;
      Sections? retValue;

      if (!LJC.HasText(dataFileSpec))
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
          retValue = LJC.XmlDeserialize(typeof(Sections)
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
    #endregion

    #region Properties

    // Gets or sets the line comment start characters.
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/CommentChars/*'/>
    public string CommentChars { get; set; } = null!;
    #endregion

    #region Public Methods

    // Create Sections from template definition.
    /// <include file='Doc/GenerateText.xml'
    ///  path='items/CreateSections/*'/>
    public Sections? CreateSections(string[] templateLines)
    {
      Sections? retValue;

      if (null == CommentChars)
      {
        CommentChars = "//";
      }

      LJCReflect? reflectDefaults = null;
      if (LJC.XmlDeserialize(typeof(DefaultValues)
        , "DefaultValues.xml") is DefaultValues defaultValues)
      {
        reflectDefaults = new(defaultValues);
      }
      retValue = [];

      while (true)
      {
        Section? section = null;
        for (int lineIndex = 0; lineIndex < templateLines.Length; lineIndex++)
        {
          string line = templateLines[lineIndex];
          var directive = Directive.GetDirective(line, CommentChars);
          if (null == directive)
          {
            continue;
          }

          if (Directive.IsSectionDirective(line, CommentChars))
          {
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

          // A section is not active or not a value.
          if (null == section
            || !Directive.IsValue(directive))
          {
            continue;
          }

          // Create a repeat item.
          if (0 == section.RepeatItems.Count)
          {
            section.RepeatItems.Add("Item1");
          }

          // Check for existing replacement.
          Replacement? replacement = null;
          var replacements = section.RepeatItems[0].Replacements;
          if (replacements.Count > 0)
          {
            replacement = replacements.Retrieve(directive.Name);
          }

          if (replacement != null)
          {
            continue;
          }

          // Create undefined replacement.
          string? propertyValue = directive.Value;

          // Get default value if available.
          if (reflectDefaults != null
            && null == propertyValue)
          {
            var propertyName = directive.Name.Replace("_", "");
            if (reflectDefaults.HasProperty(propertyName))
            {
              propertyValue = reflectDefaults.GetString(propertyName);
            }
          }

          if (null == propertyValue)
          {
            propertyValue = "";
          }
          replacements.Add(directive.Name, propertyValue);
        }
        break;
      }
      return retValue;
    }

    public void PopulateSection(Section section)
    {
    }
    #endregion
  }
}
