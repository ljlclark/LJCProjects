using System;

namespace CipherLib
{
	/// <summary>
	/// 
	/// </summary>
	public class TargetCipher
	{
		// Get bytes from sourceCipher index values knowing only
		// the index locations.
		/// <include path='items/CreateNewItems/*' file='Doc/TargetCipher.xml'/>
		public InsertItems CreateNewItems(byte[] sourceCipher)
		{
			int previousLastIndex = 0;
			int source3Index = 0;
			int source4Index = 0;
			int sourceIndex = 0;
			int sourceLength = 0;
			int vector;
			InsertItems retValue = new InsertItems();

			byte[] iv = new byte[16];
			byte[] key = new byte[32];

			sourceIndex = previousLastIndex + 2;
			sourceLength = 4;
			vector = 512;
			source3Index = GetItemIndex(sourceCipher, sourceIndex, sourceLength, vector);
			Array.Copy(sourceCipher, source3Index, key, 0, 32);
			previousLastIndex += sourceLength;

			sourceIndex = previousLastIndex + 4;
			sourceLength = 4;
			vector = 1024;
			source4Index = GetItemIndex(sourceCipher, sourceIndex, sourceLength, vector);
			Array.Copy(sourceCipher, source4Index, iv, 0, 16);

			// Create Insert Items based on original cipher.
			int insertByteCount = 56;
			byte[] newBytes = new byte[4];
			retValue.Add("Item1", 2, newBytes);
			retValue.Add("Item2", 4, newBytes);
			if (source3Index > sourceCipher.Length - insertByteCount)
			{
				source3Index = sourceCipher.Length - insertByteCount;
			}
			if (source4Index > sourceCipher.Length - insertByteCount)
			{
				source4Index = sourceCipher.Length - insertByteCount;
			}
			retValue.Add("Item3", source3Index, key);
			retValue.Add("Item4", source4Index, iv);
			return retValue;
		}

		// Calculates the Item Index.
		private static int GetItemIndex(byte[] source, int sourceIndex
			, int sourceLength, int vector)
		{
			int retValue = -1;

			byte[] keyBytes = new byte[sourceLength];
			Array.Copy(source, sourceIndex, keyBytes, 0, sourceLength);
			int index = BitConverter.ToInt32(keyBytes, 0);
			retValue = int.MaxValue - vector - index;
			return retValue;
		}
	}
}
