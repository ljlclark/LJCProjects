// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroup.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>The ProcessGroup table Data Record.</summary>
	public class ProcessGroup : IComparable<ProcessGroup>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProcessGroup()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProcessGroup Clone()
		{
			ProcessGroup retValue = MemberwiseClone() as ProcessGroup;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(ProcessGroup other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Not case sensitive.
				retValue = string.Compare(Name, other.Name, true);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the ProcessGroupID value.</summary>
		//[Required]
		//[Column("ProcessGroupID", TypeName="int")]
		public Int32 ProcessGroupID
		{
			get { return mProcessGroupID; }
			set
			{
				mProcessGroupID = ChangedNames.Add(ColumnProcessGroupID, mProcessGroupID, value);
			}
		}
		private Int32 mProcessGroupID;

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="varchar(60)")]
		public String Name
		{
			get { return mName; }
			set
			{
				value = NetString.InitString(value);
				mName = ChangedNames.Add(ColumnName, mName, value);
			}
		}
		private String mName;

		/// <summary>Gets or sets the Description value.</summary>
		//[Column("Description", TypeName="varchar(100)")]
		public String Description
		{
			get { return mDescription; }
			set
			{
				value = NetString.InitString(value);
				mDescription = ChangedNames.Add(ColumnDescription, mDescription, value);
			}
		}
		private String mDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "ProcessGroup";

		/// <summary>The ProcessGroupID column name.</summary>
		public static string ColumnProcessGroupID = "ProcessGroupID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;
		#endregion
	}
}
