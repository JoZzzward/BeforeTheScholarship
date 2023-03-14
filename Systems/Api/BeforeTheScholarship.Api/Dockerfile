FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
VOLUME /EmailPages /app/emailpages
COPY ["Systems/Api/BeforeTheScholarship.Api/EmailPages", "/app/emailpages"]
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/BeforeTheScholarship.Api/BeforeTheScholarship.Api.csproj", "Systems/Api/BeforeTheScholarship.Api/"]
COPY ["Data/BeforeTheScholarship.Context/BeforeTheScholarship.Context.csproj", "Data/BeforeTheScholarship.Context/"]
COPY ["Shared/BeforeTheScholarship.Settings/BeforeTheScholarship.Settings.csproj", "Shared/BeforeTheScholarship.Settings/"]
COPY ["Data/BeforeTheScholarship.Entities/BeforeTheScholarship.Entities.csproj", "Data/BeforeTheScholarship.Entities/"]
COPY ["Data/Migrations/BeforeTheScholarship.Context.Migrations.PostgreSQL/BeforeTheScholarship.Context.Migrations.PostgreSQL.csproj", "Data/Migrations/BeforeTheScholarship.Context.Migrations.PostgreSQL/"]
COPY ["Services/BeforeTheScholarship.Services.CacheService/BeforeTheScholarship.Services.CacheService.csproj", "Services/BeforeTheScholarship.Services.CacheService/"]
COPY ["Services/BeforeTheScholarship.Services.Settings/BeforeTheScholarship.Services.Settings.csproj", "Services/BeforeTheScholarship.Services.Settings/"]
COPY ["Services/BeforeTheScholarship.Services.DebtService/BeforeTheScholarship.Services.DebtService.csproj", "Services/BeforeTheScholarship.Services.DebtService/"]
COPY ["Services/BeforeTheScholarship.Services.Actions/BeforeTheScholarship.Services.Actions.csproj", "Services/BeforeTheScholarship.Services.Actions/"]
COPY ["Services/BeforeTheScholarship.Services.EmailSender/BeforeTheScholarship.Services.EmailSender.csproj", "Services/BeforeTheScholarship.Services.EmailSender/"]
COPY ["Shared/BeforeTheScholarship.Common/BeforeTheScholarship.Common.csproj", "Shared/BeforeTheScholarship.Common/"]
COPY ["Services/BeforeTheScholarship.Services.RabbitMqService/BeforeTheScholarship.Services.RabbitMqService.csproj", "Services/BeforeTheScholarship.Services.RabbitMqService/"]
COPY ["Services/BeforeTheScholarship.Services.StudentService/BeforeTheScholarship.Services.StudentService.csproj", "Services/BeforeTheScholarship.Services.StudentService/"]
COPY ["Services/BeforeTheScholarship.Services.UserAccountService/BeforeTheScholarship.Services.UserAccountService.csproj", "Services/BeforeTheScholarship.Services.UserAccountService/"]
RUN dotnet restore "Systems/Api/BeforeTheScholarship.Api/BeforeTheScholarship.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/BeforeTheScholarship.Api"
RUN dotnet build "BeforeTheScholarship.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeforeTheScholarship.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeforeTheScholarship.Api.dll"]