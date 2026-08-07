// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoinOns.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of join on definitions.
  /// <include file='Doc/DbJoinOns.xml'
  ///  path='items/DbJoinOns/*'/>
  public class LJCDBJoinOns : List<LJCDBJoinOn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCDBJoinOns()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public LJCDBJoinOns(LJCDBJoinOns items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDBJoinOn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    /// <include file='Doc/DbJoinOns.xml'
    ///  path='items/Add/*'/>
    public LJCDBJoinOn Add(string fromColumnName, string toColumnName
      , string joinOperator = "=")
    {
      LJCDBJoinOn retValue;

      retValue = new LJCDBJoinOn()
      {
        FromColumnName = fromColumnName,
        ToColumnName = toColumnName,
        JoinOnOperator = joinOperator
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCDBJoinOns Clone()
    {
      var retValue = new LJCDBJoinOns();
      foreach (LJCDBJoinOn item in this)
      {
        var clone = item.Clone();
        if (clone != null)
        {
          retValue.Add(clone);
        }
      }
      return retValue;
    }
    #endregion
  }
}
