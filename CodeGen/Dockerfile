FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim
EXPOSE 4200
EXPOSE 5001

RUN dotnet tool install -g dotnet-ef
RUN dotnet tool install -g dotnet-t4
ENV PATH="/root/.dotnet/tools:${PATH}"

RUN apt-get update -y
RUN curl -fsSL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install -yq nodejs

WORKDIR /src
COPY ["CodeGen/CodeGen.csproj", "CodeGen/"]
COPY ["CodeGen.Models/CodeGen.Models.csproj", "CodeGen.Models/"]
COPY ["CodeGen.Framework/CodeGen.Framework.csproj", "CodeGen.Framework/"]
RUN dotnet restore "CodeGen/CodeGen.csproj"
COPY . .

WORKDIR "/src/CodeGen"
ENTRYPOINT ["dotnet", "watch", "run"]
