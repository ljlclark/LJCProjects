echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDataAccess.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDataAccess\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon
echo copy %src%\%bin%\LJCNetCommon.dll %to%
copy %src%\%bin%\LJCNetCommon.dll %to%

echo copy %mainRoot%MySql.Data.dll %to%
copy %mainRoot%MySql.Data.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%toRoot%LJCDataAccess\%bin%
rem echo.
rem echo *** %to% ***

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
