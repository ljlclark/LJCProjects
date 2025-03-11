// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskTransform.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>The TaskTransform table Data Record.</summary>
	public class TaskTransform : IComparable<TaskTransform>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TaskTransform()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TaskTransform Clone()
		{
			TaskTransform retValue = MemberwiseClone() as TaskTransform;
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
		public int CompareTo(TaskTransform other)
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

		/// <summary>Gets or sets the TransformID value.</summary>
		//[Required]
		//[Column("TransformID", TypeName="int")]
		public Int32 TransformID
		{
			get { return mTransformID;  }
			set
			{
				mTransformID = ChangedNames.Add(ColumnTransformID, mTransformID, value);
			}
		}
		private Int32 mTransformID;

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

		/// <summary>Gets or sets the StepTaskID value.</summary>
		//[Required]
		//[Column("StepTaskID", TypeName="int")]
		public Int32 StepTaskID
		{
			get { return mStepTaskID;  }
			set
			{
				mStepTaskID = ChangedNames.Add(ColumnStepTaskID, mStepTaskID, value);
			}
		}
		private Int32 mStepTaskID;

		/// <summary>Gets or sets the Source DataSourceID value.</summary>
		//[Required]
		//[Column("DataSourceID", TypeName="int")]
		public Int32 SourceDataID
		{
			get { return mSourceDataID;  }
			set
			{
				mSourceDataID = ChangedNames.Add(PropertySourceDataID, mSourceDataID, value);
			}
		}
		private Int32 mSourceDataID;

		/// <summary>Gets or sets the Target DataSourceID value.</summary>
		//[Required]
		//[Column("TargetID", TypeName="int")]
		public Int32 TargetDataID
		{
			get { return mTargetDataID;  }
			set
			{
				mTargetDataID = ChangedNames.Add(PropertyTargetDataID, mTargetDataID, value);
			}
		}
		private Int32 mTargetDataID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "TaskTransform";

		/// <summary>The TransformID column name.</summary>
		public static string ColumnTransformID = "TransformID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The StepTaskID column name.</summary>
		public static string ColumnStepTaskID = "StepTaskID";

		/// <summary>The Source DataSourceID column name.</summary>
		public static string ColumnSourceDataID = "DataSourceID";

		/// <summary>The Source DataSourceID property name.</summary>
		public static string PropertySourceDataID = "SourceDataID";

		/// <summary>The Target DataSourceID column name.</summary>
		public static string ColumnTargetDataID = "TargetID";

		/// <summary>The Target DataSourceID property name.</summary>
		public static string PropertyTargetDataID = "TargetDataID";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;
		#endregion
	}
}
