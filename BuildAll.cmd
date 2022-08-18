rem BuildAll.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

set /a counter=1
echo on
echo ----------------- %counter% - LJCNetCommon --------------- > Build.txt
echo LJC.Net.Common >> Build.txt
msbuild LJCNetCommon\LJCNetCommon.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCCodeLineCounter ---------- >> Build.txt
echo LJCCodeLineCounter >> Build.txt
call LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll >> Build.txt
msbuild LJCCodeLineCounter\LJCCodeLineCounter.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCTextDataReader ----------- >> Build.txt
echo LJCTextDataReader >> Build.txt
call LJCTextDataReader\UpdateTextDataReader.cmd BuildAll >> Build.txt
msbuild LJCTextDataReader\LJCTextDataReader.sln

set /a counter+=1
echo on
echo ----------------- %counter% - LJCDataAccessConfig --------- >> Build.txt
echo LJCDataAccessConfig >> Build.txt
call LJCDataAccessConfig\UpdateDataAccessConfig.cmd BuildAll >> Build.txt
msbuild LJCDataAccessConfig\LJCDataAccessConfig.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDataAccess---------------- >> Build.txt
echo LJCDataAccess >> Build.txt
call LJCDataAccess\UpdateDataAccess.cmd BuildAll >> Build.txt
msbuild LJCDataAccess\LJCDataAccess.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBMessage ---------------- >> Build.txt
echo LJCDBMessage >> Build.txt
call LJCDBMessage\UpdateDBMessage.cmd BuildAll >> Build.txt
msbuild LJCDBMessage\LJCDBMessage.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBDataAccess ------------- >> Build.txt
echo LJCDBDataAccess >> Build.txt
call LJCDBDataAccess\UpdateDBDataAccess.cmd BuildAll >> Build.txt
msbuild LJCDBDataAccess\LJCDBDataAccess.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBServiceLib ------------- >> Build.txt
echo LJCDBServiceLib >> Build.txt
call LJCDBServiceLib\UpdateDBServiceLib.cmd BuildAll >> Build.txt
msbuild LJCDBServiceLib\LJCDBServiceLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCLibraries ---------------- >> Build.txt
echo LJCLibraries >> Build.txt
call LJCLibraries\UpdateLibraries.cmd BuildAll >> Build.txt
msbuild LJCLibraries\LJCLibraries.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCGridDataLib --------------- >> Build.txt
echo LJCGridDataLib >> Build.txt
call LJCGridDataLib\UpdateGridDataLib.cmd BuildAll >> Build.txt
msbuild LJCGridDataLib\LJCGridDataLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBClientLib -------------- >> Build.txt
echo LJCDBClientLib >> Build.txt
call LJCDBClientLib\UpdateDBClientLib.cmd BuildAll >> Build.txt
msbuild LJCDBClientLib\LJCDBClientLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBServiceHosts ----------- >> Build.txt
echo LJCDBServiceHosts >> Build.txt
call LJCDBServiceHosts\UpdateDBServiceHosts.cmd BuildAll >> Build.txt
msbuild LJCDBServiceHosts\LJCDBServiceHosts.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCGenText ------------------ >> Build.txt
echo LJCGenText >> Build.txt
call LJCGenText\UpdateGenText.cmd BuildAll >> Build.txt
msbuild LJCGenText\LJCGenText.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDocLib.sln --------------- >> Build.txt
echo LJCDocLib >> Build.txt
call LJCDocLib\UpdateDocLib.cmd BuildAll >> Build.txt
msbuild LJCDocLib\LJCDocLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBViewDAL ---------------- >> Build.txt
echo LJCDBViewDAL >> Build.txt
call LJCDBViewDAL\UpdateDBViewDAL.cmd BuildAll >> Build.txt
msbuild LJCDBViewDAL\LJCDBViewDAL.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - DataDetail ------------------ >> Build.txt
echo DataDetail >> Build.txt
call DataDetail\UpdateDataDetail.cmd BuildAll >> Build.txt
msbuild DataDetail\DataDetail.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCSQLUtilLib --------------- >> Build.txt
echo LJCSQLUtilLib >> Build.txt
call LJCSQLUtilLib\UpdateSQLUtilLib.cmd BuildAll >> Build.txt
msbuild LJCSQLUtilLib\LJCSQLUtilLib.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCViewEditor --------------- >> Build.txt
echo LJCViewEditor >> Build.txt
call LJCViewEditor\UpdateViewEditor.cmd BuildAll >> Build.txt
msbuild LJCViewEditor\LJCViewEditor.sln
pause
