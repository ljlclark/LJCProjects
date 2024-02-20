echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateBackupWatcherHosts.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\BackupWatcherHosts\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%utilRoot%LJCBackupCommonLib\LJCBackupCommonLib\%bin%
echo copy %src%\LJCBackupCommonLib.dll %to%
copy %src%\LJCBackupCommonLib.dll %to%

rem *** set src=%utilRoot%BackupWatcherLib\BackupWatcherLib\%bin%
rem *** echo copy %src%\BackupWatcherLib.dll %to%
rem *** copy %src%\BackupWatcherLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------------
set to=%toRoot%BackupWatcherConsoleHost\%bin%
rem echo.
rem echo *** %to% ***

if %mainRoot%. == . goto End
if %1%. neq nopause. pause
:End
