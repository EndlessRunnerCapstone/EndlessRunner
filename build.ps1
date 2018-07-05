$proc = start-process -FilePath "c:\program files\unity\editor\unity.exe" -ArgumentList "-nographics","-projectPath","$PSScriptRoot\src","-logFile","unity.log","-batchMode","-buildWindows64Player","$PSScriptRoot\output\Endless.exe","-quit" -Wait -NoNewWindow

get-content unity.log

if($proc.ExitCode -gt 0)
{
  exit $proc.ExitCode
}

$proc = start-process -FilePath "c:\program files\unity\editor\unity.exe" -ArgumentList "-nographics","-projectPath","$PSScriptRoot\src","-logFile","unityGL.log","-batchMode","-executeMethod","WebGLBuilder.build","-quit" -Wait -NoNewWindow

get-content unityGL.log

exit $proc.ExitCode
