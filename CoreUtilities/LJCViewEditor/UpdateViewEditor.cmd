echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateViewEditor.cmd

echo.
if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\LJCViewEditor\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=DataDetail\DataDetail\%bin%
echo copy %assmRoot%%src%\DataDetail.exe %to%
copy %assmRoot%%src%\DataDetail.exe %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %assmRoot%%src%\LJCDataAccess.dll %to%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=DataDetail\LJCDataDetailLib\%bin%
echo copy %assmRoot%%src%\LJCDataDetailLib.dll %to%
copy %assmRoot%%src%\LJCDataDetailLib.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %assmRoot%%src%\LJCDBClientLib.dll %to%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %assmRoot%%src%\LJCDBDataAccess.dll %to%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
echo copy %assmRoot%%src%\LJCDBMessage.dll %to%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
echo copy %assmRoot%%src%\LJCDBServiceLib.dll %to%
copy %assmRoot%%src%\LJCDBServiceLib.dll %to%

set src=LJCDBViewDAL\LJCDBViewDAL\%bin%
echo copy %assmRoot%%src%\LJCDBViewDAL.dll %to%
copy %assmRoot%%src%\LJCDBViewDAL.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %assmRoot%%src%\LJCGridDataLib.dll %to%
copy %assmRoot%%src%\LJCGridDataLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
echo copy %assmRoot%%src%\LJCNetCommon.dll %to%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

set src=LJCSQLUtilLib\LJCSQLUtilLib\%bin%
echo copy %utilRoot%%src%\LJCSQLUtilLib.dll %to%
copy %utilRoot%%src%\LJCSQLUtilLib.dll %to%

set src=LJCSQLUtilLib\LJCSQLUtilLibDAL\%bin%
echo copy %utilRoot%%src%\LJCSQLUtilLibDAL.dll %to%
copy %utilRoot%%src%\LJCSQLUtilLibDAL.dll %to%

set src=LJCLibraries\LJCWinFormCommon\%bin%
echo copy %assmRoot%%src%\LJCWinFormCommon.dll %to%
copy %assmRoot%%src%\LJCWinFormCommon.dll %to%

set src=LJCLibraries\LJCWinFormControls\%bin%
echo copy %assmRoot%%src%\LJCWinFormControls.dll %to%
copy %assmRoot%%src%\LJCWinFormControls.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%toroot%LJCViewEditor\%bin%
echo.
echo *** %to% ***

set src=LJCDBMessage\CipherLib\%bin%
echo copy %assmRoot%%src%\CipherLib.dll %to%
copy %assmRoot%%src%\CipherLib.dll %to%

set src=DataDetail\LJCDataDetailDAL\%bin%
echo copy %assmRoot%%src%\LJCDataDetailDAL.dll %to%
copy %assmRoot%%src%\LJCDataDetailDAL.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %assmRoot%%src%\LJCDataAccess.dll %to%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig
echo copy %assmRoot%%src%\DataConfigs.xml %to%
copy %assmRoot%%src%\DataConfigs.xml %to%
echo copy %assmRoot%%src%\ConnectionTemplates.xml %to%
copy %assmRoot%%src%\ConnectionTemplates.xml %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
