echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBClientLib.cmd

set bin=bin\Debug
if %1%. == BuildAll. goto BuildAll
set root=..\
set runRoot=
set to=External
goto Update

:BuildAll
set root=
set runRoot=LJCDBClientLib\
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

set src=LJCDBMessage\CipherLib\%bin%
copy %root%%src%\CipherLib.dll %to%
set src=LJCDBMessage\LJCDBMessage\%bin%
copy %root%%src%\LJCDBMessage.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %root%%src%\LJCDBServiceLib.dll %to%

set src=LJCGridDataLib\LJCGridDataLib\%bin%
copy %root%%src%\LJCGridDataLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

set src=LJCTextDataReader\LJCTextDataReaderLib\%bin%
copy %root%%src%\LJCTextDataReaderLib.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------
set to=%runRoot%TestObjectManager\%bin%

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %root%%src%\LJCDataAccess.dll %to%

rem --- LJCDataAccessConfig
set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %root%%src%\DataConfigs.xml %to%
copy %root%%src%\ConnectionTemplates.xml %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %root%%src%\LJCDataAccessConfig.dll %to%
rem ---

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %root%%src%\LJCDBDataAccess.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %root%%src%\LJCNetCommon.dll %to%

copy %root%MySql.Data.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
