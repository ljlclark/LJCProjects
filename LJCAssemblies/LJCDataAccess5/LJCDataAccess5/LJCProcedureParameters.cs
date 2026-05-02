// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcedureParameters.cs
using System.Data;
using LJCNetCommon5;

namespace LJCDataAccess5
{
  // Represents a collection of ProcedureParameter objects.
  /// <include path='items/ProcedureParameters/*' file='Doc/ProcedureParameters.xml'/>
  public class LJCProcedureParameters : List<LJCProcedureParameter>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCProcedureParameters()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public LJCProcedureParameters(LJCProcedureParameters? items)
    {
      if (LJC.HasItems(items))
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
    /// <include path='items/AddMySql/*' file='Doc/ProcedureParameters.xml'/>
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
    /// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
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

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
