set Find="?xml"

if %1. == . goto default
set folder=%1
goto work

:default
set folder="C:\Users\Les\Documents\Visual Studio 2017\DocCode" 

:work
LJCCodeLineCounter %folder% "*.html" %Find% > FindLines.txt
