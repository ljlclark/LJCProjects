// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Businesses.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class Businesses : List<Business> { }
}
