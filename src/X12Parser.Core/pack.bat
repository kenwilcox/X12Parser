REM del *.nupkg
REM nuget pack X12Parser.Core.csproj -Prop Configuration=Release
pushd bin\Release
nuget push *.nupkg -Source https://api.nuget.org/v3/index.json
popd