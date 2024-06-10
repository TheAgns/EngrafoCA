# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY ./WebUI/WebUI.csproj ./WebUI/
COPY ./Application/Application.csproj ./Application/
COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY ./Domain/Domain.csproj ./Domain/

RUN dotnet restore ./WebUI/WebUI.csproj

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish ./WebUI/WebUI.csproj -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port the application runs on
EXPOSE 81
EXPOSE 443

# Set environment variable to listen on port 81
ENV ASPNETCORE_URLS=http://+:81

# Set the entry point to run the application (Without db migrations)
ENTRYPOINT ["dotnet", "WebUI.dll"]
