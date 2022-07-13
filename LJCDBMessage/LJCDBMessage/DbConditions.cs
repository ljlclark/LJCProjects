// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System.Collections.Generic;

namespace LJCDBMessage
{
  // Represents a collection of DbCondition objects.
  /// <include path='items/DbConditions/*' file='Doc/DbConditions.xml'/>
  public class DbConditions : List<DbCondition>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(DbConditions collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbConditions()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DbConditions(DbConditions items)
    {
      if (HasItems(items))
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
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbConditions Clone()
    {
      var retValue = MemberwiseClone() as DbConditions;
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCDocLib/Common/Collection.xml'/>
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