echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved

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
pause
