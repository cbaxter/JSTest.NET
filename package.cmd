set /p BuildVersion=Build Version: %=%
cd src
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild build.proj /target:Package /property:BuildVersion=%BuildVersion%
if errorlevel 1 pause
cd..