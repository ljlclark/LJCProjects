// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextRegions5.cs
using LJCNetCommon5;

namespace LJCTextDataReader5
{
  // Represents a collection of TextRegion objects.
  /// <include file='Doc/ProjectTextRegions.xml'
  ///  path='items/LJCTextRegions/*'/>
  public class LJCTextRegions : List<LJCTextRegion>
  {
    #region Properties

    // The field delimiter.
    /// <include file='Doc/TextRegions.xml'
    ///  path='items/LJCFieldDelimiter/*'/>
    public char LJCFieldDelimiter { get; set; }

    // The text region delimiter.
    /// <include file='Doc/TextRegions.xml'
    ///  path='items/LJCRegionDelimiter/*'/>
    public char LJCRegionDelimiter { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/TextRegions.xml'
    ///  path='items/Constructor/*'/>
    public LJCTextRegions()
    {
      LJCFieldDelimiter = ',';
      LJCRegionDelimiter = '\"';
    }
    #endregion

    #region Collection Methods

    // Creates a TextRegion from the supplied values and adds it to the collection.
    /// <include file='Doc/TextRegions.xml'
    ///  path='items/Add/*'/>
    public LJCTextRegion Add(int beginIndex, int endIndex)
    {
      LJCTextRegion retValue;

      retValue = new LJCTextRegion()
      {
        BeginIndex = beginIndex,
        EndIndex = endIndex
      };
      Add(retValue);
      return retValue;
    }
    #endregion

    #region Other Methods

    // Returns a value indicating if the supplied text value has text regions.
    /// <include file='Doc/TextRegions.xml'
    ///  path='items/LJCHasRegions/*'/>
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

    // Determines if a delimiter is in a text region.
    /// <include file='Doc/TextRegions.xml'
    ///  path='items/LJCIsInRegion/*'/>
    public bool LJCIsInRegion(int index)
    {
      bool retValue = false;

      foreach (LJCTextRegion region in this)
      {
        if (region.BeginIndex <= index && region.EndIndex >= index)
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Splits a line of text on the delimiters not enclosed in text regions.
    /// <include file='Doc/TextRegions.xml'
    ///  path='items/LJCSplit/*'/>
    public string[]? LJCSplit(string line)
    {
      var fields = new List<string>();
      int currentIndex;
      string[]? retValue = null;

      int beginIndex = -1;
      if (LJC.HasText(line))
      {
        beginIndex = 0;
      }
      while (beginIndex > -1 && beginIndex < line.Length)
      {
        // Skip quote at beginning of field.
        //if (line.Substring(beginIndex).Trim().StartsWith('\"'))
        if (line[beginIndex..].Trim().StartsWith('\"'))
        {
          //int index = line.Substring(beginIndex).IndexOf('\"');
          int index = line[beginIndex..].IndexOf('\"');
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
            //string value = line.Substring(beginIndex, endIndex - beginIndex).Trim();
            string value = line[beginIndex..endIndex].Trim();

            // Remove trailing quote.
            if (value.EndsWith('\"'))
            {
              //value = value.Substring(0, value.Length - 1);
              value = value[..^1];
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
  }
}
