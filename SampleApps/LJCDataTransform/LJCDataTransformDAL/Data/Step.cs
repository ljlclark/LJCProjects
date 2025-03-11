// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Step.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>The Step table Data Record.</summary>
	public class Step : IComparable<Step>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Step()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Step Clone()
		{
			Step retValue = MemberwiseClone() as Step;
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
		public int CompareTo(Step other)
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
		//[Column("Sequence", TypeName="smallint")]
		public Int16 Sequence
		{
			get { return mSequence; }
			set
			{
				mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
			}
		}
		private Int16 mSequence;

		/// <summary>Gets or sets the Sequence value.</summary>
		//[Required]
		//[Column("StatusID", TypeName="smallint")]
		public Int16 StatusID
		{
			get { return mStatusID; }
			set
			{
				mStatusID = ChangedNames.Add(ColumnSequence, mSequence, value);
			}
		}
		private Int16 mStatusID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "Step";

		/// <summary>The StepID column name.</summary>
		public static string ColumnStepID = "StepID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The ProcessID column name.</summary>
		public static string ColumnDataProcessID = "DataProcessID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The StatusID column name.</summary>
		public static string ColumnStatusID = "StatusID";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;
		#endregion
	}
}
