FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CodeGen.API/CodeGen.API.csproj", "CodeGen.API/"]
COPY ["CodeGen.Models/CodeGen.Models.csproj", "CodeGen.Models/"]
COPY ["CodeGen.Framework/CodeGen.Framework.csproj", "CodeGen.Framework/"]
RUN dotnet restore "CodeGen.API/CodeGen.API.csproj"
COPY . .
WORKDIR "/src/CodeGen.API"
RUN dotnet build "CodeGen.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeGen.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeGen.API.dll"]
