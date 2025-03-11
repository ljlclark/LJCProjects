// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserAppProgram.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCAppManagerDAL
{
	/// <summary>The UserAppProgram table Data Record.</summary>
	public class UserAppProgram
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UserAppProgram()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UserAppProgram Clone()
		{
			UserAppProgram retValue = MemberwiseClone() as UserAppProgram;
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the AppManagerUserID value.</summary>
		//[Required]
		//[Column("AppManagerUser_Id", TypeName="int")]
		public Int32 AppManagerUserID
		{
			get { return mAppManagerUserID; }
			set
			{
				mAppManagerUserID = ChangedNames.Add(PropertyAppManagerUserID
					, mAppManagerUserID, value);
			}
		}
		private Int32 mAppManagerUserID;

		/// <summary>Gets or sets the AppProgramID value.</summary>
		//[Required]
		//[Column("AppProgram_Id", TypeName="int")]
		public Int32 AppProgramID
		{
			get { return mAppProgramID; }
			set
			{
				mAppProgramID = ChangedNames.Add(PropertyAppProgramID, mAppProgramID, value);
			}
		}
		private Int32 mAppProgramID;

		/// <summary>Gets or sets the Active value.</summary>
		//[Required]
		//[Column("Active", TypeName="bit")]
		public Boolean? Active
		{
			get { return mActive; }
			set
			{
				mActive = ChangedNames.Add(ColumnActive, mActive, value);
			}
		}
		private Boolean? mActive;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the join user name.</summary>
		public string UserName
		{
			get { return mUserName; }
			set { mUserName = NetString.InitString(value); }
		}
		private string mUserName;

		/// <summary>Gets or sets the join UserID value.</summary>
		public string UserID
		{
			get { return mUserID; }
			set { mUserID = NetString.InitString(value); }
		}
		private string mUserID;

		/// <summary>Gets or sets the join FileName value.</summary>
		public string FileName
		{
			get { return mFileName; }
			set
			{
				mFileName = ChangedNames.Add("FileName", mFileName, value);
			}
		}
		private string mFileName;

		/// <summary>Gets or sets the join Title value.</summary>
		public string Title
		{
			get { return mTitle; }
			set
			{
				mTitle = ChangedNames.Add("Title", mTitle, value);
			}
		}
		private string mTitle;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "UserAppProgram";

		/// <summary>The AppManagerUserID value.</summary>
		public static string ColumnAppManagerUserID = "AppManagerUser_Id";

		/// <summary>the AppManagerUserID property name.</summary>
		public static string PropertyAppManagerUserID = "AppManagerUserID";

		/// <summary>The AppProgramID value.</summary>
		public static string ColumnAppProgramID = "AppProgram_Id";

		/// <summary>the AppProgramID property name.</summary>
		public static string PropertyAppProgramID = "AppProgramID";

		/// <summary>The Active value.</summary>
		public static string ColumnActive = "Active";
		#endregion
	}
}
