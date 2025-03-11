// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCFacilityManager/Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCFacilityManager
{
	// The program entry point class.
	/// <include path='items/Program/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
	static class Program
	{
		// The main entry point for the application.
		/// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FacilityForm());
		}
	}
}
