FROM mcr.microsoft.com/dotnet/aspnet:5.0-nanoserver-1809
COPY bin/Release/net5.0/publish/ App/
WORKDIR /App
EXPOSE 5000
ENV ASPNETCORE_URLS=https://+:5000
ENTRYPOINT ["dotnet", "API.dll"]