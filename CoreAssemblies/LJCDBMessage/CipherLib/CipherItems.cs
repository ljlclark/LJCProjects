// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CipherItems.cs
using System;
using LJCNetCommon;

namespace CipherLib
{
  /// <summary>Provides methods for handling cipher items.</summary>
  public class CipherItems
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public CipherItems()
    {
      Cryptography = new LJCCryptography();
    }
    #endregion

    #region Public Methods

    // Creates the Cipher.
    /// <include path='items/CreateCipher/*' file='Doc/CipherItems.xml'/>
    public byte[] CreateCipher(string plainText)
    {
      byte[] retValue = Cryptography.Encrypt(plainText, Key, IV);
      return retValue;
    }

    // Creates the InsertItems.
    /// <include path='items/CreateItems/*' file='Doc/CipherItems.xml'/>
    public InsertItems CreateItems()
    {
      InsertItems retValue = new InsertItems();

      int endStartNumber = mEndStartNumber;
      InsertItem insertItem;

      byte[] value = BitConverter.GetBytes(0);
      retValue.Add("Int1", 2, value);
      retValue.Add("Int2", 4, value);

      Key = Cryptography.GenerateKey();
      insertItem = new InsertItem()
      {
        Name = "Key",
        InsertIndex = 30,
        InsertValue = Key
      };
      retValue.Add(insertItem);

      IV = Cryptography.GenerateIV();
      insertItem = new InsertItem()
      {
        Name = "IV",
        InsertIndex = endStartNumber,
        InsertValue = IV
      };
      retValue.Add(insertItem);
      //endStartNumber++;
      return retValue;
    }

    // Get bytes from sourceCipher index values knowing only
    // the index locations.
    /// <include path='items/CreateReceivedItems/*' file='Doc/CipherItems.xml'/>
    public InsertItems CreateReceivedItems(byte[] receivedCipher
      , bool setObjectItems = true)
    {
      int previousSourceLastIndex = 0;
      int source3Index;
      int source4Index;
      int sourceIndex;
      int sourceLength;
      int vector;
      InsertItems retValue = new InsertItems();

      byte[] iv = new byte[16];
      byte[] key = new byte[32];

      sourceIndex = previousSourceLastIndex + 2;
      sourceLength = 4;
      vector = 512;
      source3Index = GetItemIndex(receivedCipher, sourceIndex, sourceLength, vector);
      Array.Copy(receivedCipher, source3Index, key, 0, 32);
      previousSourceLastIndex += sourceLength;

      sourceIndex = previousSourceLastIndex + 4;
      sourceLength = 4;
      vector = 1024;
      source4Index = GetItemIndex(receivedCipher, sourceIndex, sourceLength, vector);
      Array.Copy(receivedCipher, source4Index, iv, 0, 16);
      previousSourceLastIndex += sourceLength;

      // Create Insert Items based on original cipher.
      int insertByteCount = 56;
      byte[] newBytes = new byte[4];
      retValue.Add("Item1", 2, newBytes);
      retValue.Add("Item2", 4, newBytes);
      if (source3Index > receivedCipher.Length - insertByteCount)
      {
        source3Index = receivedCipher.Length - insertByteCount;
      }
      else
      {
        source3Index -= previousSourceLastIndex;
      }
      if (source4Index > receivedCipher.Length - insertByteCount)
      {
        source4Index = receivedCipher.Length - insertByteCount;
      }
      else
      {
        source4Index -= previousSourceLastIndex;
      }
      retValue.Add("Item3", source3Index, key);
      retValue.Add("Item4", source4Index, iv);
      if (setObjectItems)
      {
        SetInsertValues(retValue);
      }
      return retValue;
    }

    // Returns the Plain Text value.
    /// <include path='items/CreatePlainText/*' file='Doc/CipherItems.xml'/>
    public string CreatePlainText(byte[] cipher)
    {
      string retValue;

      retValue = Cryptography.Decrypt(cipher, Key, IV);
      return retValue;
    }

    // Initializes the internal InsertItems property.
    /// <include path='items/SetInsertValues/*' file='Doc/CipherItems.xml'/>
    public void SetInsertValues(InsertItems insertItems)
    {
      // Uses the names created by the TargetCipher CreateNewItems() method.
      var insertItem = insertItems.LJCSearchName("Item3");
      if (insertItem != null)
      {
        Key = insertItem.InsertValue;
      }
      insertItem = insertItems.LJCSearchName("Item4");
      if (insertItem != null)
      {
        IV = insertItem.InsertValue;
      }
    }
    #endregion

    #region Private Methods

    // Adjusts InsertIndex values that are set beyond the end of the Cipher.
    //private void AdjustIndex(string plainText, InsertItems insertItems)
    //private void AdjustIndex(byte[] cipher, InsertItems insertItems)
    //{
    //	// Adjust qualifying items to end of array.
    //	int adjustIndexValue = mEndStartNumber;
    //	insertItems.LJCSortIndex();
    //	foreach (InsertItem insertItem in insertItems)
    //	{
    //		if (insertItem.InsertIndex >= mEndStartNumber)
    //		{
    //			insertItem.InsertIndex = adjustIndexValue;
    //			adjustIndexValue++;
    //		}
    //		else
    //		{
    //			//if (insertItem.InsertIndex > plainText.Length)
    //			if (insertItem.InsertIndex > cipher.Length)
    //			{
    //				insertItem.InsertIndex = adjustIndexValue;
    //				adjustIndexValue++;
    //			}
    //		}
    //	}
    //}

    // Calculates the Item Index.
    private static int GetItemIndex(byte[] source, int sourceIndex
      , int sourceLength, int vector)
    {
      int retValue;

      byte[] keyBytes = new byte[sourceLength];
      Array.Copy(source, sourceIndex, keyBytes, 0, sourceLength);
      int index = BitConverter.ToInt32(keyBytes, 0);
      retValue = int.MaxValue - vector - index;
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the Initialization Vector.
    private byte[] IV { get; set; }

    // Gets or setst the Key value.
    private byte[] Key { get; set; }

    // Gets or sets the Cryptography object.
    private LJCCryptography Cryptography { get; set; }
    #endregion

    #region Class Data

    private readonly int mEndStartNumber = short.MaxValue - 10;
    #endregion
  }
}
