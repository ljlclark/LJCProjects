// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenRequest.cs
using System;
using System.Text;
using LJCNetCommon;
using LJCDBMessage;
using System.Collections.Generic;

namespace LJCViewEditor
{
	/// <summary>Provides methods to generate DbRequest create code.</summary>
	public class GenRequest
	{
		#region Static Functions

		// Creates and returns the DbRequest create code for the specified DbRequest.
		/// <include path='items/RequestCode/*' file='Doc/GenRequest.xml'/>
		public static string RequestCode(DbRequest dbRequest)
		{
			string retValue;

			string indent = "  ";
			StringBuilder builder = new StringBuilder(128);
			builder.Append("  DbRequest dbRequest = new DbRequest(RequestType.Load");
			builder.Append($", \"{dbRequest.TableName}\", \"{dbRequest.DataConfigName}\")\r\n");
			builder.Append("  {\r\n");
			builder.Append(ColumnsCode(dbRequest.Columns, indent));
			builder.Append("  };\r\n");
			builder.Append(JoinsCode(dbRequest, indent));
			builder.Append(FiltersCode(dbRequest, indent));
			builder.Append(OrderByNamesCode(dbRequest.OrderByNames, indent));
			retValue = builder.ToString();
			return retValue;
		}
		#endregion

		#region Child Functions

		// Creates and returns the DbColumns create code for the specified	DbColumns.
		/// <include path='items/ColumnsCode/*' file='Doc/GenRequest.xml'/>
		public static string ColumnsCode(DbColumns dbColumns, string indent = null)
		{
			string retValue;

			string[] columns = new string[]
			{
				"0 {0}Columns = new DbColumns\r\n",
				"1 {0}{{\r\n",
				"2 {0}  // columnName, propertyName = null, renameAs = null, ",
				"3 dataTypeName = \"String\", caption = null\r\n",  // Continuation
				"4 {0}  {{ \"{1}\"",
				"5 , \"{0}\"",  // Continuation
				"6  }}",  // Continuation
				"7 {0}}}\r\n"
			};

			indent += "  ";
			StringBuilder builder = new StringBuilder(128);
			builder.Append(Line(columns, 0, indent));
			builder.Append(Line(columns, 1, indent));
			builder.Append(Line(columns, 2, indent));
			builder.Append(Line(columns, 3, indent));

			bool isFirst = true;
			foreach (DbColumn dbColumn in dbColumns)
			{
				bool hasPropertyName = NetString.HasValue(dbColumn.PropertyName)
					&& dbColumn.PropertyName != dbColumn.ColumnName;
				bool hasRenameAs = NetString.HasValue(dbColumn.RenameAs)
					&& dbColumn.RenameAs != dbColumn.ColumnName;
				bool isNotString = dbColumn.DataTypeName != "String";
				bool hasCaption = NetString.HasValue(dbColumn.Caption)
					&& dbColumn.Caption != dbColumn.ColumnName;

				if (false == isFirst)
				{
					builder.AppendLine(",");
				}
				isFirst = false;

				builder.Append(Line(columns, 4, indent, dbColumn.ColumnName));
				if (hasPropertyName || hasRenameAs
					|| isNotString || hasCaption)
				{
					if (hasPropertyName)
					{
						builder.Append(Line(columns, 5, indent, dbColumn.PropertyName));
					}
					else
					{
						builder.Append(", null");
					}
					if (hasRenameAs || isNotString || hasCaption)
					{
						if (hasRenameAs)
						{
							builder.Append(Line(columns, 5, dbColumn.RenameAs));
						}
						else
						{
							builder.Append(", null");
						}
						if (isNotString || hasCaption)
						{
							if (isNotString)
							{
								builder.Append(Line(columns, 5, dbColumn.DataTypeName));
							}
							else
							{
								if (hasCaption)
								{
									builder.Append(", String");
								}
							}
							if (hasCaption)
							{
								builder.Append(Line(columns, 5, dbColumn.Caption));
							}
						}
					}
				}
				builder.Append(Line(columns, 6));
			}
			builder.AppendLine();
			builder.Append(Line(columns, 7, indent));
			retValue = builder.ToString();
			return retValue;
		}

		// Creates and returns the DbConditions create code for the specified DbFilter.
		/// <include path='items/ConditionsCode/*' file='Doc/GenRequest.xml'/>
		public static string ConditionsCode(DbFilter dbFilter, string indent = null)
		{
			string retValue;

			string[] conditions = new string[] {
				"0 {0}Conditions = new DbConditions()\r\n",
				"1 {0}{{\r\n",
				"2 {0}  // firstValue, secondValue, comparisonOperator = \"=\"\r\n",
				"3 {0}  {{ \"{1}\", \"{2}\" }}\r\n",
				"4 {0}  {{ \"{1}\", \"{2}\", \"{3}\" }}\r\n",
				"5 {0}}}\r\n"
			};

			StringBuilder builder = new StringBuilder(128);
			builder.Append(Line(conditions, 0, indent));
			builder.Append(Line(conditions, 1, indent));
			builder.Append(Line(conditions, 2, indent));
			bool isFirst = true;
			foreach (DbCondition dbCondition in dbFilter.ConditionSet.Conditions)
			{
				if (false == isFirst)
				{
					builder.AppendLine(",");
				}
				isFirst = false;
				if (false == NetString.IsEqual(dbCondition.ComparisonOperator.Trim(), "="))
				{
					// Add comparison that is not "=".
					builder.Append(Line(conditions, 4, indent, dbCondition.FirstValue
						, dbCondition.SecondValue, dbCondition.ComparisonOperator));
				}
				else
				{
					builder.Append(Line(conditions, 3, indent, dbCondition.FirstValue
						, dbCondition.SecondValue));
				}
				builder.Append(Line(conditions, 5, indent));
			}
			retValue = builder.ToString();
			return retValue;
		}

		// Creates and returns the DbFilters create code for the specified DbRequest.
		/// <include path='items/FiltersCode/*' file='Doc/GenRequest.xml'/>
		public static string FiltersCode(DbRequest dbRequest, string indent = null)
		{
			string retValue = null;

			string[] filters = new string[]
			{
				"0 {0}DbFilters dbFilters = new DbFilters();\r\n",
				"1 {0}dbRequest.Filters = dbFilters;\r\n",
				"2 \r\n",
				"3 {0}DbFilter dbFilter = new DbFilter();\r\n",
				"4 {0}dbFilter.ConditionSet = new DbConditionSet()\r\n",
				"5 {0}{{\r\n",
				"6 {0}  BooleanOperator = \"{1}\",\r\n",
				"7 {0}}};\r\n",
				"8 {0}dbFilters.Add(dbFilter);\r\n"
			};

			if (dbRequest.Filters != null && dbRequest.Filters.Count > 0)
			{
				StringBuilder builder = new StringBuilder(128);
				builder.AppendLine();
				builder.Append($"{indent}// Filters\r\n");
				builder.Append(Line(filters, 0, indent));
				builder.Append(Line(filters, 1, indent));
				foreach (DbFilter dbFilter in dbRequest.Filters)
				{
					// Filter
					builder.Append(Line(filters, 2, indent));
					builder.Append(Line(filters, 3, indent));
					builder.Append(Line(filters, 4, indent));
					builder.Append(Line(filters, 5, indent));
					builder.Append(Line(filters, 6, indent
						, dbFilter.ConditionSet.BooleanOperator));

					// Conditions
					builder.Append(ConditionsCode(dbFilter, indent + "  "));

					builder.Append(Line(filters, 7, indent));
					builder.Append(Line(filters, 8, indent));
				}
				retValue = builder.ToString();
			}
			return retValue;
		}

		// Creates and returns the JoinOns create code for the specified Join.
		/// <include path='items/JoinOnsCode/*' file='Doc/GenRequest.xml'/>
		public static string JoinOnsCode(DbJoin dbJoin, string indent = null)
		{
			string retValue = null;

			string[] joinOns = new string[]
			{
				"0 {0}JoinOns = new DbJoinOns()\r\n",
				"1 {0}{{\r\n",
				"2 {0}  {{ \"{1}\", \"{2}\" }}",
				"3 {0}}},\r\n"
			};

			if (dbJoin.JoinOns != null && dbJoin.JoinOns.Count > 0)
			{
				indent += "  ";
				StringBuilder builder = new StringBuilder(128);
				builder.Append(Line(joinOns, 0, indent));
				builder.Append(Line(joinOns, 1, indent));
				bool first = true;
				foreach (DbJoinOn dbJoinOn in dbJoin.JoinOns)
				{
					if (false == first)
					{
						builder.AppendLine(",");
					}
					first = false;

					builder.Append(Line(joinOns, 2, indent, dbJoinOn.FromColumnName
						, dbJoinOn.ToColumnName));
				}
				builder.AppendLine();
				builder.Append(Line(joinOns, 3, indent));
				retValue = builder.ToString();
			}
			return retValue;
		}

		// Creates and returns the DbJoins create code for the specified DbRequest.
		/// <include path='items/JoinsCode/*' file='Doc/GenRequest.xml'/>
		public static string JoinsCode(DbRequest dbRequest, string indent = null)
		{
			string retValue = null;

			string[] joins = new string[]
			{
				"0 {0}DbJoins dbJoins = new DbJoins();\r\n",
				"1 {0}\r\n",
				"2 {0}DbJoin dbJoin = new DbJoin()\r\n",
				"3 {0}{{\r\n",
				"4 {0}  TableAlias = \"{1}\",\r\n",
				"5 {0}  TableName = \"{1}\",\r\n",
				"6 {0}  JoinType = \"{1}\",\r\n",
				"7 {0}}};\r\n",
				"8 {0}dbJoins.Add(dbJoin);\r\n",
				"9 {0}dbRequest.Joins = dbJoins;\r\n"
			};

			if (dbRequest.Joins != null && dbRequest.Joins.Count > 0)
			{
				StringBuilder builder = new StringBuilder(128);
				builder.AppendLine();
				builder.Append($"{indent}// Joins\r\n");
				builder.Append(Line(joins, 0, indent));
				foreach (DbJoin dbJoin in dbRequest.Joins)
				{
					// Join
					builder.Append(Line(joins, 1, indent));
					builder.Append(Line(joins, 2, indent));
					builder.Append(Line(joins, 3, indent));
					if (NetString.HasValue(dbJoin.TableAlias))
					{
						builder.Append(Line(joins, 4, indent, dbJoin.TableAlias));
					}
					builder.Append(Line(joins, 5, indent, dbJoin.TableName));
					builder.Append(Line(joins, 6, indent, dbJoin.JoinType));

					// JoinOns
					builder.Append(JoinOnsCode(dbJoin, indent));

					// Join Columns
					if (dbJoin.Columns != null && dbJoin.Columns.Count > 0)
					{
						builder.Append(ColumnsCode(dbJoin.Columns, indent));
					}

					builder.Append(Line(joins, 7, indent));
					builder.Append(Line(joins, 8, indent));
				}
				builder.Append(Line(joins, 9, indent));
				retValue = builder.ToString();
			}
			return retValue;
		}

		// Returns the formatted line from the template array.
		/// <include path='items/Line/*' file='Doc/GenRequest.xml'/>
		public static string Line(string[] template, int index, params object[] values)
		{
			if (null == values || 0 == values.Length)
			{
				values = new object[] {
					null
				};
			}
			return string.Format(template[index].Substring(2), values);
		}

		// Creates and returns the OrderBynames create code.
		/// <include path='items/OrderByNamesCode/*' file='Doc/GenRequest.xml'/>
		public static string OrderByNamesCode(List<string> orderByNames, string indent = null)
		{
			string retValue = null;

			string[] orderBys = new string[]
			{
				"0 {0}dbRequest.OrderByNames = new List<string>\r\n",
				"1 {0}{{\r\n",
				"2 {0}  \"{1}\"\r\n",
				"3 {0}}};\r\n",
			};

			if (orderByNames != null && orderByNames.Count > 0)
			{
				StringBuilder builder = new StringBuilder(128);
				builder.AppendLine();
				builder.Append(Line(orderBys, 0, indent));
				builder.Append(Line(orderBys, 1, indent));

				bool isFirst = true;
				foreach (string columnName in orderByNames)
				{
					if (false == isFirst)
					{
						builder.AppendLine(",");
					}
					isFirst = false;

					builder.Append(Line(orderBys, 2, indent, columnName));
				}
				builder.Append(Line(orderBys, 3, indent));
				retValue = builder.ToString();
			}
			return retValue;
		}
		#endregion
	}
}
