FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files for dependency resolution
COPY ["src/MovieTicket.Api/MovieTicket.Api.csproj", "src/MovieTicket.Api/"]
COPY ["src/MovieTicket.Application/MovieTicket.Application.csproj", "src/MovieTicket.Application/"]
COPY ["src/MovieTicket.Infrastructure/MovieTicket.Infrastructure.csproj", "src/MovieTicket.Infrastructure/"]
COPY ["src/MovieTicket.Domain/MovieTicket.Domain.csproj", "src/MovieTicket.Domain/"]

# Restore dependencies
RUN dotnet restore "src/MovieTicket.Api/MovieTicket.Api.csproj"

# Copy the entire source code
COPY src/ src/

# Build the application
WORKDIR "/src/src/MovieTicket.Api"
RUN dotnet build "MovieTicket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MovieTicket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieTicket.Api.dll"]
