// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskSources.cs
using System;
using System.Collections.Generic;

namespace LJCDataTransformDAL
{
	// Represents a collection of TaskSource objects. 
	/// <include path='items/TaskSources/*' file='Doc/TaskSources.xml'/>
	public class TaskSources : List<TaskSource>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/TaskSources.xml'/>
		public TaskSource Add(int stepTaskID, int dataSourceID)
		{
			TaskSource retValue = null;

			if (stepTaskID > 0
				&& dataSourceID > 0)
			{
				retValue = new TaskSource()
				{
					StepTaskID = stepTaskID,
					DataSourceID = dataSourceID
				};
				Add(retValue);
			}
			return retValue;
		}
		#endregion
	}
}
