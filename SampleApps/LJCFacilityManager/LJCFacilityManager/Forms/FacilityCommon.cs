// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityCommon.cs
using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LJCFacilityManagerDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormControls;
using LJCWinFormCommon;

namespace LJCFacilityManager
{
	/// <summary>Contains common FacilityManager methods.</summary>
	public class FacilityCommon
	{
		// Check for Depencency files.
		internal static string CheckDependencies()
		{
			string retValue = "";

			string fileTypeName = "Dependency";

			string fileSpec = "LJCNetCommon.dll";
      if (!File.Exists(fileSpec))
			{
				retValue += $"{fileTypeName} '{fileSpec}' is not found.\r\n";
			}
			retValue += CheckFile("LJCDBViewDAL.dll", fileTypeName);
			retValue += CheckFile("LJCFacilityManagerDAL.dll", fileTypeName);
			retValue += CheckFile("LJCRegionManager.exe", fileTypeName);
			retValue += CheckFile("LJCRegionDAL.dll", fileTypeName);
			retValue += CheckFile("LJCRegionForm.exe", fileTypeName);
			retValue += CheckFile("LJCViewBuilder.exe", fileTypeName);
			return retValue;
		}

		// Checks for file.
		internal static string CheckFile(string fileSpec, string fileTypeName = "File")
		{
			string retValue = null;

			if (!NetString.HasValue(fileSpec))
			{
				retValue = $"{fileTypeName} name is missing.\r\n";
			}
			else
			{
				if (!File.Exists(fileSpec))
				{
					retValue = $"{fileTypeName} '{fileSpec}' is not found.\r\n";
				}
			}
			return retValue;
		}

		// Returns the Administrator Person object.
		/// <include path='items/GetAdministrator/*' file='Doc/FacilityCommon.xml'/>
		public static Person GetAdministrator(int currentPersonID)
		{
			PersonManager manager;
			Crypto crypto;
			Persons list;
			Person personRecord;
			Person retValue = null;

			crypto = new Crypto();

			// Get singleton values.
			ValuesFacility values = ValuesFacility.Instance;

			StandardUISettings settings = values.StandardSettings;

			manager = new PersonManager(settings.DbServiceRef
				, settings.DataConfigName);
			var keyColumns = new DbColumns()
			{
				{ Person.ColumnUserID, (object)"Admin" }
			};
			personRecord = manager.Retrieve(keyColumns);
			if (personRecord != null
				&& (currentPersonID == 0
				|| personRecord.ID != currentPersonID))
			{
				// currentPersonID == 0 - look for any other admin.
				// currentPersonID > 0 - look for an admin other than current person.
				retValue = personRecord;
			}
			else
			{
				list = manager.Load(null);
				foreach (Person record in list)
				{
					if (record.ID != currentPersonID)
					{
						crypto.Decrypt(record.Password);
						if (crypto.IsAdministrator)
						{
							retValue = record;
							break;
						}
					}
				}
			}
			return retValue;
		}

		/// <summary>Displays the Corrupt Password message.</summary>
		public static void ShowCorruptPasswordMessage()
		{
			StringBuilder builder;
			Person adminRecord;
			string adminName;
			string message;

			// Get singleton values.
			//ValuesFacility values = ValuesFacility.Instance;
			//StandardUISettings settings = values.StandardUISettings;

			adminName = "Error";
			adminRecord = GetAdministrator(0);
			if (adminRecord != null)
			{
				adminName = adminRecord.FullName;
			}

			builder = new StringBuilder(64);
			builder.AppendLine("Corrupted password information.");
			builder.Append($"Please see your administrator '{adminName}'.");
			message = builder.ToString();
			MessageBox.Show(message, "Corrupted Password Data", MessageBoxButtons.OK
				, MessageBoxIcon.Error);
		}
	}
}
