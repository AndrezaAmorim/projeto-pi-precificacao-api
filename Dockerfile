
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ProjetoPiPrecificacao/ProjetoPiPrecificacao.csproj", "ProjetoPiPrecificacao/"]
RUN dotnet restore "./ProjetoPiPrecificacao/ProjetoPiPrecificacao.csproj"
COPY . .

RUN dotnet build "src/ProjetoPiPrecificacao/ProjetoPiPrecificacao.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/ProjetoPiPrecificacao/ProjetoPiPrecificacao.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoPiPrecificacao.dll"]