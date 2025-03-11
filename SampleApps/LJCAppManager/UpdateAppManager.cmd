echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateAppManager.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%apps%\LJCAppManager\
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

rem -------------------------------
set to=%toRoot%LJCAppManager\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
copy %src%\CipherLib.dll %to%

set src=%utilRoot%LJCAddressParserLib\LJCAddressParserLib\%bin%
echo copy %src%\LJCAddressParserLib.dll %to%
copy %src%\LJCAddressParserLib.dll %to%

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig
echo copy %src%\ConnectionTemplates.xml %to%
copy %src%\ConnectionTemplates.xml %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %src%\LJCDBClientLib.dll %to%
copy %src%\LJCDBClientLib.dll %to%

set src=%appsRoot%LJCDataTransform\LJCCommonModuleLib\%bin%
echo copy %src%\LJCCommonModuleLib.dll %to%
copy %src%\LJCCommonModuleLib.dll %to%

set src=%appsRoot%LJCDataTransform\LJCDataTransformDAL\%bin%
echo copy %src%\LJCDataTransformDAL.dll %to%
copy %src%\LJCDataTransformDAL.dll %to%

set src=%appsRoot%LJCDataTransform\LJCDataTransformProcess\%bin%
echo copy %src%\LJCDataTransformProcess.exe %to%
copy %src%\LJCDataTransformProcess.exe %to%
echo copy %src%\LJCDataTransformProcess.exe.config %to%
copy %src%\LJCDataTransformProcess.exe.config %to%

set src=%appsRoot%LJCDataTransform\LJCTransformManager\%bin%
echo copy %src%\LJCTransformManager.exe %to%
copy %src%\LJCTransformManager.exe %to%
echo copy %src%\LJCTransformManager.exe.config %to%
copy %src%\LJCTransformManager.exe.config %to%

set src=%appsRoot%LJCDataTransform\TransformServiceTest\%bin%
echo copy %src%\TransformServiceTest.exe %to%
copy %src%\TransformServiceTest.exe %to%

set src=%assmRoot%LJCDBMessage\CipherLib\%bin%
echo copy %src%\CipherLib.dll %to%
copy %src%\CipherLib.dll %to%

set src=%assmRoot%LJCDBViewDAL\LJCDBViewDAL\%bin%
echo copy %src%\LJCDBViewDAL.dll %to%
copy %src%\LJCDBViewDAL.dll %to%

set src=%appsRoot%LJCFacilityManager\Output
echo copy %src%\*.* %to%
copy %src%\*.* %to%

set src=%assmRoot%LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %src%\LJCGridDataLib.dll %to%
copy %src%\LJCGridDataLib.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionDAL\%bin%
echo copy %src%\LJCRegionDAL.dll %to%
copy %src%\LJCRegionDAL.dll %to%

set src=%appsRoot%LJCRegionManager\LJCRegionForm\%bin%
echo copy %src%\LJCRegionForm.exe %to%
copy %src%\LJCRegionForm.exe %to%

set src=%appsRoot%LJCRegionManager\LJCRegionManager\%bin%
echo copy %src%\LJCRegionManager.exe %to%
copy %src%\LJCRegionManager.exe %to%
echo copy %src%\LJCRegionManager.exe.config %to%
copy %src%\LJCRegionManager.exe.config %to%

set src=%utilRoot%LJCSQLUtilLib\LJCSQLUtilLib\%bin%
echo copy %src%\LJCSQLUtilLib.dll %to%
copy %src%\LJCSQLUtilLib.dll %to%
echo copy %src%\LJCSQLUtilLibDAL.dll %to%
copy %src%\LJCSQLUtilLibDAL.dll %to%

set src=%assmRoot%LJCTextDataReader\LJCTextDataReaderLib\%bin%
echo copy %src%\LJCTextDataReaderLib.dll %to%
copy %src%\LJCTextDataReaderLib.dll %to%

set src=%utilRoot%LJCViewBuilder\LJCViewBuilder\%bin%
echo copy %src%\LJCViewBuilder.exe %to%
copy %src%\LJCViewBuilder.exe %to%
echo copy %src%\LJCViewBuilder.exe.config %to%
copy %src%\LJCViewBuilder.exe.config %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
