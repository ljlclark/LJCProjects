// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;

namespace LJCSQLUtilLibDAL
{
	/// <summary>Represents a collection of DbMetaDataKey objects.</summary>
	public class DbMetaDataKeys : List<DbMetaDataKey>
	{
		#region Collection Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/DbMetaDataKeys.xml'/>
		public DbMetaDataKey Add(int id, int columnID, int keyTypeID)
		{
			DbMetaDataKey retValue = new DbMetaDataKey()
			{
				ID = id,
				DbMetaDataColumnID = columnID,
				DbMetaDataKeyTypeID = keyTypeID
			};
			Add(retValue);
			return retValue;
		}
		#endregion

		#region Class Data

		//private int mPrevCount;
		#endregion
	}
}
