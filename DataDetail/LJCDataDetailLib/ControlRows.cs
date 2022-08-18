// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDataDetailLib
{
	// Represents a collection of ControlRow objects.
	/// <include path='items/ControlRows/*' file='Doc/ControlRows.xml'/>
	[XmlRoot("ControlRows")]
	public class ControlRows
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/Deserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public static ControlRows Deserialize(string fileSpec = null)
		{
			ControlRows retValue = null;

			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = DefaultFileName;
			}
			if (false == File.Exists(fileSpec))
			{
				var errorText = $"File '{fileSpec}' was not found.";
				throw new FileNotFoundException(errorText);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(ControlRows)
					, fileSpec) as ControlRows;
			}
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ControlRows()
		{
			Items = new List<ControlRow>();
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Adds the specified object.
		/// <include path='items/Add1/*' file='Doc/ControlRows.xml'/>
		public void Add(ControlRow controlRow)
		{
			Items.Add(controlRow);
		}

		// Creates and adds the object from the provided values.
		/// <include path='items/Add2/*' file='Doc/ControlRows.xml'/>
		public ControlRow Add(string dataValueName, int tabPageIndex = 0
			, int controlColumnIndex = 0, int rowIndex = 0)
		{
			ControlRow retValue = null;

			if (NetString.HasValue(dataValueName))
			{
				retValue = SearchName(dataValueName);
				if (null == retValue)
				{
					retValue = new ControlRow()
					{
						DataValueName = dataValueName,
						TabPageIndex = tabPageIndex,
						ColumnIndex = controlColumnIndex,
						RowIndex = rowIndex
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ControlRows Clone()
		{
			ControlRows retValue = MemberwiseClone() as ControlRows;
			return retValue;
		}

		/// <summary>The Collection count.</summary>
		public int Count
		{
			get { return Items.Count; }
		}

		// Serializes the collection to a file.
		/// <include path='items/Serialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public void Serialize(string fileSpec = null)
		{
			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = DefaultFileName;
			}
			NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
		}
		#endregion

		#region Sort and Search Methods

		// Retrieve the collection element.
		/// <include path='items/SearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public ControlRow SearchName(string name)
		{
			ControlRow retValue = null;

			SortName();
			ControlRow searchItem = new ControlRow()
			{
				DataValueName = name
			};
			int index = Items.BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = Items[index];
			}
			return retValue;
		}

		// Retrieve the collection element with control order.
		/// <include path='items/SearchControlOrder/*' file='Doc/ControlRows.xml'/>
		public ControlRow SearchRowOrder(int tabIndex, int columnIndex
			, int rowIndex)
		{
			RowOrderComparer comparer;
			ControlRow retValue = null;

			comparer = new RowOrderComparer();
			SortRowOrder(comparer);
			ControlRow searchItem = new ControlRow()
			{
				TabIndex = tabIndex,
				ColumnIndex = columnIndex,
				RowIndex = rowIndex
			};
			int index = Items.BinarySearch(searchItem, comparer);
			if (index > -1)
			{
				retValue = Items[index];
			}
			return retValue;
		}

		// Sort on Name.
		/// <include path='items/SortName/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public void SortName()
		{
			if (Items.Count != mPrevCount
				|| mSortType.CompareTo(SortType.Name) != 0)
			{
				mPrevCount = Items.Count;
				Items.Sort();
				mSortType = SortType.Name;
			}
		}

		// Sort with a Comparer.
		/// <include path='items/SortComparer/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public void SortRowOrder(RowOrderComparer comparer)
		{
			if (Items.Count != mPrevCount
				|| mSortType.CompareTo(SortType.ControlOrder) != 0)
			{
				mPrevCount = Items.Count;
				Items.Sort(comparer);
				mSortType = SortType.ControlOrder;
			}
		}
		#endregion

		#region Collection Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string DefaultFileName
		{
			get { return "ControlRows.xml"; }
		}

		/// <summary>The ControlRow items.</summary>
		public List<ControlRow> Items { get; set; }

		// Gets the item by index value.
		/// <include path='items/Indexer/*' file='Doc/ControlRows.xml'/>
		public ControlRow this[int index]
		{
			get { return Items[index]; }
		}
		#endregion

		#region Custom Methods

		// Sets the Width value.
		/// <include path='items/SetControlRowWidth/*' file='Doc/ControlRows.xml'/>
		public void SetControlRowWidth(ControlColumns controlColumns, ControlRow controlRow)
		{
			if (controlRow != null)
			{
				var controlColumn = controlColumns.ControlRowColumn(controlRow);
				if (controlColumn != null)
				{
					int width = controlColumn.LabelsWidth;
					width += BorderHorizontal;
					width += controlRow.RowControl.Width;
					controlRow.Width = width;
				}
			}
		}
		#endregion

		#region Properties

		/// <summary>The Horizontal Border value.</summary>
		public int BorderHorizontal { get; set; }

		/// <summary>The Vertical Border value.</summary>
		public int BorderVertical { get; set; }

		/// <summary>The Pixels per character value.</summary>
		public int CharacterPixels { get; set; }

		/// <summary>The ControlColumn rows limit.</summary>
		public int ColumnRowsLimit { get; set; }

		/// <summary>Gets or sets the Config file name.</summary>
		public string ConfigRowsFileName { get; set; }

		/// <summary>The ControlColumns count.</summary>
		public int ControlColumnsCount { get; set; }

		/// <summary>The ControlRow height.</summary>
		public int ControlRowHeight { get; set; }

		/// <summary>The ControlRow spacing.</summary>
		public int ControlRowSpacing { get; set; }

		/// <summary>The Controls Set height.</summary>
		public int ControlsHeight { get; set; }

		/// <summary>The Controls Set width.</summary>
		public int ControlsWidth { get; set; }

		/// <summary>The DataConfig name value.</summary>
		public string DataConfigName { get; set; }

		/// <summary>The DataValue count.</summary>
		public int DataValueCount { get; set; }

		/// <summary>The Maximum control display character width.</summary>
		public int MaxControlCharacters { get; set; }

		/// <summary>The PageColumns limit.</summary>
		public int PageColumnsLimit { get; set; }
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			ControlOrder,
			Name
		}
		#endregion
	}
}
