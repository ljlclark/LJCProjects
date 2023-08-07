echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateCVRManager.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SetupUpdate.cmd %1%
call %mainRoot%SetupFolder.cmd
goto Process:
:BuildAll
call SetupUpdate.cmd %1%
set toRoot=%apps%\CVRManager\
call SetupFolder.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %assmRoot%%src%\LJCDBServiceLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

set src=LJCRegionManager\LJCRegionDAL\%bin%
copy %appsRoot%%src%\LJCRegionDAL.dll %to%

set src=LJCRegionManager\LJCRegionForm\%bin%
copy %appsRoot%%src%\LJCRegionForm.exe %to%

set src=LJCRegionManager\LJCRegionItem\%bin%
copy %appsRoot%%src%\LJCRegionItem.dll %to%

set src=LJCRegionManager\LJCRegionManager\%bin%
copy %appsRoot%%src%\LJCRegionManager.exe %to%

set src=LJCLibraries\LJCWinFormCommon\%bin%
copy %assmRoot%%src%\LJCWinFormCommon.dll %to%

set src=LJCLibraries\LJCWinFormControls\%bin%
copy %assmRoot%%src%\LJCWinFormControls.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------
set to=%toRoot%CVRManager\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %assmRoot%%src%\LJCDBServiceLib.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
copy %assmRoot%%src%\LJCGridDataLib.dll %to%

set src=LJCRegionManager\LJCRegionDAL\%bin%
copy %appsRoot%%src%\LJCRegionDAL.dll %to%

set src=LJCRegionManager\LJCRegionForm\%bin%
copy %appsRoot%%src%\LJCRegionForm.exe %to%

set src=LJCRegionManager\LJCRegionItem\%bin%
copy %appsRoot%%src%\LJCRegionItem.dll %to%

set src=LJCRegionManager\LJCRegionManager\%bin%
copy %appsRoot%%src%\LJCRegionManager.exe %to%
copy %appsRoot%%src%\LJCRegionManager.exe.config %to%

set src=LJCUnitMeasure\LJCUnitMeasureDAL\%bin%
copy %appsRoot%%src%\LJCUnitMeasureDAL.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
