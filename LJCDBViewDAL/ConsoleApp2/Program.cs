// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using LJCDBDataAccessLib;
using LJCDBViewDAL;

namespace ConsoleApp2
{
	// The program entry point class.
	/// <include path = 'items/Program/*' file='Doc/ProjectConsoleApp2.xml'/>
	internal class Program
	{
		/// The program entry point function.
		/// <include path = 'items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
		private static void Main(string[] args)
		{
			string dataConfigName = "FacilityManager";
			DbServiceRef dbServiceRef = new DbServiceRef()
			{
				DbDataAccess = new DbDataAccess(dataConfigName)
			};
			ViewHelper viewHelper = new ViewHelper(dbServiceRef, dataConfigName);
			DbRequest dbRequest = viewHelper.GetViewRequest("Person", "PersonStandard");
			//viewHelper.SaveRequestView("PersonTest", "The Test View.", dbRequest);

			ViewColumn viewColumn = viewHelper.ViewColumnManager.RetrieveWithID(1261);
			//DbColumn dbColumn = viewHelper.DbViewColumnManager.RetrieveWithID(1261);

			//DbColumns dbColumns = viewHelper.DbViewColumnManager.LoadWithParentID(1039);
			DbColumns dbColumns = viewHelper.ViewColumnManager.LoadDbColumnsWithParentID(1039);
		}
	}
}
