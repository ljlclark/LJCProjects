set Find="B =="

if %1. == . goto default
set folder=%1
goto work

:default
set folder="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects" 

:work
LJCCodeLineCounter %folder% "*.cs" %Find% > FindLines.txt
