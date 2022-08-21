// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJCDataDetailLib;
using LJCNetCommon;
using LJCTestDataLib;

namespace DataDetail
{
	// The program entry point class.
	/// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
	static class Program
	{
		// The program entry point function.
		/// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//string configName = "TestConfig.xml";
			DataDetailDialog dialog = new DataDetailDialog("LJCData")
			{
				LJCDataColumns = TestData.GetRecord(),
				LJCKeyItems = TestData.GetKeyItems("FifthValue")
			};

			Application.Run(dialog);
			if (DialogResult.OK == dialog.DialogResult)
			{
				ShowResult(dialog.LJCDataColumns, dialog.LJCKeyItems);
			}
		}

		// 
		private static void ShowResult(DbColumns dbColumns
			, KeyItems controlItems)
		{
			string results = "";
			foreach (DbColumn dbColumn in dbColumns)
			{
				var item = controlItems.GetItem(dbColumn);
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
			MessageBox.Show(results, "Data Values");
		}
	}
}
