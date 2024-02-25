// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProjectFiles.cs
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  public class ProjectFiles : List<ProjectFile>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProjectFiles()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with unique values.
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="targetSolution">The Target Solution name.</param>
    /// <param name="targetProject">The Target Project name.</param>
    /// <param name="sourceFileName">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public ProjectFile LJCSearchUnique(string targetSolution
      , string targetProject, string sourceFileName)
    {
      ProjectFile retValue = null;

      LJCSortUnique();
      ProjectFile searchItem = new ProjectFile()
      {
        TargetSolution = targetSolution,
        TargetProject = targetProject,
        SourceFileName = sourceFileName
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
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
