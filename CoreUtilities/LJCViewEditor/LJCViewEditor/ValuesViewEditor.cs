// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesViewEditor.cs
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
			StandardSettings = new StandardUISettings();
			StandardSettings.SetProperties("LJCViewEditor.exe.config");
		}
		#endregion

		#region Properties

		// The singleton instance.
		internal static ValuesViewEditor Instance { get; } = new ValuesViewEditor();

		// Gets or sets the StandardSettings value.
		internal StandardUISettings StandardSettings { get; set; }
		#endregion
	}
}
