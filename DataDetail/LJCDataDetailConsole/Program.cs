// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Windows.Forms;
using CVRItem;
using DataDetail;
using LJCNetCommon;
using LJCTestDataLib;

namespace LJCDataDetailConsole
{
	// The program entry point class.
	/// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
	class Program
	{
		// The program entry point function.
		/// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
		private static void Main(string[] args)
		{
			string dataConfigName = "TestMySql";
			int cvPersonID = 6;
			CVPersonItem cvPersonItem = new CVPersonItem(cvPersonID, dataConfigName);
			var dataColumns = cvPersonItem.DataColumns;
			var keyItems = cvPersonItem.KeyItems();

			DataDetailDialog dialog = new DataDetailDialog("LJCData")
			{
				LJCDataColumns = dataColumns,
				LJCKeyItems = keyItems
			};
			if (DialogResult.OK == dialog.ShowDialog())
			{
				ShowResult(dialog.LJCDataColumns, dialog.LJCKeyItems);
			}
		}

		// Show the data record results.
		private static void ShowResult(DbColumns dbColumns
			, KeyItems keyItems)
		{
			string results = "";
			foreach (DbColumn dbColumn in dbColumns)
			{
				var item = keyItems.GetItem(dbColumn);
				if (item != null)
				{
					results += $"{dbColumn.PropertyName}, {item.Description}" +
						$", {dbColumn.Value}\r\n";
				}
				else
				{
					results += $"{dbColumn.PropertyName}, {dbColumn.Value}\r\n";
				}
			}
			Console.WriteLine(results);
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		// Get the KeyItems collection.
		private static KeyItems TestKeyItems()
		{
			KeyItems retValue = null;

			retValue = TestData.GetKeyItems("FifthItem");
			var keyItem = TestData.GetKeyItem();
			if (keyItem != null)
			{
				retValue.Add(keyItem);
			}
			return retValue;
		}
	}
}
