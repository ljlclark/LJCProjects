// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppUser.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCAppManagerDAL
{
	/// <summary>The AppManagerUser table Data Record.</summary>
	// Renamed class from AppManagerUser to AppUser.
	public class AppUser : IComparable<AppUser>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AppUser()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AppUser Clone()
		{
			AppUser retValue = MemberwiseClone() as AppUser;
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
		public int CompareTo(AppUser other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = UserID.CompareTo(other.UserID);

				// Not case sensitive.
				retValue = string.Compare(UserID, other.UserID, true);
			}
			return retValue;
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

		/// <summary>Gets or sets the Name value.</summary>
		//[Column("Name", TypeName="nvarchar(60)")]
		public String Name
		{
			get { return mName;  }
			set
			{
				value = NetString.InitString(value);
				mName = ChangedNames.Add(ColumnName, mName, value);
			}
		}
		private String mName;

		/// <summary>Gets or sets the UserID value.</summary>
		//[Column("UserId", TypeName="nvarchar(60)")]
		public String UserID
		{
			get { return mUserID;  }
			set
			{
				value = NetString.InitString(value);
				mUserID = ChangedNames.Add(PropertyUserID, mUserID, value);
			}
		}
		private String mUserID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "AppManagerUser";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "Id";

		/// <summary>The ID column name.</summary>
		public static string PropertyID = "ID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The UserID column name.</summary>
		public static string ColumnUserID = "UserId";

		/// <summary>The UserID property name.</summary>
		public static string PropertyUserID = "UserID";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The UserID maximum length.</summary>
		public static int LengthUserID = 60;
		#endregion
	}
}
