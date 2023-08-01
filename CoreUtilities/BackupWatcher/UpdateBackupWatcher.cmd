echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateBackupWatcher.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SetupUpdate.cmd %1%
call %mainRoot%SetupFolder.cmd
goto Process:
:BuildAll
call SetupUpdate.cmd %1%
set toRoot=%util%\BackupWatcher\
call SetupFolder.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

rmdir External
set to=Bin

set src=LJCBackupWatcherLib\LJCBackupChanges\%bin%
copy %utilRoot%%src%\LJCBackupChanges.exe %to%

set src=LJCBackupWatcherLib\LJCBackupChangesLib\%bin%
copy %utilRoot%%src%\LJCBackupChangesLib.dll %to%

set src=LJCBackupWatcherLib\LJCBackupWatcherLib\%bin%
copy %utilRoot%%src%\LJCBackupWatcherLib.dll %to%

set src=LJCBackupCreateChanges\LJCCreateFileChanges\%bin%
copy %utilRoot%%src%\LJCCreateFileChanges.exe %to%

set src=LJCBackupCreateChanges\LJCCreateFileChangesLib\%bin%
copy %utilRoot%%src%\LJCCreateFileChangesLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------------
set to=%toRoot%BackupWatcher\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
