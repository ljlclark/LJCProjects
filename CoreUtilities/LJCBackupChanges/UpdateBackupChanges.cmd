echo off
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ***UpdateBackupChanges.cmd

set mainRoot=..\..\
if %1%. == BuildAll. (
  call SetupUpdate.cmd %1%
  set toRoot=%util%\LJCBackupChanges\
  call SetupFolder.cmd
) else (
  call %mainRoot%SetupUpdate.cmd %1%
  call %mainRoot%SetupFolder.cmd
)

rem ***************************
rem *** Referenced Binaries ***

set src=LJCBackupCommonLib\LJCBackupCommonLib\%bin%
copy %utilRoot%%src%\LJCBackupCommonLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------------
set to=%toRoot%LJCBackupChanges\%bin%

if %1%. neq nopause. pause
