# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Order/Presentation/AuraShop.Order.WebApi/AuraShop.Order.API.csproj", "Services/Order/Presentation/AuraShop.Order.WebApi/"]
COPY ["Services/Order/Infrastructure/AuraShop.Order.Persistence/AuraShop.Order.Persistence.csproj", "Services/Order/Infrastructure/AuraShop.Order.Persistence/"]
COPY ["Services/Order/Core/AuraShop.Order.Application/AuraShop.Order.Application/AuraShop.Order.Application.csproj", "Services/Order/Core/AuraShop.Order.Application/AuraShop.Order.Application/"]
COPY ["Shared/AuraShop.Shared/AuraShop.Shared.csproj", "Shared/AuraShop.Shared/"]
COPY ["Services/Order/Core/AuraShop.Order.Domain/AuraShop.Order.Domain.csproj", "Services/Order/Core/AuraShop.Order.Domain/"]
RUN dotnet restore "./Services/Order/Presentation/AuraShop.Order.WebApi/AuraShop.Order.API.csproj"
COPY . .
WORKDIR "/src/Services/Order/Presentation/AuraShop.Order.WebApi"
RUN dotnet build "./AuraShop.Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuraShop.Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuraShop.Order.API.dll"]