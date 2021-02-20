FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim
EXPOSE 5001

RUN dotnet tool install -g dotnet-ef
RUN dotnet tool install -g dotnet-t4
ENV PATH="/root/.dotnet/tools:${PATH}"

WORKDIR /src
COPY ["CodeGen.csproj", ""]
RUN dotnet restore "./CodeGen.csproj"
COPY . .

WORKDIR "/src/."
ENTRYPOINT ["dotnet", "watch", "run"]
