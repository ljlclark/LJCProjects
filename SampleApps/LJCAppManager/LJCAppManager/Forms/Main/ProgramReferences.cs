// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramReferences.cs
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCAppManager
{
	// Represents a collection of ProgramReference objects.
	/// <include path='items/ProgramReferences/*' file='Doc/ProgramReferences.xml'/>
	public class ProgramReferences : List<ProgramReference>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ProgramReferences()
		{
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ProgramReferences.xml'/>
		public ProgramReference Add(string fileName, string displayName)
		{
			ProgramReference retValue = new ProgramReference()
			{
				FileName = fileName,
				DisplayName = displayName
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the app program by display name.
		/// <include path='items/BinarySearch1/*' file='Doc/ProgramReferences.xml'/>
		public ProgramReference BinarySearch(string displayName)
		{
			ProgramReference retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.DisplayName) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.DisplayName;
			}

			ProgramReference programReference = new ProgramReference()
			{
				DisplayName = displayName
			};
			int index = BinarySearch(programReference);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Retrieve the app program by file name.
		/// <include path='items/BinarySearch2/*' file='Doc/ProgramReferences.xml'/>
		public ProgramReference BinarySearch(string fileName, IComparer<ProgramReference> fileNameComparer)
		{
			ProgramReference retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.FileName) != 0)
			{
				mPrevCount = Count;
				Sort(fileNameComparer);
				mSortType = SortType.FileName;
			}

			ProgramReference programReference = new ProgramReference()
			{
				FileName = fileName
			};
			int index = BinarySearch(programReference, fileNameComparer);
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
		#endregion

		private enum SortType
		{
			DisplayName,
			FileName
		}
	}

	// Sort and search program on file name.
	/// <include path='items/ProgramFileNameComparer/*' file='Doc/ProgramReferences.xml'/>
	public class ProgramFileNameComparer : IComparer<ProgramReference>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(ProgramReference x, ProgramReference y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				retValue = x.FileName.CompareTo(y.FileName);
			}
			return retValue;
		}
	}
}
