// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformMatch.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	/// <summary>The TransformMatch table Data Record.</summary>
	public class TransformMatch : IComparable<TransformMatch>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TransformMatch()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TransformMatch Clone()
		{
			TransformMatch retValue = MemberwiseClone() as TransformMatch;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(TransformMatch other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				retValue = TransformMatchID.CompareTo(other.TransformMatchID);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the TransformMatchID value.</summary>
		//[Required]
		//[Column("TransformMatchID", TypeName="int")]
		public Int32 TransformMatchID
		{
			get { return mTransformMatchID;  }
			set
			{
				mTransformMatchID = ChangedNames.Add(ColumnTransformMatchID, mTransformMatchID, value);
			}
		}
		private Int32 mTransformMatchID;

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
		public static string TableName = "TransformMatch";

		/// <summary>The TransformMatchID column name.</summary>
		public static string ColumnTransformMatchID = "TransformMatchID";

		/// <summary>The TransformID column name.</summary>
		public static string ColumnTransformID = "TransformID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The SourceColumnID column name.</summary>
		public static string ColumnSourceColumnID = "SourceColumnID";

		/// <summary>The TargetColumnID column name.</summary>
		public static string ColumnTargetColumnID = "TargetColumnID";
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
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class MatchSourceIDComparer : IComparer<TransformMatch>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(TransformMatch x, TransformMatch y)
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
	public class MatchTargetIDComparer : IComparer<TransformMatch>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(TransformMatch x, TransformMatch y)
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
	public class MatchColumnIDsComparer : IComparer<TransformMatch>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(TransformMatch x, TransformMatch y)
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
