// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCKeyItem.cs

namespace LJCNetCommon5
{
  // Represents Key item values.
  /// <include path="items/LJCKeyItem/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
  public class LJCKeyItem : IComparable<LJCKeyItem>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="items/DefaultConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public LJCKeyItem()
    {
    }

    // Initializes an object instance.
    /// <include path="items/KeyItem/*" file="Doc/LJCKeyItem.xml"/>
    public LJCKeyItem(string propertyName, long id, string description)
    {
      PropertyName = propertyName;
      ID = id;
      Description = description;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path="items/Clone/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public LJCKeyItem? Clone()
    {
      LJCKeyItem retValue = MemberwiseClone() as LJCKeyItem;
      return retValue;
    }

    // The object string identifier.
    /// <include path="items/ToString/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public override string? ToString()
    {
      return Description;
    }
    #endregion

    #region Search and Sort Methods

    // Provides the default Sort functionality.
    /// <include path="items/CompareTo/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
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

    #region Properties

    // Gets or sets the DataConfig name.
    /// <include path="items/DataConfigName/*" file="Doc/LJCKeyItem.xml"/>
    public string? DataConfigName
    {
      get { return mDataConfigName; }
      set { mDataConfigName = LJCNetString.InitString(value); }
    }
    private string? mDataConfigName;

    // The Description value.
    /// <include path="items/Description/*" file="Doc/LJCKeyItem.xml"/>
    public string? Description
    {
      get { return mDescription; }
      set { mDescription = LJCNetString.InitString(value); }
    }
    private string? mDescription;

    // The ID value.
    /// <include path="items/ID/*" file="Doc/LJCKeyItem.xml"/>
    public long ID { get; set; }

    // Gets or sets the MaxLength value.
    /// <include path="items/MaxLength/*" file="Doc/LJCKeyItem.xml"/>
    public int MaxLength { get; set; }

    // Gets or sets the Primary Key name.
    /// <include path="items/PrimaryKeyName/*" file="Doc/LJCKeyItem.xml"/>
    public string? PrimaryKeyName
    {
      get { return mPrimaryKeyName; }
      set { mPrimaryKeyName = LJCNetString.InitString(value); }
    }
    private string? mPrimaryKeyName;

    // The Property name.
    /// <include path="items/PropertyName/*" file="Doc/LJCKeyItem.xml"/>
    public string? PropertyName
    {
      get { return mPropertyName; }
      set { mPropertyName = LJCNetString.InitString(value); }
    }
    private string? mPropertyName;

    // Gets or sets the Table name.
    /// <include path="items/TableName/*" file="Doc/LJCKeyItem.xml"/>
    public string? TableName
    {
      get { return mTableName; }
      set { mTableName = LJCNetString.InitString(value); }
    }
    private string? mTableName;
    #endregion
  }
}
