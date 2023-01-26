// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewEditorManagers.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCViewEditorDAL
{
	/// <summary>Creates the SystemBuildDAL Manager objects.</summary>
	public class ViewEditorManagers
	{
		#region Constructors

		// Initializes an object instance.
		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <param name="dbServiceRef">The database service reference object.</param>
		/// <param name="dataConfigName">The data configuration name.</param>
		public ViewEditorManagers(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;
		}
		#endregion

		#region Properties

		/// <summary>Gets the DataTypeManager object.</summary>
		public DataTypeManager DataTypeManager
		{
			get
			{
				if (null == mDataTypeManager)
				{
					DataTypeManager
						= new DataTypeManager(mDbServiceRef, mDataConfigName);
				}
				return mDataTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mDataTypeManager = value;
				}
			}
		}

		/// <summary>Gets the SqlTableManager object.</summary>
		public SqlTableManager SqlTableManager
		{
			get
			{
				if (null == mSqlTableManager)
				{
					SqlTableManager
						= new SqlTableManager(mDbServiceRef, mDataConfigName);
				}
				return mSqlTableManager;
			}

			private set
			{
				if (value != null)
				{
					mSqlTableManager = value;
				}
			}
		}
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		private readonly string mDataConfigName;

		private DataTypeManager mDataTypeManager;
		private SqlTableManager mSqlTableManager;
		#endregion
	}
}
