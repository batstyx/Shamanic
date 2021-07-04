Param(
    [Parameter(Mandatory=$true)]
    [string]$project,
    [Parameter(Mandatory=$true)]
    [int]$build,
    [Parameter()]
    [int]$overrideMajor,
    [Parameter()]
    [int]$overrideMinor,
    [Parameter()]
    [int]$overridePatch
)

$solutionPath = $(Resolve-Path "$PSScriptRoot\..").Path
$assemblyInfoFile = "$solutionPath\$project\Properties\AssemblyInfo.cs"

$assemblyInfo = [IO.File]::ReadAllText($assemblyInfoFile)
$versionRegex = [System.Text.RegularExpressions.Regex]::new('Version\(\"(?<major>\d+)\.(?<minor>\d+)(\.(?<patch>\d+))?(\.(?<build>\d+))?\"\)')

$match = $versionRegex.Match($assemblyInfo)
if(!$match.Success) {
    throw "No version numbers formatted <major>.<minor>(.<patch>(.<build>)) found in $assemblyInfoFile"
}
$major = if($overrideMajor) {$overrideMajor} else {$match.Groups['major'].Value}
$minor = if($overrideMinor) {$overrideMinor} else {$match.Groups['minor'].Value}
$patch = if($overridePatch) {$overridePatch} else {$match.Groups['patch'].Value}


$assemblyVersion = "$major.$minor.$patch.$build"
$assemblyInfo = $versionRegex.Replace($assemblyInfo, 'Version("' + $assemblyVersion + '")')
[IO.File]::WriteAllText($assemblyInfoFile, $assemblyInfo)

Write-Host "$project AssemblyVersion=$assemblyVersion"
Write-Output $assemblyVersion