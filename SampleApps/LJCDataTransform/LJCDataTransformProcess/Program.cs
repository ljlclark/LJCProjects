// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataTransformProcess/Program.cs
using System;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCDataTransformDAL;

namespace LJCDataTransformProcess
{
	// The DataProcess process entry point class.
	/// <include path='items/Program/*' file='Doc/ProjectDataTransformProcess.xml'/>
	public class Program
	{
		// The program entry point function.
		// For testing only.
		/// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
		static void Main(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("Syntax: LJC.DataTransformProcess processGroupID processID dataConfigName");
				Console.WriteLine("Press any key to continue . . .");
				Console.ReadKey();
			}
			else
			{
				int.TryParse(args[0], out int groupID);
				int.TryParse(args[1], out int processID);
				string dataConfigName = args[2];

				DbServiceRef dbServiceRef = new DbServiceRef()
				{
					DbService = new DbService()
				};
				LJCDataProcessHelper ljcProcessHelper = new LJCDataProcessHelper(dbServiceRef, dataConfigName);

				LJCTransformProcess transformProcess = new LJCTransformProcess(dataConfigName);

				// Resets all to status "Available".
				ljcProcessHelper.ResetToStatus(groupID, StepTaskStatus.Available);

				// Resets only the processes to "Active".
				ljcProcessHelper.ResetToStatus(groupID, StepTaskStatus.Active, true);

				transformProcess.StartProcess(groupID, processID);
			}
		}
	}
}
