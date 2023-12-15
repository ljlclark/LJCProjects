// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LogTime.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LJCNetCommon;

namespace CVRManager
{
	// Logs the Elapsed time.
	internal class LogTime
	{
		#region Constructors

		// Initializes an object instance.
		internal LogTime(string fileSpec, string rangeName = null
			, bool useExisting = false)
		{
			mLogRanges = new LogRanges();
			FileSpec = fileSpec;
			mCurrentProcess = Process.GetCurrentProcess();
			if (!useExisting)
			{
				File.WriteAllText(FileSpec, "");
			}
			if (NetString.HasValue(rangeName))
			{
				Start(rangeName);
			}
		}
		#endregion

		#region Methods

		// Start the timer. 
		internal string Start(string rangeName)
		{
			LogRange logRange = mLogRanges.Add(rangeName);
			WriteLogLine("Start - {0}", rangeName);
			logRange.StartTime = mCurrentProcess.TotalProcessorTime;
			return rangeName;
		}

		// Stop the timer.
		internal void Stop(string rangeName)
		{
			LogRange logRange = mLogRanges.SearchName(rangeName);
			if (logRange != null)
			{
				TimeSpan stopTime = mCurrentProcess.TotalProcessorTime;
				TimeSpan elapsedTime = stopTime - logRange.StartTime;
				WriteLogLine("Stop -  {0} - Time: {1}", logRange.RangeName
					, elapsedTime.TotalSeconds);
				mLogRanges.Remove(logRange);
			}
		}

		// Write to the log file.
		internal void WriteLogLine(string formatText, params object[] parameters)
		{
			NetFile.WriteLogLine(FileSpec, formatText, parameters);
		}
		#endregion

		#region Properties

		// Gets the FileSpec value.
		internal string FileSpec { get; private set; }
		#endregion

		#region Class Data

		private readonly Process mCurrentProcess;
		private readonly LogRanges mLogRanges;
		#endregion
	}
}
