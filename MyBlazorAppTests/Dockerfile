FROM yehudak/standalone-chrome-dotnet:sdk-7.0
WORKDIR /app
COPY . .

# ENV HTTP_PROXY=http://192.168.176.80:8080
# ENV HTTPS_PROXY=http://192.168.176.80:8080


ENTRYPOINT ["dotnet", "test"]