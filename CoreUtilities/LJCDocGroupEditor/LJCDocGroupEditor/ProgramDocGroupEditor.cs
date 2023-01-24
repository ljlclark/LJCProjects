// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramDocGroupEditor.cs
using System;
using System.Windows.Forms;

namespace LJCDocGroupEditor
{
	// The program entry point class.
	/// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
	static class ProgramDocGroupEditor
	{
		// The program entry point function.
		/// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DocGenGroupList());
		}
	}
}
