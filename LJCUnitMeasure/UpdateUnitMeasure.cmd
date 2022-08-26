echo Copyright (c) Lester J. Clark 2021 - All Rights Reserved
set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
mkdir External
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCUnitMeasure\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCLibraries\Output
copy %root%%src%\*.* %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------------
set to=%runRoot%LJCUnitMeasure\%bin%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%
set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %root%%src%\CipherLib.dll %to%

set src=LJCDBServiceLib\LJCDBDataAccessLib\%bin%
copy %root%%src%\LJCDBDataAccessLib.dll %to%
set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %root%%src%\LJCDBServiceLib.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
