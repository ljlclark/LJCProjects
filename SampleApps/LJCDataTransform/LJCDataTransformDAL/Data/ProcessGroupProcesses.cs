// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupProcesses.cs
using System;
using System.Collections.Generic;

namespace LJCDataTransformDAL
{
	/// <summary>Represents a collection of ProcessGroupProcess objects.</summary>
	public class ProcessGroupProcesses : List<ProcessGroupProcess>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ProcessGroupProcesses.xml'/>
		public ProcessGroupProcess Add(int processGroupID, int processID, int sequence)
		{
			ProcessGroupProcess retValue = null;

			if (processGroupID > 0
				&& processID > 0)
			{
				retValue = new ProcessGroupProcess()
				{
					ProcessGroupID = processGroupID,
					DataProcessID = processID,
					Sequence = sequence
				};
				Add(retValue);
			}
			return retValue;
		}
		#endregion
	}
}
