#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
#WORKDIR /src

# Copy everything
#COPY . ./

# Restore as distinct layers
#RUN dotnet restore
# Build and publish a release
#RUN dotnet publish -c Release -o out

#EXPOSE 80
#EXPOSE 443

# Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /App
#COPY --from=build-env /App/out .

# RUN ls -la /App
#ENTRYPOINT ["dotnet", "/src/WebUI/WebUI/bin/Release/net8.0/WebUI.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR ./
COPY ["WebUI.csproj", "WebUI/"]
COPY ["Application.csproj", "Application/"]
COPY ["Domain.csproj", "Domain/"]
COPY ["Infrastructure.csproj", "Infrastructure/"]


RUN dotnet restore "WebUI.csproj"
COPY . ../
WORKDIR /WebUI
RUN dotnet build "WebUI.csproj" -c Release -o /app/build
FROM build-env AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
