// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBRows.cs
using System.Xml.Serialization;
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of DbRow objects.
  /// <include file='Doc/DbRows.xml'
  ///  path='members/LJCDbRows/*'/>
  [XmlRoot("DbRows")]
  public class LJCDBRows : List<LJCDBRow>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static LJCDBRows? LJCDeserialize(string? fileSpec = null)
    {
      LJCDBRows? retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = LJC.XmlDeserialize(typeof(LJCDBRows), fileSpec)
        as LJCDBRows;
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/DbRows.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get => "DbRows.xml";
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDBRows()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/ConstructorCopy/*'/>
    public LJCDBRows(LJCDBRows items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDBRow(item));
        }
      }
    }
    #endregion

    #region Data Class Methods

    // Clones the structure of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDBRows Clone()
    {
      var retValue = new LJCDBRows();
      foreach (LJCDBRow dbRow in this)
      {
        if (dbRow != null)
        {
          var clone = dbRow.Clone();
          if (clone != null)
          {
            retValue.Add(clone);
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Collection Methods

    // Adds the supplied object.
    /// <include file='Doc/DbRows.xml'
    ///  path='members/Add/*'/>
    public LJCDBRow? Add(LJCDataValues dataValues)
    {
      LJCDBRow? retValue = null;

      if (LJC.HasListItems(dataValues))
      {
        retValue = new LJCDBRow()
        {
          Values = [.. dataValues]
        };
        Add(retValue);
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems2/*'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include file='Doc/DbResult.xml'
    ///  path='members/Serialize2/*'/>
    public void Serialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion
  }
}
