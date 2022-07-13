// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Represents a filter which is part of a where clause.</summary>
  public class DbFilter
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
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
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
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
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
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
