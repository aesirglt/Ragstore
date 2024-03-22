#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Totten.Solution.Ragstore.WebApi/Totten.Solution.Ragstore.WebApi.csproj", "Totten.Solution.Ragstore.WebApi/"]
RUN dotnet restore "./Totten.Solution.Ragstore.WebApi/Totten.Solution.Ragstore.WebApi.csproj"
COPY . .
WORKDIR "/src/Totten.Solution.Ragstore.WebApi"
RUN dotnet build "./Totten.Solution.Ragstore.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Totten.Solution.Ragstore.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Totten.Solution.Ragstore.WebApi.dll"]