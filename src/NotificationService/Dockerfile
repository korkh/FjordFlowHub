# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Копируем .csproj и восстанавливаем зависимости
COPY src/NotificationService/NotificationService.csproj ./src/NotificationService/
RUN dotnet restore ./src/NotificationService/NotificationService.csproj

# Копируем всё остальное и собираем
COPY . .
RUN dotnet publish ./src/NotificationService/NotificationService.csproj -c Debug -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .

# Включаем установку дебаггера для контейнера
RUN apt-get update && apt-get install -y procps

ENTRYPOINT ["dotnet", "NotificationService.dll"]