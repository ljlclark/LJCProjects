// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TraceFile.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	/// <summary>Represents a Trace File.</summary>
	public class TraceFile
	{
		// Initalizes an object instance.
		/// <include path='items/TraceFileC/*' file='Doc/TraceFile.xml'/>
		public TraceFile(string formName)
		{
			FormName = formName;
			mFileName = $"{FormName}Trace.txt";
			File.WriteAllText(mFileName, "");
		}

		// Writes data to the trace file.
		/// <include path='items/Write/*' file='Doc/TraceFile.xml'/>
		public void Write(string text, LJCDataGrid grid, LJCGridRow row = null)
		{
			StringBuilder builder = new StringBuilder(64);
			//string output;

			//if (null == text)
			//{
			//	output = "\r\n";
			//}
			//else
			//{
			//	builder.AppendLine($"PersonModule:{text}");
			//	if (grid.CurrentRow != null)
			//	{
			//		builder.Append($" - CurrentIndex:{grid.CurrentRow.Index}");
			//	}
			//	if (row != null)
			//	{
			//		builder.Append($"\r\n - RowIndex:{row.Index}");
			//		row = grid.CurrentRow as LJCGridRow;
			//	}
			//	builder.AppendLine($" - {grid.Name}.LastIndex:{grid.LastRowIndex}");
			//	output = builder.ToString();
			//}
			//File.AppendAllText(mFileName, output);
		}

		/// <summary>Gets or sets the FormName value.</summary>
		public string FormName { get; set; }

		private string mFileName;
	}
}
