// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCCrypto.cs
using System;
using System.Text;
using LJCNetCommon;

namespace LJCDataAccessConfig
{
  // Data access encryption.
  /// <include path='items/LJCCrypto/*' file='Doc/LJCCrypto.xml'/>
  public class LJCCrypto
  {
    #region Methods

    // Retrieves the administrator flag.
    /// <include path='items/GetIsAdministrator/*' file='Doc/LJCCrypto.xml'/>
    public static bool GetIsAdministrator(string password)
    {
      LJCCrypto crypto = new LJCCrypto();

      crypto.Decrypt(password);
      return crypto.IsAdministrator;
    }

    // Encrypt the user information.
    /// <include path='items/Encrypt/*' file='Doc/LJCCrypto.xml'/>
    public string Encrypt()
    {
      StringBuilder builder;
      LJCCryptography ljcCryptography;
      byte[] cipher;
      string data;
      string retValue;

      // Create data value.
      builder = new StringBuilder(32);
      builder.Append("{mPassword}:{mPersonID}:{mIsAdministrator}");
      data = builder.ToString();

      ljcCryptography = new LJCCryptography();
      cipher = ljcCryptography.Encrypt(data, mKey, mIv);
      retValue = NetCommon.TextBytesToBase64(cipher);
      return retValue;
    }

    // Decrypts the user information.
    /// <include path='items/Decrypt/*' file='Doc/LJCCrypto.xml'/>
    public bool Decrypt(string value)
    {
      LJCCryptography ljcCryptography;
      byte[] cipher;
      char[] delimiters = { ':' };
      string[] data;
      string text;
      bool retVal = false;

      if (value != null)
      {
        ljcCryptography = new LJCCryptography();
        cipher = Convert.FromBase64String(value);
        text = ljcCryptography.Decrypt(cipher, mKey, mIv);

        // Parse data values.
        data = text.Split(delimiters);
        if (data.Length == 3)
        {
          retVal = true;
          mPassword = data[0];
          int.TryParse(data[1], out mPersonID);
          bool.TryParse(data[2], out mIsAdministrator);
        }
      }
      return retVal;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Password value.</summary>
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

    /// <summary>Gets or sets the PersonID value.</summary>
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

    /// <summary>Gets or sets the Administrator flag.</summary>
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
