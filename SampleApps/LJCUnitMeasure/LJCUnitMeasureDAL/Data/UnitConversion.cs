// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitConversion.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCUnitMeasureDAL
{
	/// <summary>The UnitConversion table Data Object.</summary>
	public class UnitConversion : IComparable<UnitConversion>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitConversion()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitConversion Clone()
		{
			UnitConversion retValue = MemberwiseClone() as UnitConversion;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return $"{mFromUnitMeasureID}-{mToUnitMeasureID}-{Expression}";
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int CompareTo(UnitConversion other)
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
				retValue = FromUnitMeasureID.CompareTo(other.FromUnitMeasureID);
				if (0 == retValue)
				{
					retValue = ToUnitMeasureID.CompareTo(other.ToUnitMeasureID);
				}

				// Not case sensitive.
				//retValue = string.Compare(Name, other.Name, true);
			}
			return retValue;
		}
		#endregion

		/// <summary>
		/// Returns the converted Unit.
		/// </summary>
		/// <param name="value">The compute value expression.</param>
		/// <returns>The converted value.</returns>
		public object ConvertUnit(object value)
		{
			object retValue = Evaluator.Compute(Expression
				, value);
			return retValue;
		}

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the FromUnitMeasureID value.</summary>
		//[Required]
		//[Column("FromUnitMeasureID", TypeName="int")]
		public Int32 FromUnitMeasureID
		{
			get { return mFromUnitMeasureID; }
			set
			{
				mFromUnitMeasureID = ChangedNames.Add(ColumnFromUnitMeasureID, mFromUnitMeasureID, value);
			}
		}
		private Int32 mFromUnitMeasureID;

		/// <summary>Gets or sets the ToUnitMeasureID value.</summary>
		//[Required]
		//[Column("ToUnitMeasureID", TypeName="int")]
		public Int32 ToUnitMeasureID
		{
			get { return mToUnitMeasureID; }
			set
			{
				mToUnitMeasureID = ChangedNames.Add(ColumnToUnitMeasureID, mToUnitMeasureID, value);
			}
		}
		private Int32 mToUnitMeasureID;

		/// <summary>Gets or sets the Expression value.</summary>
		//[Required]
		//[Column("Expression", TypeName="nvarchar(25)")]
		public String Expression
		{
			get { return mExpression; }
			set
			{
				value = NetString.InitString(value);
				mExpression = ChangedNames.Add(ColumnExpression, mExpression, value);
			}
		}
		private String mExpression;
		#endregion

		#region Calculated and Join Data Properties

		///// <summary>Gets or sets the Join TypeName value.</summary>
		//public string TypeName { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "UnitConversion";

		/// <summary>The FromUnitMeasureID column name.</summary>
		public static string ColumnFromUnitMeasureID = "FromUnitMeasureID";

		/// <summary>The ToUnitMeasureID column name.</summary>
		public static string ColumnToUnitMeasureID = "ToUnitMeasureID";

		/// <summary>The Expression column name.</summary>
		public static string ColumnExpression = "Expression";

		/// <summary>The Expression maximum length.</summary>
		public static int LengthExpression = 25;
		#endregion

		#region Calculated and Join Class Data

		///// <summary>The Join TypeName column name.</summary>
		//public static string ColumnTypeName = "TypeName";
		#endregion
	}
}
