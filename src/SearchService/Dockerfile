# Stage 1: Build the application using the .NET 10 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy solution and project files first to leverage Docker layer caching
COPY FjordFlowHub.sln ./
COPY src/SearchService/SearchService.csproj ./src/SearchService/
COPY src/Contracts/Contracts.csproj ./src/Contracts/

# Restore dependencies
RUN dotnet restore ./src/SearchService/SearchService.csproj

# Copy the rest of the source code
COPY . .

# Build and publish the application in Release mode
RUN dotnet publish ./src/SearchService/SearchService.csproj -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Standard web ports
EXPOSE 80
EXPOSE 443

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "SearchService.dll"]