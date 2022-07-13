// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;

namespace LJCCodeLineCounter
{
	/// <summary>The Replacement processor.</summary>
	public class ReplaceProcessor
	{
		#region Public Methods

		// Processes the files and sub-folders in this folder.
		/// <include path='items/ProcessFolder/*' file='Doc/FindProcessor.xml'/>
		public void ProcessFolder(string folderPath, string folderPathName
			, string pattern)
		{
			string folderName = folderPath.Substring(folderPath.LastIndexOf("\\") + 1);
			if (CodeCommon.IsValidFolder(folderName))
			{
				// Process the files in this folder.
				string[] filePaths = Directory.GetFiles(folderPath, pattern);
				if (filePaths.Length > 0)
				{
					Console.WriteLine($"Folder: {folderPathName}");
					for (int fileIndex = 0; fileIndex < filePaths.Length; fileIndex++)
					{
						if (CodeCommon.IsValidFile(filePaths[fileIndex]))
						{
							ProcessFile(filePaths[fileIndex]);
						}
					}
				}

				// Recursively process child folders.
				string[] folderPaths = Directory.GetDirectories(folderPath);
				if (folderPaths.Length > 0)
				{
					for (int folderIndex = 0; folderIndex < folderPaths.Length; folderIndex++)
					{
						folderName = folderPaths[folderIndex].Substring(folderPaths[folderIndex].LastIndexOf("\\") + 1);
						string newFolderPathName = Path.Combine(folderPathName, folderName);
						ProcessFolder(folderPaths[folderIndex], newFolderPathName, pattern);
					}
				}
			}
		}
		#endregion

		#region Private Methods

		// Processes a file.
		private void ProcessFile(string filePath)
		{
			mOutputWriter = File.AppendText(filePath);
			string fileName = Path.GetFileName(filePath);
			string backupFilePath = CodeCommon.CreateBackup(filePath);

			Console.WriteLine($"{fileName}");
			IEnumerable<string> lines = File.ReadLines(backupFilePath);
			foreach (string line in lines)
			{
				ReplaceValues(line);
			}
			CloseOutputWriter();
		}

		// Performs a hard-coded value replacement.
		private void ReplaceValues(string line)
		{
			string outputLine = line;
			int startPosition;
			int stopPosition;

			startPosition = line.IndexOf("<para>Syntax:");
			if (startPosition >= 0)
			{
				stopPosition = line.IndexOf(@"</para>", startPosition + 1);
				if (stopPosition > 0)
				{
					stopPosition += 7;
					outputLine = line.Substring(0, startPosition);
					outputLine += line.Substring(stopPosition);
				}
			}

			string tempLine = outputLine;
			startPosition = tempLine.IndexOf("<remarks>");
			if (startPosition >= 0)
			{
				stopPosition = tempLine.IndexOf(@"</remarks>", startPosition + 1);
				if (stopPosition > 0)
				{
					stopPosition += 10;
					outputLine = tempLine.Substring(0, startPosition);
					outputLine += tempLine.Substring(stopPosition);
					if (outputLine.Trim() == "///")
					{
						outputLine = null;
					}
				}
			}

			if (outputLine.Trim() != null)
			{
				mOutputWriter.WriteLine(outputLine);
			}
		}

		// Closes the stream writer.
		private void CloseOutputWriter()
		{
			if (mOutputWriter != null)
			{
				mOutputWriter.Close();
			}
		}
		#endregion

		#region Class Data

		private StreamWriter mOutputWriter;
		#endregion
	}
}
