// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBCondition.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a filter condition.
  /// <include file='Doc/DbCondition.xml'
  ///  path='members/LJCDBCondition/*'/>
  public class LJCDBCondition
  {
    #region Properties

    // The comparison operator.
    /// <include file='Doc/DbCondition.xml'
    ///  path='members/ComparisonOperator/*'/>
    public string? ComparisonOperator
    {
      get => mComparisonOperator;
      set => mComparisonOperator = value?.Trim();
    }
    private string? mComparisonOperator;

    // The first data value.
    /// <include file='Doc/DbCondition.xml'
    ///  path='members/FirstValue/*'/>
    public string? FirstValue
    {
      get => mFirstValue;
      set => mFirstValue = value?.Trim();
    }
    private string? mFirstValue;

    // The second data value.
    /// <include file='Doc/DbCondition.xml'
    ///  path='members/SecondValue/*'/>
    public string? SecondValue
    {
      get => mSecondValue;
      set => mSecondValue = value?.Trim();
    }
    private string? mSecondValue;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDBCondition()
    {
    }

    // The Copy constructor.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/ConstructorCopy/*'/>
    public LJCDBCondition(LJCDBCondition item)
    {
      ComparisonOperator = item.ComparisonOperator;
      FirstValue = item.FirstValue;
      SecondValue = item.SecondValue;
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDBCondition? Clone()
    {
      LJCDBCondition? retValue = MemberwiseClone() as LJCDBCondition;
      return retValue;
    }
    #endregion
  }
}
