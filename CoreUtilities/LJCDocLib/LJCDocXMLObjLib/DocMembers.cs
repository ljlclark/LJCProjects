// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMembers.cs
using System.Collections.Generic;

namespace LJCDocXMLObjLib
{
  // The deserialized XML documentation members.
  /// <include path='items/DocMembers/*' file='Doc/DocMembers.xml'/>
  public class DocMembers : List<DocMember>
  {
    #region Methods

    // Creates an element from the supplied values and adds it to the list.
    /// <include path='items/Add/*' file='Doc/DocMembers.xml'/>
    public DocMember Add(string name, string summary, string returns)
    {
      DocMember retValue = new DocMember
      {
        Name = name,
        Summary = summary,
        Returns = returns
      };
      return retValue;
    }

    // Adds elements from another list.
    /// <include path='items/AddFromList/*' file='Doc/DocMembers.xml'/>
    public void AddFromList(List<DocMember> list)
    {
      foreach (DocMember docMember in list)
      {
        Add(docMember);
      }
    }

    // Retrieve the collection element by name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DocMember LJCSearchName(string name)
    {
      DocMember retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      DocMember docMember = new DocMember()
      {
        Name = name
      };
      int index = BinarySearch(docMember);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Class Data

    // The previous count value.
    private int mPrevCount;
    #endregion
  }
}
