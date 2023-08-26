// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbConditions.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDBMessage
{
  // Represents a collection of DbCondition objects.
  /// <include path='items/DbConditions/*' file='Doc/DbConditions.xml'/>
  public class DbConditions : List<DbCondition>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public DbConditions()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public DbConditions(DbConditions items)
    {
      if (NetCommon.HasItems(items))
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
    /// <include path='items/Add/*' file='Doc/DbConditions.xml'/>
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
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
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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
