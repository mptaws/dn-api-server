# escape=`

ARG REPO=mcr.microsoft.com/dotnet/aspnet
FROM $REPO:5.0-windowsservercore-ltsc2019

ENV `
    # Unset ASPNETCORE_URLS from aspnet base image
    ASPNETCORE_URLS= `
    DOTNET_SDK_VERSION=5.0.202 `
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true `
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip `
    # PowerShell telemetry for docker image usage
    POWERSHELL_DISTRIBUTION_CHANNEL=PSDocker-DotnetSDK-WindowsServerCore-ltsc2019

RUN powershell -Command "`
    $ErrorActionPreference = 'Stop'; `
    $ProgressPreference = 'SilentlyContinue'; `
    `
    # Retrieve .NET SDK
    Invoke-WebRequest -OutFile dotnet.zip https://dotnetcli.azureedge.net/dotnet/Sdk/$Env:DOTNET_SDK_VERSION/dotnet-sdk-$Env:DOTNET_SDK_VERSION-win-x64.zip; `
    $dotnet_sha512 = 'ba76852b979ec98034d70a0c8012f7ec1c6638129d66121368766f3da423f46be942a6a4f2d8dafa8bdbd91218d7eeed89cd3c1818fb0df2656e9f9f6a7c6893'; `
    if ((Get-FileHash dotnet.zip -Algorithm sha512).Hash -ne $dotnet_sha512) { `
    Write-Host 'CHECKSUM VERIFICATION FAILED!'; `
    exit 1; `
    }; `
    tar -C $Env:ProgramFiles\dotnet -oxzf dotnet.zip ./packs ./sdk ./templates ./LICENSE.txt ./ThirdPartyNotices.txt ./shared/Microsoft.WindowsDesktop.App; `
    Remove-Item -Force dotnet.zip; `
    `
    # Install PowerShell global tool
    $powershell_version = '7.1.3'; `
    Invoke-WebRequest -OutFile PowerShell.Windows.x64.$powershell_version.nupkg https://pwshtool.blob.core.windows.net/tool/$powershell_version/PowerShell.Windows.x64.$powershell_version.nupkg; `
    $powershell_sha512 = '6a390cddbc88fe9a645363ccec9819603cfb754b5dead3161197f45c4b248b6bd4176b369ded139d7f9d485105351395de07cd8e61ed4f22a9cdd34bfaff9fc5'; `
    if ((Get-FileHash PowerShell.Windows.x64.$powershell_version.nupkg -Algorithm sha512).Hash -ne $powershell_sha512) { `
    Write-Host 'CHECKSUM VERIFICATION FAILED!'; `
    exit 1; `
    }; `
    & $Env:ProgramFiles\dotnet\dotnet tool install --add-source . --tool-path $Env:ProgramFiles\powershell --version $powershell_version PowerShell.Windows.x64; `
    & $Env:ProgramFiles\dotnet\dotnet nuget locals all --clear; `
    Remove-Item -Force PowerShell.Windows.x64.$powershell_version.nupkg; `
    Remove-Item -Path $Env:ProgramFiles\powershell\.store\powershell.windows.x64\$powershell_version\powershell.windows.x64\$powershell_version\powershell.windows.x64.$powershell_version.nupkg -Force;"

RUN setx /M PATH "%PATH%;C:\Program Files\powershell"

# Trigger first run experience by running arbitrary cmd
RUN dotnet help
WORKDIR /App
COPY bin/Release/net5.0/publish/ App/

EXPOSE 5000
ENV ASPNETCORE_URLS=https://+:5000
ENTRYPOINT ["dotnet", "API.dll"]
