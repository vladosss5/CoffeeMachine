FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CoffeeMachine.API/CoffeeMachine.API.csproj", "src/CoffeeMachine.API/"]
COPY ["src/CoffeeMachine.Application/CoffeeMachine.Application.csproj", "src/CoffeeMachine.Application/"]
COPY ["src/CoffeeMachine.Core/CoffeeMachine.Core.csproj", "src/CoffeeMachine.Core/"]
COPY ["src/CoffeeMachine.Persistence/CoffeeMachine.Persistence.csproj", "src/CoffeeMachine.Persistence/"]
RUN dotnet restore "src/CoffeeMachine.API/CoffeeMachine.API.csproj"
COPY . .
WORKDIR "/src/src/CoffeeMachine.API"
RUN dotnet build "CoffeeMachine.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CoffeeMachine.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeMachine.API.dll"]
