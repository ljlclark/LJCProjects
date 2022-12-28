// ControlColumns.cs
using LJCNetCommon;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCDataDetailLib
{
	// Represents a collection of ControlColumn objects.
	/// <include path='items/ControlColumns/*' file='Doc/ControlColumns.xml'/>
	public class ControlColumns : IEnumerable<ControlColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ControlColumns()
		{
			Items = new List<ControlColumn>();
		}
		#endregion

		#region Collection Methods

		// Adds the specified object.
		/// <include path='items/Add/*' file='Doc/ControlColumns.xml'/>
		public void Add(ControlColumn item)
		{
			if (item != null)
			{
				Items.Add(item);
			}
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

		#region Public Methods

		// Gets the ControlRow parent column.
		/// <include path='items/ControlRowColumn/*' file='Doc/ControlColumns.xml'/>
		public ControlColumn ControlRowColumn(ControlRow controlRow)
		{
			ControlColumn retValue = null;

			if (controlRow != null && controlRow.ColumnIndex >= 0)
			{
				retValue = this[controlRow.ColumnIndex];
			}
			return retValue;
		}
		#endregion

		#region IEnumerable Methods and Properties

		/// <summary>The Collection count.</summary>
		public int Count
		{
			get { return Items.Count; }
		}

		// Gets the Collection Enumerator.
		/// <include path='items/GetEnumerator/*' file='Doc/ControlColumns.xml'/>
		public IEnumerator<ControlColumn> GetEnumerator()
		{
			return ((IEnumerable<ControlColumn>)Items).GetEnumerator();
		}

		// Gets the Collection Enumerator.
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<ControlColumn>)Items).GetEnumerator();
		}

		/// <summary>The ControlColumn items.</summary>
		[XmlArray("ControlColumns")]
		public List<ControlColumn> Items { get; set; }

		// Gets the item by index value.
		/// <include path='items/Indexer/*' file='Doc/ControlColumns.xml'/>
		public ControlColumn this[int index]
		{
			get
			{
				ControlColumn retValue = null;

				if (index > -1 && index < Count)
				{
					retValue = Items[index];
				}
				return retValue;
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string DefaultFileName
		{
			get { return "ControlColumns.xml"; }
		}
		#endregion
	}
}
