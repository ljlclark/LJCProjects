echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBViewDAL.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%assm%\LJCDBViewDAL\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %assmRoot%%src%\LJCDBClientLib.dll %to%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
echo copy %assmRoot%%src%\LJCDBMessage.dll %to%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
echo copy %assmRoot%%src%\LJCNetCommon.dll %to%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------
set to=%toRoot%LJCDBViewDAL\%bin%
echo.
echo *** %to% ***

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %assmRoot%%src%\LJCDataAccess.dll %to%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %assmRoot%%src%\LJCDBDataAccess.dll %to%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
echo copy %assmRoot%%src%\CipherLib.dll %to%
copy %assmRoot%%src%\CipherLib.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
