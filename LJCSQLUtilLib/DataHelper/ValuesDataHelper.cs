// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Drawing;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCDBDataAccessLib;
using LJCDataAccess;

namespace DataHelper
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class ValuesDataHelper
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ValuesDataHelper()
		{
			StandardSettings = new StandardSettings();
			StandardSettings.SetProperties("DataHelper.exe.config");
		}
		#endregion

		#region Properties

		// The singleton instance.
		internal static ValuesDataHelper Instance { get; } = new ValuesDataHelper();

		// Gets or sets the StandardSettings value.
		internal StandardSettings StandardSettings { get; set; }
		#endregion
	}
}
