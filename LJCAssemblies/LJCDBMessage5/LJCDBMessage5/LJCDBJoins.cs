// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoins.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of table joins.
  /// <include file='Doc/DbJoins.xml'
  ///  path='items/DbJoins/*'/>
  public class LJCDBJoins : List<LJCDBJoin>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCDBJoins()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public LJCDBJoins(LJCDBJoins? items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDBJoin(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates the element from the supplied values and adds it to the collection.
    /// <include file='Doc/DbJoins.xml'
    ///  path='items/Add/*'/>
    public LJCDBJoin Add(string tableName, string? tableAlias = null
      , string? fromJoinOnColumn = null, string? toJoinOnColumn = null)
    {
      var retValue = new LJCDBJoin()
      {
        TableName = tableName,
        TableAlias = tableAlias
      };
      if (LJC.HasText(fromJoinOnColumn)
        && LJC.HasText(toJoinOnColumn))
      {
        retValue.JoinOns.Add(fromJoinOnColumn, toJoinOnColumn);
      }
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCDBJoins Clone()
    {
      var retValue = new LJCDBJoins();
      foreach (LJCDBJoin item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }
    #endregion
  }
}
