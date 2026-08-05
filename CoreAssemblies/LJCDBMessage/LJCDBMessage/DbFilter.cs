// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbFilter.cs
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Represents a filter which is part of a where clause.</summary>
  public class DbFilter
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbFilter()
    {
      BooleanOperator = "and";
      ConditionSet = new DbConditionSet
      {
        BooleanOperator = "and",
        Conditions = new DbConditions()
        {
        }
      };
      Filters = new DbFilters();
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbFilter(DbFilter item)
    {
      BooleanOperator = item.BooleanOperator;
      ConditionSet = new DbConditionSet(item.ConditionSet);
      Filters = new DbFilters(item.Filters);
      Name = item.Name;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbFilter Clone()
    {
      DbFilter retValue = MemberwiseClone() as DbFilter;
      retValue.ConditionSet = ConditionSet.Clone();
      retValue.Filters = Filters.Clone();
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The filter boolean operator.</summary>
    public string BooleanOperator
    {
      get { return mBooleanOperator; }
      set { mBooleanOperator = NetString.InitString(value); }
    }
    private string mBooleanOperator;

    /// <summary>Gets or sets the filter condition set.</summary>
    public DbConditionSet ConditionSet { get; set; }

    /// <summary>Gets or sets the contained filters.</summary>
    public DbFilters Filters { get; set; }

    /// <summary>Gets or sets the Name value.</summary>
    public string Name
    {
      get { return mName; }
      set { mName = NetString.InitString(value); }
    }
    private string mName;
    #endregion
  }
}
