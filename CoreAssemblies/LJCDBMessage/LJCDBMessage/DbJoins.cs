// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbJoins.cs
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDBMessage
{
  // Represents a collection of table joins.
  /// <include path='items/DbJoins/*' file='Doc/DbJoins.xml'/>
  public class DbJoins : List<DbJoin>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbJoins()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public DbJoins(DbJoins items)
    {
      if (NetCommon.HasItems(items))
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
    /// <include path='items/Add/*' file='Doc/DbJoins.xml'/>
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
