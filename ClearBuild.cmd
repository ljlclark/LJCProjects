echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBuild.cmd
del Build.txt
call CVRManager\ClearCVRManager.cmd ClearAll
call DataDetail\ClearDataDetail.cmd ClearAll
call LJCCodeLineCounter\ClearCodeLineCounter.cmd ClearAll
call LJCDataAccess\ClearDataAccess.cmd ClearAll
call LJCDataAccessConfig\ClearDataAccessConfig.cmd ClearAll
call LJCDBClientLib\ClearDBClientLib.cmd ClearAll
call LJCDBDataAccess\ClearDbDataAccess.cmd ClearAll
call LJCDBMessage\ClearDBMessage.cmd ClearAll
call LJCDBServiceHosts\ClearDBServiceHosts.cmd ClearAll
call LJCDBServiceLib\ClearDBServiceLib.cmd ClearAll
call LJCDBViewDAL\ClearDBViewDAL.cmd ClearAll
call LJCDocLib\ClearDocLib.cmd ClearAll
call LJCGenText\ClearGenText.cmd ClearAll
call LJCGridDataLib\ClearGridDataLib.cmd ClearAll
call LJCLibraries\ClearLibraries.cmd ClearAll
call LJCNetCommon\ClearNetCommon.cmd ClearAll
call LJCRegionManager\ClearRegionManager.cmd ClearAll
call LJCSQLUtilLib\ClearSQLUtilLib.cmd ClearAll
call LJCTextDataReader\ClearTextDataReader.cmd ClearAll
call LJCUnitMeasure\ClearUnitMeasure.cmd ClearAll
call LJCViewEditor\ClearViewEditor.cmd ClearAll
