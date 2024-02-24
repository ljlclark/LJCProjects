echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDBDataAccess.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDBDataAccess\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCDBMessage\LJCDBMessage\%bin%
echo copy %src%\LJCDBMessage.dll %to%
copy %src%\LJCDBMessage.dll %to%

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
copy %src%\CipherLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%toRoot%LJCDBDataAccess\%bin%
rem echo.
rem echo *** %to% ***

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
