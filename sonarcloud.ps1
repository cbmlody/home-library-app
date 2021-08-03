param(
    [string] $sonarSecret
)

$runningDirectory = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition

$testOutputDir = "$runningDirectory/TestResults"

if (Test-Path $testOutputDir)
{
    Write-host "Cleaning temporary Test Output path $testOutputDir"
    Remove-Item $testOutputDir -Recurse -Force
}

.\.sonar\scanner\dotnet-sonarscanner begin /k:"cbmlody_home-library-app" /o:"cbmlody" /d:sonar.login="$sonarSecret" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.cs.vstest.reportsPaths=TestResults/*.trx /d:sonar.cs.opencover.reportsPaths=TestResults/*/coverage.opencover.xml /d:sonar.coverage.exclusions="**/HomeLibraryAPI.Tests*/**/*, **/Migrations/**, **/Entities/*, **/Extensions/*"
Write-Host "Restoring dependencies..."
dotnet restore .\HomeLibraryAPI\HomeLibraryAPI.sln

Write-Host "Starting build..."
dotnet build .\HomeLibraryAPI\ --configuration Release

Write-Host "Starting tests..."
dotnet test .\HomeLibraryAPI\HomeLibraryAPI.sln --collect:"XPlat Code Coverage" -r .\TestResults --logger "trx;LogFileName=unittests.trx" --no-build --no-restore --configuration Release -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

.\.sonar\scanner\dotnet-sonarscanner end /d:sonar.login="$sonarSecret"