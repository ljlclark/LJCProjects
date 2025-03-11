// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ErrorMessage.cs
using System;

namespace LJCAddressParserLib
{
	/// <summary>
	/// Represents an Address Parsing error.
	/// </summary>
	public class ErrorMessage
	{
		/// <summary>Gets or sets the error line.</summary>
		public string Line { get; set; }

		/// <summary>Gets or sets the error message.</summary>
		public string Message { get; set; }
	}
}
