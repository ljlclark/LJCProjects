// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;

namespace LJCDataAccessConfig
{
	// Represents a Connection String template.
	/// <include path='items/ConnectionTemplate/*' file='Doc/ConnectionTemplate.xml'/>
	public class ConnectionTemplate : IComparable<ConnectionTemplate>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ConnectionTemplateC/*' file='Doc/ConnectionTemplate.xml'/>
		public ConnectionTemplate()
		{
		}
		#endregion

		#region Methods

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='Doc/ConnectionTemplate.xml'/>
		public ConnectionTemplate Clone()
		{
			ConnectionTemplate retValue = MemberwiseClone() as ConnectionTemplate;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='Doc/ConnectionTemplate.xml'/>
		public override string ToString()
		{
			return Name;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='Doc/ConnectionTemplate.xml'/>
		public int CompareTo(ConnectionTemplate other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = Name.CompareTo(other.Name);

				// Not case sensitive.
				retValue = string.Compare(Name, other.Name, true);
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the connection type name.</summary>
		public string Name { get; set; }

		/// <summary>Gets or sets the connection string template.</summary>
		public string Template { get; set; }
		#endregion
	}
}
