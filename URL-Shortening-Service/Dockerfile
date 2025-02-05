# Establecer la imagen base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Establecer la imagen de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["URL-Shortening-Service.csproj", "./"]
RUN dotnet restore "URL-Shortening-Service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "URL-Shortening-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "URL-Shortening-Service.csproj" -c Release -o /app/publish

# Establecer la imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ["dotnet", "URL-Shortening-Service.dll"]