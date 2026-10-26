// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCKeyItem.cs

namespace LJCNetCommon5
{
  // Represents Key item values.
  /// <include file='Doc/KeyItem.xml'
  ///  path='items/LJCKeyItem/*'/>
  public class LJCKeyItem : IComparable<LJCKeyItem>
  {
    #region Properties

    // Gets or sets the DataConfig name.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/DataConfigName/*'/>
    public string? DataConfigName
    {
      get => mDataConfigName;
      set => mDataConfigName = value?.Trim();
    }
    private string? mDataConfigName;

    // The Description value.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/Description/*'/>
    public string? Description
    {
      get => mDescription;
      set => mDescription = value?.Trim();
    }
    private string? mDescription;

    // The ID value.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/ID/*'/>
    public long ID { get; set; }

    // Gets or sets the MaxLength value.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/MaxLength/*'/>
    public int MaxLength { get; set; }

    // Gets or sets the Primary Key name.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/PrimaryKeyName/*'/>
    public string? PrimaryKeyName
    {
      get => mPrimaryKeyName;
      set => mPrimaryKeyName = value?.Trim();
    }
    private string? mPrimaryKeyName;

    // The Property name.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/PropertyName/*'/>
    public string? PropertyName
    {
      get => mPropertyName;
      set => mPropertyName = value?.Trim();
    }
    private string? mPropertyName;

    // Gets or sets the Table name.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/TableName/*'/>
    public string? TableName
    {
      get => mTableName;
      set => mTableName = value?.Trim();
    }
    private string? mTableName;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCKeyItem()
    {
    }

    // Initializes an object instance.
    /// <include file='Doc/KeyItem.xml'
    ///  path='members/ConstructorParam/*'/>
    public LJCKeyItem(string propertyName, long id, string description) : this()
    {
      PropertyName = propertyName;
      ID = id;
      Description = description;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCKeyItem? Clone()
    {
      LJCKeyItem retValue = MemberwiseClone() as LJCKeyItem;
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public override string? ToString()
    {
      return Description;
    }
    #endregion

    #region Search and Sort Methods

    // Provides the default Sort functionality.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(LJCKeyItem? other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        // Not case sensitive.
        retValue = string.Compare(PropertyName, other.PropertyName, true);
      }
      return retValue;
    }
    #endregion
  }
}
