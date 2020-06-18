FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LoginRadius.WeatherApp/LoginRadius.WeatherApp.csproj", "LoginRadius.WeatherApp/"]
COPY ["LoginRadius.WeatherApp.Core/LoginRadius.WeatherApp.Core.csproj", "LoginRadius.WeatherApp.Core/"]
COPY ["LoginRadius.WeatherApp.Models/LoginRadius.WeatherApp.Models.csproj", "LoginRadius.WeatherApp.Models/"]
COPY ["LoginRadius.WeatherApp.WeatherProvider.OpenWeatherMap/LoginRadius.WeatherApp.WeatherProvider.OpenWeatherMap.csproj", "LoginRadius.WeatherApp.WeatherProvider.OpenWeatherMap/"]
RUN dotnet restore "LoginRadius.WeatherApp/LoginRadius.WeatherApp.csproj"
COPY . .
WORKDIR "/src/LoginRadius.WeatherApp"
RUN dotnet build "LoginRadius.WeatherApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoginRadius.WeatherApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -la
ENTRYPOINT ["dotnet", "LoginRadius.WeatherApp.dll"]