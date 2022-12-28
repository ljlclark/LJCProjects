echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBuild.cmd
del Build.txt

rem CoreAssemblies
call CoreAssemblies\DataDetail\ClearDataDetail.cmd ClearAll
call CoreAssemblies\LJCDataAccess\ClearDataAccess.cmd ClearAll
call CoreAssemblies\LJCDataAccessConfig\ClearDataAccessConfig.cmd ClearAll
call CoreAssemblies\LJCDBClientLib\ClearDBClientLib.cmd ClearAll
call CoreAssemblies\LJCDBDataAccess\ClearDbDataAccess.cmd ClearAll
call CoreAssemblies\LJCDBMessage\ClearDBMessage.cmd ClearAll
call CoreAssemblies\LJCDBServiceHosts\ClearDBServiceHosts.cmd ClearAll
call CoreAssemblies\LJCDBServiceLib\ClearDBServiceLib.cmd ClearAll
call CoreAssemblies\LJCDBViewDAL\ClearDBViewDAL.cmd ClearAll
call CoreAssemblies\LJCGridDataLib\ClearGridDataLib.cmd ClearAll
call CoreAssemblies\LJCLibraries\ClearLibraries.cmd ClearAll
call CoreAssemblies\LJCNetCommon\ClearNetCommon.cmd ClearAll
call CoreAssemblies\LJCTextDataReader\ClearTextDataReader.cmd ClearAll

rem CoreUtilities
call CoreUtilities\LJCCodeLineCounter\ClearCodeLineCounter.cmd ClearAll
call CoreUtilities\LJCDocLib\ClearDocLib.cmd ClearAll
call CoreUtilities\LJCGenText\ClearGenText.cmd ClearAll
call CoreUtilities\LJCSQLUtilLib\ClearSQLUtilLib.cmd ClearAll
call CoreUtilities\LJCViewEditor\ClearViewEditor.cmd ClearAll

rem SampleApps
call SampleApps\CVRManager\ClearCVRManager.cmd ClearAll
call SampleApps\LJCRegionManager\ClearRegionManager.cmd ClearAll
call SampleApps\LJCUnitMeasure\ClearUnitMeasure.cmd ClearAll
