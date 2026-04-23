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
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBConditions()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public LJCDBConditions(LJCDBConditions items)
    {
      if (LJC.HasItems(items))
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
    /// <include path='items/Add/*' file='Doc/DbConditions.xml'/>
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
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
