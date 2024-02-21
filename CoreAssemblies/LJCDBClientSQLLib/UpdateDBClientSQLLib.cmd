echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDBClientSQLLib.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDBClientSQLLib\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

rem set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
rem echo copy %src%\CipherLib.dll %to%
rem copy %src%\CipherLib.dll %to%

set src=%assmRoot%LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %src%\LJCDBClientLib.dll %to%
copy %src%\LJCDBClientLib.dll %to%

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

rem set src=%assmRoot%LJCDBDataAccess\LJCDBDataAccess\%bin%
rem echo copy %src%\LJCDBDataAccess.dll %to%
rem copy %src%\LJCDBDataAccess.dll %to%

set src=%assmRoot%LJCDBMessage\LJCDBMessage\%bin%
echo copy %src%\LJCDBMessage.dll %to%
copy %src%\LJCDBMessage.dll %to%

rem set src=%assmRoot%LJCDBServiceLib\LJCDBServiceLib\%bin%
rem echo copy %src%\LJCDBServiceLib.dll %to%
rem copy %src%\LJCDBServiceLib.dll %to%

set src=%assmRoot%LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %src%\LJCGridDataLib.dll %to%
copy %src%\LJCGridDataLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

rem set src=%assmRoot%LJCTextDataReader\LJCTextDataReaderLib\%bin%
rem echo copy %src%\LJCTextDataReaderLib.dll %to%
rem copy %src%\LJCTextDataReaderLib.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%toRoot%LJCDBClientSQLLib\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
