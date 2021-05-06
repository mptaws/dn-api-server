FROM mcr.microsoft.com/dotnet/sdk:4.8-20210309-windowsservercore-ltsc2019 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:4.8-20210209-windowsservercore-ltsc2019
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "API.dll"]