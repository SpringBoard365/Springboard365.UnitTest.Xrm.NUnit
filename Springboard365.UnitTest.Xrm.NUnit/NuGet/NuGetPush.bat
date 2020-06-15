SET packageVersion=2.0.0-beta1

NuGet.exe pack ../Springboard365.UnitTest.Xrm.NUnit.csproj -Build -Symbols -Version %packageVersion% -Properties Configuration=Release

NuGet.exe push Springboard365.UnitTest.Xrm.NUnit.%packageVersion%.nupkg -source "https://api.nuget.org/v3/index.json"

pause