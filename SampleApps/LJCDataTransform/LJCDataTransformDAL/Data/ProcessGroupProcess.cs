// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupProcess.cs
using System;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	/// <summary>The ProcessGroupProcess table Data Record.</summary>
	public class ProcessGroupProcess
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProcessGroupProcess()
		{
			ChangedNames = new ChangedNames();
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
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "ProcessGroupProcess";

		/// <summary>The ProcessGroupID column name.</summary>
		public static string ColumnProcessGroupID = "ProcessGroupID";

		/// <summary>The ProcessID column name.</summary>
		public static string ColumnDataProcessID = "DataProcessID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";
		#endregion
	}
}
