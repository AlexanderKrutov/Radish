@echo off

set PATH_MSBUILD=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
%PATH_MSBUILD%msbuild Radish.sln /p:Configuration="Release" /p:Platform="AnyCPU" /t:Clean;Build

set BUILD_STATUS=%ERRORLEVEL% 
if %BUILD_STATUS%==0 goto pack 
if not %BUILD_STATUS%==0 goto fail 
 
:fail 
exit /b 1 
 
:pack
.nuget\nuget.exe pack "Radish\Radish.csproj" -Prop Configuration=Release -OutputDirectory "."