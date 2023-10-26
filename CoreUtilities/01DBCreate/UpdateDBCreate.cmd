echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBCreate.cmd

if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
set to=..\01DBCreate\
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\01DBCreate\
set to=%toRoot%
:Process

rem ***************************
rem *** Runtime Binaries ***

set src=LJCDataAccess\LJCDataAccess\%bin%
copy %assmRoot%%src%\LJCDataAccess.dll %to%

set src=LJCDataAccessConfig\LJCDataAccessConfig\%bin%
copy %assmRoot%%src%\LJCDataAccessConfig.dll %to%

set src=LJCExecuteScript\LJCExecuteScripts\%bin%
copy %utilRoot%%src%\LJCExecuteScripts.Exe %to%

set src=LJCExecuteScript\LJCExecuteScripts\%bin%
copy %utilRoot%%src%\LJCExecuteScripts.exe.config %to%

set src=LJCNetCommon\LJCNetCommon\%bin%
copy %assmRoot%%src%\LJCNetCommon.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
