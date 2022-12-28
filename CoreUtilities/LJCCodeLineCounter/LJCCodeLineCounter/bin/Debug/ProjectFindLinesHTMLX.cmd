set Find="Keywords"

if %1. == . goto default
set folder=%1
goto work

:default
rem set folder="C:\Users\Les\Documents\Visual Studio 2022\LJCDocCode" 
set folder="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects" 

:work
LJCCodeLineCounter %folder% "*.html" %Find% > FindLines.txt
