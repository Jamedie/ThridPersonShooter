@echo off
echo %cd%
echo "this will delete some files within the directory above!  Make sure unity is not running!"
pause
echo "are you sure you would like to do this?"
pause
rd /s /q Library
rd /s /q Temp
rd /s /q Builds
rd /s /q builds
rd /s /q Build
rd /s /q build
rd /s /q obj
rd /s /q .vsconfig
rd /s /q .vs
del /s /q /f *.csproj
del /s /q /f *.pidb
del /s /q /f *.unityproj
del /s /q /f *.DS_Store
del /s /q /f *.sln
del /s /q /f *.userprefs
echo "done."
echo "This script will now self-destruct. Please ignore the next error message"
pause
del "%~f0"