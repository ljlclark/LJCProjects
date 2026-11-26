echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDataUtility5.cmd

if exist SubFolders2.cmd goto BuildAll
rem *** Called from solution folder. ***
set mainRoot=..\..\
rem *** Sets the solution group folder values. ***
call %mainRoot%SubFolders2.cmd %1%
rem *** Sets the "to" value as External and creates folder. ***
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
rem *** Called from line root folder UpdateAll.cmd. ***
rem *** Sets the solution group folder values. ***
call SubFolders2.cmd %1%
set toRoot=%util%\LJCDataUtility5\
rem *** Sets the "to" value as External and creates folder. ***
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set fromBin=%bin%\net8.0

set src=%assmRoot%LJCNetCommonS5\LJCNetCommon5\%fromBin%
echo copy %src%\LJCNetCommon5.dll %to%
copy %src%\LJCNetCommon5.dll %to%

set src=%assmRoot%LJCCSParserS5\LJCCSParser5\%fromBin%
echo copy %src%\LJCCSParser5.dll %to%
copy %src%\LJCCSParser5.dll %to%

set src=%assmRoot%LJCDBConfigS5\LJCDBConfig5\%fromBin%
echo copy %src%\LJCDBConfig5.dll %to%
copy %src%\LJCDBConfig5.dll %to%

set src=%assmRoot%LJCDataAccessS5\LJCDataAccess5\%fromBin%
echo copy %src%\LJCDataAccess5.dll %to%
copy %src%\LJCDataAccess5.dll %to%

set src=%assmRoot%LJCDBMessageS5\LJCDBMessage5\%fromBin%
echo copy %src%\LJCDBMessage5.dll %to%
copy %src%\LJCDBMessage5.dll %to%

set src=%assmRoot%LJCDBMessageS5\LJCCipherLib5\%fromBin%
echo copy %src%\LJCCipherLib5.dll %to%
copy %src%\LJCCipherLib5.dll %to%

set src=%assmRoot%LJCDBDataAccessS5\LJCDBDataAccess5\%fromBin%
echo copy %src%\LJCDBDataAccess5.dll %to%
copy %src%\LJCDBDataAccess5.dll %to%

set src=%assmRoot%LJCDBClientS5\LJCDBClientLib5\%fromBin%
echo copy %src%\LJCDBClientLib5.dll %to%
copy %src%\LJCDBClientLib5.dll %to%

rem ---------------------------
set fromBin=%bin%\net8.0-windows
set src=%assmRoot%LJCControlsS5\LJCControls5\%fromBin%
echo copy %src%\LJCControls5.dll %to%
copy %src%\LJCControls5.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set tobin=%bin%-windows
set to=%toRoot%LJCDataUtility5\%tobin%
echo.
echo *** %to% ***

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
