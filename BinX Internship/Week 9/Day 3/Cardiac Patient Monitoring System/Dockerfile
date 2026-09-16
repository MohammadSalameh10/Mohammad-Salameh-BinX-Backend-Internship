FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["CardiacPatientMonitoringSystem.API/CardiacPatientMonitoringSystem.API.csproj", "CardiacPatientMonitoringSystem.API/"]

RUN dotnet restore "CardiacPatientMonitoringSystem.API/CardiacPatientMonitoringSystem.API.csproj"

COPY . .

WORKDIR "/src/CardiacPatientMonitoringSystem.API"

RUN dotnet publish "CardiacPatientMonitoringSystem.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

CMD ["sh", "-c", "dotnet CardiacPatientMonitoringSystem.API.dll --urls http://0.0.0.0:${PORT:-8080}"]