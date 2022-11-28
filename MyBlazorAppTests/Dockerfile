FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
WORKDIR /app
COPY *.csproj ./

ENV HTTP_PROXY=http://192.168.176.80:8080
ENV HTTPS_PROXY=http://192.168.176.80:8080

RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM selenium/standalone-chrome:latest
WORKDIR /app
COPY --from=publish /app/out .

ENV HTTP_PROXY=http://192.168.176.80:8080
ENV HTTPS_PROXY=http://192.168.176.80:8080

# install dotnet
RUN sudo apt-get update && \
    sudo apt-get install -y dotnet7

ENTRYPOINT ["dotnet", "test"]