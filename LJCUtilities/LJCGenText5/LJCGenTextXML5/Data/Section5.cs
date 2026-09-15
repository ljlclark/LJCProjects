// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Section.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCGenTextXML5
{
  // Represents a GenText section.
  /// <include file='Doc/Section5.xml'
  ///  path='items/Section/*'/>
  public class Section : IComparable<Section>
  {
    #region Static Functions

    // Checks for RepeatItem data.
    /// <include file='Doc/Section5.xml'
    ///  path='items/HasData/*'/>
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

    // Checks for Subsection.
    /// <include file='Doc/Section5.xml'
    ///  path='items/HasSubsection/*'/>
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

    // Checks for specified name.
    /// <include file='Doc/Section5.xml'
    ///  path='items/IsName/*'/>
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

    #region Data Properties

    // Gets or sets the IsList indicator.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/IsList/*'/>
    [XmlIgnore()]
    public bool IsList { get; set; }

    // Gets or sets the section name.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/Name/*'/>
    public string Name { get; set; } = null!;

    // Gets or sets the repeate items.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/RepeatItems/*'/>
    public RepeatItems RepeatItems { get; set; }
    #endregion

    #region Class Properties

    // Gets or sets the current repeate item.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/CurrentRepeatItem/*'/>
    [XmlIgnore()]
    public RepeatItem CurrentRepeatItem { get; set; } = null!;

    // Gets or sets the EndProcessing flag.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/EndProcessing/*'/>
    public bool EndProcessing { get; set; }

    // Gets or sets the starting line index.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/BeginLineIndex/*'/>
    [XmlIgnore()]
    public int BeginLineIndex { get; set; }
    #endregion

    #region Constructors

    //Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/Constructor/*'/>
    public Section()
    {
      RepeatItems = [];
    }

    // Initializes the Section object with the supplied values.
    /// <include file='Doc/Section5.xml'
    ///  path='items/ParamConstructor/*'/>
    public Section(string name)
    {
      Name = name;
      RepeatItems = [];
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public Section? Clone()
    {
      Section? retValue = MemberwiseClone() as Section;
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/CompareTo/*'/>
    public int CompareTo(Section? other)
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

    // Checks for RepeatItem data.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/HasData/*'/>
    public bool HasData()
    {
      bool retValue = false;

      if (LJC.HasListItems(RepeatItems))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for Subsection.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/HasSubsection/*'/>
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

    // Checks for Current Subsection RepeatItems.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/HasSubsectionData/*'/>
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

    // Checks for specified name.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/IsName/*'/>
    public bool IsName(string name)
    {
      bool retValue = false;

      if (Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion
  }
}
