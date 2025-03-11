// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleHost/Program.cs
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ModuleHost
{
	// The program entry point class.
	/// <include path='items/Program/*' file='Doc/ProjectModuleHost.xml'/>
	static class Program
	{
		// The debug module names.
		private static string GetDebugModuleName()
		{
			string retValue;

			//retValue = "LJCFacilityManager.BusinessModule";
			//retValue = "LJCFacilityManager.CodeTypeModule";
			//retValue = "LJCFacilityManager.FacilityModule";
			retValue = "LJCFacilityManager.PersonModule";
			return retValue;
		}

		// The main entry point for the application.
		/// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (0 == args.Length)
			{
				args = new string[2];
				args[0] = "LJCFacilityManager.exe";
			}
			if (1 == args.Length)
			{
				string temp = args[0];
				args = new string[2];
				args[0] = temp;
			}
			if (null == args[1])
			{
				args[1] = GetDebugModuleName();
			}

			Application.Run(new ModuleHostForm(args[0], args[1]));
		}

		#region Class Data

		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		//const int SW_HIDE = 0;
		//const int SW_SHOW = 5;
		#endregion
	}
}
