// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Data;

namespace LJCUnitMeasureDAL
{
	/// <summary>Evaluates an expression string.</summary>
	public class Evaluator
	{
		#region Methods

		/// <summary>
		/// Evaluates the supplied expression string.
		/// </summary>
		/// <param name="expression">The expression string.</param>
		/// <param name="digits">The Decimal or Double digits.</param>
		/// <returns>The evaluated result.</returns>
		public static object Compute(string expression, int digits = 0)
		{
			object retValue = 0;

			DataTable dataTable = new DataTable();
			retValue = dataTable.Compute(expression, "");
			if (digits > 0)
			{
				switch (retValue.GetType().Name)
				{
					case "Decimal":
						retValue = Math.Round((decimal)retValue, digits);
						break;
					case "Double":
						retValue = Math.Round((double)retValue, digits);
						break;
				}
			}
			return retValue;
		}

		/// <summary>
		/// Evaluates the supplied expression format.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="values">The object values.</param>
		/// <returns>The evaluated result.</returns>
		public static object Compute(string format, params object[] values)
		{
			var retValue = ComputeRound(format, 0, values);
			return retValue;
		}

		/// <summary>
		/// Evaluates the supplied expression format.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="digits">The number of rounding digits.</param>
		/// <param name="values">The object values.</param>
		/// <returns>The evaluated result.</returns>
		public static object ComputeRound(string format, int digits
			, params object[] values)
		{
			string expression = string.Format(format, values);
			var retValue = Compute(expression, digits);
			return retValue;
		}
		#endregion
	}
}
