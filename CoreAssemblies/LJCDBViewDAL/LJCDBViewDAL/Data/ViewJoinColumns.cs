// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinColumns.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	// <summary>Represents a collection of object items.</summary>
	/// <include path='items/Collection/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
	public class ViewJoinColumns : List<ViewJoinColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ViewJoinColumns()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewJoinColumn Add(int id, string name)
		{
			ViewJoinColumn retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchName(name);
				if (null == retValue)
				{
					retValue = new ViewJoinColumn()
					{
						ID = id,
						ColumnName = name
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Get custom collection from List<T>.
		/// <include path='items/GetCollection/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewJoinColumns GetCollection(List<ViewJoinColumn> list)
		{
			ViewJoinColumns retValue = null;

			if (NetCommon.HasItems(list))
			{
				retValue = new ViewJoinColumns();
				foreach (ViewJoinColumn item in list)
				{
					retValue.Add(item);
				}
			}
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		// Sort on Name.
		/// <include path='items/LJCSortName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public void LJCSortName()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewJoinColumn LJCSearchName(string name)
		{
			ViewJoinColumn retValue = null;

			LJCSortName();
			ViewJoinColumn searchItem = new ViewJoinColumn()
			{
				ColumnName = name
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the last error text value.</summary>
		public string LJCErrorText
		{
			get { return mErrorText; }
			set { mErrorText = NetString.InitString(value); }
		}
		private string mErrorText;

		/// <summary>Returns true if there is an ErrorText value.</summary>
		public bool LJCIsError
		{
			get { return NetString.HasValue(LJCErrorText); }
		}

		/// <summary>Returns true if there is no ErrorText.</summary>
		public bool LJCIsSuccess
		{
			get { return !NetString.HasValue(LJCErrorText); }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}

