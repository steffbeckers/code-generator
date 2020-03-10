FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Test.API.csproj", "Test.API/"]
RUN dotnet restore "Test.API/Test.API.csproj"
COPY . "Test.API/"
WORKDIR "/src/Test.API"
RUN dotnet build "Test.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Test.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Test.API.dll"]
