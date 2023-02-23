FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/IdentityServer4/BeforeTheScholarship.IdentityServer/BeforeTheScholarship.IdentityServer.csproj", "Systems/IdentityServer4/BeforeTheScholarship.IdentityServer/"]
COPY ["Data/BeforeTheScholarship.Context/BeforeTheScholarship.Context.csproj", "Data/BeforeTheScholarship.Context/"]
COPY ["Shared/BeforeTheScholarship.Settings/BeforeTheScholarship.Settings.csproj", "Shared/BeforeTheScholarship.Settings/"]
COPY ["Data/BeforeTheScholarship.Entities/BeforeTheScholarship.Entities.csproj", "Data/BeforeTheScholarship.Entities/"]
COPY ["Shared/BeforeTheScholarship.Common/BeforeTheScholarship.Common.csproj", "Shared/BeforeTheScholarship.Common/"]
RUN dotnet restore "Systems/IdentityServer4/BeforeTheScholarship.IdentityServer/BeforeTheScholarship.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Systems/IdentityServer4/BeforeTheScholarship.IdentityServer"
RUN dotnet build "BeforeTheScholarship.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeforeTheScholarship.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeforeTheScholarship.IdentityServer.dll"]