FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Web/BeforeTheScholarship.Web/BeforeTheScholarship.Web.csproj", "Systems/Web/BeforeTheScholarship.Web/BeforeTheScholarship.Web.csproj"]
RUN dotnet restore "Systems/Web/BeforeTheScholarship.Web/BeforeTheScholarship.Web.csproj"
COPY . .
WORKDIR "/src/Systems/Web/BeforeTheScholarship.Web"
RUN dotnet build "BeforeTheScholarship.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeforeTheScholarship.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY Systems/Web/BeforeTheScholarship.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
