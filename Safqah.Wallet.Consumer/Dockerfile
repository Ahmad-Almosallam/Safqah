#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Safqah.Wallet.Consumer/Safqah.Wallet.Consumer.csproj", "Safqah.Wallet.Consumer/"]
COPY ["Safqah.Shared/Safqah.Shared.csproj", "Safqah.Shared/"]
COPY ["services/Safqah.Wallet/Safqah.Wallet.csproj", "services/Safqah.Wallet/"]
RUN dotnet restore "Safqah.Wallet.Consumer/Safqah.Wallet.Consumer.csproj"
COPY . .
WORKDIR "/src/Safqah.Wallet.Consumer"
RUN dotnet build "Safqah.Wallet.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Safqah.Wallet.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Safqah.Wallet.Consumer.dll"]