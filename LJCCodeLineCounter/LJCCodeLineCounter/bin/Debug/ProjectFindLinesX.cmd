set Find="2017,2018,2019,2020"

if %1. == . goto default
set folder=%1
goto work

:default
set folder="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects" 

:work
LJCCodeLineCounter %folder% "*.cs" %Find% > FindLines.txt
