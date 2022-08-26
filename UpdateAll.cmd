echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem UpdateAll.cmd
call CVRManager\UpdateCVRManager.cmd BuildAll >> Update.txt
call DataDetail\UpdateDataDetail.cmd BuildAll >> Update.txt
call LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll > Update.txt
call LJCDataAccess\UpdateDataAccess.cmd BuildAll > Update.txt
call LJCDataAccessConfig\UpdateDataAccessConfig.cmd BuildAll > Update.txt
call LJCDBClientLib\UpdateDBClientLib.cmd BuildAll > Update.txt
call LJCDBDataAccess\UpdateDBDataAccess.cmd BuildAll > Update.txt
call LJCDBMessage\UpdateDBMessage.cmd BuildAll > Update.txt
call LJCDBServiceHosts\UpdateDBServiceHosts.cmd BuildAll >> Update.txt
call LJCDBServiceLib\UpdateDBServiceLib.cmd BuildAll >> Update.txt
call LJCDBViewDAL\UpdateDBViewDAL.cmd BuildAll >> Update.txt
call LJCDocLib\UpdateDocLib.cmd BuildAll >> Update.txt
call LJCGenText\UpdateGenText.cmd BuildAll >> Update.txt
call LJCGridDataLib\UpdateGridDataLib.cmd BuildAll >> Update.txt
call LJCLibraries\UpdateLibraries.cmd BuildAll >> Update.txt
call LJCRegionManager\UpdateRegionManager.cmd BuildAll >> Update.txt
call LJCSQLUtilLib\UpdateSQLUtilLib.cmd BuildAll >> Update.txt
call LJCTextDataReader\UpdateTextDataReader.cmd BuildAll > Update.txt
call LJCUnitMeasure\UpdateUnitMeasure.cmd BuildAll >> Update.txt
call LJCViewEditor\UpdateViewEditor.cmd BuildAll >> Update.txt

