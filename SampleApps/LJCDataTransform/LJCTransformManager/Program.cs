// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTransformManager/Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCTransformManager
{
	// The program entry point class.
	/// <include path='items/Program/*' file='Doc/ProjectTransformManager.xml'/>
	static class Program
	{
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DataSourceList());
			//Application.Run(new LayoutColumnList());
			//Application.Run(new ProcessList());
			//Application.Run(new StepList());
		}
	}
}
