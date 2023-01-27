// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCUnitMeasure\Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCUnitMeasure
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new UnitMeasureList());
		}
	}
}
