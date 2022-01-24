#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Inventory/EShop.Inventory.API/EShop.Inventory.API.csproj", "Services/Inventory/EShop.Inventory.API/"]
RUN dotnet restore "Services/Inventory/EShop.Inventory.API/EShop.Inventory.API.csproj"
COPY . .
WORKDIR "/src/Services/Inventory/EShop.Inventory.API"
RUN dotnet build "EShop.Inventory.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Inventory.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Inventory.API.dll"]
