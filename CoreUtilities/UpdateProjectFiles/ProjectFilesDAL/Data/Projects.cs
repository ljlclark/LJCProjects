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
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Projects()
    {
      ArgError = new ArgError("ProjectFilesDAL.Projects");
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Projects Clone()
    {
      var retValue = MemberwiseClone() as Projects;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
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

    // Creates and adds the object with the provided values.
    /// <include path='items/Add/*' file='Doc/Project.xml'/>
    public Project Add(ProjectParentKey parentKey, string name
      , string path = null)
    {
      Project retValue;

      ArgError.MethodName = "Add(parentKey, name, path)";
      ArgError.Add(Project.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      // Do not add duplicate of existing item.
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
    /// <include path='items/LJCDelete/*' file='Doc/Project.xml'/>
    public void LJCDelete(ProjectParentKey parentKey, string name)
    {
      ArgError.MethodName = "LJCDelete(parentKey, name)";
      ArgError.Add(Project.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var item = LJCRetrieve(parentKey, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves items that match the supplied values.
    /// <include path='items/LJCLoad/*' file='Doc/Project.xml'/>
    public Projects LJCLoad(ProjectParentKey parentKey = null)
    {
      Projects retValue = null;

      if (null == parentKey)
      {
        retValue = Clone();
      }
      else
      {
        ArgError.MethodName = "LJCLoad(parentKey)";
        ArgError.Add(Project.ParentKeyValues(parentKey));
        NetString.ThrowArgError(ArgError.ToString());

        var items = FindAll(x =>
          x.CodeLine == parentKey.CodeLine
          && x.CodeGroup == parentKey.CodeGroup
          && x.Solution == parentKey.Solution);
        retValue = GetCollection(items);
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <include path='items/LJCRetrieve/*' file='Doc/Project.xml'/>
    public Project LJCRetrieve(ProjectParentKey parentKey, string name)
    {
      Project retValue = null;

      ArgError.MethodName = "LJCRetrieve(parentKey, name)";
      ArgError.Add(Project.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

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
    /// <include path='items/LJCRetrieveWithPath/*' file='Doc/Project.xml'/>
    public Project LJCRetrieveWithPath(ProjectParentKey parentKey, string path)
    {
      Project retValue = null;

      ArgError.MethodName = "LJCRetrieveWithPath(parentKey, path)";
      ArgError.Add(Project.ParentKeyValues(parentKey));
      ArgError.Add(path, "path");
      NetString.ThrowArgError(ArgError.ToString());

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
    /// <include path='items/LJCUpdate/*' file='Doc/Project.xml'/>
    public void LJCUpdate(Project project)
    {
      if (NetCommon.HasItems(this))
      {
        ArgError.MethodName = "LJCUpdate(project)";
        ArgError.Add(Project.ItemValues(project));
        NetString.ThrowArgError(ArgError.ToString());

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

    // Retrieves the ParentKey from the object</summary>
    /// <include path='items/GetParentKey/*' file='Doc/Project.xml'/>
    public ProjectParentKey GetParentKey(Project project)
    {
      ArgError.MethodName = "GetParentKey(project)";
      ArgError.Add(Project.ItemParentValues(project));
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "LJCSortPath(comparer)";
      ArgError.Add(comparer, "comparer");
      NetString.ThrowArgError(ArgError.ToString());

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

    #region Properties

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
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
