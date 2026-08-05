// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcedureParameters.cs
using System.Collections.Generic;
using System.Data;
using LJCNetCommon;
using MySql.Data.MySqlClient;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDataAccess
{
  // Represents a collection of ProcedureParameter objects.
  /// <include file='Doc/ProcedureParameters.xml'
  ///  path='items/ProcedureParameters/*'/>
  public class ProcedureParameters : List<ProcedureParameter>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public ProcedureParameters()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public ProcedureParameters(ProcedureParameters items)
    {
      if (LJC.HasListItems(items))
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
    /// <include file='Doc/ProcedureParameters.xml'
    ///  path='items/AddSql/*'/>
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
    /// <include file='Doc/ProcedureParameters.xml'
    ///  path='items/AddMySql/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/LJCSearchName/*'/>
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
