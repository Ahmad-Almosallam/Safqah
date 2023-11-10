#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["services/Safqah.Investor/Safqah.Investors.csproj", "services/Safqah.Investor/"]
RUN dotnet restore "services/Safqah.Investor/Safqah.Investors.csproj"
COPY . .
WORKDIR "/src/services/Safqah.Investor"
RUN dotnet build "Safqah.Investors.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Safqah.Investors.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Safqah.Investors.dll"]