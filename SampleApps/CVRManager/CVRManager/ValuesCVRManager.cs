// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace CVRManager
{
	/// <summary>Application values singleton class.</summary>
	internal sealed class ValuesCVRManager
	{
		#region Constructors

		// Initializes an object instance.
		internal ValuesCVRManager()
		{
			StandardSettings = new StandardUISettings();
			StandardSettings.SetProperties("CVRManager.exe.config");

			string value;
			AppSettings appSettings = StandardSettings.AppSettings;

			TemperatureUnitCode = appSettings.GetString("TemperatureUnitCode");
			if (null == TemperatureUnitCode
				|| "dCdF".IndexOf(TemperatureUnitCode) < 0)
			{
				TemperatureUnitCode = "dF";
			}

			value = appSettings.GetString("TemperatureHighValue");
			decimal.TryParse(value, out decimal decimalHighValue);

			value = appSettings.GetString("TemperatureLowValue");
			decimal.TryParse(value, out decimal decimalLowValue);

			switch (TemperatureUnitCode)
			{
				case "dC":
					if (decimalHighValue < 37.1m)
					{
						decimalHighValue = 38m;
					}
					TemperatureHighValue = decimalHighValue;

					if (decimalLowValue < 35m)
					{
						decimalLowValue = 35m;
					}
					TemperatureLowValue = decimalLowValue;
					break;

				case "dF":
					if (decimalHighValue < 98.8m)
					{
						decimalHighValue = 100.4m;
					}
					TemperatureHighValue = decimalHighValue;

					if (decimalLowValue < 95m)
					{
						decimalLowValue = 95m;
					}
					TemperatureLowValue = decimalLowValue;
					break;
			}
		}
		#endregion

		#region Properties

		// The singleton instance.
		internal static ValuesCVRManager Instance { get; }
			= new ValuesCVRManager();

		// Gets or sets the StandardUISettings value.
		internal StandardUISettings StandardSettings { get; set; }

		// Gets or sets the Temperature Low value.
		internal decimal TemperatureLowValue { get; set; }

		// Gets or sets the Temperature High value.
		internal decimal TemperatureHighValue { get; set; }

		// Gets or sets the Temperature unit code.
		internal string TemperatureUnitCode { get; set; }
		#endregion
	}
}
