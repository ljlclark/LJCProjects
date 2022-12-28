echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateViewEditor.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runroot=
set to=External
goto Update

:BuildAll
set root=
set runroot=LJCViewEditor\
set to=%runroot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=DataDetail\DataDetail\%bin%
echo copy %root%%src%\DataDetail.exe %to%
copy %root%%src%\DataDetail.exe %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %root%%src%\LJCDataAccess.dll %to%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %root%%src%\LJCDataAccessConfig.dll %to%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=DataDetail\LJCDataDetailLib\%bin%
echo copy %root%%src%\LJCDataDetailLib.dll %to%
copy %root%%src%\LJCDataDetailLib.dll %to%

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

set src=LJCDBViewDAL\LJCDBViewDAL\%bin%
echo copy %root%%src%\LJCDBViewDAL.dll %to%
copy %root%%src%\LJCDBViewDAL.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
echo copy %root%%src%\LJCGridDataLib.dll %to%
copy %root%%src%\LJCGridDataLib.dll %to%

set src=LJCLibraries\Output
echo copy %root%%src%\*.dll %to%
copy %root%%src%\*.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
echo copy %root%%src%\LJCNetCommon.dll %to%
copy %root%%src%\LJCNetCommon.dll %to%

rem --- LJCSQLUtilLib
set src=LJCSQLUtilLib\LJCSQLUtilLib\%bin%
echo copy %root%%src%\LJCSQLUtilLib.dll %to%
copy %root%%src%\LJCSQLUtilLib.dll %to%

set src=LJCSQLUtilLib\LJCSQLUtilLibDAL\%bin%
echo copy %root%%src%\LJCSQLUtilLibDAL.dll %to%
copy %root%%src%\LJCSQLUtilLibDAL.dll %to%
rem ---

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%runroot%LJCViewEditor\%bin%

set src=LJCDBMessage\CipherLib\%bin%
echo copy %root%%src%\CipherLib.dll %to%
copy %root%%src%\CipherLib.dll %to%

set src=DataDetail\LJCDataDetailDAL\%bin%
echo copy %root%%src%\LJCDataDetailDAL.dll %to%
copy %root%%src%\LJCDataDetailDAL.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
echo copy %root%%src%\LJCDataAccess.dll %to%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig
echo copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\DataConfigs.xml %to%
echo copy %root%%src%\ConnectionTemplates.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
