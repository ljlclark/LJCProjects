// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramReference.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LJCAppManager
{
	// Represents a program reference.
	/// <include path='items/ProgramReference/*' file='Doc/ProgramReference.xml'/>
	public class ProgramReference : IComparable<ProgramReference>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProgramReference()
		{
		}
		#endregion

		#region Public Methods

		// Retrieves the Assembly reference.
		/// <include path='items/GetAssembly/*' file='Doc/ProgramReference.xml'/>
		public Assembly GetAssembly()
		{
			if (File.Exists(FileName)
				&& null == Assembly)
			{
				Assembly = Assembly.LoadFrom(FileName);
			}
			return Assembly;
		}
		#endregion

		#region IComparable Methods

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(ProgramReference other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				retValue = DisplayName.CompareTo(other.DisplayName);
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>The assembly file name.</summary>
		public string FileName { get; set; }

		/// <summary>The display name.</summary>
		public string DisplayName { get; set; }

		/// <summary>The assembly reference.</summary>
		public Assembly Assembly { get; set; }
		#endregion
	}
}
