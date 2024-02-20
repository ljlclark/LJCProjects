echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateCVRManager.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%apps%\CVRManager\
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

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionDAL\%bin%
echo copy %src%\LJCRegionDAL.dll %to%
copy %src%\LJCRegionDAL.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionForm\%bin%
echo copy %src%\LJCRegionForm.exe %to%
copy %src%\LJCRegionForm.exe %to%

set src=%appsRoot%LJCRegionManager\LJCRegionItem\%bin%
echo copy %src%\LJCRegionItem.dll %to%
copy %src%\LJCRegionItem.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionManager\%bin%
echo copy %src%\LJCRegionManager.exe %to%
copy %src%\LJCRegionManager.exe %to%

set src=%assmRoot%LJCLibraries\LJCWinFormCommon\%bin%
echo copy %src%\LJCWinFormCommon.dll %to%
copy %src%\LJCWinFormCommon.dll %to%

set src=%assmRoot%LJCLibraries\LJCWinFormControls\%bin%
echo copy %src%\LJCWinFormControls.dll %to%
copy %src%\LJCWinFormControls.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------
set to=%toRoot%CVRManager\%bin%
echo.
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

set src=%assmRoot%LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %src%\LJCDBClientLib.dll %to%
copy %src%\LJCDBClientLib.dll %to%

set src=%assmRoot%LJCDBMessage\LJCDBMessage\%bin%
echo copy %src%\LJCDBMessage.dll %to%
copy %src%\LJCDBMessage.dll %to%

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
copy %src%\CipherLib.dll %to%

set src=%assmRoot%LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %src%\LJCDBDataAccess.dll %to%
copy %src%\LJCDBDataAccess.dll %to%

set src=%assmRoot%LJCDBServiceLib\LJCDBServiceLib\%bin%
echo copy %src%\LJCDBServiceLib.dll %to%
copy %src%\LJCDBServiceLib.dll %to%

set src=%assmRoot%LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %src%\LJCGridDataLib.dll %to%
copy %src%\LJCGridDataLib.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionDAL\%bin%
echo copy %src%\LJCRegionDAL.dll %to%
copy %src%\LJCRegionDAL.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionForm\%bin%
echo copy %src%\LJCRegionForm.exe %to%
copy %src%\LJCRegionForm.exe %to%

set src=%appsRoot%LJCRegionManager\LJCRegionItem\%bin%
echo copy %src%\LJCRegionItem.dll %to%
copy %src%\LJCRegionItem.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionManager\%bin%
echo copy %src%\LJCRegionManager.exe %to%
copy %src%\LJCRegionManager.exe %to%
echo copy %src%\LJCRegionManager.exe.config %to%
copy %src%\LJCRegionManager.exe.config %to%

set src=%appsRoot%LJCUnitMeasure\LJCUnitMeasureDAL\%bin%
echo copy %src%\LJCUnitMeasureDAL.dll %to%
copy %src%\LJCUnitMeasureDAL.dll %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
