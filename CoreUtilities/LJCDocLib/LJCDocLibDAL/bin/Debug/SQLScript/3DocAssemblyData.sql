/* 3DocAssemblyData.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
select
  ID, DocAssemblyGroupID, Sequence, Name, Description, FileSpec, MainImage
from DocAssembly;
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

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'DataTransform');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCTransformManager')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCTransformManager', @DocAssemblyGroupID
   , 'A program to manage Data Transform data.'
   , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCTransformManager\bin\Debug\LJCTransformManager.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDataTransformProcess')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDataTransformProcess', @DocAssemblyGroupID
   , 'A program to Automate Data Processes.'
   , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformProcess\bin\Debug\LJCDataTransformProcess.xml'
   , null, 2);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'CommonModuleLib')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('CommonModuleLib', @DocAssemblyGroupID
   , 'A library for common Transform Process Modules. (D)'
   , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCCommonModuleLib\bin\Debug\LJCCommonModuleLib.xml'
   , null, 3);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDataTransformDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDataTransformDAL', @DocAssemblyGroupID
   , 'The Data Transform Data Access Layer library.'
   , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformDAL\bin\Debug\LJCDataTransformDAL.xml'
   , null, 4);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'TransformServiceTest')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('TransformServiceTest', @DocAssemblyGroupID
   , 'A program to test the LJCDataTransformProcess library. (D)'
   , '..\..\..\..\..\SampleApps\LJCDataTransform\TransformServiceTest\bin\Debug\TransformServiceTest.xml'
   , null, 5);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'CVRManager');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'CVRManager')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('CVRManager', @DocAssemblyGroupID
   , 'The Contact Visit Record Manager.'
   , '..\..\..\..\..\SampleApps\CVRManager\CVRManager\bin\Debug\CVRManager.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'CVRDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('CVRDAL', @DocAssemblyGroupID
   , 'The CVR Data Access Layer library. (D)'
   , '..\..\..\..\..\SampleApps\CVRManager\CVRDAL\bin\Debug\CVRDAL.xml'
   , null, 2);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'LJCSales');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCSalesManager')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCSalesManager', @DocAssemblyGroupID
   , 'The Sales Manager program. (D)'
   , '..\..\..\..\..\SampleApps\LJCSales\LJCSalesManager\bin\Debug\LJCSalesManager.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCSalesDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCSalesDAL', @DocAssemblyGroupID
   , 'The Sales Data Access Layer library.'
   , '..\..\..\..\..\SampleApps\LJCSales\LJCSalesDAL\bin\Debug\LJCSalesDAL.xml'
   , null, 2);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'LJCUnitMeasure');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCUnitMeasure')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCUnitMeasure', @DocAssemblyGroupID
   , 'The Unit Measure program. (D)'
   , '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasure\bin\Debug\LJCUnitMeasure.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCUnitMeasureDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCUnitMeasureDAL', @DocAssemblyGroupID
   , 'The Unit Measure Data Access Layer library.'
   , '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasureDAL\bin\Debug\LJCUnitMeasureDAL.xml'
   , null, 2);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'FacilityManager');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCFacilityManager')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCFacilityManager', @DocAssemblyGroupID
   , 'A program to manage facility assets such as buildings, rooms, fixtures and equipment. (D)'
   , '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManager\bin\Debug\LJCFacilityManager.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCFacilityManagerDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCFacilityManagerDAL', @DocAssemblyGroupID
   , 'The LJCFacilityManager Data Access Layer library.'
   , '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManagerDAL\bin\Debug\LJCFacilityManagerDAL.xml'
   , null, 2);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'FacilityForm')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('FacilityForm', @DocAssemblyGroupID
   , 'The Facility Test program.'
   , '..\..\..\..\..\SampleApps\LJCFacilityManager\FacilityForm\bin\Debug\LJCFacilityForm.xml'
   , null, 3);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'ModuleHost')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('ModuleHost', @DocAssemblyGroupID
   , 'The FacilityManager Module Test program.'
   , '..\..\..\..\..\SampleApps\LJCFacilityManager\ModuleHost\bin\Debug\ModuleHost.xml'
   , null, 4);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'FacilityManagerSetup');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCFacilityManagerSetup')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCFacilityManagerSetup', @DocAssemblyGroupID
   , 'The Facility Manager Setup program.'
   , '..\..\..\..\..\SampleApps\LJCFacilityManagerSetup\LJCFacilityManagerSetup\bin\Debug\LJCFacilityManagerSetup.xml'
   , null, 1);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'RegionManager');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCRegionManager')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCRegionManager', @DocAssemblyGroupID
   , 'A program to manage Region data. (D)'
   , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionManager\bin\Debug\LJCRegionManager.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCRegionDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCRegionDAL', @DocAssemblyGroupID
   , 'The Region Data Access Layer library.'
   , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionDAL\bin\Debug\LJCRegionDAL.xml'
   , null, 2);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCRegionForm')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCRegionForm', @DocAssemblyGroupID
   , 'The Region Manager Test program.'
   , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionForm\bin\Debug\LJCRegionForm.xml'
   , null, 3);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'AppManager');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCAppManager')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCAppManager', @DocAssemblyGroupID
   , 'A program to manage and host application modules.'
   , '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManager\bin\Debug\LJCAppManager.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCAppManagerDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCAppManagerDAL', @DocAssemblyGroupID
   , 'The LJCAppManager Data Access Layer library. (D)'
   , '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManagerDAL\bin\Debug\LJCAppManagerDAL.xml'
   , null, 2);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'DocAppManager');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocAppManager')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocAppManager', @DocAssemblyGroupID
   , 'A program to manage Document images.'
   , '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManager\bin\Debug\LJCDocAppManager.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDocAppManagerDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDocAppManagerDAL', @DocAssemblyGroupID
   , 'The DocApp Manager Data Access Layer library.'
   , '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManagerDAL\bin\Debug\LJCDocAppManagerDAL.xml'
   , null, 2);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'DBViewDAL');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDBViewDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDBViewDAL', @DocAssemblyGroupID
   , 'The Data View library.'
   , '..\..\..\..\..\CoreAssemblies\LJCDBViewDAL\LJCDBViewDAL\bin\Debug\LJCDBViewDAL.xml'
   , null, 1);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'ViewBuilder');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCViewBuilder')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCViewBuilder', @DocAssemblyGroupID
   , 'A program to create and edit Views. (D)'
   , '..\..\..\..\..\CoreUtilities\LJCViewBuilder\LJCViewBuilder\bin\Debug\LJCViewBuilder.xml'
   , null, 1);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'ViewEditor');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCViewEditor')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCViewEditor', @DocAssemblyGroupID
   , 'A program to maintain View data. (D)'
   , '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditor\bin\Debug\LJCViewEditor.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCViewEditorDAL')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCViewEditorDAL', @DocAssemblyGroupID
   , 'The LJCViewEditor Data Access Library.'
   , '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditorDAL\bin\Debug\LJCViewEditorDAL.xml'
   , null, 2);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'CodeLine');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCCodeLineCounter')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCCodeLineCounter', @DocAssemblyGroupID
   , 'The Code Line Counter console application.'
   , '..\..\..\..\..\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\LJCCodeLineCounter.xml'
   , null, 1);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'TextInvasion');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCTextInvasion')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCTextInvasion', @DocAssemblyGroupID
   , 'A typing tudor game.'
   , '..\..\..\..\..\SampleApps\LJCTextInvasion\LJCTextInvasion\bin\Debug\LJCTextInvasion.xml'
   , null, 1);
go

declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = 'LJCPagination');
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'LJCDataPageList')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('LJCDataPageList', @DocAssemblyGroupID
   , 'Database Pagination'
   , '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCDataPageList\bin\Debug\LJCDataPageList.xml'
   , null, 1);
IF NOT EXISTS (select ID from DocAssembly
  where Name = 'Text File Pagination')
insert into DocAssembly
 (Name, DocAssemblyGroupID, Description, FileSpec, MainImage, Sequence)
 values ('Text File Pagination', @DocAssemblyGroupID
   , 'A typing tudor game.'
   , '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCTextPageList\bin\Debug\LJCTextPageList.xml'
   , null, 2);
go
