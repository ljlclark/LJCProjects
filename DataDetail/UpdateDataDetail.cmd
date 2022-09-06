set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=DataDetail\
set to=%runRoot%\External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=CVRManager\CVRItem\%bin%
echo copy %root%%src%\CVRItem.dll %to%
copy %root%%src%\CVRItem.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %root%%src%\LJCDataAccess.dll %to%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %root%%src%\LJCDBClientLib.dll %to%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %root%%src%\LJCDBDataAccess.dll %to%
copy %root%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
echo copy %root%%src%\LJCDBMessage.dll %to%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %root%%src%\LJCGridDataLib.dll %to%
copy %root%%src%\LJCGridDataLib.dll %to%

set src=LJCLibraries\Output
echo copy %root%%src%\LJCWinFormCommon.dll %to%
copy %root%%src%\LJCWinFormCommon.dll %to%
echo copy %root%%src%\LJCWinFormControls.dll %to%
copy %root%%src%\LJCWinFormControls.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
echo copy %root%%src%\LJCNetCommon.dll %to%
copy %root%%src%\LJCNetCommon.dll %to%

set src=LJCRegionManager\LJCRegionItem\%bin%
echo copy %root%%src%\LJCRegionItem.dll %to%
copy %root%%src%\LJCRegionItem.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------
set to=%runRoot%DataDetail\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %root%%src%\LJCDataAccess.dll %to%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %root%%src%\LJCDataAccessConfig.dll %to%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %root%%src%\LJCDBClientLib.dll %to%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
echo copy %root%%src%\CipherLib.dll %to%
copy %root%%src%\CipherLib.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
echo copy %root%%src%\LJCDBServiceLib.dll %to%
copy %root%%src%\LJCDBServiceLib.dll %to%

rem ----------------------------
set to=%runRoot%LJCDataDetailConsole\%bin%

set src=CVRManager\CVRDAL\%bin%
echo copy %root%%src%\CVRDAL.dll %to%
copy %root%%src%\CVRDAL.dll %to%

set src=DataDetail\DataDetail\%bin%
echo copy %root%%src%\DataDetail.exe %to%
copy %root%%src%\DataDetail.exe %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %root%%src%\LJCDataAccess.dll %to%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %root%%src%\LJCDataAccessConfig.dll %to%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
echo copy %root%%src%\LJCDBClientLib.dll %to%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
echo copy %root%%src%\LJCDBDataAccess.dll %to%
copy %root%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
echo copy %root%%src%\LJCDBMessage.dll %to%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
echo copy %root%%src%\LJCDBServiceLib.dll %to%
copy %root%%src%\LJCDBServiceLib.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %root%%src%\LJCGridDataLib.dll %to%
copy %root%%src%\LJCGridDataLib.dll %to%

set src=LJCLibraries\Output
echo copy %root%%src%\LJCWinFormCommon.dll %to%
copy %root%%src%\LJCWinFormCommon.dll %to%
echo copy %root%%src%\LJCWinFormControls.dll %to%
copy %root%%src%\LJCWinFormControls.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
echo copy %root%%src%\LJCNetCommon.dll %to%
copy %root%%src%\LJCNetCommon.dll %to%

set src=LJCRegionManager\LJCRegionDAL\%bin%
echo copy %root%%src%\LJCRegionDAL.dll %to%
copy %root%%src%\LJCRegionDAL.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
