// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBConditionSet.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents the conditions and properties.
  /// <include path='items/DbConditionSet/*' file='Doc/DbConditionSet.xml'/>
  public class LJCDBConditionSet
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbConditionSetC/*' file='Doc/DbConditionSet.xml'/>
    public LJCDBConditionSet()
    {
      //Conditions = new DbConditions();
      Conditions = [];
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBConditionSet(LJCDBConditionSet item)
    {
      BooleanOperator = item.BooleanOperator;
      //Conditions = new DbConditions(item.Conditions);
      Conditions = [.. item.Conditions];
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='Doc/DbConditionSet.xml'/>
    public LJCDBConditionSet? Clone()
    {
      LJCDBConditionSet? retValue = MemberwiseClone() as LJCDBConditionSet;
      if (retValue != null)
      {
        retValue.Conditions = Conditions.Clone();
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The conditions boolean operator.</summary>
    public string? BooleanOperator
    {
      get => mBooleanOperator;
      set => mBooleanOperator = LJCNetString.InitString(value);
    }
    private string? mBooleanOperator;

    /// <summary>Gets or sets the conditions.</summary>
    public LJCDBConditions Conditions { get; set; }
  }
  #endregion
}
