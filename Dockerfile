FROM mcr.microsoft.com/dotnet/sdk:5.0.202-nanoserver-1809 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0.5-nanoserver-1809 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "api.dll"]

