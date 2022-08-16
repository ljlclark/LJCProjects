echo Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
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
copy %root%%src%\DataDetail.exe %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=DataDetail\LJCDataDetailLib\%bin%
copy %root%%src%\LJCDataDetailLib.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBServiceLib\LJCDBDataAccessLib\%bin%
copy %root%%src%\LJCDBDataAccessLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %root%%src%\LJCDBServiceLib.dll %to%

set src=LJCDBViewDAL\LJCDBViewDAL\%bin%
copy %root%%src%\LJCDBViewDAL.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
copy %root%%src%\LJCGridDataLib.dll %to%

set src=LJCLibraries\Output
copy %root%%src%\*.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem --- LJCSQLUtilLib
set src=LJCSQLUtilLib\LJCSQLUtilLib\%bin%
copy %root%%src%\LJCSQLUtilLib.dll %to%

set src=LJCSQLUtilLib\LJCSQLUtilLibDAL\%bin%
copy %root%%src%\LJCSQLUtilLibDAL.dll %to%
rem ---

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%runroot%LJCViewEditor\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %root%%src%\CipherLib.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
