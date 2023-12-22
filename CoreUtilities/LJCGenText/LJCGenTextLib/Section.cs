// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Section.cs
using LJCNetCommon;
using System;
using System.Xml.Serialization;

namespace LJCGenTextLib
{
  // Represents a TextGen section.
  /// <include path='items/Section/*' file='Doc/Section.xml'/>
  public class Section : IComparable<Section>
  {
    #region Static Functions

    /// <summary>Checks for RepeatItem data.</summary>
    public static bool HasData(Section section)
    {
      bool retValue = false;

      if (section != null
        && section.HasData())
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks for Subsection.</summary>
    public static bool HasSubsection(Section section)
    {
      bool retValue = false;

      if (section != null
        && section.HasSubsection())
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks for specified name.</summary>
    public static bool IsName(Section section, string name)
    {
      bool retValue = false;

      if (section != null)
      {
        retValue = section.IsName(name);
      }
      return retValue;
    }
    #endregion

    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Section()
    {
      RepeatItems = new RepeatItems();
    }

    // Initializes the Section object with the supplied values.
    /// <include path='items/SectionC/*' file='Doc/Section.xml'/>
    public Section(string name)
    {
      Name = name;
      RepeatItems = new RepeatItems();
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Section Clone()
    {
      Section retValue = MemberwiseClone() as Section;
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(Section other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        retValue = Name.CompareTo(other.Name);
      }
      return retValue;
    }
    #endregion

    #region Other Methods

    /// <summary>Checks for RepeatItem data.</summary>
    public bool HasData()
    {
      bool retValue = false;

      if (NetCommon.HasItems(RepeatItems))
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks for Subsection.</summary>
    public bool HasSubsection()
    {
      bool retValue = false;

      if (CurrentRepeatItem != null
        && CurrentRepeatItem.Subsection != null)
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks for Current Subsection RepeatItems.</summary>
    public bool HasSubsectionData()
    {
      bool retValue = false;

      if (HasSubsection()
        && CurrentRepeatItem.Subsection.HasData())
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks for specified name.</summary>
    public bool IsName(string name)
    {
      bool retValue = false;

      if (Name.ToLower() == name.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the IsList indicator.</summary>
    [XmlIgnore()]
    public bool IsList { get; set; }

    /// <summary>Gets or sets the section name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the repeate items.</summary>
    public RepeatItems RepeatItems { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the current repeate item.</summary>
    [XmlIgnore()]
    public RepeatItem CurrentRepeatItem { get; set; }

    /// <summary>Gets or sets the EndProcessing flag.</summary>
    public bool EndProcessing { get; set; }

    /// <summary>Gets or sets the starting line index.</summary>
    [XmlIgnore()]
    public int StartLineIndex { get; set; }
    #endregion
  }
}
