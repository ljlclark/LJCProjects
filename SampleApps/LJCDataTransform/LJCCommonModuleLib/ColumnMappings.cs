// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnMappings.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LJCDataTransformDAL;

namespace LJCCommonModuleLib
{
	// Represents a collection of ColumnMap objects. 
	/// <include path='items/ColumnMappings/*' file='Doc/ColumnMappings.xml'/>
	public class ColumnMappings : List<ColumnMapping>
	{
		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ColumnMappings.xml'/>
		public ColumnMapping Add(LayoutColumn sourceColumn, LayoutColumn targetColumn
			, int mapTypeID = 0)
		{
			ColumnMapping retValue = new ColumnMapping()
			{
				SourceColumn = sourceColumn,
				TargetColumn = targetColumn,
				MapTypeID = mapTypeID
			};
			Add(retValue);
			return retValue;
		}
	}
}
