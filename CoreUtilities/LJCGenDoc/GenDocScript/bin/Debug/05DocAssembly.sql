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
order by dag.Sequence, DocAssembly.Sequence;
*/

declare @groupName nvarchar(60);
declare @seq int;

set @groupName = 'CommonLibraries';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'SystemBuild',
  'The System Build documentation.',
  'SystemBuild.html',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCNetCommon',
  'The .NET Common library. (DE1)',
  '..\..\..\..\..\CoreAssemblies\LJCNetCommon\LJCNetCommon\bin\Debug\LJCNetCommon.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCExecuteScript',
  'A Console program to execute a SQL Script.',
  '..\..\..\..\..\CoreUtilities\LJCExecuteScript\LJCExecuteScript\bin\Debug\LJCExecuteScript.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCAddressParserLib',
  'A library for parsing U.S. Address data. (D)',
  '..\..\..\..\..\CoreUtilities\LJCAddressParserLib\LJCAddressParserLib\bin\Debug\LJCAddressParserLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCWinFormCommon',
  'The WinForm Common library.',
  '..\..\..\..\..\CoreAssemblies\LJCLibraries\LJCWinFormCommon\bin\Debug\LJCWinFormCommon.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCWinFormControls',
  'The WinForm Controls library.',
  '..\..\..\..\..\CoreAssemblies\LJCLibraries\LJCWinFormControls\bin\Debug\LJCWinFormControls.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGridDataLib',
  'The DbResult Grid Helper Library.',
  '..\..\..\..\..\CoreAssemblies\LJCGridDataLib\LJCGridDataLib\bin\Debug\LJCGridDataLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCTextDataReaderLib',
  'The Text Data Reader library.',
  '..\..\..\..\..\CoreAssemblies\LJCTextDataReader\LJCTextDataReaderLib\bin\Debug\LJCTextDataReaderLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataDetail',
  'The DataDetail Dynamic Detail dialog. (D)',
  '..\..\..\..\..\CoreAssemblies\LJCDataDetail\LJCDataDetail\bin\Debug\LJCDataDetail.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataDetailDAL',
  'The DataDetail DAL',
  '..\..\..\..\..\CoreAssemblies\LJCDataDetail\LJCDataDetailDAL\bin\Debug\LJCDataDetailDAL.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataDetailLib',
  'The DataDetail Supporting Library. (D)',
  '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailLib\bin\Debug\LJCDataDetailLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataDetailConsole',
  'The DataDetail Test Console. (D)',
  '..\..\..\..\..\CoreAssemblies\DataDetail\LJCDataDetailConsole\bin\Debug\LJCDataDetailConsole.xml',
  '', @seq;

set @groupName = 'DataLibraries';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'DataOverview',
  'The Data Service Libraries Overview.',
  'DataOverview.html',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataAccess',
  'The ADO.NET Data Access library. (DE)',
  '..\..\..\..\..\CoreAssemblies\LJCDataAccess\LJCDataAccess\bin\Debug\LJCDataAccess.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDBDataAccess',
  'The Message Data Access library. (DE4)',
  '..\..\..\..\..\CoreAssemblies\LJCDBDataAccess\LJCDBDataAccess\bin\Debug\LJCDBDataAccess.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataAccessConfig',
  'The Data Access Configuration library. (DO)',
  '..\..\..\..\..\CoreAssemblies\LJCDataAccessConfig\LJCDataAccessConfig\bin\Debug\LJCDataAccessConfig.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDBClientLib',
  'The Data Service Client library. (DOE3)',
  '..\..\..\..\..\CoreAssemblies\LJCDBClientLib\LJCDBClientLib\bin\Debug\LJCDBClientLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDBMessage',
  'The Data Service Message library. (DOGE2)',
  '..\..\..\..\..\CoreAssemblies\LJCDBMessage\LJCDBMessage\bin\Debug\LJCDBMessage.xml',
  'DBMessageGraph.jpg', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'ForeignKeyManagerTest',
  'The testing application.',
  '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\ForeignKeyManagerTest\bin\Debug\ForeignKeyManagerTest.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCSQLUtilLib',
  'The SQL Utilities library. (DO)',
  '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib\bin\Debug\LJCSQLUtilLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCSQLUtilLibDAL',
  'The SQL Utilities Data Access Layer library.',
  '..\..\..\..\..\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLibDAL\bin\Debug\LJCSQLUtilLibDAL.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDBServiceHost',
  'The LJCDBServiceLib windows host.',
  '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceHost\bin\Debug\LJCDBServiceHost.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDBServiceConsoleHost',
  'The LJCDBServiceLib console host.',
  '..\..\..\..\..\CoreAssemblies\LJCDBServiceHosts\LJCDBServiceConsoleHost\bin\Debug\LJCDBServiceConsoleHost.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDBServiceLib',
  'The Data Service library. (ROE)',
  '..\..\..\..\..\CoreAssemblies\LJCDBServiceLib\LJCDBServiceLib\bin\Debug\LJCDBServiceLib.xml',
  '', @seq;

set @groupName = 'GenText';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCGenText',
  'The Gen Text console program. (RO)',
  '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenText\bin\Debug\LJCGenText.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGenTableCode',
  'A program to generate table related code.',
  '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTableCode\bin\Debug\LJCGenTableCode.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGenTextLib',
  'A Text Generator library. (RO)',
  '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextLib\bin\Debug\LJCGenTextLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGenTextEdit',
  'The GenText Editor Test Program. (D)',
  '..\..\..\..\..\CoreUtilities\LJCGenText\LJCGenTextEdit\bin\Debug\LJCGenTextEdit.xml',
  '', @seq;

set @groupName = 'GenDoc';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCGenDoc',
  'A program to generate code documentation.',
  'LJCGenDoc.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGenDocLib',
  'The Code HTML Documentation Generator library.',
  'LJCGenDocLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGenDocDAL',
  'The Code Documentation Generator Data Access Layer library. (DO)',
  '..\..\..\..\..\CoreUtilities\LJCGenDoc\LJCGenDocDAL\bin\Debug\LJCGenDocDAL.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDocObjLib',
  'The Code Documentation data object library. (DOG)',
  '..\..\..\..\..\CoreUtilities\LJCGenDoc\LJCDocObjLib\bin\Debug\LJCDocObjLib.xml',
  'DocLibDataGraph.jpg', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDocXmlObjLib',
  'The Code Documentation XML object library. (DOG)',
  '..\..\..\..\..\CoreUtilities\LJCGenDoc\LJCDocXmlObjLib\bin\Debug\LJCDocXmlObjLib.xml',
  'DocLibXMLGraph.jpg', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGenDocEdit',
  'The GenDoc Group Editor',
  '..\..\..\..\..\CoreUtilities\LJCGenDocEdit\LJCGenDocEdit\bin\Debug\LJCGenDocEdit.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCGenPageGen',
  'Genealogy Page Generation.',
  '..\..\..\..\..\SampleApps\Genealogy\LJCGenPageGen\bin\Debug\LJCGenPageGen.xml',
  '', @seq;

set @groupName = 'DBViewDAL';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCDBViewDAL',
  'The Data View library.',
  '..\..\..\..\..\CoreAssemblies\LJCDBViewDAL\LJCDBViewDAL\bin\Debug\LJCDBViewDAL.xml',
  '', @seq;

set @groupName = 'ViewBuilder';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCViewBuilder',
  'A program to create and edit Views. (D)',
  '..\..\..\..\..\CoreUtilities\LJCViewBuilder\LJCViewBuilder\bin\Debug\LJCViewBuilder.xml',
  '', @seq;

set @groupName = 'ViewEditor';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCViewEditor',
  'A program to maintain View data. (D)',
  '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditor\bin\Debug\LJCViewEditor.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCViewEditorDAL',
  'The LJCViewEditor Data Access Library.',
  '..\..\..\..\..\CoreUtilities\LJCViewEditor\LJCViewEditorDAL\bin\Debug\LJCViewEditorDAL.xml',
  '', @seq;

set @groupName = 'LJCPagination';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCDataPageList',
  'Database Pagination',
  '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCDataPageList\bin\Debug\LJCDataPageList.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'Text File Pagination',
  'A typing tudor game.',
  '..\..\..\..\..\CoreAssemblies\LJCPagination\LJCTextPageList\bin\Debug\LJCTextPageList.xml',
  '', @seq;

set @groupName = 'CodeLine';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCCodeLineCounter',
  'The Code Line Counter console application.',
  '..\..\..\..\..\CoreUtilities\LJCCodeLineCounter\LJCCodeLineCounter\bin\Debug\LJCCodeLineCounter.xml',
  '', @seq;

set @groupName = 'RegionManager';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCRegionManager',
  'A program to manage Region data. (DO)',
  '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionManager\bin\Debug\LJCRegionManager.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCRegionDAL',
  'The Region Data Access Layer library.',
  '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionDAL\bin\Debug\LJCRegionDAL.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCRegionForm',
  'The Region Manager Test program.',
  '..\..\..\..\..\SampleApps\LJCRegionManager\LJCRegionForm\bin\Debug\LJCRegionForm.xml',
  '', @seq;

set @groupName = 'LJCUnitMeasure';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCUnitMeasure',
  'The Unit Measure program. (D)',
  '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasure\bin\Debug\LJCUnitMeasure.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCUnitMeasureDAL',
  'The Unit Measure Data Access Layer library.',
  '..\..\..\..\..\SampleApps\LJCUnitMeasure\LJCUnitMeasureDAL\bin\Debug\LJCUnitMeasureDAL.xml',
  '', @seq;

set @groupName = 'CVRManager';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'CVRManager',
  'The Contact Visit Record Manager.',
  '..\..\..\..\..\SampleApps\CVRManager\CVRManager\bin\Debug\CVRManager.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'CVRDAL',
  'The CVR Data Access Layer library. (D)',
  '..\..\..\..\..\SampleApps\CVRManager\CVRDAL\bin\Debug\CVRDAL.xml',
  '', @seq;

set @groupName = 'FacilityManager';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCFacilityManager',
  'A program to manage facility assets such as buildings, rooms, fixtures and equipment. (D)',
  '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManager\bin\Debug\LJCFacilityManager.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCFacilityManagerDAL',
  'The LJCFacilityManager Data Access Layer library.',
  '..\..\..\..\..\SampleApps\LJCFacilityManager\LJCFacilityManagerDAL\bin\Debug\LJCFacilityManagerDAL.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'FacilityForm',
  'The Facility Test program.',
  '..\..\..\..\..\SampleApps\LJCFacilityManager\FacilityForm\bin\Debug\LJCFacilityForm.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'ModuleHost',
  'The FacilityManager Module Test program.',
  '..\..\..\..\..\SampleApps\LJCFacilityManager\ModuleHost\bin\Debug\ModuleHost.xml',
  '', @seq;

set @groupName = 'DocAppManager';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCDocAppManager',
  'A program to manage Document images. (O)',
  '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManager\bin\Debug\LJCDocAppManager.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDocAppManagerDAL',
  'The DocApp Manager Data Access Layer library.',
  '..\..\..\..\..\SampleApps\LJCDocAppManager\LJCDocAppManagerDAL\bin\Debug\LJCDocAppManagerDAL.xml',
  '', @seq;

set @groupName = 'TextInvasion';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCTextInvasion',
  'A typing tudor game.',
  '..\..\..\..\..\SampleApps\LJCTextInvasion\LJCTextInvasion\bin\Debug\LJCTextInvasion.xml',
  '', @seq;

set @groupName = 'DataTransform';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCTransformManager',
  'A program to manage Data Transform data. (RO)',
  '..\..\..\..\..\SampleApps\LJCDataTransform\LJCTransformManager\bin\Debug\LJCTransformManager.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataTransformProcess',
  'A program to Automate Data Processes.',
  '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformProcess\bin\Debug\LJCDataTransformProcess.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'CommonModuleLib',
  'A library for common Transform Process Modules. (D)',
  '..\..\..\..\..\SampleApps\LJCDataTransform\LJCCommonModuleLib\bin\Debug\LJCCommonModuleLib.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCDataTransformDAL',
  'The Data Transform Data Access Layer library.',
  '..\..\..\..\..\SampleApps\LJCDataTransform\LJCDataTransformDAL\bin\Debug\LJCDataTransformDAL.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'TransformServiceTest',
  'A program to test the LJCDataTransformProcess library. (D)',
  '..\..\..\..\..\SampleApps\LJCDataTransform\TransformServiceTest\bin\Debug\TransformServiceTest.xml',
  '', @seq;

set @groupName = 'AppManager';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCAppManager',
  'A program to manage and host application modules. (DO)',
  '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManager\bin\Debug\LJCAppManager.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCAppManagerDAL',
  'The LJCAppManager Data Access Layer library. (D)',
  '..\..\..\..\..\SampleApps\LJCAppManager\LJCAppManagerDAL\bin\Debug\LJCAppManagerDAL.xml',
  '', @seq;

set @groupName = 'FacilityManagerSetup';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCFacilityManagerSetup',
  'The Facility Manager Setup program.',
  '..\..\..\..\..\SampleApps\LJCFacilityManagerSetup\LJCFacilityManagerSetup\bin\Debug\LJCFacilityManagerSetup.xml',
  '', @seq;

set @groupName = 'LJCSales';
set @seq = 1;
exec sp_DAAddUnique @groupName, 'LJCSalesManager',
  'The Sales Manager program. (DO)',
  '..\..\..\..\..\SampleApps\LJCSales\LJCSalesManager\bin\Debug\LJCSalesManager.xml',
  '', @seq;
set @seq += 1;
exec sp_DAAddUnique @groupName, 'LJCSalesDAL',
  'The Sales Data Access Layer library.',
  '..\..\..\..\..\SampleApps\LJCSales\LJCSalesDAL\bin\Debug\LJCSalesDAL.xml',
  '', @seq;
