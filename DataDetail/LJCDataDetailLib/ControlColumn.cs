// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;

namespace LJCDataDetailLib
{
	// Represents a column of Controls consisting of ControlRows.
	/// <include path='items/ControlColumn/*' file='Doc/ControlColumn.xml'/>
	public class ControlColumn
	{
		#region Properties

		/// <summary>Gets or sets the unique Index value.</summary>
		public int Index { get; set; }

		/// <summary>Gets or sets the Labels column width.</summary>
		public int LabelsWidth { get; set; }

		/// <summary>Gets or sets the Controls column width.</summary>
		public int ControlsWidth { get; set; }

		/// <summary>Gets the ControlColumn width.</summary>
		public int Width
		{
			get { return LabelsWidth + ControlsWidth; }
		}

		/// <summary>Gets or sets the number of ControlRows.</summary>
		public int RowCount { get; set; }
		#endregion
	}
}
