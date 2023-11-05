// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbJoinOns.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDBMessage
{
  // Represents a collection of join on definitions.
  /// <include path='items/DbJoinOns/*' file='Doc/DbJoinOns.xml'/>
  public class DbJoinOns : List<DbJoinOn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbJoinOns()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public DbJoinOns(DbJoinOns items)
    {
      if (NetCommon.HasItems(items))
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
    /// <include path='items/Add/*' file='Doc/DbJoinOns.xml'/>
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
