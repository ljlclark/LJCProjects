// ControlRow.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LJCDataDetailLib
{
	// Represents the controls for a DataValue.
	/// <include path='items/ControlRow/*' file='Doc/ControlRow.xml'/>
	public class ControlRow : IComparable<ControlRow>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ControlRow()
		{
			AllowDisplay = true;
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ControlRow Clone()
		{
			ControlRow retValue = MemberwiseClone() as ControlRow;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ControlRow other)
		{
			int retValue;

			if (null == other)
			{
				// This value is greater than null.
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = DataValueName.CompareTo(other.DataValueName);

				// Not case sensitive.
				retValue = string.Compare(DataValueName, other.DataValueName, true);
			}
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return DataValueName;
		}
		#endregion

		#region Properties

		/// <summary>The AllowDisplay value.</summary>
		public bool AllowDisplay { get; set; }

		/// <summary>Gets or sets the ControlColumn index.</summary>
		public int ColumnIndex { get; set; }

		/// <summary>Gets or sets the DataValue name.</summary>
		public string DataValueName { get; set; }

		/// <summary>Gets or sets the Row Control value.</summary>
		[XmlIgnore()]
		public Control RowControl { get; set; }

		/// <summary>Gets or sets the ControlRow index.</summary>
		public int RowIndex { get; set; }

		/// <summary>Gets or sets the Row Label value.</summary>
		[XmlIgnore()]
		public Control RowLabel { get; set; }

		/// <summary>Gets or sets the ControlRow Tabbing Index.</summary>
		public int TabIndex { get; set; }

		/// <summary>Gets or sets the TabPage index.</summary>
		public int TabPageIndex { get; set; }

		/// <summary>Gets or sets the ControlRow width.</summary>
		public int Width { get; set; }
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class RowOrderComparer : IComparer<ControlRow>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int Compare(ControlRow x, ControlRow y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				retValue = x.TabPageIndex.CompareTo(y.TabPageIndex);
				if (0 == retValue)
				{
					retValue = x.ColumnIndex.CompareTo(y.ColumnIndex);
					if (0 == retValue)
					{
						retValue = x.RowIndex.CompareTo(y.RowIndex);
					}
				}
			}
			return retValue;
		}
	}
	#endregion
}
