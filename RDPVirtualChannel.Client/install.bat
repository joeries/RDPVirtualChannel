reg add "HKEY_CURRENT_USER\Software\Microsoft\Terminal Server Client\Default\AddIns\RDPVC" /v Name /d "%~dp0RDPVirtualChannel.Client.dll" /f
pause