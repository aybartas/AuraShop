#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Order/Presentation/AuraShop.Order.WebApi/AuraShop.Order.WebApi.csproj", "Services/Order/Presentation/AuraShop.Order.WebApi/"]
COPY ["Services/Order/Core/AuraShop.Order.Application/AuraShop.Order.Application.csproj", "Services/Order/Core/AuraShop.Order.Application/"]
COPY ["Services/Order/Core/AuraShop.Order.Domain/AuraShop.Order.Domain.csproj", "Services/Order/Core/AuraShop.Order.Domain/"]
COPY ["Services/Order/Infrastructure/AuraShop.Order.Persistence/AuraShop.Order.Persistence.csproj", "Services/Order/Infrastructure/AuraShop.Order.Persistence/"]
RUN dotnet restore "./Services/Order/Presentation/AuraShop.Order.WebApi/AuraShop.Order.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/Order/Presentation/AuraShop.Order.WebApi"
RUN dotnet build "./AuraShop.Order.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuraShop.Order.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuraShop.Order.WebApi.dll"]