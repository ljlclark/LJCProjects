using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCSQLUtilLibDAL
{
	/// <summary>Gets the SQLUtilLibDAL Managers.</summary>
	public class SQLUtilLibManagers
	{
		#region Constructors

		// Initializes an object instance.
		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <param name="dbServiceRef">The database service reference object.</param>
		/// <param name="dataConfigName">The data configuration name.</param>
		public SQLUtilLibManagers(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;
		}
		#endregion

		#region Properties

		/// <summary>Gets the DbMetaDataTableManager object.</summary>
		public DbMetaDataTableManager DbMetaDataTableManager
		{
			get
			{
				if (null == mDbMetaDataTableManager)
				{
					DbMetaDataTableManager
						= new DbMetaDataTableManager(mDbServiceRef, mDataConfigName);
				}
				return mDbMetaDataTableManager;
			}

			private set
			{
				if (value != null)
				{
					mDbMetaDataTableManager = value;
				}
			}
		}

		/// <summary>Gets the DbMetaDataColumnManager object.</summary>
		public DbMetaDataColumnManager DbMetaDataColumnManager
		{
			get
			{
				if (null == mDbMetaDataColumnManager)
				{
					DbMetaDataColumnManager
						= new DbMetaDataColumnManager(mDbServiceRef, mDataConfigName);
				}
				return mDbMetaDataColumnManager;
			}

			private set
			{
				if (value != null)
				{
					mDbMetaDataColumnManager = value;
				}
			}
		}

		/// <summary>Gets the DbMetaDataKeyManager object.</summary>
		public DbMetaDataKeyManager DbMetaDataKeyManager
		{
			get
			{
				if (null == mDbMetaDataKeyManager)
				{
					DbMetaDataKeyManager = new DbMetaDataKeyManager(mDbServiceRef, mDataConfigName);
				}
				return mDbMetaDataKeyManager;
			}

			private set
			{
				if (value != null)
				{
					mDbMetaDataKeyManager = value;
				}
			}
		}

		/// <summary>Gets the DbMetaDataKeyTypeManager object.</summary>
		public DbMetaDataKeyTypeManager DbMetaDataKeyTypeManager
		{
			get
			{
				if (null == mDbMetaDataKeyTypeManager)
				{
					DbMetaDataKeyTypeManager = new DbMetaDataKeyTypeManager(mDbServiceRef
						, mDataConfigName);
				}
				return mDbMetaDataKeyTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mDbMetaDataKeyTypeManager = value;
				}
			}
		}

		/// <summary>Gets the ForeignKeyManager object.</summary>
		public ForeignKeyManager ForeignKeyManager
		{
			get
			{
				if (null == mForeignKeyManager)
				{
					ForeignKeyManager = new ForeignKeyManager(mDbServiceRef
						, mDataConfigName);
				}
				return mForeignKeyManager;
			}

			private set
			{
				if (value != null)
				{
					mForeignKeyManager = value;
				}
			}
		}
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		private readonly string mDataConfigName;

		private DbMetaDataTableManager mDbMetaDataTableManager;
		private DbMetaDataColumnManager mDbMetaDataColumnManager;
		private DbMetaDataKeyManager mDbMetaDataKeyManager;
		private DbMetaDataKeyTypeManager mDbMetaDataKeyTypeManager;
		private ForeignKeyManager mForeignKeyManager;
		#endregion
	}
}
