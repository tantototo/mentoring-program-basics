FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FirstSymbolConsole/FirstSymbolConsole.csproj", "FirstSymbolConsole/"]
RUN dotnet restore "FirstSymbolConsole/FirstSymbolConsole.csproj"
COPY . .
WORKDIR "/src/FirstSymbolConsole"
RUN dotnet build "FirstSymbolConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FirstSymbolConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FirstSymbolConsole.dll"]
