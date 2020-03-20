FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["RJM.API.csproj", "RJM.API/"]
RUN dotnet restore "RJM.API/RJM.API.csproj"
COPY . "RJM.API/"
WORKDIR "/src/RJM.API"
RUN dotnet build "RJM.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RJM.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RJM.API.dll"]
