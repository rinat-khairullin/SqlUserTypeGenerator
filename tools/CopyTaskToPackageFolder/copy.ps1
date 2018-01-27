param(
[string]$SolutionFolder
);

[string]$userTypeGeneratorProjectDir = "$SolutionFolder`SqlUserTypeGenerator";

[xml]$xmlNuspec = Get-Content -Path $userTypeGeneratorProjectDir\SqlUserTypeGenerator.nuspec
[string]$packageVersion = $xmlNuspec.package.metadata.version;

[string]$packageDir = "$SolutionFolder`\packages\SqlUserTypeGenerator.$packageVersion";

Copy-Item "$userTypeGeneratorProjectDir`\bin\Debug\SqlUserTypeGenerator.dll" "$packageDir\lib\net452\"

Copy-Item "$userTypeGeneratorProjectDir`\build\*.targets" "$packageDir\build\"
