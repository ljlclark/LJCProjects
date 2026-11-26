echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateControlsS5.cmd

rem *** Check if Group Folder ***
if exist SubFolders2.cmd goto BuildAll

rem *** Called from Solution Folder. ***
set mainRoot=..\..\
call %mainRoot%SubFolders2.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:

:BuildAll
rem *** Called from Codeline Root or Group Folder. ***
call SubFolders2.cmd %1%
rem *** Set to Solution Folder. ***
set toRoot=%assmRoot%\LJCCSParserS5\
call TargetFolders.cmd

:Process
rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set bin=%bin%\net8.0

set src=%assmRoot%LJCNetCommonS5\LJCNetCommon5\%bin%
echo copy %src%\LJCNetCommon5.dll %to%
copy %src%\LJCNetCommon5.dll %to%

set src=%assmRoot%LJCCSParserS5\LJCCSParser5\%bin%
echo copy %src%\LJCCSParser5.dll %to%
copy %src%\LJCCSParser5.dll %to%

set src=%assmRoot%LJCDBConfigS5\LJCDBConfig5\%bin%
echo copy %src%\LJCDBConfig5.dll %to%
copy %src%\LJCDBConfig5.dll %to%

set src=%assmRoot%LJCDataAccessS5\LJCDataAccess5\%bin%
echo copy %src%\LJCDataAccess5.dll %to%
copy %src%\LJCDataAccess5.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set to=%toRoot%LJCProject\%bin%
echo.
echo *** %to% ***

rem set src=%assmRoot%LJCNetCommonS5\LJCNetCommon5\%bin%
rem echo copy %src%\LJCNetCommon5.dll %to%
rem copy %src%\LJCNetCommon5.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
