start-process -FilePath "c:\program files\unity\editor\unity.exe" -ArgumentList "-nographics","-projectPath","$PSScriptRoot\src","-logFile","unity.log","-batchMode","-buildWindows64Player","$PSScriptRoot\output\Endless.exe","-quit" -Wait -NoNewWindow

get-content unity.log
