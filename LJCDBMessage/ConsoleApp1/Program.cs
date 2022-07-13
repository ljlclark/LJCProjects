// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherLib;
using LJCDBMessage;
using LJCNetCommon;

namespace ConsoleApp1
{
  class Program
  {
    static void Main()
    {
      var names = new ExampleIList()
      {
        "One",
        "Two",
        "Three",
        "Four"
      };
      string text = "";
      foreach (string name in names)
      {
        text += name + "\r\n";
      }
      Console.WriteLine(text);

      var dbResult = new DbResult()
      {
        Columns = new DbColumns()
        {
          { "ColumnName" }
        },
        DatabaseName = "DatabaseName",
        ProcedureName = "ProcedureName",
        RequestTypeName = "Select",
        SchemaName = "SchemaName",
        TableName = "TableName"
      };

      var dbValues = new DbValues()
      {
        { "Value1", 1 },
        { "Value2", "2" }
      };
      dbResult.Rows.Add(dbValues);

      // *** Begin *** Create Transport Cipher.
      //string plainText = "Here it is!";

      // Create the Cipher InsertItems.
      //CipherItems cipherItems = new CipherItems();
      //var insertItems = cipherItems.CreateItems();
      //byte[] cipher = cipherItems.CreateCipher(plainText);

      // Create the transportCipher with the plainText and InsertItems.
      //SendCipher sendCipher = new SendCipher(insertItems);
      //byte[] transportCipher = sendCipher.GetSendCipher(cipher);
      // *** End   *** Create Transport Cipher.

      // *** Begin *** recieved Transport Cipher.
      // Target side must not depend on a previously created item.
      //cipherItems = new CipherItems();
      //var receivedInsertItems = cipherItems.CreateReceivedItems(transportCipher);

      // Target side must not depend on a previously created item.
      //sendCipher = new SendCipher(receivedInsertItems);
      //byte[] receivedCipher = sendCipher.SendCipherToCipher(transportCipher);

      //string retrievedPlainText = cipherItems.CreatePlainText(receivedCipher);
      // *** End   *** recieved Transport Cipher.

      // The copied object does not change the original object.
      var dbResult1 = dbResult.Clone();
      dbResult1.DatabaseName = "DatabaseNameChanged";
      //dbResult1.Columns[0].ColumnName = "ColumnNameChanged";

      // The copied object does not change the original object.
      var dbResult2 = new DbResult(dbResult)
      {
        DatabaseName = "DatabaseNameChanged"
      };
      dbResult2.Columns[0].ColumnName = "ColumnNameChanged";

      // The copied object does not change the original object.
      DbValues rowValues = dbResult2.Rows[0].Values;
      if (DbValues.HasItems(rowValues))
      {
        foreach (DbValue dbValueItem in rowValues)
        {
          dbValueItem.PropertyName += "Changed";
        }
      }

      //var commonDataTypes = new CommonDataTypes();
      //var newCommonDataTypes = CommonDataTypes.Deserialize();

      //string resultText = dbResult.Serialize();
      //DbResult newResult = DbResult.DeserializeMessage(resultText);

      dbResult.Columns.LJCSerialize();
      //DbColumns newColumns = DbColumns.LJCDeserialize();

      dbResult.Rows.Serialize();
      //DbRows newRows = DbRows.LJCDeserialize();

      dbResult.Rows[0].Values.LJCSerialize();
      //DbValues newValues = DbValues.LJCDeserialize();

      Console.ReadKey();
    }
  }
}
