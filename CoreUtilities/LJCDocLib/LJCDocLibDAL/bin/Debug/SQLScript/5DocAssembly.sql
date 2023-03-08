/* 4DocAssembly.sql */
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

declare @groupName nvarchar(60) = 'CommonLibraries';
exec sp_DAAddUnique @groupName, 'SystemBuild'
  , 'The System Build documentation.'
  , 'SystemBuild.html'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCNetCommon'
  , 'The .NET Common library. (DE1)'
  , '..\..\..\..\..\CoreAssemblies\LJCNetCommon\LJCNetCommon\bin\Debug\LJCNetCommon.xml'
  , null, 2;
exec sp_DAAddUnique @groupName, 'LJCExecuteScript'
  , 'A Console program to execute a SQL Script.'
  , '..\..\..\..\..\CoreUtilities\LJCExecuteScript\LJCExecuteScript\bin\Debug\LJCExecuteScript.xml'
  , null, 3;
exec sp_DAAddUnique @groupName, 'LJCAddressParserLib'
  , 'A library for parsing U.S. Address data. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCAddressParserLib\LJCAddressParserLib\bin\Debug\LJCAddressParserLib.xml'
  , null, 4;
exec sp_DAAddUnique @groupName, 'LJCWinFormCommon'
  , 'The WinForm Common library.'
  , '..\..\..\..\..\CoreAssemblies\LJCLibraries\LJCWinFormCommon\bin\Debug\LJCWinFormCommon.xml'
  , null, 5;
exec sp_DAAddUnique @groupName, 'LJCWinFormControls'
  , 'The WinForm Controls library.'
  , '..\..\..\..\..\CoreAssembliles\LJCLibraries\LJCWinFormControls\bin\Debug\LJCWinFormControls.xml'
  , null, 6;
exec sp_DAAddUnique @groupName, 'LJCGridDataLib'
  , 'The DbResult Grid Helper Library.'
  , '..\..\..\..\..\CoreAssemblies\LJCGridDataLib\LJCGridDataLib\bin\Debug\LJCGridDataLib.xml'
  , null, 7;
exec sp_DAAddUnique @groupName, 'LJCTextDataReaderLib'
  , 'The Text Data Reader library.'
  , '..\..\..\..\..\CoreAssemblies\LJCTextDataReader\LJCTextDataReaderLib\bin\Debug\LJCTextDataReaderLib.xml'
  , null, 8;
exec sp_DAAddUnique @groupName, 'DataDetail'
  , 'The DataDetail Dynamic Detail dialog. (D)'
  , '..\..\..\..\..\CoreAssemblies\DataDetail\DataDetail\bin\Debug\DataDetail.xml'
  , null, 9;
exec sp_DAAddUnique @groupName, 'LJCDataDetailLib'
  , 'The DataDetail Supporting Library. (D)'
  , '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailLib\bin\Debug\LJCDataDetailLib.xml'
  , null, 10;
exec sp_DAAddUnique @groupName, 'LJCDataDetailConsole'
  , 'The DataDetail Test Console. (D)'
  , '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailConsole\bin\Debug\LJCDataDetailConsole.xml'
  , null, 11;

set @groupName = 'DataLibraries';
exec sp_DAAddUnique @groupName, 'DataOverview'
  , 'The Data Service Libraries Overview.'
  , 'DataOverview.html'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCDataAccess'
  , 'The ADO.NET Data Access library. (DE)'
  , '..\..\..\..\..\CoreAssemblies\LJCDataAccess\LJCDataAccess\bin\Debug\LJCDataAccess.xml'
  , null, 2;
exec sp_DAAddUnique @groupName, 'LJCDBDataAccessLib'
  , 'The Message Data Access library. (DE4)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceLib\LJCDBDataAccessLib\bin\Debug\LJCDBDataAccessLib.xml'
  , null, 3;
exec sp_DAAddUnique @groupName, 'LJCDataAccessConfig'
  , 'The Data Access Configuration library.'
  , '..\..\..\..\..\CoreAssemblies\LJCDataAccessConfig\LJCDataAccessConfig\bin\Debug\LJCDataAccessConfig.xml'
  , null, 4;
exec sp_DAAddUnique @groupName, 'LJCDBClientLib'
  , 'The Data Service Client library. (DOE3)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBClientLib\LJCDBClientLib\bin\Debug\LJCDBClientLib.xml'
  , null, 5;
exec sp_DAAddUnique @groupName, 'LJCDBMessage'
  , 'The Data Service Message library. (DOGE2)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBMessage\LJCDBMessage\bin\Debug\LJCDBMessage.xml'
  , 'DBMessageGraph.jpg', 6;
exec sp_DAAddUnique @groupName, 'ForeignKeyManagerTest'
  , 'The testing application.'
  , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\ForeignKeyManagerTest\bin\Debug\ForeignKeyManagerTest.xml'
  , null, 7;
exec sp_DAAddUnique @groupName, 'LJCSQLUtilLib'
  , 'The SQL Utilities library.'
  , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib\bin\Debug\LJCSQLUtilLib.xml'
  , null, 8;
exec sp_DAAddUnique @groupName, 'LJCSQLUtilLibDAL'
  , 'The SQL Utilities Data Access Layer library.'
  , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLibDAL\bin\Debug\LJCSQLUtilLibDAL.xml'
  , null, 9;
exec sp_DAAddUnique @groupName, 'LJCDBServiceHost'
  , 'The LJCDBServiceLib windows host.'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceHost\bin\Debug\LJCDBServiceHost.xml'
  , null, 10;
exec sp_DAAddUnique @groupName, 'LJCDBServiceConsoleHost'
  , 'The LJCDBServiceLib console host.'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceConsoleHost\bin\Debug\LJCDBServiceConsoleHost.xml'
  , null, 11;
exec sp_DAAddUnique @groupName, 'LJCDBServiceLib'
  , 'The Data Service library. (ROE)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceLib\LJCDBServiceLib\bin\Debug\LJCDBServiceLib.xml'
  , null, 12;

set @groupName = 'CodeGen';
exec sp_DAAddUnique @groupName, 'LJCGenText'
  , 'The Gen Text console program.'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenText\bin\Debug\LJCGenText.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCGenTableCode'
  , 'A program to generate table related code.'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTableCode\bin\Debug\LJCGenTableCode.xml'
  , null, 2;
exec sp_DAAddUnique @groupName, 'LJCGenTextLib'
  , 'A Text Generator library. (RO)'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextLib\bin\Debug\LJCGenTextLib.xml'
  , null, 3;
exec sp_DAAddUnique @groupName, 'LJCGenTextEdit'
  , 'The GenText Editor Test Program. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextEdit\bin\Debug\LJCGenTextEdit.xml'
  , null, 4;

set @groupName = 'DocGen';
exec sp_DAAddUnique @groupName, 'LJCDocGen'
  , 'A program to generate code documentation.'
  , 'LJCDocGen.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCGenPageGen'
  , 'Genealogy Page Generation.'
  , '..\..\..\..\..\SampleApps\Genealogy\LJCGenPageGen\bin\Debug\LJCGenPageGen.xml'
  , null, 2;
exec sp_DAAddUnique @groupName, 'LJCDocGenLib'
  , 'The Code HTML Documentation Generator library. (O)'
  , 'LJCDocGenLib.xml'
  , null, 3;
exec sp_DAAddUnique @groupName, 'LJCDocLibDAL'
  , 'The Code Documentation Generator Data Access Layer library.'
  , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocLibDAL\bin\Debug\LJCDocLibDAL.xml'
  , null, 4;
exec sp_DAAddUnique @groupName, 'LJCDocObjLib'
  , 'The Code Documentation data object library. (DOG)'
  , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocObjLib\bin\Debug\LJCDocObjLib.xml'
  , 'DocLibDataGraph.jpg', 5;
exec sp_DAAddUnique @groupName, 'LJCDocXmlObjLib'
  , 'The Code Documentation XML object library. (DOG)'
  , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocXmlObjLib\bin\Debug\LJCDocXmlObjLib.xml'
  , 'DocLibXMLGraph.jpg', 6;
exec sp_DAAddUnique @groupName, 'LJCDocGroupEditor'
  , 'The Documentation Group Manager.'
  , '..\..\..\..\..\CoreUtilities\LJCDocGroupEditor\LJCDocGroupEditor\bin\Debug\LJCDocGroupEditor.xml'
  , null, 7;

set @groupName = 'DataTransform';
exec sp_DAAddUnique @groupName, 'LJCTransformManager'
  , 'A program to manage Data Transform data.'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCTransformManager\bin\Debug\LJCTransformManager.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCDataTransformProcess'
  , 'A program to Automate Data Processes.'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformProcess\bin\Debug\LJCDataTransformProcess.xml'
  , null, 2;
exec sp_DAAddUnique @groupName, 'CommonModuleLib'
  , 'A library for common Transform Process Modules. (D)'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCCommonModuleLib\bin\Debug\LJCCommonModuleLib.xml'
  , null, 3;
exec sp_DAAddUnique @groupName, 'LJCDataTransformDAL'
  , 'The Data Transform Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformDAL\bin\Debug\LJCDataTransformDAL.xml'
  , null, 4;
exec sp_DAAddUnique @groupName, 'TransformServiceTest'
  , 'A program to test the LJCDataTransformProcess library. (D)'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\TransformServiceTest\bin\Debug\TransformServiceTest.xml'
  , null, 5;

set @groupName = 'CVRManager';
exec sp_DAAddUnique @groupName, 'CVRManager'
  , 'The Contact Visit Record Manager.'
  , '..\..\..\..\..\SampleApps\CVRManager\CVRManager\bin\Debug\CVRManager.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'CVRDAL'
  , 'The CVR Data Access Layer library. (D)'
  , '..\..\..\..\..\SampleApps\CVRManager\CVRDAL\bin\Debug\CVRDAL.xml'
  , null, 2;

set @groupName = 'LJCSales';
exec sp_DAAddUnique @groupName, 'LJCSalesManager'
  , 'The Sales Manager program. (D)'
  , '..\..\..\..\..\SampleApps\LJCSales\LJCSalesManager\bin\Debug\LJCSalesManager.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCSalesDAL'
  , 'The Sales Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCSales\LJCSalesDAL\bin\Debug\LJCSalesDAL.xml'
  , null, 2;

set @groupName = 'LJCUnitMeasure';
exec sp_DAAddUnique @groupName, 'LJCUnitMeasure'
  , 'The Unit Measure program. (D)'
  , '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasure\bin\Debug\LJCUnitMeasure.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCUnitMeasureDAL'
  , 'The Unit Measure Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasureDAL\bin\Debug\LJCUnitMeasureDAL.xml'
  , null, 2;

set @groupName = 'FacilityManager';
exec sp_DAAddUnique @groupName, 'LJCFacilityManager'
  , 'A program to manage facility assets such as buildings, rooms, fixtures and equipment. (D)'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManager\bin\Debug\LJCFacilityManager.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCFacilityManagerDAL'
  , 'The LJCFacilityManager Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManagerDAL\bin\Debug\LJCFacilityManagerDAL.xml'
  , null, 2;
exec sp_DAAddUnique @groupName, 'FacilityForm'
  , 'The Facility Test program.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\FacilityForm\bin\Debug\LJCFacilityForm.xml'
  , null, 3;
exec sp_DAAddUnique @groupName, 'ModuleHost'
  , 'The FacilityManager Module Test program.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\ModuleHost\bin\Debug\ModuleHost.xml'
  , null, 4;

set @groupName = 'FacilityManagerSetup';
exec sp_DAAddUnique @groupName, 'LJCFacilityManagerSetup'
  , 'The Facility Manager Setup program.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManagerSetup\LJCFacilityManagerSetup\bin\Debug\LJCFacilityManagerSetup.xml'
  , null, 1;

set @groupName = 'RegionManager';
exec sp_DAAddUnique @groupName, 'LJCRegionManager'
  , 'A program to manage Region data. (D)'
  , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionManager\bin\Debug\LJCRegionManager.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCRegionDAL'
  , 'The Region Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionDAL\bin\Debug\LJCRegionDAL.xml'
  , null, 2;
exec sp_DAAddUnique @groupName, 'LJCRegionForm'
  , 'The Region Manager Test program.'
  , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionForm\bin\Debug\LJCRegionForm.xml'
  , null, 3;

set @groupName = 'AppManager';
exec sp_DAAddUnique @groupName, 'LJCAppManager'
  , 'A program to manage and host application modules.'
  , '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManager\bin\Debug\LJCAppManager.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCAppManagerDAL'
  , 'The LJCAppManager Data Access Layer library. (D)'
  , '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManagerDAL\bin\Debug\LJCAppManagerDAL.xml'
  , null, 2;

set @groupName = 'DocAppManager';
exec sp_DAAddUnique @groupName, 'LJCDocAppManager'
  , 'A program to manage Document images.'
  , '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManager\bin\Debug\LJCDocAppManager.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCDocAppManagerDAL'
  , 'The DocApp Manager Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManagerDAL\bin\Debug\LJCDocAppManagerDAL.xml'
  , null, 2;

set @groupName = 'DBViewDAL';
exec sp_DAAddUnique @groupName, 'LJCDBViewDAL'
  , 'The Data View library.'
  , '..\..\..\..\..\CoreAssemblies\LJCDBViewDAL\LJCDBViewDAL\bin\Debug\LJCDBViewDAL.xml'
  , null, 1;

set @groupName = 'ViewBuilder';
exec sp_DAAddUnique @groupName, 'LJCViewBuilder'
  , 'A program to create and edit Views. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCViewBuilder\LJCViewBuilder\bin\Debug\LJCViewBuilder.xml'
  , null, 1;

set @groupName = 'ViewEditor';
exec sp_DAAddUnique @groupName, 'LJCViewEditor'
  , 'A program to maintain View data. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditor\bin\Debug\LJCViewEditor.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'LJCViewEditorDAL'
  , 'The LJCViewEditor Data Access Library.'
  , '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditorDAL\bin\Debug\LJCViewEditorDAL.xml'
  , null, 2;

set @groupName = 'CodeLine';
exec sp_DAAddUnique @groupName, 'LJCCodeLineCounter'
  , 'The Code Line Counter console application.'
  , '..\..\..\..\..\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\LJCCodeLineCounter.xml'
  , null, 1;

set @groupName = 'TextInvasion';
exec sp_DAAddUnique @groupName, 'LJCTextInvasion'
  , 'A typing tudor game.'
  , '..\..\..\..\..\SampleApps\LJCTextInvasion\LJCTextInvasion\bin\Debug\LJCTextInvasion.xml'
  , null, 1;

set @groupName = 'LJCPagination';
exec sp_DAAddUnique @groupName, 'LJCDataPageList'
  , 'Database Pagination'
  , '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCDataPageList\bin\Debug\LJCDataPageList.xml'
  , null, 1;
exec sp_DAAddUnique @groupName, 'Text File Pagination'
  , 'A typing tudor game.'
  , '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCTextPageList\bin\Debug\LJCTextPageList.xml'
  , null, 2;
