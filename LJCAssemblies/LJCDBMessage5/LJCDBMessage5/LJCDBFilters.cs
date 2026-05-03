// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBFilters.cs
using System.Collections.Generic;
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of DbFilter objects.
  /// <include path='items/DbFilters/*' file='Doc/DbFilters.xml'/>
  public class LJCDBFilters : List<LJCDBFilter>
  {
    #region Static Methods

    // Creates the SQL Soundex filters from the supplied values.
    /// <include path='items/SQLSoundexFilters/*' file='Doc/DbFilters.xml'/>
    public static LJCDBFilters SQLSoundexFilters(string columnName
      , string searchValue, string? soundexColumn = null)
    {
      var retValue = new LJCDBFilters();

      var conditions = retValue.Add(name: "SQLSoundexFilter");
      var value1 = $"Soundex({columnName})";
      var value2 = $"Soundex('{searchValue}')";
      if (LJC.HasValue(soundexColumn))
      {
        value1 = soundexColumn;
      }
      conditions.Add(value1, value2);
      return retValue;
    }

    // Creates the Soundex filters from the supplied values.
    /// <include path='items/SoundexFilters/*' file='Doc/DbFilters.xml'/>
    public static LJCDBFilters SoundexFilters(string pColumnName, string pSearchValue
      , string? lColumnName = null, string? lSearchValue = null)
    {
      LJCDBFilters retValue = new LJCDBFilters();
      var conditions = retValue.Add("and", "or", "SoundexFilter");

      string? soundex;
      if (LJC.HasValue(pColumnName)
        && LJC.HasValue(pSearchValue))
      {
        soundex = LJCNetString.CreatePSoundex(pSearchValue);
        conditions.Add(pColumnName, $"'{soundex}%'", "like");
      }
      if (LJC.HasValue(lColumnName)
        && LJC.HasValue(lSearchValue))
      {
        soundex = LJCNetString.CreateLSoundex(lSearchValue);
        conditions.Add(lColumnName, $"'{soundex}%'", "like");
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbFiltersC/*' file='Doc/DbFilters.xml'/>
    public LJCDBFilters()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public LJCDBFilters(LJCDBFilters? items)
    {
      if (LJC.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDBFilter(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    /// <include path='items/Add1/*' file='Doc/DbFilters.xml'/>
    public LJCDBConditions Add(string filterBooleanOperator = "and"
      , string conditionSetBooleanOperator = "and", string name = "Filter")
    {
      LJCDBConditions retValue;

      var filter = new LJCDBFilter()
      {
        Name = name,
        BooleanOperator = filterBooleanOperator
      };
      filter.ConditionSet.BooleanOperator = conditionSetBooleanOperator;
      Add(filter);
      retValue = filter.ConditionSet.Conditions;
      return retValue;
    }

    // Creates and adds the element from the supplied values.
    /// <include path='items/Add2/*' file='Doc/DbFilters.xml'/>
    public LJCDBFilter Add(string name, LJCDBConditionSet dbConditionSet
      , LJCDBFilters? dbFilters = null, string? booleanOperator = "and")
    {
      LJCDBFilter retValue = new LJCDBFilter()
      {
        Name = name,
        ConditionSet = dbConditionSet,
        Filters = dbFilters,
        BooleanOperator = booleanOperator
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBFilters Clone()
    {
      LJCDBFilters retValue = new LJCDBFilters();
      foreach (LJCDBFilter item in this)
      {
        var filter = item.Clone();
        if (filter != null)
        {
          retValue.Add(filter);
        }
      }
      return retValue;
    }
    #endregion
  }
}
