// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _ClassName_ ManagersAppName
// Managers_ClassName_.cs
// #SectionEnd Title
using System;
using LJCDBClientLib;
using LJCNetCommon;

// #SectionBegin Class
// #Value _ClassName_ ManagersAppName
// #Value _NameSpace_ ManagersAppNameDAL5
namespace _NameSpace_
{
	/// <summary>Gets the Manager objects.</summary>
	public class _ClassName_
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../CommonData.xml'/>
		public _ClassName_()
		{
		}
		#endregion

		/// <summary>
		/// Sets the DB properties.
		/// </summary>
		/// <param name="dbServiceRef">The database service reference object.</param>
		/// <param name="dataConfigName">The data configuration name.</param>
		public void SetDBProperties(DbServiceRef dbServiceRef
			, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;
		}

		#region Properties
		// #SectionBegin Properties
		// #Value _TableName_ DataColumn

		/// <summary>Gets the _TableName_Manager object.</summary>
		public _TableName_Manager _TableName_Manager
		{
			get
			{
				if (null == m_TableName_Manager)
				{
					_TableName_Manager
						= new _TableName_Manager(mDbServiceRef, mDataConfigName);
				}
				return m_TableName_Manager;
			}

			private set
			{
				if (value != null)
				{
					m_TableName_Manager = value;
				}
			}
		}
		private _TableName_Manager m_TableName_Manager;
		// #SectionEnd Properties
		#endregion

		#region Class Data

		private DbServiceRef mDbServiceRef;
		private string mDataConfigName;
		#endregion
	}
}
// #SectionEnd Class
