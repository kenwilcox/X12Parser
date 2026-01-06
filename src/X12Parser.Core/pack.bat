REM del *.nupkg
REM nuget pack X12Parser.Core.csproj -Prop Configuration=Release
REM - right click on the X12Parser.Core project is VS and select Pack
pushd bin\Release
nuget push *.nupkg -Source https://api.nuget.org/v3/index.json
popd