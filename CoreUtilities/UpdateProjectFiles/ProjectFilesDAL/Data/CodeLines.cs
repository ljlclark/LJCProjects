// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLines.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>Represents a collection of CodeLine Data Objects.</summary>
  public class CodeLines : List<CodeLine>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CodeLines()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CodeLines Clone()
    {
      var retValue = MemberwiseClone() as CodeLines;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public CodeLines GetCollection(List<CodeLine> list)
    {
      CodeLines retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new CodeLines();
        foreach (CodeLine item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public CodeLine Add(string name, string path = null)
    {
      CodeLine retValue;

      string message = "";
      NetString.AddMissingArgument(message, name);
      NetString.ThrowInvalidArgument(message);

      retValue = LJCRetrieve(name);
      if (null == retValue)
      {
        retValue = new CodeLine()
        {
          Name = name,
          Path = path
        };
        Add(retValue);
      }
      return retValue;
    }

    // Removes an item by unique values.
    /// <summary>
    /// Removes an item by unique values.
    /// </summary>
    /// <param name="name">The Name value.</param>
    public void LJCDelete(string name)
    {
      CodeLine item = LJCRetrieve(name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves the collection element with unique values.
    /// <summary>
    /// Retrieves the collection element with unique values.
    /// </summary>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public CodeLine LJCRetrieve(string name)
    {
      CodeLine retValue = null;

      LJCSortUnique();
      CodeLine searchItem = new CodeLine()
      {
        Name = name
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and updates the collection item.
    /// <summary>
    /// Finds and updates the collection item.
    /// </summary>
    /// <param name="codeLine">The CodeLine object.</param>
    public void LJCUpdate(CodeLine codeLine)
    {
      if (NetCommon.HasItems(this))
      {
        var item = LJCRetrieve(codeLine.Name);
        if (item != null)
        {
          item.Path = codeLine.Path;
        }
      }
    }
    #endregion

    #region Public Methods

    /// <summary>Sorts on Unique values.</summary>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
