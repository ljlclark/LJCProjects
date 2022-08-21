// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDBViewDAL;

namespace LJCViewEditorDAL
{
	// Provides ViewEditor helper functions.
	/// <include path='items/DataHelper/*' file='Doc/ProjectViewEditorDAL.xml'/>
	public class DataHelper
	{
		// Initializes an object instance.
		/// <include path='items/DataHelperC/*' file='Doc/DataHelper.xml'/>
		public DataHelper(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;
			mDataManager = new DataManager(mDbServiceRef, mDataConfigName, null);
		}

		// Retrieves the table columns.
		/// <include path='items/GetTableColumns/*' file='Doc/DataHelper.xml'/>
		public DbColumns GetTableColumns(string tableName)
		{
			DbColumns retValue;

			mDataManager.Reset(null, mDataConfigName, tableName);
			retValue = mDataManager.DataDefinition;
			return retValue;
		}

		// Retrieves the parent ViewData table columns.
		/// <include path='items/GetJoinFromColumns/*' file='Doc/DataHelper.xml'/>
		public DbColumns GetJoinFromColumns(int joinID)
		{
			ViewTableManager viewTableManager;
			ViewDataManager viewDataManager;
			ViewJoinManager viewJoinManager;
			DataManager dataManager;
			ViewData viewData;
			ViewJoin viewJoin;
			ViewTable viewTable;
			DbColumns retValue = null;

			if (null == mViewHelper)
			{
				mViewHelper = new ViewHelper(mDbServiceRef, mDataConfigName);
			}
			viewTableManager = mViewHelper.ViewTableManager;
			viewDataManager = mViewHelper.ViewDataManager;
			viewJoinManager = mViewHelper.ViewJoinManager;

			viewJoin = viewJoinManager.RetrieveWithID(joinID);
			if (viewJoin != null)
			{
				// Get parent table record.
				viewData = viewDataManager.RetrieveWithID(viewJoin.ViewDataID);
				viewTable = viewTableManager.RetrieveWithID(viewData.ViewTableID);

				// Get parent table columns.
				dataManager = new DataManager(mDbServiceRef, mDataConfigName
					, viewTable.Name);
				retValue = dataManager.DataDefinition;
			}
			return retValue;
		}

		// Retrieves the ViewJoin table columns.
		/// <include path='items/GetJoinToColumns/*' file='Doc/DataHelper.xml'/>
		public DbColumns GetJoinToColumns(int joinID)
		{
			ViewJoinManager viewJoinManager;
			DataManager dataManager;
			ViewJoin viewJoin;
			DbColumns retValue;

			if (null == mViewHelper)
			{
				mViewHelper = new ViewHelper(mDbServiceRef, mDataConfigName);
			}
			viewJoinManager = mViewHelper.ViewJoinManager;

			// Get join record.
			viewJoin = viewJoinManager.RetrieveWithID(joinID);

			// Get join columns.
			dataManager = new DataManager(mDbServiceRef, mDataConfigName
				, viewJoin.JoinTableName);
			retValue = dataManager.DataDefinition;
			return retValue;
		}

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		private readonly string mDataConfigName;
		private readonly DataManager mDataManager;
		private ViewHelper mViewHelper;
		#endregion
	}
}
