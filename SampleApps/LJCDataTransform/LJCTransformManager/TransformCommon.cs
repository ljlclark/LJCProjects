// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformCommon.cs
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCDataTransformDAL;
using LJCDBClientLib;
using LJCWinFormCommon;

namespace LJCTransformManager
{
	/// <summary>Common Static Methods</summary>
	public class TransformCommon
	{
		// Check for Depencency files.
		/// <include path='items/CheckDependencies/*' file='Doc/TransformCommon.xml'/>
		internal static string CheckDependencies()
		{
			string retValue = "";

			string fileSpec = "LJCNetCommon.dll";
			if (!File.Exists(fileSpec))
			{
				retValue += $"Dependency '{fileSpec}' is not found.\r\n";
			}
			retValue += CheckFile("LJCDBMessage.dll");
			retValue += CheckFile("LJCDBClientLib.dll");
			retValue += CheckFile("LJCDBServiceLib.dll");
			retValue += CheckFile("LJCDataAccessConfig.dll");
			retValue += CheckFile("LJCDBDataAccessLib.dll");
			retValue += CheckFile("LJCDataAccess.dll");
			retValue += CheckFile("LJCDataTransformDAL.dll");
			return retValue;
		}

		// Checks for file.
		private static string CheckFile(string fileSpec)
		{
			string retValue = null;

			if (!NetString.HasValue(fileSpec))
			{
				retValue = "Dependency name is missing.\r\n";
			}
			else
			{
				if (!File.Exists(fileSpec))
				{
					retValue = $"Dependency '{fileSpec}' is not found.\r\n";
				}
			}
			return retValue;
		}

		// Create the application tables.
		internal static void CreateTables(SystemException e, string dataConfigName)
		{
			string[] fileSpecs = {
				@"SQLScript\CreateTransformDataTables.sql"
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

		// Sets the Grid Cell color based on the Status value.
		/// <include path='items/SetStatusColor/*' file='Doc/TransformCommon.xml'/>
		internal static void SetStatusColor(DataGridViewCell cell, short statusID)
		{
			switch (statusID)
			{
				case (short)ProcessStatusType.Available:
					cell.Style.BackColor = Color.White;
					break;
				case (short)ProcessStatusType.Active:
					cell.Style.BackColor = Color.LightSteelBlue;
					break;
				case (short)ProcessStatusType.Inprocess:
					cell.Style.BackColor = Color.LemonChiffon;
					break;
				case (short)ProcessStatusType.Ready:
					//cell.Style.BackColor = Color.PaleGreen;
					cell.Style = new DataGridViewCellStyle { BackColor = Color.PaleGreen };
					break;
				case (short)ProcessStatusType.Failed:
					cell.Style.BackColor = Color.LightSalmon;
					break;
			}
		}
	}
}
