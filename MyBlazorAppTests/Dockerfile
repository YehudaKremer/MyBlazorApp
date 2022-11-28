FROM selenium/standalone-chrome:latest
WORKDIR /app
COPY . .

# ENV HTTP_PROXY=http://192.168.176.80:8080
# ENV HTTPS_PROXY=http://192.168.176.80:8080

# install dotnet
RUN sudo wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN sudo dpkg -i packages-microsoft-prod.deb
RUN sudo apt-get update
RUN sudo apt-get install -y dotnet-sdk-7.0

ENTRYPOINT ["dotnet", "test"]