// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Replacements.cs
using LJCNetCommon5;

namespace LJCGenTextXML5
{
  // Represents a collection of Replacement objects.
  /// <include file='Doc/Replacements5.xml'
  ///  path='items/Replacements/*'/>
  public class Replacements : List<Replacement>
  {
    #region Class Data

    private int _PrevCount;
    #endregion

    #region Constructors

    //Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public Replacements()
    {
      _PrevCount = -1;
    }
    #endregion

    #region Methods

    // Creates the Replacement object with the supplied values
    /// <include file='Doc/Replacements5.xml'
    ///  path='items/Add/*'/>
    public Replacement? Add(string name, string value)
    {
      Replacement? retValue = null;

      if (LJC.HasText(name))
      {
        retValue = Retrieve(name);
        if (null == retValue)
        {
          retValue = new Replacement(name, value)
          {
            Name = name
          };
          Add(retValue);
        }
      }
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='items/LJCSearchName/*'/>
    public Replacement? Retrieve(string name)
    {
      Replacement replacement;
      int index;
      Replacement? retValue = null;

      if (Count != _PrevCount)
      {
        _PrevCount = Count;
        Sort();
      }

      //replacement = new Replacement(name, null);
      replacement = new Replacement(name, "");
      index = base.BinarySearch(replacement);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion
  }
}
