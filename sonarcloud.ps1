param(
    [string] $sonarSecret
)

Install-package BuildUtils -Confirm:$false -Scope CurrentUser -Force
Import-Module BuildUtils

$runningDirectory = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition

$testOutputDir = "$runningDirectory/TestResults"

if (Test-Path $testOutputDir)
{
    Write-host "Cleaning temporary Test Output path $testOutputDir"
    Remove-Item $testOutputDir -Recurse -Force
}

.\.sonar\scanner\dotnet-sonarscanner begin /k:"cbmlody_home-library-app" /o:"cbmlody" /d:sonar.login="$sonarSecret" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.cs.vstest.reportsPaths=TestResults/*.trx /d:sonar.cs.opencover.reportsPaths=TestResults/*/coverage.opencover.xml /d:sonar.coverage.exclusions="**Test*.cs" dotnet restore .\HomeLibraryAPI\HomeLibraryAPI.sln

Write-Host "Starting build..."
dotnet build .\HomeLibraryAPI\HomeLibraryAPI.sln --configuration Release
Write-Host "Finished build."

Write-Host "Starting tests..."
dotnet test ".\HomeLibraryAPI\HomeLibraryAPI.sln" --collect:"XPlat Code Coverage" -r .\TestResults --logger "trx;LogFileName=unittests.trx" --no-build --no-restore --configuration Release -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
Write-Host "Finished tests."

dotnet tool run dotnet-sonarscanner end /d:sonar.login="$sonarSecret"