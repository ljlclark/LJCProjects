// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBFilter.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  /// <summary>Represents a filter which is part of a where clause.</summary>
  public class LJCDBFilter
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBFilter()
    {
      BooleanOperator = "and";
      ConditionSet = InitConditionSet();
      //Filters = new DbFilters();
      Filters = [];
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
    private LJCDBConditionSet InitConditionSet()
    {
      var retValue = new LJCDBConditionSet
      {
        BooleanOperator = "and",
        //Conditions = new DbConditions() { }
        Conditions = []
      };
      return retValue;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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

    #region Properties

    /// <summary>The filter boolean operator.</summary>
    public string? BooleanOperator
    {
      get => mBooleanOperator;
      set => mBooleanOperator = LJCNetString.InitString(value);
    }
    private string? mBooleanOperator;

    /// <summary>Gets or sets the filter condition set.</summary>
    public LJCDBConditionSet ConditionSet { get; set; }

    /// <summary>Gets or sets the contained filters.</summary>
    public LJCDBFilters? Filters { get; set; }

    /// <summary>Gets or sets the Name value.</summary>
    public string? Name
    {
      get => mName;
      set => mName = LJCNetString.InitString(value);
    }
    private string? mName;
    #endregion
  }
}
