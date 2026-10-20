echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDataAccessConfig5.cmd

rem *** Check if Group Folder ***
if exist SubFolders2.cmd goto BuildAll
echo *** Called from Solution Folder. ***
set mainRoot=..\..\
rem *** Sets the Solution Group Folder values. ***
call %mainRoot%SubFolders2.cmd %1%
rem *** Sets the "to" value as External and creates folder. ***
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
echo *** Called from Codeline Root Folder. ***
rem *** Sets the Codeline Group Folder values. ***
call SubFolders2.cmd %1%
echo assmRoot-%assmRoot%
echo utilRoot-%utilRoot%
set toRoot=%util%\LJCDataAccessConfigS5\
rem *** Sets the "to" value as External and creates folder. ***
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set bin=%bin%\net8.0
set src=%assmRoot%LJCNetCommonS5\LJCNetCommon5\%bin%
echo copy %src%\LJCNetCommon5.dll %to%
copy %src%\LJCNetCommon5.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set to=%toRoot%LJCGenDoc5\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
rem copy %src%\CipherLib.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
