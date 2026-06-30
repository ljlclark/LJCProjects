// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCPerson.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace _AICodeTest
{
  /// <include path="members/LJCPerson" file="Doc/LJCPerson.xml"/>
  public class LJCPerson : IComparable<LJCPerson>
  {
    #region Constructor Methods

    /// <include path="members/LJCPersonCtor" file="Doc/LJCPerson.xml"/>
    public LJCPerson()
    {
      _id = 0;
      _parentID = 0;
      _name = null;
      _description = null;

      _changedProperties = new HashSet<string>();
      _originalValues = new OriginalValues();
      SetOriginalState();
    }

    /// <include path="members/LJCPersonCtor2" file="Doc/LJCPerson.xml"/>
    public LJCPerson(long id, string name, string description = null) : this()
    {
      _id = id;
      _name = name;
      _description = description;
      SetOriginalState();
    }

    /// <summary>
    /// The copy constructor.
    /// </summary>
    /// <param name="person">The person object to copy.</param>
    protected LJCPerson(LJCPerson person)
    {
      _id = person.ID;
      _parentID = person.ParentID;
      _name = person.Name;
      _description = person.Description;

      _originalValues = new OriginalValues()
      {
        ID = person._originalValues.ID,
        ParentID = person._originalValues.ParentID,
        Name = person._originalValues.Name,
        Description = person._originalValues.Description,
      };
      _changedProperties = new HashSet<string>(person._changedProperties);
    }
    #endregion

    #region Data Object Methods

    /// <include path="members/Clone" file="Doc/LJCPerson.xml"/>
    public LJCPerson Clone()
    {
      var retClone = new LJCPerson(this);
      return retClone;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='Doc/LJCPerson.xml'/>
    public int CompareTo(LJCPerson other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        retValue = ParentID.CompareTo(other.ParentID);
        if (0 == retValue)
        {
          // Case sensitive.
          retValue = Name.CompareTo(other.Name);

          // Not case sensitive.
          //retValue = string.Compare(Name, other.Name, true);
        }
      }
      return retValue;
    }

    /// <include path="members/SetOriginalState" file="Doc/LJCPerson.xml"/>
    public void SetOriginalState()
    {
      _originalValues.ID = _id;
      _originalValues.ParentID = _parentID;
      _originalValues.Name = _name;
      _originalValues.Description = _description;
      _changedProperties.Clear();
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='Doc/LJCPerson.xml'/>
    public override string ToString()
    {
      var retValue = $"{_name}:{_parentID}, {_id}";
      return retValue;
    }
    #endregion

    #region Methods

    private void TrackChange<T>(string propertyName, T originalValue, T newValue)
    {
      // Use an explicit null check for reference types.
      if (EqualityComparer<T>.Default.Equals(originalValue, newValue))
      {
        _changedProperties.Remove(propertyName);
      }
      else
      {
        _changedProperties.Add(propertyName);
      }
    }
    #endregion

    #region Data Properties

    /// <include path="members/ID" file="Doc/LJCPerson.xml"/>
    [Required]
    [Column("ID", TypeName = "bigint")]
    public long ID
    {
      get => _id;
      set
      {
        if (_id != value)
        {
          TrackChange(nameof(ID), _originalValues.ID, value);
          _id = value;
        }
      }
    }
    private long _id;

    /// <include path="members/ParentID" file="Doc/LJCPerson.xml"/>
    [Required]
    [Column("ParentID", TypeName = "bigint")]
    public long ParentID
    {
      get => _parentID;
      set
      {
        if (_parentID != value)
        {
          TrackChange(nameof(ParentID), _originalValues.ParentID, value);
          _parentID = value;
        }
      }
    }
    private long _parentID;

    /// <include path="members/Name" file="Doc/LJCPerson.xml"/>
    [Required]
    [Column("Name", TypeName = "varchar(60)")]
    public string Name
    {
      get => _name;
      set
      {
        // If value is changed.
        var newValue = value?.Trim();
        if (_name != newValue)
        {
          TrackChange(nameof(Name), _originalValues.Name, newValue);
          _name = newValue;
        }
      }
    }
    private string _name;

    /// <include path="members/Description" file="Doc/LJCPerson.xml"/>
    [Column("Description", TypeName = "varchar(100)")]
    public string Description
    {
      get => _description;
      set
      {
        var newValue = value?.Trim();
        if (_description != newValue)
        {
          TrackChange(nameof(Description), _originalValues.Description, newValue);
          _description = newValue;
        }
      }
    }
    private string _description;

    /// <include path="members/ChangedProperties" file="Doc/LJCPerson.xml"/>
    public List<string> ChangedProperties
    {
      get => _changedProperties.ToList<string>();
    }
    #endregion

    #region Class Data

    // Prevents duplicates automatically.
    // Faster than List<T>.
    private readonly HashSet<string> _changedProperties;

    private readonly OriginalValues _originalValues;

    private class OriginalValues
    {
      #region Public Properties

      public long ID { get; set; }
      public long ParentID { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      #endregion
    }
    #endregion
  }
}
