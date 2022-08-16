// Copyright (c) Lester J Clark 2017-2019 - All Rights Reserved
using System;

namespace LJCViewEditorDAL
{
	/// <summary>Represents a database table.</summary>
	public class SqlTable
	{
		#region Data Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return Name;
		}
		#endregion

		#region Data Properties

		/// <summary>Gets or sets the Name value.</summary>
		public string Name { get; set; }
		#endregion
	}
}
