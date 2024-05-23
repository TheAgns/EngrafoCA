FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Copy everything
# COPY . ./
COPY ["./build", "."]
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

EXPOSE 80
EXPOSE 443

# Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /App
#COPY --from=build-env /App/out .

RUN ls -la /App
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
