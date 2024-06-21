FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CoffeeMachine.IntegrationTests/CoffeeMachine.IntegrationTests.csproj", "src/CoffeeMachine.IntegrationTests/"]
COPY ["src/CoffeeMachine.API/CoffeeMachine.API.csproj", "src/CoffeeMachine.API/"]
COPY ["src/CoffeeMachine.Application/CoffeeMachine.Application.csproj", "src/CoffeeMachine.Application/"]
COPY ["src/CoffeeMachine.Core/CoffeeMachine.Core.csproj", "src/CoffeeMachine.Core/"]
COPY ["src/CoffeeMachine.Persistence/CoffeeMachine.Persistence.csproj", "src/CoffeeMachine.Persistence/"]
RUN dotnet restore "src/CoffeeMachine.IntegrationTests/CoffeeMachine.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/src/CoffeeMachine.IntegrationTests"
RUN dotnet build "CoffeeMachine.IntegrationTests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet test --nobuild -c 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeMachine.IntegrationTests.dll"]
