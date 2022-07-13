echo Copyright (c) Lester J. Clark 2022 - All Rights Reserved

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

set /a counter=1
echo on
echo ----------------- %counter% - LJCNetCommon --------------- > Build.txt
echo LJC.Net.Common >> Build.txt
msbuild LJCNetCommon\LJCNetCommon.sln
pause
