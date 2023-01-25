// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCryptography.cs
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace LJCNetCommon
{
  /// <summary>Provides methods to encrypt and decrypt data in memory.</summary>
  public class LJCCryptography
  {
    #region Constructors

    // Initializes a new instance of the MemoryCrypto class.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public LJCCryptography()
    {
      Type = Cryptography_Type.Crypto_Rijndael;
      return;
    }
    #endregion

    #region Methods

    // Returns a decrypted string from a cipher byte array. (E)
    /// <include path='items/Decrypt/*' file='Doc/LJCCryptography.xml'/>
    public string Decrypt(byte[] cipher, byte[] key
      , byte[] initializationVector)
    {
      byte[] workBytes;
      string retVal;

      workBytes = DecryptBytes(cipher, key, initializationVector);
      retVal = Encoding.ASCII.GetString(workBytes);
      return retVal;
    }

    // Returns a decrypted byte array from a cipher byte array.
    /// <include path='items/DecryptBytes/*' file='Doc/LJCCryptography.xml'/>
    public byte[] DecryptBytes(byte[] cipher, byte[] key
      , byte[] initializationVector)
    {
      CryptoStream cryptoStream;
      Stream srcStream;
      Stream destStream;
      int bufferLength;
      byte[] buffer;
      byte[] retVal;

      mSal.Key = key;
      mSal.IV = initializationVector;

      // Stack destination stream and crypto stream.
      destStream = new MemoryStream();
      cryptoStream = new CryptoStream(destStream, mSal.CreateDecryptor()
        , CryptoStreamMode.Write);

      // Create source stream.
      srcStream = NetCommon.BytesToMemStream(cipher);

      // Read from source stream and write to crypto stream.
      buffer = new byte[1024];
      do
      {
        bufferLength = srcStream.Read(buffer, 0, 1024);
        cryptoStream.Write(buffer, 0, bufferLength);
      } while (bufferLength > 0);

      // Get text from destination stream.
      cryptoStream.FlushFinalBlock();
      retVal = NetCommon.MemStreamToBytes(destStream);

      // Clean-up.
      destStream.Close();
      srcStream.Close();
      cryptoStream.Clear();
      cryptoStream.Close();
      return retVal;
    }

    // Returns an encrypted byte array from a byte array.
    /// <include path='items/Encrypt1/*' file='Doc/LJCCryptography.xml'/>
    public byte[] Encrypt(byte[] bytes, byte[] key
      , byte[] initializationVector)
    {
      CryptoStream cryptoStream;
      Stream srcStream;
      Stream destStream;
      BinaryWriter destWriter;
      int bufferLength;
      byte[] buffer;
      byte[] retVal;

      mSal.Key = key;
      mSal.IV = initializationVector;

      // Stack source stream and crypto stream.
      srcStream = NetCommon.BytesToMemStream(bytes);
      cryptoStream = new CryptoStream(srcStream, mSal.CreateEncryptor()
        , CryptoStreamMode.Read);

      // Create destination stream and stream writer.
      destStream = new MemoryStream();
      destWriter = new BinaryWriter(destStream);

      // Read from crypto stream and write to destination stream.
      buffer = new byte[1024];
      do
      {
        bufferLength = cryptoStream.Read(buffer, 0, 1024);
        destWriter.Write(buffer, 0, bufferLength);
      } while (bufferLength > 0);

      // Get cipher from destination stream.
      destWriter.Flush();
      retVal = NetCommon.MemStreamToBytes(destStream);

      // Clean-up.
      destWriter.Close();
      destStream.Close();
      srcStream.Close();
      cryptoStream.Clear();
      cryptoStream.Close();
      return retVal;
    }

    // Returns an encrypted byte array from a string value. (E)
    /// <include path='items/Encrypt2/*' file='Doc/LJCCryptography.xml'/>
    public byte[] Encrypt(string plainText, byte[] key
      , byte[] initializationVector)
    {
      byte[] workBytes;

      workBytes = Encoding.ASCII.GetBytes(plainText);
      return Encrypt(workBytes, key, initializationVector);
    }

    // Generates a random initialization vector based on the encryption type.
    /// <include path='items/GenerateIV/*' file='Doc/LJCCryptography.xml'/>
    public byte[] GenerateIV()
    {
      mSal.GenerateIV();
      return mSal.IV;
    }

    // Generates a random encryption key based on the encryption type.
    /// <include path='items/GenerateKey/*' file='Doc/LJCCryptography.xml'/>
    public byte[] GenerateKey()
    {
      mSal.GenerateKey();
      return mSal.Key;
    }

    // Returns true if the key size is valid otherwise returns false.
    /// <include path='items/IsValidKeySize/*' file='Doc/LJCCryptography.xml'/>
    public bool IsValidKeySize(int sizeValue)
    {
      KeySizes[] keySizesArray;
      int keySize;
      bool retVal = false;

      keySizesArray = LegalKeySizes;
      foreach (KeySizes keySizes in keySizesArray)
      {
        keySize = keySizes.MinSize;
        do
        {
          if (keySize == sizeValue)
          {
            retVal = true;
            break;
          }
          keySize += keySizes.SkipSize;
        } while (keySize <= keySizes.MaxSize);
      }
      return retVal;
    }

    // Returns the crypto type text using the CryptoType enumeration.
    private string GetCryptoTypeText(Cryptography_Type cryptoType)
    {
      string retVal = null;

      switch (cryptoType)
      {
        case Cryptography_Type.Crypto_DES:
          retVal = "DES";
          break;

        case Cryptography_Type.Crypto_3DES:
          retVal = "3DES";
          break;

        case Cryptography_Type.Crypto_RC2:
          retVal = "RC2";
          break;

        case Cryptography_Type.Crypto_Rijndael:
          retVal = "Rijndael";
          break;
      }
      return retVal;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the secret key size in bits.</summary>
    public int KeySize
    {
      get { return mSal.KeySize; }
      set
      {
        if (IsValidKeySize(value))
        {
          mSal.KeySize = value;
        }
        return;
      }
    }

    // Gets the supported key sizes.
    /// <include path='items/LegalKeySizes/*' file='Doc/LJCCryptography.xml'/>
    public KeySizes[] LegalKeySizes
    {
      get { return mSal.LegalKeySizes; }
    }

    /// <summary>Gets or sets the encryption type.</summary>
    public Cryptography_Type Type
    {
      get { return mType; }
      set
      {
        mType = value;
        mCryptoTypeText = GetCryptoTypeText(mType);
        mSal = SymmetricAlgorithm.Create(mCryptoTypeText);
        return;
      }
    }
    #endregion

    #region Class Data

    // Property values.
    string mCryptoTypeText;
    SymmetricAlgorithm mSal;
    Cryptography_Type mType;
    #endregion
  }

  /// <summary>The encryption types.</summary>
  public enum Cryptography_Type
  {
    /// <summary>Use DES encryption.</summary>
    Crypto_DES,
    /// <summary>Use triple DES encryption.</summary>
    Crypto_3DES,
    /// <summary>Use RC2 encryption.</summary>
    Crypto_RC2,
    /// <summary>Use Rijndael (AES) encryption.</summary>
    Crypto_Rijndael
  }
}
