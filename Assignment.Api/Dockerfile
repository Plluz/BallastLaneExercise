FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Assignment.Api/Assignment.Api.csproj", "Assignment.Api/"]
COPY ["Assignment.Domain/Assignment.Domain.csproj", "Assignment.Domain/"]
COPY ["Assignment.Infrastructure/Assignment.Infrastructure.csproj", "Assignment.Infrastructure/"]
RUN dotnet restore "./Assignment.Api/./Assignment.Api.csproj"
COPY . .
WORKDIR "/src/Assignment.Api"
RUN dotnet build "./Assignment.Api.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Assignment.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assignment.Api.dll"]