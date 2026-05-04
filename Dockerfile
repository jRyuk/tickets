FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tickets/Tickets.csproj", "Tickets/"]
COPY ["Tickets.Application/Tickets.Application.csproj", "Tickets.Application/"]
COPY ["Tickets.Domain/Tickets.Domain.csproj", "Tickets.Domain/"]
COPY ["Tickets.Infrastructure/Tickets.Infrastructure.csproj", "Tickets.Infrastructure/"]
RUN dotnet restore "Tickets/Tickets.csproj"
COPY . .
WORKDIR "/src/Tickets"
RUN dotnet build "Tickets.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish 
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Tickets.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tickets.dll"]