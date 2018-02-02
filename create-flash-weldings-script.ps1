
$root = "$PSScriptRoot\fls-rewinder\weldings"
$content = Get-Content "$PSScriptRoot\weldingTypes.txt"

$content |`
    ForEach-Object -Begin { $filename = "$root\register-coilshift-welding" } -Process { $name = $_.split('-')[0].trim(); `
        Copy-Item -Path "$filename.resx" -Destination  "$root\register-$name-welding.resx"; `
        Copy-Item -Path "$filename.tstest" -Destination "$root\register-$name-welding.tstest" }