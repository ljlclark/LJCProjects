echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateAll.cmd

rem CoreAssemblies
call CoreAssemblies\DataDetail\UpdateDataDetail.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCDataAccess\UpdateDataAccess.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDataAccessConfig\UpdateDataAccessConfig.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDBClientLib\UpdateDBClientLib.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDBDataAccess\UpdateDBDataAccess.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDBMessage\UpdateDBMessage.cmd BuildAll > Update.txt
call CoreAssemblies\LJCDBServiceHosts\UpdateDBServiceHosts.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCDBServiceLib\UpdateDBServiceLib.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCDBViewDAL\UpdateDBViewDAL.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCGridDataLib\UpdateGridDataLib.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCLibraries\UpdateLibraries.cmd BuildAll >> Update.txt
call CoreAssemblies\LJCTextDataReader\UpdateTextDataReader.cmd BuildAll > Update.txt

rem CoreUtilities
call CoreUtilities\LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll > Update.txt
call CoreUtilities\LJCDocLib\UpdateDocLib.cmd BuildAll >> Update.txt
call CoreUtilities\LJCGenText\UpdateGenText.cmd BuildAll >> Update.txt
call CoreUtilities\LJCSQLUtilLib\UpdateSQLUtilLib.cmd BuildAll >> Update.txt
call CoreUtilities\LJCViewEditor\UpdateViewEditor.cmd BuildAll >> Update.txt

rem SampleApps
call SampleApps\CVRManager\UpdateCVRManager.cmd BuildAll >> Update.txt
call SampleApps\LJCRegionManager\UpdateRegionManager.cmd BuildAll >> Update.txt
call SampleApps\LJCUnitMeasure\UpdateUnitMeasure.cmd BuildAll >> Update.txt
