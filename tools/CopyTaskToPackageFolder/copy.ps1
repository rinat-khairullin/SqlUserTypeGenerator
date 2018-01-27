param(
[string]$SourceFile,
[string]$SolutionFolder
);

[xml]$xmlNuspec = Get-Content -Path $SolutionFolder\SqlUserTypeGenerator\SqlUserTypeGenerator.nuspec
[string]$packageVersion = $xmlNuspec.package.metadata.version;

Copy-Item $SourceFile "$SolutionFolder\packages\SqlUserTypeGenerator.$packageVersion\lib\"
