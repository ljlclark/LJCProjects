// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewCondition.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;

namespace LJCDBViewDAL
{
	/// <summary>The ViewCondition table Data Record.</summary>
	public class ViewCondition
	{
		#region Constructors

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ViewCondition()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return $"{FirstValue} {ComparisonOperator} {SecondValue}";
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the ID value.</summary>
		//[Required]
		//[Column("ID", TypeName="int")]
		public Int32 ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the ViewConditionSetID value.</summary>
		//[Required]
		//[Column("ViewConditionSetID", TypeName="int")]
		public Int32 ViewConditionSetID
		{
			get { return mViewConditionSetID; }
			set
			{
				mViewConditionSetID = ChangedNames.Add(ColumnViewConditionSetID, mViewConditionSetID, value);
			}
		}
		private Int32 mViewConditionSetID;

		/// <summary>Gets or sets the FirstValue value.</summary>
		//[Required]
		//[Column("FirstValue", TypeName="nvarchar(60)")]
		public String FirstValue
		{
			get { return mFirstValue; }
			set
			{
				value = NetString.InitString(value);
				mFirstValue = ChangedNames.Add(ColumnFirstValue, mFirstValue, value);
			}
		}
		private String mFirstValue;

		/// <summary>Gets or sets the SecondValue value.</summary>
		//[Required]
		//[Column("SecondValue", TypeName="nvarchar(60)")]
		public String SecondValue
		{
			get { return mSecondValue; }
			set
			{
				value = NetString.InitString(value);
				mSecondValue = ChangedNames.Add(ColumnSecondValue, mSecondValue, value);
			}
		}
		private String mSecondValue;

		/// <summary>Gets or sets the ComparisonOperator value.</summary>
		//[Column("ComparisonOperator", TypeName="nvarchar(10)")]
		public String ComparisonOperator
		{
			get { return mComparisonOperator; }
			set
			{
				value = NetString.InitString(value);
				mComparisonOperator = ChangedNames.Add(ColumnComparisonOperator, mComparisonOperator, value);
			}
		}
		private String mComparisonOperator;
		#endregion

		#region Calculated and Join Data Properties
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewCondition";

		/// <summary>The ID column value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewConditionSet column ID value.</summary>
		public static string ColumnViewConditionSetID = "ViewConditionSetID";

		/// <summary>The FirstValue value.</summary>
		public static string ColumnFirstValue = "FirstValue";

		/// <summary>The SecondValue value.</summary>
		public static string ColumnSecondValue = "SecondValue";

		/// <summary>The ComparisonOperator value.</summary>
		public static string ColumnComparisonOperator = "ComparisonOperator";

		/// <summary>The FirstValue maximum length.</summary>
		public static int LengthFirstValue = 60;

		/// <summary>The SecondValue maximum length.</summary>
		public static int LengthSecondValue = 60;

		/// <summary>The ComparisonOperator maximum length.</summary>
		public static int LengthComparisonOperator = 10;
		#endregion
	}
}
