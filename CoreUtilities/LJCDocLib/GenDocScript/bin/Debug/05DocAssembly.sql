/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 05DocAssembly.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select DocAssembly.ID 'DocAssembly' , DocAssembly.Name 'Assembly Name',
  dag.Name as GroupName, Description, FileSpec, MainImage
from DocAssembly
left join DocAssemblyGroup as dag on DocAssemblyGroupID = dag.ID
order by dag.Sequence, DocAssembly.Sequence
*/

declare @groupName nvarchar(60);
declare @seq int;

set @groupName = 'CommonLibraries';
set @seq = 1;
exec sp_DAAddUnique @heading, 'SystemBuild'
  , 'The System Build documentation.'
  , 'SystemBuild.html'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCNetCommon'
  , 'The .NET Common library. (DE1)'
  , '..\..\..\..\..\CoreAssemblies\LJCNetCommon\LJCNetCommon\bin\Debug\LJCNetCommon.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCExecuteScript'
  , 'A Console program to execute a SQL Script.'
  , '..\..\..\..\..\CoreUtilities\LJCExecuteScript\LJCExecuteScript\bin\Debug\LJCExecuteScript.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCAddressParserLib'
  , 'A library for parsing U.S. Address data. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCAddressParserLib\LJCAddressParserLib\bin\Debug\LJCAddressParserLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCWinFormCommon'
  , 'The WinForm Common library.'
  , '..\..\..\..\..\CoreAssemblies\LJCLibraries\LJCWinFormCommon\bin\Debug\LJCWinFormCommon.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCWinFormControls'
  , 'The WinForm Controls library.'
  , '..\..\..\..\..\CoreAssemblies\LJCLibraries\LJCWinFormControls\bin\Debug\LJCWinFormControls.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCGridDataLib'
  , 'The DbResult Grid Helper Library.'
  , '..\..\..\..\..\CoreAssemblies\LJCGridDataLib\LJCGridDataLib\bin\Debug\LJCGridDataLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCTextDataReaderLib'
  , 'The Text Data Reader library.'
  , '..\..\..\..\..\CoreAssemblies\LJCTextDataReader\LJCTextDataReaderLib\bin\Debug\LJCTextDataReaderLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'DataDetail'
  , 'The DataDetail Dynamic Detail dialog. (D)'
  , '..\..\..\..\..\CoreAssemblies\DataDetail\DataDetail\bin\Debug\DataDetail.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDataDetailLib'
  , 'The DataDetail Supporting Library. (D)'
  , '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailLib\bin\Debug\LJCDataDetailLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDataDetailConsole'
  , 'The DataDetail Test Console. (D)'
  , '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailConsole\bin\Debug\LJCDataDetailConsole.xml'
  , '', @seq;

set @groupName = 'DataLibraries';
set @seq = 1;
exec sp_DAAddUnique @heading, 'DataOverview'
  , 'The Data Service Libraries Overview.'
  , 'DataOverview.html'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDataAccess'
  , 'The ADO.NET Data Access library. (DE)'
  , '..\..\..\..\..\CoreAssemblies\LJCDataAccess\LJCDataAccess\bin\Debug\LJCDataAccess.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDBDataAccessLib'
  , 'The Message Data Access library. (DE4)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceLib\LJCDBDataAccessLib\bin\Debug\LJCDBDataAccessLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDataAccessConfig'
  , 'The Data Access Configuration library. (DO)'
  , '..\..\..\..\..\CoreAssemblies\LJCDataAccessConfig\LJCDataAccessConfig\bin\Debug\LJCDataAccessConfig.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDBClientLib'
  , 'The Data Service Client library. (DOE3)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBClientLib\LJCDBClientLib\bin\Debug\LJCDBClientLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDBMessage'
  , 'The Data Service Message library. (DOGE2)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBMessage\LJCDBMessage\bin\Debug\LJCDBMessage.xml'
  , 'DBMessageGraph.jpg', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'ForeignKeyManagerTest'
  , 'The testing application.'
  , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\ForeignKeyManagerTest\bin\Debug\ForeignKeyManagerTest.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCSQLUtilLib'
  , 'The SQL Utilities library. (DO)'
  , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib\bin\Debug\LJCSQLUtilLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCSQLUtilLibDAL'
  , 'The SQL Utilities Data Access Layer library.'
  , '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLibDAL\bin\Debug\LJCSQLUtilLibDAL.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDBServiceHost'
  , 'The LJCDBServiceLib windows host.'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceHost\bin\Debug\LJCDBServiceHost.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDBServiceConsoleHost'
  , 'The LJCDBServiceLib console host.'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceConsoleHost\bin\Debug\LJCDBServiceConsoleHost.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDBServiceLib'
  , 'The Data Service library. (ROE)'
  , '..\..\..\..\..\CoreAssemblies\LJCDBServiceLib\LJCDBServiceLib\bin\Debug\LJCDBServiceLib.xml'
  , '', @seq;

set @groupName = 'CodeGen';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCGenText'
  , 'The Gen Text console program. (RO)'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenText\bin\Debug\LJCGenText.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCGenTableCode'
  , 'A program to generate table related code.'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTableCode\bin\Debug\LJCGenTableCode.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCGenTextLib'
  , 'A Text Generator library. (RO)'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextLib\bin\Debug\LJCGenTextLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCGenTextEdit'
  , 'The GenText Editor Test Program. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextEdit\bin\Debug\LJCGenTextEdit.xml'
  , '', @seq;

set @groupName = 'DocGen';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCDocGen'
  , 'A program to generate code documentation.'
  , 'LJCDocGen.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDocGenLib'
  , 'The Code HTML Documentation Generator library.'
  , 'LJCDocGenLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDocLibDAL'
  , 'The Code Documentation Generator Data Access Layer library. (DO)'
  , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocLibDAL\bin\Debug\LJCDocLibDAL.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDocObjLib'
  , 'The Code Documentation data object library. (DOG)'
  , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocObjLib\bin\Debug\LJCDocObjLib.xml'
  , 'DocLibDataGraph.jpg', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDocXmlObjLib'
  , 'The Code Documentation XML object library. (DOG)'
  , '..\..\..\..\..\CoreUtilities\LJCDocLib\LJCDocXmlObjLib\bin\Debug\LJCDocXmlObjLib.xml'
  , 'DocLibXMLGraph.jpg', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCGenDocEdit'
  , 'The GenDoc Group Editor'
  , '..\..\..\..\..\CoreUtilities\LJCDocGroupEditor\LJCDocGroupEditor\bin\Debug\LJCDocGroupEditor.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCGenPageGen'
  , 'Genealogy Page Generation.'
  , '..\..\..\..\..\SampleApps\Genealogy\LJCGenPageGen\bin\Debug\LJCGenPageGen.xml'
  , '', @seq;

set @groupName = 'DataTransform';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCTransformManager'
  , 'A program to manage Data Transform data. (RO)'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCTransformManager\bin\Debug\LJCTransformManager.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDataTransformProcess'
  , 'A program to Automate Data Processes.'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformProcess\bin\Debug\LJCDataTransformProcess.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'CommonModuleLib'
  , 'A library for common Transform Process Modules. (D)'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCCommonModuleLib\bin\Debug\LJCCommonModuleLib.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDataTransformDAL'
  , 'The Data Transform Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformDAL\bin\Debug\LJCDataTransformDAL.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'TransformServiceTest'
  , 'A program to test the LJCDataTransformProcess library. (D)'
  , '..\..\..\..\..\SampleApps\LJCDataTransform\TransformServiceTest\bin\Debug\TransformServiceTest.xml'
  , '', @seq;

set @groupName = 'CVRManager';
set @seq = 1;
exec sp_DAAddUnique @heading, 'CVRManager'
  , 'The Contact Visit Record Manager.'
  , '..\..\..\..\..\SampleApps\CVRManager\CVRManager\bin\Debug\CVRManager.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'CVRDAL'
  , 'The CVR Data Access Layer library. (D)'
  , '..\..\..\..\..\SampleApps\CVRManager\CVRDAL\bin\Debug\CVRDAL.xml'
  , '', @seq;

set @groupName = 'LJCSales';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCSalesManager'
  , 'The Sales Manager program. (DO)'
  , '..\..\..\..\..\SampleApps\LJCSales\LJCSalesManager\bin\Debug\LJCSalesManager.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCSalesDAL'
  , 'The Sales Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCSales\LJCSalesDAL\bin\Debug\LJCSalesDAL.xml'
  , '', @seq;

set @groupName = 'LJCUnitMeasure';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCUnitMeasure'
  , 'The Unit Measure program. (D)'
  , '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasure\bin\Debug\LJCUnitMeasure.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCUnitMeasureDAL'
  , 'The Unit Measure Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasureDAL\bin\Debug\LJCUnitMeasureDAL.xml'
  , '', @seq;

set @groupName = 'FacilityManager';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCFacilityManager'
  , 'A program to manage facility assets such as buildings, rooms, fixtures and equipment. (D)'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManager\bin\Debug\LJCFacilityManager.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCFacilityManagerDAL'
  , 'The LJCFacilityManager Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManagerDAL\bin\Debug\LJCFacilityManagerDAL.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'FacilityForm'
  , 'The Facility Test program.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\FacilityForm\bin\Debug\LJCFacilityForm.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'ModuleHost'
  , 'The FacilityManager Module Test program.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManager\ModuleHost\bin\Debug\ModuleHost.xml'
  , '', @seq;

set @groupName = 'FacilityManagerSetup';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCFacilityManagerSetup'
  , 'The Facility Manager Setup program.'
  , '..\..\..\..\..\SampleApps\LJCFacilityManagerSetup\LJCFacilityManagerSetup\bin\Debug\LJCFacilityManagerSetup.xml'
  , '', @seq;

set @groupName = 'RegionManager';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCRegionManager'
  , 'A program to manage Region data. (DO)'
  , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionManager\bin\Debug\LJCRegionManager.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCRegionDAL'
  , 'The Region Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionDAL\bin\Debug\LJCRegionDAL.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCRegionForm'
  , 'The Region Manager Test program.'
  , '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionForm\bin\Debug\LJCRegionForm.xml'
  , '', @seq;

set @groupName = 'AppManager';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCAppManager'
  , 'A program to manage and host application modules. (DO)'
  , '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManager\bin\Debug\LJCAppManager.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCAppManagerDAL'
  , 'The LJCAppManager Data Access Layer library. (D)'
  , '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManagerDAL\bin\Debug\LJCAppManagerDAL.xml'
  , '', @seq;

set @groupName = 'DocAppManager';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCDocAppManager'
  , 'A program to manage Document images. (O)'
  , '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManager\bin\Debug\LJCDocAppManager.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCDocAppManagerDAL'
  , 'The DocApp Manager Data Access Layer library.'
  , '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManagerDAL\bin\Debug\LJCDocAppManagerDAL.xml'
  , '', @seq;

set @groupName = 'DBViewDAL';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCDBViewDAL'
  , 'The Data View library.'
  , '..\..\..\..\..\CoreAssemblies\LJCDBViewDAL\LJCDBViewDAL\bin\Debug\LJCDBViewDAL.xml'
  , '', @seq;

set @groupName = 'ViewBuilder';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCViewBuilder'
  , 'A program to create and edit Views. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCViewBuilder\LJCViewBuilder\bin\Debug\LJCViewBuilder.xml'
  , '', @seq;

set @groupName = 'ViewEditor';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCViewEditor'
  , 'A program to maintain View data. (D)'
  , '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditor\bin\Debug\LJCViewEditor.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'LJCViewEditorDAL'
  , 'The LJCViewEditor Data Access Library.'
  , '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditorDAL\bin\Debug\LJCViewEditorDAL.xml'
  , '', @seq;

set @groupName = 'CodeLine';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCCodeLineCounter'
  , 'The Code Line Counter console application.'
  , '..\..\..\..\..\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\LJCCodeLineCounter.xml'
  , '', @seq;

set @groupName = 'TextInvasion';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCTextInvasion'
  , 'A typing tudor game.'
  , '..\..\..\..\..\SampleApps\LJCTextInvasion\LJCTextInvasion\bin\Debug\LJCTextInvasion.xml'
  , '', @seq;

set @groupName = 'LJCPagination';
set @seq = 1;
exec sp_DAAddUnique @heading, 'LJCDataPageList'
  , 'Database Pagination'
  , '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCDataPageList\bin\Debug\LJCDataPageList.xml'
  , '', @seq;
set @seq += 1;
exec sp_DAAddUnique @heading, 'Text File Pagination'
  , 'A typing tudor game.'
  , '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCTextPageList\bin\Debug\LJCTextPageList.xml'
  , '', @seq;
