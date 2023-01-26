// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewGridColumn.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	/// <summary>The ViewGridColumn table Data Record.</summary>
	public class ViewGridColumn : IComparable<ViewGridColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public ViewGridColumn()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public ViewGridColumn Clone()
		{
			ViewGridColumn retValue = MemberwiseClone() as ViewGridColumn;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ViewGridColumn other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				retValue = ViewDataID.CompareTo(other.ViewDataID);
				if (0 == retValue)
				{
					retValue = ViewColumnID.CompareTo(other.ViewColumnID);
				}
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the ViewDataID value.</summary>
		//[Required]
		//[Column("ViewDataID", TypeName="int")]
		public Int32 ViewDataID
		{
			get { return mViewDataID;  }
			set
			{
				mViewDataID = ChangedNames.Add(ColumnViewDataID, mViewDataID, value);
			}
		}
		private Int32 mViewDataID;

		/// <summary>Gets or sets the ViewColumnID value.</summary>
		//[Required]
		//[Column("ViewColumnID", TypeName="int")]
		public Int32 ViewColumnID
		{
			get { return mViewColumnID;  }
			set
			{
				mViewColumnID = ChangedNames.Add(ColumnViewColumnID, mViewColumnID, value);
			}
		}
		private Int32 mViewColumnID;

		/// <summary>Gets or sets the Sequence value.</summary>
		//[Column("Sequence", TypeName="int")]
		public Int32 Sequence
		{
			get { return mSequence;  }
			set
			{
				mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
			}
		}
		private Int32 mSequence;

		/// <summary>Gets or sets the Caption value.</summary>
		//[Column("Caption", TypeName="nvarchar(60)")]
		public String Caption
		{
			get { return mCaption; }
			set
			{
				value = NetString.InitString(value);
				mCaption = ChangedNames.Add(ColumnCaption, mCaption, value);
			}
		}
		private String mCaption;

		/// <summary>Gets or sets the Width value.</summary>
		//[Column("Width", TypeName="int")]
		public Int32 Width
		{
			get { return mWidth;  }
			set
			{
				mWidth = ChangedNames.Add(ColumnWidth, mWidth, value);
			}
		}
		private Int32 mWidth;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the join ColumnName value.</summary>
		public string ColumnName { get; set; }

		/// <summary>Gets or sets the join PropertyName value.</summary>
		public string PropertyName { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "ViewGridColumn";

		/// <summary>The ViewDataID column name.</summary>
		public static string ColumnViewDataID = "ViewDataID";

		/// <summary>The ViewColumnID column name.</summary>
		public static string ColumnViewColumnID = "ViewColumnID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The Caption column name.</summary>
		public static string ColumnCaption = "Caption";

		/// <summary>The Width column name.</summary>
		public static string ColumnWidth = "Width";
		#endregion
	}
}
