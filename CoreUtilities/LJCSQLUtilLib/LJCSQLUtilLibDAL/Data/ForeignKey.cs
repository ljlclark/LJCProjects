// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ForeignKey.cs
using System;

namespace LJCSQLUtilLibDAL
{
	/// <summary>The ForeignKey Data Record.</summary>
	public class ForeignKey : IComparable<ForeignKey>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ForeignKey()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ForeignKey Clone()
		{
			ForeignKey retValue = MemberwiseClone() as ForeignKey;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			string retValue;

			retValue = $"{SourceTable}.{SourceColumn}-{TargetTable}.{TargetColumn}";
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ForeignKey other)
		{
			int retValue;

			if (null == other)
			{
				// This value is greater than null.
				retValue = 1;
			}
			else
			{
				// Not case sensitive.
				retValue = string.Compare(TargetTable, other.TargetTable, true);
				if (1 == retValue)
				{
					retValue = string.Compare(TargetColumn, other.TargetColumn, true);
				}
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		/// <summary>Gets or sets the Table Database name.</summary>
		public string TableCatalog { get; set; }

		/// <summary>Gets or sets the Table Schema name.</summary>
		public string TableSchema { get; set; }

		/// <summary>Gets or sets the Source Table name.</summary>
		public string SourceTable { get; set; }

		/// <summary>Gets or sets the Source Column name.</summary>
		public string SourceColumn { get; set; }

		/// <summary>Gets or sets the Source Constraint name.</summary>
		public string SourceConstraint { get; set; }

		/// <summary>Gets or sets the join Update Rule text.</summary>
		public string UpdateRule { get; set; }

		/// <summary>Gets or sets the join Delete Rule text.</summary>
		public string DeleteRule { get; set; }

		/// <summary>Gets or sets the join Ordinal Position.</summary>
		public int OrdinalPosition { get; set; }

		/// <summary>Gets or sets the join Target Table name.</summary>
		public string TargetTable { get; set; }

		/// <summary>Gets or sets the join Target Column name.</summary>
		public string TargetColumn { get; set; }

		/// <summary>Gets or sets the join Target Constraint name.</summary>
		public string TargetConstraint { get; set; }
		#endregion
	}
}
