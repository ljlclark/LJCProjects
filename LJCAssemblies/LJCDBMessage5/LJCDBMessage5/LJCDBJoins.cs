// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoins.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of table joins.
  /// <include path='items/DbJoins/*' file='Doc/DbJoins.xml'/>
  public class LJCDBJoins : List<LJCDBJoin>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoins()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public LJCDBJoins(LJCDBJoins? items)
    {
      if (LJC.HasItems(items))
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
    /// <include path='items/Add/*' file='Doc/DbJoins.xml'/>
    public LJCDBJoin Add(string tableName, string? tableAlias = null
      , string? fromJoinOnColumn = null, string? toJoinOnColumn = null)
    {
      var retValue = new LJCDBJoin()
      {
        TableName = tableName,
        TableAlias = tableAlias
      };
      if (LJC.HasValue(fromJoinOnColumn)
        && LJC.HasValue(toJoinOnColumn))
      {
        retValue.JoinOns.Add(fromJoinOnColumn, toJoinOnColumn);
      }
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
