echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildTestApps.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem ************
rem CoreTestApps
rem ************
set /a counter+=1
set marker=----------------- %counter% - LJCDataAccessTest ----------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
call CoreTestApps\LJCDataAccessTest\UpdateDataAccessTest.cmd >> Build.txt
msbuild CoreTestApps\LJCDataAccessTest\LJCDataAccessTest.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDataManagerTest ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
call CoreTestApps\LJCDataManagerTest\UpdateDataManagerTest.cmd >> Build.txt
msbuild CoreTestApps\LJCDataManagerTest\LJCDataManagerTest.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCNetCommonTest ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCNetCommonTest >> Build.txt
call CoreTestApps\LJCNetCommonTest\UpdateNetCommonTest.cmd BuildAll >> Build.txt
msbuild CoreTestApps\LJCNetCommonTest\LJCNetCommonTest.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - UsingDataAccess ------------ >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCNetCommonTest >> Build.txt
call CoreTestApps\UsingDataAccess\UpdateUsingDataAccess.cmd BuildAll >> Build.txt
msbuild CoreTestApps\UsingDataAccess\UsingDataAccess.sln
call CoreTestApps\UsingDataAccess\UpdateUsingDataAccessPost.cmd BuildAll >> Build.txt
