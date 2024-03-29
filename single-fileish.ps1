$ErrorActionPreference = 'Stop'

$dnSpyVersion = 'v6.1.8'
$dnSpyUrl     = 'https://github.com/dnSpy/dnSpy.git'
$LibPath      = 'lib'
$ExePath      = './sequence.exe'
$GitUri       = 'https://github.com/git-for-windows/git/releases/download/v2.31.1.windows.1/MinGit-2.31.1-64-bit.zip'

Invoke-WebRequest -Uri $GitUri -OutFile mingit.zip -UseBasicParsing
Expand-Archive -Path mingit.zip -DestinationPath mingit

./mingit/cmd/git.exe clone --single-branch --branch $dnSpyVersion --sparse $dnSpyUrl
./mingit/cmd/git.exe -C dnspy sparse-checkout init --cone
./mingit/cmd/git.exe -C dnspy sparse-checkout set Build/AppHostPatcher

# Hack until .net6 is added to dnSpy
$csproj = Get-Content .\dnSpy\DnSpyCommon.props -Raw
$csproj = $csproj -replace 'net5\.0-windows', 'net6.0-windows'
Set-Content -Value $csproj -Path .\dnSpy\DnSpyCommon.props

dotnet publish .\dnSpy\Build\AppHostPatcher\ -f 'net6.0-windows' -c Release -o ./ahp
Copy-Item -Path (Join-Path $LibPath $ExePath) -Destination .
.\ahp\AppHostPatcher.exe $ExePath -d $LibPath
