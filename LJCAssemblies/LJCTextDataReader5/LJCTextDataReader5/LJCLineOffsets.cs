// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LineOffsets.cs

namespace LJCTextDataReader5
{
  /// <summary>
  /// Represents a collection of LineOffset items.
  /// </summary>
  public class LJCLineOffsets : List<LJCLineOffset>
  {
    public LJCLineOffsets()
    {
      LJCStream = null;
    }

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="stream">The file stream object.</param>
    public LJCLineOffsets(Stream stream)
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

    /// <summary>
    /// Retrieve the collection element by number.
    /// </summary>
    /// <param name="number">The item line number.</param>
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

    /// <summary>
    /// Returns the line offset for the specified line.
    /// </summary>
    /// <param name="number">The line number.</param>
    /// <returns>The line offset value.</returns>
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

    /// <summary>
    /// Gets the offset for the next line.
    /// </summary>
    /// <param name="currentLine">The current line.</param>
    /// <returns>The line offset value.</returns>
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

    /// <summary>
    /// Sets the offset for the next line.
    /// </summary>
    /// <param name="line">The current line.</param>
    public void LJCSetNextLineOffset(string? line)
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
    public Stream? LJCStream { get; set; }

    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
