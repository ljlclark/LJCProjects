echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem ClearBuild.cmd
del Build.txt
call LJCCodeLineCounter\ClearCodeLineCounter.cmd ClearAll
call LJCDataAccess\ClearDataAccess.cmd ClearAll
call LJCDataAccessConfig\ClearDataAccessConfig.cmd ClearAll
call LJCDBClientLib\ClearDBClientLib.cmd ClearAll
call LJCDBMessage\ClearDBMessage.cmd ClearAll
call LJCDBServiceHosts\ClearDBServiceHosts.cmd ClearAll
call LJCDBServiceLib\ClearDBServiceLib.cmd ClearAll
call LJCGridDataLib\ClearGridDataLib.cmd ClearAll
call LJCLibraries\ClearLibraries.cmd ClearAll
call LJCNetCommon\ClearNetCommon.cmd ClearAll
call LJCTextDataReader\ClearTextDataReader.cmd ClearAll
