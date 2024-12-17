FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5129

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SportMaster.API/SportMaster.API.csproj", "SportMaster.API/"]
COPY ["SportMaster.DAL/SportMaster.DAL.csproj", "SportMaster.DAL/"]
COPY ["SportMaster.Domain/SportMaster.Domain.csproj", "SportMaster.Domain/"]
COPY ["SportMaster.BLL/SportMaster.BLL.csproj", "SportMaster.BLL/"]
RUN dotnet restore "SportMaster.API/SportMaster.API.csproj"
COPY . .
WORKDIR "/src/SportMaster.API"
RUN dotnet build "SportMaster.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SportMaster.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportMaster.API.dll"]
