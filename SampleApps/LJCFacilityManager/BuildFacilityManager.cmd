call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateFacilityManager.cmd nopause
msbuild LJCFacilityManager.sln -t:rebuild
call UpdateFacilityManagerPost.cmd
pause
