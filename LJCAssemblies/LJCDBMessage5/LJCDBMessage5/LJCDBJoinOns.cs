// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoinOns.cs
using LJCNetCommon5;
using System.Collections.Generic;

namespace LJCDBMessage5
{
  // Represents a collection of join on definitions.
  /// <include path='items/DbJoinOns/*' file='Doc/DbJoinOns.xml'/>
  public class LJCDBJoinOns : List<LJCDBJoinOn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoinOns()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public LJCDBJoinOns(LJCDBJoinOns items)
    {
      if (LJC.HasItems(items))
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
    /// <include path='items/Add/*' file='Doc/DbJoinOns.xml'/>
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
