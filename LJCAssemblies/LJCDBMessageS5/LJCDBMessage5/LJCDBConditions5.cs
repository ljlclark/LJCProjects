// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBConditions.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of DbCondition objects.
  /// <include path='items/DbConditions/*' file='Doc/DbConditions.xml'/>
  public class LJCDBConditions : List<LJCDBCondition>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCDBConditions()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public LJCDBConditions(LJCDBConditions items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDBCondition(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    /// <include file='Doc/DbConditions.xml'
    ///  path='items/Add/*'/>
    public LJCDBCondition Add(string value1, string value2, string comparisonOperator = "=")
    {
      var retValue = new LJCDBCondition()
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
    public LJCDBConditions Clone()
    {
      var retValue = new LJCDBConditions();
      foreach (LJCDBCondition dbCondition in this)
      {
        var clone = dbCondition.Clone();
        if (clone != null)
        {
          retValue.Add(clone);
        }
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
