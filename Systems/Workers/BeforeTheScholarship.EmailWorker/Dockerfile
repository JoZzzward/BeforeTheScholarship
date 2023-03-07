FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
VOLUME /EmailTask/EmailContentPages /app/emailpages
COPY ["Systems/Workers/BeforeTheScholarship.EmailWorker/EmailTask/EmailContentPages","/app/emailpages"]
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Workers/BeforeTheScholarship.EmailWorker/BeforeTheScholarship.EmailWorker.csproj", "Systems/Workers/BeforeTheScholarship.EmailWorker/"]
COPY ["Data/BeforeTheScholarship.Context/BeforeTheScholarship.Context.csproj", "Data/BeforeTheScholarship.Context/"]
COPY ["Shared/BeforeTheScholarship.Settings/BeforeTheScholarship.Settings.csproj", "Shared/BeforeTheScholarship.Settings/"]
COPY ["Data/BeforeTheScholarship.Entities/BeforeTheScholarship.Entities.csproj", "Data/BeforeTheScholarship.Entities/"]
COPY ["Services/BeforeTheScholarship.DebtService/BeforeTheScholarship.DebtService.csproj", "Services/BeforeTheScholarship.DebtService/"]
COPY ["Services/BeforeTheScholarship.EmailSender/BeforeTheScholarship.EmailSender.csproj", "Services/BeforeTheScholarship.EmailSender/"]
COPY ["Services/BeforeTheScholarship.StudentService/BeforeTheScholarship.StudentService.csproj", "Services/BeforeTheScholarship.StudentService/"]
RUN dotnet restore "Systems/Workers/BeforeTheScholarship.EmailWorker/BeforeTheScholarship.EmailWorker.csproj"
COPY . .
WORKDIR "/src/Systems/Workers/BeforeTheScholarship.EmailWorker"
RUN dotnet build "BeforeTheScholarship.EmailWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeforeTheScholarship.EmailWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeforeTheScholarship.EmailWorker.dll"]