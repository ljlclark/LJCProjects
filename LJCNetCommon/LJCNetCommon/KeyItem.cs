// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;

namespace LJCNetCommon
{
  /// <summary>Represents Key item values.</summary>
  public class KeyItem : IComparable<KeyItem>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public KeyItem()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public KeyItem(KeyItem item)
    {
      DataConfigName = item.DataConfigName;
      Description = Description;
      ID = ID;
      MaxLength = MaxLength;
      PrimaryKeyName = PrimaryKeyName;
      PropertyName = PropertyName;
      TableName = TableName;
    }

    // Initializes an object instance.
    /// <include path='items/KeyItem/*' file='Doc/KeyItem.xml'/>
    public KeyItem(string propertyName, long id, string description)
    {
      PropertyName = propertyName;
      ID = id;
      Description = description;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public KeyItem Clone()
    {
      KeyItem retValue = MemberwiseClone() as KeyItem;
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return Description;
    }
    #endregion

    #region Search and Sort Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(KeyItem other)
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

    /// <summary>Gets or sets the DataConfig name.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      set { mDataConfigName = NetString.InitString(value); }
    }
    private string mDataConfigName;

    /// <summary>The Description value.</summary>
    public string Description
    {
      get { return mDescription; }
      set { mDescription = NetString.InitString(value); }
    }
    private string mDescription;

    /// <summary>The ID value.</summary>
    public long ID { get; set; }

    /// <summary>Gets or sets the MaxLength value.</summary>
    public int MaxLength { get; set; }

    /// <summary>Gets or sets the Primary Key name.</summary>
    public string PrimaryKeyName
    {
      get { return mPrimaryKeyName; }
      set { mPrimaryKeyName = NetString.InitString(value); }
    }
    private string mPrimaryKeyName;

    /// <summary>The Property name.</summary>
    public string PropertyName
    {
      get { return mPropertyName; }
      set { mPropertyName = NetString.InitString(value); }
    }
    private string mPropertyName;

    /// <summary>Gets or sets the Table name.</summary>
    public string TableName
    {
      get { return mTableName; }
      set { mTableName = NetString.InitString(value); }
    }
    private string mTableName;
    #endregion
  }
}
