@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework\v4.0.30319
chdir /d %~dp0
installutil "Elysium.Notifications.Server.exe"
pause