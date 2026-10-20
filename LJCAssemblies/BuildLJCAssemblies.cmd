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
set marker=----------------- %counter% - LJCNetCommon5 ---------------
echo.
echo                                         %marker%
echo %marker% > Build.txt
echo LJCNetCommonS5 >> Build.txt
msbuild LJCNetCommonS5\LJCNetCommonS5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCTextDataReader5 ---------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCTextDataReaderS5 >> Build.txt
msbuild LJCTextDataReaderS5\LJCTextDataReaderS5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDataAccessConfig5 ---------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataAccessConfigS5 >> Build.txt
call LJCDataAccessConfigS5\UpdateDataAccessConfigS5.cmd BuildAll >> Build.txt
msbuild LJCDataAccessConfigS5\LJCDataAccessConfigS5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDataAccess5 --------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataAccessS5 >> Build.txt
call LJCDataAccessS5\UpdateDataAccessS5.cmd BuildAll >> Build.txt
msbuild LJCDataAccessS5\LJCDataAccessS5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDBMessage5 ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBMessageS5 >> Build.txt
call LJCDBMessageS5\UpdateDBMessageS5.cmd BuildAll >> Build.txt
msbuild LJCDBMessageS5\LJCDBMessageS5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDBDataAccess5 ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBDataAccessS5 >> Build.txt
call LJCDBDataAccessS5\UpdateDBDataAccessS5.cmd BuildAll >> Build.txt
msbuild LJCDBDataAccessS5\LJCDBDataAccessS5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDBClient5 ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBClientS5 >> Build.txt
rem call LJCDBClientS5\UpdateDBClientS5.cmd BuildAll >> Build.txt
msbuild LJCDBClientS5\LJCDBClientS5.sln

set /a counter+=1
set marker=----------------- %counter% - LJCDBServiceLib5 ---------------
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBServiceLibS5 >> Build.txt
call LJCDBServiceLibS5\UpdateDBServiceLibS5.cmd BuildAll >> Build.txt
msbuild LJCDBServiceLibS5\LJCDBServiceLibS5.sln
pause