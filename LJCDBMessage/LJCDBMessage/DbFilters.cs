// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbFilters.cs
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDBMessage
{
  // Represents a collection of DbFilter objects.
  /// <include path='items/DbFilters/*' file='Doc/DbFilters.xml'/>
  public class DbFilters : List<DbFilter>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(DbFilters collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Creates the SQL Soundex filters from the supplied values.
    /// <include path='items/SQLSoundexFilters/*' file='Doc/DbFilters.xml'/>
    public static DbFilters SQLSoundexFilters(string columnName
      , string searchValue, string soundexColumn = null)
    {
      DbFilters retValue = new DbFilters();

      var conditions = retValue.Add(name: "SQLSoundexFilter");
      var value1 = $"Soundex({columnName})";
      var value2 = $"Soundex('{searchValue}')";
      if (NetString.HasValue(soundexColumn))
      {
        value1 = soundexColumn;
      }
      conditions.Add(value1, value2);
      return retValue;
    }

    // Creates the Soundex filters from the supplied values.
    /// <include path='items/SoundexFilters/*' file='Doc/DbFilters.xml'/>
    public static DbFilters SoundexFilters(string pColumnName, string pSearchValue
      , string lColumnName = null, string lSearchValue = null)
    {
      DbFilters retValue = new DbFilters();
      //var filter = new DbFilter();
      //filter.ConditionSet.BooleanOperator = "or";
      //var conditions = filter.ConditionSet.Conditions;
      var conditions = retValue.Add("and", "or", "SoundexFilter");

      string soundex;
      if (NetString.HasValue(pColumnName)
        && NetString.HasValue(pSearchValue))
      {
        soundex = NetString.CreatePSoundex(pSearchValue);
        conditions.Add(pColumnName, $"'{soundex}%'", "like");
      }
      if (NetString.HasValue(lColumnName)
        && NetString.HasValue(lSearchValue))
      {
        soundex = NetString.CreateLSoundex(lSearchValue);
        conditions.Add(lColumnName, $"'{soundex}%'", "like");
      }
      //retValue.Add(filter);
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbFiltersC/*' file='Doc/DbFilters.xml'/>
    public DbFilters()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DbFilters(DbFilters items)
    {
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbFilter(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    /// <include path='items/Add1/*' file='Doc/DbFilters.xml'/>
    public DbConditions Add(string filterBooleanOperator = "and"
      , string conditionSetBooleanOperator = "and", string name = "Filter")
    {
      DbConditions retValue;

      var filter = new DbFilter()
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
    public DbFilter Add(string name, DbConditionSet dbConditionSet, DbFilters dbFilters = null
      , string booleanOperator = "and")
    {
      DbFilter retValue = new DbFilter()
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
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbFilters Clone()
    {
      DbFilters retValue = new DbFilters();
      foreach (DbFilter item in this)
      {
        retValue.Add(item.Clone());
      }
      return retValue;
    }
    #endregion
  }
}
