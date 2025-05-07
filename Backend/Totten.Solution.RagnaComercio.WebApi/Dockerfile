FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Instala openssl para gerar o certificado
RUN apt-get update && apt-get install -y openssl

# Cria diretório para o certificado
RUN mkdir /https

# Gera um certificado autofirmado .pfx
RUN openssl req -x509 -nodes -days 365 \
    -newkey rsa:2048 \
    -keyout /https/https.key \
    -out /https/https.crt \
    -subj "/CN=localhost"

RUN openssl pkcs12 -export \
    -out /https/https.pfx \
    -inkey /https/https.key \
    -in /https/https.crt \
    -passout pass:devpass

# Expor as portas padrão do Kestrel
EXPOSE 80
EXPOSE 443

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/https.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=devpass

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Totten.Solution.RagnaComercio.WebApi/Totten.Solution.RagnaComercio.WebApi.csproj", "Totten.Solution.RagnaComercio.WebApi/"]
COPY ["Totten.Solution.RagnaComercio.ApplicationService/Totten.Solution.RagnaComercio.ApplicationService.csproj", "Totten.Solution.RagnaComercio.ApplicationService/"]
COPY ["Totten.Solution.RagnaComercio.Domain/Totten.Solution.RagnaComercio.Domain.csproj", "Totten.Solution.RagnaComercio.Domain/"]
COPY ["Totten.Solution.RagnaComercio.Infra.Cross/Totten.Solution.RagnaComercio.Infra.Cross.csproj", "Totten.Solution.RagnaComercio.Infra.Cross/"]
COPY ["Totten.Solution.RagnaComercio.Infra.Data/Totten.Solution.RagnaComercio.Infra.Data.csproj", "Totten.Solution.RagnaComercio.Infra.Data/"]
RUN dotnet restore "./Totten.Solution.RagnaComercio.WebApi/Totten.Solution.RagnaComercio.WebApi.csproj"
COPY . .
WORKDIR "/src/Totten.Solution.RagnaComercio.WebApi"
RUN dotnet build "./Totten.Solution.RagnaComercio.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Totten.Solution.RagnaComercio.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=base /https /https
ENTRYPOINT ["dotnet", "Totten.Solution.RagnaComercio.WebApi.dll"]
