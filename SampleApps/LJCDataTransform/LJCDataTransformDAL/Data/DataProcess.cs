// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataProcess.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	// The Process table Data Record. 
	/// <include path='items/DataProcess/*' file='Doc/ProjectDataTransformDAL.xml'/>
	public class DataProcess : IComparable<DataProcess>
	{
		#region Static Methods

		// Returns the ProcessStatus enum name from the enum value.
		/// <include path='items/StatusName/*' file='Doc/DataProcess.xml'/>
		public static string StatusName(int StatusValue)
		{
			return Enum.GetName(typeof(ProcessStatusType), StatusValue);
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public DataProcess()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public DataProcess Clone()
		{
			DataProcess retValue = MemberwiseClone() as DataProcess;
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
		public int CompareTo(DataProcess other)
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

		/// <summary>Gets or sets the ProcessID value.</summary>
		//[Required]
		//[Column("DataProcessID", TypeName="int")]
		public Int32 DataProcessID
		{
			get { return mDataProcessID; }
			set
			{
				mDataProcessID = ChangedNames.Add(ColumnDataProcessID, mDataProcessID, value);
			}
		}
		private Int32 mDataProcessID;

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
		//[Required]
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

		/// <summary>Gets or sets the ProcessStatusID value.</summary>
		//[Required]
		//[Column("ProcessStatisID", TypeName="smallint")]
		public Int16 ProcessStatusID
		{
			get { return mProcessStatusID; }
			set
			{
				mProcessStatusID = ChangedNames.Add(ColumnProcessStatusID, mProcessStatusID, value);
			}
		}
		private Int16 mProcessStatusID;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the join Sequence value.</summary>
		public Int16 Sequence
		{
			get { return mSequence; }
			set
			{
				mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
			}
		}
		private Int16 mSequence;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "DataProcess";

		/// <summary>The ProcessID column name.</summary>
		public static string ColumnDataProcessID = "DataProcessID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The ProcessStatusID column name.</summary>
		public static string ColumnProcessStatusID = "ProcessStatusID";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;
		#endregion

		#region Join Class Data

		/// <summary>The Name maximum length.</summary>
		public static string ColumnSequence = "Sequence";
		#endregion
	}

	/// <summary>The MapType values.</summary>
	public enum MapType
	{
		/// <summary>The Merge option.</summary>
		Merge = 1,
		/// <summary>The Overwrite option.</summary>
		Overwrite,
		/// <summary>The InsertInclude option.</summary>
		InsertInclude
	}
}
