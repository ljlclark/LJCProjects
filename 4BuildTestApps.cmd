echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildTestApps.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem ************
rem CoreTestApps
rem ************
set /a counter=1
echo on
echo ----------------- %counter% - LJCDataAccessTest ----------- > Build.txt
echo LJCDataAccessTest >> Build.txt
call CoreTestApps\LJCDataAccessTest\UpdateDataAccessTest.cmd BuildAll >> Build.txt
msbuild CoreTestApps\LJCDataAccessTest\LJCDataAccessTest.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCDataManagerTest ------------ >> Build.txt
echo LJCDataManagerTest >> Build.txt
call CoreTestApps\LJCDataManagerTest\UpdateDataManagerTest.cmd BuildAll >> Build.txt
msbuild CoreTestApps\LJCDataManagerTest\LJCDataManagerTest.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - LJCNetCommonTest ------------ >> Build.txt
echo LJCNetCommonTest >> Build.txt
call CoreTestApps\LJCNetCommonTest\UpdateNetCommonTest.cmd BuildAll >> Build.txt
msbuild CoreTestApps\LJCNetCommonTest\LJCNetCommonTest.sln

set /a counter+=1
echo - >> Build.txt
echo ----------------- %counter% - UsingDataAccess ------------ >> Build.txt
echo LJCNetCommonTest >> Build.txt
call CoreTestApps\UsingDataAccess\UpdateUsingDataAccess.cmd BuildAll >> Build.txt
msbuild CoreTestApps\UsingDataAccess\UsingDataAccess.sln
call CoreTestApps\UsingDataAccess\UpdateUsingDataAccessPost.cmd BuildAll >> Build.txt

