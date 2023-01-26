// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LineOffsets.cs
using System.Collections.Generic;
using System.IO;

namespace LJCTextDataReaderLib
{
  /// <summary>
  /// Represents a collection of LineOffset items.
  /// </summary>
  public class LineOffsets : List<LineOffset>
  {
    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="stream">The file stream object.</param>
    public LineOffsets(Stream stream)
    {
      LJCStream = stream;
      LJCCurrentLineNumber = 0;
      LJCCurrentOffset = 3;
      LJCSetNextLineOffset(null);
    }

    #region Collection Methods

    /// <summary>
    /// Creates and adds the object from the provided values.
    /// </summary>
    /// <param name="number">The line number.</param>
    /// <param name="offset">The line offset value.</param>
    /// <returns>A reference to the added item.</returns>
    public LineOffset Add(long number, long offset)
    {
      LineOffset retValue = new LineOffset()
      {
        Number = number,
        Offset = offset
      };
      Add(retValue);
      return retValue;
    }
    #endregion

    #region Sort and Search Methods

    /// <summary>
    /// Retrieve the collection element by number.
    /// </summary>
    /// <param name="number">The item line number.</param>
    public LineOffset LJCSearchByNumber(long number)
    {
      LineOffset retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      LineOffset searchItem = new LineOffset()
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

    /// <summary>
    /// Returns the line offset for the specified line.
    /// </summary>
    /// <param name="number">The line number.</param>
    /// <returns>The line offset value.</returns>
    public long LJCGetLineOffset(long number)
    {
      long retValue = -1;

      LineOffset lineOffset = LJCSearchByNumber(number);
      if (lineOffset != null)
      {
        retValue = lineOffset.Offset;
      }
      return retValue;
    }

    /// <summary>
    /// Gets the offset for the next line.
    /// </summary>
    /// <param name="line">The current line.</param>
    /// <returns>The line offset value.</returns>
    public long LJCGetNextLineOffset(string line)
    {
      byte[] buffer = new byte[2];
      long retValue = LJCCurrentOffset;

      if (line != null)
      {
        retValue += line.Length;
      }

      long prevPosition = LJCStream.Position;
      LJCStream.Position = retValue;
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
      return retValue;
    }

    /// <summary>
    /// Sets the offset for the next line.
    /// </summary>
    /// <param name="line">The current line.</param>
    public void LJCSetNextLineOffset(string line)
    {
      LJCCurrentLineNumber++;
      LJCCurrentOffset = LJCGetNextLineOffset(line);
      Add(LJCCurrentLineNumber, LJCCurrentOffset);
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the current line number.</summary>
    public long LJCCurrentLineNumber { get; set; }

    /// <summary>Gets or sets the current line offset value.</summary>
    public long LJCCurrentOffset { get; set; }

    /// <summary>Gets or sets a reference to the file stream.</summary>
    public Stream LJCStream { get; set; }

    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
