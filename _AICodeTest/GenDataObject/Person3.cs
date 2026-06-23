using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LJC
{
  /// <include path="members/LJCPerson" file="Doc/LJCPerson.xml"/>
  public class LJCPerson
  {
    #region Static Methods
    #endregion

    #region Constructors

    /// <include path="members/DefaultCtor" file="Doc/LJCPerson.xml"/>
    public LJCPerson()
    {
      ChangedProperties = new List<string>();
      mOriginalValues = new OriginalValues();
    }

    /// <include path="members/ParameterizedCtor" file="Doc/LJCPerson.xml"/>
    public LJCPerson(long id, long parentID, string name,
      string description = null)
    {
      mID = id;
      mParentID = parentID;
      mName = name?.Trim();
      mDescription = description?.Trim();

      ChangedProperties = new List<string>();
      mOriginalValues = new OriginalValues();
      SetOriginalState();
    }

    /// <include path="members/CopyCtor" file="Doc/LJCPerson.xml"/>
    public LJCPerson(LJCPerson person)
    {
      mID = person.ID;
      mParentID = person.ParentID;
      mName = person.Name;
      mDescription = person.Description;

      ChangedProperties = new List<string>(person.ChangedProperties);
      mOriginalValues = new OriginalValues()
      {
        ID = person.mOriginalValues.ID,
        ParentID = person.mOriginalValues.ParentID,
        Name = person.mOriginalValues.Name,
        Description = person.mOriginalValues.Description
      };
    }
    #endregion

    #region Data Object Methods

    /// <include path="members/Clone" file="Doc/LJCPerson.xml"/>
    public LJCPerson Clone()
    {
      return new LJCPerson(this);
    }

    /// <include path="members/SetOriginalState" file="Doc/LJCPerson.xml"/>
    public void SetOriginalState()
    {
      mOriginalValues.ID = mID;
      mOriginalValues.ParentID = mParentID;
      mOriginalValues.Name = mName;
      mOriginalValues.Description = mDescription;
      ChangedProperties.Clear();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void TrackChanges(string propertyName, object currentValue,
      object originalValue)
    {
      if (Equals(currentValue, originalValue))
      {
        if (mChangedProperties.Contains(propertyName))
        {
          mChangedProperties.Remove(propertyName);
        }
      }
      else
      {
        if (!ChangedProperties.Contains(propertyName))
        {
          ChangedProperties.Add(propertyName);
        }
      }
    }
    #endregion

    #region Public Properties

    /// <include path="members/ID" file="Doc/LJCPerson.xml"/>
    [Key]
    [Column("ID")]
    public long ID
    {
      get => mID;
      set
      {
        mID = value;
        TrackChanges("ID", mID, mOriginalValues.ID);
      }
    }

    /// <include path="members/ParentID" file="Doc/LJCPerson.xml"/>
    [Column("ParentID")]
    public long ParentID
    {
      get => mParentID;
      set
      {
        mParentID = value;
        TrackChanges("ParentID", mParentID, mOriginalValues.ParentID);
      }
    }

    /// <include path="members/Name" file="Doc/LJCPerson.xml"/>
    [Required]
    [StringLength(60)]
    [Column("Name")]
    public string Name
    {
      get => mName;
      set
      {
        mName = value?.Trim();
        TrackChanges("Name", mName, mOriginalValues.Name);
      }
    }

    /// <include path="members/Description" file="Doc/LJCPerson.xml"/>
    [StringLength(100)]
    [Column("Description")]
    public string Description
    {
      get => mDescription;
      set
      {
        mDescription = value?.Trim();
        TrackChanges("Description", mDescription, mOriginalValues.Description);
      }
    }

    /// <include path="members/ChangedProperties" file="Doc/LJCPerson.xml"/>
    public List<string> ChangedProperties { get; set; }
    #endregion

    #region Private Properties
    #endregion

    #region Public Fields
    #endregion

    #region Private Fields

    private long mID;
    private long mParentID;
    private string mName;
    private string mDescription;
    private OriginalValues mOriginalValues;
    #endregion

    #region Private Classes

    private class OriginalValues
    {
      public long ID { get; set; }
      public long ParentID { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
    }
    #endregion
  }
}
