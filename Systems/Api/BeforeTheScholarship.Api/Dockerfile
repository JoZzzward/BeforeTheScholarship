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
COPY ["Services/BeforeTheScholarship.DebtService/BeforeTheScholarship.DebtService.csproj", "Services/BeforeTheScholarship.DebtService/"]
COPY ["Services/BeforeTheScholarship.Settings/BeforeTheScholarship.Services.Settings.csproj", "Services/BeforeTheScholarship.Settings/"]
COPY ["Services/BeforeTheScholarship.StudentService/BeforeTheScholarship.StudentService.csproj", "Services/BeforeTheScholarship.StudentService/"]
COPY ["Services/BeforeTheScholarship.UserAccountService/BeforeTheScholarship.UserAccountService.csproj", "Services/BeforeTheScholarship.UserAccountService/"]
COPY ["Shared/BeforeTheScholarship.Common/BeforeTheScholarship.Common.csproj", "Shared/BeforeTheScholarship.Common/"]
COPY ["Services/BeforeTheScholarship.EmailSender/BeforeTheScholarship.EmailSender.csproj", "Services/BeforeTheScholarship.EmailSender/"]
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