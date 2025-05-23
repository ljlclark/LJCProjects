echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateDataTransform.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%apps%\LJCDataTransform\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%utilRoot%LJCAddressParserLib\LJCAddressParserLib\%bin%
echo copy %src%\LJCAddressParserLib.dll %to%
copy %src%\LJCAddressParserLib.dll %to%

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

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

set src=%assmRoot%LJCTextDataReader\LJCTextDataReaderLib\%bin%
echo copy %src%\LJCTextDataReaderLib.dll %to%
copy %src%\LJCTextDataReaderLib.dll %to%

set src=%assmRoot%LJCLibraries\LJCWinFormCommon\%bin%
echo copy %src%\LJCWinFormCommon.dll %to%
copy %src%\LJCWinFormCommon.dll %to%

set src=%assmRoot%LJCLibraries\LJCWinFormControls\%bin%
echo copy %src%\LJCWinFormControls.dll %to%
copy %src%\LJCWinFormControls.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------------
set to=%toRoot%LJCDataTransformProcess\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig
echo copy %src%\ConnectionTemplates.xml %to%
copy %src%\ConnectionTemplates.xml %to%

rem -----------------------------
set to=%toRoot%LJCTransformManager\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig
echo copy %src%\ConnectionTemplates.xml %to%
copy %src%\ConnectionTemplates.xml %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCDataTransform\TransformServiceTest\%bin%
rem echo copy %src%\TransformServiceTest.exe %to%
rem copy %src%\TransformServiceTest.exe %to%
rem echo copy %src%\TransformServiceTest.exe.config %to%
rem copy %src%\TransformServiceTest.exe.config %to%

rem -----------------------------
set to=%toRoot%ModuleHost\%bin%
echo.
echo *** %to% ***

set src=%utilRoot%LJCAddressParserLib\LJCAddressParserLib\%bin%
echo copy %src%\LJCAddressParserLib.dll %to%
copy %src%\LJCAddressParserLib.dll %to%

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

rem set src=%utilRoot%LJCDataTransform\LJCDataTransformDAL\%bin%
rem echo copy %src%\LJCDataTransformDAL.dll %to%
rem copy %src%\LJCDataTransformDAL.dll %to%

rem set src=%utilRoot%LJCDataTransform\LJCTransformManager\%bin%
rem echo copy %src%\LJCTransformManager.exe %to%
rem copy %src%\LJCTransformManager.exe %to%

set src=%assmRoot%LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %src%\LJCGridDataLib.dll %to%
copy %src%\LJCGridDataLib.dll %to%

rem -----------------------------
set to=%toRoot%TransformServiceTest\%bin%
echo.
echo *** %to% ***

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig
echo copy %src%\ConnectionTemplates.xml %to%
copy %src%\ConnectionTemplates.xml %to%

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
