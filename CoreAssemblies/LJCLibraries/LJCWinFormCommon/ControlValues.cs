// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlValues.cs
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCWinFormCommon
{
  /// <summary>Represents a collection of ControlValue objects.</summary>
  [XmlRoot("ControlValues")]
  public class ControlValues : List<ControlValue>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlValues()
    {
    }
    #endregion

    #region Methods

    // Creates the ControlValue object from the supplied values and adds the
    // element to the collection list.
    /// <include path='items/Add/*' file='Doc/ControlValues.xml'/>
    public void Add(string controlName, int left = 0, int top = 0
      , int width = 0, int height = 0)
    {
      ControlValue controlValue = new ControlValue()
      {
        ControlName = controlName,
        Left = left,
        Top = top,
        Width = width,
        Height = height
      };
      Add(controlValue);
    }

    // Retrieve the collection element by name.
    /// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public ControlValue LJCSearchName(string name)
    {
      ControlValue retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      ControlValue searchValue = new ControlValue()
      {
        ControlName = name
      };
      int index = BinarySearch(searchValue);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
