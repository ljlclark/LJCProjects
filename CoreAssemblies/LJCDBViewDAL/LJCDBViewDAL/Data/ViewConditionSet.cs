// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionSet.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;

namespace LJCDBViewDAL
{
	/// <summary>The ViewConditionSet table Data Record.</summary>
	public class ViewConditionSet
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ViewConditionSet()
		{
			ChangedNames = new ChangedNames();
		}
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ViewConditionSet Clone()
    {
      ViewConditionSet retValue = MemberwiseClone() as ViewConditionSet;
      return retValue;
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

		/// <summary>Gets or sets the ViewFilterID value.</summary>
		//[Required]
		//[Column("ViewFilterID", TypeName="int")]
		public Int32 ViewFilterID
		{
			get { return mViewFilterID; }
			set
			{
				mViewFilterID = ChangedNames.Add(ColumnViewFilterID, mViewFilterID, value);
			}
		}
		private Int32 mViewFilterID;

		/// <summary>Gets or sets the BooleanOperator value.</summary>
		//[Required]
		//[Column("BooleanOperator", TypeName="nvarchar(3)")]
		public String BooleanOperator
		{
			get { return mBooleanOperator; }
			set
			{
				value = NetString.InitString(value);
				mBooleanOperator = ChangedNames.Add(ColumnBooleanOperator, mBooleanOperator, value);
			}
		}
		private String mBooleanOperator;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewConditionSet";

		/// <summary>The ID column value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewFilterID value.</summary>
		public static string ColumnViewFilterID = "ViewFilterID";

		/// <summary>The BooleanOperator value.</summary>
		public static string ColumnBooleanOperator = "BooleanOperator";

		/// <summary>The BooleanOperator maximum length.</summary>
		public static int LengthBooleanOperator = 10;
		#endregion
	}
}
