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

$version = Invoke-Gitversion
$assemblyVer = $version.assemblyVersion

$branch = git branch --show-current
Write-Host "branch is $branch"

dotnet tool restore
dotnet tool run dotnet-sonarscanner begin `
  /k:"cbmlody_home-library-app" `            # Key of the project
  /v:"$assemblyVer" `                        # Version of the assemly as calculated by gitversion
  /o:"cbmlody" `                             # account
  /d:sonar.login="$sonarSecret" `            # Secret
  /d:sonar.host.url="https://sonarcloud.io" `
  /d:sonar.cs.vstest.reportsPaths=TestResults/*.trx ` # Path where I'm expecting to find test result in trx format
  /d:sonar.cs.opencover.reportsPaths=TestResults/*/coverage.opencover.xml ` # Name of the code coverage file
  /d:sonar.coverage.exclusions="**Test*.cs" `   # asembly names to be excluded from code coverage
  /d:sonar.branch.name="$branch"                # Actual branch I'm analyzing.

dotnet restore HomeLibraryAPI.sln
dotnet build HomeLibraryAPI.sln --configuration Release

# Now execute tests with special attention to produce output
# that can be easily read by SonarCloud analyzer
dotnet test ".\HomeLibraryAPI\HomeLibraryAPI.sln" --collect:"XPlat Code Coverage" -r .\TestResults --logger "trx;LogFileName=unittests.trx" --no-build --no-restore --configuration Release -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

dotnet tool run dotnet-sonarscanner end /d:sonar.login="$sonarSecret"