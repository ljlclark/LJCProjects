echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBServiceLib.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\..\CoreAssemblies\
set runRoot=
set to=External
goto Update

:BuildAll
set root=CoreAssemblies\
set runRoot=CoreAssemblies\LJCDBServiceLib\
set to=%runRoot%External

:Update
rem ***************************
rem *** Referenced Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %root%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCDBMessage\CipherLib\%bin%
copy %root%%src%\CipherLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%runRoot%ConfigTestConsole\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%

rem ----------------------------------
set to=%runRoot%TestDbDataAccess\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

rem --- LJCDataAccessConfig
set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%
rem ---

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
