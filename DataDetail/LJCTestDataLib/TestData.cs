// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using LJCDataDetailLib;
using LJCNetCommon;

namespace LJCTestDataLib
{
	/// <summary>Creates Test Data for DataDetail.</summary>
	public class TestData
	{
		/// <summary>Get the test record</summary>
		public static DbColumns GetRecord()
		{
			DbColumn dbColumn;
			DbColumns retValue = new DbColumns();

			// Set the data elements.
			int sequence = 0;
			dbColumn = new DbColumn("FirstName", "Lester")
			{
				Caption = "First Name",
				MaxLength = 40,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("MiddleName", "John")
			{
				Caption = "Middle Name",
				MaxLength = 30,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("LastName", "Clark")
			{
				Caption = "Last Name",
				MaxLength = 40,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("FourthValue", "Value4")
			{
				Caption = "Fourth Value",
				DataTypeName = "Boolean",
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("FifthValue", "2")
			{
				Caption = "Fifth Value",
				MaxLength = 20,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("SixthValue", "2")
			{
				Caption = "Sixth Value",
				MaxLength = 20,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("SeventhValue", "Value7")
			{
				Caption = "Seventh Value",
				MaxLength = 20,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("EighthValue", "Value8")
			{
				Caption = "Eighth Value",
				MaxLength = 20,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);

			dbColumn = new DbColumn("NinethValue", "Value9")
			{
				Caption = "Nineth Value",
				MaxLength = 20,
				Sequence = sequence++
			};
			retValue.Add(dbColumn);
			return retValue;
		}

		/// <summary>Get the test KeyItems.</summary>
		public static KeyItems GetKeyItems(string recordPropertyName)
		{
			KeyItems retValue;

			retValue = new KeyItems
			{
				{ recordPropertyName, 1, "First One" },
				{ recordPropertyName, 2, "Second One" },
				{ recordPropertyName, 3, "Third One" },
				{ recordPropertyName, 4, "Fourth One" }
			};
			return retValue;
		}

		/// <summary>Get the test KeyItem.</summary>
		public static KeyItem GetKeyItem()
		{
			KeyItem retValue = new KeyItem()
			{
				Description = "Fifth One",
				ID = 1,
				PrimaryKeyName = "ID",
				PropertyName = "SixthValue",
				TableName = "Person"
			};
			return retValue;
		}
	}
}
