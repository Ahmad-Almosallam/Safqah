#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Safqah.Opportunities.Consumer/Safqah.Opportunities.Consumer.csproj", "Safqah.Opportunities.Consumer/"]
COPY ["Safqah.Shared/Safqah.Shared.csproj", "Safqah.Shared/"]
COPY ["services/Safqah.Opportunities/Safqah.Opportunities.csproj", "services/Safqah.Opportunities/"]
RUN dotnet restore "Safqah.Opportunities.Consumer/Safqah.Opportunities.Consumer.csproj"
COPY . .
WORKDIR "/src/Safqah.Opportunities.Consumer"
RUN dotnet build "Safqah.Opportunities.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Safqah.Opportunities.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Safqah.Opportunities.Consumer.dll"]