FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# Set the working directory witin the container
WORKDIR /build

# Copy the sln and csproj files. These are the only files
# required in order to restore
COPY ./src/InvoiceGenerator.BusinessLogic/InvoiceGenerator.BusinessLogic.csproj ./src/InvoiceGenerator.BusinessLogic/InvoiceGenerator.BusinessLogic.csproj
COPY ./src/InvoiceGenerator.Domain/InvoiceGenerator.Domain.csproj ./src/InvoiceGenerator.Domain/InvoiceGenerator.Domain.csproj
COPY ./src/InvoiceGenerator.ViewModels/InvoiceGenerator.ViewModels.csproj ./src/InvoiceGenerator.ViewModels/InvoiceGenerator.ViewModels.csproj
COPY ./src/InvoiceGenerator.WebApi/InvoiceGenerator.WebApi.csproj ./src/InvoiceGenerator.WebApi/InvoiceGenerator.WebApi.csproj

COPY ./tests/InvoiceGenerator.BusinessLogicTests/InvoiceGenerator.BusinessLogicTests.csproj ./tests/InvoiceGenerator.BusinessLogicTests/InvoiceGenerator.BusinessLogicTests.csproj 
COPY ./tests/InvoiceGenerator.WebApi.IntegrationTests/InvoiceGenerator.WebApi.IntegrationTests.csproj ./tests/InvoiceGenerator.WebApi.IntegrationTests/InvoiceGenerator.WebApi.IntegrationTests.csproj 

# Restore all packages
RUN dotnet restore ./src/InvoiceGenerator.WebApi/InvoiceGenerator.WebApi.csproj --force --no-cache

# Copy the remaining source
COPY ./src/InvoiceGenerator.BusinessLogic/ ./src/InvoiceGenerator.BusinessLogic/
COPY ./src/InvoiceGenerator.Domain/ ./src/InvoiceGenerator.Domain/
COPY ./src/InvoiceGenerator.ViewModels/ ./src/InvoiceGenerator.ViewModels/
COPY ./src/InvoiceGenerator.WebApi/ ./src/InvoiceGenerator.WebApi/

COPY ./tests/InvoiceGenerator.BusinessLogicTests/ ./tests/InvoiceGenerator.BusinessLogicTests/ 
COPY ./tests/InvoiceGenerator.WebApi.IntegrationTests/ ./tests/InvoiceGenerator.WebApi.IntegrationTests/
COPY ./InvoiceGenerator.WebApi.sln ./InvoiceGenerator.WebApi.sln

# Build the source code
RUN dotnet build ./src/InvoiceGenerator.WebApi/InvoiceGenerator.WebApi.csproj --configuration Release --no-restore

# # Run all tests
RUN dotnet test

# Publish application
RUN dotnet publish ./src/InvoiceGenerator.WebApi/InvoiceGenerator.WebApi.csproj --configuration Release --no-restore --no-build --output "./dist"

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS app
WORKDIR /app
COPY --from=build /build/dist .
ENV ASPNETCORE_URLS http://+:5000

ENTRYPOINT ["dotnet", "InvoiceGenerator.WebApi.dll"]
