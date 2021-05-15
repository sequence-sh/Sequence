$dnSpyVersion = 'v6.1.8'
$dnSpyUrl     = 'https://github.com/dnSpy/dnSpy.git'
$LibPath      = 'lib'
$ExePath      = './edr.exe'
$GitUri       = 'https://github.com/git-for-windows/git/releases/download/v2.31.1.windows.1/MinGit-2.31.1-64-bit.zip'

Invoke-WebRequest -Uri $GitUri -OutFile mingit.zip -UseBasicParsing
Expand-Archive -Path mingit.zip -DestinationPath mingit
./mingit/cmd/git.exe clone --single-branch --branch $dnSpyVersion --sparse $dnSpyUrl
./mingit/cmd/git.exe -C dnspy sparse-checkout init --cone
./mingit/cmd/git.exe -C dnspy sparse-checkout set Build/AppHostPatcher
dotnet publish .\dnSpy\Build\AppHostPatcher\ -f 'net5.0-windows' -c Release -o ./ahp
Copy-Item -Path (Join-Path $LibPath $ExePath) -Destination .
.\ahp\AppHostPatcher.exe $ExePath -d $LibPath
