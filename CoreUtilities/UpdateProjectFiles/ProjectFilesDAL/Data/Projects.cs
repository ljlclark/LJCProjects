// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Projects.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>Represents a collection of Project Data Objects.</summary>
  public class Projects : List<Project>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Projects()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Projects Clone()
    {
      var retValue = MemberwiseClone() as Projects;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public Projects GetCollection(List<Project> list)
    {
      Projects retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new Projects();
        foreach (Project item in list)
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
    public Project Add(ProjectParentKey parentKey, string name
      , string path = null)
    {
      Project retValue;

      string message = "";
      NetString.AddMissingArgument(message, parentKey);
      AddMissingValues(message, parentKey);
      NetString.AddMissingArgument(message, name);
      NetString.ThrowInvalidArgument(message);

      retValue = LJCRetrieve(parentKey, name);
      if (null == retValue)
      {
        retValue = new Project()
        {
          CodeLine = parentKey.CodeLine,
          CodeGroup = parentKey.CodeGroup,
          Solution = parentKey.Solution,
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
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    public void LJCDelete(ProjectParentKey parentKey, string name)
    {
      var item = LJCRetrieve(parentKey, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves items that match the supplied values.
    /// <summary>
    /// Retrieves a collection that match the supplied values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <returns>The collection object.</returns>
    public Projects LJCLoad(ProjectParentKey parentKey)
    {
      Projects retValue = null;

      if (parentKey != null)
      {
        var items = FindAll(x =>
          x.CodeLine == parentKey.CodeLine
          && x.CodeGroup == parentKey.CodeGroup
          && x.Solution == parentKey.Solution);
        retValue = GetCollection(items);
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <summary>
    /// Retrieves the collection element with unique values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public Project LJCRetrieve(ProjectParentKey parentKey, string name)
    {
      Project retValue = null;

      LJCSortUnique();
      Project searchItem = new Project()
      {
        CodeLine = parentKey.CodeLine,
        CodeGroup = parentKey.CodeGroup,
        Solution = parentKey.Solution,
        Name = name
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieves the collection element with path.
    /// <summary>
    /// Retrieves the collection element with path.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="path">The path value.</param>
    /// <returns>A reference to the matching item.</returns>
    public Project LJCRetrieveWithPath(ProjectParentKey parentKey, string path)
    {
      Project retValue = null;

      var comparer = new ProjectPath();
      LJCSortPath(comparer);
      Project searchItem = new Project()
      {
        CodeLine = parentKey.CodeLine,
        CodeGroup = parentKey.CodeGroup,
        Solution = parentKey.Solution,
        Path = path
      };
      int index = BinarySearch(searchItem, comparer);
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
    /// <param name="project">The Project object.</param>
    public void LJCUpdate(Project project)
    {
      if (NetCommon.HasItems(this))
      {
        var parentKey = GetParentKey(project);
        var item = LJCRetrieve(parentKey, project.Name);
        if (item != null)
        {
          item.Path = project.Path;
        }
      }
    }
    #endregion

    #region Public Methods

    // Adds the missing ParentKey values messages.
    /// <summary>
    /// Adds the missing ParentKey values messages.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="parentKey">The ParentKey object.</param>
    public void AddMissingValues(string message, ProjectParentKey parentKey)
    {
      if (parentKey != null)
      {
        if (!NetString.HasValue(parentKey.CodeLine))
        {
          message += $"{parentKey.CodeLine} is missing.";
        }
        if (!NetString.HasValue(parentKey.CodeGroup))
        {
          message += $"{parentKey.CodeGroup} is missing.";
        }
        if (!NetString.HasValue(parentKey.Solution))
        {
          message += $"{parentKey.Solution} is missing.";
        }
      }
    }

    // Retrieves the ParentKey from the object</summary>
    /// <summary>Retrieves the ParentKey from the object</summary>
    /// <param name="project">The Project object.</param>
    /// <returns>The ParentKey object.</returns>
    public ProjectParentKey GetParentKey(Project project)
    {
      var retValue = new ProjectParentKey()
      {
        CodeLine = project.CodeLine,
        CodeGroup = project.CodeGroup,
        Solution = project.Solution
      };
      return retValue;
    }

    /// <summary>Sorts on Path values.</summary>
    public void LJCSortPath(ProjectPath comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Path) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Path;
      }
    }

    /// <summary>Sorts on Unique values.</summary>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Unique) != 0)
      {
        mPrevCount = Count;
        Sort();
      }
      mSortType = SortType.Unique;
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Unique,
      Path
    }
    #endregion
  }
}
