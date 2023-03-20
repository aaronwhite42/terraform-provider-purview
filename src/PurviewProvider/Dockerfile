FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PurviewProvider/PurviewProvider.csproj", "PurviewProvider/"]
RUN dotnet restore "PurviewProvider/PurviewProvider.csproj"
COPY . .
WORKDIR "/src/PurviewProvider"
RUN dotnet build "PurviewProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PurviewProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurviewProvider.dll"]
