// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppModule.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCAppManagerDAL
{
	// The AppModule table Data Record. 
	/// <include path='items/AppModule/*' file='Doc/ProjectAppManagerDAL.xml'/>
	public class AppModule
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AppModule()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AppModule Clone()
		{
			AppModule retValue = MemberwiseClone() as AppModule;
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

		/// <summary>Gets or sets the TypeName value.</summary>
		//[Required]
		//[Column("TypeName", TypeName="nvarchar(60)")]
		public String TypeName
		{
			get { return mTypeName; }
			set
			{
				value = NetString.InitString(value);
				mTypeName = ChangedNames.Add(ColumnTypeName, mTypeName, value);
			}
		}
		private String mTypeName;

		/// <summary>Gets or sets the Title value.</summary>
		//[Required]
		//[Column("Title", TypeName="nvarchar(60)")]
		public String Title
		{
			get { return mTitle; }
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

		#region Join Properties

		/// <summary>Gets or sets the join FileName value.</summary>
		public string FileName
		{
			get { return mFileName; }
			set { mFileName = NetString.InitString(value); }
		}
		private string mFileName;

		/// <summary>Gets or sets the join ProgramTitle value.</summary>
		public string ProgramTitle
		{
			get { return mProgramTitle; }
			set { mProgramTitle = NetString.InitString(value); }
		}
		private string mProgramTitle;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "AppModule";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "Id";

		/// <summary>The ID column name.</summary>
		public static string PropertyID = "ID";

		/// <summary>The AppProgramID column name.</summary>
		public static string ColumnAppProgramID = "AppProgram_Id";

		/// <summary>The AppProgramID property name.</summary>
		public static string PropertyAppProgramID = "AppProgramID";

		/// <summary>The TypeName column name.</summary>
		public static string ColumnTypeName = "TypeName";

		/// <summary>The Title column name.</summary>
		public static string ColumnTitle = "Title";

		/// <summary>The Active column name.</summary>
		public static string ColumnActive = "Active";

		#region Join Class Data
		#endregion

		/// <summary>The TypeName maximum length.</summary>
		public static int LengthTypeName = 60;

		/// <summary>The Title maximum length.</summary>
		public static int LengthTitle = 60;
		#endregion
	}
}
