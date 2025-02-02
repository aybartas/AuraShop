#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Cargo/AuraShop.Cargo.WebAPI/AuraShop.Cargo.WebAPI.csproj", "Services/Cargo/AuraShop.Cargo.WebAPI/"]
COPY ["Services/Cargo/AuraShop.Cargo.Business/AuraShop.Cargo.Business.csproj", "Services/Cargo/AuraShop.Cargo.Business/"]
COPY ["Services/Cargo/AuraShop.Cargo.DataAccess/AuraShop.Cargo.DataAccess.csproj", "Services/Cargo/AuraShop.Cargo.DataAccess/"]
COPY ["Services/Cargo/AuraShop.Cargo.Entity/AuraShop.Cargo.Entity.csproj", "Services/Cargo/AuraShop.Cargo.Entity/"]
COPY ["Services/Cargo/AuraShop.Cargo.Dto/AuraShop.Cargo.Dto.csproj", "Services/Cargo/AuraShop.Cargo.Dto/"]
RUN dotnet restore "./Services/Cargo/AuraShop.Cargo.WebAPI/AuraShop.Cargo.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Cargo/AuraShop.Cargo.WebAPI"
RUN dotnet build "./AuraShop.Cargo.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuraShop.Cargo.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuraShop.Cargo.WebAPI.dll"]