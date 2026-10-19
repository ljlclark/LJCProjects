// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBCondition.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  /// <summary>Represents a filter condition.</summary>
  public class LJCDBCondition
  {
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

    #region Properties

    /// <summary>The comparison operator.</summary>
    public string? ComparisonOperator
    {
      get => mComparisonOperator;
      set => mComparisonOperator = LJCNetString.InitString(value);
    }
    private string? mComparisonOperator;

    /// <summary>The first data value.</summary>
    public string? FirstValue
    {
      get => mFirstValue;
      set => mFirstValue = LJCNetString.InitString(value);
    }
    private string? mFirstValue;

    /// <summary>The second data value.</summary>
    public string? SecondValue
    {
      get => mSecondValue;
      set => mSecondValue = LJCNetString.InitString(value);
    }
    private string? mSecondValue;
    #endregion
  }
}
