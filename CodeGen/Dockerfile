FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim
EXPOSE 4200
EXPOSE 5001

# Install dotnet CLI tooling
RUN dotnet tool install -g dotnet-ef
RUN dotnet tool install -g dotnet-t4
ENV PATH="/root/.dotnet/tools:${PATH}"

# Update/upgrade apt dependencies
RUN apt-get update && \
apt-get upgrade -y

# Install git
RUN apt-get install -y make libssl-dev libghc-zlib-dev libcurl4-gnutls-dev libexpat1-dev gettext unzip
RUN git clone https://github.com/git/git.git && \
cd git && \
make prefix=/usr all && \
make prefix=/usr install && \
git config --global user.name "Code Generator" && \
git config --global user.email "code-generator@steffbeckers.eu" && \
git --version

# Install nodejs
RUN curl -fsSL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install -yq nodejs && \
node --version 

# Restore NuGet packages
WORKDIR /src
COPY ["CodeGen/CodeGen.csproj", "CodeGen/"]
COPY ["CodeGen.Models/CodeGen.Models.csproj", "CodeGen.Models/"]
COPY ["CodeGen.Framework/CodeGen.Framework.csproj", "CodeGen.Framework/"]
RUN dotnet restore "CodeGen/CodeGen.csproj"
COPY . .

WORKDIR "/src/CodeGen"

# Initialize git repo => for cleanup during regeneration
RUN git init && \
git add . && \
git commit -m "Initial commit" && \
git status

ENTRYPOINT ["dotnet", "watch", "run"]
