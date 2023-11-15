FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["rent/PersonApi.csproj", "rent/"]
RUN dotnet restore "rent/PersonApi.csproj"
COPY . .
WORKDIR "/src/rent"
RUN dotnet build "PersonApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonApi.dll"]