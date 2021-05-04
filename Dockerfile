FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
COPY bin/Release/net5.0/publish/ App/
WORKDIR /App
EXPOSE 5000
ENV ASPNETCORE_URLS=https://+:5000
ENTRYPOINT ["dotnet", "API.dll"]