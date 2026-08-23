// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlValues5.cs
using System.Xml.Serialization;

namespace LJCControls5
{
  // Represents a collection of ControlValue objects.
  /// <include file='Doc/ControlValues5.xml'
  ///  path='items/ControlValues/*'/>
  [XmlRoot("ControlValues")]
  public class ControlValues : List<ControlValue>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Constructor/*'/>
    public ControlValues()
    {
    }
    #endregion

    #region Methods

    // Creates the ControlValue object from the supplied values and adds the
    // element to the collection list.
    /// <include file='Doc/ControlValues5.xml'
    ///  path='items/Add/*'/>
    public void Add(string controlName, int left = 0, int top = 0
      , int width = 0, int height = 0)
    {
      var controlValue = new ControlValue()
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
    /// <include file='Doc/ControlValues5.xml'
    ///  path='items/LJCSearchName/*'/>
    public ControlValue? LJCSearchName(string name)
    {
      ControlValue? retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      ControlValue searchValue = new()
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
