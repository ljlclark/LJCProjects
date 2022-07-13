using System;
using LJCNetCommon;

namespace ConsoleApp1
{
	// 
	public class CipherItems
	{
		#region Constructors

		// 
		internal CipherItems()
		{
			Cryptography = new LJCCryptography();
		}
		#endregion

		#region Public Methods

		// Creates the Cipher.
		// Adjusts InsertIndex values that are set beyond the end of the Cipher.
		public byte[] CreateCipher(string plainText, InsertItems insertItems)
		{
			byte[] retValue = Cryptography.Encrypt(plainText, Key, IV);
			//AdjustIndex(plainText, insertItems);
			AdjustIndex(retValue, insertItems);
			return retValue;
		}

		// Creates the InsertItems.
		public InsertItems CreateItems()
		{
			InsertItems retValue = new InsertItems();

			int endStartNumber = mEndStartNumber;
			InsertItem insertItem = null;

			byte[] value = BitConverter.GetBytes(0);
			retValue.Add("Int1", 2, value);
			retValue.Add("Int2", 4, value);

			Key = Cryptography.GenerateKey();
			insertItem = new InsertItem()
			{
				Name = "Key",
				//InsertIndex = 2,
				//InsertIndex = endStartNumber,
				InsertIndex = 30,
				InsertValue = Key
			};
			retValue.Add(insertItem);
			//endStartValue++; // Add after each item that uses endStartValue.

			IV = Cryptography.GenerateIV();
			insertItem = new InsertItem()
			{
				Name = "IV",
				//InsertIndex = 4,
				InsertIndex = endStartNumber,
				InsertValue = IV
			};
			retValue.Add(insertItem);
			endStartNumber++; // Add after each item that uses endStartValue.
			return retValue;
		}

		// Returns the Plain Text value.
		public string CreatePlainText(byte[] cipher)
		{
			string retValue = null;

			retValue = Cryptography.Decrypt(cipher, Key, IV);
			return retValue;
		}
		#endregion

		#region Private Methods

		// Adjusts InsertIndex values that are set beyond the end of the Cipher.
		//private void AdjustIndex(string plainText, InsertItems insertItems)
		private void AdjustIndex(byte[] cipher, InsertItems insertItems)
		{
			// Adjust qualifying items to end of array.
			int adjustIndexValue = mEndStartNumber;
			insertItems.LJCSortIndex();
			foreach (InsertItem insertItem in insertItems)
			{
				if (insertItem.InsertIndex >= mEndStartNumber)
				{
					insertItem.InsertIndex = adjustIndexValue;
					adjustIndexValue++;
				}
				else
				{
					//if (insertItem.InsertIndex > plainText.Length)
					if (insertItem.InsertIndex > cipher.Length)
					{
						insertItem.InsertIndex = adjustIndexValue;
						adjustIndexValue++;
					}
				}
			}
		}
		#endregion

		#region Properties

		// 
		internal byte[] IV { get; private set; }

		// 
		internal byte[] Key { get; private set; }

		// 
		private LJCCryptography Cryptography { get; set; }
		#endregion

		#region Class Data

		private readonly int mEndStartNumber = short.MaxValue - 10;
		#endregion
	}
}
