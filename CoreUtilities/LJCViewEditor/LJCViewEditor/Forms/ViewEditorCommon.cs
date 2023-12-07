// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewEditorCommon.cs
using System;
using LJCDBClientLib;
using LJCWinFormCommon;

namespace LJCViewEditor
{
	/// <summary>Contains ViewEditor common static functions.</summary>
	public class ViewEditorCommon
	{
		// Create the application tables.
		internal static void CreateTables(SystemException e, string dataConfigName)
		{
			string[] fileSpecs = {
				@"SQLScript\CreateViewTables.sql"
			};

			int errorCode = ManagerCommon.GetMissingTableErrorCode(dataConfigName);
			if (e.HResult == errorCode)
			{
				if (FormCommon.CreateTablesPrompt(e.Message, fileSpecs))
				{
					if (!ManagerCommon.CreateTables(dataConfigName, fileSpecs))
					{
						throw new SystemException(e.Message);
					}
				}
			}
		}

		// Truncate the name at a Hyphen.
		internal static string TruncateAtHyphen(string name)
		{
			string retValue = name;

			int index = name.IndexOf('-');
			if (index > -1)
			{
				retValue = name.Substring(0, index).Trim();
			}
			return retValue;
		}
	}
}
