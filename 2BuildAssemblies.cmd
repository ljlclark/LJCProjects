echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildAssemblies.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

rem ***************
rem Core Assemblies
rem ***************
set /a counter+=1
set marker=----------------- %counter% - LJCNetCommon --------------- > Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJC.Net.Common >> Build.txt
msbuild CoreAssemblies\LJCNetCommon\LJCNetCommon.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDataAccessConfig --------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataAccessConfig >> Build.txt
call CoreAssemblies\LJCDataAccessConfig\UpdateDataAccessConfig.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDataAccessConfig\LJCDataAccessConfig.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDataAccess---------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataAccess >> Build.txt
call CoreAssemblies\LJCDataAccess\UpdateDataAccess.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDataAccess\LJCDataAccess.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDBMessage ---------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBMessage >> Build.txt
call CoreAssemblies\LJCDBMessage\UpdateDBMessage.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBMessage\LJCDBMessage.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDBDataAccess ------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBDataAccess >> Build.txt
call CoreAssemblies\LJCDBDataAccess\UpdateDBDataAccess.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBDataAccess\LJCDBDataAccess.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCLibraries ---------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCLibraries >> Build.txt
call CoreAssemblies\LJCLibraries\UpdateLibraries.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCLibraries\LJCLibraries.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCGridDataLib --------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCGridDataLib >> Build.txt
call CoreAssemblies\LJCGridDataLib\UpdateGridDataLib.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCGridDataLib\LJCGridDataLib.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDBServiceLib ------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBServiceLib >> Build.txt
call CoreAssemblies\LJCDBServiceLib\UpdateDBServiceLib.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBServiceLib\LJCDBServiceLib.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDBServiceHosts ----------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBServiceHosts >> Build.txt
call CoreAssemblies\LJCDBServiceHosts\UpdateDBServiceHosts.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBServiceHosts\LJCDBServiceHosts.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCTextDataReader ----------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCTextDataReader >> Build.txt
call CoreAssemblies\LJCTextDataReader\UpdateTextDataReader.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCTextDataReader\LJCTextDataReader.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDBClientLib -------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBClientLib >> Build.txt
call CoreAssemblies\LJCDBClientLib\UpdateDBClientLib.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBClientLib\LJCDBClientLib.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDBClientSQLLib -------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBClientLib >> Build.txt
call CoreAssemblies\L
set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDBViewDAL ---------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDBViewDAL >> Build.txt
call CoreAssemblies\LJCDBViewDAL\UpdateDBViewDAL.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBViewDAL\LJCDBViewDAL.sln
JCDBClientSQLLib\UpdateDBClientSQLLib.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDBClientSQLLib\LJCDBClientSQLLib.sln

set /a counter+=1
echo - >> Build.txt
set marker=----------------- %counter% - LJCDataDetail ---------------- >> Build.txt
echo.
echo                                         %marker%
echo %marker% >> Build.txt
echo LJCDataDetail >> Build.txt
call CoreAssemblies\LJCDataDetail\UpdateDataDetail.cmd BuildAll >> Build.txt
msbuild CoreAssemblies\LJCDataDetail\LJCDataDetail.sln
