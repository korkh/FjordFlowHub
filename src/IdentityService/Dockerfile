# Stage 1: Сборка
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Копируем файл решения и проект сервиса
COPY FjordFlowHub.sln ./
COPY src/IdentityService/IdentityService.csproj ./src/IdentityService/

# Восстанавливаем только нужные зависимости
RUN dotnet restore ./src/IdentityService/IdentityService.csproj

# Копируем исходный код сервиса (и папки, которые могут быть нужны для сборки решения)
COPY . .

# Собираем и публикуем
RUN dotnet publish ./src/IdentityService/IdentityService.csproj -c Release -o /app/publish

# Stage 2: Запуск
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Открываем порты для IdentityServer
EXPOSE 80
EXPOSE 443

# Копируем готовый бинарный код
COPY --from=build /app/publish .

# Точка входа
ENTRYPOINT ["dotnet", "IdentityService.dll"]