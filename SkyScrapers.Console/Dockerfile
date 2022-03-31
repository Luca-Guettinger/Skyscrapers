FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SkyScrapers.Console/SkyScrapers.Console.csproj", "SkyScrapers.Console/"]
RUN dotnet restore "SkyScrapers.Console/SkyScrapers.Console.csproj"
COPY . .
WORKDIR "/src/SkyScrapers.Console"
RUN dotnet build "SkyScrapers.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkyScrapers.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkyScrapers.Console.dll"]
