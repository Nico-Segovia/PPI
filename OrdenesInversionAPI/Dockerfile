FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrdenesInversionAPI/OrdenesInversionAPI.csproj", "OrdenesInversionAPI/"]
RUN dotnet restore "OrdenesInversionAPI/OrdenesInversionAPI.csproj"
COPY . .
WORKDIR "/src/OrdenesInversionAPI" 
RUN dotnet build "OrdenesInversionAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdenesInversionAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdenesInversionAPI.dll"]
