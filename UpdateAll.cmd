echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateAll.cmd

rem CoreAssemblies
call CoreAssemblies\LJCDataDetail\UpdateDataDetail.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCDataAccess\UpdateDataAccess.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDataAccessConfig\UpdateDataAccessConfig.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDBClientLib\UpdateDBClientLib.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCDBDataAccess\UpdateDBDataAccess.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDBMessage\UpdateDBMessage.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDBServiceHosts\UpdateDBServiceHosts.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCDBServiceLib\UpdateDBServiceLib.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCDBViewDAL\UpdateDBViewDAL.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCGridDataLib\UpdateGridDataLib.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCLibraries\UpdateLibraries.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCPagination\UpdatePagination.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCTextDataReader\UpdateTextDataReader.cmd BuildAll > Update.txt

rem CoreTestApps
call CoreTestApps\LJCDataAccessTest\UpdateDataAccessTest.cmd BuildAll > Update.txt
call CoreTestApps\LJCDataManagerTest\UpdateDataManagerTest.cmd BuildAll >> Update.txt

rem CoreUtilities
call CoreUtilities\LJCAddressParserLib\UpdateAddressParserLib.cmd BuildAll > Update.txt
call CoreUtilities\LJCBackupCreateChanges\UpdateCreateChanges.cmd ClearAll
pause
call CoreUtilities\LJCBackupWatcherHosts\UpdateBackupWatcherHosts.cmd ClearAll
call CoreUtilities\LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll > Update.txt
call CoreUtilities\LJCDocGroupEditor\UpdateDocGroupEditor.cmd BuildAll >> Update.txt
call CoreUtilities\LJCGenDoc\UpdateGenDoc.cmd BuildAll >> Update.txt
call CoreUtilities\LJCExecuteScript\UpdateExecuteScript.cmd BuildAll >> Update.txt
call CoreUtilities\LJCGenText\UpdateGenText.cmd BuildAll >> Update.txt
call CoreUtilities\LJCSQLUtilLib\UpdateSQLUtilLib.cmd BuildAll >> Update.txt
call CoreUtilities\LJCSystemBuilder\UpdateSystemBuilder.cmd BuildAll >> Update.txt
call CoreUtilities\LJCViewBuilder\UpdateViewBuilder.cmd BuildAll >> Update.txt
call CoreUtilities\LJCViewEditor\UpdateViewEditor.cmd BuildAll >> Update.txt

rem SampleApps
call SampleApps\CVRManager\UpdateCVRManager.cmd BuildAll >> Update.txt
call SampleApps\Genealogy\UpdateGenealogy.cmd BuildAll >> Update.txt
call SampleApps\LJCAppManager\UpdateAppManager.cmd BuildAll >> Update.txt
call SampleApps\LJCDataTransform\UpdateDataTransform.cmd BuildAll >> Update.txt
call SampleApps\LJCDocAppManager\UpdateDocAppManager.cmd BuildAll >> Update.txt
call SampleApps\LJCFacilityManager\UpdateFacilityManager.cmd BuildAll >> Update.txt
call SampleApps\LJCFacilityManagerSetup\UpdateFacilityManagerSetup.cmd BuildAll >> Update.txt
call SampleApps\LJCFacilityManagerSetup\UpdateFacilityManagerResources.cmd BuildAll >> Update.txt
call SampleApps\LJCRegionManager\UpdateRegionManager.cmd BuildAll >> Update.txt
call SampleApps\LJCSales\UpdateSalesManager.cmd BuildAll >> Update.txt
call SampleApps\LJCTextInvasion\UpdateTextInvasion.cmd BuildAll >> Update.txt
call SampleApps\LJCUnitMeasure\UpdateUnitMeasure.cmd BuildAll >> Update.txt
