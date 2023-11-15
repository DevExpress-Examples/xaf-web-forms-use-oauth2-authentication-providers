@echo off

for /d /r . %%d in (bin,obj,packages) do @if exist "%%d" rd /s/q "%%d"
FOR /R %%H IN (*.log) DO del "%%H"
FOR /r %%G IN (*.bak) DO del "%%G"
FOR /R %%J IN (*.suo) DO del "%%J"

for /f %%a in ('dir /b *.sln 2^> nul') do set sln=%%a
if defined sln set sln=%cd%\%sln%



