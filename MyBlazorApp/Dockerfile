FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

ENV HTTP_PROXY=http://192.168.176.80:8080
ENV HTTPS_PROXY=http://192.168.176.80:8080

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "MyBlazorApp.dll"]