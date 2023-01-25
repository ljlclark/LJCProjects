// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SendCipher.cs
using System;

namespace CipherLib
{
  /// <summary>Provides methods to deal with a SendCipher.</summary>
  public class SendCipher
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public SendCipher()
    {
    }

    // Initializes an object instance.
    /// <include path='items/SendCipherC/*' file='Doc/SendCipher.xml'/>
    public SendCipher(InsertItems insertItems)
    {
      InsertItems = new InsertItems(insertItems);
    }
    #endregion

    #region Public Methods

    // Returns the insert value from the sendCipher by name.
    /// <include path='items/GetInsertItemValue/*' file='Doc/SendCipher.xml'/>
    public byte[] GetInsertItemValue(byte[] sourceCipher, string name)
    {
      byte[] retValue = null;

      int offset = 0;
      InsertItems.LJCSortIndex();
      foreach (InsertItem insertItem in InsertItems)
      {
        if (0 == insertItem.Name.CompareTo(name))
        {
          retValue = new byte[insertItem.InsertValue.Length];
          Array.Copy(sourceCipher, insertItem.InsertIndex + offset, retValue, 0
            , insertItem.InsertValue.Length);
          break;
        }
        offset += insertItem.InsertValue.Length;
      }
      return retValue;
    }

    // Returns the SendCipher value.
    /// <include path='items/GetSendCipher/*' file='Doc/SendCipher.xml'/>
    public byte[] GetSendCipher(byte[] sourceCipher)
    {
      int valuesLength = GetValuesLength();
      byte[] retValue = new byte[sourceCipher.Length + valuesLength];

      mCurrentSourceIndex = 0;
      mPreviousSourceIndex = 0;
      mCurrentTargetIndex = 0;
      mPreviousTargetIndex = 0;

      InsertItems.LJCSortIndex();
      foreach (InsertItem insertItem in InsertItems)
      {
        CopySourceData(insertItem, sourceCipher, retValue);
        mPreviousTargetIndex = mCurrentTargetIndex;
        CopyInsertValue(insertItem, retValue);

        // Create value indexes.
        if (0 == insertItem.Name.CompareTo("Key"))
        {
          int number = int.MaxValue - mPreviousTargetIndex - 512;
          byte[] value = BitConverter.GetBytes(number);
          Array.Copy(value, 0, retValue, 2, 4);
        }
        if (0 == insertItem.Name.CompareTo("IV"))
        {
          int number = int.MaxValue - mPreviousTargetIndex - 1024;
          byte[] value = BitConverter.GetBytes(number);
          Array.Copy(value, 0, retValue, 4 + 4, 4);
        }
      }

      // Ending data.
      mCurrentSourceIndex += 1;
      if (mCurrentSourceIndex < sourceCipher.Length)
      {
        int sourceLength = sourceCipher.Length - mCurrentSourceIndex + 1;
        Array.Copy(sourceCipher, mPreviousSourceIndex, retValue, mCurrentTargetIndex
          , sourceLength);
      }

      // Update InsertItems added at the End.
      //for (int index = 0; index < InsertItems.Count; index++)
      //{
      //	var insertItem = InsertItems[index];
      //	if (insertItem != null
      //		&& insertItem.InsertIndex >= mEndStartNumber)
      //	{
      //		insertItem.InsertIndex = sourceCipher.Length;
      //	}
      //}
      return retValue;
    }

    // Returns the Cipher value.
    /// <include path='items/SendCipherToCipher/*' file='Doc/SendCipher.xml'/>
    public byte[] SendCipherToCipher(byte[] sourceCipher)
    {
      int valuesLength = GetValuesLength();
      byte[] retValue = new byte[sourceCipher.Length - valuesLength];

      int previousLastIndex = 0;
      int previousInsertIndex = 0;
      int sourceIndex;
      int sourceLength = 0;
      int targetIndex = 0;

      InsertItems.LJCSortIndex();
      foreach (InsertItem insertItem in InsertItems)
      {
        sourceIndex = previousLastIndex;
        sourceLength = insertItem.InsertIndex - previousInsertIndex;
        if (0 == sourceLength)
        {
          break;
        }
        Array.Copy(sourceCipher, sourceIndex, retValue, targetIndex, sourceLength);

        previousInsertIndex = insertItem.InsertIndex;
        targetIndex += sourceLength;
        previousLastIndex += sourceLength + insertItem.InsertValue.Length;
      }

      // Ending data.
      if (sourceLength > 0)
      {
        sourceIndex = previousLastIndex;
        sourceLength = sourceCipher.Length - sourceIndex;
        if (sourceLength > 0)
        {
          Array.Copy(sourceCipher, sourceIndex, retValue, targetIndex, sourceLength);
        }
      }

      return retValue;
    }

    // Initializes the internal InsertItems property.
    /// <include path='items/SendInsertItems/*' file='Doc/SendCipher.xml'/>
    public void SetInsertItems(InsertItems insertItems)
    {
      InsertItems = new InsertItems(insertItems);
    }
    #endregion

    #region Private Methods

    // Copy the Insert Value to the Target bytes.
    private void CopyInsertValue(InsertItem insertItem, byte[] target)
    {
      if (insertItem.InsertValue != null && insertItem.InsertValue.Length > 0)
      {
        Array.Copy(insertItem.InsertValue, 0, target, mCurrentTargetIndex
          , insertItem.InsertValue.Length);
        mCurrentTargetIndex += insertItem.InsertValue.Length;
      }
    }

    // Copy the Source Data between Insert Indexes to the Target bytes.
    private void CopySourceData(InsertItem insertItem, byte[] source
      , byte[] target)
    {
      if (insertItem.InsertIndex >= mEndStartNumber)
      {
        // Copy all remaining source values.
        mCurrentSourceIndex = source.Length;
      }
      else
      {
        mCurrentSourceIndex = insertItem.InsertIndex;
      }
      int sourceLength = mCurrentSourceIndex - mPreviousSourceIndex;
      if (sourceLength > 0)
      {
        Array.Copy(source, mPreviousSourceIndex, target, mCurrentTargetIndex
          , sourceLength);
        mCurrentTargetIndex += sourceLength;
      }
      mPreviousSourceIndex = mCurrentSourceIndex;
    }

    // Returns the total Insert Items InsertValue bytes length.
    private int GetValuesLength()
    {
      int retValue = 0;
      foreach (InsertItem insertItem in InsertItems)
      {
        if (insertItem.InsertValue != null)
        {
          retValue += insertItem.InsertValue.Length;
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    // The collection of Insert Items.
    internal InsertItems InsertItems { get; private set; }
    #endregion

    #region Class Data

    private int mCurrentSourceIndex;
    private int mCurrentTargetIndex;
    private readonly int mEndStartNumber = short.MaxValue - 10;
    private int mPreviousSourceIndex;
    private int mPreviousTargetIndex;
    #endregion
  }
}
