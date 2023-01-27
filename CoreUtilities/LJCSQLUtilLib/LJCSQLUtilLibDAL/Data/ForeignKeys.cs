// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ForeignKeys.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJCSQLUtilLibDAL
{
	// <summary>Represents a collection of objects items.</summary>
	/// <include path='items/Collection/*' file='../../LJCDocLib/Common/Collection.xml'/>
	public class ForeignKeys : List<ForeignKey>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ForeignKeys()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ForeignKeys Clone()
		{
			ForeignKeys retValue = new ForeignKeys();
			foreach (ForeignKey item in this)
			{
				retValue.Add(item.Clone());
			}
			return retValue;
		}

		// Get custom collection from List<T>.
		/// <summary>
		/// Get custom collection from List&lt;T&gt;.
		/// </summary>
		/// <param name="list">The list object reference.</param>
		/// <returns>The collection object reference.</returns>
		public ForeignKeys GetCollection(List<ForeignKey> list)
		{
			ForeignKeys retValue = null;

			if (list != null && list.Count > 0)
			{
				retValue = new ForeignKeys();
				foreach (ForeignKey item in list)
				{
					retValue.Add(item);
				}
			}
			return retValue;
		}
		#endregion

		#region Custom Methods

		/// <summary>
		/// Returns a collection of referenced tables.
		/// </summary>
		/// <param name="sourceTableName">The source table name.</param>
		/// <returns>The collection of referenced tables.</returns>
		public ForeignKeys GetForeignKeys(string sourceTableName)
		{
			ForeignKeys retValue = null;

			LJCSortTarget();
			List<ForeignKey> keyList = FindAll(x => x.SourceTable.Equals(sourceTableName));
			retValue = GetCollection(keyList);
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		// Sort on Name.
		/// <summary>
		/// Sort on Name.
		/// </summary>
		public void LJCSortTarget()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		// Retrieve the collection element.
		/// <summary>
		/// Retrieve the collection element.
		/// </summary>
		/// <param name="targetTableName">The target table name.</param>
		/// <param name="targetColumnName">The target column name.</param>
		/// <returns>The target foreign key.</returns>
		public ForeignKey LJCSearchTarget(string targetTableName
			, string targetColumnName)
		{
			ForeignKey retValue = null;

			LJCSortTarget();
			ForeignKey searchItem = new ForeignKey()
			{
				TargetTable = targetTableName,
				TargetColumn = targetColumnName
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
