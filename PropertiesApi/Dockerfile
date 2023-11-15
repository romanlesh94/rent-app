FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PropertiesApi/HouseApi.csproj", "PropertiesApi/"]
RUN dotnet restore "PropertiesApi/HouseApi.csproj"
COPY . .
WORKDIR "/src/PropertiesApi"
RUN dotnet build "HouseApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HouseApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HouseApi.dll"]