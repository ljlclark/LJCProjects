// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// DbService.cs
using CipherLib;
using LJCDBDataAccessLib;
using LJCNetCommon;
using System;
using LJCDBMessage;

namespace LJCDBServiceLib
{
  // The Service Type for performing database operations using request
  // XML messages.
  /// <include path='items/DbService/*' file='Doc/ProjectDBServiceLib.xml'/>
  public class DbService : IDbService
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbServiceC/*' file='Doc/DbService.xml'/>
    public DbService(bool useEncryption = true)
    {
      UseEncryption = useEncryption;
      if (UseEncryption)
      {
        mRequestCipherItems = new CipherItems();
        mRequestInsertItems = mRequestCipherItems.CreateItems();
        mRequestSendCipher = new SendCipher(mRequestInsertItems);

        mResponseCipherItems = new CipherItems();
        mResponseSendCipher = new SendCipher();
      }
    }
    #endregion

    #region Methods

    // Executes the specified request XML message.
    /// <include path='items/Execute/*' file='Doc/DbService.xml'/>
    public string Execute(string request)
    {
      string requestText;
      string retValue;

      if (UseEncryption)
      {
        byte[] requestSendCipher = Convert.FromBase64String(request);
        requestText = GetIncommingText(requestSendCipher);
      }
      else
      {
        requestText = NetCommon.Base64ToText(request);
      }
      DbRequest dbRequest = DbRequest.Deserialize(requestText);

      DbDataAccess dbDataAccess = new DbDataAccess(dbRequest.DataConfigName);
      DbResult dbResult = dbDataAccess.Execute(dbRequest);

      retValue = dbResult.Serialize();
      if (UseEncryption)
      {
        byte[] responseCipher = GetOutgoingCipher(retValue);
        var tempResponse = Convert.ToBase64String(responseCipher);

        // Test decrypt.
        //byte[] responseSendCipher = Convert.FromBase64String(tempResponse);
        //var resultText = GetIncommingText(responseSendCipher);

        retValue = tempResponse;
      }
      else
      {
        retValue = NetCommon.TextToBase64(retValue);
      }

      return retValue;
    }
    #endregion

    #region Private Methods

    // Decrypt Response Cipher
    private string GetIncommingText(byte[] sendCipher)
    {
      var responseInsertItems = mResponseCipherItems.CreateReceivedItems(sendCipher);
      mResponseSendCipher.SetInsertItems(responseInsertItems);
      byte[] responseCipher = mResponseSendCipher.SendCipherToCipher(sendCipher);
      var retValue = mResponseCipherItems.CreatePlainText(responseCipher);
      return retValue;
    }

    // Encrypt Request Cipher
    private byte[] GetOutgoingCipher(string plainText)
    {
      byte[] cipher = mRequestCipherItems.CreateCipher(plainText);
      byte[] retValue = mRequestSendCipher.GetSendCipher(cipher);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the UseEncyption flag.</summary>
    public bool UseEncryption { get; set; }
    #endregion

    #region Class Data

    private readonly CipherItems mResponseCipherItems;
    private readonly SendCipher mResponseSendCipher;
    private readonly CipherItems mRequestCipherItems;
    private readonly InsertItems mRequestInsertItems;
    private readonly SendCipher mRequestSendCipher;
    #endregion
  }
}
