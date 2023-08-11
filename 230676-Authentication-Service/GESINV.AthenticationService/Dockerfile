#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GESINV.AthenticationService/GESINV.AthenticationService.csproj", "GESINV.AthenticationService/"]
RUN dotnet restore "GESINV.AthenticationService/GESINV.AthenticationService.csproj"
COPY . .
WORKDIR "/src/GESINV.AthenticationService"
RUN dotnet build "GESINV.AthenticationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GESINV.AthenticationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GESINV.AthenticationService.dll"]