// Copyright (c) Lester J Clark 2021,2022 - All Rights Reserved
using System.Collections.Generic;

namespace LJCTextDataReaderLib
{
  /// <summary>Represents a collection of TextRegion objects.</summary>
  public class TextRegions : List<TextRegion>
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    public TextRegions()
    {
      LJCFieldDelimiter = ',';
      LJCRegionDelimiter = '\"';
    }
    #endregion

    #region Collection Methods

    /// <summary>
    /// Creates a TextRegion from the supplied values and adds it to the collection.
    /// /// </summary>
    /// <param name="beginIndex">The beginning index.</param>
    /// <param name="endIndex">The ending index.</param>
    /// <returns>A reference to the created TextRegion object.</returns>
    public TextRegion Add(int beginIndex, int endIndex)
    {
      TextRegion retValue;

      retValue = new TextRegion()
      {
        BeginIndex = beginIndex,
        EndIndex = endIndex
      };
      Add(retValue);
      return retValue;
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Returns a value indicating if the supplied text value has text regions.
    /// </summary>
    /// <param name="text">The text value.</param>
    /// <returns>Returns 'true' if the supplied text has regions.
    /// Otherwise 'false'.</returns>
    public bool LJCHasRegions(string text)
    {
      bool retValue = false;

      Clear();

      int currentIndex = 0;
      int beginIndex = text.IndexOf(LJCRegionDelimiter, currentIndex);
      while (beginIndex > -1)
      {
        currentIndex = beginIndex + 1;
        int endIndex = text.IndexOf(LJCRegionDelimiter, currentIndex);
        if (-1 == endIndex)
        {
          beginIndex = -1;
        }
        else
        {
          retValue = true;
          Add(beginIndex, endIndex);

          currentIndex = endIndex + 1;
          //endIndex = -1;
          beginIndex = text.IndexOf(LJCRegionDelimiter, currentIndex);
        }
      }
      return retValue;
    }

    /// <summary>
    /// Determines if a delimiter is in a text region.
    /// </summary>
    /// <param name="index">The index of the delimiter.</param>
    /// <returns>Returns 'true' if the delimiter is in a text region
    /// otherwise 'false'.</returns>
    public bool LJCIsInRegion(int index)
    {
      bool retValue = false;

      foreach (TextRegion region in this)
      {
        if (region.BeginIndex <= index && region.EndIndex >= index)
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    /// <summary>
    /// Splits a line of text on the delimiters not enclosed in text regions.
    /// </summary>
    /// <param name="line">The line of text</param>
    /// <returns>An array of field values.</returns>
    public string[] LJCSplit(string line)
    {
      List<string> fields = new List<string>();
      int currentIndex;
      string[] retValue = null;

      int beginIndex = -1;
      if (line != null && line.Length > 0)
      {
        beginIndex = 0;
      }
      while (beginIndex > -1 && beginIndex < line.Length)
      {
        // Skip quote at beginning of field.
        if (line.Substring(beginIndex).Trim().StartsWith("\""))
        {
          int index = line.Substring(beginIndex).IndexOf('\"');
          beginIndex += index + 1;
        }

        // Find the end of field.
        //currentIndex = beginIndex + 1;
        currentIndex = beginIndex;
        int endIndex = line.IndexOf(LJCFieldDelimiter, currentIndex);
        if (-1 == endIndex)
        {
          // No more field delimiters so use length to get last field.
          endIndex = line.Length;
        }
        if (endIndex > -1)
        {
          while (LJCIsInRegion(endIndex))
          {
            currentIndex = endIndex + 1;
            endIndex = line.IndexOf(LJCFieldDelimiter, currentIndex);
            if (-1 == endIndex)
            {
              // No more field delimiters so use length to get last field.
              endIndex = line.Length;
            }
          }

          // Get the field value.
          if (endIndex > -1)
          {
            currentIndex = endIndex + 1;
            string value = line.Substring(beginIndex, endIndex - beginIndex).Trim();

            // Remove trailing quote.
            if (value.EndsWith("\""))
            {
              value = value.Substring(0, value.Length - 1);
            }

            fields.Add(value);
          }
        }

        // Set beginning of next field.
        beginIndex = currentIndex;
      }

      if (fields.Count > 0)
      {
        retValue = new string[fields.Count];
        for (int index = 0; index < fields.Count; index++)
        {
          retValue[index] = fields[index];
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The field delimiter.</summary>
    public char LJCFieldDelimiter { get; set; }

    /// <summary>The text region delimiter.</summary>
    public char LJCRegionDelimiter { get; set; }
    #endregion
  }
}
