// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Sections.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LJCGenTextLib
{
  // Represents a collection of Section objects.
  /// <include path='items/Sections/*' file='Doc/Sections.xml'/>
  [XmlRoot("Sections")]
  public class Sections : List<Section>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static Sections LJCDeserialize(string fileSpec = null)
    {
      Sections retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (false == File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        retValue = NetCommon.XmlDeserialize(typeof(Sections)
          , fileSpec) as Sections;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Sections()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Methods

    // Creates the Section object with the supplied values
    /// <include path='items/Add/*' file='Doc/Sections.xml'/>
    public Section Add(string name)
    {
      Section retValue = null;

      if (NetString.HasValue(name))
      {
        retValue = LJCSearchName(name);
        if (null == retValue)
        {
          retValue = new Section(name);
          Add(retValue);
        }
      }
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public Section LJCSearchName(string name)
    {
      Section section;
      int index;
      Section retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      section = new Section(name);
      index = BinarySearch(section);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Serializes the collection to a file.
    /// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }

    // Serializes the collection to a file.
    /// <summary>
    /// Serializes the collection to a file.
    /// </summary>
    /// <returns>The serialized string.</returns>
    public string LJCSerializeToString()
    {
      string retValue;

      retValue = NetCommon.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    [XmlIgnore()]
    public static string LJCDefaultFileName
    {
      get { return "Sections.xml"; }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
