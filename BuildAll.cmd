echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem BuildAll.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

set /a counter=1
echo on
echo ----------------- %counter% - LJCNetCommon --------------- > Build.txt
echo LJC.Net.Common >> Build.txt
msbuild LJCNetCommon\LJCNetCommon.sln

rem *** Requires ***
rem LJCNetCommon
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

rem *** Requires ***
rem LJCNetCommon
rem LJCDataAccessConfig
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDataAccess---------------- >> Build.txt
echo LJCDataAccess >> Build.txt
call LJCDataAccess\UpdateDataAccess.cmd BuildAll >> Build.txt
msbuild LJCDataAccess\LJCDataAccess.sln

rem *** Requires ***
rem LJCNetCommon
rem LJCTextDataReaderLib
rem LJCDataAccess
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBMessage ---------------- >> Build.txt
echo LJCDBMessage >> Build.txt
call LJCDBMessage\UpdateDBMessage.cmd BuildAll >> Build.txt
msbuild LJCDBMessage\LJCDBMessage.sln

rem *** Requires ***
rem LJCNetCommon
rem LJCDataAccessConfig
rem LJCDataAccess
rem LJCDataMessage
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

rem *** Requires ***
rem LJCNetCommon
rem LJCDataMessage
rem LJCWinFormControls
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCGridDataLib --------------- >> Build.txt
echo LJCGridDataLib >> Build.txt
call LJCGridDataLib\UpdateGridDataLib.cmd BuildAll >> Build.txt
msbuild LJCGridDataLib\LJCGridDataLib.sln

rem *** Requires ***
rem LJCNetCommon
rem LJCTextDataReaderLib
rem LJCDataAccessConfig
rem LJCDataAccess
rem LJCDataMessage
rem LJCDBDataAccessLib (Project-LJCDBServiceLib)
rem LJCGridDataLib
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBClientLib -------------- >> Build.txt
echo LJCDBClientLib >> Build.txt
call LJCDBClientLib\UpdateDBClientLib.cmd BuildAll >> Build.txt
msbuild LJCDBClientLib\LJCDBClientLib.sln

rem *** Requires ***
rem LJCNetCommon
rem LJCDataAccessConfig
rem LJCDataAccess
rem LJCDBMessage
rem LJCDBDataAccessLib (Project-LJCDBServiceLib)
rem LJCGridDataLib
rem LJCDBClientLib
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDBServiceHosts ----------- >> Build.txt
echo LJCDBServiceHosts >> Build.txt
call LJCDBServiceHosts\UpdateDBServiceHosts.cmd BuildAll >> Build.txt
msbuild LJCDBServiceHosts\LJCDBServiceHosts.sln

rem *** Requires ***
rem LJCNetCommon
rem LJCDataAccessConfig
rem LJCDataAccess
rem LJCDBMessage
rem LJCDBDataAccessLib (Project-LJCDBServiceLib)
rem LJCDBClientLib
rem LJCWinFormCommon
rem LJCWinFormControls
set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCGenText ------------------ >> Build.txt
echo LJCGenText >> Build.txt
call LJCGenText\UpdateGenText.cmd BuildAll >> Build.txt
msbuild LJCGenText\LJCGenText.sln

rem *** Requires ***
rem LJCNetCommon
rem LJCGenTextLib
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
