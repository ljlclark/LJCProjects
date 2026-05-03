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
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBCondition()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBCondition(LJCDBCondition item)
    {
      ComparisonOperator = item.ComparisonOperator;
      FirstValue = item.FirstValue;
      SecondValue = item.SecondValue;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
