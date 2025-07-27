# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o .sln e todos os .csproj
COPY *.sln ./
COPY "AppDeCastracao.csproj" ./

# Restaura dependências
RUN dotnet restore AppDeCastracao.sln

# Copia todo o restante do código
COPY . .

# Publica o projeto
RUN dotnet publish AppDeCastracao.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "AppDeCastracao.dll"]
