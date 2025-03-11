// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformServiceTest/Program.cs
using System;
using System.Collections.Generic;
using LJCAddressParserLib;

namespace TransformServiceTest
{
	// The program entry point class.
	/// <include path='items/Program/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
	public class Program
	{
		// The program entry point fuction.
		/// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
		static void Main(string[] args)
		{
			bool addressTesting = false;
			if (!addressTesting)
			{
				if (args.Length < 2)
				{
					Console.WriteLine("Syntax: TransformServiceTest processGroupID dataConfigName");
					Console.WriteLine("Press any key to continue . . .");
					Console.ReadKey();
				}
				else
				{
					int.TryParse(args[0], out int processGroupID);
					string dataConfigName = args[1];

					// ToDo: Create a TransformService windows service.
					LJCTransformService transformService = new LJCTransformService();
					transformService.StartTransform(processGroupID, dataConfigName);
				}
			}
			else
			{
				TestAddressParsing();
			}
		}

		// Test Address Parsing.
		private static void TestAddressParsing()
		{
			TestDeliveryLine();
			TestLastLine();
		}

		// Test the address Delivery line.
		private static void TestDeliveryLine()
		{
			StandardAddress standardAddress = new StandardAddress();
			List<string> deliveryLines = new List<string>
			{
				"123A North South Street South East Apartment E",
				"123A North Street South North Apartment E",
				"123 N South Street E Front",
				"123 South Street #1"
			};
			foreach (string deliveryLine in deliveryLines)
			{
				standardAddress.ParseDeliveryAddressLine(deliveryLine);
				Console.WriteLine(deliveryLine);
				Console.WriteLine(standardAddress.DeliveryAddressLine);
				Console.WriteLine();
			}

			foreach (ErrorMessage errorMessage in standardAddress.ErrorMessages)
			{
				Console.WriteLine(errorMessage.Line);
				Console.WriteLine(errorMessage.Message);
				Console.WriteLine();
			}
			Console.WriteLine("Press the ENTER key to continue.");
			Console.ReadLine();
		}

		// Test the addess Last line.
		private static void TestLastLine()
		{
			StandardAddress standardAddress = new StandardAddress();
			List<string> lastLines = new List<string>
			{
				"New Orleans, LA 12345-1234",
				"New Orleans,LA 12345 - 1234",
				"New Orleans ,LA 12345 -1234",
				"New Orleans , LA 12345- 1234",
				"New Orleans LX 12345",
				"New Orleans, LA 1234",
				"New Orleans, LA 123"
			};
			foreach (string lastLine in lastLines)
			{
				standardAddress.ParseLastLine(lastLine);
				Console.WriteLine(lastLine);
				Console.WriteLine(standardAddress.LastLine);
				Console.WriteLine();
			}

			foreach (ErrorMessage errorMessage in standardAddress.ErrorMessages)
			{
				Console.WriteLine(errorMessage.Line);
				Console.WriteLine(errorMessage.Message);
				Console.WriteLine();
			}
			Console.WriteLine("Press the ENTER key to continue.");
			Console.ReadLine();
		}

		//// The method to add multiple process records.
		//private static void CreateProcesses(DbServiceRef dbServiceRef, int beginIndex
		//	, int endIndex)
		//{
		//	DataProcessManager processManager = new DataProcessManager(dbServiceRef, "DataProcess");

		//	for (long index = beginIndex; index < endIndex; index++)
		//	{
		//		string name = $"Name{index}";
		//		string description = $"Description{index}";
		//		//processManager.Add(new DataProcess(name, description));
		//		if (NetString.HasValue(processManager.ErrorText))
		//		{
		//			Console.WriteLine(processManager.ErrorText);
		//		}
		//	}
		//}
	}
}
