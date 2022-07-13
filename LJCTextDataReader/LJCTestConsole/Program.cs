// Copyright (c) Lester J Clark 2021,2022 - All Rights Reserved
using LJCNetCommon;
using LJCTextDataReaderLib;
using System;
using System.Data.SqlClient;
using System.IO;

namespace LJCTestConsole
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/ProjectTestConsole.xml'/>
  public class Program
  {
    // The program entry point function.
    /// <include path='items/Main2/*' file='../../LJCDocLib/Common/Program.xml'/>
    private static void Main()
    {
      //TestGetSchemaTable();
      TestReadZip4();

      Console.Write("Press any key to continue...");
      Console.ReadKey();
    }

    /// <summary>
    /// Tests doing a bulk copy of the text file records.
    /// </summary>
    public static void Load()
    {
      string fileName = "SampleData.txt";
      string connString = @"
				Data Source=DESKTOP-PDPBE34\SQL2016;
				Initial Catalog=AppManagerData;
				Integrated Security=True";

      TextDataReader dataReader = new TextDataReader();
      dataReader.LJCSetFile(fileName);
      try
      {
        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connString))
        {
          bulkCopy.DestinationTableName = "TestAddress";
          bulkCopy.ColumnMappings.Add("Name1", "Name");
          bulkCopy.ColumnMappings.Add("Address2", "Address");
          bulkCopy.ColumnMappings.Add("City3", "City");
          bulkCopy.ColumnMappings.Add("State4", "State");
          bulkCopy.ColumnMappings.Add("Zip5", "Zip");

          try
          {
            bulkCopy.WriteToServer(dataReader);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }
          finally
          {
            bulkCopy.Close();
            dataReader.Close();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }

    //// Creates the Test USPS CityStateAlias layout.
    //private static void CreateCityStateAliasLayout(string fileName)
    //{
    //  DbColumns dbColumns = new DbColumns
    //  {
  		//	// Name, Position, MaxLength.
				//{ "DetailCode", 1, 1 }, // A
				//{ "ZipCode", 2, 5 },
    //    { "AliasPreDirectional", 7, 2 },
    //    { "AliasStreetName", 9, 28 },
    //    { "AliasSuffix", 37, 4 },
    //    { "AliasPostDirectional", 41, 2 },
    //    { "PreDirectional", 43, 2 },
    //    { "StreetName", 45, 28 },
    //    { "Suffix", 73, 4 },
    //    { "PostDirectional", 77, 2 },
    //    { "AliasType", 79, 1 },
    //    { "AliasCentry", 80, 2 },
    //    { "AliasYear", 82, 2 },
    //    { "AliasMonth", 84, 2 },
    //    { "AliasDay", 86, 2 },
    //    { "AliasAddressLowNumber", 88, 10 },
    //    { "AliasAddressHighNumber", 98, 10 },
    //    { "AliasAddressOddEven", 108, 1 },
    //  };
    //  dbColumns.LJCSerialize(fileName);
    //}

    //// Creates the Test USPS CityState layout.
    //private static void CreateCityStateLayout(string fileName)
    //{
    //  DbColumns dbColumns = new DbColumns
    //  {
  		//	// Name, Position, MaxLength.
				//{ "DetailCode", 1, 1 }, // D
				//{ "ZipCode", 2, 5 },
    //    { "CityStateKey", 7, 6 },
    //    { "ZipClassCode", 13, 1 },
    //    { "CityName", 14, 28 },
    //    { "CityAbbreviation", 42, 13 },
    //    { "FacilityCode", 55, 1 },
    //    { "MailingIndicator", 56, 1 },
    //    { "PreferredCityStateKey", 57, 6 },
    //    { "PreferredCityName", 63, 28 },
    //    { "DeliveryIndicator", 91, 1 },
    //    { "CarrierRouteSort", 92, 1 },
    //    { "UniqueZipNameIndicator", 93, 1 },
    //    { "StateCode", 100, 2 },
    //    { "CountyNumber", 102, 3 },
    //    { "CountyName", 105, 25 },
    //  };
    //  dbColumns.LJCSerialize(fileName);
    //}

    //// Creates the Test USPS ZipCode layout.
    //private static void CreateZipCodeLayout(string fileName)
    //{
    //  DbColumns dbColumns = new DbColumns
    //  {
  		//	// Name, Position, MaxLength.
				//{ "DetailCode", 1, 1 }, // D
				//{ "ZipCode", 2, 5 },
    //    { "UpdateKey", 7, 10 },
    //    { "ActionCode", 17, 1 },
    //    { "RecordTypeCode", 18, 1 },
    //    { "PreDirectional", 19, 2 },
    //    { "StreetName", 25, 28 },
    //    { "Suffix", 49, 4 },
    //    { "PostDirectional", 53, 2 },
    //    { "AddressLowNumber", 55, 10 },
    //    { "AddressHighNumber", 65, 10 },
    //    { "AddressOddEven", 75, 1 },
    //    { "FinanceNumber", 76, 6 },
    //    { "StateCode", 82, 2 },
    //    { "UrbanizationKey", 84, 6 },
    //    { "LastLineKey", 90, 6 }
    //  };
    //  dbColumns.LJCSerialize(fileName);
    //}

    // Creates the Test USPS Zip4 layout.
    private static void CreateZip4Layout(string fileName)
    {
      DbColumns dbColumns = new DbColumns
      {
  			// Name, Position, MaxLength.
				{ "DetailCode", 1, 1 }, // D
				{ "ZipCode", 2, 5 },
        { "UpdateKey", 7, 10 },
        { "ActionCode", 17, 1 },
        { "RecordTypeCode", 18, 1 },
        { "CarrierRouteID", 19, 4 },
        { "PreDirectional", 23, 2 },
        { "StreetName", 25, 28 },
        { "Suffix", 53, 4 },
        { "PostDirectional", 57, 2 },
        { "AddressLowNumber", 59, 10 },
        { "AddressHighNumber", 69, 10 },
        { "AddressOddEven", 79, 1 },
        { "Addressee", 80, 40 },
        { "UnitType", 120, 4 },
        { "UnitLowNumber", 124, 4 },
        { "UnitHighNumber", 132, 4 },
        { "UnitOddEven", 140, 1 },
        { "Zip4LowNumber", 141, 4 },
        { "Zip4HighNumber", 145, 4 },
        { "BaseCode", 149, 1 },
        { "LACSStatus", 150, 1 },
        { "GovernmentBuildingCode", 151, 1 },
        { "FinanceNumber", 152, 6 },
        { "StateCode", 158, 2 },
        { "CountryNumber", 160, 3 },
        { "District", 163, 2 },
        { "MunicipalityKey", 165, 6 },
        { "UrbanizationKey", 171, 6 },
        { "LastLineKey", 177, 6 }
      };
      dbColumns.LJCSerialize(fileName);
    }

    ///// <summary>
    ///// Tests getting the field values by field name.
    ///// </summary>
    ///// <param name="reader">The reader object.</param>
    //private static void GetByFieldName(TextDataReader reader)
    //{
    //  while (reader.Read())
    //  {
    //    bool first = true;
    //    string[] fieldNames = reader.LJCGetFieldNames();
    //    foreach (string fieldName in fieldNames)
    //    {
    //      string value = reader.GetString(reader.GetOrdinal(fieldName));
    //      if (false == first)
    //      {
    //        Console.Write("-");
    //      }
    //      first = false;
    //      Console.Write(value);
    //    }
    //    Console.WriteLine();
    //  }
    //}

    //// Tests TextDataReader.GetSchemaTable().
    //private static void TestGetSchemaTable()
    //{
    //  // Setup TextDataReader.
    //  bool hasheading = false;
    //  short skipLines = 1;
    //  bool fixedLengthFields = true;
    //  TextDataReader reader = new TextDataReader(hasheading, skipLines
    //    , fixedLengthFields);
    //  reader.LJCSetFields("Zip4Layout.xml");
    //  reader.GetSchemaTable();
    //}

    // Tests TextDataReader.Read().
    private static void TestReadZip4()
    {
      // Create layout files.
      string fileName = "Zip4Layout.xml";
      if (false == File.Exists(fileName))
      {
        CreateZip4Layout(fileName);
      }

      // Setup TextDataReader.
      bool hasheading = false;
      short skipLines = 1;
      bool fixedLengthFields = true;
      TextDataReader reader = new TextDataReader(hasheading, skipLines
        , fixedLengthFields);
      reader.LJCSetFields("Zip4Layout.xml");
      reader.LJCSetFile("Zip4.txt");

      // Set constraints.
      int startIndex = 0;
      int stopIndex = reader.FieldCount;

      // Write heading.
      bool first = true;
      for (int nameIndex = startIndex; nameIndex < stopIndex; nameIndex++)
      {
        DbColumn dbColumn = reader.LJCDataFields[nameIndex];
        if (false == first)
        {
          Console.Write(", ");
        }
        first = false;
        Console.Write($"{dbColumn.ColumnName}-{dbColumn.MaxLength}");
      }
      Console.WriteLine();

      while (reader.Read())
      {
        string data = null;
        int prevLength = 0;
        for (int index = startIndex; index < stopIndex; index++)
        {
          // Write trailing spaces.
          if (index > 0)
          {
            int dataLength;
            if (null == data)
            {
              dataLength = 0;
            }
            else
            {
              dataLength = data.Length;
            }
            for (int spaceIndex = dataLength; spaceIndex < prevLength
              ; spaceIndex++)
            {
              Console.Write(" ");
            }
          }

          data = reader.GetString(index);
          Console.Write(data);

          //DbColumn dbColumn = reader.LJCDataFields[index];
          prevLength = reader.LJCDataFields[index].MaxLength;
        }
        Console.WriteLine();
      }
    }
  }
}
