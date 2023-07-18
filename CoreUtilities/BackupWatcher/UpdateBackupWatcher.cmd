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
