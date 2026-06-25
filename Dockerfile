# Etapa 1: Compilación usando el SDK oficial de .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Copiar la solución y todos los archivos del repositorio
COPY . ./

# Restaurar dependencias enfocándose en el proyecto de la API
RUN dotnet restore src/SGIP.API/SGIP.API.csproj

# Compilar y publicar en la carpeta 'out'
RUN dotnet publish src/SGIP.API/SGIP.API.csproj -c Release -o out

# Etapa 2: Entorno de ejecución ligero
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# Railway inyecta la variable PORT automáticamente, nos aseguramos de escuchar en ella
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SGIP.API.dll"]