// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskSource.cs
using System;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	/// <summary>The TaskSource table Data Record.</summary>
	public class TaskSource
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TaskSource()
		{
			ChangedNames = new ChangedNames();
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

		/// <summary>Gets or sets the SourceID value.</summary>
		//[Required]
		//[Column("DataSourceID", TypeName="int")]
		public Int32 DataSourceID
		{
			get { return mDataSourceID; }
			set
			{
				mDataSourceID = ChangedNames.Add(ColumnDataSourceID, mDataSourceID, value);
			}
		}
		private Int32 mDataSourceID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "TaskSource";

		/// <summary>The StepTaskID column name.</summary>
		public static string ColumnStepTaskID = "StepTaskID";

		/// <summary>The SourceID column name.</summary>
		public static string ColumnDataSourceID = "DataSourceID";
		#endregion
	}
}
