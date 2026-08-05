// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbConditions.cs
using System.Collections.Generic;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDBMessage
{
  // Represents a collection of DbCondition objects.
  /// <include path='items/DbConditions/*' file='Doc/DbConditions.xml'/>
  public class DbConditions : List<DbCondition>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbConditions()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbConditions(DbConditions items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbCondition(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    /// <include file='Doc/DbConditions.xml'
    ///  path='items/Add/*'/>
    public DbCondition Add(string value1, string value2, string comparisonOperator = "=")
    {
      DbCondition retValue = new DbCondition()
      {
        FirstValue = value1,
        ComparisonOperator = comparisonOperator,
        SecondValue = value2
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbConditions Clone()
    {
      var retValue = new DbConditions();
      foreach (DbCondition dbCondition in this)
      {
        retValue.Add(dbCondition.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/HasItems2/*'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion
  }
}
