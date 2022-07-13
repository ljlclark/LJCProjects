echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
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
pause
