// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StepTask.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	// The Task table Data Record.
	/// <include path='items/StepTask/*' file='Doc/StepTask.xml'/>
	public class StepTask : IComparable<StepTask>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public StepTask()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public StepTask Clone()
		{
			StepTask retValue = MemberwiseClone() as StepTask;
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
		public int CompareTo(StepTask other)
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

		/// <summary>Gets or sets the StepTaskID value.</summary>
		//[Required]
		//[Column("StepTaskID", TypeName="int")]
		public Int32 StepTaskID
		{
			get { return mStepTaskID; }
			set
			{
				mStepTaskID = ChangedNames.Add(ColumnStepTaskID, mStepTaskID, value);
			}
		}
		private Int32 mStepTaskID;

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

		/// <summary>Gets or sets the StepID value.</summary>
		//[Required]
		//[Column("StepID", TypeName="int")]
		public Int32 StepID
		{
			get { return mStepID; }
			set
			{
				mStepID = ChangedNames.Add(ColumnStepID, mStepID, value);
			}
		}
		private Int32 mStepID;

		/// <summary>Gets or sets the Sequence value.</summary>
		//[Required]
		//[Column("Sequence", TypeName="int")]
		public Int32 Sequence
		{
			get { return mSequence; }
			set
			{
				mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
			}
		}
		private Int32 mSequence;

		/// <summary>Gets or sets the TaskTypeID value.</summary>
		//[Required]
		//[Column("TaskTypeID", TypeName="smallint")]
		public Int16 TaskTypeID
		{
			get { return mTaskTypeID; }
			set
			{
				mTaskTypeID = ChangedNames.Add(ColumnTaskTypeID, mTaskTypeID, value);
			}
		}
		private Int16 mTaskTypeID;

		/// <summary>Gets or sets the ActionItemName value.</summary>
		//[Column("ActionItemName", TypeName="nvarchar(100)")]
		public String ActionItemName
		{
			get { return mActionItemName; }
			set
			{
				value = NetString.InitString(value);
				mActionItemName = ChangedNames.Add(ColumnActionItemName, mActionItemName, value);
			}
		}
		private String mActionItemName;

		/// <summary>Gets or sets the TaskTypeID value.</summary>
		//[Required]
		//[Column("TaskStatusID", TypeName="smallint")]
		public Int16 TaskStatusID
		{
			get { return mTaskStatusID; }
			set
			{
				mTaskStatusID = ChangedNames.Add(ColumnTaskStatusID, mTaskStatusID, value);
			}
		}
		private Int16 mTaskStatusID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "StepTask";

		/// <summary>The StepTaskID column name.</summary>
		public static string ColumnStepTaskID = "StepTaskID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The StepID column name.</summary>
		public static string ColumnStepID = "StepID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The TaskTypeID column name.</summary>
		public static string ColumnTaskTypeID = "TaskTypeID";

		/// <summary>The CodeItemName column name.</summary>
		public static string ColumnActionItemName = "ActionItemName";

		/// <summary>The CodeItemName column name.</summary>
		public static string ColumnTaskStatusID = "TaskStatusID";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;

		/// <summary>The ConnectionString maximum length.</summary>
		public static int LengthConnectionString = 100;

		/// <summary>The CodeItemName maximum length.</summary>
		public static int LengthCodeItemName = 100;
		#endregion
	}
}
