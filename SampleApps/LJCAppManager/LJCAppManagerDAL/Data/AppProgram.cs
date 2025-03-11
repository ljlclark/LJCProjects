// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppProgram.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCAppManagerDAL
{
	/// <summary>The AppProgram table Data Record.</summary>
	public class AppProgram
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AppProgram()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AppProgram Clone()
		{
			AppProgram retValue = MemberwiseClone() as AppProgram;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mTitle;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the ID value.</summary>
		//[Required]
		//[Column("Id", TypeName="int")]
		public Int32 ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(PropertyID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the FileName value.</summary>
		//[Required]
		//[Column("FileName", TypeName="nvarchar(40)")]
		public String FileName
		{
			get { return mFileName;  }
			set
			{
				value = NetString.InitString(value);
				mFileName = ChangedNames.Add(ColumnFileName, mFileName, value);
			}
		}
		private String mFileName;

		/// <summary>Gets or sets the Title value.</summary>
		//[Required]
		//[Column("Title", TypeName="nvarchar(60)")]
		public String Title
		{
			get { return mTitle;  }
			set
			{
				value = NetString.InitString(value);
				mTitle = ChangedNames.Add(ColumnTitle, mTitle, value);
			}
		}
		private String mTitle;

		/// <summary>Gets or sets the Active value.</summary>
		//[Column("Active", TypeName="bit")]
		public bool? Active { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "AppProgram";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "Id";

		/// <summary>The ID column name.</summary>
		public static string PropertyID = "ID";

		/// <summary>The FileName column name.</summary>
		public static string ColumnFileName = "FileName";

		/// <summary>The Title column name.</summary>
		public static string ColumnTitle = "Title";

		/// <summary>The Active column name.</summary>
		public static string ColumnActive = "Active";

		/// <summary>The FileName maximum length.</summary>
		public static int LengthFileName = 40;

		/// <summary>The Title maximum length.</summary>
		public static int LengthTitle = 60;
		#endregion
	}
}
