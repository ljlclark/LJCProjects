// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Sections.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace LJCGenTextXAL5
{
  // Represents a collection of Section objects.
  /// <include file='Doc/Sections5.xml'
  ///  path='items/Sections/*'/>
  [XmlRoot("Sections")]
  public class Sections : List<Section>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCDeserialize/*'/>
    public static Sections? LJCDeserialize(string? fileSpec = null)
    {
      Sections? retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      if (!File.Exists(fileSpec))
      {
        string errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        retValue = LJC.XmlDeserialize(typeof(Sections)
          , fileSpec) as Sections;
      }
      return retValue;
    }

    // Deserializes from the supplied XML string.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCDeserializeString/*'/>
    public static Sections? LJCDeserializeString(string? xml)
    {
      Sections? retValue;

      if (!LJC.HasText(xml))
      {
        string errorText = $"Parameter xml is missing.";
        throw new ArgumentNullException(errorText);
      }
      else
      {
        retValue = LJC.XmlDeserializeMessage(typeof(Sections)
          , xml) as Sections;
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCDefaultFileName/*'/>
    [XmlIgnore()]
    public static string LJCDefaultFileName
    {
      get { return "Sections.xml"; }
    }
    #endregion

    #region Class Data

    private int _PrevCount;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public Sections()
    {
      _PrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and adds the Section object with the supplied values.
    /// <include file='Doc/Sections5.xml'
    ///  path='items/Add/*'/>
    public Section? Add(string name)
    {
      Section? retValue = null;

      if (LJC.HasText(name))
      {
        retValue = Retrieve(name);
        if (null == retValue)
        {
          retValue = new Section(name);
          Add(retValue);
        }
      }
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCSearchName/*'/>
    public Section? Retrieve(string name)
    {
      Section? retValue = null;

      if (Count != _PrevCount)
      {
        _PrevCount = Count;
        Sort();
      }

      Section section = new(name);
      var index = BinarySearch(section);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Serializes the collection to a file.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCSerialize/*'/>
    public void LJCSerialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }

    // Serializes the collection to a string.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCSerializeToString/*'/>
    public string LJCSerializeToString()
    {
      string retValue;

      retValue = LJC.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }
    #endregion
  }
}
