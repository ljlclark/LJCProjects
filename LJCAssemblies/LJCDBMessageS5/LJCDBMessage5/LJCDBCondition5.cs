// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBCondition.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a filter condition.
  /// <include file='Doc/ProjectDbCondition.xml'
  ///  path='items/LJCDBCondition/*'/>
  public class LJCDBCondition
  {
    #region Properties

    // The comparison operator.
    /// <include file='Doc/DbCondition.xml'
    ///  path='items/ComparisonOperator/*'/>
    public string? ComparisonOperator
    {
      get => mComparisonOperator;
      set => mComparisonOperator = value?.Trim();
    }
    private string? mComparisonOperator;

    // The first data value.
    /// <include file='Doc/DbCondition.xml'
    ///  path='items/FirstValue/*'/>
    public string? FirstValue
    {
      get => mFirstValue;
      set => mFirstValue = value?.Trim();
    }
    private string? mFirstValue;

    // The second data value.
    /// <include file='Doc/DbCondition.xml'
    ///  path='items/SecondValue/*'/>
    public string? SecondValue
    {
      get => mSecondValue;
      set => mSecondValue = value?.Trim();
    }
    private string? mSecondValue;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCDBCondition()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
    public LJCDBCondition(LJCDBCondition item)
    {
      ComparisonOperator = item.ComparisonOperator;
      FirstValue = item.FirstValue;
      SecondValue = item.SecondValue;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCDBCondition? Clone()
    {
      LJCDBCondition? retValue = MemberwiseClone() as LJCDBCondition;
      return retValue;
    }
    #endregion
  }
}
