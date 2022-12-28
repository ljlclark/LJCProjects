// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ModuleReferences.cs
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCWinFormCommon
{
  /// <summary>Represents a collection of ModuleReference objects.</summary>
  public class ModuleReferences : List<ModuleReference>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ModuleReferences()
    {
    }
    #endregion

    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/ModuleReferences.xml'/>
    public ModuleReference Add(string fileName, string moduleName, string moduleDisplayName)
    {
      ModuleReference retValue = new ModuleReference()
      {
        FileName = fileName,
        ModuleName = moduleName,
        ModuleDisplayName = moduleDisplayName
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the App Module by file name and display name.
    /// <include path='items/BinarySearch1/*' file='Doc/ModuleReferences.xml'/>
    public ModuleReference LJCSearch(string fileName, string moduleDisplayName)
    {
      ModuleReference retValue = null;

      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.DisplayName) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.DisplayName;
      }

      ModuleReference programReference = new ModuleReference()
      {
        FileName = fileName,
        ModuleDisplayName = moduleDisplayName
      };
      int index = BinarySearch(programReference);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the App Module by file name and module name.
    /// <include path='items/BinarySearch2/*' file='Doc/ModuleReferences.xml'/>
    public ModuleReference LJCSearch(string fileName, string moduleName
      , IComparer<ModuleReference> moduleNameComparer)
    {
      ModuleReference retValue = null;

      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.ModuleName) != 0)
      {
        mPrevCount = Count;
        Sort(moduleNameComparer);
        mSortType = SortType.ModuleName;
      }

      ModuleReference moduleReference = new ModuleReference()
      {
        FileName = fileName,
        ModuleName = moduleName
      };
      int index = BinarySearch(moduleReference, moduleNameComparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;
    private enum SortType
    {
      DisplayName,
      ModuleName
    }
    #endregion
  }

  // Sort and search on file name and module name.
  /// <include path='items/ModuleNameComparer/*' file='Doc/ModuleReferences.xml'/>
  public class ModuleNameComparer : IComparer<ModuleReference>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ModuleReference x, ModuleReference y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = x.FileName.CompareTo(y.FileName);
        if (0 == retValue)
        {
          retValue = x.ModuleName.CompareTo(y.ModuleName);
        }
      }
      return retValue;
    }
  }
}
