// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCProcedureParameters5.cs
using System.Data;
using LJCNetCommon5;

namespace LJCDataAccess5
{
  // Represents a collection of ProcedureParameter objects.
  /// <include file='Doc/ProcedureParameters.xml'
  ///  path='members/LJCProcedureParameters/*'/>
  public class LJCProcedureParameters : List<LJCProcedureParameter>
  {
    #region Class Data

    private int mPrevCount;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCProcedureParameters()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/ConstructorCopy/*'/>
    public LJCProcedureParameters(LJCProcedureParameters? items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCProcedureParameter(item));
        }
      }
    }
    #endregion

    #region Methods

    // Creates and adds the object from the supplied values.
    /// <include file='Doc/ProcedureParameters.xml'
    ///  path='members/Add/*'/>
    public LJCProcedureParameter Add(string parameterName
      , int sqlDbTypeID, int mySqlDbTypeID, int size, object? value = null
      , ParameterDirection direction = ParameterDirection.Input)
    {
      var retValue = new LJCProcedureParameter()
      {
        ParameterName = parameterName,
        SqlDbTypeID = sqlDbTypeID,
        MySqlDbTypeID = mySqlDbTypeID,
        Size = size,
        Value = value,
        Direction = direction
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the collection element by name.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCSearchName/*'/>
    public LJCProcedureParameter? LJCSearchName(string name)
    {
      LJCProcedureParameter? retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      var searchItem = new LJCProcedureParameter()
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
  }
}
