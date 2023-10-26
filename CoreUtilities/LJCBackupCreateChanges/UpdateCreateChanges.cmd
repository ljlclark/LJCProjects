echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateCreateChanges.cmd

echo.
set mainRoot=..\..\
if %1%. == BuildAll. (
  call SubFolders.cmd %1%
  set toRoot=%util%\LJCBackupCreateChanges\
  call TargetFolders.cmd
) else (
  call %mainRoot%SubFolders.cmd %1%
  call %mainRoot%TargetFolders.cmd
)

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%utilRoot%LJCBackupCommonLib\LJCBackupCommonLib\%bin%
echo copy %src%\LJCBackupCommonLib.dll %to%
copy %src%\LJCBackupCommonLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ------------------------------------
set to=%toRoot%LJCBackupCreateChanges\%bin%
rem echo.
rem echo *** %to% ***

if %1%. == BuildAll. goto End
if %1%. neq nopause. pause
:End