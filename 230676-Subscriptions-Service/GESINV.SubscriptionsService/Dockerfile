#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GESINV.SubscriptionsService/GESINV.SubscriptionsService.csproj", "GESINV.SubscriptionsService/"]
RUN dotnet restore "GESINV.SubscriptionsService/GESINV.SubscriptionsService.csproj"
COPY . .
WORKDIR "/src/GESINV.SubscriptionsService"
RUN dotnet build "GESINV.SubscriptionsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GESINV.SubscriptionsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5300
ENTRYPOINT ["dotnet", "GESINV.SubscriptionsService.dll"]