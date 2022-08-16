// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCViewEditor
{
	// The program entry point class.
	/// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
	static class Program
	{
		// The program entry point function.
		/// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ViewEditorList());
		}
	}
}
