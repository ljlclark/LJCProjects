// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbJoinOns.cs
using System.Collections.Generic;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDBMessage
{
  // Represents a collection of join on definitions.
  /// <include path='items/DbJoinOns/*' file='Doc/DbJoinOns.xml'/>
  public class DbJoinOns : List<DbJoinOn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbJoinOns()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbJoinOns(DbJoinOns items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbJoinOn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    /// <include file='Doc/DbJoinOns.xml'
    ///  path='items/Add/*'/>
    public DbJoinOn Add(string fromColumnName, string toColumnName
      , string joinOperator = "=")
    {
      DbJoinOn retValue;

      retValue = new DbJoinOn()
      {
        FromColumnName = fromColumnName,
        ToColumnName = toColumnName,
        JoinOnOperator = joinOperator
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbJoinOns Clone()
    {
      DbJoinOns retValue = new DbJoinOns();
      foreach (DbJoinOn item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }
    #endregion
  }
}
