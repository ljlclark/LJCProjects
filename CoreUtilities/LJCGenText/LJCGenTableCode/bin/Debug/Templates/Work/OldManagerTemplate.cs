using System.Collections.Generic;
using LJC.Net.Common;
using LJC.DBMessage;
using LJC.DBServiceLib;

// #SectionBegin Class
namespace _Namespace_
{
	// Copyright (c) Lester J Clark 2017, 2018 - All Rights Reserved
	/// <summary>
	/// Provides _TableName_ specific data manipulation methods.
	/// </summary>
	/// <remarks>
	/// <para>Syntax: public class _ClassName_Manager</para>
	/// 
	/// <para>-- Library Level Remarks</para>
	/// </remarks>
	public class _ClassName_Manager
	{
		#region Constructors

		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <param name="dbService">The database service object.</param>
		/// <remarks><para>Syntax: public _ClassName_Manager(IDBService dbService)</para></remarks>
		public _ClassName_Manager(IDBService dbService)
		{
			ErrorText = null;
			bool success = true;

			mDBService = dbService;
			mDataManager = new DataManager(mDBService, "_DataConfigName_", "_TableName_");
			if (mDataManager.ErrorText != null)
			{
				success = false;
				ErrorText = mDataManager.ErrorText;
			}

			if (true == success)
			{
				mDataManager.MapNames(_ClassName_.ColumnId, "Id");
			}
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Adds a _ClassName_ record to the database.
		/// </summary>
		/// <param name="dataRecord">The data record.</param>
		/// <param name="columnNames">The included column names.</param>
		/// <returns>The _ClassName_ object with the DB assigned key values.</returns>
		/// <remarks><para>Syntax: public _ClassName_ Add(_ClassName_ dataRecord, List&lt;string&gt; columnNames = null)</para></remarks>
		public _ClassName_ Add(_ClassName_ dataRecord, List<string> columnNames = null)
		{
			_ClassName_ retValue = null;

			// The database assigned column names.
			mDataManager.SetDBAssignedColumnNames(new string[]
			{
				_ClassName_.ColumnId
			});

			// The lookup column names to find the inserted record for
			// the Add() method to retrieve the DB assigned column values.
			mDataManager.AddLookupColumnNames(new string[]
			{
				"Name"
			});

			DBResult dbResult = mDataManager.Add(dataRecord, columnNames);
			ErrorText = mDataManager.ErrorText;
			if (dbResult != null && dbResult.Records.Count > 0)
			{
				// Populate a data object with the result values.
				retValue = new _ClassName_();
				DBCommon.SetObjectValues(dbResult.Records[0], retValue);
			}
			return retValue;
		}

		/// <summary>
		/// Retrieves a _ClassName_ record from the database.
		/// </summary>
		/// <param name="keyRecord">The key record object.</param>
		/// <param name="columnNames">The incuded column names.</param>
		/// <param name="filters">The filter values.</param>
		/// <returns>The _ClassName_ object.</returns>
		/// <remarks><para>Syntax: public _ClassName_ Retrieve(_ClassName_ keyRecord, List&lt;string&gt; columnNames = null, DBFilters filters = null)</para></remarks>
		public _ClassName_ Retrieve(_ClassName_ keyRecord, List<string> columnNames = null
			, DBFilters filters = null)
		{
			_ClassName_ retValue = null;

			DBResult dbResult = mDataManager.Retrieve(keyRecord, columnNames);
			ErrorText = mDataManager.ErrorText;
			if (dbResult != null && dbResult.Records.Count > 0)
			{
				// Populate a data object with the result values.
				// Uses retValue as an object and processes with reflection.
				retValue = new _ClassName_();
				DBCommon.SetObjectValues(dbResult.Records[0], retValue);
			}
			return retValue;
		}

		/// <summary>
		/// Retrieves a collection of data records.
		/// </summary>
		/// <param name="keyRecord">The record containing the key field values.</param>
		/// <param name="columnNames">The incuded column names.</param>
		/// <param name="filters">The filter values.</param>
		/// <param name="joins">The join values.</param>
		/// <returns>The _CollectionName_ collection.</returns>
		/// <remarks>
		/// <para>
		/// Syntax: public _CollectionName_ Load(_ClassName_ keyRecord = null, List&lt;string&gt; columnNames = null
		///   , DBFilters filters = null, DBJoins joins = null)
		/// </para>
		/// </remarks>
		public _CollectionName_ Load(_ClassName_ keyRecord = null, List<string> columnNames = null
			, DBFilters filters = null, DBJoins joins = null)
		{
			_CollectionName_ retValue = null;

			DBResult dbResult = mDataManager.Load(keyRecord, columnNames, filters, joins);
			ErrorText = mDataManager.ErrorText;
			if (dbResult != null && dbResult.Records.Count > 0
				&& string.IsNullOrWhiteSpace(dbResult.ErrorText))
			{
				if (dbResult.Records.Count > 0)
				{
					// Populate a collection with the result records.
					retValue = CreateCollection(dbResult);
				}
			}
			return retValue;
		}

		/// <summary>
		/// Updates the _ClassName_ record.
		/// </summary>
		/// <param name="dataRecord">The data record.</param>
		/// <param name="keyRecord">The key record object.</param>
		/// <param name="columnList">The incuded column names.</param>
		/// <param name="filters">The filter values.</param>
		/// <remarks><para>Syntax: public void Update(_ClassName_ dataRecord, _ClassName_ keyRecord, List&lt;string&gt; columnList = null, DBFilters filters = null)</para></remarks>
		public void Update(_ClassName_ dataRecord, _ClassName_ keyRecord, List<string> columnList = null
			, DBFilters filters = null)
		{
			mDataManager.Update(dataRecord, keyRecord, columnList, filters);
			ErrorText = mDataManager.ErrorText;
		}

		/// <summary>
		/// Deletes a record with the specified ID.
		/// </summary>
		/// <param name="keyRecord">The key record object.</param>
		/// <param name="filters">The filter values.</param>
		/// <remarks><para>Syntax: public void Delete(_ClassName_ keyRecord, DBFilters filters = null)</para></remarks>
		public void Delete(_ClassName_ keyRecord, DBFilters filters = null)
		{
			mDataManager.Delete(keyRecord, filters);
			ErrorText = mDataManager.ErrorText;
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Creates a collection from the result object.
		/// </summary>
		/// <param name="dbResult">The result object.</param>
		/// <returns>The collection.</returns>
		private _CollectionName_ CreateCollection(DBResult dbResult)
		{
			_CollectionName_ retValue = new _CollectionName_();

			foreach (DBColumns dbColumns in dbResult.Records)
			{
				_ClassName_ dataRecord = new _ClassName_();
				DBCommon.SetObjectValues(dbColumns, dataRecord);
				retValue.Add(dataRecord);
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the last error text value.</summary>
		/// <remarks><para>Syntax: public string ErrorText</para></remarks>
		public string ErrorText
		{
			get { return mErrorText; }
			set { mErrorText = LJCNetCommon.InitString(value); }
		}
		private string mErrorText;

		/// <summary>Gets or sets the pagination size.</summary>
		public int PageSize
		{
			get { return mPageSize; }
			set
			{
				mPageSize = value;
				mDataManager.PageSize = mPageSize;
			}
		}
		int mPageSize;

		/// <summary>Gets or sets the pagination start index.</summary>
		public int PageStartIndex { get; set; }
		#endregion

		#region Class Values

		private IDBService mDBService;
		private DataManager mDataManager;
		#endregion
	}
}
// #SectionEnd Class