// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataAssemblies.cs
using System.Collections.Generic;

namespace LJCDocObjLib
{
  // Represents a collection of DataAssembly objects.
  /// <include path='items/DataAssemblies/*' file='Doc/ProjectDocObjLib.xml'/>
  public class DataAssemblies : List<DataAssembly>
  {
    // Returns the element with the name matching the supplied value.
    /// <include path='items/LJCSearchByDescription/*' file='Doc/DataAssemblies.xml'/>
    public DataAssembly LJCSearchByDescription(string description)
    {
      DataAssembly retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      DataAssembly dataAssembly = new DataAssembly(null, null, null)
      {
        Description = description
      };
      int index = BinarySearch(dataAssembly);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    #region Class Data

    // <summary>The previous count value.</summary>
    private int mPrevCount;
    #endregion
  }
}
