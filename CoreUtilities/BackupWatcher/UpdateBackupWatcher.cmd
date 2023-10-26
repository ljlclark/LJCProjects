echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateBackupWatcher.cmd

echo.
if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\BackupWatcher\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

rmdir External
set to=Bin
echo *** %to% ***

set src=%utilRoot%LJCBackupChanges\LJCBackupChanges\%bin%
echo copy %src%\LJCBackupChanges.exe %to%
copy %src%\LJCBackupChanges.exe %to%

set src=%utilRoot%LJCBackupChanges\LJCBackupChangesLib\%bin%
echo copy %src%\LJCBackupChangesLib.dll %to%
copy %src%\LJCBackupChangesLib.dll %to%

set src=%utilRoot%LJCBackupCommonLib\LJCBackupCommonLib\%bin%
echo copy %src%\LJCBackupCommonLib.dll %to%
copy %src%\LJCBackupCommonLib.dll %to%

set src=%utilRoot%LJCBackupWatcherHosts\LJCBackupWatcherLib\%bin%
echo copy %src%\LJCBackupWatcherLib.dll %to%
copy %src%\LJCBackupWatcherLib.dll %to%

set src=%utilRoot%LJCBackupCreateChanges\LJCCreateFileChanges\%bin%
echo copy %src%\LJCCreateFileChanges.exe %to%
copy %src%\LJCCreateFileChanges.exe %to%

set src=%utilRoot%LJCBackupCreateChanges\LJCCreateFileChangesLib\%bin%
echo copy %src%\LJCCreateFileChangesLib.dll %to%
copy %src%\LJCCreateFileChangesLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------------
set to=%toRoot%BackupWatcher\%bin%
rem echo *** %to% ***

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
