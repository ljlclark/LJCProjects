echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDocLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCGenDoc
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCGenDoc
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=GenDocScript
set File=GenDocScript
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml

set Project=LJCDocObjLib
set File=LJCDocObjLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml
del %Solution%\%Project%\%bin%\LJCDocXMLObjLib.xml

set Project=LJCDocXMLObjLib
set File=LJCDocXMLObjLib
call %ClearBuild%

set Project=LJCGenDoc
set File=LJCGenDoc
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues\*.xml
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\*.log
del %Solution%\%Project%\%bin%\LJCDocGroupEditor.exe
del %Solution%\%Project%\%bin%\LJCDocGenLib.xml
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml
del %Solution%\%Project%\%bin%\LJCDocObjLib.log
del %Solution%\%Project%\%bin%\LJCDocObjLib.xml
del %Solution%\%Project%\%bin%\LJCDocXMLObjLib.xml

set Project=LJCGenDocLib
set File=LJCGenDocLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml
del %Solution%\%Project%\%bin%\LJCDocLib.xml
del %Solution%\%Project%\%bin%\LJCDocObjLib.xml
del %Solution%\%Project%\%bin%\LJCDocXMLObjLib.xml
del %Solution%\%Project%\%bin%\LJCGenTextLib.xml

set Project=LJCGenDocDAL
set File=LJCGenDocDAL
call %ClearBuild%

set Project=TestSyntaxConsole
set File=TestSyntaxConsole
call %ClearBuild%
