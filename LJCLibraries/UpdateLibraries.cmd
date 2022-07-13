echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem UpdateLibraries.cmd
set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCLibraries\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCNetCommon\LJCNetCommon
copy %root%%src%\%bin%\LJCNetCommon.dll %to%

copy %root%MySql.Data.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem ----------------------------------
set to=%runRoot%LJCWinFormCommon\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
