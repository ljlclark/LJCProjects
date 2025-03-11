// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewInfo.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LJCGridDataLib;

namespace LJCFacilityManager
{
	/// <summary>Contains the current View information.</summary>
	public class ViewInfo
	{
		// Gets or sets the View DataID.
		internal int DataID { get; set; }

		// Gets or sets the View TableID.
		internal int TableID { get; set; }

		// Gets or sets the View TableName.
		internal string TableName { get; set; }
	}
}
