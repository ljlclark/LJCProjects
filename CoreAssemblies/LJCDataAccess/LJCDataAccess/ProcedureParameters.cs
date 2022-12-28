// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProcedureParameters.cs
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace LJCDataAccess
{
  // Represents a collection of ProcedureParameter objects.
  /// <include path='items/ProcedureParameters/*' file='Doc/ProcedureParameters.xml'/>
  public class ProcedureParameters : List<ProcedureParameter>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(ProcedureParameters collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ProcedureParameters()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public ProcedureParameters(ProcedureParameters items)
    {
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new ProcedureParameter(item));
        }
      }
    }
    #endregion

    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/AddSql/*' file='Doc/ProcedureParameters.xml'/>
    public ProcedureParameter Add(string parameterName, SqlDbType sqlDbType, int size
      , object value = null, ParameterDirection direction = ParameterDirection.Input)
    {
      ProcedureParameter retValue = new ProcedureParameter()
      {
        ParameterName = parameterName,
        SqlDbType = sqlDbType,
        Size = size,
        Value = value,
        Direction = direction
      };
      Add(retValue);
      return retValue;
    }

    // Creates and adds the object from the provided values.
    /// <include path='items/AddMySql/*' file='Doc/ProcedureParameters.xml'/>
    public ProcedureParameter Add(string parameterName, MySqlDbType mySqlDbType, int size
      , object value = null, ParameterDirection direction = ParameterDirection.Input)
    {
      ProcedureParameter retValue = new ProcedureParameter()
      {
        ParameterName = parameterName,
        MySqlDbType = mySqlDbType,
        Size = size,
        Value = value,
        Direction = direction
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the collection element by name.
    /// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public ProcedureParameter LJCSearchName(string name)
    {
      ProcedureParameter retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      ProcedureParameter searchItem = new ProcedureParameter()
      {
        ParameterName = name
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
