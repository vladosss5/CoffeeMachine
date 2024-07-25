# Используем базовый образ ASP.NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 8081

# Используем SDK 8.0 для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем и восстанавливаем зависимости
COPY ../ .
WORKDIR "CoffeeMachine.API"
RUN dotnet restore "CoffeeMachine.API.csproj"

# Копируем все файлы и собираем проект
#COPY . .
#WORKDIR "CoffeeMachine.API"
RUN dotnet build "CoffeeMachine.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикуем проект
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CoffeeMachine.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Создаем финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeMachine.API.dll"]