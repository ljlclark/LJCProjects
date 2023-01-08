echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearDocLib.cmd

set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCDocLib
set ClearBuild=..\..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=CoreUtilities\LJCDocLib
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=DocGroupEditor
set File=DocGroupEditor
call %ClearBuild%
del %Solution%\%Project%\bin\Debug\LJCDocLibDAL.xml

set Project=LJCDocGen
set File=LJCDocGen
call %ClearBuild%
del %Solution%\%Project%\%bin%\ControlValues*.xml
rmdir %Solution%\%Project%\%bin%\ControlValues
del %Solution%\%Project%\%bin%\*.log
del %Solution%\%Project%\%bin%\LJCDocGroupEditor.exe
del %Solution%\%Project%\%bin%\LJCDocGenLib.xml
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml
del %Solution%\%Project%\%bin%\LJCDocLib.xml
del %Solution%\%Project%\%bin%\LJCDocObjLib.xml
del %Solution%\%Project%\%bin%\LJCDocXMLObjLib.xml
del %Solution%\%Project%\%bin%\LJCGenTextLib.xml

set Project=LJCDocGenLib
set File=LJCDocGenLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml
del %Solution%\%Project%\%bin%\LJCDocLib.xml
del %Solution%\%Project%\%bin%\LJCDocObjLib.xml
del %Solution%\%Project%\%bin%\LJCDocXMLObjLib.xml
del %Solution%\%Project%\%bin%\LJCGenTextLib.xml

set Project=LJCDocLibDAL
set File=LJCDocLibDAL
call %ClearBuild%

set Project=LJCDocObjLib
set File=LJCDocObjLib
call %ClearBuild%
del %Solution%\%Project%\%bin%\LJCDocLibDAL.xml
del %Solution%\%Project%\%bin%\LJCDocXMLObjLib.xml

set Project=LJCDocXMLObjLib
set File=LJCDocXMLObjLib
call %ClearBuild%

set Project=TestSyntaxConsole
set File=TestSyntaxConsole
call %ClearBuild%
