echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBServiceHosts.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%assm%\LJCDBServiceHosts\
call TargetFolders.cmd
:Process

rem *** Referenced Binaries ***
rem ***************************

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCDBClientLib\LJCDBClientLib\%bin%
copy %assmRoot%%src%\LJCDBClientLib.dll %to%

set src=LJCDBMessage\LJCDBMessage\%bin%
copy %assmRoot%%src%\LJCDBMessage.dll %to%

set src=LJCDBDataAccess\LJCDBDataAccess\%bin%
copy %assmRoot%%src%\LJCDBDataAccess.dll %to%

set src=LJCDBServiceLib\LJCDBServiceLib\%bin%
copy %assmRoot%%src%\LJCDBServiceLib.dll %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -----------------------------------------
set to=%toRoot%LJCDBServiceConsoleHost\%bin%

set src=LJCDataAccessConfig\LJCDataAccessConfig
copy %assmRoot%%src%\DataConfigs.xml %to%
copy %assmRoot%%src%\ConnectionTemplates.xml %to%

rem ----------------------------------
set to=%toRoot%LJCDBServiceHost\%bin%

copy %assmRoot%%src%\DataConfigs.xml %to%
copy %assmRoot%%src%\ConnectionTemplates.xml %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
