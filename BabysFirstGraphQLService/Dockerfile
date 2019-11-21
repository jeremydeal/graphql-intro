FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["BabysFirstGraphQLService/BabysFirstGraphQLService.csproj", "BabysFirstGraphQLService/"]
COPY ["BabysFirstGraphQLService.Data/BabysFirstGraphQLService.Data.csproj", "BabysFirstGraphQLService.Data/"]
RUN dotnet restore "BabysFirstGraphQLService/BabysFirstGraphQLService.csproj"
COPY . .
WORKDIR "/src/BabysFirstGraphQLService"
RUN dotnet build "BabysFirstGraphQLService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BabysFirstGraphQLService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BabysFirstGraphQLService.dll"]
