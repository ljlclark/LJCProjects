echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateCVRManager.cmd

set bin=bin\Debug
set assm=CoreAssemblies\
set util=CoreUtilities\
set app=SampleApps\
if %1%. == BuildAll. goto BuildAll

rem Run from Solution folder.
set assmRoot=..\..\%assm%
set utilRoot=..\..\%util%
set appRoot=..\..\%app%
set toRoot=
set to=External
goto Update

:BuildAll
rem Run from main Projects folder.
set assmRoot=%assm%
set utilRoot=%util%
set appRoot=%app%
set toRoot=%appRoot%CVRManager\
set to=%toRoot%External

:Update
if exist %to%\NUL goto continue
mkdir %to%
:continue

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

set src=LJCLibraries\Output
copy %assmRoot%%src%\*.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

set src=LJCRegionManager\LJCRegionDAL\%bin%
copy %altRoot%%src%\LJCRegionDAL.dll %to%

set src=LJCRegionManager\LJCRegionForm\%bin%
copy %appRoot%%src%\LJCRegionForm.exe %to%

set src=LJCRegionManager\LJCRegionItem\%bin%
copy %appRoot%%src%\LJCRegionItem.dll %to%

set src=LJCRegionManager\LJCRegionManager\%bin%
copy %appRoot%%src%\LJCRegionManager.exe %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------
set to=%toRoot%CVRManager\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBServiceLib\LJCDBDataAccessLib\%bin%
copy %assmRoot%%src%\LJCDBDataAccessLib.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %assmRoot%%src%\CipherLib.dll %to%

set src=LJCDBServiceLib\LJCDBDataAccessLib\%bin%
copy %assmRoot%%src%\LJCDBDataAccessLib.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %assmRoot%%src%\LJCDBServiceLib.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
copy %assmRoot%%src%\LJCGridDataLib.dll %to%

set src=LJCRegionManager\LJCRegionDAL\%bin%
copy %appltRoot%%src%\LJCRegionDAL.dll %to%

set src=LJCRegionManager\LJCRegionForm\%bin%
copy %appRoot%%src%\LJCRegionForm.exe %to%

set src=LJCRegionManager\LJCRegionItem\%bin%
copy %appRoot%%src%\LJCRegionItem.dll %to%

set src=LJCRegionManager\LJCRegionManager\%bin%
copy %appRoot%%src%\LJCRegionManager.exe %to%
copy %appRoot%%src%\LJCRegionManager.exe.config %to%

set src=LJCUnitMeasure\LJCUnitMeasureDAL\%bin%
copy %appRoot%%src%\LJCUnitMeasureDAL.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
