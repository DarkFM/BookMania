
$GulpJob = {
    gulp --gulpfile ".\wwwroot\gulpfile.js" watch
}

$DotnetWatch = {
    dotnet watch run
}

# https://stackoverflow.com/a/17388991
function start-jobhere([scriptblock]$block) {
    return Start-Job -Init ([ScriptBlock]::Create("Set-Location '$pwd'")) -Script $block
}

$dotnetwatchJOB = start-jobhere($DotnetWatch);
Start-Sleep -s 10
$gulpJOB = start-jobhere($GulpJob);

Write-Host "BrowserSync and Gulp-Sass started"
Write-Host "Dotnet Watch Run started"
Write-Host "Press Ctrl + C to terminate..."

# wait till terminating command Ctrl+C is issued
# https://powershell.org/forums/topic/wait-till-ctrl-c/
[console]::TreatControlCAsInput = $true
while ($true)
{
    if ([console]::KeyAvailable)
    {
        $key = [system.console]::readkey($true)

        if (($key.modifiers -band [consolemodifiers]"control") -and ($key.key -eq "C"))
        {
            "Terminating Processes. Please wait..."
            Stop-Job -Id $dotnetwatchJOB.Id, $gulpJOB.Id
            break
        }
    }
}
