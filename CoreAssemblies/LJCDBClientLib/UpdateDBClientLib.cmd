echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDBClientLib.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDBClientLib\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %src%\LJCDBDataAccess.dll %to%
copy %src%\LJCDBDataAccess.dll %to%

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
copy %src%\CipherLib.dll %to%

set src=%assmRoot%LJCDBMessage\LJCDBMessage\%bin%
echo copy %src%\LJCDBMessage.dll %to%
copy %src%\LJCDBMessage.dll %to%

set src=%assmRoot%LJCDBServiceLib\LJCDBServiceLib\%bin%
echo copy %src%\LJCDBServiceLib.dll %to%
copy %src%\LJCDBServiceLib.dll %to%

set src=%assmRoot%LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %src%\LJCGridDataLib.dll %to%
copy %src%\LJCGridDataLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

set src=%assmRoot%LJCTextDataReader\LJCTextDataReaderLib\%bin%
echo copy %src%\LJCTextDataReaderLib.dll %to%
copy %src%\LJCTextDataReaderLib.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%LJCDBClientLib\%bin%
rem echo.
rem echo *** %to% ***

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
