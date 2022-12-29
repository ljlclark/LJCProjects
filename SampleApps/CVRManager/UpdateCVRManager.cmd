echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateCVRManager.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\..\CoreAssemblies\
set altRoot=..\..\SampleApps\
set runRoot=
set to=External
goto Update

:BuildAll
set root=CoreAssemblies\
set altRoot=SampleApps\
set runRoot=SampleApps\CVRManager\
set to=%runRoot%External

:Update
if exist %to%\NUL goto continue
mkdir %to%
:continue

rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %root%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %root%%src%\LJCDBServiceLib.dll %to%
rem ---

set src=LJCLibraries\Output
copy %root%%src%\*.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem --- LJCRegionManager
set src=LJCRegionManager\LJCRegionDAL\%bin%
copy %altRoot%%src%\LJCRegionDAL.dll %to%

set src=LJCRegionManager\LJCRegionForm\%bin%
copy %altRoot%%src%\LJCRegionForm.exe %to%

set src=LJCRegionManager\LJCRegionItem\%bin%
copy %altRoot%%src%\LJCRegionItem.dll %to%

set src=LJCRegionManager\LJCRegionManager\%bin%
copy %altRoot%%src%\LJCRegionManager.exe %to%
rem ---

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------
set to=%runRoot%CVRManager\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBServiceLib\LJCDBDataAccessLib\%bin%
copy %root%%src%\LJCDBDataAccessLib.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%
set src=LJCDBMessage\CipherLib\%bin%
copy %root%%src%\CipherLib.dll %to%

set src=LJCDBServiceLib\LJCDBDataAccessLib\%bin%
copy %root%%src%\LJCDBDataAccessLib.dll %to%
set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %root%%src%\LJCDBServiceLib.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
copy %root%%src%\LJCGridDataLib.dll %to%

set src=LJCRegionManager\LJCRegionDAL\%bin%
copy %altRoot%%src%\LJCRegionDAL.dll %to%

set src=LJCRegionManager\LJCRegionForm\%bin%
copy %altRoot%%src%\LJCRegionForm.exe %to%

set src=LJCRegionManager\LJCRegionItem\%bin%
copy %altRoot%%src%\LJCRegionItem.dll %to%

set src=LJCRegionManager\LJCRegionManager\%bin%
copy %altRoot%%src%\LJCRegionManager.exe %to%
copy %altRoot%%src%\LJCRegionManager.exe.config %to%

set src=LJCUnitMeasure\LJCUnitMeasureDAL\%bin%
copy %altRoot%%src%\LJCUnitMeasureDAL.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
