// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVCommon.cs
using System;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCWinFormCommon;

namespace CVRManager
{
	// Common Functions
	internal class CVCommon
	{
		// Create the application tables.
		internal static void CreateTables(SystemException e, string dataConfigName)
		{
			string[] fileSpecs = {
					 @"MySQLScript\2FacilityShared.sql",
					 @"MySQLScript\3CVRTables.sql"
				};

			int errorCode = ManagerCommon.GetMissingTableErrorCode(dataConfigName);
			if (e.HResult == errorCode)
			{
				if (FormCommon.CreateTablesPrompt(e.Message, fileSpecs))
				{
					if (false == ManagerCommon.CreateTables(dataConfigName, fileSpecs))
					{
						throw new SystemException(e.Message);
					}
				}
			}
		}
	}
}
