@echo off

C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe /t:build /p:Configuration=Debug /p:Platform=Win32
if %ERRORLEVEL% equ 0 (	
	echo ***************** Debug/x86 Build succeeded ***************** 
) else (
	echo ***************** Debug/x86 Build failed! *****************
	echo Error level: %ERRORLEVEL%
	pause
)

C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe /t:build /p:Configuration=Release /p:Platform=Win32
if %ERRORLEVEL% equ 0 (	
	echo ***************** Release/x86 Build succeeded ***************** 
) else (
	echo ***************** Release/x86 Build failed! *****************
	echo Error level: %ERRORLEVEL%
	pause
)

C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe /t:build /p:Configuration=Debug /p:Platform=x64
if %ERRORLEVEL% equ 0 (	
	echo ***************** Debug/x64 Build succeeded ***************** 
) else (
	echo ***************** Debug/x64 Build failed! *****************
	echo Error level: %ERRORLEVEL%
	pause
)

C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe /t:build /p:Configuration=Release /p:Platform=x64
if %ERRORLEVEL% equ 0 (	
	echo ***************** Release/x64 Build succeeded ***************** 
) else (
	echo ***************** Release/x64 Build failed! *****************
	echo Error level: %ERRORLEVEL%
	pause
)