// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using LJCDBClientLib;

namespace LJCViewEditor
{
	//Application config values singleton.
	internal sealed class ValuesViewEditor
	{
		#region Constructors

		// Initializes an object instance.
		internal ValuesViewEditor()
		{
			StandardSettings = new StandardSettings();
			StandardSettings.SetProperties("LJCViewEditor.exe.config");
		}
		#endregion

		#region Properties

		// The singleton instance.
		internal static ValuesViewEditor Instance { get; } = new ValuesViewEditor();

		// Gets or sets the StandardSettings value.
		internal StandardSettings StandardSettings { get; set; }
		#endregion
	}
}
