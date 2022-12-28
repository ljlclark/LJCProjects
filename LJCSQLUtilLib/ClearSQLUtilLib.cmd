echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearSQLUtilLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCSQLUtilLib
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCSQLUtilLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCSQLUtilLib
set File=LJCSQLUtilLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCSQLUtilLibDAL.xml

set Project=DataHelper
set File=DataHelper
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues /q
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\LJCSQLUtilLibDAL.xml
del %Solution%\%Project%\%bin%\DataDetail.*

set Project=LJCSQLUtilLibDAL
set File=LJCSQLUtilLibDAL
call %ClearBuild%

set Project=ForeignKeyManagerTest
set File=ForeignKeyManagerTest
call %ClearBuild%
del %Solution%\%Project%\%bin%\ConnectionTemplates.xml
del %Solution%\%Project%\%bin%\DataConfigs.xml
del %Solution%\%Project%\%bin%\LJCSQLUtilLibDAL.xml
