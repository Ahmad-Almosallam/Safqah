#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["services/Safqah.Wallet/Safqah.Wallet.csproj", "services/Safqah.Wallet/"]
COPY ["Safqah.Shared/Safqah.Shared.csproj", "Safqah.Shared/"]
RUN dotnet restore "services/Safqah.Wallet/Safqah.Wallet.csproj"
COPY . .
WORKDIR "/src/services/Safqah.Wallet"
RUN dotnet build "Safqah.Wallet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Safqah.Wallet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Safqah.Wallet.dll"]