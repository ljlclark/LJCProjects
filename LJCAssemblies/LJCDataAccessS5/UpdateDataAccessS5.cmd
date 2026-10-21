echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDBMessage5.cmd

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
set toRoot=%utilRoot%\LJCDataAccessS5\
call TargetFolders.cmd

:Process
rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set bin=%bin%\net8.0

set src=%assmRoot%LJCNetCommonS5\LJCNetCommon5\%bin%
echo copy %src%\LJCNetCommon5.dll %to%
copy %src%\LJCNetCommon5.dll %to%

set src=%assmRoot%LJCDBConfigS5\LJCDBConfig5\%bin%
echo copy %src%\LJCDBConfig5.dll %to%
copy %src%\LJCDBConfig5.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------
set to=%toRoot%LJCGenDoc5\%bin%
echo.
echo *** %to% ***

rem set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
rem echo copy %src%\CipherLib.dll %to%
rem copy %src%\CipherLib.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
