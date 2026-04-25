rem echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildLJCAssemblies.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem ***************
rem LJC Assemblies
rem ***************
set /a counter+=1
set marker=----------------- %counter% - LJCNetCommon5 --------------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCNetCommon5 >> Build.txt
msbuild LJCNetCommon5\LJCNetCommon5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDataAccessConfig --------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataAccessConfig5 >> Build.txt
call LJCDataAccessConfig5\UpdateDataAccessConfig5.cmd >> Build.txt
msbuild LJCDataAccessConfig5\LJCDataAccessConfig5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDataAccess5 -------------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataAccess5 >> Build.txt
call LJCDataAccess5\UpdateDataAccess5.cmd >> Build.txt
msbuild LJCDataAccess5\LJCDataAccess5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDBDataAccess5 --------------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBDataAccess5 >> Build.txt
call LJCDBDataAccess5\UpdateDBDataAccess5.cmd >> Build.txt
msbuild LJCDBDataAccess5\LJCDBDataAccess5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDBMessage5 --------------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBMessage5 >> Build.txt
call LJCDBMessage5\UpdateDBMessage5.cmd >> Build.txt
msbuild LJCDBMessage5\LJCDBMessage5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDBClientLib5 --------------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBClientLib5 >> Build.txt
call LJCDBClientLib5\UpdateDBClientLib5.cmd >> Build.txt
msbuild LJCDBClientLib5\LJCDBClientLib5.sln
pause