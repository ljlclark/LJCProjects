echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocal.cmd

BackupChanges "C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDevBKP" "ChangeFile.txt" "LJCProjectsDev"
BackupChanges "C:\Users\Les\Documents\Visual Studio 2022\LJCProjects_Stage" "ChangeFile.txt" "LJCProjectsDev"
BackupChanges "C:\Users\Les\Documents\Visual Studio 2022\LJCProjects" "ChangeFile.txt" "LJCProjectsDev"
pause