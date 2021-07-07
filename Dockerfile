FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy projects
COPY PLX.API ./PLX.API
COPY PLX.Persistence ./PLX.Persistence
COPY PLX.Persistence.EF ./PLX.Persistence.EF

# Restore PLX API
WORKDIR /app/PLX.API
RUN dotnet restore

# Publish application and its dependencies
WORKDIR /app
RUN dotnet publish PLX.API -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

# Run the app on container startup
# Use your project name for the second parameter
# e.g. MyProject.dll
#ENTRYPOINT [ "dotnet", "PLX.API.dll" ]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PLX.API.dll