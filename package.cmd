cd src
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild build.proj /property:BuildVersion=1.0.2.0
if errorlevel 1 pause
