set Find="grid columns"

if %1. == . goto default
set folder=%1
goto work

:default
set folder="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects" 

:work
LJCCodeLineCounter %folder% "*.xml" %Find% > FindLines.txt
