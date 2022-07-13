set Find="&lt;?xml"

if %1. == . goto default
set folder=%1
goto work

:default
set folder="C:\Users\Les\Documents\Visual Studio 2017\LJCProjects" 

:work
LJCCodeLineCounter %folder% "*.xml" %Find% > FindLines.txt
