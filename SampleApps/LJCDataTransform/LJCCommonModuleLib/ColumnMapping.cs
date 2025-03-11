// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnMapping.cs
using System;
using LJCDataTransformDAL;

namespace LJCCommonModuleLib
{
	// Represents a Column Mapping.
	/// <include path='items/ColumnMapping/*' file='Doc/ProjectCommonModuleLib.xml'/>
	public class ColumnMapping
	{
		#region Data Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			const int FromOnly = 1;
			const int ToOnly = 2;
			const int Both = 3;

			int textLayout = 0;
			string text = null;

			if (TargetColumn != null)
			{
				textLayout = ToOnly;
			}
			if (SourceColumn != null)
			{
				textLayout = FromOnly;
				if (TargetColumn != null)
				{
					textLayout = Both;
				}
			}

			switch (textLayout)
			{
				case FromOnly:
					text = SourceColumn.Name;
					break;
				case ToOnly:
					text = TargetColumn.Name;
					break;
				case Both:
					text = $"{SourceColumn.Name} - {TargetColumn.Name}";
					break;
			}
			return text;
		}
		#endregion

		#region Data Properties

		// Gets or sets the MapType ID value.
		/// <include path='items/MapTypeID/*' file='Doc/ColumnMapping.xml'/>
		public int MapTypeID { get; set; }

		// Gets or sets the "Source" column value.
		/// <include path='items/SourceColumn/*' file='Doc/ColumnMapping.xml'/>
		public LayoutColumn SourceColumn { get; set; }

		// Gets or sets the "Target" column value.
		/// <include path='items/TargetColumn/*' file='Doc/ColumnMapping.xml'/>
		public LayoutColumn TargetColumn { get; set; }
		#endregion
	}
}
