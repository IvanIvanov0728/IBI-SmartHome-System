# 1. SDK Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy project files using EXACT names from your screenshot
# Note the quotes: they are mandatory because of the spaces
COPY ["IBI SmartHome System/IBI SmartHome System.csproj", "IBI SmartHome System/"]
COPY ["IBI SmartHome System.Data/IBI SmartHome System.Data.csproj", "IBI SmartHome System.Data/"]
COPY ["IBI SmartHome System.Service/IBI SmartHome System.Service.csproj", "IBI SmartHome System.Service/"]

# 3. Restore dependencies
RUN dotnet restore "IBI SmartHome System/IBI SmartHome System.csproj"

# 4. Copy everything else
COPY . .

# 5. Build - we use the full path to the project file
RUN dotnet build "IBI SmartHome System/IBI SmartHome System.csproj" -c Release -o /app/build /p:Nullable=disable

# 6. Publish
FROM build AS publish
RUN dotnet publish "IBI SmartHome System/IBI SmartHome System.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:Nullable=disable

# 7. Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Railway needs these
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "IBI SmartHome System.dll"]