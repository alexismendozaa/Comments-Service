@echo off
REM ==== Windows CMD ====
dotnet format --verify-no-changes
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet build --no-restore
if %errorlevel% neq 0 exit /b %errorlevel%
echo Chequeo estatico OK! 🚦
exit /b 0
