call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDataTransform.cmd nopause
msbuild LJCDataTransform.sln -t:rebuild
call UpdateDataTransformPost.cmd
pause
