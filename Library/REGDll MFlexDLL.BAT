@echo off
color 2a
if not exist MflexC.dll goto er1
if not exist MflexC.tlb goto er2
echo updating c:\windows\System32\MflexC.dll ...
copy MflexC.dll %systemroot%\system32\MflexC.dll
copy MflexC.tlb %systemroot%\system32\MflexC.tlb
echo registering ...
path=C:\Windows\Microsoft.NET\Framework\v4.0.30319
regasm.exe %systemroot%\System32\MflexC.dll /codebase
goto end
:er1
echo .\MflexC.dll not exists!
:er2
echo .\MflexC.tlb not exists!
:end
pause>nul
