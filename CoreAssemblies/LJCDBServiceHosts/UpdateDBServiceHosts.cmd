echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBServiceHosts.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\..\CoreAssemblies\
set runRoot=
set to=External
goto Update

:BuildAll
set root=CoreAssemblies\
set runRoot=CoreAssemblies\LJCDBServiceHosts\
set to=%runRoot%External

:Update
rem *** Referenced Binaries ***
rem ***************************

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %root%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

rem --- LJCDBServiceLib
set src=LJCDBServiceLib\LJCDBDataAccessLib\%bin%
copy %root%%src%\LJCDBDataAccessLib.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %root%%src%\LJCDBServiceLib.dll %to%
rem ---

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------------
set to=%runRoot%LJCDBServiceConsoleHost\%bin%

set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%

rem ----------------------------------
set to=%runRoot%LJCDBServiceHost\%bin%
copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
