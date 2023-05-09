$serviceNameInput = $args[0]

$solution = Get-ChildItem *.sln | Select-Object -First 1 | Get-ItemProperty -Name Name
$name = (Get-Item $solution.PSPath).Basename
$pascalCase = $serviceNameInput 
$service = $pascalCase + "Service"
$folder = $serviceNameInput.ToLower()

abp new "$name.$service" -t module --no-ui -o services\$folder
Remove-Item -Recurse -Force (Get-ChildItem -Path "services\$folder" -Recurse -Include *.IdentityServer)
Remove-Item -Recurse -Force (Get-ChildItem -Path "services\$folder" -Recurse -Include *.MongoDB.Tests)
Remove-Item -Recurse -Force (Get-ChildItem -Path "services\$folder" -Recurse -Include *.MongoDB)
Remove-Item -Recurse -Force (Get-ChildItem -Path "services\$folder" -Recurse -Include *.Host.Shared)
Remove-Item -Recurse -Force (Get-ChildItem -Path "services\$folder" -Recurse -Include *.Installer)
dotnet sln ".\$name.sln" add (Get-ChildItem -Path "services\$folder" -Recurse -Include *.csproj)
