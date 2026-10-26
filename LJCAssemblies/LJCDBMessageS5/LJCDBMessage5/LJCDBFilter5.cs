// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBFilter.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a filter which is part of a where clause.
  /// <include file='Doc/DbFilter.xml'
  ///  path='members/LJCDBFilter/*'/>
  public class LJCDBFilter
  {
    #region Properties

    // The filter boolean operator.
    /// <include file='Doc/DbFilter.xml'
    ///  path='members/BooleanOperator/*'/>
    public string? BooleanOperator
    {
      get => mBooleanOperator;
      set => mBooleanOperator = value?.Trim();
    }
    private string? mBooleanOperator;

    // Gets or sets the filter condition set.
    /// <include file='Doc/DbFilter.xml'
    ///  path='members/ConditionSet/*'/>
    public LJCDBConditionSet ConditionSet { get; set; }

    // Gets or sets the contained filters.
    /// <include file='Doc/DbFilter.xml'
    ///  path='members/Filters/*'/>
    public LJCDBFilters? Filters { get; set; }

    // Gets or sets the Name value.
    /// <include file='Doc/DbFilter.xml'
    ///  path='members/Name/*'/>
    public string? Name
    {
      get => mName;
      set => mName = value?.Trim();
    }
    private string? mName;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDBFilter()
    {
      BooleanOperator = "and";
      ConditionSet = InitConditionSet();
      //Filters = new DbFilters();
      Filters = [];
    }

    // The Copy constructor.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCDBFilter(LJCDBFilter item)
    {
      BooleanOperator = item.BooleanOperator;
      ConditionSet = InitConditionSet();
      //Filters = new DbFilters();
      Filters = [];
      if (item != null)
      {
        if (item.ConditionSet != null)
        {
          ConditionSet = new LJCDBConditionSet(item.ConditionSet);
        }
        if (item.Filters != null)
        {
          //Filters = new DbFilters(item.Filters);
          Filters = [.. item.Filters];
        }
        Name = item.Name;
      }
    }

    // Sets condition set initial values.
    private static LJCDBConditionSet InitConditionSet()
    {
      var retValue = new LJCDBConditionSet
      {
        BooleanOperator = "and",
        Conditions = []
      };
      return retValue;
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDBFilter? Clone()
    {
      LJCDBFilter? retValue = MemberwiseClone() as LJCDBFilter;
      if (retValue != null)
      {
        if (ConditionSet != null)
        {
          var conditionSet = ConditionSet.Clone();
          if (conditionSet != null)
          {
            retValue.ConditionSet = conditionSet;
          }
        }
        retValue.Filters = Filters?.Clone();
      }
      return retValue;
    }
    #endregion
  }
}
