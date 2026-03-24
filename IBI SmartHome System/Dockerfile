# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Tell .NET to listen on port 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["IBI SmartHome System/IBI SmartHome System.csproj", "IBI SmartHome System/"]
COPY ["IBI SmartHome System.Data/IBI SmartHome System.Data.csproj", "IBI SmartHome System.Data/"]
COPY ["IBI SmartHome System.Service/IBI SmartHome System.Service.csproj", "IBI SmartHome System.Service/"]

# Restore dependencies
RUN dotnet restore "./IBI SmartHome System/IBI SmartHome System.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/IBI SmartHome System"
RUN dotnet build "./IBI SmartHome System.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IBI SmartHome System.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IBI SmartHome System.dll"]