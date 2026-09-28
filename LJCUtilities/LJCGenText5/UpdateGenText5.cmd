echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateGenText5.cmd

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
set toRoot=%util%\LJCGenText5\
rem *** Sets the "to" value as External and creates folder. ***
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set fromBin=%bin%\net8.0
set src=%assmRoot%LJCNetCommon5\LJCNetCommon5\%fromBin%
echo copy %src%\LJCNetCommon5.dll %to%
copy %src%\LJCNetCommon5.dll %to%

set fromBin=%bin%\net8.0-windows
set src=%utilRoot%LJCDataUtility5\LJCControls5\%fromBin%
echo copy %src%\LJCControls5.dll %to%
copy %src%\LJCControls5.dll %to%

set bin=%fromBin%\net8.0

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set to=%toRoot%LJCGenTextXML5\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
rem copy %src%\CipherLib.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
