// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskStatus.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>The TaskStatus table Data Record.</summary>
	public class TaskStatus : IComparable<TaskStatus>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TaskStatus()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TaskStatus Clone()
		{
			TaskStatus retValue = MemberwiseClone() as TaskStatus;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mDescription;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(TaskStatus other)
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

		/// <summary>Gets or sets the TaskStatusID value.</summary>
		//[Required]
		//[Column("TaskStatusID", TypeName="smallint")]
		public Int16 TaskStatusID
		{
			get { return mTaskStatusID;  }
			set
			{
				mTaskStatusID = ChangedNames.Add(ColumnTaskStatusID, mTaskStatusID, value);
			}
		}
		private Int16 mTaskStatusID;

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="varchar(60)")]
		public String Name
		{
			get { return mName;  }
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
			get { return mDescription;  }
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
		public static string TableName = "TaskStatus";

		/// <summary>The TaskStatusID column name.</summary>
		public static string ColumnTaskStatusID = "TaskStatusID";

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

	/// <summary>The StepTask Status values.</summary>
	public enum StepTaskStatus : short
	{
		/// <summary>Available for processing.</summary>
		Available = 1,
		/// <summary>Selected for active step.</summary>
		Active,
		/// <summary>In-process</summary>
		Inprocess,
		/// <summary>Successfull</summary>
		Ready,
		/// <summary>Failed</summary>
		Failed
	}
}
