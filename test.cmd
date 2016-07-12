REM You may need to comment out the #n directives in the config.wyam file to avoid conflicts
@echo off
..\Wyam\src\Wyam\bin\Debug\Wyam.exe -a "..\Wyam\src\*\bin\Debug\*.dll" -g "SourcePath=../../Wyam/src/Wyam.sln" -pw