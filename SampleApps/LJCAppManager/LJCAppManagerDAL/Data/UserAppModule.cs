// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserAppModule.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCAppManagerDAL
{
	/// <summary>The UserAppModule table Data Record.</summary>
	public class UserAppModule
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UserAppModule()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UserAppModule Clone()
		{
			UserAppModule retValue = MemberwiseClone() as UserAppModule;
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
				mAppManagerUserID = ChangedNames.Add(PropertyAppManagerUserID, mAppManagerUserID
					, value);
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

		/// <summary>Gets or sets the AppModuleID value.</summary>
		//[Required]
		//[Column("AppModule_Id", TypeName="int")]
		public Int32 AppModuleID
		{
			get { return mAppModuleID; }
			set
			{
				mAppModuleID = ChangedNames.Add(PropertyAppModuleID, mAppModuleID, value);
			}
		}
		private Int32 mAppModuleID;

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

		/// <summary>Gets or sets the join Name value.</summary>
		public string Name
		{
			get { return mName; }
			set { mName = NetString.InitString(value); }
		}
		private string mName;

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
			set { mFileName = NetString.InitString(value); }
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

		/// <summary>Gets or sets the join TypeName value.</summary>
		public string TypeName
		{
			get { return mTypeName; }
			set
			{
				mTypeName = ChangedNames.Add("TypeName", mTypeName, value);
			}
		}
		private string mTypeName;

		/// <summary>Gets or sets the join ModuleTitle value.</summary>
		public string ModuleTitle
		{
			get { return mModuleTitle; }
			set
			{
				mModuleTitle = ChangedNames.Add("ModuleTitle", mModuleTitle, value);
			}
		}
		private string mModuleTitle;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "UserAppModule";

		/// <summary>The AppManagerUserID column name.</summary>
		public static string ColumnAppManagerUserID = "AppManagerUser_Id";

		/// <summary>The AppManagerUserID property name.</summary>
		public static string PropertyAppManagerUserID = "AppManagerUserID";

		/// <summary>The AppProgramID column name.</summary>
		public static string ColumnAppProgramID = "AppProgram_Id";

		/// <summary>the AppProgramID property name.</summary>
		public static string PropertyAppProgramID = "AppProgramID";

		/// <summary>The AppModuleID column name.</summary>
		public static string ColumnAppModuleID = "AppModule_Id";

		/// <summary>the AppModuleID property name.</summary>
		public static string PropertyAppModuleID = "AppModuleID";

		/// <summary>The Active column name.</summary>
		public static string ColumnActive = "Active";
		#endregion
	}
}
