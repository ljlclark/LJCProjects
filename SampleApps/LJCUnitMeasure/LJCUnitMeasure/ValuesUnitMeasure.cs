// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCUnitMeasure
{
	// Application config values singleton.
	internal sealed class ValuesUnitMeasure
	{
		// Initializes an object instance.
		internal ValuesUnitMeasure()
		{
			string fileSpec = "LJCUnitMeasure.exe.config";

			StandardSettings = new StandardUISettings();
			StandardSettings.SetProperties(fileSpec);

			//AppSettings appSettings = new AppSettings(fileSpec);
		}

		#region Properties

		// The singleton instance.
		internal static ValuesUnitMeasure Instance { get; } = new ValuesUnitMeasure();

		// Gets or sets the StandardSettings value.
		internal StandardUISettings StandardSettings { get; set; }
		#endregion
	}
}
