#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/API/APP.csproj", "src/API/"]
COPY ["src/Modules/SecureToken/SI.Infrastructure/SI.Infrastructure.csproj", "src/Modules/SecureToken/SI.Infrastructure/"]
COPY ["src/Modules/SecureToken/SI.Application/SI.Application.csproj", "src/Modules/SecureToken/SI.Application/"]
COPY ["src/Modules/Core/Core.Application/Core.Application.csproj", "src/Modules/Core/Core.Application/"]
COPY ["src/Modules/SecureToken/SI.Domain/SI.Domain.csproj", "src/Modules/SecureToken/SI.Domain/"]
RUN dotnet restore "src/API/APP.csproj"
COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "APP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "APP.dll"]