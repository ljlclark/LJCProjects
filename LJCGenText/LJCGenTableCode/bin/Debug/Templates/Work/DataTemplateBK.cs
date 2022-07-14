// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using LJC.Net.Common;
using LJC.DBClientLib;

// #SectionBegin Class
// #Value _NameSpace_
// #Value _CollectionName_
// #Value _ClassName_
// #Value _TableName_
// #Value _FullAppName_
// #Value _AppName_
// #Value _ToStringName_
// #Value _CompareToName_
namespace _Namespace_
{
	/// <summary>The _TableName_ table Data Object.</summary>
	public class _ClassName_ : IComparable<_ClassName_>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../CommonData.xml'/>
		public _ClassName_()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../CommonData.xml'/>
		public _ClassName_ Clone()
		{
			_ClassName_ retValue = MemberwiseClone() as _ClassName_;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../CommonData.xml'/>
		public override string ToString()
		{
			return m_ToStringName_;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../CommonData.xml'/>
		public int CompareTo(_ClassName_ other)
		{
			int retValue;

			if (null == other)
			{
				// This value is greater than null.
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = _CompareToName_.CompareTo(other._CompareToName_);

				// Not case sensitive.
				retValue = string.Compare(_CompareToName_, other._CompareToName_, true);
			}
			return retValue;
		}
		#endregion

		#region Data Properties
		// #SectionBegin Properties
		// #Value _DataType_
		// #Value _ColumnName_
		// #Value _PropertyName_
		// #Value _MaxLength_

		/// <summary>Gets or sets the _PropertyName_ value.</summary>
		// #IfBegin _DataType_ String
		public _DataType_ _PropertyName_
		{
			get { return m_PropertyName_; }
			set
			{
				// Change next line to "Property" constant if property was renamed.
				m_PropertyName_ = ChangedNames.Add(Column_PropertyName_, m_PropertyName_, value);
			}
		}
		private _DataType_ m_PropertyName_;
		// #IfElse _DataType_
		public _DataType_ _PropertyName_ { get; set; }
		// #IfEnd _DataType_
		// #SectionEnd Properties
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the Join TypeName value.</summary>
		//public string TypeName { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "_TableName_";
		// #SectionBegin Properties
		// #Value _DataType_
		// #Value _ColumnName_
		// #Value _PropertyName_
		// #Value _MaxLength_

		/// <summary>The _ColumnName_ column name.</summary>
		public static string Column_ColumnName_ = "_ColumnName_";
		// #SectionEnd Properties

		// #SectionBegin Properties
		// #Value _DataType_
		// #Value _ColumnName_
		// #Value _PropertyName_
		// #Value _MaxLength_
		// #IfBegin _DataType_ String

		/// <summary>The _ColumnName_ maximum length.</summary>
		public static int Length_ColumnName_ = _MaxLength_;
		// #IfEnd _DataType_
		// #SectionEnd Properties
		#endregion

		#region Calculated and Join Class Data

		///// <summary>The Join TypeName column name.</summary>
		//public static string ColumnTypeName = "TypeName";
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class NameComparer : IComparer<_ClassName_>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../CommonData.xml'/>
		public int Compare(_ClassName_ x, _ClassName_ y)
		{
			int retValue;

			retValue = LJCNetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				//retValue = x.Name.CompareTo(y.Name);

				// Not case sensitive.
				retValue = string.Compare(x.Name, y.Name, true);
			}
			return retValue;
		}
	}
	#endregion
}
// #SectionEnd Class
