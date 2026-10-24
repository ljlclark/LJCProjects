// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LineOffsets5.cs

namespace LJCTextDataReader5
{
  // Represents a collection of LineOffset items.
  /// <include file='Doc/ProjectLineOffsets.xml'
  ///  path='items/LJCLineOffsets/*'/>
  public class LJCLineOffsets : List<LJCLineOffset>
  {
    #region Properties

    // Gets or sets the current line number.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/LJCCurrentLineNumber/*'/>
    public long LJCCurrentLineNumber { get; set; }

    // Gets or sets the current line offset value.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/LJCCurrentOffset/*'/>
    public long LJCCurrentOffset { get; set; }

    // Gets or sets a reference to the file stream.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/LJCStream/*'/>
    public Stream? LJCStream { get; set; }
    #endregion

    #region Class Data

    // The list previous count.
    private int mPrevCount;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/Constructor/*'/>
    public LJCLineOffsets()
    {
      LJCStream = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/ConstructorParam/*'/>
    public LJCLineOffsets(Stream stream) : this()
    {
      LJCStream = stream;
      LJCCurrentLineNumber = 0;
      LJCCurrentOffset = 3;
      LJCSetNextLineOffset(null);
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/Add/*'/>
    public LJCLineOffset Add(long number, long offset)
    {
      var retValue = new LJCLineOffset()
      {
        Number = number,
        Offset = offset
      };
      Add(retValue);
      return retValue;
    }
    #endregion

    #region Sort and Search Methods

    // Retrieve the collection element by number.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/LJCSearchByNumber/*'/>
    public LJCLineOffset? LJCSearchByNumber(long number)
    {
      LJCLineOffset? retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      var searchItem = new LJCLineOffset()
      {
        Number = number
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Other Methods

    // Returns the line offset for the specified line.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/LJCGetLineOffset/*'/>
    public long LJCGetLineOffset(long number)
    {
      long retValue = -1;

      LJCLineOffset? lineOffset = LJCSearchByNumber(number);
      if (lineOffset != null)
      {
        retValue = lineOffset.Offset;
      }
      return retValue;
    }

    // Gets the offset for the next line.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/LJCGetNextLineOffset/*'/>
    public long LJCGetNextLineOffset(string? currentLine)
    {
      long retValue = LJCCurrentOffset;

      if (currentLine != null)
      {
        retValue += currentLine.Length;
      }

      if (LJCStream != null)
      {
        long prevPosition = LJCStream.Position;
        LJCStream.Position = retValue;
        byte[] buffer = new byte[2];
        LJCStream.Read(buffer, 0, 2);
        for (int index = 0; index < 2; index++)
        {
          if (13 == buffer[index] || 10 == buffer[index])
          {
            retValue++;
          }
          else
          {
            break;
          }
        }
        LJCStream.Position = prevPosition;
      }
      return retValue;
    }

    // Sets the offset for the next line.
    /// <include file='Doc/LineOffsets.xml'
    ///  path='items/LJCSetNextLineOffset/*'/>
    public void LJCSetNextLineOffset(string? line)
    {
      LJCCurrentLineNumber++;
      LJCCurrentOffset = LJCGetNextLineOffset(line);
      Add(LJCCurrentLineNumber, LJCCurrentOffset);
    }
    #endregion
  }
}
