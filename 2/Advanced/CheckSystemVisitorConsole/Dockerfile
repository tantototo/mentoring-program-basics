FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CheckSystemVisitorConsole/CheckSystemVisitorConsole.csproj", "CheckSystemVisitorConsole/"]
RUN dotnet restore "CheckSystemVisitorConsole/CheckSystemVisitorConsole.csproj"
COPY . .
WORKDIR "/src/CheckSystemVisitorConsole"
RUN dotnet build "CheckSystemVisitorConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CheckSystemVisitorConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheckSystemVisitorConsole.dll"]
