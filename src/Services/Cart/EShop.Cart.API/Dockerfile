#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Cart/EShop.Cart.API/EShop.Cart.API.csproj", "Services/Cart/EShop.Cart.API/"]
COPY ["../lib/Serializer/Serializer.csproj", "../lib/Serializer/"]
RUN dotnet restore "Services/Cart/EShop.Cart.API/EShop.Cart.API.csproj"
COPY . .
WORKDIR "/src/Services/Cart/EShop.Cart.API"
RUN dotnet build "EShop.Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Cart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Cart.API.dll"]
