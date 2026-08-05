// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbJoins.cs
using System.Collections.Generic;
using LJCNetCommon;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDBMessage
{
  // Represents a collection of table joins.
  /// <include file='Doc/DbJoins.xml'
  ///  path='items/DbJoins/*'/>
  public class DbJoins : List<DbJoin>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbJoins()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbJoins(DbJoins items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbJoin(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates the element from the supplied values and adds it to the collection.
    /// <include file='Doc/DbJoins.xml'
    ///  path='items/Add/*'/>
    public DbJoin Add(string tableName, string tableAlias = null
      , string fromJoinOnColumn = null, string toJoinOnColumn = null)
    {
      DbJoin retValue = new DbJoin()
      {
        TableName = tableName,
        TableAlias = tableAlias
      };
      if (NetString.HasValue(fromJoinOnColumn)
        && NetString.HasValue(toJoinOnColumn))
      {
        retValue.JoinOns.Add(fromJoinOnColumn, toJoinOnColumn);
      }
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbJoins Clone()
    {
      DbJoins retValue = new DbJoins();
      foreach (DbJoin item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }
    #endregion
  }
}
