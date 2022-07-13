// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Represents a filter condition.</summary>
  public class DbCondition
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbCondition()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbCondition(DbCondition item)
    {
      ComparisonOperator = item.ComparisonOperator;
      FirstValue = item.FirstValue;
      SecondValue = item.SecondValue;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbCondition Clone()
    {
      DbCondition retValue = MemberwiseClone() as DbCondition;
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The comparison operator.</summary>
    public string ComparisonOperator
    {
      get
      {
        return mComparisonOperator;
      }
      set
      {
        mComparisonOperator = NetString.InitString(value);
      }
    }
    private string mComparisonOperator;

    /// <summary>The first data value.</summary>
    public string FirstValue
    {
      get
      {
        return mFirstValue;
      }
      set
      {
        mFirstValue = NetString.InitString(value);
      }
    }
    private string mFirstValue;

    /// <summary>The second data value.</summary>
    public string SecondValue
    {
      get
      {
        return mSecondValue;
      }
      set
      {
        mSecondValue = NetString.InitString(value);
      }
    }
    private string mSecondValue;
    #endregion
  }
}
