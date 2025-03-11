// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformMaps.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	/// <summary>The TransformMap table Data Record.</summary>
	public class TransformMap : IComparable<TransformMap>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TransformMap()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TransformMap Clone()
		{
			TransformMap retValue = MemberwiseClone() as TransformMap;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			string retValue = "";
			retValue += SourceColumnName;
			if (NetString.HasValue(TargetColumnName))
			{
				retValue += "-";
				retValue += TargetColumnName;
			}
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(TransformMap other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Not case sensitive.
				retValue = TransformMapID.CompareTo(other.TransformMapID);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the TransformMapID value.</summary>
		//[Required]
		//[Column("TransformMapID", TypeName="int")]
		public Int32 TransformMapID
		{
			get { return mTransformMapID;  }
			set
			{
				mTransformMapID = ChangedNames.Add(ColumnTransformMapID, mTransformMapID, value);
			}
		}
		private Int32 mTransformMapID;

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

		/// <summary>Gets or sets the SourceColumnID value.</summary>
		//[Required]
		//[Column("SourceColumnID", TypeName="smallint")]
		public Int16 SourceColumnID
		{
			get { return mSourceColumnID;  }
			set
			{
				mSourceColumnID = ChangedNames.Add(ColumnSourceColumnID, mSourceColumnID, value);
			}
		}
		private Int16 mSourceColumnID;

		/// <summary>Gets or sets the TargetColumnID value.</summary>
		//[Required]
		//[Column("TargetColumnID", TypeName="smallint")]
		public Int16 TargetColumnID
		{
			get { return mTargetColumnID;  }
			set
			{
				mTargetColumnID = ChangedNames.Add(ColumnTargetColumnID, mTargetColumnID, value);
			}
		}
		private Int16 mTargetColumnID;

		/// <summary>Gets or sets the MapTypeID value.</summary>
		//[Required]
		//[Column("MapTypeID", TypeName="smallint")]
		public Int16 MapTypeID
		{
			get { return mMapTypeID;  }
			set
			{
				mMapTypeID = ChangedNames.Add(ColumnMapTypeID, mMapTypeID, value);
			}
		}
		private Int16 mMapTypeID;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the Join SourceColumn name.</summary>
		public string SourceColumnName { get; set; }

		/// <summary>Gets or sets the Join Source Description.</summary>
		public string SourceDescription { get; set; }

		/// <summary>Gets or sets the Join TargetColumn name.</summary>
		public string TargetColumnName { get; set; }

		/// <summary>Gets or sets the Join Target Description.</summary>
		public string TargetDescription { get; set; }

		/// <summary>Gets or sets the Join MapType name.</summary>
		public string MapTypeName { get; set; }

		/// <summary>Gets or sets the Join LayoutColumn ID.</summary>
		public int LayoutColumnID { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }

		/// <summary>Gets or sets the Change Status value.</summary>
		public ChangeStatus ChangeStatus { get; set; }
			#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "TransformMap";

		/// <summary>The TransformMapID column name.</summary>
		public static string ColumnTransformMapID = "TransformMapID";

		/// <summary>The TransformID column name.</summary>
		public static string ColumnTransformID = "TransformID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The SourceColumnID column name.</summary>
		public static string ColumnSourceColumnID = "SourceColumnID";

		/// <summary>The TargetColumnID column name.</summary>
		public static string ColumnTargetColumnID = "TargetColumnID";

		/// <summary>The TransformTypeID column name.</summary>
		public static string ColumnMapTypeID = "MapTypeID";
		#endregion

		#region Calculated and Join Class Data

		/// <summary>The Join SourceColumn name.</summary>
		public static string ColumnSourceColumnName = "SourceColumnName";

		/// <summary>The Join SourceColumn name.</summary>
		public static string ColumnSourceDescription = "SourceDescription";

		/// <summary>The Join TypeName column name.</summary>
		public static string ColumnTargetColumnName = "TargetColumnName";

		/// <summary>The Join TypeName column name.</summary>
		public static string ColumnTargetDescription = "TargetDescription";

		/// <summary>The Join TypeName column name.</summary>
		public static string ColumnMapTypeName = "MapTypeName";
		#endregion
	}

	/// <summary>The Change Status values.</summary>
	public enum ChangeStatus
	{
		/// <summary>Add record.</summary>
		Add,
		/// <summary>Change record.</summary>
		Change,
		/// <summary>Delete record.</summary>
		Delete
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class SourceIDComparer : IComparer<TransformMap>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(TransformMap x, TransformMap y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				retValue = x.TransformID.CompareTo(y.TransformID);
				if (0 == retValue)
				{
					retValue = x.SourceColumnID.CompareTo(y.SourceColumnID);
				}
			}
			return retValue;
		}
	}

	/// <summary>Sort and search on Name value.</summary>
	public class TargetIDComparer : IComparer<TransformMap>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(TransformMap x, TransformMap y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				retValue = x.TransformID.CompareTo(y.TransformID);
				if (0 == retValue)
				{
					retValue = x.TargetColumnID.CompareTo(y.TargetColumnID);
				}
			}
			return retValue;
		}
	}

	/// <summary>Sort and search on Name value.</summary>
	public class ColumnIDsComparer : IComparer<TransformMap>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(TransformMap x, TransformMap y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				retValue = x.TransformID.CompareTo(y.TransformID);
				if (0 == retValue)
				{
					retValue = x.SourceColumnID.CompareTo(y.SourceColumnID);
					if (0 == retValue)
					{
						retValue = x.TargetColumnID.CompareTo(y.TargetColumnID);
					}
				}
			}
			return retValue;
		}
	}
	#endregion
}
