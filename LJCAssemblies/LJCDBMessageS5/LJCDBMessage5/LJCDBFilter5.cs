// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBFilter.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a filter which is part of a where clause.
  /// <include file='Doc/ProjectDbFilter.xml'
  ///  path='items/LJCDBFilter/*'/>
  public class LJCDBFilter
  {
    #region Properties

    // The filter boolean operator.
    /// <include file='Doc/DbFilter.xml'
    ///  path='items/BooleanOperator/*'/>
    public string? BooleanOperator
    {
      get => mBooleanOperator;
      set => mBooleanOperator = value?.Trim();
    }
    private string? mBooleanOperator;

    // Gets or sets the filter condition set.
    /// <include file='Doc/DbFilter.xml'
    ///  path='items/ConditionSet/*'/>
    public LJCDBConditionSet ConditionSet { get; set; }

    // Gets or sets the contained filters.
    /// <include file='Doc/DbFilter.xml'
    ///  path='items/Filters/*'/>
    public LJCDBFilters? Filters { get; set; }

    // Gets or sets the Name value.
    /// <include file='Doc/DbFilter.xml'
    ///  path='items/Name/*'/>
    public string? Name
    {
      get => mName;
      set => mName = value?.Trim();
    }
    private string? mName;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCDBFilter()
    {
      BooleanOperator = "and";
      ConditionSet = InitConditionSet();
      //Filters = new DbFilters();
      Filters = [];
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
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

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
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
