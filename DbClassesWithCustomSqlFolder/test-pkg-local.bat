rem nuget update packages.config -id SqlUserTypeGenerator -source %1
msbuild DbClassesWithCustomSqlFolder.csproj /t:rebuild
