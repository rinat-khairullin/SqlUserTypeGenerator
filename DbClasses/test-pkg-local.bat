rem nuget update packages.config -id SqlUserTypeGenerator -source %1
msbuild DbClasses.csproj /t:rebuild
