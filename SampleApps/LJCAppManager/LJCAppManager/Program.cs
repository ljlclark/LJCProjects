// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAppManager/Program.cs
using System;
using System.Windows.Forms;

namespace LJCAppManager
{
	// The main entry point class.
	/// <include path='items/Program/*' file='Doc/ProjectAppManager.xml'/>
	public static class Program
	{
		// The main entry point for the application.
		/// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}
	}
}
