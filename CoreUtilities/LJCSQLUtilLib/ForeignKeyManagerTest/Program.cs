// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using System;
using System.Windows.Forms;

namespace ForeignKeyManagerTest
{
	// The program entry point class.
	/// <include path='items/Program/*' file='Doc/ProjectForeignKeyManagerTest.xml'/>
	static class Program
	{
		// The main entry point for the application.
		/// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
