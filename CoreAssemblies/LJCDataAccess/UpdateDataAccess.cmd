echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDataAccess.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCDataAccess\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCNetCommon\LJCNetCommon
copy %root%%src%\%bin%\LJCNetCommon.dll %to%

copy %root%MySql.Data.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------
set to=%runRoot%LJCDataAccess\%bin%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
