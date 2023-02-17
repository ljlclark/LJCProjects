/* 3DocAssemblyData.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
select ID, DocAssemblyGroupID, Sequence, Name, Description, FileSpec, MainImage from DocAssembly;
*/

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'CommonLibraries');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'SystemBuild')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('SystemBuild', @DocAssemblyGroupID
   , 'The System Build documentation.'
   , 'SystemBuild.html'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCNetCommon')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCNetCommon', @DocAssemblyGroupID
   , 'The .NET Common library. (DE1)'
   , '..\..\..\..\..\CoreAssemblies\LJCNetCommon\LJCNetCommon\bin\Debug\LJCNetCommon.xml'
   , null, 2);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCExecuteScript')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCExecuteScript', @DocAssemblyGroupID
   , 'A Console program to execute a SQL Script.'
   , '..\..\..\..\..\CoreUtilities\LJCExecuteScript\LJCExecuteScript\bin\Debug\LJCExecuteScript.xml'
   , null, 3);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCAddressParserLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCAddressParserLib', @DocAssemblyGroupID
   , 'A library for parsing U.S. Address data. (D)'
   , '..\..\..\..\..\CoreUtilities\LJCAddressParserLib\LJCAddressParserLib\bin\Debug\LJCAddressParserLib.xml'
   , null, 4);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCWinFormCommon')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCWinFormCommon', @DocAssemblyGroupID
   , 'The WinForm Common library.'
   , '..\..\..\..\..\CoreAssemblies\LJCLibraries\LJCWinFormCommon\bin\Debug\LJCWinFormCommon.xml'
   , null, 5);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCWinFormControls')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCWinFormControls', @DocAssemblyGroupID
   , 'The WinForm Controls library.'
   , '..\..\..\..\..\CoreAssembliles\LJCLibraries\LJCWinFormControls\bin\Debug\LJCWinFormControls.xml'
   , null, 6);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCGridDataLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCGridDataLib', @DocAssemblyGroupID
   , 'The DbResult Grid Helper Library.'
   , '..\..\..\..\..\CoreAssemblies\LJCGridDataLib\LJCGridDataLib\bin\Debug\LJCGridDataLib.xml'
   , null, 7);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCTextDataReaderLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCTextDataReaderLib', @DocAssemblyGroupID
   , 'The Text Data Reader library.'
   , '..\..\..\..\..\CoreAssemblies\LJCTextDataReader\LJCTextDataReaderLib\bin\Debug\LJCTextDataReaderLib.xml'
   , null, 8);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'DataDetail')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('DataDetail', @DocAssemblyGroupID
   , 'The DataDetail Dynamic Detail dialog. (D)'
   , '..\..\..\..\..\CoreAssemblies\DataDetail\DataDetail\bin\Debug\DataDetail.xml'
   , null, 9);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDataDetailLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDataDetailLib', @DocAssemblyGroupID
   , 'The DataDetail Supporting Library. (D)'
   , '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailLib\bin\Debug\LJCDataDetailLib.xml'
   , null, 10);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDataDetailConsole')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDataDetailConsole', @DocAssemblyGroupID
   , 'The DataDetail Test Console. (D)'
   , '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailConsole\bin\Debug\LJCDataDetailConsole.xml'
   , null, 11);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'DataLibraries');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'DataOverview')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('DataOverview', @DocAssemblyGroupID
   , 'The Data Service Libraries Overview.'
   , 'DataOverview.html'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDataAccess')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDataAccess', @DocAssemblyGroupID
   , 'The ADO.NET Data Access library. (DE)'
   , '..\..\..\..\..\CoreAssemblies\LJCDataAccess\LJCDataAccess\bin\Debug\LJCDataAccess.xml'
   , null, 2);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDBDataAccessLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDBDataAccessLib', @DocAssemblyGroupID
   , 'The Message Data Access library. (DE4)'
   , '..\..\..\..\..\CoreAssemblies\LJCDBServiceLib\LJCDBDataAccessLib\bin\Debug\LJCDBDataAccessLib.xml'
   , null, 3);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDataAccessConfig')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDataAccessConfig', @DocAssemblyGroupID
   , 'The Data Access Configuration library.'
   , '..\..\..\..\..\CoreAssemblies\LJCDataAccessConfig\LJCDataAccessConfig\bin\Debug\LJCDataAccessConfig.xml'
   , null, 4);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDBClientLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDBClientLib', @DocAssemblyGroupID
   , 'The Data Service Client library. (DOE3)'
   , '..\..\..\..\..\CoreAssemblies\LJCDBClientLib\LJCDBClientLib\bin\Debug\LJCDBClientLib.xml'
   , null, 5);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDBMessage')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDBMessage', @DocAssemblyGroupID
   , 'The Data Service Message library. (DOGE2)'
   , '..\..\..\..\..\CoreAssemblies\LJCDBMessage\LJCDBMessage\bin\Debug\LJCDBMessage.xml'
   , 'DBMessageGraph.jpg', 6);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'ForeignKeyManagerTest')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('ForeignKeyManagerTest', @DocAssemblyGroupID
   , 'The testing application.'
   , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\ForeignKeyManagerTest\bin\Debug\ForeignKeyManagerTest.xml'
   , null, 7);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCSQLUtilLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCSQLUtilLib', @DocAssemblyGroupID
   , 'The SQL Utilities library.'
   , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib\bin\Debug\LJCSQLUtilLib.xml'
   , null, 8);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCSQLUtilLibDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCSQLUtilLibDAL', @DocAssemblyGroupID
   , 'The SQL Utilities Data Access Layer library.'
   , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLibDAL\bin\Debug\LJCSQLUtilLibDAL.xml'
   , null, 9);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDBServiceHost')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDBServiceHost', @DocAssemblyGroupID
   , 'The LJCDBServiceLib windows host.'
   , '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceHost\bin\Debug\LJCDBServiceHost.xml'
   , null, 10);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDBServiceConsoleHost')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDBServiceConsoleHost', @DocAssemblyGroupID
   , 'The LJCDBServiceLib console host.'
   , '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceConsoleHost\bin\Debug\LJCDBServiceConsoleHost.xml'
   , null, 11);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDBServiceLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDBServiceLib', @DocAssemblyGroupID
   , 'The Data Service library. (ROE)'
   , '..\..\..\..\..\CoreAssemblies\LJCDBServiceLib\LJCDBServiceLib\bin\Debug\LJCDBServiceLib.xml'
   , null, 12);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'CodeGen');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCGenText')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCGenText', @DocAssemblyGroupID
   , 'The Gen Text console program.'
   , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenText\bin\Debug\LJCGenText.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCGenTableCode')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCGenTableCode', @DocAssemblyGroupID
   , 'A program to generate table related code.'
   , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTableCode\bin\Debug\LJCGenTableCode.xml'
   , null, 2);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCGenTextLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCGenTextLib', @DocAssemblyGroupID
   , 'A Text Generator library. (RO)'
   , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextLib\bin\Debug\LJCGenTextLib.xml'
   , null, 3);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCGenTextEdit')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCGenTextEdit', @DocAssemblyGroupID
   , 'The GenText Editor Test Program. (D)'
   , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextEdit\bin\Debug\LJCGenTextEdit.xml'
   , null, 4);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'DocGen');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocGen')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocGen', @DocAssemblyGroupID
   , 'A program to generate code documentation.'
   , 'LJCDocGen.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCGenPageGen')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCGenPageGen', @DocAssemblyGroupID
   , 'Genealogy Page Generation.'
   , '..\..\..\..\..\SampleApps\Genealogy\LJCGenPageGen\bin\Debug\LJCGenPageGen.xml'
   , null, 2);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocGenLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocGenLib', @DocAssemblyGroupID
   , 'The Code HTML Documentation Generator library. (O)'
   , 'LJCDocGenLib.xml'
   , null, 3);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocLibDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocLibDAL', @DocAssemblyGroupID
   , 'The Code Documentation Generator Data Access Layer library.'
   , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocLibDAL\bin\Debug\LJCDocLibDAL.xml'
   , null, 4);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocObjLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocObjLib', @DocAssemblyGroupID
   , 'The Code Documentation data object library. (DOG)'
   , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocObjLib\bin\Debug\LJCDocObjLib.xml'
   , 'DocLibDataGraph.jpg', 5);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocXmlObjLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocXmlObjLib', @DocAssemblyGroupID
   , 'The Code Documentation XML object library. (DOG)'
   , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocXmlObjLib\bin\Debug\LJCDocXmlObjLib.xml'
   , 'DocLibXMLGraph.jpg', 6);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocGroupEditor')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocGroupEditor', @DocAssemblyGroupID
   , 'The Documentation Group Manager.'
   , '..\..\..\..\..\CoreUtilities\LJCDocGroupEditor\LJCDocGroupEditor\bin\Debug\LJCDocGroupEditor.xml'
   , null, 7);
go
