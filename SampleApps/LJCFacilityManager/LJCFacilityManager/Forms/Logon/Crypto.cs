// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Crypto.cs
using System;
using System.Text;
using LJCNetCommon;

namespace LJCFacilityManager
{
	/// <summary>
	/// X
	/// </summary>
	public class Crypto
	{
		#region Methods

		/// <summary>
		/// X
		/// </summary>
		/// <param name="password">X</param>
		public static bool GetIsAdministrator(string password)
		{
			Crypto crypto = new Crypto();

			crypto.Decrypt(password);
			return crypto.IsAdministrator;
		}

		/// <summary>
		/// X
		/// </summary>
		/// <returns>X</returns>
		public string Encrypt()
		{
			StringBuilder builder;
			LJCCryptography cryptography;
			byte[] cipher;
			string data;
			string retValue;

			// Create data value.
			builder = new StringBuilder(32);
			builder.Append($"{mPassword}:{mPersonID}:{mIsAdministrator}");
			data = builder.ToString();

			cryptography = new LJCCryptography();
			cipher = cryptography.Encrypt(data, mKey, mIv);
			retValue = NetCommon.TextBytesToBase64(cipher);
			return retValue;
		}

		/// <summary>
		/// X
		/// </summary>
		/// <param name="value">X</param>
		/// <returns>X</returns>
		public bool Decrypt(string value)
		{
			LJCCryptography cryptography;
			byte[] cipher;
			char[] delimiters = { ':' };
			string[] data;
			string text;
			bool retValue = false;

			if (value != null)
			{
				cryptography = new LJCCryptography();
				cipher = Convert.FromBase64String(value);
				text = cryptography.Decrypt(cipher, mKey, mIv);

				// Parse Class Data.
				data = text.Split(delimiters);
				if (data.Length == 3)
				{
					retValue = true;
					mPassword = data[0];
					int.TryParse(data[1], out mPersonID);
					bool.TryParse(data[2], out mIsAdministrator);
				}
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>
		/// X
		/// </summary>
		public string Password
		{
			get
			{
				return mPassword;
			}
			set
			{
				mPassword = NetString.InitString(value);
			}
		}

		/// <summary>
		/// X
		/// </summary>
		public int PersonID
		{
			get
			{
				return mPersonID;
			}
			set
			{
				mPersonID = value;
			}
		}

		/// <summary>
		/// X
		/// </summary>
		public bool IsAdministrator
		{
			get
			{
				return mIsAdministrator;
			}
			set
			{
				mIsAdministrator = value;
			}
		}
		#endregion

		#region Member Data

		// Property values.
		private string mPassword;
		private int mPersonID;
		private bool mIsAdministrator;

		// Class Data.
		private readonly byte[] mKey = {230, 191, 150, 1, 207, 136, 112, 200
		                       , 7, 192, 109, 130, 188, 192, 27, 183
        		               , 238, 1, 248, 75, 35, 47, 1, 144
		                       , 90, 91, 5, 167, 87, 170, 11, 141};
		private readonly byte[] mIv = {94, 9, 42, 5, 219, 86, 120, 94
		                      , 209, 208, 217, 111, 24, 4, 255, 67};
		#endregion
	}
}
