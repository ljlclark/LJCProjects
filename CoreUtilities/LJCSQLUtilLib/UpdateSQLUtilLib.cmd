echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateSQLUtilLib.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\LJCSQLUtilLib\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCDataDetail\LJCDataDetail\%bin%
echo copy %src%\LJCDataDetail.exe %to%
copy %src%\LJCDataDetail.exe %to%

set src=%assmRoot%LJCDataDetail\LJCDataDetailDAL\%bin%
echo copy %src%\LJCDataDetailDAL.dll %to%
copy %src%\LJCDataDetailDAL.dll %to%

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCDataDetail\LJCDataDetailLib\%bin%
echo copy %src%\LJCDataDetailLib.dll %to%
copy %src%\LJCDataDetailLib.dll %to%

set src=%assmRoot%LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %src%\LJCDBClientLib.dll %to%
copy %src%\LJCDBClientLib.dll %to%

set src=%assmRoot%LJCDBMessage\LJCDBMessage\%bin%
echo copy %src%\LJCDBMessage.dll %to%
copy %src%\LJCDBMessage.dll %to%

set src=%assmRoot%LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %src%\LJCDBDataAccess.dll %to%
copy %src%\LJCDBDataAccess.dll %to%

set src=%assmRoot%LJCDBServiceLib\LJCDBServiceLib\%bin%
echo copy %src%\LJCDBServiceLib.dll %to%
copy %src%\LJCDBServiceLib.dll %to%

set src=%assmRoot%LJCDBViewDAL\LJCDBViewDAL\%bin%
echo copy %src%\LJCDBViewDAL.dll %to%
copy %src%\LJCDBViewDAL.dll %to%

set src=%assmRoot%LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %src%\LJCGridDataLib.dll %to%
copy %src%\LJCGridDataLib.dll %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

set src=%assmRoot%LJCLibraries\LJCWinFormCommon\%bin%
echo copy %src%\LJCWinFormCommon.dll %to%
copy %src%\LJCWinFormCommon.dll %to%

set src=%assmRoot%LJCLibraries\LJCWinFormControls\%bin%
echo copy %src%\LJCWinFormControls.dll %to%
copy %src%\LJCWinFormControls.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ---------------------------------------
set to=%toRoot%ForeignKeyManagerTest\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig
echo copy %src%\DataConfigs.xml %to%
copy %src%\DataConfigs.xml %to%
echo copy %src%\ConnectionTemplates.xml %to%
copy %src%\ConnectionTemplates.xml %to%

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
copy %src%\CipherLib.dll %to%

rem ---------------------------------------
set to=%toRoot%DataHelper\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
