param(
    [string]$rootPath
)

$allTests = Get-ChildItem $rootPath -Recurse -Filter "*Tests.dll"
foreach ($assembly in $allTests) {
    Remove-Item $assembly.FullName
}
